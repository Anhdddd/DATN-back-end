using DATN_back_end.Dtos;

namespace DATN_back_end.Services.DashBoardService
{
    public interface IEmployerDashboardService
    {
        Task<CustomResponse<object>> GetTotalJobPostings();
        Task<CustomResponse<object>> GetTotalViews();
        Task<CustomResponse<object>> GetTotalApplications();
    }
}
