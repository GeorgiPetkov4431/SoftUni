using System;
using System.Collections.Generic;
using System.Linq;

namespace _10._ForceBook;

internal class Program
{
    static void Main(string[] args)
    {
        Dictionary<string, HashSet<string>> sides = new Dictionary<string, HashSet<string>>();

        string input;

        while ((input = Console.ReadLine()) != "Lumpawaroo")
        {
            string[] inputArgs;
            if (input.Contains(" | ")) // {forceSide} | {forceUser}
            {
                inputArgs = input.Split(" | ");
                string forceSide = inputArgs[0];
                string forceUser = inputArgs[1];

                if (!sides.Values.Any(u => u.Contains(forceUser)))
                {
                    if (!sides.ContainsKey(forceSide))
                    {
                        sides.Add(forceSide, new HashSet<string>());
                    }

                    sides[forceSide].Add(forceUser);
                }
            }
            else                       // {forceUser} -> {forceSide}
            {
                inputArgs = input.Split(" -> ");
                string forceUser = inputArgs[0];
                string forceSide = inputArgs[1];

                /*if (sides.Values.Any(u => u.Contains(forceUser)))
                {
                    sides[forceSide].Remove(forceUser);
                }*/

                foreach (var side in sides)
                {
                    if (side.Value.Contains(forceUser))
                    {
                        side.Value.Remove(forceUser);
                        break;
                    }
                }

                if (!sides.ContainsKey(forceSide))
                {
                    sides.Add(forceSide, new HashSet<string>());
                }

                sides[forceSide].Add(forceUser);
                Console.WriteLine($"{forceUser} joins the {forceSide} side!");
            }
        }

        sides = sides.OrderByDescending(s => s.Value.Count).ThenBy(s => s.Key).ToDictionary();

        foreach (var side in sides)
        {
            if (side.Value.Count > 0)
            {
                Console.WriteLine($"Side: {side.Key}, Members: {side.Value.Count}");

                foreach (var user in side.Value.OrderBy(u => u))
                {
                    Console.WriteLine($"! {user}");
                }
            }
        }
    }
}
