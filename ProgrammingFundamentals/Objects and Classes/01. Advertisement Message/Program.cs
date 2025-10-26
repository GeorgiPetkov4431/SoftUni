using System;

namespace _01._Advertisement_Message;

internal class Program
{
    static void Main(string[] args)
    {
        Advertisement advertisement = new Advertisement();

        int messageCount = int.Parse(Console.ReadLine());

        for (int i = 0; i < messageCount; i++)
        {
            Console.WriteLine(advertisement.GetAdvertisement());
        }
    }
}

class Advertisement 
{
    private readonly string[] Phrases = { "Excellent product.",
        "Such a great product.",
        "I always use that product.",
        "Best product of its category.",
        "Exceptional product.",
        "I can't live without this product." };

    private readonly string[] Events = { "Now I feel good.",
        "I have succeeded with this product.",
        "Makes miracles. I am happy of the results!",
        "I cannot believe but now I feel awesome.",
        "Try it yourself, I am very satisfied.",
        "I feel great!" };

    private readonly string[] Authors = { "Diana", "Petya", "Stella", "Elena", "Katya", "Iva", "Annie", "Eva" };

    private readonly string[] Cities = { "Burgas", "Sofia", "Plovdiv", "Varna", "Ruse" };

    public string GetAdvertisement()
    {
        Random rnd = new Random();

        int index = rnd.Next(Phrases.Length);
        string advertisement = Phrases[index];

        index = rnd.Next(Events.Length);
        advertisement += $" {Events[index]}";

        index = rnd.Next(Authors.Length);
        advertisement += $" {Authors[index]}";

        index = rnd.Next(Cities.Length);
        advertisement += $" {Cities[index]}";

        return advertisement;
    }
}
