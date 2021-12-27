using eSolutionTech.ViewModels.Catalog.Shifts;
using eSolutionTech.ViewModels.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace eSolutionTech.ApiIntegration
{
  public interface IShiftApiClient
  {
    Task<PagedResult<ShiftManageViewModel>> GetPagings(GetShiftPagingRequest request);

    Task<string> LoginShift(ShiftCreateRequest request);

    Task<string> LogoutShift(ShiftUpdateRequest request);

    Task<ShiftViewModel> GetById(int id);

    Task<bool> DeleteShift(int id);
  }
}
