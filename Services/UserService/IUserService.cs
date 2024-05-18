using DATN_back_end.Dtos;
using DATN_back_end.Dtos.User;
using DATN_back_end.Entities;

namespace DATN_back_end.Services.UserService
{
    public interface IUserService 
    {
        Task<User> GetUserByEmailAsync(string email);
        Task<List<User>> GetUsersAsync();
        Task<Guid> AddAsync(User user);

        Task<CustomResponse<UserDetailDto>> GetUserByIdAsync(Guid id);
        Task<CustomResponse<UserDetailDto>> GetMyInfo();
        Task<CustomResponse<UserDetailDto>> UpdateAsync(UserUpdateDto user);
        Task<CustomResponse<UserDetailDto>> UpdateAvatarAsync(IFormFile file);
    }
}