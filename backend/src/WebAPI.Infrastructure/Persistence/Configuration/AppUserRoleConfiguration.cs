using WebAPI.Infrastructure.Persistence.Configuration.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebAPI.Domain.Entity;

namespace WebAPI.Infrastructure.Persistence.Configuration
{
    public class AppUserRoleConfiguration : BaseEntityConfiguration<AppUserRole>
    {
        public override void Configure(EntityTypeBuilder<AppUserRole> builder)
        {
            base.Configure(builder);

            builder.ToTable("app_user_role");

            builder.Property(x => x.AppUserId).HasColumnName("app_user_id").HasMaxLength(2000).HasColumnOrder(1);
            builder.Property(x => x.AppRoleId).HasColumnName("app_role_id").HasMaxLength(2000).HasColumnOrder(2);
        }
    }
}