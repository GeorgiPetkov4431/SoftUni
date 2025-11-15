using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace _02._Race;

internal class Program
{
    static void Main(string[] args)
    {
        string namePattern = @"[A-Za-z]";
        string speedPattern = @"\d";

        string[] participantsArr = Console.ReadLine().Split(", ", StringSplitOptions.RemoveEmptyEntries);

        Dictionary<string, Participant> participants = new Dictionary<string, Participant>();

        foreach (string participant in participantsArr)
        {
            participants.Add(participant, new Participant(participant));
        }

        string command;
        while ((command = Console.ReadLine()) != "end of race")
        {
            StringBuilder name = new StringBuilder();
            foreach (Match m in Regex.Matches(command, namePattern))
            {
                name.Append(m.Value);
            }

            int distance = 0;
            foreach (Match m in Regex.Matches(command, speedPattern))
            {
                distance += int.Parse(m.Value);
            }

            if (participants.ContainsKey(name.ToString()))
            {
                participants[name.ToString()].Distance += distance;
            }
        }

        List<Participant> orderedParticipants = participants.Select(p => p.Value).OrderByDescending(p => p.Distance).Take(3).ToList();

        Console.WriteLine($"1st place: {orderedParticipants[0].Name}");
        Console.WriteLine($"2nd place: {orderedParticipants[1].Name}");
        Console.WriteLine($"3rd place: {orderedParticipants[2].Name}");
    }
}

class Participant
{
    public string Name {  get; set; }
    public int Distance { get; set; }

    public Participant(string name)
    {
        Name = name;
    }
}