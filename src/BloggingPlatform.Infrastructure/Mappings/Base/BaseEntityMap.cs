using BloggingPlatform.Domain.Entities.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BloggingPlatform.Infrastructure.Data.Mappings.Base
{
    public class BaseEntityMap<T> : IEntityTypeConfiguration<T> where T : BaseEntity
    {
        public virtual void Configure(EntityTypeBuilder<T> builder)
        {
            builder.HasKey(x => x.Id);

            builder
                .Property(c => c.Created)
                .HasDefaultValueSql("GETUTCDATE()");

            builder.Property(c => c.CreatedBy);

            builder.Property(c => c.Updated);

            builder.Property(c => c.UpdatedBy);

            builder.HasQueryFilter(x => x.Deleted == null);
        }
    }
}
