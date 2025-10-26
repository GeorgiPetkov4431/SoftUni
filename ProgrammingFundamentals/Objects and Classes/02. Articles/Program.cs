using System;

namespace _02._Articles;

internal class Program
{
    static void Main(string[] args)
    {
        string[] articleDetails = Console.ReadLine().Split(", ");

        Article article = new Article(articleDetails[0], articleDetails[1], articleDetails[2]);

        int commandNum = int.Parse(Console.ReadLine());

        for (int i = 0; i < commandNum; i++)
        {
            string[] command = Console.ReadLine().Split(": ");

            switch (command[0])
            {
                case "Edit":
                    article.Edit(command[1]);
                    break;
                case "ChangeAuthor":
                    article.ChangeAuthor(command[1]);
                    break;
                case "Rename":
                    article.Rename(command[1]);
                    break;
            }
        }

        Console.WriteLine(article.GetArticle());
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

    public void Edit(string content)
    {
        this.Content = content;
    }

    public void ChangeAuthor(string author)
    {
        this.Author = author;
    }

    public void Rename(string title)
    {
        this.Title = title;
    }

    public string GetArticle()
    {
        return $"{this.Title} - {this.Content}: {this.Author}";
    }
}
