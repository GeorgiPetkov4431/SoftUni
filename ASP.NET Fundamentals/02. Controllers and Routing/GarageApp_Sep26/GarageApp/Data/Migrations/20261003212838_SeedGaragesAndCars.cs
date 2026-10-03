using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace GarageApp.Data.Migrations
{
    /// <inheritdoc />
    public partial class SeedGaragesAndCars : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Garages",
                columns: new[] { "Id", "Location", "Name" },
                values: new object[,]
                {
                    { 1, "Sofia, Bulgaria", "Central Garage" },
                    { 2, "Plovdiv, Bulgaria", "AutoCare Garage" },
                    { 3, "Varna, Bulgaria", "Elite Motors" }
                });

            migrationBuilder.InsertData(
                table: "Cars",
                columns: new[] { "Id", "CarType", "GarageId", "IsAvailable", "Make", "Model", "ProductionMonth", "Year" },
                values: new object[,]
                {
                    { 1, 2, 1, true, "BMW", "M3", 6, 2022 },
                    { 2, 1, 1, true, "Toyota", "Corolla", 3, 2021 },
                    { 3, 0, 2, false, "Volkswagen", "Golf", 9, 2020 },
                    { 4, 8, 2, true, "Mercedes-Benz", "GLE", 1, 2023 },
                    { 5, 2, 3, false, "Ford", "Mustang", 7, 2019 },
                    { 6, 5, 3, true, "Audi", "A4 Avant", null, 2022 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Garages",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Garages",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Garages",
                keyColumn: "Id",
                keyValue: 3);
        }
    }
}
