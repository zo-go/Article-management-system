// using WebAPI.Infrastructure.Persistence.Configuration.Base;
// using Microsoft.EntityFrameworkCore;
// using Microsoft.EntityFrameworkCore.Metadata.Builders;
// using WebAPI.Domain.Entity;

// namespace WebAPI.Infrastructure.Persistence.Configuration
// {
//     public class AppGiveALikeConfiguration : BaseEntityConfiguration<AppGiveALike>
//     {
//         public override void Configure(EntityTypeBuilder<AppGiveALike> builder)
//         {
//             base.Configure(builder);

//             //设置表名
//             builder.ToTable("app_give_a_like");

//             builder.Property(x => x.ArticleId).HasColumnName("article_id").HasColumnOrder(1);
//             builder.Property(x => x.UserId).HasColumnName("user_id").HasColumnOrder(2);
//             builder.Property(x => x.IsGiveALike).HasColumnName("is_give_a_like").HasColumnOrder(3);

//             // builder.HasOne(x=>x.AppArticleClasses).WithMany(p=>p.AppArticles).HasForeignKey(m=>m.AppArticleClassId);
//             // builder.HasOne(x=>x.AppUsers).WithMany(p=>p.AppArticles).HasForeignKey(m=>m.AppUserId);
//         }
//     }
// }