using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballTeamGenerator.Models
{
    public class Player
    {
        private const string StatOutOfRangeException = "{0} should be between 0 and 100.";

        private string name;
        private int endurance;
        private int sprint;
        private int dribble;
        private int passing;
        private int shooting;

        public Player(string name, int endurance, int sprint, int dribble, int passing, int shooting)
        {
            this.Name = name;
            this.Endurance = endurance;
            this.Sprint = sprint;
            this.Dribble = dribble;
            this.Passing = passing;
            this.Shooting = shooting;
        }

        public string Name
        {
            get { return this.name; }
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("A name should not be empty.");
                }

                this.name = value;
            }
        }

        public int Endurance
        {
            get { return this.endurance; }
            private set
            {
                if (!CheckStatValue(value))
                {
                    throw new ArgumentException(string.Format(StatOutOfRangeException, nameof(Endurance)));
                }

                this.endurance = value;
            }
        }

        public int Sprint
        {
            get { return this.sprint; }
            private set
            {
                if (!CheckStatValue(value))
                {
                    throw new ArgumentException(string.Format(StatOutOfRangeException, nameof(Sprint)));
                }

                this.sprint = value;
            }
        }

        public int Dribble
        {
            get { return this.dribble; }
            private set
            {
                if (!CheckStatValue(value))
                {
                    throw new ArgumentException(string.Format(StatOutOfRangeException, nameof(Dribble)));
                }

                this.dribble = value;
            }
        }

        public int Passing
        {
            get { return this.passing; }
            private set
            {
                if (!CheckStatValue(value))
                {
                    throw new ArgumentException(string.Format(StatOutOfRangeException, nameof(Passing)));
                }

                this.passing = value;
            }
        }

        public int Shooting
        {
            get { return this.shooting; }
            private set
            {
                if (!CheckStatValue(value))
                {
                    throw new ArgumentException(string.Format(StatOutOfRangeException, nameof(Shooting)));
                }

                this.shooting = value;
            }
        }

        public double StatAverage
        {
            get { return (Endurance + Sprint + Dribble + Passing + Shooting) / 5.0; }
        }

        private bool CheckStatValue(int value)
        {
            return value >= 0 && value <= 100;
        }
    }
}
