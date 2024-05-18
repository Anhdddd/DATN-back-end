namespace DATN_back_end.Dtos.UserJobPosting
{
    public class UserJobPostingDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public DateTime SubmittedDate { get; set; }
        public string Status { get; set; }
        public string CV { get; set; }
        public string SalaryRange { get; set; }
    }
}
