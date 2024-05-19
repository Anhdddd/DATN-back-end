using DATN_back_end.Common;
using DATN_back_end.Dtos;
using DATN_back_end.Dtos.Auth;
using DATN_back_end.Entities;
using DATN_back_end.Services.UserService;

namespace DATN_back_end.Services.AuthService
{
    public class AuthService : BaseService, IAuthService
    {
        private readonly IUserService _userService;

        public AuthService(IUserService userService, IServiceProvider serviceProvider) : base(serviceProvider)
        {
            _userService = userService;

        }

        public async Task<CustomResponse<LoginRegisterResponseDto>> Login(LoginDto loginDto)
        {
            var currentUser = await _userService.GetUserByEmailAsync(loginDto.Email);
            if (currentUser == null)
            {
                throw new InvalidCredentialsException();
            }

            var hashedPassword = SecurityFunction.HashPassword(loginDto.Password, currentUser.PasswordSalt);
            if (currentUser.HashedPassword != hashedPassword)
            {
                throw new InvalidCredentialsException();
            }

            return new CustomResponse<LoginRegisterResponseDto>
            {
                Data = new LoginRegisterResponseDto
                {
                    Id = currentUser.Id,
                    Role = currentUser.Role,
                    Token = SecurityFunction.GenerateToken(new ClaimData()
                    {
                        UserId = currentUser.Id,
                        Role = currentUser.Role,
                        FullName = currentUser.FullName,
                        Email = currentUser.Email
                    }, _configuration)
                }
            };

        }

        public async Task<CustomResponse<LoginRegisterResponseDto>> Register(RegistrationDto registrationDto)
        {
            var users = await _userService.GetUsersAsync();
            if (users.Any(u => u.Email == registrationDto.Email))
            {
                throw new EmailIsAlreadyExistedException();
            }

            var passwordSalt = SecurityFunction.GenerateRandomString();
            var hashedPassword = SecurityFunction.HashPassword(registrationDto.Password, passwordSalt);

            var newUser = new User
            {
                FullName = registrationDto.FullName,
                PasswordSalt = passwordSalt,
                HashedPassword = hashedPassword,
                Email = registrationDto.Email,
                Role = (Role)registrationDto.Role,
                PhoneNumber = registrationDto.PhoneNumber,
                FacebookLink = registrationDto.FacebookLink
            };

            await _userService.AddAsync(newUser);

            return new CustomResponse<LoginRegisterResponseDto>
            {
                Data = new LoginRegisterResponseDto
                {
                    Id = newUser.Id,
                    Role = newUser.Role,
                    Token = SecurityFunction.GenerateToken(new ClaimData()
                    {
                        UserId = newUser.Id,
                        Role = newUser.Role,
                        FullName = newUser.FullName,
                        Email = newUser.Email
                    }, _configuration)
                }
            };
        }
    }
}