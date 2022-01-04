using eSolutionTech.ApiIntegration;
using eSolutionTech.ViewModels.System.Roles;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eSolutionTech.Manager.Controllers
{
  public class RoleController : Controller
  {
    private readonly IRoleApiClient _roleApiClient;
    private readonly IConfiguration _configuration;

    public RoleController(
    IConfiguration configuration,
    IRoleApiClient roleApiClient)
    {
      _configuration = configuration;
      _roleApiClient = roleApiClient;
    }
    public async Task<IActionResult> Index(string keyword, int pageIndex = 1, int pageSize = 10)
    {
      try
      {
        var request = new RolePagingRequest()
        {
          Keyword = keyword,
          PageIndex = pageIndex,
          PageSize = pageSize
        };

        ViewBag.Keyword = keyword;

        var data = await _roleApiClient.GetRolesPagings(request);

        var result = data.ResultObj;

        if (TempData["result"] != null)
        {
          ViewBag.SuccessMsg = TempData["result"];
        }
        return View(result);
      }
      catch (Exception ex)
      {
        return RedirectToAction("ErrorView", "Home");
      }
    }

    [HttpGet]
    public IActionResult Create()
    {
      return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateRoleRequest request)
    {
      try
      {
        if (!ModelState.IsValid)
          return View();

        var result = await _roleApiClient.Create(request);
        if (result.IsSuccessed)
        {
          TempData["result"] = "Thêm mới vai trò thành công";
          return RedirectToAction("Index");
        }

        ModelState.AddModelError("", result.Message);

        return View(request);
      }
      catch (Exception ex)
      {
        return RedirectToAction("ErrorView", "Home");
      }
    }

    [HttpGet]
    public async Task<IActionResult> Edit(Guid id)
    {
      var result = await _roleApiClient.GetById(id);
      if (result.IsSuccessed)
      {
        var role = result.ResultObj;
        var updateRequest = new RoleUpdateRequest()
        {
          Description = role.Description,
          Name = role.Name,
          Id = role.Id.ToString()
        };
        return View(updateRequest);
      }
      return RedirectToAction("Error", "Home");
    }

    [HttpPost]
    public async Task<IActionResult> Edit(RoleUpdateRequest request)
    {
      if (!ModelState.IsValid)
        return View();

      var result = await _roleApiClient.Update(Guid.Parse(request.Id), request);
      if (result.IsSuccessed)
      {
        TempData["result"] = "Cập nhật vai trò thành công";
        return RedirectToAction("Index");
      }

      ModelState.AddModelError("", result.Message);
      return View(request);
    }

    [HttpPost]
    public async Task<IActionResult> Delete([FromBody] RoleDeleteRequest request)
    {
      if (!ModelState.IsValid)
        return View();

      var result = await _roleApiClient.Delete(request.Id);
      if (result.IsSuccessed)
      {
        TempData["result"] = "Xóa vai trò thành công";
        return RedirectToAction("Index");
      }

      ModelState.AddModelError("", result.Message);
      return View(request);
    }
  }
}
