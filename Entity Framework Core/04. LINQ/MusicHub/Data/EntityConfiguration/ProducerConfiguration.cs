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
    public class ProducerConfiguration : IEntityTypeConfiguration<Producer>
    {
        public void Configure(EntityTypeBuilder<Producer> entity)
        {
            entity.HasKey(p => p.Id);

            entity.Property(p => p.Name)
                .IsRequired()
                .HasMaxLength(EntityValidation.ProducerNameMaxLength);

            entity.Property(p => p.Pseudonym)
                .IsRequired(false)
                .HasMaxLength(EntityValidation.ProducerPseudonymMaxLength);

            entity.Property(p => p.PhoneNumber)
                .IsRequired(false)
                .HasMaxLength(EntityValidation.ProducerPhoneNumberMaxLength);
        }
    }
}
