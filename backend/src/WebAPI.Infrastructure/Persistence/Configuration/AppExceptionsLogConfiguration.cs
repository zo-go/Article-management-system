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
    public class AppExceptionsLogConfiguration : BaseEntityConfiguration<AppExceptionLog>
    {
        public override void Configure(EntityTypeBuilder<AppExceptionLog> builder)
        {
            base.Configure(builder);

            //设置表名
            builder.ToTable("app_exception_log");

            builder.Property(x => x.ShortMessage).HasColumnName("short_message").HasColumnOrder(1);
            builder.Property(x => x.FullMessage).HasColumnName("full_message").HasColumnOrder(2);
        }
    }
}