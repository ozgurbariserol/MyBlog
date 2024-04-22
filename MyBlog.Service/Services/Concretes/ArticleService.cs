using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using MyBlog.Data.UnitOfWorks;
using MyBlog.Entity.DTOs.Articles;
using MyBlog.Entity.DTOs.Categories;
using MyBlog.Entity.Entities;
using MyBlog.Service.Extensions;
using MyBlog.Service.Helpers.Images;
using MyBlog.Service.Services.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace MyBlog.Service.Services.Concretes
{
    public class ArticleService : IArticleService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly IImageHelper imageHelper;
        private readonly ClaimsPrincipal _user;

        public ArticleService(IUnitOfWork unitOfWork, IMapper mapper,IHttpContextAccessor httpContextAccessor,IImageHelper imageHelper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
            this.httpContextAccessor = httpContextAccessor;
            _user = httpContextAccessor.HttpContext.User;
            this.imageHelper = imageHelper;
        }

       


        public async Task CreateArticleAsync(ArticleAddDto articleAddDto)
        {
            var userId = _user.GetLoggedInUserId();
            var userEmail = _user.GetLoggedInUserEmail();

            if(articleAddDto.Photo !=null)
            {

            var imageUpload = await imageHelper.Upload(articleAddDto.Title,articleAddDto.Photo,Entity.Enums.ImageType.Post);
            Image image = new(imageUpload.FullName,articleAddDto.Photo.ContentType,userEmail);
            await unitOfWork.GetRepository<Image>().AddAsync(image);

            var article = new Article
            {
                Title = articleAddDto.Title,
                Content = articleAddDto.Content,
                CategoryId = articleAddDto.CategoryId,
                UserId = userId,
                CreatedBy = userEmail,  
                ImageId = image.Id,
            };
            await unitOfWork.GetRepository<Article>().AddAsync(article);
            await unitOfWork.SaveAsync();
            }
            else
            {
                var article = new Article
                {
                    Title = articleAddDto.Title,
                    Content = articleAddDto.Content,
                    CategoryId = articleAddDto.CategoryId,
                    UserId = userId,
                    CreatedBy = userEmail,
                };
                await unitOfWork.GetRepository<Article>().AddAsync(article);
            await unitOfWork.SaveAsync();

            }
        }

        public async Task<List<ArticleDto>> GetAllArticlesWithCategoryNonDeletedAsync()
        {     
           var articles = await unitOfWork.GetRepository<Article>().GetAllAsync(x=>!x.IsDeleted,x => x.Category);
           var map = mapper.Map<List<ArticleDto>>(articles);
           return map;
        }
        public async Task<ArticleDto> GetArticleWithCategoryNonDeletedAsync(Guid articleId)
        {
            var article = await unitOfWork.GetRepository<Article>().GetAsync(x => !x.IsDeleted && x.Id ==articleId, x => x.Category, i => i.Image,x=>x.User);
            var map = mapper.Map<ArticleDto>(article);
            return map;
        }

        public async Task<string> UpdateArticleAsync(ArticleUpdateDto articleUpdateDto)
        {
            var userEmail = _user.GetLoggedInUserEmail();
            var article = await unitOfWork.GetRepository<Article>().GetAsync(x => !x.IsDeleted && x.Id == articleUpdateDto.Id, x => x.Category, i => i.Image);

            if(articleUpdateDto.Photo != null)
            {

                if (article.Image != null)
                {
                    imageHelper.Delete(article.Image.FileName);
                    var imageUpload = await imageHelper.Upload(articleUpdateDto.Title, articleUpdateDto.Photo, Entity.Enums.ImageType.Post);
                    Image image = new(imageUpload.FullName, articleUpdateDto.Photo.ContentType, userEmail);
                    await unitOfWork.GetRepository<Image>().AddAsync(image);

                    article.ImageId = image.Id;
                }
                else
                {
                    var imageUpload = await imageHelper.Upload(articleUpdateDto.Title, articleUpdateDto.Photo, Entity.Enums.ImageType.Post);
                    Image image = new(imageUpload.FullName, articleUpdateDto.Photo.ContentType, userEmail);
                    await unitOfWork.GetRepository<Image>().AddAsync(image);

                    article.Image = image;
                    article.ImageId = image.Id;
                }
                
            }

            article.Title = articleUpdateDto.Title;
            article.Content = articleUpdateDto.Content;
            article.CategoryId = articleUpdateDto.CategoryId;
            article.ModifiedDate = DateTime.Now;
            article.ModifiedBy = _user.GetLoggedInUserEmail();

            await unitOfWork.GetRepository<Article>().UpdateAsync(article);
            await unitOfWork.SaveAsync();

            return article.Title;
        }

        public async Task<string> SafeDeleteArticleAsync(Guid articleId)
        {
            var article = await unitOfWork.GetRepository<Article>().GetyGuidAsync(articleId);
            article.IsDeleted = true;
            article.DeletedDate = DateTime.Now;
            article.DeletedBy = _user.GetLoggedInUserEmail();

            await unitOfWork.GetRepository<Article>().UpdateAsync(article);
            await unitOfWork.SaveAsync();

            return article.Title;
        }

        public async Task<List<ArticleDto>> GetAllArticlesWithCategoryDeletedAsync()
        {
            var deletedArticles = await unitOfWork.GetRepository<Article>().GetAllAsync(x => x.IsDeleted == true,x=>x.Category);
            var map = mapper.Map<List<ArticleDto>>(deletedArticles);
            return map;        
        }

        public async Task<string> UndoDeleteArticleAsync(Guid articleId)
        {
            var article = await unitOfWork.GetRepository<Article>().GetyGuidAsync(articleId);
            article.IsDeleted = false;
            article.DeletedDate = null;
            article.DeletedBy = null;

            await unitOfWork.GetRepository<Article>().UpdateAsync(article);
            await unitOfWork.SaveAsync();

            return article.Title;

        }
        public async Task<ArticleListDto> GetAllByPagingAsync(Guid? categoryId, int currentPage = 1, int pageSize = 3, bool isAscending = false)
        {
            pageSize = pageSize > 20 ? 20 : pageSize;

            var articles = categoryId == null ?
                await unitOfWork.GetRepository<Article>().GetAllAsync(x => !x.IsDeleted, x => x.Category, i => i.Image, u => u.User)
                : await unitOfWork.GetRepository<Article>().GetAllAsync(a => a.CategoryId == categoryId && !a.IsDeleted, x => x.Category, i => i.Image, u => u.User);

            var sortedArticles = isAscending
                ? articles.OrderBy(x => x.CreatedDate).Skip((currentPage - 1) * pageSize).Take(pageSize).ToList()
                : articles.OrderByDescending(x => x.CreatedDate).Skip((currentPage - 1) * pageSize).Take(pageSize).ToList();

            return new ArticleListDto
            {
                Articles = sortedArticles,
                CategoryId = categoryId == null ? null : categoryId.Value,
                CurrentPage = currentPage,
                PageSize = pageSize,
                TotalCount = articles.Count,
                IsAscending = isAscending,
            };
        }
        public async Task<ArticleListDto> SearchAsync(string keyword, int currentPage = 1, int pageSize = 3, bool isAscending = false)
        {
            pageSize = pageSize > 20 ? 20 : pageSize;
            var articles = await unitOfWork.GetRepository<Article>().GetAllAsync(a => !a.IsDeleted && (a.Title.Contains(keyword) || a.Content.Contains(keyword) || a.Category.Name.Contains(keyword)),
            a => a.Category, i => i.Image, u => u.User);

            var sortedArticles = isAscending
                ? articles.OrderBy(a => a.CreatedDate).Skip((currentPage - 1) * pageSize).Take(pageSize).ToList()
                : articles.OrderByDescending(a => a.CreatedDate).Skip((currentPage - 1) * pageSize).Take(pageSize).ToList();
            return new ArticleListDto
            {
                Articles = sortedArticles,
                CurrentPage = currentPage,
                PageSize = pageSize,
                TotalCount = articles.Count,
                IsAscending = isAscending,
                Keyword = keyword,
            };
        }

        public async Task<List<ArticleDto>> GetAllArticlesLastThree()
        {
            var article = await GetAllArticlesWithCategoryNonDeletedAsync();
            DateTime currentTime = DateTime.Now;
            var map = mapper.Map<List<ArticleDto>>(article);
            var data = map.OrderByDescending(x => x.CreatedDate).Take(3).ToList();
            return data;
        }

    }
}
