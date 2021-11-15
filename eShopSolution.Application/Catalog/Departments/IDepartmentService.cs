using eSolutionTech.ViewModels.Catalog.Departments;
using eSolutionTech.ViewModels.Common;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace eSolutionTech.ViewModels.Catalog.Departments
{
    public interface IDepartmentService
    {
        Task<int> Create(DepartmentCreateRequest request);
        Task<int> Update(DepartmentUpdateRequest request);
        Task<int> Delete(int departmentId);
        Task<List<DepartmentViewModel>> GetAll();
        Task<PagedResult<DepartmentViewModel>> GetAllPaging(GetDepartmentPagingRequest request);
        Task<DepartmentViewModel> GetById(int departmentId);
    }
}
