using eSolutionTech.ViewModels.Catalog.Departments;
using eSolutionTech.ViewModels.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace eSolutionTech.ApiIntegration
{
    public interface IDepartmentApiClient
    {
        Task<PagedResult<DepartmentViewModel>> GetPagings(GetDepartmentPagingRequest request);
        Task<List<DepartmentViewModel>> GetAll();

        Task<bool> CreateDepartment(DepartmentCreateRequest request);

        Task<bool> UpdateDepartment(DepartmentUpdateRequest request);

        Task<DepartmentViewModel> GetById(int id);

        Task<bool> DeleteDepartment(int id);
    }
}
