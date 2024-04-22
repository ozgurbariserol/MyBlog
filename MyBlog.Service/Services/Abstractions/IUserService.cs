using Microsoft.AspNetCore.Identity;
using MyBlog.Entity.DTOs.Users;
using MyBlog.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBlog.Service.Services.Abstractions
{
    public interface IUserService
    {
        Task<List<UserDto>> GetAllUsersWithRoleAsync();
        Task<List<AppRole>> GetAllRolesAsync();

        Task<IdentityResult> CreateUserAsync(UserAddDto userAddDto);
        Task<IdentityResult> UpdateUserAsync(UserUpdateDto userUpdateDto);
        Task<(IdentityResult identityResult,string? email)> DeleteUserAsync(Guid userId);
        Task<AppUser> GetAppUserByIdAsync(Guid userId);
        Task<String> GetUserRoleAsync(AppUser user);
        Task<UserProfileDto> GetUserProfileAsync();
        Task<bool> UserProfileUpdateAsync(UserProfileDto userProfileDto);
    }
}
