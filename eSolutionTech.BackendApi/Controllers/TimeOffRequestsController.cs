using eShopSolution.Utilities.Exceptions;
using eSolutionTech.Application.Catalog.TimeOffRequests;
using eSolutionTech.Data.Entities;
using eSolutionTech.ViewModels.Catalog.TimeOffRequests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace eSolutionTech.BackendApi.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  [Authorize]
  public class TimeOffRequestsController : ControllerBase
  {
    private readonly ITimeOffRequestsService _timeOffService;
    private readonly UserManager<User> _userManager;
    public TimeOffRequestsController(ITimeOffRequestsService timeOffService, UserManager<User> userManager)
    {
      _timeOffService = timeOffService;
      _userManager = userManager;
    }
    [HttpGet]
    public async Task<IActionResult> Get()
    {
      try
      {

        var timeOffRequests = await _timeOffService.GetAll();
        return Ok(timeOffRequests);
      }
      catch (eTechException ex)
      {
        return Ok("Fail");
      }
    }

    [HttpGet("paging")]
    public async Task<IActionResult> GetAllPaging([FromQuery] TimeOffPagingRequest request)
    {
      try
      {
        var TimeOffRequestss = await _timeOffService.GetAllPaging(request);
        if (TimeOffRequestss != null)
          return Ok(TimeOffRequestss);
        else
          return Ok("Fail");
      }
      catch (eTechException ex)
      {
        return Ok("Fail");
      }
    }

    [HttpGet("paging-user")]
    public async Task<IActionResult> GetAllPagingUser([FromQuery] TimeOffPagingRequest request)
    {
      try
      {
        var TimeOffRequestss = await _timeOffService.GetAllPaging(request);
        if (TimeOffRequestss != null)
          return Ok(TimeOffRequestss);
        else
          return Ok("Fail");
      }
      catch (eTechException ex)
      {
        return Ok("Fail");
      }
    }

    [HttpGet("{timeOffRequestsId}")]
    public async Task<IActionResult> GetById(int TimeOffRequestsId)
    {
      try
      {
        var TimeOffRequests = await _timeOffService.GetById(TimeOffRequestsId);
        if (TimeOffRequests == null)
          return BadRequest("Không tìm thấy lịch xin nghỉ");
        return Ok(TimeOffRequests);
      }
      catch (eTechException ex)
      {
        return Ok("Fail");
      }
    }

    [HttpPost]
    [Consumes("multipart/form-data")]
    public async Task<IActionResult> Create([FromForm] TimeOffCreateRequest request)
    {
      if (!ModelState.IsValid)
      {
        return BadRequest(ModelState);
      }
      var TimeOffRequestsId = await _timeOffService.Create(request);
      if (TimeOffRequestsId == 0)
        return BadRequest();
      var TimeOffRequests = await _timeOffService.GetById(2);
      return CreatedAtAction(nameof(GetById), new { id = 2 }, TimeOffRequests);
    }

    [HttpPut("{timeOffRequestsId}")]
    [Consumes("multipart/form-data")]
    public async Task<IActionResult> Update([FromRoute] int TimeOffRequestsId, [FromForm] TimeOffUpdateRequest request)
    {
      if (!ModelState.IsValid)
      {
        return BadRequest(ModelState);
      }

      request.Id = TimeOffRequestsId;

      var affectedResult = await _timeOffService.Update(request);
      if (affectedResult == 0)
        return BadRequest();
      return Ok();
    }

    [HttpDelete("{timeOffRequestsId}")]
    public async Task<IActionResult> Delete(int TimeOffRequestsId)
    {
      var affectedResult = await _timeOffService.Delete(TimeOffRequestsId);
      if (affectedResult == 0)
        return BadRequest();
      return Ok();
    }

    [HttpPost("{apply}")]
    [Consumes("multipart/form-data")]
    public async Task<IActionResult> Apply([FromForm] TimeOffApplyRequest request)
    {
      if (!ModelState.IsValid)
      {
        return BadRequest(ModelState);
      }

      var affectedResult = await _timeOffService.Apply(Int32.Parse(request.Id), request.Status);
      if (affectedResult == 0)
        return BadRequest();
      return Ok();
    }
  }
}
