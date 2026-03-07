using CollectionHierarchy.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollectionHierarchy.Models
{
    public class MyList : IMyList
    {
        private const int StartIndex = 0;

        private readonly List<string> data;

        public MyList()
        {
            this.data = new List<string>();
        }

        public int Used
        {
            get { return this.data.Count; }
        }

        public int Add(string item)
        {
            this.data.Insert(StartIndex, item);

            return StartIndex;
        }

        public string Remove()
        {
            string item = null;

            if (this.data.Count > 0)
            {
                item = this.data[StartIndex];
                this.data.RemoveAt(StartIndex);
            }

            return item;
        }
    }
}
