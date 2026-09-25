using System.ComponentModel.DataAnnotations;
using static MoviesApp.Common.EntityValidation;

namespace MoviesApp.Models
{
    public class Movie
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(MovieTitleMaxLength)]
        public string Title { get; set; } = null!;

        [Required]
        [MaxLength(MovieGenreMaxLength)]
        public string Genre { get; set; } = null!;

        public DateTime ReleaseDate { get; set; }

        [Required]
        [MaxLength(MovieDirectorMaxLength)]
        public string Director { get; set; } = null!;

        public int Duration { get; set; }

        [Required]
        [MaxLength(MovieDescriptionMaxLength)]
        public string Description { get; set; } = null!;

        [MaxLength(MovieImageUrlMaxLength)]
        public string? ImageUrl { get; set; }
    }
}
