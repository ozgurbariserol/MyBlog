using Microsoft.AspNetCore.Mvc;
using MyBlog.Service.Services.Abstractions;
using MyBlog.Service.Services.Concretes;

namespace MyBlog.Web.ViewComponents
{
    public class HomeRecentPostsViewComponent : ViewComponent
    {
        private readonly IArticleService articleService;

        public HomeRecentPostsViewComponent(IArticleService articleService)
        {
            this.articleService = articleService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var articles = await articleService.GetAllArticlesLastThree();
            return View(articles);
        }
    }
}
