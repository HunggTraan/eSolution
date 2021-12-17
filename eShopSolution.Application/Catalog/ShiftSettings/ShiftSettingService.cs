using eShopSolution.Utilities.Exceptions;
using eSolutionTech.Data.EF;
using eSolutionTech.Data.Entities;
using eSolutionTech.ViewModels.Catalog.Projects;
using eSolutionTech.ViewModels.Common;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eSolutionTech.ViewModels.Catalog.ShiftSettings
{
  public class ShiftSettingService : IShiftSettingService
  {
    private readonly eTechDbContext _context;
    private readonly IProjectService _projectService;
    public ShiftSettingService(eTechDbContext context)
    {
      _context = context;
    }
    public async Task<int> Create(ShiftSettingCreateRequest request)
    {

      //if (request.TimeIn > request.TimeOut)
      //{
      //  throw new eTechException($"Giờ vào không được lớn hơn giờ ra!");
      //}

      var shiftSetting = new ShiftSetting()
      {
        Code = request.Code,
        Name = request.Name,
        TimeIn = request.TimeIn,
        TimeOut = request.TimeOut,
        ExceedTimeIn = request.ExceedTimeIn,
        ExceedTimeOut = request.ExceedTimeOut,
      };
      _context.ShiftSettings.Add(shiftSetting);
      return await _context.SaveChangesAsync();
    }

    public async Task<int> Delete(int shiftSettingId)
    {
      var ShiftSetting = await _context.ShiftSettings.FindAsync(shiftSettingId);
      if (ShiftSetting == null)
        throw new eTechException($"Không tìm thấy cài đặt ca làm việc với id là {shiftSettingId}");
      _context.ShiftSettings.Remove(ShiftSetting);
      return await _context.SaveChangesAsync();
    }

    public async Task<List<ShiftSettingViewModel>> GetAll()
    {
      var query = from shiftSetting in _context.ShiftSettings
                  select new { shiftSetting };

      var data = await query
      .Select(x => new ShiftSettingViewModel()
      {
        Id = x.shiftSetting.Id,
        Code = x.shiftSetting.Code,
        Name = x.shiftSetting.Name,
        TimeIn = x.shiftSetting.TimeIn,
        TimeOut = x.shiftSetting.TimeOut,
        ExceedTimeIn = x.shiftSetting.ExceedTimeIn,
        ExceedTimeOut = x.shiftSetting.ExceedTimeOut,
      }).ToListAsync();

      if (data != null)
        return data;
      else return new List<ShiftSettingViewModel>();
    }

    public async Task<PagedResult<ShiftSettingViewModel>> GetAllPaging(GetShiftSettingPagingRequest request)
    {
      var query = from shiftSetting in _context.ShiftSettings
                  select new { shiftSetting };

      if (!string.IsNullOrEmpty(request.KeyWord))
      {
        query = query.Where(x => x.shiftSetting.Code == request.KeyWord || x.shiftSetting.Name == request.KeyWord);
      }

      int totalRow = await query.CountAsync();

      var data = await query.Skip((request.PageIndex - 1) * request.PageSize)
          .Take(request.PageSize)
          .Select(x => new ShiftSettingViewModel()
          {
            Id = x.shiftSetting.Id,
            Code = x.shiftSetting.Code,
            Name = x.shiftSetting.Name,
            TimeIn = x.shiftSetting.TimeIn,
            TimeOut = x.shiftSetting.TimeOut,
            ExceedTimeIn = x.shiftSetting.ExceedTimeIn,
            ExceedTimeOut = x.shiftSetting.ExceedTimeOut,
          }).ToListAsync();

      var pagedResult = new PagedResult<ShiftSettingViewModel>()
      {
        TotalRecords = totalRow,
        Items = data
      };
      return pagedResult;
    }

    public async Task<ShiftSettingViewModel> GetById(int shiftSettingId)
    {
      var shiftSetting = await _context.ShiftSettings.FindAsync(shiftSettingId);

      var shiftSettingViewModel = new ShiftSettingViewModel()
      {
        Id = shiftSetting.Id,
        Code = shiftSetting.Code,
        Name = shiftSetting.Name,
        TimeIn = shiftSetting.TimeIn,
        TimeOut = shiftSetting.TimeOut,
        ExceedTimeIn = shiftSetting.ExceedTimeIn,
        ExceedTimeOut = shiftSetting.ExceedTimeOut
      };
      return shiftSettingViewModel;
    }

    public async Task<int> Update(ShiftSettingUpdateRequest request)
    {
      var shiftSetting = await _context.ShiftSettings.FindAsync(request.Id);
      if (shiftSetting == null) throw new eTechException($"Không tìm thấy cài đặt ca làm việc với id: {request.Id}");

      shiftSetting.Code = request.Code;
      shiftSetting.Name = request.Name;
      shiftSetting.TimeIn = request.TimeIn;
      shiftSetting.TimeOut = request.TimeOut;
      shiftSetting.ExceedTimeIn = request.ExceedTimeIn;
      shiftSetting.ExceedTimeOut = request.ExceedTimeOut;

      return await _context.SaveChangesAsync();
    }
  }
}
