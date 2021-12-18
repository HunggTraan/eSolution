using eSolutionTech.Data.EF;
using eSolutionTech.Data.Entities;
using eSolutionTech.ViewModels.Catalog.Departments;
using eSolutionTech.ViewModels.Catalog.JobTitles;
using eSolutionTech.ViewModels.Common;
using eSolutionTech.ViewModels.System.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
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
    private readonly IDepartmentService _departmentService;
    private readonly IJobTitleService _jobTitleService;
    private readonly eTechDbContext _context;
    private readonly IConfiguration _config;
    public UserService(UserManager<User> userManager,
        SignInManager<User> signInManager,
        RoleManager<Role> roleManager,
        IDepartmentService departmentService,
        IJobTitleService jobTitleService,
        IConfiguration configuration,
        eTechDbContext context
        )
    {
      _signInManager = signInManager;
      _userManager = userManager;
      _roleManager = roleManager;
      _departmentService = departmentService;
      _jobTitleService = jobTitleService;
      _config = configuration;
      _context = context;
    }
    public async Task<ApiResult<string>> Authencate(LoginRequest request)
    {
      try
      {
        var user = await _userManager.FindByNameAsync(request.UserName);
        //if (user == null) return new ApiErrorResult<string>("Tài khoản không tồn tại");

        var result = await _signInManager.PasswordSignInAsync(user, request.Password, request.RememberMe, true);
        if (!result.Succeeded)
        {
          return new ApiErrorResult<string>("Phiên đăng nhập không hợp lệ");
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

        return new ApiSuccessResult<string>(new JwtSecurityTokenHandler().WriteToken(token));
      }
      catch (Exception ex)
      {
        return new ApiErrorResult<string>("Đăng nhập không thành công");
      }
    }

    public async Task<ApiResult<bool>> CreateUser(CreateUserRequest request)
    {
      try
      {
        var user = await _userManager.FindByNameAsync(request.UserName);
        if (user != null)
        {
          return new ApiErrorResult<bool>(Constants.Message.USEREXIST);
        }
        if (await _userManager.FindByEmailAsync(request.UserEmail) != null)
        {
          return new ApiErrorResult<bool>(Constants.Message.EMAILEXIST);
        }

        user = new User()
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

        if (result.Succeeded)
        {
          return new ApiSuccessResult<bool>();
        }
        return new ApiErrorResult<bool>("Đăng ký không thành công");
      }
      catch (Exception ex)
      {
        return new ApiErrorResult<bool>(Constants.Message.ERROR);
      }
    }

    public async Task<ApiResult<bool>> Delete(Guid id)
    {
      try
      {
        var user = await _userManager.FindByIdAsync(id.ToString());
        if (user == null)
        {
          var message = string.Format(Constants.Message.NOTFOUND, "Nhân viên");
          return new ApiErrorResult<bool>(message);
        }
        var result = await _userManager.DeleteAsync(user);
        if (result.Succeeded)
          return new ApiSuccessResult<bool>();

        return new ApiErrorResult<bool>(Constants.Message.DELETENOTSUCCESS);
      }
      catch (Exception ex)
      {
        return new ApiErrorResult<bool>(Constants.Message.ERROR);
      }
    }

    public async Task<ApiResult<UserViewModel>> GetById(Guid id)
    {
      try
      {
        var user = await _userManager.FindByIdAsync(id.ToString());

        var department = await _departmentService.GetById(Int32.Parse(user.DepartmentId));
        var jobTitle = await _jobTitleService.GetById(Int32.Parse(user.JobTitleId));

        if (user == null)
        {
          var message = string.Format(Constants.Message.NOTFOUND, "Nhân viên");
          return new ApiErrorResult<UserViewModel>(message);
        }
        var roles = await _userManager.GetRolesAsync(user);

        var userVm = new UserViewModel()
        {
          Email = user.Email,
          PhoneNumber = user.Phone,
          Code = user.Code,
          JobTitle = jobTitle.Name,
          Dob = user.DoB,
          Id = user.Id,
          UserName = user.UserName,
          FullName = user.FullName,
          Department = department.Name,
          Roles = roles
        };
        return new ApiSuccessResult<UserViewModel>(userVm);
      }
      catch (Exception ex)
      {
        return new ApiErrorResult<UserViewModel>(Constants.Message.ERROR);
      }
    }

    public async Task<ApiResult<List<UserViewModel>>> GetUsers()
    {
      var query = from users in _context.Users
                  join jobTitle in _context.JobTitles on users.JobTitleId equals jobTitle.Id.ToString()
                  join department in _context.Departments on users.DepartmentId equals department.Id.ToString()
                  select new { users, jobTitle, department };

      var data = await query.Select(x => new UserViewModel()
      {
        Email = x.users.Email,
        PhoneNumber = x.users.Phone,
        UserName = x.users.UserName,
        FullName = x.users.FullName,
        Id = x.users.Id,
        JobTitle = x.jobTitle.Name,
        Department = x.department.Name,
        Dob = x.users.DoB
      }).ToListAsync();

      if (data != null)
        return new ApiSuccessResult<List<UserViewModel>>(data);
      else return new ApiErrorResult<List<UserViewModel>>(Constants.Message.ERROR);
    }

    public async Task<ApiResult<PagedResult<UserViewModel>>> GetUsersPaging(GetUserPagingRequest request)
    {
      try
      {
        var query = from users in _context.Users
                    join jobTitle in _context.JobTitles on users.JobTitleId equals jobTitle.Id.ToString()
                    join department in _context.Departments on users.DepartmentId equals department.Id.ToString()
                    select new { users, jobTitle, department };

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
              Department = x.department.Name,
              Dob = x.users.DoB
            }).ToListAsync();

        //4. Select and projection
        var pagedResult = new PagedResult<UserViewModel>()
        {
          TotalRecords = totalRow,
          PageIndex = request.PageIndex,
          PageSize = request.PageSize,
          Items = data
        };

        return new ApiSuccessResult<PagedResult<UserViewModel>>(pagedResult);
      }
      catch (Exception ex)
      {
        return new ApiErrorResult<PagedResult<UserViewModel>>(Constants.Message.ERROR);
      }
    }

    public async Task<ApiResult<bool>> Update(Guid id, UserUpdateRequest request)
    {
      try
      {
        if (await _userManager.Users.AnyAsync(x => x.Email == request.UserEmail && x.Id != id))
        {
          return new ApiErrorResult<bool>("Email đã tồn tại");
        }
        var user = await _userManager.FindByIdAsync(id.ToString());

        user.DoB = request.Dob;
        user.Email = request.UserEmail;
        user.FullName = request.FullName;
        user.PhoneNumber = request.Phone;
        user.DepartmentId = request.DepartmentId;
        user.JobTitleId = request.JobTitleId;

        var result = await _userManager.UpdateAsync(user);
        if (result.Succeeded)
        {
          return new ApiSuccessResult<bool>();
        }
        return new ApiErrorResult<bool>("Cập nhật không thành công");
      }
      catch (Exception ex)
      {
        return new ApiErrorResult<bool>(Constants.Message.ERROR);
      }
    }

    public async Task<ApiResult<bool>> RoleAssign(Guid id, RoleAssignRequest request)
    {
      var user = await _userManager.FindByIdAsync(id.ToString());
      if (user == null)
      {
        return new ApiErrorResult<bool>("Tài khoản không tồn tại");
      }
      var removedRoles = request.Roles.Where(x => x.Selected == false).Select(x => x.Name).ToList();
      foreach (var roleName in removedRoles)
      {
        if (await _userManager.IsInRoleAsync(user, roleName) == true)
        {
          await _userManager.RemoveFromRoleAsync(user, roleName);
        }
      }
      await _userManager.RemoveFromRolesAsync(user, removedRoles);

      var addedRoles = request.Roles.Where(x => x.Selected).Select(x => x.Name).ToList();
      foreach (var roleName in addedRoles)
      {
        if (await _userManager.IsInRoleAsync(user, roleName) == false)
        {
          await _userManager.AddToRoleAsync(user, roleName);
        }
      }

      return new ApiSuccessResult<bool>();
    }
  }
}
