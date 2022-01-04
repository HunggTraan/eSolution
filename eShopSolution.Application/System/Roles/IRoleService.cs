using eSolutionTech.ViewModels.Common;
using eSolutionTech.ViewModels.System.Roles;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace eSolutionTech.Application.System.Roles
{
  public interface IRoleService
  {
    Task<List<RoleVm>> GetAll();
    Task<ApiResult<bool>> CreateRole(CreateRoleRequest request);
    Task<ApiResult<bool>> Update(Guid id, RoleUpdateRequest request);
    Task<ApiResult<bool>> Delete(string id);
    Task<ApiResult<PagedResult<RoleVm>>> GetRolesPaging(RolePagingRequest request);
    Task<ApiResult<RoleVm>> GetById(Guid id);
  }
}
