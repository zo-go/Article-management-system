using WebAPI.Infrastructure.Persistence.Configuration.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebAPI.Domain.Entity;

namespace WebAPI.Infrastructure.Persistence.Configuration
{
    public class AppRoleConfiguration : BaseEntityConfiguration<AppRole>
    {
        public override void Configure(EntityTypeBuilder<AppRole> builder)
        {
            base.Configure(builder);

            builder.ToTable("app_role");

            builder.Property(x => x.RoleName).HasColumnName("role_name").HasMaxLength(2000).HasColumnOrder(1);
        }
    }
}