using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static EventManager.Common.EntityValidation.Registration;

namespace EventManager.Data.Models
{
    public class Registration
    {
        [Key]
        public Guid Id { get; set; }

        [ForeignKey(nameof(Event))]
        public int EventId { get; set; }

        public virtual Event Event { get; set; } = null!;

        [Required]
        [MinLength(ParticipantNameMinLength)]
        [MaxLength(ParticipantNameMaxLength)]
        public string ParticipantName { get; set; } = null!;

        [Required]
        [MinLength(EmailMinLength)]
        [MaxLength(EmailMaxLength)]
        public string Email { get; set; } = null!;
    }
}
