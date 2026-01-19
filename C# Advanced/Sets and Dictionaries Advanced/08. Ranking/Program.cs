using System;
using System.Collections.Generic;
using System.Linq;

namespace _08._Ranking;

internal class Program
{
    static void Main(string[] args)
    {
        Dictionary<string, string> contestPasswords = new Dictionary<string, string>();
        SortedDictionary<string, Dictionary<string, int>> students = new SortedDictionary<string, Dictionary<string, int>>();

        string input;

        while ((input = Console.ReadLine()) != "end of contests")
        {
            string[] inputArgs = input.Split(':', StringSplitOptions.RemoveEmptyEntries);
            string contest = inputArgs[0];
            string password = inputArgs[1];
            if (!contestPasswords.ContainsKey(contest))
            {
                contestPasswords.Add(contest, password);
            }
        }

        while ((input = Console.ReadLine()) != "end of submissions")
        {
            string[] inputArgs = input.Split("=>", StringSplitOptions.RemoveEmptyEntries);
            string contest = inputArgs[0];
            string password = inputArgs[1];
            string username = inputArgs[2];
            int points = int.Parse(inputArgs[3]);

            if (contestPasswords.ContainsKey(contest) && contestPasswords[contest] == password)
            {
                if (!students.ContainsKey(username))
                {
                    students.Add(username, new Dictionary<string, int>());
                }

                if (!students[username].ContainsKey(contest))
                {
                    students[username].Add(contest, points);
                }

                if (students[username][contest] < points)
                {
                    students[username][contest] = points;
                }
            }
        }

        string bestCandidate = students.MaxBy(student => student.Value.Values.Sum()).Key;
        int bestCandidatePoints = students[bestCandidate].Values.Sum();

        Console.WriteLine($"Best candidate is {bestCandidate} with total {bestCandidatePoints} points.");
        Console.WriteLine("Ranking:");
        foreach (var student in students.OrderBy(st => st.Key))
        {
            Console.WriteLine(student.Key);
            foreach (var subject in student.Value.OrderByDescending(sc => sc.Value))
            {
                Console.WriteLine($"#  {subject.Key} -> {subject.Value}");
            }
        }
    }
}
