using GameZone.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static GameZone.Common.ValidationConstants;

namespace GameZone.Data.Models
{
    public class Game
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MinLength(GameTitleMinLength)]
        [MaxLength(GameTitleMaxLength)]
        public string Title { get; set; } = string.Empty;

        [Required]
        [MinLength(GameDescriptionMinLength)]
        [MaxLength(GameDescriptionMaxLength)]
        public string Description { get; set; } = string.Empty;

        [MaxLength(GameImageUrlMaxLength)]
        public string? ImageUrl { get; set; }

        [Required]
        [MinLength(GamePublisherMinLength)]
        [MaxLength(GamePublisherMaxLength)]
        public string PublisherName { get; set; } = string.Empty;

        [Required]
        [Column(TypeName = "datetime2")]
        public DateTime ReleasedOn { get; set; }

        [Required]
        public int GenreId { get; set; }

        public virtual Genre Genre { get; set; } = null!;
    }
}
