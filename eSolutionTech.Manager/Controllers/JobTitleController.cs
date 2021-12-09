using eSolutionTech.ApiIntegration;
using eSolutionTech.ViewModels.Catalog.JobTitles;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eSolutionTech.Manager.Controllers
{
    public class JobTitleController : Controller
    {
        private readonly IJobTitleApiClient _jobTitleApiClient;
        private readonly IConfiguration _configuration;

        public JobTitleController(IJobTitleApiClient jobTitleApiClient,
            IConfiguration configuration)
        {
            _configuration = configuration;
            _jobTitleApiClient = jobTitleApiClient;
        }

        public async Task<IActionResult> Index(string keyword, int pageIndex = 1, int pageSize = 10)
        {
            var request = new GetJobTitlePagingRequest()
            {
                KeyWord = keyword,
                PageIndex = pageIndex,
                PageSize = pageSize
            };

            var data = await _jobTitleApiClient.GetPagings(request);
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
        public async Task<IActionResult> Create([FromForm] JobTitleCreateRequest request)
        {
            if (!ModelState.IsValid)
                return View(request);

            var result = await _jobTitleApiClient.CreateJobTitle(request);
            if (result)
            {
                TempData["result"] = "Thêm mới chức vụ thành công";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Thêm chức vụ thất bại");
            return View(request);
        }


        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var JobTitle = await _jobTitleApiClient.GetById(id);
            var editVm = new JobTitleUpdateRequest()
            {
                Id = JobTitle.Id,
                Description = JobTitle.Description,
                Name = JobTitle.Name,
                Code = JobTitle.Code
            };
            return View(editVm);
        }

        [HttpPost]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> Edit([FromForm] JobTitleUpdateRequest request)
        {
            if (!ModelState.IsValid)
                return View(request);

            var result = await _jobTitleApiClient.UpdateJobTitle(request);
            if (result)
            {
                TempData["result"] = "Cập nhật chức vụ thành công";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Cập nhật chức vụ thất bại");
            return View(request);
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            return View(new JobTitleDeleteRequest()
            {
                Id = id
            });
        }

        [HttpPost]
        public async Task<IActionResult> Delete(JobTitleDeleteRequest request)
        {
            if (!ModelState.IsValid)
                return View();

            var result = await _jobTitleApiClient.DeleteJobTitle(request.Id);
            if (result)
            {
                TempData["result"] = "Xóa chức vụ thành công";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Xóa không thành công");
            return View(request);
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var result = await _jobTitleApiClient.GetById(id);
            return View(result);
        }
    }
}
