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
  }
}
