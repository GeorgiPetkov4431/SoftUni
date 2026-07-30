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
    public class WriterConfiguration : IEntityTypeConfiguration<Writer>
    {
        public void Configure(EntityTypeBuilder<Writer> entity)
        {
            entity.HasKey(w => w.Id);

            entity.Property(w => w.Name)
                .IsRequired()
                .HasMaxLength(EntityValidation.WriterNameMaxLength);

            entity.Property(w => w.Pseudonym)
                .IsRequired(false)
                .HasMaxLength(EntityValidation.WriterPseudonymMaxLength);
        }
    }
}
