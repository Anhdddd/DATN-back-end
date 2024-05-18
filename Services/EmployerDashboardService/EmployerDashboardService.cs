
using DATN_back_end.Dtos;
using DATN_back_end.Entities;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace DATN_back_end.Services.DashBoardService
{
    public class EmployerDashboardService : BaseService, IEmployerDashboardService
    {
        public EmployerDashboardService(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }

        public async Task<CustomResponse<object>> GetTotalJobPostings()
        {
            var myCompanyId = await (await _unitOfWork.Queryable<Company>()).Where(x => x.OwnerId == _currentUserService.UserId).Select(x => x.Id).FirstOrDefaultAsync();

            return new CustomResponse<object>
            {
                Data = await (await _unitOfWork.Queryable<JobPosting>()).Where(x => x.CompanyId == myCompanyId).CountAsync(),
            };
        }

        public async Task<CustomResponse<object>> GetTotalApplications()
        {
            var myCompanyId = await (await _unitOfWork.Queryable<Company>()).Where(x => x.OwnerId == _currentUserService.UserId).Select(x => x.Id).FirstOrDefaultAsync();

            return new CustomResponse<object>
            {
                Data = await (await _unitOfWork.Queryable<UserJobPosting>()).Where(x => x.JobPosting.CompanyId == myCompanyId).CountAsync(),
            };
        }

        public async Task<CustomResponse<object>> GetTotalViews()
        {
            var myCompanyId = await (await _unitOfWork.Queryable<Company>()).Where(x => x.OwnerId == _currentUserService.UserId).Select(x => x.Id).FirstOrDefaultAsync();

            return new CustomResponse<object>
            {
                Data = await (await _unitOfWork.Queryable<JobPosting>()).Where(x => x.CompanyId == myCompanyId).SumAsync(x => x.ViewCount),
            };
        }
    }
}
