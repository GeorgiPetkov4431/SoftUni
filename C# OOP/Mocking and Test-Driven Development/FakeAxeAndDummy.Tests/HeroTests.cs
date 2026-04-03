using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FakeAxeAndDummy.Tests
{
    public class HeroTests
    {
        [Test]
        public void HeroGetsExperienceWhenTargetIsDead()
        {
            // Arrange
            const int experience = 250; 
            //Mock<ITarget> mockTarget = new Mock<ITarget>();

            //mockTarget.Setup(t => t.IsDead()).Returns(true);

            //mockTarget.Setup(t => t.GiveExperience()).Returns(250);

            //Mock<IWeapon> mockWeapon = new Mock<IWeapon>();

            Hero testHero = new Hero("TestHero", new FakeWeapon());

            // Act
            testHero.Attack(new FakeTarget());

            // Assert
            Assert.That(testHero.Experience, Is.EqualTo(experience));
        }
    }
}
