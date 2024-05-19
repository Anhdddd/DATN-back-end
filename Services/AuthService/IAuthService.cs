using DATN_back_end.Dtos;
using DATN_back_end.Dtos.Auth;

namespace DATN_back_end.Services.AuthService
{
    public interface IAuthService
    {
        Task<CustomResponse<LoginRegisterResponseDto>> Login(LoginDto loginDto);

        Task<CustomResponse<LoginRegisterResponseDto>> Register(RegistrationDto registrationDto);

    }
}
