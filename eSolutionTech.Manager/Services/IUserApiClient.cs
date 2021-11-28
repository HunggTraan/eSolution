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
    }
}
