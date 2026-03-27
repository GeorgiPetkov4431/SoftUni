namespace FightingArena.Tests
{
    using NUnit.Framework;
    using System;

    [TestFixture]
    public class WarriorTests
    {
        private const string ValidName = "Conan";
        private const int ValidDamage = 50;
        private const int ValidHP = 100;

        [Test]
        public void ConstructorShouldInitializeCorrectlyWhenValidParameters()
        {
            // Arrange
            // Act
            Warrior warrior = new Warrior(ValidName, ValidDamage, ValidHP);

            // Assert
            Assert.AreEqual(ValidName, warrior.Name);
            Assert.AreEqual(ValidDamage, warrior.Damage);
            Assert.AreEqual(ValidHP, warrior.HP);
        }

        [TestCase(null)]
        [TestCase("")]
        [TestCase("   ")]
        public void ConstructorShouldThrowExceptionWhenNameIsInvalid(string invalidName)
        {
            // Arrange
            // Act & Assert
            ArgumentException ex = Assert.Throws<ArgumentException>(() =>
            new Warrior(invalidName, ValidDamage, ValidHP));

            Assert.That(ex.Message, Is.EqualTo("Name should not be empty or whitespace!"));
        }

        [TestCase(-1)]
        [TestCase(-50)]
        public void ConstructorShouldThrowExceptionWhenHPIsNegative(int invalidHp)
        {
            // Arrange
            // Act & Assert
            ArgumentException ex = Assert.Throws<ArgumentException>(()
                => new Warrior(ValidName, ValidDamage, invalidHp));

            Assert.That(ex.Message, Is.EqualTo("HP should not be negative!"));
        }

        [TestCase(-1)]
        [TestCase(-50)]
        public void ConstructorShouldThrowExceptionWhenDamageIsNegative(int invalidDamage)
        {
            // Arrange
            // Act & Assert
            ArgumentException ex = Assert.Throws<ArgumentException>(()
                => new Warrior(ValidName, -10, ValidHP));
            Assert.That(ex.Message, Is.EqualTo("Damage value should be positive!"));
        }

        [Test]
        public void AttackShouldDecreaseOpponentHPByDamage()
        {
            Warrior attacker = new Warrior("Attacker", 40, 100);
            Warrior defender = new Warrior("Defender", 30, 100);

            attacker.Attack(defender);

            Assert.AreEqual(70, attacker.HP);
            Assert.AreEqual(60, defender.HP);
        }

        [Test]
        public void AttackShouldReduceHPToZeroIfDamageExceedsCurrentHP()
        {
            Warrior attacker = new Warrior("Attacker", 120, 100);
            Warrior defender = new Warrior("Defender", 50, 100);

            attacker.Attack(defender);

            Assert.AreEqual(50, attacker.HP);
            Assert.AreEqual(0, defender.HP);
        }

        [Test]
        public void AttackMethodShouldThrowExceptionIfAttackerHPIsBelow30()
        {
            Warrior attacker = new Warrior("Attacker", 40, 20);
            Warrior defender = new Warrior("Defender", 30, 100);

            InvalidOperationException ex = Assert.Throws<InvalidOperationException>(()
                => attacker.Attack(defender));

            Assert.That(ex.Message, Is.EqualTo("Your HP is too low in order to attack other warriors!"));
        }

        [Test]
        public void AttackMethodShouldThrowExceptionIfDefenderHPIsBelow30()
        {
            Warrior attacker = new Warrior("Attacker", 40, 100);
            Warrior defender = new Warrior("Defender", 30, 20);

            InvalidOperationException ex = Assert.Throws<InvalidOperationException>(()
                => attacker.Attack(defender));

            Assert.That(ex.Message, Is.EqualTo("Enemy HP must be greater than 30 in order to attack him!"));
        }

        [Test]
        public void AttackShouldThrowExceptionWhenDefenderIsStronger()
        {
            Warrior attacker = new Warrior("Attacker", 30, 100);
            Warrior defender = new Warrior("Defender", 150, 100);

            InvalidOperationException ex = Assert.Throws<InvalidOperationException>(()
                => attacker.Attack(defender));

            Assert.That(ex.Message, Is.EqualTo("You are trying to attack too strong enemy"));
        }

        [Test]
        public void AttackShouldAllowExactThresholdHP()
        {
            Warrior attacker = new Warrior("Attacker", 40, 31);
            Warrior defender = new Warrior("Defender", 20, 31);

            attacker.Attack(defender);

            int expectedHP = Math.Max(0, defender.HP);
            Assert.AreEqual(0, expectedHP);
        }
    }
}