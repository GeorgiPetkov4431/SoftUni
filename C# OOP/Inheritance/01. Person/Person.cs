using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InhertanceExercise
{
    public class Person
    {
        private string name;
        private int age;
        public Person(string  name, int age)
        {
            Name = name;
            Age = age;
        }
        public string Name
        {
            get { return this.name; }
            set { this.name = value; }
        }

        public virtual int Age
        {
            get { return this.age; }
            set
            {
                if (value > 0)
                {
                    this.age = value;
                }
            }
        }

        public override string ToString()
        {
            return $"{GetType().Name} -> Name: {Name}, Age: {Age}";
        }
    }
}
