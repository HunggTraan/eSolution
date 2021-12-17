using eSolutionTech.ApiIntegration;
using eSolutionTech.ViewModels.Catalog.ShiftSettings;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eSolutionTech.Manager.Controllers
{
  public class ShiftSettingController : Controller
  {
    private readonly IShiftSettingApiClient _shiftSettingApiClient;
    private readonly IConfiguration _configuration;

    public ShiftSettingController(IShiftSettingApiClient shiftSettingApiClient,
        IConfiguration configuration)
    {
      _configuration = configuration;
      _shiftSettingApiClient = shiftSettingApiClient;
    }

    public async Task<IActionResult> Index(string keyword, int pageIndex = 1, int pageSize = 10)
    {
      var request = new GetShiftSettingPagingRequest()
      {
        KeyWord = keyword,
        PageIndex = pageIndex,
        PageSize = pageSize
      };

      var data = await _shiftSettingApiClient.GetPagings(request);
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
    public async Task<IActionResult> Create([FromForm] ShiftSettingCreateRequest request)
    {
      if (!ModelState.IsValid)
        return View(request);

      var result = await _shiftSettingApiClient.CreateShiftSetting(request);
      if (result)
      {
        TempData["result"] = "Thêm mới loại khai báo thành công";
        return RedirectToAction("Index");
      }

      ModelState.AddModelError("", "Thêm loại khai báo thất bại");
      return View(request);
    }


    [HttpGet]
    public async Task<IActionResult> Edit(int id)
    {
      var shiftSetting = await _shiftSettingApiClient.GetById(id);
      var editVm = new ShiftSettingUpdateRequest()
      {
        Id = shiftSetting.Id,
        Name = shiftSetting.Name,
        Code = shiftSetting.Code,
        TimeIn = shiftSetting.TimeIn,
        TimeOut = shiftSetting.TimeOut,
        ExceedTimeIn = shiftSetting.ExceedTimeIn,
        ExceedTimeOut = shiftSetting.ExceedTimeOut
      };
      return View(editVm);
    }

    [HttpPost]
    [Consumes("multipart/form-data")]
    public async Task<IActionResult> Edit([FromForm] ShiftSettingUpdateRequest request)
    {
      if (!ModelState.IsValid)
        return View(request);

      var result = await _shiftSettingApiClient.UpdateShiftSetting(request);
      if (result)
      {
        TempData["result"] = "Cập nhật loại khai báo thành công";
        return RedirectToAction("Index");
      }

      ModelState.AddModelError("", "Cập nhật loại khai báo thất bại");
      return View(request);
    }

    [HttpGet]
    public IActionResult Delete(int id)
    {
      return View(new ShiftSettingDeleteRequest()
      {
        Id = id
      });
    }

    [HttpPost]
    public async Task<IActionResult> Delete(ShiftSettingDeleteRequest request)
    {
      if (!ModelState.IsValid)
        return View();

      var result = await _shiftSettingApiClient.DeleteShiftSetting(request.Id);
      if (result)
      {
        TempData["result"] = "Xóa loại khai báo thành công";
        return RedirectToAction("Index");
      }

      ModelState.AddModelError("", "Xóa không thành công");
      return View(request);
    }

    [HttpGet]
    public async Task<IActionResult> Details(int id)
    {
      var result = await _shiftSettingApiClient.GetById(id);
      return View(result);
    }
  }
}
