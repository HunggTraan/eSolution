using eSolutionTech.Application.Catalog.TimeOffTypes.Dtos;
using eSolutionTech.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace eSolutionTech.Application.Catalog.TimeOffTypes
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
