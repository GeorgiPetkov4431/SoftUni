using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LegendsOfValor_TheGuildTrials.Models
{
    public class Sorcerer : Hero
    {
        private const int StartingPower = 40;
        private const int StartingMana = 120;
        private const int StartingStamina = 0;

        public Sorcerer(string name, string runeMark) 
            : base(name, runeMark, StartingPower, StartingMana, StartingStamina)
        {
        }

        public override void Train()
        {
            this.Power += 20;
            this.Mana += 25;
        }
    }
}
