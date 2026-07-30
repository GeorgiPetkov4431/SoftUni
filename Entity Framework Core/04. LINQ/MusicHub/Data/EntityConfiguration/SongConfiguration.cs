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
    public class SongConfiguration : IEntityTypeConfiguration<Song>
    {
        public void Configure(EntityTypeBuilder<Song> entity)
        {
            entity.HasKey(s => s.Id);

            entity.Property(s => s.Name)
                .IsRequired()
                .HasMaxLength(EntityValidation.SongNameMaxLength);

            entity.Property(s => s.CreatedOn)
                .HasColumnType(EntityValidation.SongCreatedOnColumnType);

            entity.Property(s => s.Price)
                .HasColumnType(EntityValidation.SongPriceColumnType);

            entity.HasOne(s => s.Album)
                .WithMany(a => a.Songs)
                .HasForeignKey(s => s.AlbumId)
                .OnDelete(DeleteBehavior.SetNull);

            entity.HasOne(s => s.Writer)
                .WithMany(w => w.Songs)
                .HasForeignKey(s => s.WriterId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
