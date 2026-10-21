using System.ComponentModel.DataAnnotations;
using static Homies.GCommon.Constants.EntityValidation;

namespace Homies.Data.Models
{
    public class Type
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MinLength(TypeNameMinLength)]
        [MaxLength(TypeNameMaxLength)]
        public string Name { get; set; } = null!;

        public virtual ICollection<Event> Events { get; set; }
            = new List<Event>();
    }
}
