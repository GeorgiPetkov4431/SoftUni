using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake.Contracts
{
    public interface IRandomGenerator
    {
        int NextNumber(int min = 0, int max = int.MaxValue);
    }
}
