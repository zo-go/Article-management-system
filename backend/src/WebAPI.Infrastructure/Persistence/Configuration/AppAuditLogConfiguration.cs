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
    public class AppAuditLogConfiguration : BaseEntityConfiguration<AppAuditLog>
    {
        public override void Configure(EntityTypeBuilder<AppAuditLog> builder)
        {
            base.Configure(builder);

            //设置表名
            builder.ToTable("app_audit_log");

            builder.Property(x => x.Parameters).HasColumnName("parameters").HasColumnOrder(1);
            builder.Property(x => x.BrowserInfo).HasColumnName("browser_info").HasColumnOrder(2);
            builder.Property(x => x.ClientName).HasColumnName("client_name").HasColumnOrder(3);
            builder.Property(x => x.ClientIpAddress).HasColumnName("client_ip_address").HasColumnOrder(4);
            builder.Property(x => x.ExecutionDuration).HasColumnName("execution_duration").HasColumnOrder(5);
            builder.Property(x => x.ExecutionTime).HasColumnName("execution_time").HasColumnOrder(6);
            builder.Property(x => x.ReturnValue).HasColumnName("return_value").HasColumnOrder(7);
            builder.Property(x => x.Exception).HasColumnName("exception").HasColumnOrder(8);
            builder.Property(x => x.ServiceName).HasColumnName("service_name").HasColumnOrder(9);
            builder.Property(x => x.UserInfo).HasColumnName("user_info").HasColumnOrder(10);
            builder.Property(x => x.CustomData).HasColumnName("custom_data").HasColumnOrder(11);
        }
    }
}