using eSolutionTech.Application.Catalog.Projects.Dtos;
using eSolutionTech.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace eSolutionTech.Application.Catalog.Projects
{
    public interface IProjectService
    {
        Task<int> Create(ProjectCreateRequest request);
        Task<int> Update(ProjectUpdateRequest request);
        Task<int> Delete(int projectId);
        Task<List<ProjectViewModel>> GetAll();
        Task<PagedResult<ProjectViewModel>> GetAllPaging(GetProjectPagingRequest request);
        Task<ProjectViewModel> GetById(int projectId);
    }
}
