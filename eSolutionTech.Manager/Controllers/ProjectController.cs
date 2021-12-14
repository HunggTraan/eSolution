using eSolutionTech.ApiIntegration;
using eSolutionTech.ViewModels.Catalog.Projects;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eSolutionTech.Manager.Controllers
{
    public class ProjectController : Controller
    {
        private readonly IProjectApiClient _projectApiClient;
        private readonly IConfiguration _configuration;

        public ProjectController(IProjectApiClient ProjectApiClient,
            IConfiguration configuration)
        {
            _configuration = configuration;
            _projectApiClient = ProjectApiClient;
        }

        public async Task<IActionResult> Index(string keyword, int pageIndex = 1, int pageSize = 10)
        {
            var request = new GetProjectPagingRequest()
            {
                KeyWord = keyword,
                PageIndex = pageIndex,
                PageSize = pageSize
            };

            var data = await _projectApiClient.GetPagings(request);
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
        public async Task<IActionResult> Create([FromForm] ProjectCreateRequest request)
        {
            if (!ModelState.IsValid)
                return View(request);

            var result = await _projectApiClient.CreateProject(request);
            if (result)
            {
                TempData["result"] = "Thêm mới dự án thành công";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Thêm dự án thất bại");
            return View(request);
        }


        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var Project = await _projectApiClient.GetById(id);
            var editVm = new ProjectUpdateRequest()
            {
                Id = Project.Id,
                Description = Project.Description,
                Name = Project.Name,
                Code = Project.Code,
                EndDate = Project.EndDate,
                StartDate = Project.StartDate,
                ManagerId = Project.ManagerId,
                Status = Project.Status,
                UserIds = Project.UserIds
            };
            return View(editVm);
        }

        [HttpPost]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> Edit([FromForm] ProjectUpdateRequest request)
        {
            if (!ModelState.IsValid)
                return View(request);

            var result = await _projectApiClient.UpdateProject(request);
            if (result)
            {
                TempData["result"] = "Cập nhật dự án thành công";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Cập nhật dự án thất bại");
            return View(request);
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            return View(new ProjectDeleteRequest()
            {
                Id = id
            });
        }

        [HttpPost]
        public async Task<IActionResult> Delete(ProjectDeleteRequest request)
        {
            if (!ModelState.IsValid)
                return View();

            var result = await _projectApiClient.DeleteProject(request.Id);
            if (result)
            {
                TempData["result"] = "Xóa dự án thành công";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Xóa không thành công");
            return View(request);
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var result = await _projectApiClient.GetById(id);
            return View(result);
        }
    }
}
