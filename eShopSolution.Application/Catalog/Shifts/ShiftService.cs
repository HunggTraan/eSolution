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

namespace eSolutionTech.ViewModels.Catalog.Shifts
{
    public class ShiftService : IShiftService
    {
        private readonly eTechDbContext _context;
        private readonly IProjectService _projectService;
        public ShiftService(eTechDbContext context, IProjectService projectService)
        {
            _context = context;
            _projectService = projectService;
        }
        public async Task<int> Create(ShiftCreateRequest request)
        {
            var projectInfo = _projectService.GetById(request.ProjectId);

            if (projectInfo.Result.StartDate < DateTime.Now)
            {
                throw new eTechException($"Chưa đến thời điểm chấm công cho dự án này!");
            }
            else if (projectInfo.Result.EndDate < DateTime.Now)
            {
                throw new eTechException($"Hết thời hạn chấm công cho dự án này!");
            }

            var shift = new Shift()
            {
                ProjectId = request.ProjectId,
                UserId = request.UserId,
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

            if (!string.IsNullOrEmpty(request.UserId))
            {
                query = query.Where(x => x.shift.UserId == request.UserId);
            }

            if (request.ProjectId > 0)
            {
                query = query.Where(x => x.shift.ProjectId == request.ProjectId);
            }

            int totalRow = await query.CountAsync();

            var data = await query.Skip((request.PageIndex - 1) * request.PageSize)
                .Take(request.PageSize)
                .Select(x => new ShiftViewModel()
                {
                    Id = x.shift.Id,
                    ProjectId = x.shift.ProjectId,
                    UserId = x.shift.UserId,
                    WorkingHours = x.shift.WorkingHours
                }).ToListAsync();

            var pagedResult = new PagedResult<ShiftViewModel>()
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

        public async Task<int> Update(ShiftUpdateRequest request)
        {
            var shift = await _context.Shifts.FindAsync(request.Id);
            if (shift == null) throw new eTechException($"Không tìm thấy ca làm việc với id: {request.Id}");

            shift.ProjectId = request.ProjectId;
            shift.UserId = request.UserId;
            shift.WorkingHours = request.WorkingHours;

            return await _context.SaveChangesAsync();
        }
    }
}
