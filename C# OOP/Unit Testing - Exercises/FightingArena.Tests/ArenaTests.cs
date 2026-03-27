namespace FightingArena.Tests
{
    using NUnit.Framework;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    [TestFixture]
    public class ArenaTests
    {
        private Arena arena;
        private Warrior warrior1;
        private Warrior warrior2;

        [SetUp]
        public void SetUp()
        {
            arena = new Arena();
            warrior1 = new Warrior("Conan", 50, 100);
            warrior2 = new Warrior("Thor", 40, 100);
        }

        [Test]
        public void ConstructorShouldInitializeCorrectly()
        {
            Assert.AreEqual(0, arena.Count);
            Assert.IsNotNull(arena.Warriors);
            Assert.IsEmpty(arena.Warriors);
        }

        [Test]
        public void EnrollShouldAddWarriorToArena()
        {
            int expectedResult = 1;

            arena.Enroll(warrior1);

            Assert.AreEqual(expectedResult, arena.Count);
            Assert.Contains(warrior1, arena.Warriors.ToList());
        }

        [Test]
        public void EnrollShouldThrowExceptionWhenWarriorAlreadyEnrolled()
        {
            arena.Enroll(warrior1);
            InvalidOperationException ex = Assert.Throws<InvalidOperationException>(() => arena.Enroll(warrior1));
            Assert.That(ex.Message, Is.EqualTo("Warrior is already enrolled for the fights!"));
        }

        //[Test]
        //public void WarriorsPropertyShouldReturnReadOnlyCollection()
        //{
        //    arena.Enroll(warrior1);

        //    IReadOnlyCollection<Warrior> warriors = arena.Warriors;

        //    Assert.AreEqual(1, warriors.Count);
        //    Assert.IsInstanceOf<IReadOnlyCollection<Warrior>>(warriors);
        //    Assert.Throws<NotSupportedException>(() =>
        //    {
        //        ((System.Collections.ICollection)warriors).Add();
        //    });
        //    Assert.Contains(warrior1, warriors.ToList());
        //}

        [Test]
        public void FightShouldDecreaseHPOfBothWarriors()
        {
            arena.Enroll(warrior1);
            arena.Enroll(warrior2);

            arena.Fight(warrior1.Name, warrior2.Name);

            Assert.AreEqual(60, warrior1.HP);
            Assert.AreEqual(50, warrior2.HP);
        }

        [Test]
        public void FightShouldThrowExceptionWhenAttackerNotEnrolled()
        {
            arena.Enroll(warrior2);

            InvalidOperationException ex = Assert.Throws<InvalidOperationException>(()
                => arena.Fight(warrior1.Name, warrior2.Name));
            Assert.That(ex.Message, Is.EqualTo($"There is no fighter with name {warrior1.Name} enrolled for the fights!"));
        }

        [Test]
        public void FightShouldThrowExceptionWhenDefenderNotEnrolled()
        {
            arena.Enroll(warrior1);

            InvalidOperationException ex = Assert.Throws<InvalidOperationException>(()
                => arena.Fight(warrior1.Name, warrior2.Name));
            Assert.That(ex.Message, Is.EqualTo($"There is no fighter with name {warrior2.Name} enrolled for the fights!"));
        }

        [Test]
        public void FightShouldThrowExceptionWhenBothWarriorsNotEnrolled()
        {
            InvalidOperationException ex = Assert.Throws<InvalidOperationException>(()
                => arena.Fight(warrior1.Name, warrior2.Name));
            Assert.That(ex.Message, Is.EqualTo($"There is no fighter with name {warrior2.Name} enrolled for the fights!"));
        }

        [Test]
        public void CountShouldIncreaseWhenWarriorEnrolled()
        {
            int expectedResult = 2;

            arena.Enroll(warrior1);
            arena.Enroll(warrior2);

            Assert.AreEqual(expectedResult, arena.Count);
        }
    }
}
