using CollectionHierarchy.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollectionHierarchy.Models
{
    public class AddCollection : IAddCollection
    {
        private readonly List<string> data;

        public AddCollection()
        {
            this.data = new List<string>();
        }

        public int Add(string item)
        {
            this.data.Add(item);

            return this.data.Count - 1;
        }
    }
}
