using eSolutionTech.ViewModels.Common;
using eSolutionTech.ViewModels.System.Users;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace eSolutionTech.ApiIntegration
{
    public interface IUserApiClient
    {

        Task<ApiResult<string>> Authenticate(LoginRequest request);
        Task<ApiResult<List<UserViewModel>>> GetAll();

        Task<ApiResult<PagedResult<UserViewModel>>> GetUsersPagings(GetUserPagingRequest request);

        Task<ApiResult<bool>> CreateUser(CreateUserRequest request);

        Task<ApiResult<bool>> UpdateUser(Guid id, UserUpdateRequest request);

        Task<ApiResult<UserViewModel>> GetById(Guid id);

        Task<ApiResult<bool>> Delete(Guid id);

        Task<ApiResult<bool>> RoleAssign(Guid id, RoleAssignRequest request);
    }
}
