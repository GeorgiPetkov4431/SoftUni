using LegendsOfValor_TheGuildTrials.Models.Contracts;
using LegendsOfValor_TheGuildTrials.Utilities.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LegendsOfValor_TheGuildTrials.Models
{
    public class Guild : IGuild
    {
        private string name;
        private int wealth;
        private readonly List<string> legion;
        private bool isFallen;

        public Guild(string name)
        {
            this.Name = name;
            this.Wealth = 5000;
            this.legion = new List<string>();
            this.isFallen = false;
        }

        public string Name
        {
            get { return name; }
            private set
            {
                if (value != "WarriorGuild" && value != "SorcererGuild" && value != "ShadowGuild")
                {
                    throw new ArgumentException(ErrorMessages.InvalidGuildName);
                }

                this.name = value;
            }
        }

        public int Wealth
        {
            get { return this.wealth; }
            set
            {
                this.wealth = value;

                if (this.wealth < 0)
                {
                    this.wealth = 0;
                }
            }
        }

        public IReadOnlyCollection<string> Legion { get { return this.legion.AsReadOnly(); } }

        public bool IsFallen
        {
            get { return this.isFallen; }
            set { this.isFallen = value; }
        }

        public void RecruitHero(IHero hero)
        {
            legion.Add(hero.RuneMark);
            this.Wealth -= 500;
        }

        public void TrainLegion(ICollection<IHero> heroesToTrain)
        {
            foreach (IHero hero in heroesToTrain)
            {
                this.Wealth -= 200;
                hero.Train();
            }
        }

        public void WinWar(int goldAmount)
        {
            this.Wealth += goldAmount;
        }

        public void LoseWar()
        {
            this.IsFallen = true;
            this.Wealth = 0;
        }
    }
}
