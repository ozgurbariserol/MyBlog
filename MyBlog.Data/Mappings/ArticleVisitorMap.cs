﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyBlog.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBlog.Data.Mappings
{
    public class ArticleVisitorMap : IEntityTypeConfiguration<ArticleVisitor>
    {
        public void Configure(EntityTypeBuilder<ArticleVisitor> builder)
        {
            builder.HasKey(x=> new {x.ArticleId,x.VisitorId});
        }
    }
}
