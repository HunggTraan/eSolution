using eShopSolution.Utilities.Exceptions;
using eSolutionTech.ViewModels.Catalog.Shifts;
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
  public class ShiftsController : ControllerBase
  {
    private readonly IShiftService _shiftService;
    public ShiftsController(IShiftService shiftService)
    {
      _shiftService = shiftService;
    }

    [HttpGet("paging")]
    public async Task<IActionResult> GetAllPaging([FromQuery] GetShiftPagingRequest request)
    {
      try
      {
        var shifts = await _shiftService.GetAllPaging(request);
        if (shifts != null)
          return Ok(shifts);
        else
          return Ok("Fail");
      }
      catch (eTechException ex)
      {
        return Ok("Fail");
      }

    }

    [HttpGet("{shiftId}")]
    public async Task<IActionResult> GetById(int shiftId)
    {
      try
      {
        var shift = await _shiftService.GetById(shiftId);
        if (shift == null)
          return BadRequest("Không tìm thấy lịch chấm công");
        return Ok(shift);
      }
      catch (eTechException ex)
      {
        return Ok("Fail");
      }
    }

    [HttpPost]
    [Consumes("multipart/form-data")]
    public async Task<IActionResult> Create([FromForm] ShiftCreateRequest request)
    {
      if (!ModelState.IsValid)
      {
        return BadRequest(ModelState);
      }
      var shiftId = await _shiftService.LoginShift(request);
      if (shiftId == 0)
        return Ok(new { Value = "Chấm công thất bại", Code = 0 });
      else if(shiftId == 2)
      {
        return Ok(new { Value = "Đã chấm công vào ngày hôm nay!" , Code = 2});
      }

      return Ok(new { Value = "Chấm công thành công", Code = 1 });
    }

    [HttpPost("{logout}")]
    [Consumes("multipart/form-data")]
    public async Task<IActionResult> Update([FromForm] ShiftUpdateRequest request)
    {
      if (!ModelState.IsValid)
      {
        return BadRequest(ModelState);
      }

      var affectedResult = await _shiftService.LogoutShift(request);
      if (affectedResult == 0)
        return Ok(new { Value = "Chấm công thất bại", Code = 0 });
      else if (affectedResult == 2)
      {
        return Ok(new { Value = "Chưa chấm công vào!", Code = 2 });
      }
      else if (affectedResult == 3)
      {
        return Ok(new { Value = "Chưa đến giờ chấm công ra!", Code = 3 });
      }

      return Ok(new { Value = "Chấm công thành công", Code = 1 });
    }

    [HttpDelete("{shiftId}")]
    public async Task<IActionResult> Delete(int shiftId)
    {
      var affectedResult = await _shiftService.Delete(shiftId);
      if (affectedResult == 0)
        return BadRequest();
      return Ok();
    }
  }
}
