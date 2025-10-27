using System;
using System.Collections.Generic;
using System.Linq;

namespace _05._Teamwork_Projects;

internal class Program
{
    static void Main(string[] args)
    {
        List<Team> teams = new List<Team>();

        int teamCount = int.Parse(Console.ReadLine());

        for (int i = 0; i < teamCount; i++)
        {
            string[] teamCommand = Console.ReadLine().Split('-');

            Team newTeam = new Team(teamCommand[1], teamCommand[0]);
            if (teams.Find(newTeam => newTeam.Name == teamCommand[1]) != null)
            {
                Console.WriteLine($"Team {teamCommand[1]} was already created!");
                continue;
            }

            if (teams.Find(newTeam => newTeam.Creator == teamCommand[0]) != null)
            {
                Console.WriteLine($"{teamCommand[0]} cannot create another team!");
                continue;
            }

            teams.Add(newTeam);
            Console.WriteLine($"Team {teamCommand[1]} has been created by {teamCommand[0]}!");
        }

        string command;

        while ((command = Console.ReadLine()) != "end of assignment")
        {
            string[] commandArgs = command.Split("->");

            if (!teams.Any(team => team.Name == commandArgs[1]))
            {
                Console.WriteLine($"Team {commandArgs[1]} does not exist!");
                continue;
            }

            if (teams.Any(team => team.Creator == commandArgs[0]) || teams.Any(team => team.Members.Contains(commandArgs[0])))
            {
                Console.WriteLine($"Member {commandArgs[0]} cannot join team {commandArgs[1]}!");
                continue;
            }

            teams.Find(team => team.Name == commandArgs[1]).Members.Add(commandArgs[0]);
        }

        List<Team> disbandedTeams = teams.Where(team => team.Members.Count <= 0).ToList();
        teams = teams.Where(team => team.Members.Count > 0).ToList();

        teams = teams.OrderByDescending(team => team.Members.Count).ThenBy(team => team.Name).ToList();

        teams.ForEach(team => Console.WriteLine(team));

        Console.WriteLine("Teams to disband:");
        disbandedTeams = disbandedTeams.OrderBy(team => team.Name).ToList();
        disbandedTeams.ForEach(team => Console.WriteLine(team.Name));
    }
}

class Team
{
    public Team(string teamName, string creator)
    {
        this.Name = teamName;
        this.Creator = creator;
        Members = new List<string>();
    }

    public string Name { get; set; }
    public string Creator { get; set; }
    public List<string> Members { get; set; }

    public override string ToString()
    {
        return $"{Name}\n- {Creator}\n{GetMembersString()}";
    }

    public string GetMembersString()
    {
        Members = Members.OrderBy(member => member).ToList();
        string members = "";
        for (int i = 0; i < Members.Count - 1; i++)
        {
            members += $"-- {Members[i]}\n";
        }
        members += $"-- {Members[Members.Count - 1]}";
        return members;
    }
}
