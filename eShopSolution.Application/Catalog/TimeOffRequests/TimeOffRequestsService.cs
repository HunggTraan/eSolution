using eShopSolution.Utilities.Exceptions;
using eSolutionTech.Data.EF;
using eSolutionTech.Data.Entities;
using eSolutionTech.ViewModels.Catalog.TimeOffRequests;
using eSolutionTech.ViewModels.Catalog.TimeOffRequests.Dtos;
using eSolutionTech.ViewModels.Common;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace eSolutionTech.Application.Catalog.TimeOffRequests
{
  public class TimeOffRequestsService : ITimeOffRequestsService
  {
    private readonly eTechDbContext _context;
    public TimeOffRequestsService(eTechDbContext context)
    {
      _context = context;
    }
    public async Task<int> Create(TimeOffCreateRequest request)
    {
      CultureInfo provider = CultureInfo.InvariantCulture;

      var timeOffRequest = new TimeOffRequest()
      {
        Name = request.Name,
        Description = request.Description,
        TimeOffType = request.TimeOffType,
        UserId = request.UserId,
        FromDate = request.FromDate,
        ToDate = request.ToDate,
        AdminNote = request.AdminNote,
        Duration = request.Duration,
        Status = request.Status
      };
      _context.TimeOffRequests.Add(timeOffRequest);
      return await _context.SaveChangesAsync();
    }

    public async Task<int> Delete(int timeOffRequestId)
    {
      try
      {
        var timeOffRequest = await _context.TimeOffRequests.FindAsync(timeOffRequestId);
        if (timeOffRequest == null)
          throw new eTechException($"Không tìm thấy ca làm việc với id là {timeOffRequestId}");
        _context.TimeOffRequests.Remove(timeOffRequest);
        return await _context.SaveChangesAsync();
      }
      catch (eTechException ex)
      {
        throw new eTechException($"Có lỗi xảy ra");
      }
    }

    public async Task<List<TimeOffViewModel>> GetAll()
    {
      var query = from timeOffRequest in _context.TimeOffRequests
                  join timeOffType in _context.TimeOffTypes on timeOffRequest.TimeOffType equals timeOffType.Id
                  select new { timeOffRequest, timeOffType };

      var data = await query
        .Select(x => new TimeOffViewModel()
        {
          Name = x.timeOffRequest.Name,
          Description = x.timeOffRequest.Description,
          TimeOffType = x.timeOffType.Name,
          UserId = x.timeOffRequest.UserId,
          FromDate = x.timeOffRequest.FromDate,
          ToDate = x.timeOffRequest.ToDate,
          AdminNote = x.timeOffRequest.AdminNote,
          Duration = x.timeOffRequest.Duration,
          Status = x.timeOffRequest.Status
        }).ToListAsync();

      if (data != null)
        return data;
      else return new List<TimeOffViewModel>();
    }

    public async Task<PagedResult<TimeOffViewModel>> GetAllPaging(TimeOffPagingRequest request)
    {
      try
      {
        var query = from timeOffRequest in _context.TimeOffRequests
                    join timeOffType in _context.TimeOffTypes on timeOffRequest.TimeOffType equals timeOffType.Id
                    select new { timeOffRequest, timeOffType };

        DateTime FromDate = DateTime.Now;
        DateTime ToDate = DateTime.Now;

        CultureInfo provider = CultureInfo.InvariantCulture;
        if (!string.IsNullOrEmpty(request.FromDate) || !string.IsNullOrEmpty(request.ToDate))
        {
          FromDate = DateTime.ParseExact(request.FromDate, Constants.Constants.DateTimeFormatDate, provider);
          ToDate = DateTime.ParseExact(request.ToDate, Constants.Constants.DateTimeFormatDate, provider);
        }

        if (request.UserId != null || request.UserId != Guid.Empty)
          query = query.Where(x => x.timeOffRequest.UserId == request.UserId);

        query = query.Where(x => x.timeOffRequest.FromDate >= FromDate && x.timeOffRequest.ToDate <= ToDate);

        int totalRow = await query.CountAsync();

        var data = await query.Skip((request.PageIndex - 1) * request.PageSize)
            .Take(request.PageSize)
            .Select(x => new TimeOffViewModel()
            {
              Name = x.timeOffRequest.Name,
              Description = x.timeOffRequest.Description,
              TimeOffType = x.timeOffType.Name,
              UserId = x.timeOffRequest.UserId,
              FromDate = x.timeOffRequest.FromDate,
              ToDate = x.timeOffRequest.ToDate,
              AdminNote = x.timeOffRequest.AdminNote,
              Duration = x.timeOffRequest.Duration,
              Status = x.timeOffRequest.Status
            }).ToListAsync();

        var pagedResult = new PagedResult<TimeOffViewModel>()
        {
          TotalRecords = totalRow,
          Items = data
        };
        return pagedResult;
      }
      catch (eTechException ex)
      {
        throw new eTechException($"Có lỗi xảy ra");
      }
    }

    public async Task<PagedResult<TimeOffViewModel>> GetAllPagingByUser(TimeOffPagingRequest request)
    {
      try
      {
        var query = from timeOffRequest in _context.TimeOffRequests
                    join timeOffType in _context.TimeOffTypes on timeOffRequest.TimeOffType equals timeOffType.Id
                    select new { timeOffRequest, timeOffType };

        DateTime FromDate = DateTime.Now;
        DateTime ToDate = DateTime.Now;

        CultureInfo provider = CultureInfo.InvariantCulture;
        if (!string.IsNullOrEmpty(request.FromDate) || !string.IsNullOrEmpty(request.ToDate))
        {
          FromDate = DateTime.ParseExact(request.FromDate, Constants.Constants.DateTimeFormatDate, provider);
          ToDate = DateTime.ParseExact(request.ToDate, Constants.Constants.DateTimeFormatDate, provider);
        }

        if (request.UserId != null || request.UserId != Guid.Empty)
          query = query.Where(x => x.timeOffRequest.UserId == request.UserId);

        query = query.Where(x => x.timeOffRequest.FromDate >= FromDate && x.timeOffRequest.ToDate <= ToDate);

        int totalRow = await query.CountAsync();

        var data = await query.Skip((request.PageIndex - 1) * request.PageSize)
            .Take(request.PageSize)
            .Select(x => new TimeOffViewModel()
            {
              Name = x.timeOffRequest.Name,
              Description = x.timeOffRequest.Description,
              TimeOffType = x.timeOffType.Name,
              UserId = x.timeOffRequest.UserId,
              FromDate = x.timeOffRequest.FromDate,
              ToDate = x.timeOffRequest.ToDate,
              AdminNote = x.timeOffRequest.AdminNote,
              Duration = x.timeOffRequest.Duration,
              Status = x.timeOffRequest.Status
            }).ToListAsync();

        var pagedResult = new PagedResult<TimeOffViewModel>()
        {
          TotalRecords = totalRow,
          Items = data
        };
        return pagedResult;
      }
      catch (eTechException ex)
      {
        throw new eTechException($"Có lỗi xảy ra");
      }
    }

    public async Task<TimeOffViewModel> GetById(int timeOffId)
    {

      var timeOffRequest = await _context.TimeOffRequests.FindAsync(timeOffId);

      var timeOffType = _context.TimeOffTypes.FindAsync(timeOffRequest.TimeOffType);

      var timeOffViewModel = new TimeOffViewModel()
      {
        Name = timeOffRequest.Name,
        Description = timeOffRequest.Description,
        TimeOffType = timeOffType.Result.Name,
        UserId = timeOffRequest.UserId,
        FromDate = timeOffRequest.FromDate,
        ToDate = timeOffRequest.ToDate,
        AdminNote = timeOffRequest.AdminNote,
        Duration = timeOffRequest.Duration,
        Status = timeOffRequest.Status
      };
      return timeOffViewModel;
    }

    public async Task<int> Update(TimeOffUpdateRequest request)
    {
      var timeOffRequest = await _context.TimeOffRequests.FindAsync(request.Id);
      if (timeOffRequest == null) throw new eTechException($"Không tìm thấy lịch nghỉ với id: {request.Id}");

      timeOffRequest.Name = request.Name;
      timeOffRequest.Description = request.Description;
      timeOffRequest.TimeOffType = request.TimeOffType;
      timeOffRequest.UserId = request.UserId;
      timeOffRequest.FromDate = request.FromDate;
      timeOffRequest.ToDate = request.ToDate;
      timeOffRequest.AdminNote = request.AdminNote;
      timeOffRequest.Duration = request.Duration;
      timeOffRequest.Status = request.Status;

      return await _context.SaveChangesAsync();
    }
  }
}
