using BloggingPlatform.Domain.Entities;
using BloggingPlatform.Infrastructure.Data.Mappings.Base;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BloggingPlatform.Infrastructure.Data.Mappings
{
    public class BlogPostMap : BaseEntityMap<BlogPostEntity>
    {

        public override void Configure(EntityTypeBuilder<BlogPostEntity> builder)
        {
            base.Configure(builder);

            builder.Property(x => x.Title)
                .IsRequired()
                .HasMaxLength(255);

            builder.Property(x => x.Content)
                .IsRequired()
                .IsUnicode()
                .HasMaxLength(4000);

            builder.HasIndex(x => x.Title)
                .IsUnique();
        }
    }
}
