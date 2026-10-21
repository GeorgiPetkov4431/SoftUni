using System.ComponentModel.DataAnnotations;
using static Homies.GCommon.Constants.EntityValidation;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace Homies.Data.Models
{
    public class Event
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MinLength(EventNameMinLength)]
        [MaxLength(EventNameMaxLength)]
        public string Name { get; set; } = null!;

        [Required]
        [MinLength(EventDescriptionMinLength)]
        [MaxLength(EventDescriptionMaxLength)]
        public string Description { get; set; } = null!;

        [Required]
        [ForeignKey(nameof(Organizer))]
        public string OrganizerId { get; set; } = null!;

        public virtual IdentityUser Organizer { get; set; } = null!;

        [Column(TypeName = EventDateTimeSqlType)]
        public DateTime CreatedOn { get; set; }
        
        [Column(TypeName = EventDateTimeSqlType)]
        public DateTime Start { get; set; }

        [Column(TypeName = EventDateTimeSqlType)]
        public DateTime End { get; set; }

        [ForeignKey(nameof(Type))]
        public int TypeId { get; set; }

        public virtual Type Type { get; set; } = null!;

        public virtual ICollection<EventParticipant> Participants { get; set; }
            = new List<EventParticipant>();
    }
}
