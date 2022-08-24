using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebAPI.Domain.Entity;
using WebAPI.Infrastructure.Persistence.Configuration.Base;

namespace WebAPI.Infrastructure.Persistence.Configuration
{
    public class AppArticleClassConfiguration : BaseEntityConfiguration<AppArticleClass>
    {
        public override void Configure(EntityTypeBuilder<AppArticleClass> builder)
        {
            base.Configure(builder);

            //设置表名
            builder.ToTable("app_article_class");

            builder.Property(x => x.AppArticleClassName).HasColumnName("app_article_classname").HasColumnOrder(1);
        }
    }
}