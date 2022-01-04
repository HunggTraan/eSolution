using eSolutionTech.ViewModels.Common;
using eSolutionTech.ViewModels.System.Roles;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace eSolutionTech.ApiIntegration
{
  public interface IRoleApiClient
  {
    Task<ApiResult<List<RoleVm>>> GetAll();
    Task<ApiResult<PagedResult<RoleVm>>> GetRolesPagings(RolePagingRequest request);

    Task<ApiResult<bool>> Create(CreateRoleRequest request);

    Task<ApiResult<bool>> Update(Guid id, RoleUpdateRequest request);

    Task<ApiResult<bool>> Delete(string id);
    Task<ApiResult<RoleVm>> GetById(Guid id);
  }
}
