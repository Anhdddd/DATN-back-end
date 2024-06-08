using DATN_back_end.Common;
using DATN_back_end.Dtos.JobPosting;
using DATN_back_end.Dtos.UserJobPosting;
using DATN_back_end.Filters;
using DATN_back_end.Services.JobPostingService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DATN_back_end.Controllers.JobPosting
{
    [Route("api/[controller]")]
    [ApiController]
    public class JobPostingController : ControllerBase
    {
        private readonly IJobPostingService _jobPostingService;

        public JobPostingController(IJobPostingService jobPostingService)
        {
            _jobPostingService = jobPostingService;
        }

        [AuthorizeFilterAttribute([Role.Employer])]
        [HttpPost]
        public async Task<IActionResult> AddAsync([FromBody] JobPostingAddDto jobPostingAddDto)
        {
            var jobPostingId = await _jobPostingService.AddAsync(jobPostingAddDto);
            return Ok(jobPostingId);
        }

        [AuthorizeFilterAttribute([Role.Employer])]
        [HttpPut("{jobPostingId}")]
        public async Task<IActionResult> UpdateAsync([FromRoute] Guid jobPostingId, [FromBody] JobPostingUpdateDto jobPostingUpdateDto)
        {
            await _jobPostingService.UpdateAsync(jobPostingId, jobPostingUpdateDto);
            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> GetJobPostingsAsync([FromQuery] FilterJobPostingDto filterJobPostingDto, int pageSize, int pageNumber)
        {
            var jobPostings = await _jobPostingService.GetJobPostingsAsync(filterJobPostingDto, pageSize, pageNumber);
            return Ok(jobPostings);
        }

        [HttpGet("{jobPostingId}")]
        public async Task<IActionResult> GetJobPostingByIdAsync(Guid jobPostingId)
        {
            var jobPosting = await _jobPostingService.GetJobPostingByIdAsync(jobPostingId);
            return Ok(jobPosting);
        }

        [AuthorizeFilterAttribute([Role.User])]
        [HttpPost("{jobPostingId}/save")]
        public async Task<IActionResult> SaveJobPosting(Guid jobPostingId)
        {
            await _jobPostingService.SaveJobPosting(jobPostingId);
            return Ok();
        }

        [AuthorizeFilterAttribute([Role.User])]
        [HttpPost("{jobPostingId}/unsave")]
        public async Task<IActionResult> UnSaveJobPosting(Guid jobPostingId)
        {
            await _jobPostingService.UnSaveJobPosting(jobPostingId);
            return Ok();
        }

        [AuthorizeFilterAttribute([Role.User])]
        [HttpGet("saved")]
        public async Task<IActionResult> GetSavedJobPostingsAsync(int pageSize = 10, int pageNumber = 1)
        {
            var jobPostings = await _jobPostingService.GetSavedJobPostingsAsync(pageSize, pageNumber);
            return Ok(jobPostings);
        }

        [AuthorizeFilterAttribute([Role.User])]
        [HttpPost("{jobPostingId}/increase-view-count")]
        public async Task<IActionResult> IncreaseViewCount(Guid jobPostingId)
        {
            await _jobPostingService.IncreaseViewCount(jobPostingId);
            return Ok();
        }

        [AuthorizeFilterAttribute([Role.Employer])]
        [HttpPost("{jobPostingId}/update-status")]
        public async Task<IActionResult> UpdateJobPostingStatus(Guid jobPostingId, JobPostingStatus jobPostingStatus)
        {
            await _jobPostingService.UpdateJobPostingStatus(jobPostingId, jobPostingStatus);
            return Ok();
        }

        [AuthorizeFilterAttribute([Role.Employer])]
        [HttpPost("{id}/change-status")]
        public async Task<IActionResult> ChangeUserJobPostingStatus(Guid id, UserJobPostingStatus status)
        {
            await _jobPostingService.ChangeUserJobPostingStatus(id, status);
            return Ok();
        }

        [AuthorizeFilterAttribute([Role.Employer])]
        [HttpGet("user")]
        public async Task<IActionResult> GetUserJobPostingsAsync([FromQuery] FilterUserJobPostingDto filterUserJobPostingDto, int pageSize, int pageNumber)
        {
            var userJobPostings = await _jobPostingService.GetUserJobPostingsAsync(filterUserJobPostingDto, pageSize, pageNumber);
            return Ok(userJobPostings);
        }

        [AuthorizeFilterAttribute([Role.User])]
        [HttpPost("submit-application")]
        public async Task<IActionResult> SubmitApplication(SubmitApplicationDto submitApplicationDto)
        {
            await _jobPostingService.SubmitApplication(submitApplicationDto);
            return Ok();
        }

        [HttpGet("top-occupation")]
        public async Task<IActionResult> GetTopOccupation()
        {
            var topOccupation = await _jobPostingService.GetTopOccupation();
            return Ok(topOccupation);
        }

        [HttpGet("occupations")]
        public async Task<IActionResult> GetOccupations()
        {
            var occupations = await _jobPostingService.GetOccupations();
            return Ok(occupations);
        }

        [AuthorizeFilterAttribute([Role.Employer])]
        [HttpGet("my-company-job-postings")]
        public async Task<IActionResult> GetMyCompanyJobPostingsAsync(int pageSize, int pageNumber)
        {
            var jobPostings = await _jobPostingService.GetMyCompanyJobPostingsAsync(pageSize, pageNumber);
            return Ok(jobPostings);
        }

    }
}
