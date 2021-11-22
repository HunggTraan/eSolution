﻿using eSolutionTech.ViewModels.System.Users;
using System.Threading.Tasks;

namespace eSolutionTech.Application.System.Users
{
    public interface IUserService
    {
        Task<string> Authencate(LoginRequest request);
        Task<bool> CreateUser(CreateUserRequest request);
    }
}
