﻿using AutoMapper;
using Microsoft.AspNetCore.Http;
using MyBlog.Data.UnitOfWorks;
using MyBlog.Entity.DTOs.Articles;
using MyBlog.Entity.DTOs.Categories;
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
    public class CategoryService : ICategoryService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly ClaimsPrincipal _user;


        public CategoryService(IUnitOfWork unitOfWork, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
            this.httpContextAccessor = httpContextAccessor;
            _user = httpContextAccessor.HttpContext.User;
        }

        public async Task CreateCategoryAsync(CategoryAddDto categoryAddDto)
        {
            var userEmail = _user.GetLoggedInUserEmail();

            Category category = new Category(categoryAddDto.Name, userEmail);
            await unitOfWork.GetRepository<Category>().AddAsync(category);
            await unitOfWork.SaveAsync();

        }

        public async Task<List<CategoryDto>> GetAllCategoriesNonDeleted()
        {
            var categories = await unitOfWork.GetRepository<Category>().GetAllAsync(x => !x.IsDeleted);
            var map = mapper.Map<List<CategoryDto>>(categories);
            return map;
        }

        public async Task<List<CategoryDto>> GetAllCategoriesNonDeletedForViewSorting()
        {
            var categories = await unitOfWork.GetRepository<Category>().GetAllAsync(x => !x.IsDeleted);
            var map = mapper.Map<List<CategoryDto>>(categories);
            return map.Take(24).ToList();
        }
        public async Task<Category> GetCategoryByGuidAsync(Guid id)
        {
            var category = await unitOfWork.GetRepository<Category>().GetyGuidAsync(id);
            return category;
        }


        public async Task<string> UpdateCategoryAsync(CategoryUpdateDto categoryUpdateDto)
        {
            var userEmail = _user.GetLoggedInUserEmail();
            var category = await unitOfWork.GetRepository<Category>().GetAsync(x => !x.IsDeleted && x.Id == categoryUpdateDto.Id);

            category.Name = categoryUpdateDto.Name;
            category.ModifiedBy = userEmail;
            category.ModifiedDate = DateTime.Now;


            await unitOfWork.GetRepository<Category>().UpdateAsync(category);
            await unitOfWork.SaveAsync();

            return category.Name;
        }

        public async Task<string> SafeDeleteCategoryAsync(Guid categoryId)
        {
            var category = await unitOfWork.GetRepository<Category>().GetyGuidAsync(categoryId);
            category.IsDeleted = true;
            category.DeletedDate = DateTime.Now;
            category.DeletedBy = _user.GetLoggedInUserEmail();

            await unitOfWork.GetRepository<Category>().UpdateAsync(category);
            await unitOfWork.SaveAsync();

            return category.Name;
        }

        public async Task<List<CategoryDto>> GetAllCategoriesDeleted()
        {
            var categories = await unitOfWork.GetRepository<Category>().GetAllAsync(x => x.IsDeleted);
            var map = mapper.Map<List<CategoryDto>>(categories);
            return map;
        }

        public async Task<string> UndoDeleteCategoryAsync(Guid categoryId)
        {
            var category = await unitOfWork.GetRepository<Category>().GetyGuidAsync(categoryId);
            category.IsDeleted = false;
            category.DeletedDate = null;
            category.DeletedBy = null;

            await unitOfWork.GetRepository<Category>().UpdateAsync(category);
            await unitOfWork.SaveAsync();

            return category.Name;
        }

        
    }
}
