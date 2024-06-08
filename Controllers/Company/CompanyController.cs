using DATN_back_end.Common;
using DATN_back_end.Dtos.Company;
using DATN_back_end.Filters;
using DATN_back_end.Services.CompanyService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DATN_back_end.Controllers.Company
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompanyController : ControllerBase
    {
        private readonly ICompanyService _companyService;

        public CompanyController(ICompanyService companyService)
        {
            _companyService = companyService;
        }

        [AuthorizeFilterAttribute([Role.Employer])]
        [HttpPut]
        public async Task<IActionResult> UpdateCompany([FromBody] CompanyUpdateDto companyDto)
        {
            await _companyService.UpdateAsync(companyDto);
            return Ok();
        }

        [AuthorizeFilterAttribute([Role.Employer])]
        [HttpPut("logo")]
        public async Task<IActionResult> UpdateLogo([FromForm] CompanyUpdateImageDto companyDto)
        {
            await _companyService.UpdateLogoAsync(companyDto);
            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> GetCompanies([FromQuery] FilterCompanyDto? filterCompanyDto, string searchValue = "", int pageSize = 10, int pageNumber = 1)
        {
            var companies = await _companyService.GetCompaniesAsync(filterCompanyDto, searchValue, pageSize, pageNumber);
            return Ok(companies);
        }

        [HttpGet("{companyId}")]
        public async Task<IActionResult> GetCompanyById(Guid companyId)
        {
            var company = await _companyService.GetCompanyByIdAsync(companyId);
            return Ok(company);
        }

        [AuthorizeFilterAttribute([Role.Employer])]
        [HttpGet("my-company")]
        public async Task<IActionResult> GetMyCompany()
        {
            var company = await _companyService.GetMyCompanyAsync();
            return Ok(company);
        }

        [AuthorizeFilterAttribute([Role.User])]
        [HttpPost("{companyId}/save")]
        public async Task<IActionResult> SaveCompany(Guid companyId)
        {
            await _companyService.SaveCompanyAsync(companyId);
            return Ok();
        }

        [AuthorizeFilterAttribute([Role.Employer])]
        [HttpPost]
        public async Task<IActionResult> AddCompany([FromForm] CompanyAddDto companyDto)
        {
            var companyId = await _companyService.AddAsync(companyDto);
            return Ok(companyId);
        }

        [AuthorizeFilterAttribute([Role.User])]
        [HttpGet("saved")]
        public async Task<IActionResult> GetSavedCompanies(int pageSize = 10, int pageNumber = 1)
        {
            var companies = await _companyService.GetSavedCompanyAsync(pageSize, pageNumber);
            return Ok(companies);
        }

        [AuthorizeFilterAttribute([Role.Employer, Role.Admin])]
        [HttpPut("{companyId}/status")]
        public async Task<IActionResult> UpdateCompanyStatus(Guid companyId, CompanyStatus companyStatus)
        {
            await _companyService.UpdateCompanyStatus(companyId, companyStatus);
            return Ok();
        }
    }
}
