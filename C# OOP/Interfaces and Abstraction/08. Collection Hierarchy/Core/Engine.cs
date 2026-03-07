using CollectionHierarchy.Core.Interfaces;
using CollectionHierarchy.Models;
using CollectionHierarchy.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollectionHierarchy.Core
{
    public class Engine : IEngine
    {
        public Engine() { }

        public void Run()
        {
            Dictionary<string, List<int>> collections = new Dictionary<string, List<int>>()
            {
                { "AddCollection", new List<int>() },
                { "AddRemoveCollection", new List<int>() },
                { "MyList", new List<int>() }
            };

            Dictionary<string, List<string>> removedItems = new Dictionary<string, List<string>>()
            {
                { "AddRemoveCollection", new List<string>() },
                { "MyList", new List<string>() }
            };

            IAddCollection addCollection = new AddCollection();
            IAddRemoveCollection addRemoveCollection = new AddRemoveCollection();
            IMyList myList = new MyList();

            string[] elements = Console.ReadLine().Split(' ', StringSplitOptions.RemoveEmptyEntries);
            int numOfRemoves = int.Parse(Console.ReadLine());

            foreach (string element in elements)
            {
                collections["AddCollection"].Add(addCollection.Add(element));
                collections["AddRemoveCollection"].Add(addRemoveCollection.Add(element));
                collections["MyList"].Add(myList.Add(element));
            }

            for (int i = 0; i < numOfRemoves; i++)
            {
                removedItems["AddRemoveCollection"].Add(addRemoveCollection.Remove());
                removedItems["MyList"].Add(myList.Remove());
            }

            foreach (var (key, value) in collections)
            {
                Console.WriteLine(string.Join(' ', value));
            }

            foreach (var (key, value) in removedItems)
            {
                Console.WriteLine(string.Join(' ', value));
            }
        }
    }
}
