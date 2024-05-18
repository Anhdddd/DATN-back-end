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
    [AuthorizeFilter]
    public class JobPostingController : ControllerBase
    {
        private readonly IJobPostingService _jobPostingService;

        public JobPostingController(IJobPostingService jobPostingService)
        {
            _jobPostingService = jobPostingService;
        }

        [HttpPost]
        public async Task<IActionResult> AddAsync([FromForm] JobPostingAddDto jobPostingAddDto)
        {
            var jobPostingId = await _jobPostingService.AddAsync(jobPostingAddDto);
            return Ok(jobPostingId);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateAsync([FromForm] JobPostingUpdateDto jobPostingUpdateDto)
        {
            await _jobPostingService.UpdateAsync(jobPostingUpdateDto);
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

        [HttpPost("{jobPostingId}/save")]
        public async Task<IActionResult> SaveJobPosting(Guid jobPostingId)
        {
            await _jobPostingService.SaveJobPosting(jobPostingId);
            return Ok();
        }

        [HttpPost("{jobPostingId}/unsave")]
        public async Task<IActionResult> UnSaveJobPosting(Guid jobPostingId)
        {
            await _jobPostingService.UnSaveJobPosting(jobPostingId);
            return Ok();
        }

        [HttpGet("saved")]
        public async Task<IActionResult> GetSavedJobPostingsAsync(int pageSize = 10, int pageNumber = 1)
        {
            var jobPostings = await _jobPostingService.GetSavedJobPostingsAsync(pageSize, pageNumber);
            return Ok(jobPostings);
        }

        [HttpPost("{jobPostingId}/increase-view-count")]
        public async Task<IActionResult> IncreaseViewCount(Guid jobPostingId)
        {
            await _jobPostingService.IncreaseViewCount(jobPostingId);
            return Ok();
        }

        [HttpPost("{jobPostingId}/update-status")]
        public async Task<IActionResult> UpdateJobPostingStatus(Guid jobPostingId, JobPostingStatus jobPostingStatus)
        {
            await _jobPostingService.UpdateJobPostingStatus(jobPostingId, jobPostingStatus);
            return Ok();
        }

        [HttpPost("{id}/change-status")]
        public async Task<IActionResult> ChangeUserJobPostingStatus(Guid id, UserJobPostingStatus status)
        {
            await _jobPostingService.ChangeUserJobPostingStatus(id, status);
            return Ok();
        }

        [HttpGet("user")]
        public async Task<IActionResult> GetUserJobPostingsAsync([FromQuery] FilterUserJobPostingDto filterUserJobPostingDto, int pageSize, int pageNumber)
        {
            var userJobPostings = await _jobPostingService.GetUserJobPostingsAsync(filterUserJobPostingDto, pageSize, pageNumber);
            return Ok(userJobPostings);
        }

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

    }
}
