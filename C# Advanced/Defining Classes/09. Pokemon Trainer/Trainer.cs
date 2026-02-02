using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonTrainer
{
    public class Trainer
    {
        public Trainer(string name)
        {
            this.Name = name;
            BadgeNumber = 0;
            this.Pokemon = new List<Pokemon>();
        }

        public string Name { get; set; }

        public int BadgeNumber { get; set; }

        public List<Pokemon> Pokemon { get; set; }

        public void CheckPokemon(string element)
        {
            if (Pokemon.Any(t => t.Element == element))
            {
                BadgeNumber++;
            }
            else
            {
                for (int i = 0; i < Pokemon.Count; i++)
                {
                    Pokemon[i].Health -= 10;
                    if (Pokemon[i].Health <= 0)
                    {
                        Pokemon.Remove(Pokemon[i]);
                        i--;
                    }
                }
            }
        }

        public override string ToString()
        {
            return $"{Name} {BadgeNumber} {Pokemon.Count}";
        }
    }
}
