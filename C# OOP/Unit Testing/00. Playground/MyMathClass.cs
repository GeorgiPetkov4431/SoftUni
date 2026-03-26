using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _00._Playground
{
    public class MyMathClass
    {
        public int Sum(int a, int b)
        {
            return a + b;
        }

        public int Product(int a, int b)
        {
            return a * b;
        }

        public int Power(int a, int b)
        {
            int result = 1;

            for (int i = 0; i < b; i++)
            {
                result *= a;
            }

            return result;
        }
    }
}
