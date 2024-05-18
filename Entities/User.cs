using DATN_back_end.Common;
using DATN_back_end.Entities;

namespace DATN_back_end.Entities
{
    public class User : BaseEntity
{
        public string Email { get; set; }
        public string HashedPassword { get; set; }
        public string PasswordSalt { get; set; }
        public string FullName { get; set; }
        public string? PhoneNumber { get; set; }
        public string? FacebookLink { get; set; }
        public string? Avatar { get; set; }
        public Company? Company { get; set; }
        public Guid? CompanyId { get; set; }
        public Role Role { get; set; }
        public List<Notification> Notifications { get; set; }

    }
}
