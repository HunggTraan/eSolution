using eShopSolution.Utilities.Exceptions;
using eSolutionTech.Application.Catalog.Shifts.Dtos;
using eSolutionTech.Application.Dtos;
using eSolutionTech.Data.EF;
using eSolutionTech.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace eSolutionTech.Application.Catalog.Shifts
{
    public class ShiftService : IShiftService
    {
        private readonly eTechDbContext _context;
        public ShiftService(eTechDbContext context)
        {
            _context = context;
        }
        public async Task<int> Create(ShiftCreateRequest request)
        {
            var shift = new Shift()
            {
                ProjectId = request.ProjectId,
                UserId = request.UserId,
                Activity = request.Activity,
                Comment = request.Comment,
                Date = request.Date,
                WorkingHours = request.WorkingHours
            };
            _context.Shifts.Add(shift);
            return await _context.SaveChangesAsync();
        }

        public async Task<int> Delete(int shiftId)
        {
            var shift = await _context.Shifts.FindAsync(shiftId);
            if (shift == null)
                throw new eTechException($"Không tìm thấy ca làm việc với id là {shiftId}");
            _context.Shifts.Remove(shift);
            return await _context.SaveChangesAsync();
        }

        public Task<List<ShiftViewModel>> GetAll()
        {
            throw new NotImplementedException();
        }

        //public Task<List<ShiftViewModel>> GetAllByProjectId(int projectId)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task<List<ShiftViewModel>> GetAllByUserId(Guid userId)
        //{
        //    throw new NotImplementedException();
        //}

        public async Task<PagedResult<ShiftViewModel>> GetAllPaging(GetShiftPagingRequest request)
        {
            var query = from shift in _context.Shifts
                        select new { shift };
            //if (!string.IsNullOrEmpty(request.KeyWord))
            //    query = query.Where(x => x.shift.Code.Contains(request.KeyWord) || x.jobTitle.Name.Contains(request.KeyWord));

            if(request.UserId != null || request.UserId != Guid.Empty)
            {
                query = query.Where(x => x.shift.UserId == request.UserId);
            }

            if(request.ProjectId > 0)
            {
                query = query.Where(x => x.shift.ProjectId == request.UserId.ToString());
            }

            int totalRow = await query.CountAsync();

            var data = await query.Skip((request.PageIndex - 1) * request.PageSize)
                .Take(request.PageSize)
                .Select(x => new ShiftViewModel()
                {
                    Id = x.shift.Id,
                    ProjectId = x.shift.ProjectId,
                    UserId = x.shift.UserId,
                    Activity = x.shift.Activity,
                    Comment = x.shift.Comment,
                    Date = x.shift.Date,
                    WorkingHours = x.shift.WorkingHours
                }).ToListAsync();

            var pagedResult = new PagedResult<ShiftViewModel>()
            {
                TotalRecord = totalRow,
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
                Activity = shift.Activity,
                Comment = shift.Comment,
                Date = shift.Date,
                WorkingHours = shift.WorkingHours
            };
            return shiftViewModel;
        }

        public async Task<int> Update(ShiftUpdateRequest request)
        {
            var shift = await _context.Shifts.FindAsync(request.Id);
            if (shift == null) throw new eTechException($"Không tìm thấy ca làm việc với id: {request.Id}");

            shift.ProjectId = request.ProjectId;
            shift.UserId = request.UserId;
            shift.Activity = request.Activity;
            shift.Comment = request.Comment;
            shift.Date = request.Date;
            shift.WorkingHours = request.WorkingHours;

            return await _context.SaveChangesAsync();
        }
    }
}
