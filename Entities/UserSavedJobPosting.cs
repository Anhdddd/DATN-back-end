using DATN_back_end.Entities;

namespace DATN_back_end.Entities
{
    public class UserSavedJobPosting : BaseEntity
    {
        public User User { get; set; }

        public Guid UserId { get; set; }

        public JobPosting JobPosting { get; set; }

        public Guid JobPostingId { get; set; }
    }
}
