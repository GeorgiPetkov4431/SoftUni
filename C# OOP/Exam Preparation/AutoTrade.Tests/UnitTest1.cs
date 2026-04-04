using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using NUnit.Framework;

namespace AutoTrade.Tests
{
    [TestFixture]
    public class DealerShopTests
    {

        [Test]
        public void ConstructorShouldInitializeWithValidCapacity()
        {
            DealerShop shop = new DealerShop(10);
            int expectedCapacity = 10;

            Assert.AreEqual(expectedCapacity, shop.Capacity);
            Assert.IsNotNull(shop.Vehicles);
            Assert.IsEmpty(shop.Vehicles);
        }

        [TestCase(0)]
        [TestCase(-1)]
        [TestCase(-12)]
        public void ConstructorShouldThrowExceptionWhenCapacityIsZeroOrLess(int capacity)
        {
            ArgumentException ex = Assert.Throws<ArgumentException>(() => new DealerShop(capacity));
            Assert.AreEqual("Capacity must be a positive value.", ex.Message);
        }

        [Test]
        public void AddVehicleShouldAddVehicleWhenInventoryIsNotFull()
        {
            DealerShop shop = new DealerShop(3);

            Vehicle vehicle = new Vehicle("Toyota", "Camry", 2020);

            string result = shop.AddVehicle(vehicle);

            Assert.AreEqual(1, shop.Vehicles.Count);
            Assert.AreEqual($"Added {vehicle.ToString()}", result);
        }

        [Test]
        public void AddVehicleShouldThrowExceptionWhenInventoryIsFull()
        {
            DealerShop shop = new DealerShop(2);
            shop.AddVehicle(new Vehicle("Toyota", "Camry", 2020));
            shop.AddVehicle(new Vehicle("Honda", "Civic", 2019));
            InvalidOperationException ex = Assert.Throws<InvalidOperationException>(() => shop.AddVehicle(new Vehicle("Generic", "Car", 2026)));
            Assert.AreEqual("Inventory is full", ex.Message);
        }

        [Test]
        public void SellVehicleShouldRemoveVehicleWhenItExists()
        {
            DealerShop shop = new DealerShop(4);
            Vehicle vehicle = new Vehicle("Toyota", "Camry", 2020);

            shop.AddVehicle(vehicle);
            bool result = shop.SellVehicle(vehicle);

            Assert.IsTrue(result);
            Assert.IsEmpty(shop.Vehicles);
            Assert.AreEqual(0, shop.Vehicles.Count);
        }

        [Test]
        public void SellVehicleShouldReturnFalseWhenVehicleDoesNotExist()
        {
            DealerShop shop = new DealerShop(4);
            Vehicle vehicle = new Vehicle("Toyota", "Camry", 2020);

            bool result = shop.SellVehicle(vehicle);

            Assert.IsFalse(result);
            Assert.AreEqual(0, shop.Vehicles.Count);
        }

        [Test]
        public void InventoryReportShouldPrintCorrectly()
        {
            DealerShop shop = new DealerShop(2);
            Vehicle vehicle1 = new Vehicle("Toyota", "Camry", 2020);

            shop.AddVehicle(vehicle1);

            StringBuilder sb = new StringBuilder();

            sb.AppendLine("Inventory Report");
            sb.AppendLine($"Capacity: 2");
            sb.AppendLine($"Vehicles: 1");
            sb.Append(vehicle1.ToString());

            Assert.AreEqual(sb.ToString(), shop.InventoryReport());
        }
    }
}
