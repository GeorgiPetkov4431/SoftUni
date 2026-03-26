using NUnit.Framework;
using System;

namespace Skeleton.Tests
{
    [TestFixture]
    public class DummyTests
    {
        [Test]
        public void DummyLosesHealthAfterAttack()
        {
            // Arrange
            Dummy dummy = new Dummy(100, 200);
            Axe axe = new Axe(10, 100);

            // Act
            axe.Attack(dummy);

            // Assert
            Assert.That(dummy.Health, Is.EqualTo(90), "Dummy Health doesn't change after attack.");
        }

        [Test]
        public void DeadDummyShouldThrowExceptionIfAttacked()
        {
            // Arrange
            Dummy dummy = new Dummy(0, 200);
            Axe axe = new Axe(10, 100);

            // Assert
            Assert.Throws<InvalidOperationException>(() =>
            {
                // Act
                axe.Attack(dummy);
            }, "Dummy is already dead.");
        }

        [Test]
        public void DeadDummyShouldGiveExperience()
        {
            // Arrange
            Dummy dummy = new Dummy(0, 200);

            // Act
            int experience = dummy.GiveExperience();

            // Assert
            Assert.That(experience, Is.EqualTo(200), "Dead Dummy doesn't give experience.");
        }

        [Test]
        public void AliveDummyShouldNotGiveExperience()
        {
            // Arrange
            Dummy dummy = new Dummy(100, 200);

            // Assert
            Assert.Throws<InvalidOperationException>(() =>
            {
                // Act
                dummy.GiveExperience();
            }, "Alive Dummy should not give experience.");
        }
    }
}