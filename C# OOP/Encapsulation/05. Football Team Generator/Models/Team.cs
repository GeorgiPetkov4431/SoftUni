using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballTeamGenerator.Models
{
    public class Team
    {
        private string name;
        private List<Player> players;

        public Team(string name)
        {
            players = new List<Player>();

            this.Name = name;
        }

        public string Name
        {
            get { return this.name; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("A name should not be empty.");
                }

                this.name = value;
            }
        }

        public double Rating
        {
            get
            {
                if (players.Any())
                {
                    return players.Average(p => p.StatAverage);
                }

                return 0;
            }
        }

        public void Add(Player player)
        {
            players.Add(player);
        }

        public void Remove(string playerName)
        {
            Player player = players.FirstOrDefault(p => p.Name == playerName);

            if (player is null)
            {
                throw new ArgumentException($"Player {playerName} is not in {this.Name} team.");
            }

            players.Remove(player);
        }
    }
}
