using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FakeAxeAndDummy.Tests
{
    public class FakeWeapon : IWeapon
    {
        public void Attack(ITarget target)
        {
            // Do nothing
        }
    }
}
