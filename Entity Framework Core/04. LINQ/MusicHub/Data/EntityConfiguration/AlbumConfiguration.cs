using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MusicHub.Common;
using MusicHub.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicHub.Data.EntityConfiguration
{
    public class AlbumConfiguration : IEntityTypeConfiguration<Album>
    {
        public void Configure(EntityTypeBuilder<Album> entity)
        {
            entity.HasKey(a => a.Id);

            entity.Property(a => a.Name)
                .IsRequired()
                .HasMaxLength(EntityValidation.AlbumNameMaxLength);

            entity.Property(a => a.ReleaseDate)
                .HasColumnType(EntityValidation.AlbumReleaseDateColumnType);

            // Price property should NOT be mapped to DB column! (Calculated Property in Client)
            entity.Ignore(a => a.Price);

            entity.HasOne(a => a.Producer)
                .WithMany(p => p.Albums)
                .HasForeignKey(a => a.ProducerId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
