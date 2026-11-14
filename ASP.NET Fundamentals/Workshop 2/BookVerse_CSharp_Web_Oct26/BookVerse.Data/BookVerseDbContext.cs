using BookVerse.DataModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BookVerse.Data
{
    public class BookVerseDbContext : IdentityDbContext<IdentityUser>
    {
        public BookVerseDbContext(DbContextOptions<BookVerseDbContext> options)
            : base(options)
        {

        }

        public virtual DbSet<Book> Books { get; set; } = null!;

        public virtual DbSet<Genre> Genres { get; set; } = null!;

        public virtual DbSet<UserBook> UsersBooks { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfigurationsFromAssembly(typeof(BookVerseDbContext).Assembly);
        }
    }
}
