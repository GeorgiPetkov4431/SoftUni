using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FakeAxeAndDummy
{
    public interface IWeapon
    {
        void Attack(ITarget target);
    }
}
