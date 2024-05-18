namespace DATN_back_end.Dtos.JobPosting
{
    public class SubmitApplicationDto
    {
        public Guid JobPostingId { get; set; }
        public IFormFile CV { get; set; }
    }
}
