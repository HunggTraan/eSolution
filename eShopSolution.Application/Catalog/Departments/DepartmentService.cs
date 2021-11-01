using eShopSolution.Utilities.Exceptions;
using eSolutionTech.Application.Catalog.Departments.Dtos;
using eSolutionTech.Application.Dtos;
using eSolutionTech.Data.EF;
using eSolutionTech.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eSolutionTech.Application.Catalog.Departments
{
    public class DepartmentService : IDepartmentService
    {
        private readonly eTechDbContext _context;
        public DepartmentService(eTechDbContext context)
        {
            _context = context;
        }
        public async Task<int> Create(DepartmentCreateRequest request)
        {
            var department = new Department()
            {
                Code = request.Code,
                Name = request.Name,
                Description = request.Description,
            };
            _context.Departments.Add(department);
            return await _context.SaveChangesAsync();
        }

        public async Task<int> Delete(int departmentId)
        {
            var department = await _context.Departments.FindAsync(departmentId);
            if (department == null)
                throw new eTechException($"Không tìm thấy phòng ban với id là {departmentId}");
            _context.Departments.Remove(department);
            return await _context.SaveChangesAsync();
        }

        public Task<List<DepartmentViewModel>> GetAll()
        {
            throw new NotImplementedException();
        }

        public async Task<PagedResult<DepartmentViewModel>> GetAllPaging(GetDepartmentPagingRequest request)
        {
            var query = from department in _context.Departments
                        select new { department };
            if (!string.IsNullOrEmpty(request.KeyWord))
                query = query.Where(x => x.department.Code.Contains(request.KeyWord) || x.department.Name.Contains(request.KeyWord));

            int totalRow = await query.CountAsync();

            var data = await query.Skip((request.PageIndex - 1) * request.PageSize)
                .Take(request.PageSize)
                .Select(x => new DepartmentViewModel() {
                    Id = x.department.Id,
                    Name = x.department.Name,
                    Code = x.department.Code,
                    Description = x.department.Description
                }).ToListAsync();

            var pagedResult = new PagedResult<DepartmentViewModel>()
            {
                TotalRecord = totalRow,
                Items = data
            };
            return pagedResult;
        }

        public async Task<DepartmentViewModel> GetById(int departmentId)
        {
            var department = await _context.Departments.FindAsync(departmentId);

            var departmentViewModel = new DepartmentViewModel()
            {
                Id = department.Id,
                Name = department.Name,
                Code = department.Code,
                Description = department.Description,
            };
            return departmentViewModel;
        }

        public async Task<int> Update(DepartmentUpdateRequest request)
        {
            var department = await _context.Departments.FindAsync(request.Id);
            if (department == null) throw new eTechException($"Cannot find a department with id: {request.Id}");

            if(string.IsNullOrEmpty(request.Name))
                throw new eTechException($"Name cannot be null");
            if (string.IsNullOrEmpty(request.Code))
                throw new eTechException($"Code cannot be null");
            department.Name = request.Name;
            department.Code = request.Code;
            department.Description = request.Description;

            return await _context.SaveChangesAsync();
        }

    }
}
