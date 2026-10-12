using EventManager.ViewModels.Category;
using System.ComponentModel.DataAnnotations;
using static EventManager.Common.EntityValidation.Event;

namespace EventManager.ViewModels.Events
{
    public class EventFormModel : IValidatableObject
    {
        /* Model Validation is required in the InputModel!!!
         * Data Direction is from UNTRUSTED Client to TRUSTED Server, so validation is needed before publishing data to server.
         */

        /* Data Direction Client -> Server Properties Start */
        [Required(ErrorMessage = "Event title is required.")]
        [StringLength(TitleMaxLength, MinimumLength = TitleMinLength, ErrorMessage = "Event title must be between {2} and {1} characters long.")]
        public string Title { get; set; } = null!;

        [StringLength(DescriptionMaxLength, ErrorMessage = "Event description cannot exceed {1} characters.")]
        public string? Description { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        [Range(MaxParticipantsMinValue, MaxParticipantsMaxValue, ErrorMessage = "Max participants must be between {1} and {2}.")]
        public int MaxParticipants { get; set; }

        public int CategoryId { get; set; }
        /* Data Direction Client -> Server Properties End */

        /* Data Direction Server -> Client Properties Start */
        public IEnumerable<CategoryDropdownViewModel> AllCategories { get; set; }
            = new List<CategoryDropdownViewModel>();
        /* Data Direction Server -> Client Properties End */

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (StartDate > EndDate)
            {
                yield return new ValidationResult(
                    "Start date must be earlier than end date.",
                    new[] { nameof(StartDate), nameof(EndDate) });
            }
        }
    }
}
