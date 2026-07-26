using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
//using System.ComponentModel.DataAnnotations.Schema;

namespace P02_FootballBetting.Data.Models
{
    public class Team
    {
        [Key]
        public int TeamId { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; } = null!;

        [MaxLength(2048)]
        public string? LogoUrl { get; set; }

        [Required]
        // [Column(TypeName = "CHAR(3)")]
        [MaxLength(4)]
        [Comment("These are the 3 or 4 character initials of the Team")] // This comment will also be visible in the database schema
        // This comment is visible only in the source code
        public string Initials { get; set; } = null!;

        [Required] // not strictly required, as decimal types are non-nullable by default
        [Column(TypeName = "DECIMAL(12, 3)")]
        public decimal Budget { get; set; }

        [ForeignKey(nameof(PrimaryKitColor))]
        public int PrimaryKitColorId { get; set; }

        public virtual Color PrimaryKitColor { get; set; } = null!;

        [ForeignKey(nameof(SecondaryKitColor))]
        public int SecondaryKitColorId { get; set; }

        public virtual Color SecondaryKitColor { get; set; } = null!;

        [ForeignKey(nameof(Town))]
        public int TownId { get; set; }

        public virtual Town Town { get; set; } = null!;

        /* If we define relations in FluentAPI, InverseProperty attribute can be omitted */
        [InverseProperty(nameof(Game.HomeTeam))]
        public virtual ICollection<Game> HomeGames { get; set; }
            = new HashSet<Game>();

        [InverseProperty(nameof(Game.AwayTeam))]
        public virtual ICollection<Game> AwayGames { get; set; }
            = new HashSet<Game>();

        public virtual ICollection<Player> Players { get; set; }
            = new HashSet<Player>();
    }
}
