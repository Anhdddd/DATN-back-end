using DATN_back_end.Common;
using DATN_back_end.Dtos;
using DATN_back_end.Dtos.Company;

namespace DATN_back_end.Services.CompanyService
{
    public interface ICompanyService
    {
        Task<CustomResponse<CompanyDetailDto>> AddAsync(CompanyAddDto companyDto);
        Task<CustomResponse<CompanyDetailDto>> UpdateAsync (CompanyUpdateDto companyDto);
        Task<CustomResponse<CompanyDetailDto>> UpdateLogoAsync(CompanyUpdateImageDto companyDto);
        Task<CustomResponse<List<CompanyDto>>> GetCompaniesAsync(FilterCompanyDto? filterCompanyDto, string searchValue, int pageSize, int pageNumber);
        Task<CustomResponse<CompanyDetailDto>> GetCompanyByIdAsync(Guid CompanyId);
        Task<CustomResponse<CompanyDetailDto>> GetMyCompanyAsync();
        Task<CustomResponse<object>> SaveCompanyAsync(Guid companyId);
        Task<CustomResponse<List<CompanyDto>>> GetSavedCompanyAsync(int pageSize, int pageNumber);
        Task<CustomResponse<CompanyDetailDto>> UpdateCompanyStatus(Guid companyId, CompanyStatus companyStatus);
    }
}
