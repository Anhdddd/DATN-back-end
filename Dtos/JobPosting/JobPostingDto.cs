namespace DATN_back_end.Dtos.JobPosting
{
    public class JobPostingDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string SalaryRange { get; set; }
        public string CompanyName { get; set; }
        public string CompanyLogo { get; set; }
    }
}
