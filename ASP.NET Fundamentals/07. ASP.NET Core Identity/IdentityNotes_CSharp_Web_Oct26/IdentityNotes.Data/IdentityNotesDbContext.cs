using IdentityNotes.Data.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace IdentityNotes.Data
{
    public class IdentityNotesDbContext : IdentityDbContext
    {
        public IdentityNotesDbContext(DbContextOptions<IdentityNotesDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Note> Notes { get; set; } = null!;
    }
}
