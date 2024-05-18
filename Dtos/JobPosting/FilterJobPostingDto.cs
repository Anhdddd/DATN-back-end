using DATN_back_end.Common;

namespace DATN_back_end.Dtos.JobPosting
{
    public class FilterJobPostingDto
    {
        public string? SearchValue { get; set; }
        public Guid? CompanyId { get; set; }
        public JobPostingSortType? SortType { get; set; }
    }
}
