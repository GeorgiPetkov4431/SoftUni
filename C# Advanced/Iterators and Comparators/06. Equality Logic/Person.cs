using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _06._Equality_Logic
{
    public class Person : IComparable<Person>
    {
        public Person(string name, int age)
        {
            this.Name = name;
            this.Age = age;
        }

        public string Name { get; set; }
        public int Age { get; set; }

        public override int GetHashCode()
        {
            return HashCode.Combine(Name.ToLower(), Age);
        }

        public override bool Equals(object obj)
        {
            Person other = (Person)obj;

            return Name.ToLower() == other.Name.ToLower() && Age == other.Age;
        }

        public int CompareTo(Person other)
        {
            int result = this.Name.CompareTo(other.Name);
            if (result == 0)
            {
                result = this.Age.CompareTo(other.Age);
            }

            return result;
        }
    }
}
