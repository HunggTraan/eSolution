using eShopSolution.Utilities.Exceptions;
using eSolutionTech.Data.EF;
using eSolutionTech.Data.Entities;
using eSolutionTech.ViewModels.Catalog.Projects;
using eSolutionTech.ViewModels.Catalog.ShiftSettings;
using eSolutionTech.ViewModels.Common;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace eSolutionTech.ViewModels.Catalog.Shifts
{
  public class ShiftService : IShiftService
  {
    private readonly eTechDbContext _context;
    private readonly IProjectService _projectService;
    private readonly IShiftSettingService _shiftSettingService;
    public ShiftService(eTechDbContext context, IProjectService projectService, IShiftSettingService shiftSettingService)
    {
      _context = context;
      _projectService = projectService;
      _shiftSettingService = shiftSettingService;
    }
    public async Task<int> LoginShift(ShiftCreateRequest request)
    {
      var projectInfo = await _projectService.GetById(Int32.Parse(request.ProjectId));
      var shiftSetting = await _shiftSettingService.GetById(projectInfo.shiftSettingId);

      var shift = GetAll(request.UserId);

      if (shift == null)
      {
        var dateCompare = DateTime.Now.ToString("yyyy-MM-dd");
        var dateInSettingExceed = string.Format("{0} {1}:00", dateCompare, shiftSetting.TimeIn);
        var dateInSetting = string.Format("{0} 00:00:00", dateCompare, shiftSetting.TimeIn);

        DateTime TimeInSetting = DateTime.ParseExact(dateInSetting, "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture);
        DateTime TimeInSettingExceed = DateTime.ParseExact(dateInSettingExceed, "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture);

        DateTime dateIn = DateTime.Now;

        if (shiftSetting.ExceedTimeIn > 0)
        {
          if (dateIn < TimeInSetting)
            return 3; // Chưa đến giờ chấm công
          else if (TimeInSetting < dateIn && dateIn < TimeInSettingExceed)
          {
            var shiftRequest = new Shift()
            {
              ProjectId = Int32.Parse(request.ProjectId),
              UserId = request.UserId,
              TimeIn = dateIn,
              isLate = 0,
              Status = 1,
              TimeOut = dateIn,
              WorkingHours = "0 giờ"
            };

            _context.Shifts.Add(shiftRequest);
            return await _context.SaveChangesAsync();
          }
          else if (dateIn > TimeInSettingExceed)
          {
            var shiftRequest = new Shift()
            {
              ProjectId = Int32.Parse(request.ProjectId),
              UserId = request.UserId,
              TimeIn = dateIn,
              isLate = 1,
              Status = 1,
              TimeOut = dateIn,
              WorkingHours = "0 giờ"
            };

            _context.Shifts.Add(shiftRequest);
            return await _context.SaveChangesAsync();
          }
        }
        else
        {
          var shiftRequest = new Shift()
          {
            ProjectId = Int32.Parse(request.ProjectId),
            UserId = request.UserId,
            TimeIn = dateIn,
            isLate = 0,
            Status = 1,
            TimeOut = dateIn,
            WorkingHours = "0 giờ"
          };
          _context.Shifts.Add(shiftRequest);
          return await _context.SaveChangesAsync();
        }
        return 0;
      }
      else return 2;
    }

    public async Task<int> Delete(int shiftId)
    {
      var shift = await _context.Shifts.FindAsync(shiftId);
      if (shift == null)
        throw new eTechException($"Không tìm thấy ca làm việc với id là {shiftId}");
      _context.Shifts.Remove(shift);
      return await _context.SaveChangesAsync();
    }

    public ShiftViewModel GetAll(string userId)
    {
      try
      {
        var query = from shift in _context.Shifts
                    select new { shift };

        if (!string.IsNullOrEmpty(userId))
        {
          query = query.Where(x => x.shift.UserId == userId);
        }

        var data = query.Select(x => new ShiftViewModel()
        {
          Id = x.shift.Id,
          ProjectId = x.shift.ProjectId,
          UserId = x.shift.UserId,
          WorkingHours = x.shift.WorkingHours,
          TimeIn = x.shift.TimeIn,
          TimeOut = x.shift.TimeOut,
          isLate = x.shift.isLate,
          Status = x.shift.Status
        }).OrderByDescending(x => x.Id).FirstOrDefault();

        if (data != null)
        {
          if (data.TimeIn.ToString("dd/MM/yyyy") == DateTime.Now.ToString("dd/MM/yyyy"))
          {
            return data;
          }
          else
            return null;
        }
        return null;
      }
      catch (Exception ex)
      {
        return null;
      }
    }

    public async Task<PagedResult<ShiftManageViewModel>> GetAllPaging(GetShiftPagingRequest request)
    {
      var query = from shift in _context.Shifts
                  join project in _context.Projects on shift.ProjectId equals project.Id
                  join user in _context.Users on shift.UserId equals user.Id.ToString()
                  join department in _context.Departments on user.DepartmentId equals department.Id.ToString()
                  join jobTitle in _context.JobTitles on user.JobTitleId equals jobTitle.Id.ToString()
                  select new { shift, project, user, department, jobTitle };

      if (!string.IsNullOrEmpty(request.UserId))
      {
        query = query.Where(x => x.shift.UserId == request.UserId);
      }

      if (request.ProjectId > 0)
      {
        query = query.Where(x => x.shift.ProjectId == request.ProjectId);
      }

      query = query.OrderByDescending(x => x.shift.TimeIn);

      int totalRow = await query.CountAsync();

      var data = await query.Skip((request.PageIndex - 1) * request.PageSize)
          .Take(request.PageSize)
          .Select(x => new ShiftManageViewModel()
          {
            Id = x.shift.Id,
            ProjectId = x.shift.ProjectId,
            UserId = x.shift.UserId,
            WorkingHours = x.shift.WorkingHours,
            isLate = x.shift.isLate,
            Status = x.shift.Status,
            TimeIn = x.shift.TimeIn,
            TimeOut = x.shift.TimeOut,
            ProjectName = x.project.Name,
            ProjectCode = x.project.Code,
            UserName = x.user.FullName,
            UserCode = x.user.Code,
            DepartmentCode = x.department.Code,
            JobTitleCode = x.jobTitle.Code,
            DepartmentName = x.department.Name,
            JobTitleName = x.jobTitle.Name
          }).ToListAsync();

      var pagedResult = new PagedResult<ShiftManageViewModel>()
      {
        TotalRecords = totalRow,
        Items = data
      };
      return pagedResult;
    }

    public async Task<ShiftViewModel> GetById(int shiftId)
    {
      var shift = await _context.Shifts.FindAsync(shiftId);

      var shiftViewModel = new ShiftViewModel()
      {
        Id = shift.Id,
        ProjectId = shift.ProjectId,
        UserId = shift.UserId,
        WorkingHours = shift.WorkingHours
      };
      return shiftViewModel;
    }

    public async Task<int> LogoutShift(ShiftUpdateRequest request)
    {
      try
      {
        var projectInfo = await _projectService.GetById(Int32.Parse(request.ProjectId));
        var shiftSetting = await _shiftSettingService.GetById(projectInfo.shiftSettingId);

        var shift = GetAll(request.UserId);

        if (shift != null)
        {
          if (shift.ProjectId.ToString() != request.ProjectId)
          {
            return 4;// khác project
          }
          var dateCompare = DateTime.Now.ToString("yyyy-MM-dd");
          var dateOutSetting = string.Format("{0} {1}:00", dateCompare, shiftSetting.TimeOut);
          DateTime TimeOutSetting = DateTime.ParseExact(dateOutSetting, "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture);

          if (DateTime.Now < TimeOutSetting)
          {
            return 3;
          }
          shift.Status = 2;
          shift.TimeOut = DateTime.Now;
          var duration = shift.TimeOut - shift.TimeIn;
          shift.WorkingHours = string.Format("{0} {1}", (double)duration.TotalHours, "giờ");

          return await _context.SaveChangesAsync();
        }
        return 2;
      }
      catch (Exception ex)
      {
        return 0;
      }
    }
  }
}
