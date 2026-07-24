using System;
using System.Collections.Generic;

namespace SoftUni.Models
{
    public partial class Address
    {
        public int AddressId { get; set; }
        
        public string AddressText { get; set; } = null!;
        
        public int? TownId { get; set; }

        /* Tip: Navigation properties and collctions should be virtual. */
        /* Tip: Nullable type of FK should match the nullable reference type of navigation property. */
        public virtual Town? Town { get; set; }
        
        /* Tip: Inline initialization is preferred for virtual navigation collections! */
        public virtual ICollection<Employee> Employees { get; set; } = new HashSet<Employee>();
    }
}
