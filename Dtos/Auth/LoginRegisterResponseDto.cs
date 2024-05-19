using DATN_back_end.Common;
using System.ComponentModel.DataAnnotations;

namespace DATN_back_end.Dtos.Auth
{
    public class LoginRegisterResponseDto
    {
        public Guid Id { get; set; }
        public string Token { get; set; }
        public Role Role { get; set; }
    }
}