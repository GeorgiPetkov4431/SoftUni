using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P01_StudentSystem.Data.Models
{
    public class Student
    {
        public int StudentId { get; set; }

        public string Name { get; set; }

        public string? PhoneNumber { get; set; }

        public DateTime RegisteredOn { get; set; }

        public DateTime? Birthday { get; set; }

        public virtual ICollection<Homework> Homeworks { get; set; }
            = new HashSet<Homework>();

        public virtual ICollection<StudentCourse> StudentsCourses { get; set; }
            = new HashSet<StudentCourse>();
    }
}
