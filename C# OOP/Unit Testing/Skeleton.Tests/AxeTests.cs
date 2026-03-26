using NUnit.Framework;
using System;

namespace Skeleton.Tests
{
    [TestFixture]
    public class AxeTests
    {
        [Test]
        public void AxeLosesDurabilityAfterAttack()
        {
            // Arrange
            Axe axe = new Axe(10, 20);
            Dummy dummy = new Dummy(100, 200);

            // Act
            axe.Attack(dummy);
            axe.Attack(dummy);

            // Assert
            Assert.That(axe.DurabilityPoints, Is.EqualTo(18), "Axe Durability doesn't change after attack.");
        }

        [Test]
        public void AxeCannotAttackWithZeroDurability()
        {
            // Arrange
            Axe axe = new Axe(10, 0);
            Dummy dummy = new Dummy(100, 200);

            // Assert
            Assert.Throws<InvalidOperationException>(() =>
            {
                // Act
                axe.Attack(dummy);
            }, "Axe is broken.");
        }
    }
}