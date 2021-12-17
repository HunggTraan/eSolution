using eShopSolution.Utilities.Exceptions;
using eSolutionTech.ViewModels.Catalog.Shifts;
using eSolutionTech.ViewModels.Catalog.ShiftSettings;
using Microsoft.AspNetCore.Authorization;
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
  public class ShiftSettingsController : ControllerBase
  {
    private readonly IShiftSettingService _shiftSettingService;
    public ShiftSettingsController(IShiftSettingService shiftSettingService)
    {
      _shiftSettingService = shiftSettingService;
    }
    [HttpGet]
    public async Task<IActionResult> Get()
    {
      try
      {

        var ShiftSettings = await _shiftSettingService.GetAll();
        return Ok(ShiftSettings);
      }
      catch (eTechException ex)
      {
        return Ok("Fail");
      }
    }

    [HttpGet("paging")]
    public async Task<IActionResult> GetAllPaging([FromQuery] GetShiftSettingPagingRequest request)
    {
      try
      {
        var ShiftSettings = await _shiftSettingService.GetAllPaging(request);
        if (ShiftSettings != null)
          return Ok(ShiftSettings);
        else
          return Ok("Fail");
      }
      catch (eTechException ex)
      {
        return Ok("Fail");
      }

    }

    [HttpGet("{shiftSettingId}")]
    public async Task<IActionResult> GetById(int shiftSettingId)
    {
      try
      {
        var ShiftSetting = await _shiftSettingService.GetById(shiftSettingId);
        if (ShiftSetting == null)
          return BadRequest("Cannot find jobTitle");
        return Ok(ShiftSetting);
      }
      catch (eTechException ex)
      {
        return Ok("Fail");
      }
    }

    [HttpPost]
    [Consumes("multipart/form-data")]
    public async Task<IActionResult> Create([FromForm] ShiftSettingCreateRequest request)
    {
      if (!ModelState.IsValid)
      {
        return BadRequest(ModelState);
      }
      var shiftSettingId = await _shiftSettingService.Create(request);
      if (shiftSettingId == 0)
        return BadRequest();
      var shiftSetting = await _shiftSettingService.GetById(shiftSettingId);
      return CreatedAtAction(nameof(GetById), new { id = shiftSettingId }, shiftSetting);
    }

    [HttpPut("{shiftSettingId}")]
    [Consumes("multipart/form-data")]
    public async Task<IActionResult> Update([FromRoute] int shiftSettingId, [FromForm] ShiftSettingUpdateRequest request)
    {
      if (!ModelState.IsValid)
      {
        return BadRequest(ModelState);
      }

      request.Id = shiftSettingId;

      var affectedResult = await _shiftSettingService.Update(request);
      if (affectedResult == 0)
        return BadRequest();
      return Ok();
    }

    [HttpDelete("{ShiftSettingId}")]
    public async Task<IActionResult> Delete(int ShiftSettingId)
    {
      var affectedResult = await _shiftSettingService.Delete(ShiftSettingId);
      if (affectedResult == 0)
        return BadRequest();
      return Ok();
    }
  }
}
