using WebAPI.Infrastructure.Persistence.Configuration.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebAPI.Domain.Entity;

namespace WebAPI.Infrastructure.Persistence.Configuration
{
    public class CMSUserConfiguration : BaseEntityConfiguration<AppUser>
    {
        public override void Configure(EntityTypeBuilder<AppUser> builder)
        {
            base.Configure(builder);

            //设置表名
            builder.ToTable("app_user");

            builder.Property(x => x.Username).HasColumnName("username").HasColumnOrder(1);
            builder.Property(x => x.Password).HasColumnName("password").HasColumnOrder(2);
            builder.Property(x => x.UserSex).HasColumnName("user_sex").HasColumnOrder(3);
            builder.Property(x => x.UserBirthday).HasColumnName("user_birthday").HasColumnOrder(4);
            builder.Property(x => x.Email).HasColumnName("email").HasColumnOrder(5);
            builder.Property(x => x.UserAddress).HasColumnName("user_address").HasColumnOrder(6);
            builder.Property(x => x.Avatar).HasColumnName("avatar").HasColumnOrder(7);
            builder.Property(x => x.Nickname).HasColumnName("nickname").HasColumnOrder(8);
        }
    }
}