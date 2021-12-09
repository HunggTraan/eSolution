using eShopSolution.Utilities.Exceptions;
using eSolutionTech.ViewModels.Catalog.JobTitles;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace eSolutionTech.BackendApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class JobTitlesController : ControllerBase
    {
        private readonly IJobTitleService _jobTitleService;
        public JobTitlesController(IJobTitleService jobTitleService)
        {
            _jobTitleService = jobTitleService;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {

                var jobTitles = await _jobTitleService.GetAll();
                return Ok(jobTitles);
            }
            catch (eTechException ex)
            {
                return Ok("Fail");
            }
        }

        [HttpGet("paging")]
        public async Task<IActionResult> GetAllPaging([FromQuery] GetJobTitlePagingRequest request)
        {
            try
            {
                var jobTitles = await _jobTitleService.GetAllPaging(request);
                if (jobTitles != null)
                    return Ok(jobTitles);
                else
                    return Ok("Fail");
            }
            catch (eTechException ex)
            {
                return Ok("Fail");
            }

        }

        [HttpGet("{jobTitleId}")]
        public async Task<IActionResult> GetById(int jobTitleId)
        {
            try
            {
                var jobTitle = await _jobTitleService.GetById(jobTitleId);
                if (jobTitle == null)
                    return BadRequest("Cannot find jobTitle");
                return Ok(jobTitle);
            }
            catch (eTechException ex)
            {
                return Ok("Fail");
            }
        }

        [HttpPost]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> Create([FromForm] JobTitleCreateRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var jobTitleId = await _jobTitleService.Create(request);
            if (jobTitleId == 0)
                return BadRequest();
            var department = await _jobTitleService.GetById(jobTitleId);
            return CreatedAtAction(nameof(GetById), new { id = jobTitleId }, department);
        }

        [HttpPut]
        [HttpPut("{departmentId}")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> Update([FromRoute] int jobTitleId, [FromForm] JobTitleUpdateRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (jobTitleId > 0)
                request.Id = jobTitleId;

            var affectedResult = await _jobTitleService.Update(request);
            if (affectedResult == 0)
                return BadRequest();
            return Ok();
        }

        [HttpDelete("{jobTitleId}")]
        public async Task<IActionResult> Delete(int jobTitleId)
        {
            var affectedResult = await _jobTitleService.Delete(jobTitleId);
            if (affectedResult == 0)
                return BadRequest();
            return Ok();
        }
    }
}
