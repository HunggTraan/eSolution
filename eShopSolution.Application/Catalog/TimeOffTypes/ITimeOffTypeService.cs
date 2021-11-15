using eSolutionTech.ViewModels.Catalog.TimeOffTypes.Dtos;
using eSolutionTech.ViewModels.Common;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace eSolutionTech.ViewModels.Catalog.TimeOffTypes
{
    public interface ITimeOffTypeService
    {
        Task<int> Create(TimeOffTypeCreateRequest request);
        Task<int> Update(TimeOffTypeUpdateRequest request);
        Task<int> Delete(int timeOffTypeId);
        Task<List<TimeOffTypeViewModel>> GetAll();
        Task<PagedResult<TimeOffTypeViewModel>> GetAllPaging(GetTimeOffTypePagingRequest request);
        Task<TimeOffTypeViewModel> GetById(int timeOffTypeId);
    }
}
