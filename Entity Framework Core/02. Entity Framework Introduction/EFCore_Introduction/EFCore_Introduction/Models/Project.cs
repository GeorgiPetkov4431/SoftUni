using System;
using System.Collections.Generic;

namespace SoftUni.Models
{
    public partial class Project
    {
        public int ProjectId { get; set; }
        
        public string Name { get; set; } = null!;
        
        public string? Description { get; set; }
        
        public DateTime StartDate { get; set; }
        
        public DateTime? EndDate { get; set; }

        /* Skip navigational property -> skip mapping entity */
        //public virtual ICollection<Employee> Employees { get; set; } = new HashSet<Employee>();
    
        public virtual ICollection<EmployeeProject> EmployeesProjects { get; set; } = new HashSet<EmployeeProject>();
    }
}
