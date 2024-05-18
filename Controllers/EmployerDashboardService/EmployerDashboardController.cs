using DATN_back_end.Filters;
using DATN_back_end.Services.DashBoardService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DATN_back_end.Controllers.Dashboard
{
    [Route("api/[controller]")]
    [ApiController]
    [AuthorizeFilter]
    public class EmployerDashboardController : ControllerBase
    {
        private readonly IEmployerDashboardService _employerDashboardService;

        public EmployerDashboardController(IEmployerDashboardService employerDashboardService)
        {
            _employerDashboardService = employerDashboardService;
        }

        [HttpGet("total-job-postings")]
        public IActionResult GetTotalJobPostings()
        {
            return Ok(_employerDashboardService.GetTotalJobPostings());
        }

        [HttpGet("total-applications")]
        public IActionResult GetTotalApplications()
        {
            return Ok(_employerDashboardService.GetTotalApplications());
        }

        [HttpGet("total-views")]
        public IActionResult GetTotalViews()
        {
            return Ok(_employerDashboardService.GetTotalViews());
        }

    }
}
