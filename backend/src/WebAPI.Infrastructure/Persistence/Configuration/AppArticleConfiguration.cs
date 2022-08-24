using WebAPI.Infrastructure.Persistence.Configuration.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebAPI.Domain.Entity;

namespace WebAPI.Infrastructure.Persistence.Configuration
{
    public class AppArticleConfiguration : BaseEntityConfiguration<AppArticle>
    {
        public override void Configure(EntityTypeBuilder<AppArticle> builder)
        {
            base.Configure(builder);

            //设置表名
            builder.ToTable("app_article");

            builder.Property(x => x.ArticleTitle).HasColumnName("article_title").HasColumnOrder(1);
            builder.Property(x => x.ArticleSecondTitle).HasColumnName("article_second_title").HasColumnOrder(2);
            builder.Property(x => x.ArticleContent).HasColumnName("article_content").HasColumnOrder(3);
            builder.Property(x => x.Document).HasColumnName("document").HasColumnOrder(4);
            builder.Property(x => x.AppArticleClassId).HasColumnName("app_article_class_id").HasColumnOrder(5);
            builder.Property(x=>x.AppUserId).HasColumnName("app_user_id").HasColumnOrder(6);
            builder.Property(x=>x.ArticlePraise).HasColumnName("article_praise").HasColumnOrder(7);

            // builder.HasOne(x=>x.AppArticleClasses).WithMany(p=>p.AppArticles).HasForeignKey(m=>m.AppArticleClassId);
            // builder.HasOne(x=>x.AppUsers).WithMany(p=>p.AppArticles).HasForeignKey(m=>m.AppUserId);
        }
    }
}