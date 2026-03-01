using System;
using System.Collections.Generic;
using System.Linq;
using FootballTeamGenerator.Models;


namespace FootballTeamGenerator
{
    public class StartUp
    {
        static void Main(string[] args)
        {
            List<Team> teams = new List<Team>();

            string input;

            while ((input = Console.ReadLine()) != "END")
            {
                try
                {
                    string[] inputArgs = input.Split(';', StringSplitOptions.RemoveEmptyEntries);
                    string command = inputArgs[0];
                    string teamName = inputArgs[1];

                    switch (command)
                    {
                        case "Team":
                            AddTeam(teamName, teams);
                            break;

                        case "Add":
                            AddPlayer(teamName, inputArgs[2], int.Parse(inputArgs[3]), int.Parse(inputArgs[4]), int.Parse(inputArgs[5]), int.Parse(inputArgs[6]), int.Parse(inputArgs[7]), teams);
                            break;

                        case "Remove":
                            RemovePlayer(teamName, inputArgs[2], teams);
                            break;

                        case "Rating":
                            PrintRating(teamName, teams);
                            break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }

        public static void AddTeam(string team, List<Team> teams)
        {
            teams.Add(new Team(team));
        }

        public static void AddPlayer(string teamName, string playerName,
            int endurance, int sprint, int dribble, int passing, int shooting,
            List<Team> teams)
        {
            Team team = teams.FirstOrDefault(t => t.Name == teamName);

            if (team is null)
            {
                throw new ArgumentException($"Team {teamName} does not exist.");
            }

            Player newPlayer = new Player(playerName, endurance, sprint, dribble, passing, shooting);
            team.Add(newPlayer);
        }

        public static void RemovePlayer(string teamName, string playerName, List<Team> teams)
        {
            Team team = teams.FirstOrDefault(t => t.Name == teamName);

            if (team is null)
            {
                throw new ArgumentException($"Team {teamName} does not exist.");
            }

            team.Remove(playerName);
        }

        public static void PrintRating(string teamName, List<Team> teams)
        {
            Team team = teams.FirstOrDefault(t => t.Name == teamName);

            if (team is null)
            {
                throw new ArgumentException($"Team {teamName} does not exist.");
            }

            Console.WriteLine($"{teamName} - {team.Rating:F0}");
        }
    }
}
