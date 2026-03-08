using Raiding.Core.Interfaces;
using Raiding.Factories.Interfaces;
using Raiding.IO.Interfaces;
using Raiding.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Raiding.Core
{
    public class Engine : IEngine
    {
        private readonly IReader reader;
        private readonly IWriter writer;
        private readonly IHeroFactory heroFactory;

        private readonly List<IHero> heroes;

        public Engine(IReader reader, IWriter writer, IHeroFactory heroFactory)
        {
            this.reader = reader;
            this.writer = writer;
            this.heroFactory = heroFactory;

            heroes = new List<IHero>();
        }

        public void Run()
        {
            int heroNum = int.Parse(reader.ReadLine());

            while (heroNum > 0)
            {
                string heroName = reader.ReadLine();
                string heroType = reader.ReadLine();

                try
                {
                    this.heroes.Add(heroFactory.Create(heroType, heroName));
                    heroNum--;
                }
                catch (ArgumentException ex)
                {
                    writer.WriteLine(ex.Message);
                }
            }

            foreach (var hero in this.heroes)
            {
                writer.WriteLine(hero.CastAbility());
            }

            int bossPower = int.Parse(reader.ReadLine());

            if (this.heroes.Sum(h => h.Power) >= bossPower)
            {
                writer.WriteLine("Victory!");
            }
            else
            {
                writer.WriteLine("Defeat...");
            }
        }
    }
}
