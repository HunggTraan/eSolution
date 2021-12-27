using eSolutionTech.ViewModels.Common;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace eSolutionTech.ViewModels.Catalog.Shifts
{
    public interface IShiftService
    {
        Task<int> LoginShift(ShiftCreateRequest request);
        Task<int> LogoutShift(ShiftUpdateRequest request);
        Task<int> Delete(int shiftId);
        ShiftViewModel GetAll(string userId);
        Task<PagedResult<ShiftManageViewModel>> GetAllPaging(GetShiftPagingRequest request);
        Task<ShiftViewModel> GetById(int shiftId);
    }
}
