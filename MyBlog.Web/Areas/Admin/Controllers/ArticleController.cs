using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyBlog.Entity.DTOs.Articles;
using MyBlog.Entity.Entities;
using MyBlog.Service.Extensions;
using MyBlog.Service.Services.Abstractions;
using MyBlog.Web.Consts;
using MyBlog.Web.ResultMessages;
using NToastNotify;
using System.Security.Claims;

namespace MyBlog.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ArticleController : Controller
    {
        private readonly IArticleService articleService;
        private readonly IMapper mapper;
        private readonly IValidator<Article> validator;
        private readonly IToastNotification toast;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly ICategoryService categoryService;
        private readonly ClaimsPrincipal _user;

        public ArticleController(IArticleService articleService,ICategoryService categoryService,IMapper mapper, IValidator<Article> validator, IToastNotification toast,IHttpContextAccessor httpContextAccessor)
        {
            this.articleService = articleService;
            this.mapper = mapper;
            this.validator = validator;
            this.toast = toast;
            this.httpContextAccessor = httpContextAccessor;
            this.categoryService = categoryService;
            _user = httpContextAccessor.HttpContext.User;
        }

        [HttpGet]
        [Authorize(Roles = $"{RoleConsts.Superadmin},{RoleConsts.Admin},{RoleConsts.User},")]
        public async Task<IActionResult> Index()
        {
            var articles = await articleService.GetAllArticlesWithCategoryNonDeletedAsync();
            return View(articles);
        }
        [HttpGet]
        [Authorize(Roles = $"{RoleConsts.Superadmin},{RoleConsts.Admin}")]
        public async Task<IActionResult> DeletedArticle()
        {
            var articles = await articleService.GetAllArticlesWithCategoryDeletedAsync();
            return View(articles);
        }


        [HttpGet]
        [Authorize(Roles = $"{RoleConsts.Superadmin},{RoleConsts.Admin}")]
        public async Task<IActionResult> Add()
        {
            var categories = await categoryService.GetAllCategoriesNonDeleted();
            return View(new ArticleAddDto
            {
                Categories = categories
            });
        }
        [HttpPost]
        [Authorize(Roles = $"{RoleConsts.Superadmin},{RoleConsts.Admin}")]
        public async Task<IActionResult> Add(ArticleAddDto articleAddDto)
        { 

            var map = mapper.Map<Article>(articleAddDto);
            var result = await validator.ValidateAsync(map);
            

            if (result.IsValid)
            {
                await articleService.CreateArticleAsync(articleAddDto);
                toast.AddSuccessToastMessage(Message.Article.Add(articleAddDto.Title),new ToastrOptions { Title = "Başarılı!"});
                return RedirectToAction("Index", "Article", new { Area = "Admin" });
                
            }
            else
            {
                result.AddToModelState(this.ModelState);
                var categories = await categoryService.GetAllCategoriesNonDeleted();
                return View(new ArticleAddDto
                {
                    Categories = categories
                });
            }


        }
        [HttpGet]
        [Authorize(Roles = $"{RoleConsts.Superadmin},{RoleConsts.Admin}")]

        public async Task<IActionResult> Update(Guid articleId)
        {
            var article = await articleService.GetArticleWithCategoryNonDeletedAsync(articleId);
            var categories = await categoryService.GetAllCategoriesNonDeleted();

            var articleUpdateDto = mapper.Map<ArticleUpdateDto>(article);
            articleUpdateDto.Categories = categories;
            
            return View(articleUpdateDto);
        }

        [HttpPost]
        [Authorize(Roles = $"{RoleConsts.Superadmin},{RoleConsts.Admin}")]
        public async Task<IActionResult> Update(ArticleUpdateDto articleUpdateDto)
        {
            var map = mapper.Map<Article>(articleUpdateDto);
            var result = await validator.ValidateAsync(map);

            if (result.IsValid)
            {
                var title = await articleService.UpdateArticleAsync(articleUpdateDto);
                toast.AddSuccessToastMessage(Message.Article.Update(title), new ToastrOptions { Title = "Başarılı !" });
                return RedirectToAction("Index", "Article", new { Area = "Admin" });

            }
            else
            {
                result.AddToModelState(this.ModelState);
                
            }
            var categories = await categoryService.GetAllCategoriesNonDeleted();
            articleUpdateDto.Categories = categories;

            return View(articleUpdateDto);



        }

        [Authorize(Roles = $"{RoleConsts.Superadmin}")]
        public async Task<IActionResult> Delete(Guid articleId)
        {
            var title = await articleService.SafeDeleteArticleAsync(articleId);

            toast.AddSuccessToastMessage(Message.Article.Delete(title), new ToastrOptions { Title = "Başarılı !" });

            return RedirectToAction("Index","Article",new {Area ="Admin"});
        }

        [Authorize(Roles = $"{RoleConsts.Superadmin}")]
        public async Task<IActionResult> UndoDelete(Guid articleId)
        {
            var title = await articleService.UndoDeleteArticleAsync(articleId);

            toast.AddSuccessToastMessage(Message.Article.UndoDelete(title), new ToastrOptions { Title = "Başarılı !" });

            return RedirectToAction("Index", "Article", new { Area = "Admin" });
        }
    }
}
