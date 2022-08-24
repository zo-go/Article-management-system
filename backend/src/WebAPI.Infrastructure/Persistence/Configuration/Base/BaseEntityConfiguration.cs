using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebAPI.Domain.Base;

namespace WebAPI.Infrastructure.Persistence.Configuration.Base
{
    public class BaseEntityConfiguration<T> : IEntityTypeConfiguration<T> where T : BaseEntity
    {
        public virtual void Configure(EntityTypeBuilder<T> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).HasColumnName("id").HasColumnOrder(0);
            builder.Property(x => x.CreatedAt).HasColumnName("created_at").HasColumnOrder(101);
            builder.Property(x => x.UpdatedAt).HasColumnName("updated_at").HasColumnOrder(102);
            builder.Property(x => x.CreatedBy).HasColumnName("created_by").HasColumnOrder(103);
            builder.Property(x => x.UpdatedBy).HasColumnName("updated_by").HasColumnOrder(104);
            builder.Property(x => x.IsActived).HasColumnName("is_actived").HasColumnOrder(105);
            builder.Property(x => x.IsDeleted).HasColumnName("is_deleted").HasColumnOrder(106);
            builder.Property(x => x.DisplayOrder).HasColumnName("display_order").HasColumnOrder(107);
            builder.Property(t => t.Remarks).HasColumnName("remarks").HasMaxLength(800).HasColumnOrder(108);
        }
    }
}