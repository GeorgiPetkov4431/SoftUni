using GarageApp.Data.Models;
using GarageApp.Data.Models.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GarageApp.Data.Configuration
{
    public class CarConfiguration : IEntityTypeConfiguration<Car>
    {
        private static IEnumerable<Car> Cars { get; } = new List<Car>()
        {
            new Car
            {
                Id = 1,
                Make = "BMW",
                Model = "M3",
                ProductionMonth = 6,
                Year = 2022,
                Type = CarType.Coupe,
                IsAvailable = true,
                GarageId = 1
            },
            new Car
            {
                Id = 2,
                Make = "Toyota",
                Model = "Corolla",
                ProductionMonth = 3,
                Year = 2021,
                Type = CarType.Sedan,
                IsAvailable = true,
                GarageId = 1
            },
            new Car
            {
                Id = 3,
                Make = "Volkswagen",
                Model = "Golf",
                ProductionMonth = 9,
                Year = 2020,
                Type = CarType.Hatchback,
                IsAvailable = false,
                GarageId = 2
            },
            new Car
            {
                Id = 4,
                Make = "Mercedes-Benz",
                Model = "GLE",
                ProductionMonth = 1,
                Year = 2023,
                Type = CarType.SUV,
                IsAvailable = true,
                GarageId = 2
            },
            new Car
            {
                Id = 5,
                Make = "Ford",
                Model = "Mustang",
                ProductionMonth = 7,
                Year = 2019,
                Type = CarType.Coupe,
                IsAvailable = false,
                GarageId = 3
            },
            new Car
            {
                Id = 6,
                Make = "Audi",
                Model = "A4 Avant",
                ProductionMonth = null,
                Year = 2022,
                Type = CarType.Touring,
                IsAvailable = true,
                GarageId = 3
            }
        };

        public void Configure(EntityTypeBuilder<Car> builder)
        {
            builder.HasData(Cars);
        }
    }
}
