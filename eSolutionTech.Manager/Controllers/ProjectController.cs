using eSolutionTech.ApiIntegration;
using eSolutionTech.ViewModels.Catalog.Projects;
using eSolutionTech.ViewModels.Common;
using eSolutionTech.ViewModels.System.Users;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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
        private readonly IUserApiClient _userApiClient;
        private readonly IConfiguration _configuration;

        public ProjectController(IProjectApiClient ProjectApiClient,
            IUserApiClient userApiClient,
            IConfiguration configuration)
        {
            _configuration = configuration;
            _projectApiClient = ProjectApiClient;
            _userApiClient = userApiClient;
        }

        protected void GetDataForCreateOrEdit()
        {
            try
            {
                var users = _userApiClient.GetAll();
                ViewBag.Users = users.Result.ResultObj.Select(x => new SelectListItem()
                {
                    Text = x.FullName,
                    Value = x.Id.ToString()
                });
            }
            catch (Exception ex)
            {
                return;
            }
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
            GetDataForCreateOrEdit();
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
            GetDataForCreateOrEdit();
            return View(request);
        }


        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            GetDataForCreateOrEdit();
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
                shiftSettingId = Project.shiftSettingId,
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
            GetDataForCreateOrEdit();
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

            List<UserViewModel> userInfos = new List<UserViewModel>();
            UserViewModel managerInfos = new UserViewModel();

            var managerInfo = await _userApiClient.GetById(Guid.Parse(result.ManagerId));

            managerInfos = managerInfo.ResultObj;

            foreach (var item in result.UserIds)
            {
                var userInfo = await _userApiClient.GetById(Guid.Parse(item));
                var userInfor = userInfo.ResultObj;
                userInfos.Add(userInfor);
            }

            var user = ViewData["User"] = userInfos;
            var manager = ViewData["Manager"] = managerInfos;

            return View(result);
        }
    }
}
