using eSolutionTech.ApiIntegration;
using eSolutionTech.ViewModels.Catalog.Shifts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Net.Mime;
using System.Security.Claims;
using System.Threading.Tasks;

namespace eSolutionTech.Manager.Controllers
{
  public class ShiftController : BaseController
  {
    private readonly ITimeOffApiClient _timeOffApiClient;
    private readonly ITimeOffTypeApiClient _timeOffTypeApiClient;
    private readonly IShiftApiClient _shiftApiClient;
    private readonly IJobTitleApiClient _jobTitleApiClient;
    private readonly IDepartmentApiClient _departmentApiClient;
    private readonly IProjectApiClient _projectApiClient;
    private readonly IConfiguration _configuration;

    public ShiftController(ITimeOffApiClient TimeOffApiClient, ITimeOffTypeApiClient timeOffTypeApiClient,
      IShiftApiClient shiftApiClient, IJobTitleApiClient jobTitleApiClient,
      IDepartmentApiClient departmentApiClient, IProjectApiClient projectApiClient,
      IConfiguration configuration)
    {
      _configuration = configuration;
      _timeOffApiClient = TimeOffApiClient;
      _timeOffTypeApiClient = timeOffTypeApiClient;
      _shiftApiClient = shiftApiClient;
      _jobTitleApiClient = jobTitleApiClient;
      _departmentApiClient = departmentApiClient;
      _projectApiClient = projectApiClient;
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

        var project = _projectApiClient.GetAll();
        ViewBag.Project = project.Result.Select(x => new SelectListItem()
        {
          Text = x.Name,
          Value = x.Id.ToString()
        });
      }
      catch (Exception ex)
      {
        return;
      }
    }

    public async Task<IActionResult> Index(string code, string fullName, string jobTitleID, string departmentId, string projectId, string status,
      string isLate, string fromDate, string toDate, int pageIndex = 1, int pageSize = 10)
    {
      GetDataForFilter();
      var request = new GetShiftPagingRequest()
      {
        Code = code,
        FullName = fullName,
        DepartmentId = departmentId,
        JobTitleId = jobTitleID,
        IsLate = isLate,
        ProjectId = projectId,
        Status = status,
        FromDate = fromDate,
        ToDate = toDate,
        PageIndex = pageIndex,
        PageSize = pageSize
      };

      ViewBag.FromDate = fromDate;
      ViewBag.ToDate = toDate;
      ViewBag.Code = code;
      ViewBag.FullName = fullName;


      var data = await _shiftApiClient.GetPagings(request);

      if (TempData["result"] != null)
      {
        ViewBag.SuccessMsg = TempData["result"];
      }
      return View(data);
    }

    public async Task<IActionResult> DetailsUser(string userId, string status, string isLate, string fromDate, string toDate, int pageIndex = 1, int pageSize = 10)
    {
      var request = new GetShiftPagingRequest()
      {
        PageIndex = pageIndex,
        UserId = userId,
        PageSize = pageSize,
      };

      var data = await _shiftApiClient.GetPagings(request);

      if (TempData["result"] != null)
      {
        ViewBag.SuccessMsg = TempData["result"];
      }
      return View(data);
    }

    [HttpGet]
    public IActionResult Create()
    {
      return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] ShiftCreateRequest request)
    {
      if (!ModelState.IsValid)
        return View(request);

      var claimsIdentity = (ClaimsIdentity)this.User.Identity;
      var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
      request.UserId = claim.Value;

      var result = await _shiftApiClient.LoginShift(request);

      return Json(result);
    }

    [HttpPost]
    public async Task<IActionResult> Edit([FromBody] ShiftUpdateRequest request)
    {
      if (!ModelState.IsValid)
        return View(request);

      var claimsIdentity = (ClaimsIdentity)this.User.Identity;
      var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
      request.UserId = claim.Value;

      var result = await _shiftApiClient.LogoutShift(request);
      return Json(result);
    }

    [HttpPost]
    public async Task<IActionResult> Delete([FromBody] ShiftDeleteRequest request)
    {
      if (!ModelState.IsValid)
        return View();

      var result = await _timeOffApiClient.DeleteTimeOff(Int32.Parse(request.Id));
      if (result)
      {
        TempData["result"] = "Xóa lịch chấm công thành công";
        return RedirectToAction("IndexUser");
      }

      ModelState.AddModelError("", "Xóa lịch chấm công không thành công");
      return View(result);
    }

    [HttpGet]
    public async Task<IActionResult> Details(int id)
    {
      var result = await _shiftApiClient.GetById(id);
      return View(result);
    }
  }
}
