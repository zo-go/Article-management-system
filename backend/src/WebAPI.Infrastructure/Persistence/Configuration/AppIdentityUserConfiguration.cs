using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebAPI.Domain.Base;
using WebAPI.Domain.Entity;
using WebAPI.Infrastructure.Persistence.Configuration.Base;

namespace WebAPI.Infrastructure.Persistence.Configuration
{
    public class AppIdentityUserConfiguration : BaseEntityConfiguration<AppIdentityUser>
    {
        public override void Configure(EntityTypeBuilder<AppIdentityUser> builder)
        {
            base.Configure(builder);

            builder.ToTable("app_identity_user");

            builder.Property(x => x.UserId).HasColumnName("user_id").HasMaxLength(2000).HasColumnOrder(1);
            builder.Property(x => x.Username).HasColumnName("user_name").HasMaxLength(2000).HasColumnOrder(2);
            builder.Property(x => x.RefreshToken).HasColumnName("refresh_token").HasMaxLength(2000).HasColumnOrder(3);
            builder.Property(x => x.RefreshTokenExpiration).HasColumnName("refresh_token_expiration").HasColumnOrder(4);
        }
    }
}