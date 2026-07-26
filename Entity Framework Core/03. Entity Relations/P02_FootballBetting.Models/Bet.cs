using P02_FootballBetting.Data.Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P02_FootballBetting.Data.Models
{
    public class Bet
    {
        [Key]
        public int BetId { get; set; }

        [Column(TypeName = "DECIMAL(8, 2)")]
        public decimal Amount { get; set; }

        /* Enumerations are required by default */
        /* SQL Server maps enums to INT by default */
        /* If you wish to map enums to text or another type => use value converter */
        public Prediction Prediction { get; set; }

        [Column(TypeName = "DATETIME2")]
        public DateTime DateTime { get; set; }

        [ForeignKey(nameof(User))]
        public int UserId { get; set; }

        public virtual User User { get; set; } = null!;

        [ForeignKey(nameof(Game))]
        public int GameId { get; set; }

        public virtual Game Game { get; set; } = null!;
    }
}
