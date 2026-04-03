using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FakeAxeAndDummy
{
    public interface ITarget
    {
        void TakeAttack(int attackPoints);

        int GiveExperience();

        bool IsDead();
    }
}
