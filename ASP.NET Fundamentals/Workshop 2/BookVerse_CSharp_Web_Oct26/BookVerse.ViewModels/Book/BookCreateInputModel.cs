using BookVerse.ViewModels.Genre;
using System.ComponentModel.DataAnnotations;
using static BookVerse.GCommon.ValidationConstants.Book;

namespace BookVerse.ViewModels.Book
{
    public class BookCreateInputModel
    {
        /* Input Model -> Validation Required */
        [Required]
        [StringLength(TitleMaxLength, MinimumLength = TitleMinLength)]
        public string Title { get; set; } = null!;

        [Required]
        [StringLength(DescriptionMaxLength, MinimumLength = DescriptionMinLength)]
        public string Description { get; set; } = null!;

        [StringLength(CoverImageUrlMaxLength)]
        public string? CoverImageUrl { get; set; }

        public DateTime PublishedOn { get; set; }

        public int GenreId { get; set; }

        /* View Model -> No Validation Required */
        public IEnumerable<GenreViewModel> Genres { get; set; } =
            new List<GenreViewModel>();
    }
}
