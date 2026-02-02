using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DefiningClasses
{
    public class Family
    {
        private List<Person> familyMembers = new List<Person>();
        public Family()
        {
            this.familyMembers = new();
        }

        public List<Person> FamilyMembers
        {
            get { return familyMembers; }
            set { familyMembers = value; }
        }


        public void AddMember(Person person)
        {
            familyMembers.Add(person);
        }

        public Person GetOldestMember()
        {
            Person oldestMember = familyMembers.MaxBy(x => x.Age);
            return oldestMember;
        }
    }
}
