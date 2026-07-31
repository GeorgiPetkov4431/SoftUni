namespace BookShop
{
    using BookShop.Models.Enums;
    using Data;
    using Initializer;
    using Microsoft.EntityFrameworkCore;
    using System.Text;

    public class StartUp
    {
        public static void Main()
        {
            using BookShopContext dbContext = new BookShopContext();

            DbInitializer.ResetDatabase(dbContext);

            // Task 2
            //string command = Console.ReadLine().ToLower();
            //string result = GetBooksByAgeRestriction(dbContext, command);

            // Task 3
            //string result = GetGoldenBooks(dbContext);

            // Task 4
            //string result = GetBooksByPrice(dbContext);

            // Task 5
            //int year = int.Parse(Console.ReadLine());
            //string result = GetBooksNotReleasedIn(dbContext, year);

            // Task 6
            //string input = Console.ReadLine();
            //string result = GetBooksByCategory(dbContext, input);

            // Task 7
            //string date = Console.ReadLine();
            //string result = GetBooksReleasedBefore(dbContext, date);

            // Task 8
            //string input = Console.ReadLine();
            //string result = GetAuthorNamesEndingIn(dbContext, input);

            // Task 9
            //string input = Console.ReadLine();
            //string result = GetBookTitlesContaining(dbContext, input);

            // Task 10
            //string input = Console.ReadLine();
            //string result = GetBooksByAuthor(dbContext, input);

            // Task 11
            //int lengthCheck = int.Parse(Console.ReadLine());
            //int result = CountBooks(dbContext, lengthCheck);

            // Task 12
            //string result = CountCopiesByAuthor(dbContext);

            // Task 13
            //string result = GetTotalProfitByCategory(dbContext);

            // Task 14
            //string result = GetMostRecentBooks(dbContext);
            //Console.WriteLine(result);

            // Task 15
            //IncreasePrices(dbContext);

            // Task 16
            int removedBooks = RemoveBooks(dbContext);
            Console.WriteLine(removedBooks);
        }

        public static string GetBooksByAgeRestriction(BookShopContext dbContext, string command)
        {
            StringBuilder sb = new StringBuilder();

            /* This enum variable is essentially a masked integer */
            AgeRestriction? ageRestriction = null;
            if (command != null && Enum.GetValues<AgeRestriction>().Any(ev => ev.ToString().ToLowerInvariant() == command.ToLowerInvariant()))
            {
                ageRestriction = Enum.Parse<AgeRestriction>(command, ignoreCase: true);
            }

            if (!ageRestriction.HasValue)
            {
                // Empty string result
                return string.Empty;
            }

            /* SQL Server does not understand enumerations and their string literals! */
            /* Enumeration is INT for SQL Server => Compare text of enum to string results in inefficient query! */
            /* Efficient Tip 1: Select only columns that are actually required */
            /* Efficient Tip 2: Filter only rows that are actually required */
            /* Efficient Tip 3: Use variables (parameters) for .Where() clause to enable use of cached queries */
            IEnumerable<string> ageRestrictedBooks = dbContext.Books
                .AsEnumerable()
                .Where(b => b.AgeRestriction == ageRestriction)
                .Select(b => b.Title)
                .OrderBy(b => b)
                .ToArray();

            return string.Join(Environment.NewLine, ageRestrictedBooks);
        }

        public static string GetGoldenBooks(BookShopContext context)
        {
            EditionType editionType = Enum.Parse<EditionType>("Gold", false);

            IEnumerable<string> goldenBooks = context.Books
                .Where(b => b.EditionType == editionType && b.Copies < 5000)
                .OrderBy(b => b.BookId)
                .Select(b => b.Title)
                .ToArray();

            return string.Join(Environment.NewLine, goldenBooks);
        }

        public static string GetBooksByPrice(BookShopContext context)
        {
            var booksByPrice = context.Books
                .Where(b => b.Price > 40)
                .OrderByDescending(b => b.Price)
                .Select(b => new
                {
                    b.Title,
                    b.Price
                })
                .ToArray();

            StringBuilder sb = new StringBuilder();

            foreach (var b in booksByPrice)
            {
                sb.AppendLine($"{b.Title} - ${b.Price:F2}");
            }

            return sb.ToString().TrimEnd();
        }

        public static string GetBooksNotReleasedIn(BookShopContext context, int year)
        {
            IEnumerable<string> books = context.Books
                .Where(b => b.ReleaseDate.Value.Year != year)
                .OrderBy(b => b.BookId)
                .Select(b => b.Title)
                .ToArray();

            return string.Join(Environment.NewLine, books);
        }

        public static string GetBooksByCategory(BookShopContext context, string input)
        {
            string[] categories = input
                .Split(' ', StringSplitOptions.RemoveEmptyEntries)
                .Select(c => c.ToLowerInvariant())
                .ToArray();

            IEnumerable<string> bookTitlesFromCategories = context.Books
                .Where(b => b.BookCategories.Any(bc => categories.Contains(bc.Category.Name.ToLower())))
                .Select(b => b.Title)
                .OrderBy(b => b)
                .ToArray();

            return string.Join(Environment.NewLine, bookTitlesFromCategories);
        }

        public static string GetBooksReleasedBefore(BookShopContext context, string date)
        {
            DateTime dateTime = DateTime.ParseExact(date, "dd-MM-yyyy", null);

            var booksReleasedBefore = context.Books
                .Where(b => b.ReleaseDate < dateTime)
                .OrderByDescending(b => b.ReleaseDate)
                .Select(b => new
                {
                    b.Title,
                    b.EditionType,
                    b.Price
                })
                .ToArray();

            StringBuilder sb = new StringBuilder();

            foreach (var b in booksReleasedBefore)
            {
                sb.AppendLine($"{b.Title} - {b.EditionType} - ${b.Price:F2}");
            }

            return sb.ToString().TrimEnd();
        }

        public static string GetAuthorNamesEndingIn(BookShopContext context, string input)
        {
            IEnumerable<string> authorNames = context.Authors
                .Where(a => a.FirstName.EndsWith(input))
                .Select(a => a.FirstName + " " + a.LastName)
                .OrderBy(n => n)
                .ToArray();

            return string.Join(Environment.NewLine, authorNames);
        }

        public static string GetBookTitlesContaining(BookShopContext context, string input)
        {
            IEnumerable<string> bookTitles = context.Books
                .Where(b => b.Title.ToLower().Contains(input.ToLower()))
                .Select(b => b.Title)
                .OrderBy(t => t)
                .ToArray();

            return string.Join(Environment.NewLine, bookTitles);
        }

        public static string GetBooksByAuthor(BookShopContext context, string input)
        {
            var booksByAuthor = context.Books
                .Where(b => b.Author.LastName.ToLower().StartsWith(input.ToLower()))
                .OrderBy(b => b.BookId)
                .Select(b => new
                {
                    b.Title,
                    AuthorName = b.Author.FirstName + " " + b.Author.LastName
                })
                .ToArray();

            StringBuilder sb = new StringBuilder();

            foreach (var b in booksByAuthor)
            {
                sb.AppendLine($"{b.Title} ({b.AuthorName})");
            }

            return sb.ToString().TrimEnd();
        }

        public static int CountBooks(BookShopContext context, int lengthCheck)
        {
            int booksCount = context.Books
                .Count(b => b.Title.Length > lengthCheck);

            return booksCount;
        }

        public static string CountCopiesByAuthor(BookShopContext context)
        {
            StringBuilder sb = new StringBuilder();

            var books = context.Authors
                .Select(a => new
                {
                    FullName = a.FirstName + " " + a.LastName,
                    TotalCopies = a.Books.Sum(b => b.Copies)
                })
                .OrderByDescending(a => a.TotalCopies)
                .ToArray();

            foreach (var b in books)
            {
                sb.AppendLine($"{b.FullName} - {b.TotalCopies}");
            }

            return sb.ToString().TrimEnd();
        }

        public static string GetTotalProfitByCategory(BookShopContext context)
        {
            var categoryProfits = context.Categories
                .Select(c => new
                {
                    c.Name,
                    Profit = c.CategoryBooks.Sum(cb => cb.Book.Copies * cb.Book.Price)
                })
                .OrderByDescending(c => c.Profit)
                .ThenBy(c => c.Name)
                .ToArray();

            StringBuilder sb = new StringBuilder();

            foreach (var c in categoryProfits)
            {
                sb.AppendLine($"{c.Name} ${c.Profit:F2}");
            }

            return sb.ToString().TrimEnd();
        }

        public static string GetMostRecentBooks(BookShopContext context)
        {
            var categoriesInfo = context.Categories
                .Select(c => new
                {
                    CategoryName = c.Name,
                    Books = c.CategoryBooks.Select(cb => new
                    {
                        BookName = cb.Book.Title,
                        ReleaseDate = cb.Book.ReleaseDate
                    })
                    .OrderByDescending(b => b.ReleaseDate)
                    .Take(3)
                    .ToArray()
                })
                .OrderBy(c => c.CategoryName)
                .ToArray();

            StringBuilder sb = new StringBuilder();

            foreach (var category in categoriesInfo)
            {
                sb.AppendLine($"--{category.CategoryName}");

                foreach (var book in category.Books)
                {
                    sb.AppendLine($"{book.BookName} ({book.ReleaseDate.Value.Year})");
                }
            }

            return sb.ToString().TrimEnd();
        }

        public static void IncreasePrices(BookShopContext context)
        {
            var books = context.Books
                .Where(b => b.ReleaseDate.Value.Year < 2010)
                .ToArray();

            foreach (var book in books)
            {
                book.Price += 5;
            }

            context.SaveChanges();
        }

        public static int RemoveBooks(BookShopContext context)
        {
            var booksCategoriesToRemove = context.BooksCategories
                .Where(bc => bc.Book.Copies < 4200)
                .ToArray();

            var booksToRemove = context.Books
                .Where(b => b.Copies < 4200)
                .ToArray();

            int removedBooks = booksToRemove.Count();

            context.BooksCategories.RemoveRange(booksCategoriesToRemove);
            context.Books.RemoveRange(booksToRemove);
            context.SaveChanges();

            return removedBooks;
        }
    }
}


