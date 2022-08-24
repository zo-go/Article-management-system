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
    public class AppCommentConfiguration : BaseEntityConfiguration<AppComment>
    {
        public override void Configure(EntityTypeBuilder<AppComment> builder)
        {
            base.Configure(builder);

            //设置表名
            builder.ToTable("app_comment");

            builder.Property(x => x.UserCommentId).HasColumnName("user_comment_id").HasColumnOrder(1);
            builder.Property(x => x.CommentContent).HasColumnName("comment_content").HasColumnOrder(2);
            builder.Property(x => x.AppUserId).HasColumnName("app_user_id").HasColumnOrder(3);
            builder.Property(x => x.AppArticleId).HasColumnName("app_Article_id").HasColumnOrder(4);

            // builder.HasOne(x=>x.AppArticle).WithMany(p=>p.AppComments).HasForeignKey(m=>m.AppArticleId);
            // builder.HasOne(x=>x.AppUsers).WithMany(p=>p.AppComments).HasForeignKey(m=>m.AppUserId);
        }
    }
}