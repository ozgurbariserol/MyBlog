using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyBlog.Entity.DTOs.Articles;
using MyBlog.Entity.DTOs.Users;
using MyBlog.Entity.Entities;
using MyBlog.Service.Extensions;
using MyBlog.Service.Services.Abstractions;
using MyBlog.Service.Services.Concretes;
using MyBlog.Web.Consts;
using MyBlog.Web.ResultMessages;
using NToastNotify;
using System.Security.Claims;
using static MyBlog.Web.ResultMessages.Message;

namespace MyBlog.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class UserController : Controller
    {
        private readonly IMapper mapper;
        private readonly IValidator<AppUser> validator;
        private readonly IToastNotification toast;
        private readonly IUserService userService;

        public UserController(IMapper mapper, IValidator<AppUser> validator, IToastNotification toast, IUserService userService)
        {
            this.mapper = mapper;
            this.validator = validator;
            this.toast = toast;
            this.userService = userService;
        }
        [Authorize(Roles = $"{RoleConsts.Superadmin}")]

        public async Task<IActionResult> Index()
        {
            var data = await userService.GetAllUsersWithRoleAsync();
            return View(data);
        }
        [HttpGet]
        [Authorize(Roles = $"{RoleConsts.Superadmin}")]

        public async Task<IActionResult> Add()
        {
            var roles = await userService.GetAllRolesAsync();
            return View(new UserAddDto { Roles = roles });
        }
        [HttpPost]
        [Authorize(Roles = $"{RoleConsts.Superadmin}")]

        public async Task<IActionResult> Add(UserAddDto userAddDto)
        {
            var map = mapper.Map<AppUser>(userAddDto);
            var validation = await validator.ValidateAsync(map);
            var roles = await userService.GetAllRolesAsync();

            if (ModelState.IsValid)
            {
                var result = await userService.CreateUserAsync(userAddDto);
                if (result.Succeeded)
                {

                    toast.AddSuccessToastMessage(Message.User.Add(userAddDto.Email), new ToastrOptions { Title = "Başarılı!" });
                    return RedirectToAction("Index", "User", new { Area = "Admin" });
                }
                else
                {
                    result.AddToIdentityModelState(this.ModelState);
                    validation.AddToModelState(this.ModelState);
                    return View(new UserAddDto { Roles = roles });

                }
            }
            return View(new UserAddDto { Roles = roles });
        }

        [HttpGet]
        [Authorize(Roles = $"{RoleConsts.Superadmin}")]

        public async Task<IActionResult> Update(Guid userId)
        {
            var user = await userService.GetAppUserByIdAsync(userId);
            var roles = await userService.GetAllRolesAsync();

            var map = mapper.Map<UserUpdateDto>(user);
            map.Roles = roles;

            return View(map);
        }

        [HttpPost]
        [Authorize(Roles = $"{RoleConsts.Superadmin}")]
        public async Task<IActionResult> Update(UserUpdateDto userUpdateDto)
        {
            var user = await userService.GetAppUserByIdAsync(userUpdateDto.Id);

            if (user != null) {
                var roles = await userService.GetAllRolesAsync();
                if (ModelState.IsValid)
                {
                    var map = mapper.Map(userUpdateDto, user);
                    var validation = await validator.ValidateAsync(map);

                    if (validation.IsValid)
                    {
                        user.UserName = userUpdateDto.Email;
                        user.SecurityStamp = Guid.NewGuid().ToString();
                        var result = await userService.UpdateUserAsync(userUpdateDto);
                        if (result.Succeeded)
                        {
                            toast.AddSuccessToastMessage(Message.User.Update(userUpdateDto.Email), new ToastrOptions { Title = "Başarılı!" });
                            return RedirectToAction("Index", "User", new { Area = "Admin" });
                        }
                        else
                        {
                            result.AddToIdentityModelState(this.ModelState);
                            return View(new UserUpdateDto { Roles = roles });
                        }
                    }
                    else
                    {
                        validation.AddToModelState(this.ModelState);
                        return View(new UserUpdateDto { Roles = roles });
                    }

                }

            }
            return NotFound();
        }
        [Authorize(Roles = $"{RoleConsts.Superadmin}")]

        public async Task<IActionResult> Delete(Guid userId)
        {

            var result = await userService.DeleteUserAsync(userId);

            if (result.identityResult.Succeeded)
            {
                toast.AddSuccessToastMessage(Message.User.Update(result.email), new ToastrOptions { Title = "Başarılı!" });
                return RedirectToAction("Index", "User", new { Area = "Admin" });
            }
            else
            {
                foreach (var errors in result.identityResult.Errors)
                {
                    ModelState.AddModelError("", errors.Description);
                }
            }
            return NotFound();
        }
        [HttpGet]
        [Authorize(Roles = $"{RoleConsts.Superadmin},{RoleConsts.Admin},{RoleConsts.User}")]
        public async Task<IActionResult> Profile()
        {
            var data = await userService.GetUserProfileAsync();

            return View(data);
        }

        [HttpPost]
        [Authorize(Roles = $"{RoleConsts.Superadmin},{RoleConsts.Admin},{RoleConsts.User}")]
        public async Task<IActionResult> Profile(UserProfileDto userProfileDto)
        {

            if (ModelState.IsValid)
            {
                var result = await userService.UserProfileUpdateAsync(userProfileDto);
                if (result)
                {
                    toast.AddSuccessToastMessage("Profiliniz güncellenmiştir.", new ToastrOptions { Title = "Başarılı!" });
                    return RedirectToAction("Index", "Home", new { Area = "Admin" });
                }
                else
                {
                    toast.AddErrorToastMessage("Profiliniz güncellenirken hata oluşmuştur.", new ToastrOptions { Title = "Başarılı!" });
                    return View();
                }

            }
            else
            {
               return  NotFound();
            }

        }

    }
}
