using eSolutionTech.ApiIntegration;
using eSolutionTech.ViewModels.Catalog.TimeOffTypes;
using eSolutionTech.ViewModels.Catalog.TimeOffTypes.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eSolutionTech.Manager.Controllers
{
  public class TimeOffTypeController : BaseController
  {
    private readonly ITimeOffTypeApiClient _timeOffTypeApiClient;
    private readonly IConfiguration _configuration;

    public TimeOffTypeController(ITimeOffTypeApiClient timeOffTypeApiClient,
        IConfiguration configuration)
    {
      _configuration = configuration;
      _timeOffTypeApiClient = timeOffTypeApiClient;
    }

    public async Task<IActionResult> Index(string keyword, int pageIndex = 1, int pageSize = 10)
    {
      var request = new GetTimeOffTypePagingRequest()
      {
        KeyWord = keyword,
        PageIndex = pageIndex,
        PageSize = pageSize
      };

      var data = await _timeOffTypeApiClient.GetPagings(request);
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
      return View();
    }

    [HttpPost]
    [Consumes("multipart/form-data")]
    public async Task<IActionResult> Create([FromForm] TimeOffTypeCreateRequest request)
    {
      if (!ModelState.IsValid)
        return View(request);

      var result = await _timeOffTypeApiClient.CreateTimeOffType(request);
      if (result)
      {
        TempData["result"] = "Thêm mới loại ngày nghỉ thành công";
        return RedirectToAction("Index");
      }

      ModelState.AddModelError("", "Thêm loại ngày nghỉ thất bại");
      return View(request);
    }


    [HttpGet]
    public async Task<IActionResult> Edit(int id)
    {
      var TimeOffType = await _timeOffTypeApiClient.GetById(id);
      var editVm = new TimeOffTypeUpdateRequest()
      {
        Id = TimeOffType.Id,
        Description = TimeOffType.Description,
        Name = TimeOffType.Name,
        Code = TimeOffType.Code,
        EndDate = TimeOffType.EndDate,
        StartDate = TimeOffType.StartDate,
        RequestUnit = TimeOffType.RequestUnit,
        Unpaid = TimeOffType.Unpaid
      };
      return View(editVm);
    }

    [HttpPost]
    [Consumes("multipart/form-data")]
    public async Task<IActionResult> Edit([FromForm] TimeOffTypeUpdateRequest request)
    {
      if (!ModelState.IsValid)
        return View(request);

      var result = await _timeOffTypeApiClient.UpdateTimeOffType(request);
      if (result)
      {
        TempData["result"] = "Cập nhật loại ngày nghỉ thành công";
        return RedirectToAction("Index");
      }

      ModelState.AddModelError("", "Cập nhật loại ngày nghỉ thất bại");
      return View(request);
    }

    [HttpPost]
    public async Task<IActionResult> Delete([FromBody] TimeOffTypeDeleteRequest request)
    {
      if (!ModelState.IsValid)
        return View();

      var result = await _timeOffTypeApiClient.DeleteTimeOffType(Int32.Parse(request.Id));
      if (result)
      {
        TempData["result"] = "Xóa loại ngày nghỉ thành công";
        return RedirectToAction("Index");
      }

      ModelState.AddModelError("", "Xóa không thành công");
      return View(request);
    }

    [HttpGet]
    public async Task<IActionResult> Details(int id)
    {
      var result = await _timeOffTypeApiClient.GetById(id);
      return View(result);
    }
  }
}
