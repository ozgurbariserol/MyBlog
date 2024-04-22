using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using MyBlog.Entity.DTOs.Articles;
using MyBlog.Entity.DTOs.Categories;
using MyBlog.Entity.Entities;
using MyBlog.Service.Extensions;
using MyBlog.Service.Services.Abstractions;
using MyBlog.Service.Services.Concretes;
using MyBlog.Web.Consts;
using MyBlog.Web.ResultMessages;
using NToastNotify;
using System.ComponentModel.DataAnnotations;

namespace MyBlog.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        private readonly ICategoryService categoryService;
        private readonly IMapper mapper;
        private readonly IValidator<Category> validator;
        private readonly IToastNotification toast;
        private readonly IHttpContextAccessor httpContextAccessor;

        public CategoryController(ICategoryService categoryService,IMapper mapper, IValidator<Category> validator ,IToastNotification toast, IHttpContextAccessor httpContextAccessor)
        {
            this.categoryService = categoryService;
            this.mapper = mapper;
            this.validator = validator;
            this.toast = toast;
            this.httpContextAccessor = httpContextAccessor;
        }

        [Authorize(Roles = $"{RoleConsts.Superadmin},{RoleConsts.Admin},{RoleConsts.User},")]
        public async Task<ActionResult> Index()
        {
            var categories = await categoryService.GetAllCategoriesNonDeleted();
            return View(categories);
        }

        [Authorize(Roles = $"{RoleConsts.Superadmin},{RoleConsts.Admin}")]
        public async Task<ActionResult> DeletedCategory()
        {
            var categories = await categoryService.GetAllCategoriesDeleted();
            return View(categories);
        }

        [HttpGet]
        [Authorize(Roles = $"{RoleConsts.Superadmin},{RoleConsts.Admin}")]
        public  IActionResult Add()
        {
            return View();
        }

        [HttpPost]  
        [Authorize(Roles = $"{RoleConsts.Superadmin},{RoleConsts.Admin}")]
        public async Task<IActionResult> Add(CategoryAddDto categoryAddDto)
        {
            var map = mapper.Map<Category>(categoryAddDto);
            var result = await validator.ValidateAsync(map);


            if (result.IsValid)
            {
                await categoryService.CreateCategoryAsync(categoryAddDto);
                toast.AddSuccessToastMessage(Message.Category.Add(categoryAddDto.Name), new ToastrOptions { Title = "Başarılı!" });
                return RedirectToAction("Index", "Category", new { Area = "Admin" });
            }
            result.AddToModelState(this.ModelState);
            return View();
        }

        [HttpPost]
        [Authorize(Roles = $"{RoleConsts.Superadmin},{RoleConsts.Admin}")]
        public async Task<IActionResult> AddWithAjax([FromBody] CategoryAddDto categoryAddDto)
        {
            var map = mapper.Map<Category>(categoryAddDto);
            var result = await validator.ValidateAsync(map);

            if (result.IsValid)
            {
                await categoryService.CreateCategoryAsync(categoryAddDto);
                toast.AddSuccessToastMessage(Message.Category.Add(categoryAddDto.Name), new ToastrOptions { Title = "Başarılı!" });
                return Json(Message.Category.Add(categoryAddDto.Name));
            }
            else
            {
                toast.AddErrorToastMessage(result.Errors.First().ErrorMessage,new ToastrOptions { Title = "Başarısız!" });
                return Json(result.Errors.First().ErrorMessage);
            }
            
        }

        [HttpGet]
        [Authorize(Roles = $"{RoleConsts.Superadmin},{RoleConsts.Admin}")]
        public async Task<IActionResult> Update(Guid categoryId)
        {
            var category = await categoryService.GetCategoryByGuidAsync(categoryId);
            var map = mapper.Map<CategoryUpdateDto>(category);
            return View(map);
        }

        [HttpPost]
        [Authorize(Roles = $"{RoleConsts.Superadmin},{RoleConsts.Admin}")]
        public async Task<IActionResult> Update(CategoryUpdateDto categoryUpdateDto)
        {
            var map = mapper.Map<Category>(categoryUpdateDto);
            var result = await validator.ValidateAsync(map);

            if (result.IsValid)
            {
                var name = await categoryService.UpdateCategoryAsync(categoryUpdateDto);

                toast.AddSuccessToastMessage(Message.Category.Update(name), new ToastrOptions { Title = "Başarılı!" });
                return RedirectToAction("Index", "Category", new { Area = "Admin" });
            }
            result.AddToModelState(this.ModelState);
            return View();
        }
        [HttpPost]
        [Authorize(Roles = $"{RoleConsts.Superadmin},{RoleConsts.Admin}")]
        public async Task<IActionResult> Delete(Guid categoryId)
        {
            var name = await categoryService.SafeDeleteCategoryAsync(categoryId);

            toast.AddSuccessToastMessage(Message.Category.Delete(name), new ToastrOptions { Title = "Başarılı !" });

            return RedirectToAction("Index", "Category", new { Area = "Admin" });
        }

        [Authorize(Roles = $"{RoleConsts.Superadmin}")]
        public async Task<IActionResult> UndoDelete(Guid categoryId)
        {
            var name = await categoryService.UndoDeleteCategoryAsync(categoryId);

            toast.AddSuccessToastMessage(Message.Category.UndoDelete(name), new ToastrOptions { Title = "Başarılı !" });

            return RedirectToAction("Index", "Category", new { Area = "Admin" });
        }
    }
}
