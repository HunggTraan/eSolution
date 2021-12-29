using eSolutionTech.ViewModels.Catalog.TimeOffRequests;
using eSolutionTech.ViewModels.Common;
using System.Collections.Generic;
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
    Task<List<TimeOffViewModel>> GetAll(string userId);
    Task<TimeOffViewModel> GetById(int timeOffId);
    Task<int> Apply(int id, string status);
    }
}
