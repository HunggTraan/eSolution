using eShopSolution.Utilities.Exceptions;
using eSolutionTech.Data.EF;
using eSolutionTech.Data.Entities;
using eSolutionTech.ViewModels.Catalog.TimeOffRequests;
using eSolutionTech.ViewModels.Catalog.TimeOffTypes;
using eSolutionTech.ViewModels.Common;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
namespace eSolutionTech.Application.Catalog.TimeOffRequests
{
  public class TimeOffRequestsService : ITimeOffRequestsService
  {
    private readonly eTechDbContext _context;
    private readonly ITimeOffTypeService _timeOffTypeService;
    public TimeOffRequestsService(eTechDbContext context, ITimeOffTypeService timeOffTypeService)
    {
      _context = context;
      _timeOffTypeService = timeOffTypeService;
    }
    public async Task<int> Create(TimeOffCreateRequest request)
    {
      var timeOffRequest = new TimeOffRequest()
      {
        Name = request.Name,
        Description = request.Description,
        TimeOffType = request.TimeOffTypeId,
        UserId = request.UserId,
        FromDate = request.FromDate,
        ToDate = request.ToDate,
        Duration = request.Duration,
        Status = request.Status,
        HalfDay = request.HalfDay,
        RequestUnit = request.RequestUnit
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
          throw new eTechException($"Không tìm thấy lịch nghỉ với id là {timeOffRequestId}");
        _context.TimeOffRequests.Remove(timeOffRequest);
        return await _context.SaveChangesAsync();
      }
      catch (eTechException ex)
      {
        throw new eTechException($"Có lỗi xảy ra");
      }
    }

