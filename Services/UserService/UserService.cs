using DATN_back_end.Common;
using DATN_back_end.Dtos;
using DATN_back_end.Dtos.User;
using DATN_back_end.Entities;
using Microsoft.EntityFrameworkCore;

namespace DATN_back_end.Services.UserService
{
    public class UserService : BaseService, IUserService
    {
        public UserService(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }

        public async Task<Guid> AddAsync(User user)
        {
            await _unitOfWork.AddAsync(user);
            await _unitOfWork.SaveChangesAsync();

            return user.Id;
        }

        public async Task<CustomResponse<UserDetailDto>> GetMyInfo()
        {
            var user = await (await _unitOfWork.Queryable<User>()).FirstOrDefaultAsync(u => u.Id == _currentUserService.UserId);
            if (user == null)
            {
                throw new NotFoundException();
            }

            return new CustomResponse<UserDetailDto>
            {
                Data = _mapper.Map<UserDetailDto>(user),
            };
        }

        public async Task<User> GetUserByEmailAsync(string email)
        {
            var userQuery = await _unitOfWork.Queryable<User>();
            var user = await userQuery.FirstOrDefaultAsync(u => u.Email == email);
            return user;
        }

        public async Task<CustomResponse<UserDetailDto>> GetUserByIdAsync(Guid id)
        {
            var user = await (await _unitOfWork.Queryable<User>()).FirstOrDefaultAsync(u => u.Id == id);
            if (user == null)
            {
                throw new NotFoundException();
            }

            return new CustomResponse<UserDetailDto>
            {
                Data = _mapper.Map<UserDetailDto>(user),
            };
        }

        public async Task<List<User>> GetUsersAsync()
        {
            var userQuery = await _unitOfWork.Queryable<User>();
            var users = await userQuery.ToListAsync();
            return users;
        }

        public async Task<CustomResponse<UserDetailDto>> UpdateAsync(UserUpdateDto user)
        {
            var userToUpdate = await (await _unitOfWork.Queryable<User>()).FirstOrDefaultAsync(u => u.Id == _currentUserService.UserId);
            if (userToUpdate == null)
            {
                throw new NotFoundException();
            }

            if (user.FullName != null)
            {
                userToUpdate.FullName = user.FullName;
            }

            if (user.PhoneNumber != null)
            {
                userToUpdate.PhoneNumber = user.PhoneNumber;
            }

            if (user.FacebookLink != null)
            {
                userToUpdate.FacebookLink = user.FacebookLink;
            }

            await _unitOfWork.UpdateAsync(userToUpdate);
            await _unitOfWork.SaveChangesAsync();

            return new CustomResponse<UserDetailDto>
            {
                Data = _mapper.Map<UserDetailDto>(userToUpdate),
            };
        }

        public async Task<CustomResponse<UserDetailDto>> UpdateAvatarAsync(IFormFile file)
        {
            var userToUpdate = await(await _unitOfWork.Queryable<User>()).FirstOrDefaultAsync(u => u.Id == _currentUserService.UserId);

            if (userToUpdate == null)
            {
                throw new NotFoundException();
            }
            if (file != null)
            {
                userToUpdate.Avatar = await _fileService.UploadFileGetUrlAsync(file);
            }

            await _unitOfWork.UpdateAsync(userToUpdate);
            await _unitOfWork.SaveChangesAsync();

            return new CustomResponse<UserDetailDto>
            {
                Data = _mapper.Map<UserDetailDto>(userToUpdate),
            };
        }
    }
}