using GameZone.ViewModels.Genre;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel.DataAnnotations;
using static GameZone.Common.ValidationConstants;

namespace GameZone.ViewModels.Game
{
    public class GameFormModel
    {
        /* Input Model -> Data flow is from UNTRUSTED Client App to TRUSTED Server App */
        /* We MUST perform Model Validation to transform UNTRUSTED context to TRUSTED context */

        /* Inputs (UNTRUSTED -> TRUSTED) -> Model Validation required */
        [Required]
        [StringLength(GameTitleMaxLength, MinimumLength = GameTitleMinLength)]
        public string Title { get; set; } = null!;

        [MaxLength(GameImageUrlMaxLength)]
        public string? ImageUrl { get; set; }

        [Required]
        [StringLength(GameDescriptionMaxLength, MinimumLength = GameDescriptionMinLength)]
        public string Description { get; set; } = null!;

        [Required]
        [StringLength(GamePublisherMaxLength, MinimumLength = GamePublisherMinLength)]
        public string PublisherName { get; set; } = null!;

        public DateTime ReleasedOn { get; set; }

        public int GenreId { get; set; }
        /* Inputs (UNTRUSTED -> TRUSTED) -> Model Validation required */

        /* Outputs (TRUSTED -> UNTRUSTED) -> Model Validation NOT required */
        [BindNever]
        public IEnumerable<GenreDropdownViewModel> Genres { get; set; }
            = new List<GenreDropdownViewModel>();
        /* Outputs (TRUSTED -> UNTRUSTED) -> Model Validation NOT required */
    }
}
