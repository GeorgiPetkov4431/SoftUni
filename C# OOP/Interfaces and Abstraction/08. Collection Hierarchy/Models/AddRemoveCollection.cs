using CollectionHierarchy.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollectionHierarchy.Models
{
    public class AddRemoveCollection : IAddRemoveCollection
    {
        private const int StartIndex = 0;

        private readonly List<string> data;

        public AddRemoveCollection()
        {
            this.data = new List<string>();
        }

        public int Add(string item)
        {
            this.data.Insert(StartIndex, item);

            return StartIndex;
        }

        public string Remove()
        {
            string item = null;

            if (data.Count > 0)
            {
                item = this.data[data.Count - 1];
                this.data.RemoveAt(data.Count - 1);
            }

            return item;
        }
    }
}
