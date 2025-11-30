using System;

namespace _01._Hogwarts;

internal class Program
{
    static void Main(string[] args)
    {
        string spell = Console.ReadLine();

        string command;

        while ((command = Console.ReadLine()) != "Abracadabra")
        {
            string[] commandArgs = command.Split(" ", StringSplitOptions.RemoveEmptyEntries);

            switch (commandArgs[0])
            {
                case "Abjuration":
                    spell = Abjuration(spell);
                    Console.WriteLine(spell);
                    break;
                case "Necromancy":
                    spell = Necromancy(spell);
                    Console.WriteLine(spell);
                    break;
                case "Illusion":
                    int index = int.Parse(commandArgs[1]);
                    char letter = char.Parse(commandArgs[2]);
                    spell = Illusion(spell, index, letter);
                    break;
                case "Divination":
                    string firstSubstring = commandArgs[1];
                    string secondSubstring = commandArgs[2];
                    spell = Divination(spell, firstSubstring, secondSubstring);
                    break;
                case "Alteration":
                    string substring = commandArgs[1];
                    spell = Alteration(spell, substring);
                    break;
                default:
                    Console.WriteLine("The spell did not work!");
                    break;
            }
        }
    }

    static string Abjuration(string spell)
    {
        return spell.ToUpper();
    }

    static string Necromancy(string spell)
    {
        return spell.ToLower();
    }

    static string Illusion(string spell, int index, char letter)
    {
        if (index < 0 || index >= spell.Length)
        {
            Console.WriteLine("The spell was too weak.");
            return spell;
        }

        string newSpell = spell.Substring(0, index) + letter + spell.Substring(index + 1);
        Console.WriteLine("Done!");
        return newSpell;
    }

    static string Divination(string spell, string firstSubstring, string secondSubstring)
    {
        if (!spell.Contains(firstSubstring)) return spell;

        spell = spell.Replace(firstSubstring, secondSubstring);
        Console.WriteLine(spell);
        return spell;
    }

    static string Alteration(string spell, string substring)
    {
        int substringIndex = spell.IndexOf(substring);
        if (substringIndex == -1) return spell;

        string newSpell = spell.Substring(0, substringIndex) + spell.Substring(substringIndex + substring.Length);
        Console.WriteLine(newSpell);

        return newSpell;
    }
}
