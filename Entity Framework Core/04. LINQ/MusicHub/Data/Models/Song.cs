using MusicHub.Data.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicHub.Data.Models
{
    public class Song
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        /* TimeSpan can be converted to long (Ticks) or string (invariant culture) DB Type */
        /* TimeSpan is a required type by default */
        public TimeSpan Duration { get; set; }

        /* DateOnly is converted to DATE SQL Type */
        /* DateOnly is a required type by default */
        public DateTime CreatedOn { get; set; }

        /* Enumeration is converted to INT SQL Type (SQLServer) */
        /* PostgreSQL offer enum DB Type with custom ValueConverter in EF Core */
        /* Since Enumerations are numeric values, they are required types by default */
        public Genre Genre { get; set; }

        public int? AlbumId { get; set; }

        public virtual Album? Album { get; set; }

        public int WriterId { get; set; }

        public virtual Writer Writer { get; set; } = null!;

        public decimal Price { get; set; }

        public virtual ICollection<SongPerformer> SongPerformers { get; set; }
            = new HashSet<SongPerformer>();
    }
}
