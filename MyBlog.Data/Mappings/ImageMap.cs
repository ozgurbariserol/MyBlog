using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyBlog.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBlog.Data.Mappings
{
    public class ImageMap : IEntityTypeConfiguration<Image>
    {
        public void Configure(EntityTypeBuilder<Image> builder)
        {
            builder.HasData(new Image
            {
                Id = Guid.Parse("9F6C6C6B-F940-4C39-99D6-873DC98AF56A"),
                FileName = "images/testimage",
                FileType = "jpg",
                CreatedBy = "Admin Test",
                CreatedDate = DateTime.Now,
                IsDeleted = false,
            },
            new Image
            {
                Id = Guid.Parse("125DA97B-3C5C-4C01-88DC-FB850E61C1F9"),
                FileName = "images/vsimage",
                FileType = "jpg",
                CreatedBy = "Admin Test",
                CreatedDate = DateTime.Now,
                IsDeleted = false,
            });
        }
    }
}
