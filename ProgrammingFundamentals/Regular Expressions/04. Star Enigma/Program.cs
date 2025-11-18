using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace _04._Star_Enigma;

internal class Program
{
    static void Main(string[] args)
    {
        List<Message> messages = new List<Message>();
        string messagePattern = @"\@(?<Name>[A-Za-z]+)[^\@\-\!\:\>]*\:(?<Population>\d+)[^\@\-\!\:\>]*\!(?<AttackType>A|D)\![^\@\-\!\:\>]*\-\>[^\@\-\!\:\>]*?(?<SoldierCount>\d+)[^\@\-\!\:\>]*"
;

        int messageCount = int.Parse(Console.ReadLine());

        for (int i = 0; i < messageCount; i++)
        {
            string message = Console.ReadLine();
            message = Decrypt(message);

            Match match = Regex.Match(message, messagePattern);

            if (!match.Success) continue;

            Message newMessage = new Message(match.Groups["Name"].Value,
                                            uint.Parse(match.Groups["Population"].Value),
                                            match.Groups["AttackType"].Value,
                                            uint.Parse(match.Groups["SoldierCount"].Value));

            messages.Add(newMessage);
        }

        List<Message> orderedMessages = messages.Where(m => m.AttackType == "A").OrderBy(m => m.PlanetName).ToList();

        Console.WriteLine($"Attacked planets: {orderedMessages.Count}");
        foreach (Message m in orderedMessages)
        {
            Console.WriteLine($"-> {m.PlanetName}");
        }

        orderedMessages = messages.Where(m => m.AttackType == "D").OrderBy(m => m.PlanetName).ToList();
        Console.WriteLine($"Destroyed planets: {orderedMessages.Count}");
        foreach (Message m in orderedMessages)
        {
            Console.WriteLine($"-> {m.PlanetName}");
        }
    }

    public static string Decrypt(string message)
    {
        StringBuilder result = new StringBuilder();
        int decryptKey = CountLetters(message);

        foreach (char c in message)
        {
            result.Append((char)(c - decryptKey));
        }

        return result.ToString();
    }

    public static int CountLetters(string message)
    {
        string pattern = @"[SsTtAaRr]";

        return Regex.Matches(message, pattern).Count;
    }
}

class Message
{
    public string PlanetName { get; set; }
    public uint Population { get; set; }
    public string AttackType { get; set; }
    public uint SoldierCount { get; set; }

    public Message(string planetName, uint population, string attackType, uint soldierCount)
    {
        PlanetName = planetName;
        Population = population;
        AttackType = attackType;
        SoldierCount = soldierCount;
    }
}
