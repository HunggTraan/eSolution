using eShopSolution.Utilities.Exceptions;
using eSolutionTech.Data.EF;
using eSolutionTech.Data.Entities;
using eSolutionTech.ViewModels.Catalog.TimeOffTypes.Dtos;
using eSolutionTech.ViewModels.Common;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eSolutionTech.ViewModels.Catalog.TimeOffTypes
{
    public class TimeOffTypeService : ITimeOffTypeService
    {
        private readonly eTechDbContext _context;
        public TimeOffTypeService(eTechDbContext context)
        {
            _context = context;
        }
        public async Task<int> Create(TimeOffTypeCreateRequest request)
        {
            var timeOffType = new TimeOffType()
            {
                Code = request.Code,
                Name = request.Name,
                Description = request.Description,
                StartDate = request.StartDate,
                EndDate = request.EndDate,
                RequestUnit = request.RequestUnit,
                Unpaid = request.Unpaid
            };
            _context.TimeOffTypes.Add(timeOffType);
            return await _context.SaveChangesAsync();
        }

        public async Task<int> Delete(int timeOffTypeId)
        {
            var timeOffType = await _context.TimeOffTypes.FindAsync(timeOffTypeId);
            if (timeOffType == null)
                throw new eTechException($"Không tìm thấy chức danh với id là {timeOffTypeId}");
            _context.TimeOffTypes.Remove(timeOffType);
            return await _context.SaveChangesAsync();
        }

        public Task<List<TimeOffTypeViewModel>> GetAll()
        {
            throw new NotImplementedException();
        }

        public async Task<PagedResult<TimeOffTypeViewModel>> GetAllPaging(GetTimeOffTypePagingRequest request)
        {
            var query = from timeOffType in _context.TimeOffTypes
                        select new { timeOffType };
            if (!string.IsNullOrEmpty(request.KeyWord))
                query = query.Where(x => x.timeOffType.Code.Contains(request.KeyWord) || x.timeOffType.Name.Contains(request.KeyWord));

            int totalRow = await query.CountAsync();

            var data = await query.Skip((request.PageIndex - 1) * request.PageSize)
                .Take(request.PageSize)
                .Select(x => new TimeOffTypeViewModel()
                {
                    Id = x.timeOffType.Id,
                    Name = x.timeOffType.Name,
                    Code = x.timeOffType.Code,
                    Description = x.timeOffType.Description,
                    StartDate = x.timeOffType.StartDate,
                    EndDate = x.timeOffType.EndDate,
                    RequestUnit = x.timeOffType.RequestUnit,
                    Unpaid = x.timeOffType.Unpaid
                }).ToListAsync();

            var pagedResult = new PagedResult<TimeOffTypeViewModel>()
            {
                TotalRecords = totalRow,
                Items = data
            };
            return pagedResult;
        }

        public async Task<TimeOffTypeViewModel> GetById(int timeOffTypeId)
        {
            var timeOffType = await _context.TimeOffTypes.FindAsync(timeOffTypeId);

            var timeOffTypeViewModel = new TimeOffTypeViewModel()
            {
                Id = timeOffType.Id,
                Code = timeOffType.Code,
                Name = timeOffType.Name,
                Description = timeOffType.Description,
                StartDate = timeOffType.StartDate,
                EndDate = timeOffType.EndDate,
                RequestUnit = timeOffType.RequestUnit,
                Unpaid = timeOffType.Unpaid
            };
            return timeOffTypeViewModel;
        }

        public async Task<int> Update(TimeOffTypeUpdateRequest request)
        {
            var timeOffType = await _context.TimeOffTypes.FindAsync(request.Id);
            if (timeOffType == null) throw new eTechException($"Không tìm thấy loại ngày nghỉ với id: {request.Id}");

            if (string.IsNullOrEmpty(request.Name))
                throw new eTechException($"Tên không được để trống");
            if (string.IsNullOrEmpty(request.Code))
                throw new eTechException($"Mã không được để trống");
            timeOffType.Name = request.Name;
            timeOffType.Code = request.Code;
            timeOffType.Description = request.Description;
            timeOffType.StartDate = request.StartDate;
            timeOffType.EndDate = request.EndDate;
            timeOffType.RequestUnit = request.RequestUnit;
            timeOffType.Unpaid = request.Unpaid;

            return await _context.SaveChangesAsync();
        }
    }
}
