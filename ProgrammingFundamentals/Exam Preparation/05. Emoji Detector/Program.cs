using System;
using System.Text.RegularExpressions;

namespace _05._Emoji_Detector;

internal class Program
{
    static void Main(string[] args)
    {
        string text = Console.ReadLine();
        string emojiPattern = @"(\:\:|\*\*)(?<Emoji>[A-Z][a-z]{2,})\1";
        string numberPattern = @"\d";

        MatchCollection emojiMatches = Regex.Matches(text, emojiPattern);
        MatchCollection numberMatches = Regex.Matches(text, numberPattern);

        long coolThresholdSum = 1;

        foreach (Match match in numberMatches)
        {
            coolThresholdSum *= int.Parse(match.Value);
        }

        Console.WriteLine($"Cool threshold: {coolThresholdSum}");
        Console.WriteLine($"{emojiMatches.Count} emojis found in the text. The cool ones are:");

        foreach (Match emoji in emojiMatches)
        {
            long coolness = emoji.Groups["Emoji"].Value.ToCharArray().Sum(c => c);

            if (coolness >= coolThresholdSum) Console.WriteLine(emoji.Value);
        }
    }
}

/*internal class Program
{
    static void Main(string[] args)
    {
        List<string> emojis = new List<string>();
        string input = Console.ReadLine();
        string pattern = @"(\:\:|\*\*)[A-Z][a-z]{2,}\1";

        long coolThresholdSum = GetCoolThreshold(input);

        MatchCollection matches = Regex.Matches(input, pattern);
        foreach (Match match in matches)
        {
            int coolness = CalculateCoolness(match.Value);
            if (coolness > coolThresholdSum)
            {
                emojis.Add(match.Value);
            }
        }

        Console.WriteLine($"Cool threshold: {coolThresholdSum}");
        Console.WriteLine($"{emojis.Count} emojis found in the text. The cool ones are:");
        foreach (string emoji in emojis)
        {
            Console.WriteLine(emoji);
        }
    }

    static long GetCoolThreshold(string input)
    {
        int result = 1;
        string pattern = @"\d";
        MatchCollection matches = Regex.Matches(input, pattern);

        foreach (Match match in matches)
        {
            result *= int.Parse(match.Value);
        }

        return result;
    }

    static int CalculateCoolness(string emoji)
    {
        int result = 0;
        emoji = emoji.Remove(emoji.Length - 1, 2);
        emoji = emoji.Remove(0, 2);

        foreach (char c in emoji)
        {
            result += (int)c;
        }

        return result;
    }
}*/


