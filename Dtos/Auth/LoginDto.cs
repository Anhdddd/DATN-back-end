using DATN_back_end.Common;
using System.ComponentModel.DataAnnotations;

namespace DATN_back_end.Dtos.Auth
{
    public class LoginDto
    {
        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public Role Role { get; set; }
    }
}