using eSolutionTech.ViewModels.Catalog.TimeOffRequests;
using eSolutionTech.ViewModels.Common;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace eSolutionTech.ApiIntegration
{
  public interface ITimeOffApiClient
  {
    Task<PagedResult<TimeOffViewModel>> GetPagings(TimeOffPagingRequest request);
    Task<PagedResult<TimeOffViewModel>> GetPagingsByUser(TimeOffPagingRequest request);
    Task<List<TimeOffViewModel>> GetAll();

    Task<bool> CreateTimeOff(TimeOffCreateRequest request);

    Task<bool> UpdateTimeOff(TimeOffUpdateRequest request);

    Task<TimeOffViewModel> GetById(int id);

    Task<bool> DeleteTimeOff(int id);

    Task<bool> ApplyRequest(int id, int status);
  }
}
