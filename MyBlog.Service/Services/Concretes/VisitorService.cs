using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using MyBlog.Data.UnitOfWorks;
using MyBlog.Entity.DTOs.Articles;
using MyBlog.Entity.Entities;
using MyBlog.Service.Services.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBlog.Service.Services.Concretes
{
    public class VisitorService :IVisitorService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IArticleService articleService;
        private readonly IHttpContextAccessor httpContextAccessor;

        public VisitorService(IUnitOfWork unitOfWork,IArticleService articleService,IHttpContextAccessor httpContextAccessor)
        {
            this.unitOfWork = unitOfWork;
            this.articleService = articleService;
            this.httpContextAccessor = httpContextAccessor;
        }

        public  Task<String> GetIpAdress(Guid id)
        {
            
            return  Task.FromResult(httpContextAccessor.HttpContext.Connection.RemoteIpAddress.MapToIPv4().ToString());
        }

        public async Task<List<ArticleVisitor>> getArticleVisitors(Guid id)
        {
            var articleVisitors = await unitOfWork.GetRepository<ArticleVisitor>().GetAllAsync(null, x => x.Visitor, x => x.Article);
            return articleVisitors;
        }

        public async Task<ArticleDto> UpdateVisitor(Guid id)
        {
            var ipAdress =  GetIpAdress(id).ToString();
            var articleVisitors = await getArticleVisitors(id);

            var article = await unitOfWork.GetRepository<Article>().GetAsync(x=>x.Id== id);

            var result = await articleService.GetArticleWithCategoryNonDeletedAsync(id);

            var visitor = await unitOfWork.GetRepository<Visitor>().GetAsync(x => x.IpAddress == ipAdress);

            var addArticleVisitors = new ArticleVisitor(article.Id, visitor.Id);

            if(articleVisitors.Any(x=>x.VisitorId == addArticleVisitors.VisitorId && x.ArticleId == addArticleVisitors.ArticleId)){
                return result;
            }
            else
            {
                await unitOfWork.GetRepository<ArticleVisitor>().AddAsync(addArticleVisitors);
                article.ViewCount += 1;
                await unitOfWork.GetRepository<Article>().UpdateAsync(article);
                await unitOfWork.SaveAsync();
                
            }
            return result;
        }
    }
}
