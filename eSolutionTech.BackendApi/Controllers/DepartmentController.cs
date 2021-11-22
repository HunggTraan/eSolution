using eShopSolution.Utilities.Exceptions;
using eSolutionTech.ViewModels.Catalog.Departments;
using Microsoft.AspNetCore.Authorization;
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
    [Authorize]
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

        [HttpGet("paging")]
        public async Task<IActionResult> GetAllPaging([FromQuery] GetDepartmentPagingRequest request)
        {
            try
            {
                var departments = await _departmentService.GetAllPaging(request);
                if (departments != null)
                    return Ok(departments);
                else
                    return Ok("Fail");
            }
            catch (eTechException ex)
            {
                return Ok("Fail");
            }

        }

        [HttpGet("{departmentId}")]
        public async Task<IActionResult> GetById(int departmentId)
        {
            try
            {
                var department = await _departmentService.GetById(departmentId);
                if (department == null)
                    return BadRequest("Cannot find department");
                return Ok(department);
            }
            catch (eTechException ex)
            {
                return Ok("Fail");
            }
        }

        [HttpPost]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> Create([FromForm] DepartmentCreateRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var departmentId = await _departmentService.Create(request);
            if (departmentId == 0)
                return BadRequest();
            var department = await _departmentService.GetById(departmentId);
            return CreatedAtAction(nameof(GetById), new { id = departmentId }, department);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromForm] DepartmentUpdateRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var affectedResult = await _departmentService.Update(request);
            if (affectedResult == 0)
                return BadRequest();
            return Ok();
        }

        [HttpDelete("{departmentId}")]
        public async Task<IActionResult> Delete(int departmentId)
        {
            var affectedResult = await _departmentService.Delete(departmentId);
            if (affectedResult == 0)
                return BadRequest();
            return Ok();
        }
    }
}
