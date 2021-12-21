﻿using eSolutionTech.ApiIntegration;
using eSolutionTech.ViewModels.Catalog.TimeOffRequests;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Configuration;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace eSolutionTech.Manager.Controllers
{
  public class TimeOffRequestController : BaseController
  {
    private readonly ITimeOffApiClient _timeOffApiClient;
    private readonly ITimeOffTypeApiClient _timeOffTypeApiClient;
    private readonly IConfiguration _configuration;

    public TimeOffRequestController(ITimeOffApiClient TimeOffApiClient, ITimeOffTypeApiClient timeOffTypeApiClient,
      IConfiguration configuration)
    {
      _configuration = configuration;
      _timeOffApiClient = TimeOffApiClient;
      _timeOffTypeApiClient = timeOffTypeApiClient;
    }

    protected void GetDataForCreateOrEdit()
    {
      try
      {
        var timeOffType = _timeOffTypeApiClient.GetAll();
        ViewBag.TimeOffType = timeOffType.Result.Select(x => new SelectListItem()
        {
          Text = x.Name,
          Value = string.Format("{0}|{1}", x.Id, x.RequestUnit)
        });
      }
      catch (Exception ex)
      {
        return;
      }
    }
    public async Task<IActionResult> Index(string keyword, int pageIndex = 1, int pageSize = 10)
    {
      var request = new TimeOffPagingRequest()
      {
        IsAdmin = true,
        PageIndex = pageIndex,
        PageSize = pageSize
      };

      var data = await _timeOffApiClient.GetPagings(request);
      ViewBag.Keyword = keyword;

      if (TempData["result"] != null)
      {
        ViewBag.SuccessMsg = TempData["result"];
      }
      return View(data);
    }

    public async Task<IActionResult> IndexUser(string keyword, int pageIndex = 1, int pageSize = 10)
    {
      var claimsIdentity = (ClaimsIdentity)this.User.Identity;
      var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);

      var request = new TimeOffPagingRequest()
      {
        IsAdmin = false,
        PageIndex = pageIndex,
        PageSize = pageSize,
        UserId = claim.Value
      };

      var data = await _timeOffApiClient.GetPagingsByUser(request);
      ViewBag.Keyword = keyword;

      if (TempData["result"] != null)
      {
        ViewBag.SuccessMsg = TempData["result"];
      }
      return View(data);
    }

    [HttpGet]
    public IActionResult Create()
    {
      GetDataForCreateOrEdit();
      return View();
    }

    [HttpPost]
    [Consumes("multipart/form-data")]
    public async Task<IActionResult> Create([FromForm] TimeOffCreateRequest request)
    {
      if (!ModelState.IsValid)
        return View(request);

      var claimsIdentity = (ClaimsIdentity)this.User.Identity;
      var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
      request.UserId = claim.Value;

      request.FromDate = !String.IsNullOrEmpty(request.FromDateStr) ? DateTime.ParseExact(request.FromDateStr, "dd/MM/yyyy", null) : DateTime.Now;
      request.ToDate = !String.IsNullOrEmpty(request.ToDateStr) ? DateTime.ParseExact(request.ToDateStr, "dd/MM/yyyy", null) : DateTime.Now;
      request.Status = "0";
      var splitData = request.TimeOffTypeId.Split('|');
      if (request.RequestUnit == "Theo ngày")
      {
        TimeSpan difference = request.ToDate.AddDays(1) - request.FromDate;
        double days = difference.TotalDays;
        request.Duration = string.Format("{0} days", days);
      }
      else
      {
        request.Duration = "4 hours";
      }

      request.RequestUnit = splitData[1];
      request.TimeOffTypeId = splitData[0];

      var result = await _timeOffApiClient.CreateTimeOff(request);
      if (result)
      {
        TempData["result"] = "Thêm mới ngày nghỉ thành công";
        return RedirectToAction("Index");
      }

      ModelState.AddModelError("", "Thêm ngày nghỉ thất bại");
      GetDataForCreateOrEdit();
      return View(request);
    }


    [HttpGet]
    public async Task<IActionResult> Edit(int id)
    {
      GetDataForCreateOrEdit();
      var TimeOff = await _timeOffApiClient.GetById(id);
      var editVm = new TimeOffUpdateRequest()
      {
        Id = TimeOff.Id,
        Description = TimeOff.Description,
        Name = TimeOff.Name,
        FromDate = TimeOff.FromDate,
        ToDate = TimeOff.ToDate,
        Duration = TimeOff.Duration,
        Status = TimeOff.Status,
        TimeOffTypeId = TimeOff.TimeOffTypeId,
        UserId = TimeOff.UserId,
        HalfDay = TimeOff.HalfDay,
        RequestUnit = TimeOff.RequestUnit
      };
      return View(editVm);
    }

    [HttpPost]
    [Consumes("multipart/form-data")]
    public async Task<IActionResult> Edit([FromForm] TimeOffUpdateRequest request)
    {
      if (!ModelState.IsValid)
        return View(request);

      var claimsIdentity = (ClaimsIdentity)this.User.Identity;
      var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
      request.UserId = claim.Value;

      request.FromDate = !String.IsNullOrEmpty(request.FromDateStr) ? DateTime.ParseExact(request.FromDateStr, "dd/MM/yyyy", null) : DateTime.Now;
      request.ToDate = !String.IsNullOrEmpty(request.ToDateStr) ? DateTime.ParseExact(request.ToDateStr, "dd/MM/yyyy", null) : DateTime.Now;
      request.Status = "0";
      var splitData = request.TimeOffTypeId.Split('|');
      if (request.RequestUnit == "Theo ngày")
      {
        TimeSpan difference = request.ToDate.AddDays(1) - request.FromDate;
        double days = difference.TotalDays;
        request.Duration = string.Format("{0} days", days);
      }
      else
      {
        request.Duration = "4 hours";
      }

      request.RequestUnit = splitData[1];
      request.TimeOffTypeId = splitData[0];

      var result = await _timeOffApiClient.UpdateTimeOff(request);
      if (result)
      {
        TempData["result"] = "Cập nhật ngày nghỉ thành công";
        return RedirectToAction("IndexUser");
      }

      ModelState.AddModelError("", "Cập nhật ngày nghỉ thất bại");
      GetDataForCreateOrEdit();
      return View(request);
    }


    [HttpPost]
    public async Task<IActionResult> Delete([FromBody] TimeOffDeleteRequest request)
    {
      if (!ModelState.IsValid)
        return View();

      var result = await _timeOffApiClient.DeleteTimeOff(Int32.Parse(request.Id));
      if (result)
      {
        TempData["result"] = "Xóa ngày nghỉ thành công";
        return RedirectToAction("IndexUser");
      }

      ModelState.AddModelError("", "Xóa không thành công");
      return View(result);
    }

    [HttpGet]
    public async Task<IActionResult> Details(int id)
    {
      var result = await _timeOffApiClient.GetById(id);
      return View(result);
    }

    [HttpPost]
    public async Task<IActionResult> Apply([FromBody] TimeOffApplyRequest request)
    {
      bool result = false;
      if(request.Status == "1")
      {
        result = await _timeOffApiClient.ApplyRequest(Int32.Parse(request.Id), 1);
      }
      else
      {
        result = await _timeOffApiClient.ApplyRequest(Int32.Parse(request.Id), 2);
      }

      if (result)
      {
        TempData["result"] = request.Status == "1" ? "Duyệt ngày nghỉ thành công" : "Từ chối ngày nghỉ thành công";
        return RedirectToAction("Index");
      }

      ModelState.AddModelError("", "Duyệt ngày nghỉ thất bại");
      GetDataForCreateOrEdit();
      return View(request);
    }
  }
}
