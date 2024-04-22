using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using MyBlog.Data.UnitOfWorks;
using MyBlog.Entity.DTOs.Users;
using MyBlog.Entity.Entities;
using MyBlog.Service.Extensions;
using MyBlog.Service.Services.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace MyBlog.Service.Services.Concretes
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        private readonly UserManager<AppUser> userManager;
        private readonly RoleManager<AppRole> roleManager;
        private readonly SignInManager<AppUser> signInManager;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly ClaimsPrincipal _user;

        public UserService(IUnitOfWork unitOfWork, IMapper mapper, UserManager<AppUser> userManager, RoleManager<AppRole> roleManager, SignInManager<AppUser> signInManager, IHttpContextAccessor httpContextAccessor)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
            this.userManager = userManager;
            this.roleManager = roleManager;
            this.signInManager = signInManager;
            this.httpContextAccessor = httpContextAccessor;
            _user = httpContextAccessor.HttpContext.User;
        }

        public async Task<IdentityResult> CreateUserAsync(UserAddDto userAddDto)
        {
            var map = mapper.Map<AppUser>(userAddDto);
            map.UserName = userAddDto.Email;

            var result = await userManager.CreateAsync(map, string.IsNullOrEmpty(userAddDto.Password) ? "" : userAddDto.Password);

            if (result.Succeeded)
            {
                var findRole = await roleManager.FindByIdAsync(userAddDto.RoleId.ToString());
                await userManager.AddToRoleAsync(map, findRole.ToString());
                return result;

            }
            else return result;
        }

        public async Task<(IdentityResult identityResult, string? email)> DeleteUserAsync(Guid userId)
        {
            var user = await GetAppUserByIdAsync(userId);
            var result = await userManager.DeleteAsync(user);
            return (result, user.Email);
        }

        public async Task<List<AppRole>> GetAllRolesAsync()
        {
            return await roleManager.Roles.ToListAsync();

        }

        public async Task<List<UserDto>> GetAllUsersWithRoleAsync()
        {
            var users = await userManager.Users.ToListAsync();
            var map = mapper.Map<List<UserDto>>(users);

            foreach (var user in map)
            {
                var findUser = await userManager.FindByIdAsync(user.Id.ToString());
                var role = string.Join("", await userManager.GetRolesAsync(findUser));

                user.Role = role;
            }
            return map;
        }

        public async Task<AppUser> GetAppUserByIdAsync(Guid userId)
        {
            return await userManager.FindByIdAsync(userId.ToString());
        }

        public async Task<string> GetUserRoleAsync(AppUser user)
        {
            var role = String.Join("", await userManager.GetRolesAsync(user));
            return role;

        }

        public async Task<IdentityResult> UpdateUserAsync(UserUpdateDto userUpdateDto)
        {
            var user = await GetAppUserByIdAsync(userUpdateDto.Id);
            var userRole = await GetUserRoleAsync(user);
            var result = await userManager.UpdateAsync(user);
            if (result.Succeeded)
            {

                await userManager.RemoveFromRoleAsync(user, userRole);
                var findRole = await roleManager.FindByIdAsync(userUpdateDto.RoleId.ToString());
                await userManager.AddToRoleAsync(user, findRole.Name);

            }
            return result;
        }

        public async Task<UserProfileDto> GetUserProfileAsync()
        {
            var userId = _user.GetLoggedInUserId();
            var loggedInUser = await GetAppUserByIdAsync(userId);
            var map = mapper.Map<UserProfileDto>(loggedInUser);

            return map;
        }

        public async Task<bool> UserProfileUpdateAsync(UserProfileDto userProfileDto)
        {
            var userId = _user.GetLoggedInUserId();
            var loggedInUser = await GetAppUserByIdAsync(userId);

            var isVerified = await userManager.CheckPasswordAsync(loggedInUser, userProfileDto.CurrentPassword);
            if (isVerified && userProfileDto.NewPassword != null)
            {
                var result = await userManager.ChangePasswordAsync(loggedInUser, userProfileDto.CurrentPassword, userProfileDto.NewPassword);
                if (result.Succeeded)
                {
                    await userManager.UpdateSecurityStampAsync(loggedInUser);
                    await signInManager.SignOutAsync();
                    await signInManager.PasswordSignInAsync(loggedInUser, userProfileDto.NewPassword, false, true);

                    loggedInUser.FirstName = userProfileDto.FirstName;
                    loggedInUser.LastName = userProfileDto.LastName;
                    loggedInUser.PhoneNumber = userProfileDto.PhoneNumber;
                    loggedInUser.LinkedInLink =userProfileDto.LinkedInLink;
                    loggedInUser.InstagramLink = userProfileDto.InstagramLink;
                    loggedInUser.ContactMail = userProfileDto.ContactMail;
                    loggedInUser.GithubLink =   userProfileDto.GithubLink;

                    await userManager.UpdateAsync(loggedInUser);

                    return true;
                }
                else 
                {
                    return false;
                }
            }
            else if (isVerified)
            {
                await userManager.UpdateSecurityStampAsync(loggedInUser);

                loggedInUser.FirstName = userProfileDto.FirstName;
                loggedInUser.LastName = userProfileDto.LastName;
                loggedInUser.PhoneNumber = userProfileDto.PhoneNumber;
                loggedInUser.LinkedInLink = userProfileDto.LinkedInLink;
                loggedInUser.InstagramLink = userProfileDto.InstagramLink;
                loggedInUser.ContactMail = userProfileDto.ContactMail;
                loggedInUser.GithubLink = userProfileDto.GithubLink;

                await userManager.UpdateAsync(loggedInUser);

                return true;
            }
            else
            {
                return false;
            }

        }
    }
}
