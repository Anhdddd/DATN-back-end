using DATN_back_end.Common;

namespace DATN_back_end.Dtos.User
{
    public class UserUpdateDto
    {
        public string FullName { get; set; }
        public string? PhoneNumber { get; set; }
        public string? FacebookLink { get; set; }
    }
}
