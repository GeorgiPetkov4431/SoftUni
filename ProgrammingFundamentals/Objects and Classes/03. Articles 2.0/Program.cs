using System;
using System.Collections.Generic;

namespace _03._Articles_2._0;

internal class Program
{
    static void Main(string[] args)
    {
        List<Article> articles = new();

        int articleNum = int.Parse(Console.ReadLine());

        for (int i = 0; i < articleNum; i++)
        {
            string[] articleDetails = Console.ReadLine().Split(", ");
            articles.Add(new Article(articleDetails[0], articleDetails[1], articleDetails[2]));
        }

        Console.WriteLine(string.Join("\n", articles));
    }
}

class Article
{
    public Article(string title, string content, string author)
    {
        this.Title = title;
        this.Content = content;
        this.Author = author;
    }

    private string Title { get; set; }
    private string Content { get; set; }
    private string Author { get; set; }

    public override string ToString()
    {
        return $"{this.Title} - {this.Content}: {this.Author}";
    }
}
