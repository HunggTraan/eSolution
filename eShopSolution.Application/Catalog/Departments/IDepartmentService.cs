using eSolutionTech.Application.Catalog.Departments.Dtos;
using eSolutionTech.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace eSolutionTech.Application.Catalog.Departments
{
    public interface IDepartmentService
    {
        Task<int> Create(DepartmentCreateRequest request);
        Task<int> Update(DepartmentUpdateRequest request);
        Task<int> Delete(int departmentId);
        Task<List<DepartmentViewModel>>  GetAll();
        Task<PagedResult<DepartmentViewModel>> GetAllPaging(GetDepartmentPagingRequest request);
        Task<DepartmentViewModel> GetById(int departmentId);
    }
}
