using eShopSolution.Utilities.Exceptions;
using eSolutionTech.ViewModels.Catalog.Projects;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace eSolutionTech.BackendApi.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  [Authorize]
  public class ProjectsController : ControllerBase
  {
    private readonly IProjectService _projectService;
    public ProjectsController(IProjectService projectService)
    {
      _projectService = projectService;
    }
    [HttpGet]
    public async Task<IActionResult> Get()
    {
      try
      {

        var projects = await _projectService.GetAll();
        return Ok(projects);
      }
      catch (eTechException ex)
      {
        return Ok("Fail");
      }
    }

    [HttpGet("paging")]
    public async Task<IActionResult> GetAllPaging([FromQuery] GetProjectPagingRequest request)
    {
      try
      {
        var projects = await _projectService.GetAllPaging(request);
        if (projects != null)
          return Ok(projects);
        else
          return Ok("Fail");
      }
      catch (eTechException ex)
      {
        return Ok("Fail");
      }

    }

    [HttpGet("{projectId}")]
    public async Task<IActionResult> GetById(int projectId)
    {
      try
      {
        var project = await _projectService.GetById(projectId);
        if (project == null)
          return BadRequest("Cannot find jobTitle");
        return Ok(project);
      }
      catch (eTechException ex)
      {
        return Ok("Fail");
      }
    }

    [HttpPost]
    [Consumes("multipart/form-data")]
    public async Task<IActionResult> Create([FromForm] ProjectCreateRequest request)
    {
      if (!ModelState.IsValid)
      {
        return BadRequest(ModelState);
      }
      var projectId = await _projectService.Create(request);
      if (projectId == 0)
        return BadRequest();
      var project = await _projectService.GetById(projectId);
      return CreatedAtAction(nameof(GetById), new { id = projectId }, project);
    }

    [HttpPut("{projectId}")]
    [Consumes("multipart/form-data")]
    public async Task<IActionResult> Update([FromRoute] int timeOffTypeId, [FromForm] ProjectUpdateRequest request)
    {
      if (!ModelState.IsValid)
      {
        return BadRequest(ModelState);
      }

      request.Id = timeOffTypeId;

      var affectedResult = await _projectService.Update(request);
      if (affectedResult == 0)
        return BadRequest();
      return Ok();
    }

    [HttpDelete("{projectId}")]
    public async Task<IActionResult> Delete(int projectId)
    {
      var affectedResult = await _projectService.Delete(projectId);
      if (affectedResult == 0)
        return BadRequest();
      return Ok();
    }
  }
}
