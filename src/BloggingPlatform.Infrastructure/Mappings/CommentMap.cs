using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BloggingPlatform.Domain.Entities;
using BloggingPlatform.Infrastructure.Data.Mappings.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BloggingPlatform.Infrastructure.Data.Mappings
{
    public class CommentMap : BaseEntityMap<CommentEntity>
    {

        public override void Configure(EntityTypeBuilder<CommentEntity> builder)
        {
            base.Configure(builder);

            builder.Property(x => x.Title)
                .HasMaxLength(255);

            builder.Property(x => x.Content)
                .IsRequired()
                .HasMaxLength(4000);
        }
    }
}
