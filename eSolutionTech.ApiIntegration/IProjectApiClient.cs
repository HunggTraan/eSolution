using eSolutionTech.ViewModels.Catalog.Projects;
using eSolutionTech.ViewModels.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace eSolutionTech.ApiIntegration
{
    public interface IProjectApiClient
    {
        Task<PagedResult<ProjectViewModel>> GetPagings(GetProjectPagingRequest request);
        Task<List<ProjectViewModel>> GetAll();

        Task<bool> CreateProject(ProjectCreateRequest request);

        Task<bool> UpdateProject(ProjectUpdateRequest request);

        Task<ProjectViewModel> GetById(int id);

        Task<bool> DeleteProject(int id);
    }
}
