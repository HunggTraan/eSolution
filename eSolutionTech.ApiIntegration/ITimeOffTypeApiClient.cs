using eSolutionTech.ViewModels.Catalog.TimeOffTypes;
using eSolutionTech.ViewModels.Catalog.TimeOffTypes.Dtos;
using eSolutionTech.ViewModels.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace eSolutionTech.ApiIntegration
{
    public interface ITimeOffTypeApiClient
    {
        Task<PagedResult<TimeOffTypeViewModel>> GetPagings(GetTimeOffTypePagingRequest request);
        Task<List<TimeOffTypeViewModel>> GetAll();

        Task<bool> CreateTimeOffType(TimeOffTypeCreateRequest request);

        Task<bool> UpdateTimeOffType(TimeOffTypeUpdateRequest request);

        Task<TimeOffTypeViewModel> GetById(int id);

        Task<bool> DeleteTimeOffType(int id);
    }
}
