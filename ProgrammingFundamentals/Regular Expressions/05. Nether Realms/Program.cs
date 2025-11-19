using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace _05._Nether_Realms;

internal class Program
{
    static void Main(string[] args)
    {
        string[] demonInput = Console.ReadLine().Split(new[] { ',', ' ' }, StringSplitOptions.RemoveEmptyEntries);

        List<Demon> demons = new List<Demon>();

        foreach (string demon in demonInput)
        {
            int health = CalculateHealth(demon);
            decimal damage = CalculateDamage(demon);

            Demon newDemon = new Demon(demon, health, damage);
            demons.Add(newDemon);
        }

        demons = demons.OrderBy(d => d.Name).ToList();

        foreach (Demon demon in demons)
        {
            Console.WriteLine(demon);
        }
    }

    static int CalculateHealth(string demon)
    {
        string pattern = @"[^0-9\+\-\*\/\.]";
        int health = 0;

        MatchCollection matches = Regex.Matches(demon, pattern);

        foreach (Match match in matches)
        {
            health += match.Value[0];
        }

        return health;
    }

    static decimal CalculateDamage(string demon)
    {
        string baseDamagePattern = @"(?:(?:[-+]*)(?:\d+\.\d+|\d+))";
        string damageAlterPattern = @"[\/\*]";
        decimal damage = 0m;

        MatchCollection matches = Regex.Matches(demon, baseDamagePattern);

        foreach (Match match in matches)
        {
            decimal number;
            decimal.TryParse(match.Value, out number);
            damage += number;
        }

        matches = Regex.Matches(demon, damageAlterPattern);

        foreach (Match match in matches)
        {
            switch (match.Value)
            {
                case "/":
                    damage /= 2.0m;
                    break;
                case "*":
                    damage *= 2;
                    break;
            }
        }

        return damage;
    }
}

class Demon
{
    public string Name { get; set; }
    public int Health { get; set; }
    public decimal Damage { get; set; }

    public Demon(string name, int health, decimal damage)
    {
        Name = name;
        Health = health;
        Damage = damage;
    }

    public override string ToString()
    {
        return $"{Name} - {Health} health, {Damage:F2} damage";
    }
}
