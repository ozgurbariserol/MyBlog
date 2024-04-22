using MyBlog.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBlog.Entity.DTOs.Articles
{
    public class ArticleListDto
    {
        public List<Article> Articles { get; set; }
        public Guid? CategoryId { get; set; }
        public virtual int CurrentPage { get; set; } = 1;
        public virtual int PageSize { get; set; } = 3;
        public virtual int TotalCount { get; set; }
        public virtual int TotalPages => (int)Math.Ceiling(decimal.Divide(TotalCount, PageSize));
        public virtual bool ShowPrevious => CurrentPage > 1;
        public virtual bool ShowNext => CurrentPage < TotalPages;
        public virtual bool IsAscending { get; set; }
        public string? Keyword { get; set; }
    }
}
