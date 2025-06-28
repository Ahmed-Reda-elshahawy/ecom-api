using Ecom.Core.Entities.Product;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecom.Infrastructure.Data.Configuration
{
    class PhotoConfig : IEntityTypeConfiguration<Photo>
    {
        public void Configure(EntityTypeBuilder<Photo> builder)
        {
            builder.HasKey(p => p.Id);
            builder.Property(p => p.ImageName)
                .IsRequired()
                .HasMaxLength(255);
            builder.HasData(
                new Photo
                {
                    Id = 1,
                    ImageName = "sample-photo.jpg",
                    ProductId = 1
                }
            );
        }
    }
}
