using eSolutionTech.Data.EF;
using eSolutionTech.Data.Entities;
using eSolutionTech.ViewModels.Common;
using eSolutionTech.ViewModels.System.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace eSolutionTech.Application.System.Users
{
    public class UserService : IUserService
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly RoleManager<Role> _roleManager;
        private readonly eTechDbContext _context;
        private readonly IConfiguration _config;
        public UserService(UserManager<User> userManager, 
            SignInManager<User> signInManager, 
            RoleManager<Role> roleManager, 
            IConfiguration configuration,
            eTechDbContext context
            )
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _roleManager = roleManager;
            _config = configuration;
            _context = context;
        }
        public async Task<string> Authencate(LoginRequest request)
        {
            var user = await _userManager.FindByNameAsync(request.UserName);
            //if (user == null) return new ApiErrorResult<string>("Tài khoản không tồn tại");

            var result = await _signInManager.PasswordSignInAsync(user, request.Password, request.RememberMe, true);
            if (!result.Succeeded)
            {
                return string.Empty;
            }
            var roles = await _userManager.GetRolesAsync(user);
            var claims = new[]
            {
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.GivenName,user.FullName),
                //new Claim(ClaimTypes.Role, string.Join(";", roles)),
                new Claim(ClaimTypes.Name, user.UserName)
            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Tokens:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(_config["Tokens:Issuer"],
                _config["Tokens:Issuer"],
                claims,
                expires: DateTime.Now.AddHours(3),
                signingCredentials: creds);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public async Task<bool> CreateUser(CreateUserRequest request)
        {
            var user = new User()
            {
                Code = request.Code,
                UserName = request.UserName,
                DoB = request.Dob,
                Phone = request.Phone,
                JobTitleId = request.JobTitleId,
                DepartmentId = request.DepartmentId,
                FullName = request.FullName,
                Email = request.UserEmail
            };

            var result = await _userManager.CreateAsync(user, request.Password);
            if (!result.Succeeded)
            {
                return false;
            }
            return true;
        }

        public async Task<PagedResult<UserViewModel>> GetUsersPaging(GetUserPagingRequest request)
        {
            var query = from users in _context.Users
                        join jobTitle in _context.JobTitles on users.JobTitleId equals jobTitle.Id.ToString()
                        join department in _context.Departments on users.DepartmentId equals department.Id.ToString()
                        select new { users, jobTitle , department};
            if (!string.IsNullOrEmpty(request.Keyword))
            {
                query = query.Where(x => x.users.UserName.Contains(request.Keyword)
                 || x.users.PhoneNumber.Contains(request.Keyword) || x.users.FullName.Contains(request.Keyword));
            }

            //3. Paging
            int totalRow = await query.CountAsync();

            var data = await query.Skip((request.PageIndex - 1) * request.PageSize)
                .Take(request.PageSize)
                .Select(x => new UserViewModel()
                {
                    Email = x.users.Email,
                    PhoneNumber = x.users.Phone,
                    UserName = x.users.UserName,
                    FullName = x.users.FullName,
                    Id = x.users.Id,
                    JobTitle = x.jobTitle.Name,
                    Department = x.department.Name
                }).ToListAsync();

            //4. Select and projection
            var pagedResult = new PagedResult<UserViewModel>()
            {
                TotalRecord = totalRow,
                //PageIndex = request.PageIndex,
                //PageSize = request.PageSize,
                Items = data
            };
            return pagedResult;
        }
    }
}
