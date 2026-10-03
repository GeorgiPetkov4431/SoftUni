using GarageApp.Data.Models;
using GarageApp.Data.Models.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GarageApp.Data.Configuration
{
    public class GarageConfiguration : IEntityTypeConfiguration<Garage>
    {
        private static IEnumerable<Garage> Garages { get; } = new List<Garage>()
        {
            new Garage
            {
                Id = 1,
                Name = "Central Garage",
                Location = "Sofia, Bulgaria"
            },
            new Garage
            {
                Id = 2,
                Name = "AutoCare Garage",
                Location = "Plovdiv, Bulgaria"
            },
            new Garage
            {
                Id = 3,
                Name = "Elite Motors",
                Location = "Varna, Bulgaria"
            }
        };

        public void Configure(EntityTypeBuilder<Garage> builder)
        {
            builder.HasData(Garages);
        }
    }
}
