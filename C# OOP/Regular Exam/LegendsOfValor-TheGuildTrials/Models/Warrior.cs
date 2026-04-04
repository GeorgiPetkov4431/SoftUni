using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LegendsOfValor_TheGuildTrials.Models
{
    public class Warrior : Hero
    {
        private const int StartingPower = 60;
        private const int StartingMana = 0;
        private const int StartingStamina = 100;

        public Warrior(string name, string runeMark) 
            : base(name, runeMark, StartingPower, StartingMana, StartingStamina)
        {
        }

        public override void Train()
        {
            this.Power += 30;
            this.Stamina += 10;
        }
    }
}
