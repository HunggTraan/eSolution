using eSolutionTech.ViewModels.System.Users;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Threading.Tasks;
using eSolutionTech.ApiIntegration;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;
using eSolutionTech.ViewModels.Common;

namespace eSolutionTech.Manager.Controllers
{
  public class UserController : BaseController
  {
    private readonly IUserApiClient _userApiClient;
    private readonly IConfiguration _configuration;
    private readonly IJobTitleApiClient _jobTitleApiClient;
    private readonly IDepartmentApiClient _departmentApiClient;
    private readonly IRoleApiClient _roleApiClient;
    public UserController(IUserApiClient userApiClient,
        IConfiguration configuration,
        IJobTitleApiClient jobTitleApiClient,
        IDepartmentApiClient departmentApiClient,
        IRoleApiClient roleApiClient)
    {
      _userApiClient = userApiClient;
      _configuration = configuration;
      _jobTitleApiClient = jobTitleApiClient;
      _departmentApiClient = departmentApiClient;
      _roleApiClient = roleApiClient;
    }

    protected void GetDataForCreateOrEdit()
    {
      try
      {
        var department = _departmentApiClient.GetAll();
        ViewBag.Department = department.Result.Select(x => new SelectListItem()
        {
          Text = x.Name,
          Value = x.Id.ToString()
        });

        var jobTitle = _jobTitleApiClient.GetAll();
        ViewBag.JobTitle = jobTitle.Result.Select(x => new SelectListItem()
        {
          Text = x.Description,
          Value = x.Id.ToString()
        });
      }
      catch (Exception ex)
      {
        return;
      }
    }

    protected void GetDataForFilter()
    {
      try
      {
        var department = _departmentApiClient.GetAll();

        ViewBag.Department = department.Result.Select(x => new SelectListItem()
        {
          Text = x.Name,
          Value = x.Id.ToString()
        });



        var jobTitle = _jobTitleApiClient.GetAll();
        ViewBag.JobTitle = jobTitle.Result.Select(x => new SelectListItem()
        {
          Text = x.Description,
          Value = x.Id.ToString()
        });
      }
      catch (Exception ex)
      {
        return;
      }
    }

    public async Task<IActionResult> Index(string code, string fullName, string jobTitleID, string departmentId, int pageIndex = 1, int pageSize = 10)
    {
      GetDataForCreateOrEdit();
      var request = new GetUserPagingRequest()
      {
        Code = code,
        FullName = fullName,
        DepartmentId = departmentId,
        JobTitleId = jobTitleID,
        PageIndex = pageIndex,
        PageSize = pageSize
      };

      ViewBag.Code = code;
      ViewBag.FullName = fullName;
      ViewBag.JobTitles = jobTitleID;
      ViewBag.Departments = departmentId;

      var data = await _userApiClient.GetUsersPagings(request);

      if (TempData["result"] != null)
      {
        ViewBag.SuccessMsg = TempData["result"];
      }
      return View(data.ResultObj);
    }


    [HttpPost]
    public async Task<IActionResult> Logout()
    {
      await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
      HttpContext.Session.Remove("Token");
      return RedirectToAction("Index", "Login");
    }

    [HttpGet]
    public IActionResult Create()
    {
      GetDataForCreateOrEdit();
      return View();
    }

    [HttpGet]
    public async Task<IActionResult> Details(Guid id)
    {
      var result = await _userApiClient.GetById(id);
      return View(result.ResultObj);
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateUserRequest request)
    {
      if (!ModelState.IsValid)
        return View();

      var result = await _userApiClient.CreateUser(request);
      if (result.IsSuccessed)
      {
        TempData["result"] = "Thêm mới người dùng thành công";
        return RedirectToAction("Index");
      }

      ModelState.AddModelError("", result.Message);

      return View(request);
    }

    [HttpGet]
    public async Task<IActionResult> Edit(Guid id)
    {
      GetDataForCreateOrEdit();
      var result = await _userApiClient.GetById(id);
      if (result.IsSuccessed)
      {
        var user = result.ResultObj;
        var updateRequest = new UserUpdateRequest()
        {
          Dob = user.Dob,
          UserEmail = user.Email,
          Id = id,
          DepartmentId = user.Department,
          JobTitleId = user.JobTitle,
          FullName = user.FullName,
          Phone = user.PhoneNumber,
          Code = user.Code,
          UserName = user.UserName
        };
        return View(updateRequest);
      }
      return RedirectToAction("Error", "Home");
    }

    [HttpPost]
    public async Task<IActionResult> Edit(UserUpdateRequest request)
    {
      if (!ModelState.IsValid)
        return View();

      var result = await _userApiClient.UpdateUser(request.Id, request);
      if (result.IsSuccessed)
      {
        TempData["result"] = "Cập nhật người dùng thành công";
        return RedirectToAction("Index");
      }

      ModelState.AddModelError("", result.Message);
      return View(request);
    }

    [HttpPost]
    public async Task<IActionResult> Delete([FromBody] UserDeleteRequest request)
    {
      if (!ModelState.IsValid)
        return View();

      var result = await _userApiClient.Delete(Guid.Parse(request.Id));
      if (result.IsSuccessed)
      {
        TempData["result"] = "Xóa người dùng thành công";
        return RedirectToAction("Index");
      }

      ModelState.AddModelError("", result.Message);
      return View(request);
    }

    [HttpGet]
    public async Task<IActionResult> RoleAssign(Guid id)
    {
      var roleAssignRequest = await GetRoleAssignRequest(id);
      return View(roleAssignRequest);
    }

    [HttpPost]
    public async Task<IActionResult> RoleAssign(RoleAssignRequest request)
    {
      if (!ModelState.IsValid)
        return View();

      var result = await _userApiClient.RoleAssign(request.Id, request);

      if (result.IsSuccessed)
      {
        TempData["result"] = "Cập nhật quyền thành công";
        return RedirectToAction("Index");
      }

      ModelState.AddModelError("", result.Message);
      var roleAssignRequest = await GetRoleAssignRequest(request.Id);

      return View(roleAssignRequest);
    }

    private async Task<RoleAssignRequest> GetRoleAssignRequest(Guid id)
    {
      var userObj = await _userApiClient.GetById(id);
      var roleObj = await _roleApiClient.GetAll();
      var roleAssignRequest = new RoleAssignRequest();
      foreach (var role in roleObj.ResultObj)
      {
        roleAssignRequest.Roles.Add(new SelectItem()
        {
          Id = role.Id.ToString(),
          Name = role.Name,
          Selected = userObj.ResultObj.Roles.Contains(role.Name)
        });
      }
      return roleAssignRequest;
    }

    [HttpGet]
    public async Task<JsonResult> GetUserById(
      string id
    )
    {
      var user = await _userApiClient.GetById(Guid.Parse(id));
      return Json(user);
    }
  }
}
