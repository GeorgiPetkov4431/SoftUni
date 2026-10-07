using BookShelf.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookShelf.Data.Configuration
{
    public class AuthorEntityConfiguration : IEntityTypeConfiguration<Author>
    {
        public readonly IEnumerable<Author> SeedAuthors = new List<Author>
        {
            new Author
            {
                Id = 1,
                Name = "George Orwell",
                Country = "United Kingdom"
            },
            new Author
            {
                Id = 2,
                Name = "Jane Austen",
                Country = "United Kingdom"
            },
            new Author
            {
                Id = 3,
                Name = "Mark Twain",
                Country = "United States"
            },
            new Author
            {
                Id = 4,
                Name = "Fyodor Dostoevsky",
                Country = "Russia"
            },
            new Author
            {
                Id = 5,
                Name = "Ernest Hemingway",
                Country = "United States"
            }
        };

        public void Configure(EntityTypeBuilder<Author> builder)
        {
            builder.HasData(SeedAuthors);
        }
    }
}
