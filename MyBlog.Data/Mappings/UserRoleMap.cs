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
    public class UserRoleMap : IEntityTypeConfiguration<AppUserRole>
    {
        public void Configure(EntityTypeBuilder<AppUserRole> builder)
        {
            // Primary key
            builder.HasKey(r => new { r.UserId, r.RoleId });

            // Maps to the AspNetUserRoles table
            builder.ToTable("AspNetUserRoles");

            builder.HasData(new AppUserRole
            {
                UserId = Guid.Parse("98F41B72-899C-475D-AF3E-6085304A0500"),
                RoleId = Guid.Parse("55E11478-C3C0-4078-AE42-267706D92780"),

            },
            new AppUserRole
            {
                UserId = Guid.Parse("C6C614F9-2A8C-42D0-8620-5EB4E1CF7CD4"),
                RoleId = Guid.Parse("FBF969A5-BDDF-42F8-AB58-5FB420346F67"),
            });
        }
    }
}
