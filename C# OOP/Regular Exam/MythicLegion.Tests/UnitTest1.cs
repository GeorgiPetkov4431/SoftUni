using System;
using System.Text;
using NUnit.Framework;

namespace MythicLegion.Tests
{
    public class Tests
    {
        Hero hero;
        Legion legion;

        [SetUp]
        public void Setup()
        {
            hero = new Hero("Arthur", "Knight");
            legion = new Legion();
        }

        [Test]
        public void HeroConstructorInitializesWithValidCapacity()
        {
            // Arrange
            string name = "Arthur";
            string type = "Knight";
            int power = 20;
            int health = 100;
            bool isTrained = false;
            string heroStats = $"{name} ({type}) - Power: {power}, Health: {health}, Trained: {isTrained}";

            // Act

            // Assert
            Assert.AreEqual(name, hero.Name);
            Assert.AreEqual(type, hero.Type);
            Assert.AreEqual(power, hero.Power);
            Assert.AreEqual(health, hero.Health);
            Assert.IsFalse(hero.IsTrained);
            Assert.AreEqual(heroStats, hero.ToString());
        }

        [Test]
        public void LegionConstructorInitializesWithValidCapacity()
        {
            string expected = "No heroes in the legion.";

            Assert.AreEqual(expected, legion.GetLegionInfo());
        }

        [Test]
        public void HeroesCanBeAddedToLegion()
        {
            //Arrange
            Hero hero2 = new Hero("Lancelot", "Knight");
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(hero.ToString());
            sb.Append(hero2.ToString());

            // Act
            legion.AddHero(hero);
            legion.AddHero(hero2);

            // Assert
            Assert.AreEqual(sb.ToString(), legion.GetLegionInfo());
        }

        [Test]
        public void TrainHeroIncreasesStats()
        {
            // Arrange
            string expected = "Arthur has been trained.";
            int power = 30;
            int health = 101;

            // Act
            legion.AddHero(hero);
            string actual = legion.TrainHero("Arthur");

            // Assert
            Assert.AreEqual(expected, actual);
            Assert.AreEqual(power, hero.Power);
            Assert.AreEqual(health, hero.Health);
            Assert.IsTrue(hero.IsTrained);
        }

        [TestCase("Arthorias")]
        [TestCase("Arthar")]
        [TestCase("Artur")]
        public void LegionDoesntContainAHero(string name)
        {
            // Arrange
            string expected = $"Hero with name {name} not found.";

            // Act
            string actual = legion.TrainHero(name);

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void LegionThrowsExceptionIfHeroIsNull()
        {
            // Arrange
            Hero nullHero = null;

            // Act
            // Assert
            ArgumentNullException ex = Assert.Throws<ArgumentNullException>(() => legion.AddHero(nullHero));
            Assert.AreEqual("Hero cannot be null (Parameter 'hero')", ex.Message);
        }

        [Test]
        public void LegionThrowsExceptionIfHeroAlreadyExists()
        {
            // Arrange
            legion.AddHero(hero);
            Hero cloneHero = new Hero("Arthur", "Knight");

            // Act
            // Assert
            ArgumentException ex = Assert.Throws<ArgumentException>(() => legion.AddHero(cloneHero));
            Assert.AreEqual("Hero with name Arthur already exists in the legion.", ex.Message);
        }

        [Test]
        public void HeroCanBeRemovedFromLegion()
        {
            // Arrange
            legion.AddHero(hero);

            // Act
            bool result = legion.RemoveHero("Arthur");

            // Assert
            Assert.IsTrue(result);
        }

        [Test]
        public void HeroCannotBeRemovedIfHeIsAlreadyRemoved()
        {
            // Arrange
            legion.AddHero(hero);

            // Act
            legion.RemoveHero("Arthur");
            bool result = legion.RemoveHero("Arthur");

            // Assert
            Assert.IsFalse(result);
        }

        [Test]
        public void HeroCannotBeRemovedIfHeDoesNotExist()
        {
            // Arrange
            legion.AddHero(hero);

            // Act
            bool result = legion.RemoveHero("Lancelot");

            // Assert
            Assert.IsFalse(result);
        }
    }
}