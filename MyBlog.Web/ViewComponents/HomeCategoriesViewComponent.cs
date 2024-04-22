using Microsoft.AspNetCore.Mvc;
using MyBlog.Service.Services.Abstractions;

namespace MyBlog.Web.ViewComponents
{
    public class HomeCategoriesViewComponent : ViewComponent
    {
        private readonly ICategoryService categoryService;

        public HomeCategoriesViewComponent(ICategoryService categoryService)
        {
            this.categoryService = categoryService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var categories =  await categoryService.GetAllCategoriesNonDeletedForViewSorting();
            return View(categories);
        }
    }
}
