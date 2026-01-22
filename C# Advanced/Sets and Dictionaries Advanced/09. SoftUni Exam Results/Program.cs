using System;
using System.Collections.Generic;
using System.Linq;

namespace _09._SoftUni_Exam_Results;

internal class Program
{
    static void Main(string[] args)
    {
        Dictionary<string, int> students = new Dictionary<string, int>();
        Dictionary<string, int> languages = new Dictionary<string, int>();

        // username-language-points
        string command;

        while ((command = Console.ReadLine()) != "exam finished")
        {
            string[] commandArgs = command.Split('-', StringSplitOptions.RemoveEmptyEntries);
            string username = commandArgs[0];
            
            if (commandArgs[1] == "banned")
            {
                students.Remove(username);
            }
            else
            {
                string language = commandArgs[1];
                int points = int.Parse(commandArgs[2]);

                if (!students.ContainsKey(username))
                {
                    students.Add(username, points);
                }
                else if (students[username] < points)
                {
                    students[username] = points;
                }

                if (!languages.ContainsKey(language))
                {
                    languages.Add(language, 0);
                }

                languages[language]++;
            }
        }

        students = students.OrderByDescending(st => st.Value).ThenBy(st => st.Key).ToDictionary();
        languages = languages.OrderByDescending(l => l.Value).ThenBy(l => l.Key).ToDictionary();

        Console.WriteLine("Results:");
        foreach (var student in students)
        {
            Console.WriteLine($"{student.Key} | {student.Value}");
        }

        Console.WriteLine("Submissions:");
        foreach (var language in languages)
        {
            Console.WriteLine($"{language.Key} - {language.Value}");
        }
    }
}
