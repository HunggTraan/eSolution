using eSolutionTech.ViewModels.Catalog.TimeOffRequests;
using eSolutionTech.ViewModels.Catalog.TimeOffRequests.Dtos;
using eSolutionTech.ViewModels.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace eSolutionTech.Application.Catalog.TimeOffRequests
{
    public interface ITimeOffRequestsService
    {
        Task<int> Create(TimeOffCreateRequest request);
        Task<int> Update(TimeOffUpdateRequest request);
        Task<int> Delete(int timeOffRequestId);
        Task<PagedResult<TimeOffViewModel>> GetAllPaging(TimeOffPagingRequest request);
        Task<PagedResult<TimeOffViewModel>> GetAllPagingByUser(TimeOffPagingRequest request);
        Task<List<TimeOffViewModel>> GetAll();
        Task<TimeOffViewModel> GetById(int timeOffId);
    }
}
