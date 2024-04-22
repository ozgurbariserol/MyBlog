using Microsoft.AspNetCore.Mvc;
using MyBlog.Data.UnitOfWorks;
using MyBlog.Entity.DTOs.Users;
using MyBlog.Entity.Entities;
using MyBlog.Service.Services.Abstractions;
using MyBlog.Web.Models;
using System.Diagnostics;

namespace MyBlog.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IArticleService articleService;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly IVisitorService visitorService;
        private readonly IUnitOfWork unitOfWork;
        private readonly IUserService userService;

        public HomeController(ILogger<HomeController> logger,IArticleService articleService,IHttpContextAccessor httpContextAccessor,IVisitorService visitorService,IUnitOfWork unitOfWork,IUserService userService)
        {
            _logger = logger;
            this.articleService = articleService;
            this.httpContextAccessor = httpContextAccessor;
            this.visitorService = visitorService;
            this.unitOfWork = unitOfWork;
            this.userService = userService;
        }
        [HttpGet]
        public async Task<IActionResult> Index(Guid? categoryId,int currentPage = 1,int pageSize = 3,bool isAscending = false)
        {
            var articles = await articleService.GetAllByPagingAsync(categoryId,currentPage,pageSize,isAscending);
            return View(articles);
        }
        [HttpGet]
        public async Task<IActionResult> Search(string keyword, int currentPage = 1, int pageSize = 3, bool isAscending = false)
        {
            var articles = await articleService.SearchAsync(keyword, currentPage, pageSize, isAscending);
            return View(articles);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpGet]
        public async Task<IActionResult> Detail(Guid id)
        {
            var ipAdress =  httpContextAccessor.HttpContext.Connection.RemoteIpAddress.MapToIPv4().ToString();
            var articleVisitors = await unitOfWork.GetRepository<ArticleVisitor>().GetAllAsync(null, x => x.Visitor, x => x.Article);

            var article = await unitOfWork.GetRepository<Article>().GetAsync(x => x.Id == id);

            var result = await articleService.GetArticleWithCategoryNonDeletedAsync(id);

            var visitor = await unitOfWork.GetRepository<Visitor>().GetAsync(x => x.IpAddress == ipAdress);

            var addArticleVisitors = new ArticleVisitor(article.Id, visitor.Id);

            if (articleVisitors.Any(x => x.VisitorId == addArticleVisitors.VisitorId && x.ArticleId == addArticleVisitors.ArticleId))
            {
                return View(result);
            }
            else
            {
                await unitOfWork.GetRepository<ArticleVisitor>().AddAsync(addArticleVisitors);
                article.ViewCount += 1;
                await unitOfWork.GetRepository<Article>().UpdateAsync(article);
                await unitOfWork.SaveAsync();
            }
            return View(result);


        }
        [HttpGet]
        public async Task<IActionResult> Media()
        {
            var user = await userService.GetAllUsersWithRoleAsync();
            return View(user);
        }
    }
}