    public async Task<List<TimeOffViewModel>> GetAll(string userId)
    {
      var query = from timeOffRequest in _context.TimeOffRequests
                  join timeOffType in _context.TimeOffTypes on timeOffRequest.TimeOffType equals timeOffType.Id.ToString()
                  select new { timeOffRequest, timeOffType };

      if (!string.IsNullOrEmpty(userId))
      {
        query = query.Where(x => x.timeOffRequest.UserId == userId);
      }

      var data = await query
        .Select(x => new TimeOffViewModel()
        {
          Id = x.timeOffRequest.Id,
          Name = x.timeOffRequest.Name,
          Description = x.timeOffRequest.Description,
          TimeOffType = x.timeOffRequest.Name,
          UserId = x.timeOffRequest.UserId,
          FromDate = x.timeOffRequest.FromDate,
          ToDate = x.timeOffRequest.ToDate,
          Duration = x.timeOffRequest.Duration,
          Status = x.timeOffRequest.Status,
          RequestUnit = x.timeOffRequest.RequestUnit,
          HalfDay = x.timeOffRequest.HalfDay
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
                    join timeOffType in _context.TimeOffTypes on timeOffRequest.TimeOffType equals timeOffType.Id.ToString()
                    select new { timeOffRequest, timeOffType };

        DateTime FromDate = DateTime.Now;
        DateTime ToDate = DateTime.Now;

        CultureInfo provider = CultureInfo.InvariantCulture;
        if (!string.IsNullOrEmpty(request.FromDate) || !string.IsNullOrEmpty(request.ToDate))
        {
          FromDate = DateTime.ParseExact(request.FromDate, Constants.Constants.DateTimeFormatDate, provider);
          ToDate = DateTime.ParseExact(request.ToDate, Constants.Constants.DateTimeFormatDate, provider);

          query = query.Where(x => x.timeOffRequest.FromDate >= FromDate && x.timeOffRequest.ToDate <= ToDate);
        }

        if (!string.IsNullOrEmpty(request.UserId))
          query = query.Where(x => x.timeOffRequest.UserId == request.UserId.ToString());

        int totalRow = await query.CountAsync();

        var data = await query.Skip((request.PageIndex - 1) * request.PageSize)
            .Take(request.PageSize)
            .Select(x => new TimeOffViewModel()
            {
              Id = x.timeOffRequest.Id,
              Name = x.timeOffRequest.Name,
              Description = x.timeOffRequest.Description,
              TimeOffType = x.timeOffRequest.Name,
              UserId = x.timeOffRequest.UserId,
              FromDate = x.timeOffRequest.FromDate,
              ToDate = x.timeOffRequest.ToDate,
              Duration = x.timeOffRequest.Duration,
              Status = x.timeOffRequest.Status,
              RequestUnit = x.timeOffRequest.RequestUnit,
              HalfDay = x.timeOffRequest.HalfDay
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
                    join timeOffType in _context.TimeOffTypes on timeOffRequest.TimeOffType equals timeOffType.Id.ToString()
                    select new { timeOffRequest, timeOffType };

        DateTime FromDate = DateTime.Now;
        DateTime ToDate = DateTime.Now;

        CultureInfo provider = CultureInfo.InvariantCulture;
        if (!string.IsNullOrEmpty(request.FromDate) || !string.IsNullOrEmpty(request.ToDate))
        {
          FromDate = DateTime.ParseExact(request.FromDate, Constants.Constants.DateTimeFormatDate, provider);
          ToDate = DateTime.ParseExact(request.ToDate, Constants.Constants.DateTimeFormatDate, provider);
        }

        if (string.IsNullOrEmpty(request.UserId))
          query = query.Where(x => x.timeOffRequest.UserId == request.UserId.ToString());

        query = query.Where(x => x.timeOffRequest.FromDate >= FromDate && x.timeOffRequest.ToDate <= ToDate);

        int totalRow = await query.CountAsync();

        var data = await query.Skip((request.PageIndex - 1) * request.PageSize)
            .Take(request.PageSize)
            .Select(x => new TimeOffViewModel()
            {
              Id = x.timeOffRequest.Id,
              Name = x.timeOffRequest.Name,
              Description = x.timeOffRequest.Description,
              TimeOffType = x.timeOffRequest.Name,
              UserId = x.timeOffRequest.UserId,
              FromDate = x.timeOffRequest.FromDate,
              ToDate = x.timeOffRequest.ToDate,
              Duration = x.timeOffRequest.Duration,
              Status = x.timeOffRequest.Status,
              RequestUnit = x.timeOffRequest.RequestUnit,
              HalfDay = x.timeOffRequest.HalfDay
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
      if (timeOffRequest == null)
        return new TimeOffViewModel();

      var user = await _context.Users.FindAsync(Guid.Parse(timeOffRequest.UserId));
      if (user == null)
        return new TimeOffViewModel();

      var timeOffViewModel = new TimeOffViewModel()
      {
        Name = timeOffRequest.Name,
        Description = timeOffRequest.Description,
        TimeOffType = timeOffRequest.TimeOffType,
        UserId = timeOffRequest.UserId,
        FromDate = timeOffRequest.FromDate,
        ToDate = timeOffRequest.ToDate,
        Duration = timeOffRequest.Duration,
        Status = timeOffRequest.Status,
        RequestUnit = timeOffRequest.RequestUnit,
        HalfDay = timeOffRequest.HalfDay,
        UserName = user.FullName,
        UserCode = user.Code
      };
      return timeOffViewModel;
    }

    public async Task<int> Update(TimeOffUpdateRequest request)
    {
      var timeOffRequest = await _context.TimeOffRequests.FindAsync(request.Id);
      if (timeOffRequest == null) throw new eTechException($"Không tìm thấy lịch nghỉ với id: {request.Id}");

      timeOffRequest.Name = request.Name;
      timeOffRequest.Description = request.Description;
      timeOffRequest.TimeOffType = request.TimeOffTypeId;
      timeOffRequest.UserId = request.UserId;
      timeOffRequest.FromDate = request.FromDate;
      timeOffRequest.ToDate = request.ToDate;
      timeOffRequest.Duration = request.Duration;
      timeOffRequest.Status = request.Status;
      timeOffRequest.RequestUnit = request.RequestUnit;
      timeOffRequest.HalfDay = request.HalfDay;

      return await _context.SaveChangesAsync();
    }

    public async Task<int> Apply(int id, string status)
    {
      var timeOffRequest = await _context.TimeOffRequests.FindAsync(id);
      if (timeOffRequest == null) throw new eTechException($"Không tìm thấy lịch nghỉ với id: {id}");

      timeOffRequest.Status = status;

      return await _context.SaveChangesAsync();
    }
  }
}
