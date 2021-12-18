using eSolutionTech.ViewModels.Common;
using eSolutionTech.ViewModels.System.Users;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace eSolutionTech.Application.System.Users
{
    public interface IUserService
    {

        Task<ApiResult<string>> Authencate(LoginRequest request);

        Task<ApiResult<bool>> CreateUser(CreateUserRequest request);

        Task<ApiResult<bool>> Update(Guid id, UserUpdateRequest request);

        Task<ApiResult<PagedResult<UserViewModel>>> GetUsersPaging(GetUserPagingRequest request);
        Task<ApiResult<List<UserViewModel>>> GetUsers();

        Task<ApiResult<UserViewModel>> GetById(Guid id);

        Task<ApiResult<bool>> Delete(Guid id);

        Task<ApiResult<bool>> RoleAssign(Guid id, RoleAssignRequest request);
    }
}
