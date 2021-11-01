using eSolutionTech.Application.Catalog.JobTitles.Dtos;
using eSolutionTech.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace eSolutionTech.Application.Catalog.JobTitles
{
    public interface IJobTitleService
    {
        Task<int> Create(JobTitleCreateRequest request);
        Task<int> Update(JobTitleUpdateRequest request);
        Task<int> Delete(int jobTitleId);
        Task<List<JobTitleViewModel>> GetAll();
        Task<PagedResult<JobTitleViewModel>> GetAllPaging(GetJobTitlePagingRequest request);
        Task<JobTitleViewModel> GetById(int jobTitleId);
    }
}
