using DATN_back_end.Dtos.Auth;

namespace DATN_back_end.Services.AuthService
{
    public interface IAuthService
    {
        Task<string> Login(LoginDto loginDto);

        Task<string> Register(RegistrationDto registrationDto);

    }
}
