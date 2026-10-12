using System.ComponentModel.DataAnnotations;
using static EventManager.Common.EntityValidation.Category;

namespace EventManager.Data.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MinLength(NameMinLength)]
        [MaxLength(NameMaxLength)]
        public string Name { get; set; } = null!;

        public virtual ICollection<Event> Events { get; set; }
            = new HashSet<Event>();
    }
}
