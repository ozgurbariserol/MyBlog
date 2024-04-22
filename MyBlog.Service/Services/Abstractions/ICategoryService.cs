using MyBlog.Entity.DTOs.Categories;
using MyBlog.Entity.Entities;

namespace MyBlog.Service.Services.Abstractions
{
    public interface ICategoryService
    {
        Task<List<CategoryDto>> GetAllCategoriesNonDeleted();
        Task<List<CategoryDto>> GetAllCategoriesNonDeletedForViewSorting();
        Task<List<CategoryDto>> GetAllCategoriesDeleted();
        Task CreateCategoryAsync(CategoryAddDto categoryAddDto);
        Task<Category> GetCategoryByGuidAsync(Guid Id);
        Task<string> UpdateCategoryAsync(CategoryUpdateDto categoryUpdateDto);
        Task<string> SafeDeleteCategoryAsync(Guid categoryId);
        Task<string> UndoDeleteCategoryAsync(Guid categoryId);
    }
}
