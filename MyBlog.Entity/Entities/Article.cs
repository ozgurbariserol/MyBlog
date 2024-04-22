using MyBlog.Core.Entities;

namespace MyBlog.Entity.Entities
{
    public class Article: EntityBase,IEntityBase
    {
        public Article()
        {
            
        }

        public Article(string title,string content,Guid userId,Guid categoryId,Guid? imageId,string createdBy)
        {
            Title = title;
            Content = content;
            CategoryId = categoryId;
            UserId = userId;
            ImageId = imageId;
            CreatedBy = createdBy;
        }


        public string Title { get; set; }
        public string Content { get; set; }
        public int ViewCount { get; set; } = 0;
        public Guid CategoryId { get; set; }
        public Category Category { get; set; }
        public Guid? ImageId { get; set; }
        public Image? Image{ get; set; } 

        public Guid UserId { get; set; }
        public AppUser User { get; set; }

        public ICollection<ArticleVisitor> ArticleVisitors { get; set; }
    }
}
