using eShopSolution.Utilities.Exceptions;
using eSolutionTech.ViewModels.Catalog.Departments;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eSolutionTech.BackendApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private readonly IDepartmentService _departmentService;
        public DepartmentController(IDepartmentService departmentService)
        {
            _departmentService = departmentService;
        }
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                
                var departments = await _departmentService.GetAll();
                return Ok(departments);
            }
            catch(eTechException ex)
            {
                return Ok("Fail");
            }
        }
    }
}
