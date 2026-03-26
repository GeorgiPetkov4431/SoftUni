using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _00._Playground.Test
{
    public class MyMathClassTest
    {
        [Test]
        public void Sum_ShouldReturnCorrectResult()
        {
            MyMathClass math = new MyMathClass();
            int result = math.Sum(2, 3);

            Assert.That(result, Is.EqualTo(5));
        }

        [Test]
        public void Product_ShouldReturnCorrectResult()
        {
            MyMathClass math = new MyMathClass();
            int result = math.Product(2, 3);

            Assert.That(result, Is.EqualTo(6));
        }

        [Test]
        public void Power_ShouldReturnCorrectResult()
        {
            MyMathClass math = new MyMathClass();
            int result = math.Power(2, 3);

            Assert.That(result, Is.EqualTo(8));
        }
    }
}