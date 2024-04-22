using FluentValidation;
using MyBlog.Entity.Entities;

namespace MyBlog.Service.FluentValidations
{
    public class ArticleValidator :AbstractValidator<Article>
    {
        public ArticleValidator() 
        {
            RuleFor(x => x.Title).NotEmpty()
                .NotNull()
                .MinimumLength(3)
                .MaximumLength(150)
                .WithName("Başlık");

            RuleFor(x => x.Content).NotEmpty()
                .NotNull()
                .MinimumLength(3)
                .MaximumLength(5000)
                .WithName("İçerik");

        }
    }
}
