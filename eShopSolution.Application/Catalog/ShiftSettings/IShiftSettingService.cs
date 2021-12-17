using eSolutionTech.ViewModels.Common;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace eSolutionTech.ViewModels.Catalog.ShiftSettings
{
  public interface IShiftSettingService
  {
    Task<int> Create(ShiftSettingCreateRequest request);
    Task<int> Update(ShiftSettingUpdateRequest request);
    Task<int> Delete(int shiftSettingId);
    Task<List<ShiftSettingViewModel>> GetAll();
    Task<PagedResult<ShiftSettingViewModel>> GetAllPaging(GetShiftSettingPagingRequest request);
    Task<ShiftSettingViewModel> GetById(int shiftSettingId);
  }
}
