using eSolutionTech.ViewModels.Common;
using eSolutionTech.ViewModels.System.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eSolutionTech.Manager.Services
{
    public interface IUserApiClient
    {
        Task<string> Authenticate(LoginRequest request);
        Task<PagedResult<UserViewModel>> GetUsersPagings(GetUserPagingRequest request);

        Task<bool> CreateUser(CreateUserRequest registerRequest);

        //Task<bool> UpdateUser(Guid id, U userUpdateRequest);

        Task<UserViewModel> GetById(Guid id);

        Task<bool> DeleteUser(Guid id);

        //Task<bool> RoleAssignUser(Guid id, RoleAssignRequest roleAssignRequest);
    }
}
