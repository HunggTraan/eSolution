using eShopSolution.Utilities.Exceptions;
using eSolutionTech.ViewModels.Catalog.TimeOffTypes;
using eSolutionTech.ViewModels.Catalog.TimeOffTypes.Dtos;
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
    public class TimeOffTypesController : ControllerBase
    {
        private readonly ITimeOffTypeService _timeOffTypeService;
        public TimeOffTypesController(ITimeOffTypeService timeOffTypeService)
        {
            _timeOffTypeService = timeOffTypeService;
        }
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {

                var timeOffTypes = await _timeOffTypeService.GetAll();
                return Ok(timeOffTypes);
            }
            catch (eTechException ex)
            {
                return Ok("Fail");
            }
        }

        [HttpGet("paging")]
        public async Task<IActionResult> GetAllPaging([FromQuery] GetTimeOffTypePagingRequest request)
        {
            try
            {
                var timeOffTypes = await _timeOffTypeService.GetAllPaging(request);
                if (timeOffTypes != null)
                    return Ok(timeOffTypes);
                else
                    return Ok("Fail");
            }
            catch (eTechException ex)
            {
                return Ok("Fail");
            }

        }

        [HttpGet("{timeOffTypeId}")]
        public async Task<IActionResult> GetById(int timeOffTypeId)
        {
            try
            {
                var timeOffType = await _timeOffTypeService.GetById(timeOffTypeId);
                if (timeOffType == null)
                    return BadRequest("Cannot find timeOffType");
                return Ok(timeOffType);
            }
            catch (eTechException ex)
            {
                return Ok("Fail");
            }
        }

        [HttpPost]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> Create([FromForm] TimeOffTypeCreateRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var timeOffTypeId = await _timeOffTypeService.Create(request);
            if (timeOffTypeId == 0)
                return BadRequest();
            var timeOffType = await _timeOffTypeService.GetById(timeOffTypeId);
            return CreatedAtAction(nameof(GetById), new { id = timeOffTypeId }, timeOffType);
        }

        [HttpPut("{timeOffTypeId}")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> Update([FromRoute] int timeOffTypeId, [FromForm] TimeOffTypeUpdateRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            request.Id = timeOffTypeId;

            var affectedResult = await _timeOffTypeService.Update(request);
            if (affectedResult == 0)
                return BadRequest();
            return Ok();
        }

        [HttpDelete("{timeOffTypeId}")]
        public async Task<IActionResult> Delete(int timeOffTypeId)
        {
            var affectedResult = await _timeOffTypeService.Delete(timeOffTypeId);
            if (affectedResult == 0)
                return BadRequest();
            return Ok();
        }
    }
}
