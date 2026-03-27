namespace CarManager.Tests
{
    using NUnit.Framework;
    using System;

    [TestFixture]
    public class CarManagerTests
    {
        Car car;

        [SetUp]
        public void Setup()
        {
            car = new Car("Mazda", "C5", 10.0, 100.0);
        }

        [Test]
        public void CarPropertiesShouldBeSetCorrectly()
        {
            // Arrange
            string expectedMake = "Mazda";
            string expectedModel = "C5";
            double expectedFuelConsumption = 10.0;
            double expectedFuelCapacity = 100.0;

            // Act
            // Assert
            Assert.AreEqual(expectedMake, car.Make);
            Assert.AreEqual(expectedModel, car.Model);
            Assert.AreEqual(expectedFuelConsumption, car.FuelConsumption);
            Assert.AreEqual(expectedFuelCapacity, car.FuelCapacity);
        }

        [Test]
        public void CarShouldBeCreatedWithEmptyFuelAmount()
        {
            // Arrange
            double expectedFuelAmount = 0.0;

            Assert.AreEqual(expectedFuelAmount, car.FuelAmount);
        }

        [TestCase(null)]
        [TestCase("")]
        public void CarMakeShouldThrowExceptionWhenValueIsNullOrEmpty(string make)
        {
            ArgumentException ex = Assert.Throws<ArgumentException>(
                () => new Car(make, "C5", 10, 100));
            Assert.AreEqual("Make cannot be null or empty!", ex.Message);
        }

        [TestCase(null)]
        [TestCase("")]
        public void CarModelShouldThrowExceptionWhenValueIsNullOrEmpty(string model)
        {
            ArgumentException ex = Assert.Throws<ArgumentException>(
                () => new Car("Mazda", model, 10, 100));
            Assert.AreEqual("Model cannot be null or empty!", ex.Message);
        }

        [TestCase(0)]
        [TestCase(-5)]
        public void CarFuelConsumptionShouldThrowExceptionWhenValueIsZeroOrNegative(double fuelConsumption)
        {
            ArgumentException ex = Assert.Throws<ArgumentException>(
                () => new Car("Mazda", "C5", fuelConsumption, 100));
            Assert.AreEqual("Fuel consumption cannot be zero or negative!", ex.Message);
        }

        [TestCase(0)]
        [TestCase(-5)]
        public void CarFuelCapacityShouldThrowExceptionWhenValueIsZeroOrNegative(double fuelCapacity)
        {
            ArgumentException ex = Assert.Throws<ArgumentException>(
                () => new Car("Mazda", "C5", 10, fuelCapacity));
            Assert.AreEqual("Fuel capacity cannot be zero or negative!", ex.Message);
        }

        [TestCase(0)]
        [TestCase(-5)]
        public void RefuelMethodShouldThrowExceptionWhenValueIsZeroOrNegative(double fuelToRefuel)
        {
            ArgumentException ex = Assert.Throws<ArgumentException>(
                () => car.Refuel(fuelToRefuel));
            Assert.AreEqual("Fuel amount cannot be zero or negative!", ex.Message);
        }

        [Test]
        public void RefuelMethodShouldRefuelCarCorrectly()
        {
            // Arrange
            double fuelToRefuel = 100.0;

            // Act
            car.Refuel(50);
            car.Refuel(70);

            // Assert
            Assert.AreEqual(fuelToRefuel, car.FuelAmount);
        }

        [Test]
        public void DriveMethodShouldThrowExceptionWhenFuelIsNotEnough()
        {
            InvalidOperationException ex = Assert.Throws<InvalidOperationException>(
                () => car.Drive(100));
            Assert.AreEqual("You don't have enough fuel to drive!", ex.Message);
        }

        [Test]
        public void DriveMethodShouldDriveCarCorrectly()
        {
            // Arrange
            double expectedFuelAmount = 90.0;

            // Act
            car.Refuel(100);
            car.Drive(100);

            // Assert
            Assert.AreEqual(expectedFuelAmount, car.FuelAmount);
        }
    }
}