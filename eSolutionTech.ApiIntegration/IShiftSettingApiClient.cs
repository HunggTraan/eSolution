using eSolutionTech.ViewModels.Catalog.ShiftSettings;
using eSolutionTech.ViewModels.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace eSolutionTech.ApiIntegration
{
  public interface IShiftSettingApiClient
  {
    Task<PagedResult<ShiftSettingViewModel>> GetPagings(GetShiftSettingPagingRequest request);
    Task<List<ShiftSettingViewModel>> GetAll();

    Task<bool> CreateShiftSetting(ShiftSettingCreateRequest request);

    Task<bool> UpdateShiftSetting(ShiftSettingUpdateRequest request);

    Task<ShiftSettingViewModel> GetById(int id);

    Task<bool> DeleteShiftSetting(int id);
  }
}
