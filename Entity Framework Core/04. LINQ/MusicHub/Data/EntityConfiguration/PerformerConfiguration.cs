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
    public class PerformerConfiguration : IEntityTypeConfiguration<Performer>
    {
        public void Configure(EntityTypeBuilder<Performer> entity)
        {
            entity.HasKey(p => p.Id);

            entity.Property(p => p.FirstName)
                .IsRequired()
                .HasMaxLength(EntityValidation.PerformerFirstNameMaxLength);

            entity.Property(p => p.LastName)
                .IsRequired()
                .HasMaxLength(EntityValidation.PerformerLastNameMaxLength);

            entity.Property(p => p.NetWorth)
                .HasColumnType(EntityValidation.PerformerNetWorthColumnType);
        }
    }
}
