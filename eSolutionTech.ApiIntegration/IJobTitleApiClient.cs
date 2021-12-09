using eSolutionTech.ViewModels.Catalog.JobTitles;
using eSolutionTech.ViewModels.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace eSolutionTech.ApiIntegration
{
    public interface IJobTitleApiClient
    {
        Task<PagedResult<JobTitleViewModel>> GetPagings(GetJobTitlePagingRequest request);
        Task<List<JobTitleViewModel>> GetAll();

        Task<bool> CreateJobTitle(JobTitleCreateRequest request);

        Task<bool> UpdateJobTitle(JobTitleUpdateRequest request);

        Task<JobTitleViewModel> GetById(int id);

        Task<bool> DeleteJobTitle(int id);
    }
}
