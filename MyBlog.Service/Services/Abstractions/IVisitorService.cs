using MyBlog.Entity.DTOs.Articles;
using MyBlog.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBlog.Service.Services.Abstractions
{
    public interface IVisitorService
    {
        Task<String> GetIpAdress(Guid id);
        Task<List<ArticleVisitor>> getArticleVisitors(Guid id);
        Task<ArticleDto> UpdateVisitor(Guid id);
    }
}
