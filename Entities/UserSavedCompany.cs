using DATN_back_end.Entities;

namespace DATN_back_end.Entities
{
    public class UserSavedCompany : BaseEntity
    {
        public User User { get; set; }

        public Guid UserId { get; set; }

        public Company Company { get; set; }

        public Guid CompanyId { get; set; }
    }
}
