using eSolutionTech.ViewModels.Common;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace eSolutionTech.ViewModels.Catalog.Projects
{
    public interface IProjectService
    {
        Task<int> Create(ProjectCreateRequest request);
        Task<int> Update(ProjectUpdateRequest request);
        Task<int> Delete(int projectId);
        Task<List<ProjectViewModel>> GetAll();
        Task<PagedResult<ProjectViewModel>> GetAllPaging(GetProjectPagingRequest request);
        Task<PagedResult<ProjectViewModel>> GetAllPagingByUser(GetProjectPagingRequest request);
        Task<ProjectViewModel> GetById(int projectId);
    }
}
