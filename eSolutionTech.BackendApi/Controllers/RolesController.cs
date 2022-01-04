using eSolutionTech.Application.System.Roles;
using eSolutionTech.ViewModels.System.Roles;
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
  [Authorize(Roles = "Administrator")]
  public class RolesController : ControllerBase
  {
    private readonly IRoleService _roleService;

    public RolesController(IRoleService roleService)
    {
      _roleService = roleService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
      var roles = await _roleService.GetAll();
      return Ok(roles);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(string id)
    {
      var result = await _roleService.Delete(id);
      return Ok(result);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] RoleUpdateRequest request)
    {
      if (!ModelState.IsValid)
        return BadRequest(ModelState);

      var result = await _roleService.Update(id, request);
      if (!result.IsSuccessed)
      {
        return BadRequest(result);
      }
      return Ok(result);
    }


    [HttpGet("paging")]
    public async Task<IActionResult> GetAllPaging([FromQuery] RolePagingRequest request)
    {
      var users = await _roleService.GetRolesPaging(request);
      return Ok(users);
    }

    [HttpPost("create")]
    public async Task<IActionResult> Create([FromBody] CreateRoleRequest request)
    {
      if (!ModelState.IsValid)
        return BadRequest(ModelState);

      var result = await _roleService.CreateRole(request);
      if (!result.IsSuccessed)
      {
        return BadRequest(result);
      }
      return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
      var user = await _roleService.GetById(id);
      return Ok(user);
    }
  }
}
