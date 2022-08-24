using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebAPI.Domain.Entity;
using WebAPI.Infrastructure.Persistence.Configuration.Base;

namespace WebAPI.Infrastructure.Persistence.Configuration
{
    public class AppFileInformationConfiguration : BaseEntityConfiguration<AppFileInformation>
    {
        public override void Configure(EntityTypeBuilder<AppFileInformation> builder)
        {
            base.Configure(builder);

            //设置表名
            builder.ToTable("app_file_information");

            builder.Property(x => x.OriginFileName).HasColumnName("origin_file_name").HasMaxLength(2000).HasColumnOrder(1);
            builder.Property(x => x.CurrentFileName).HasColumnName("current_file_name").HasMaxLength(2000).HasColumnOrder(2);
            builder.Property(x => x.RelativePath).HasColumnName("relative_path").HasMaxLength(2000).HasColumnOrder(3);
            builder.Property(x => x.FileSize).HasColumnName("file_size").HasColumnOrder(4);
            builder.Property(x => x.FileType).HasColumnName("file_type").HasMaxLength(2000).HasColumnOrder(5);
        }
    }
}