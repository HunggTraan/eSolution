using eSolutionTech.ApiIntegration;
using eSolutionTech.ViewModels.Catalog.Departments;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eSolutionTech.Manager.Controllers
{
    public class DepartmentController : Controller
    {
        private readonly IDepartmentApiClient _departmentApiClient;
        private readonly IConfiguration _configuration;

        public DepartmentController(IDepartmentApiClient departmentApiClient,
            IConfiguration configuration)
        {
            _configuration = configuration;
            _departmentApiClient = departmentApiClient;
        }

        public async Task<IActionResult> Index(string keyword, int pageIndex = 1, int pageSize = 10)
        {
            var request = new GetDepartmentPagingRequest()
            {
                KeyWord = keyword,
                PageIndex = pageIndex,
                PageSize = pageSize
            };

            var data = await _departmentApiClient.GetPagings(request);
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
        public async Task<IActionResult> Create([FromForm] DepartmentCreateRequest request)
        {
            if (!ModelState.IsValid)
                return View(request);

            var result = await _departmentApiClient.CreateDepartment(request);
            if (result)
            {
                TempData["result"] = "Thêm mới phòng ban thành công";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Thêm phòng ban thất bại");
            return View(request);
        }


        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var department = await _departmentApiClient.GetById(id);
            var editVm = new DepartmentUpdateRequest()
            {
                Id = department.Id,
                Description = department.Description,
                Name = department.Name,
                Code = department.Code
            };
            return View(editVm);
        }

        [HttpPost]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> Edit([FromForm] DepartmentUpdateRequest request)
        {
            if (!ModelState.IsValid)
                return View(request);

            var result = await _departmentApiClient.UpdateDepartment(request);
            if (result)
            {
                TempData["result"] = "Cập nhật sản phẩm thành công";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Cập nhật sản phẩm thất bại");
            return View(request);
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            return View(new DepartmentDeleteRequest()
            {
                Id = id
            });
        }

        [HttpPost]
        public async Task<IActionResult> Delete(DepartmentDeleteRequest request)
        {
            if (!ModelState.IsValid)
                return View();

            var result = await _departmentApiClient.DeleteDepartment(request.Id);
            if (result)
            {
                TempData["result"] = "Xóa sản phẩm thành công";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Xóa không thành công");
            return View(request);
        }
    }
}
