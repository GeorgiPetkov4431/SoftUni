using System;
using System.Text.RegularExpressions;

namespace _02._Message_Decrypter;

internal class Program
{
    static void Main(string[] args)
    {
        string expression = @"^(\$|\%)(?<Tag>[A-Z][a-z]{2,})\1\:\ \[(?<Number1>[0-9]+)\]\|\[(?<Number2>[0-9]+)\]\|\[(?<Number3>[0-9]+)\]\|$";

        int count = int.Parse(Console.ReadLine());

        for (int i = 0; i < count; i++)
        {
            string text = Console.ReadLine();
            Match match = Regex.Match(text, expression);

            if (!match.Success)
            {
                Console.WriteLine("Valid message not found!");
                continue;
            }

            char[] decryptedMessageArr = {(char)int.Parse(match.Groups["Number1"].Value), (char)int.Parse(match.Groups["Number2"].Value), (char)int.Parse(match.Groups["Number3"].Value)};
            string decryptedMessage = new string(decryptedMessageArr);

            Console.WriteLine($"{match.Groups["Tag"].Value}: {decryptedMessage}");
        }
    }
}
