using DATN_back_end.Dtos.Company;
using DATN_back_end.Dtos;
using DATN_back_end.Dtos.JobPosting;
using DATN_back_end.Common;
using DATN_back_end.Dtos.UserJobPosting;

namespace DATN_back_end.Services.JobPostingService
{
    public interface IJobPostingService
    {
        Task<CustomResponse<JobPostingDetailDto>> AddAsync(JobPostingAddDto jobPostingDto);
        Task<CustomResponse<JobPostingDetailDto>> UpdateAsync(JobPostingUpdateDto jobPostingDto);
        Task<CustomResponse<List<JobPostingDto>>> GetJobPostingsAsync(FilterJobPostingDto? filterJobPostingDto, int pageSize, int pageNumber);
        Task<CustomResponse<JobPostingDetailDto>> GetJobPostingByIdAsync(Guid jobPostingId);
        Task<CustomResponse<object>> SaveJobPosting(Guid jobPostingId);
        Task<CustomResponse<object>> UnSaveJobPosting(Guid jobPostingId);
        Task<CustomResponse<List<JobPostingDto>>> GetSavedJobPostingsAsync(int pageSize, int pageNumber);
        Task<CustomResponse<object>> IncreaseViewCount(Guid jobPostingId);
        Task<CustomResponse<JobPostingDetailDto>> UpdateJobPostingStatus(Guid jobPostingId, JobPostingStatus jobPostingStatus);
        Task<CustomResponse<UserJobPostingDto>> ChangeUserJobPostingStatus(Guid id, UserJobPostingStatus status);
        Task<CustomResponse<List<UserJobPostingDto>>> GetUserJobPostingsAsync(FilterUserJobPostingDto filterUserJobPostingDto, int pageSize, int pageNumber);
        Task<CustomResponse<UserJobPostingDto>> SubmitApplication(SubmitApplicationDto submitApplicationDto);
        Task<CustomResponse<Dictionary<string, int>>> GetTopOccupation();
    }
}
