using DATN_back_end.Common;
using DATN_back_end.Entities;

namespace DATN_back_end.Dtos.JobPosting
{
    public class JobPostingUpdateDto
    {
        public string? Title { get; set; }
        public DateTime? ExpiryDate { get; set; }
        public Occupation? Occupation { get; set; }
        public string? SalaryRange { get; set; }
        public string? Description { get; set; }
        public string? Requirement { get; set; }
        public string? Benefit { get; set; }
        public string? WorkingTime { get; set; }
        public EmployeeType? EmployeeType { get; set; }
        public Guid? ProvinceOrCity { get; set; }
        public int? HiringQuota { get; set; }
        public Experience? Experience { get; set; }
        public string? Location { get; set; }
        public JobPostingStatus? JobPostingStatus { get; set; }
    }
}
