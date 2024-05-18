using DATN_back_end.Common;
using DATN_back_end.Entities;

namespace DATN_back_end.Entities
{
    public class UserJobPosting : BaseEntity
    {
        public User User { get; set; }
        public Guid UserId { get; set; }
        public JobPosting JobPosting { get; set; }
        public Guid JobPostingId { get; set; }
        public string CV { get; set; }
        public UserJobPostingStatus Status { get; set; }
    }
}
