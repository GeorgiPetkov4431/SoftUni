using System;
using System.Collections.Generic;
using System.Linq;

namespace _07._The_V_Logger;

internal class Program
{
    static void Main(string[] args)
    {
        string command;
        Dictionary<string, SortedSet<string>> influencers = new Dictionary<string, SortedSet<string>>();
        Dictionary<string, SortedSet<string>> influencersFollowing = new Dictionary<string, SortedSet<string>>();

        while ((command = Console.ReadLine().Trim()) != "Statistics")
        {
            string[] commandArgs = command.Split(" ", StringSplitOptions.RemoveEmptyEntries);
            string vloggerOne = commandArgs[0];
            string operation = commandArgs[1];

            switch (operation)
            {
                case "joined":
                    if (!influencers.ContainsKey(vloggerOne))
                    {
                        influencers.Add(vloggerOne, new SortedSet<string>());
                        influencersFollowing.Add(vloggerOne, new SortedSet<string>());
                    }
                    break;
                case "followed":
                    string vloggerTwo = commandArgs[2];
                    if (!influencers.ContainsKey(vloggerOne) || !influencers.ContainsKey(vloggerTwo) || (vloggerOne == vloggerTwo))
                    {
                        continue;
                    }
                    influencers[vloggerTwo].Add(vloggerOne);
                    influencersFollowing[vloggerOne].Add(vloggerTwo);
                    break;
            }
        }

        influencers = influencers.OrderByDescending(vlogger => vlogger.Value.Count).ThenBy(vlogger => influencersFollowing[vlogger.Key].Count).ToDictionary();

        Console.WriteLine($"The V-Logger has a total of {influencers.Count} vloggers in its logs.");

        int index = 1;
        foreach (var influencer in influencers)
        {
            Console.WriteLine($"{index}. {influencer.Key} : {influencer.Value.Count} followers, {influencersFollowing[influencer.Key].Count} following");
            if (index == 1)
            {
                foreach (var follower in influencer.Value)
                {
                    Console.WriteLine($"*  {follower}");
                }
            }

            index++;
        }
    }
}
