namespace DATN_back_end.Dtos.JobPosting
{
    public class JobPostingDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string SalaryRange { get; set; }
        public string CompanyName { get; set; }
        public string CompanyLogo { get; set; }
        public Guid CompanyId { get; set; }
        public string OccupationName { get; set; }
        public Guid OccupationId { get; set; }
        public DateTimeOffset CreatedDate { get; set; }
    }
}
// ok Vu