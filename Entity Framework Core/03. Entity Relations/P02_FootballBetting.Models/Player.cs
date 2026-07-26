using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P02_FootballBetting.Data.Models
{
    public class Player
    {
        [Key]
        public int PlayerId { get; set; }

        [Required]
        [MaxLength(200)]
        public string Name { get; set; } = null!;

        public byte SquadNumber { get; set; }

        /* In-memory default value => Generated at persistence through client (C# App) */
        /* Not required, since default value of bool is false */
        /* Default value in DB => Use FluentAPI (HasDefaultValueSQL) */
        public bool IsInjured { get; set; } = false;

        [ForeignKey(nameof(Position))]
        public int PositionId { get; set; }

        public virtual Position Position { get; set; } = null!;

        /* A team can be a free agent (without a team) */
        /* FK Column will be nullable */
        [ForeignKey(nameof(Team))]
        public int TeamId { get; set; }

        public virtual Team Team { get; set; } = null!;

        [ForeignKey(nameof(Town))]
        public int TownId { get; set; }

        public virtual Town Town { get; set; } = null!;

        public virtual ICollection<PlayerStatistic> PlayersStatistics { get; set; }
            = new HashSet<PlayerStatistic>();
    }
}
