using BorderControl.Core.Interfaces;
using BorderControl.IO.Interfaces;
using BorderControl.Models;
using BorderControl.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace BorderControl.Core
{
    public class Engine : IEngine
    {
        private readonly IReader reader;
        private readonly IWriter writer;

        public Engine(IReader reader, IWriter writer)
        {
            this.reader = reader;
            this.writer = writer;
        }

        public void Run()
        {
            List<IIdentifiable> identifiables = new List<IIdentifiable>();

            string command;
            while ((command = reader.ReadLine()) != "End")
            {
                string[] commandArgs = command.Split(' ', StringSplitOptions.RemoveEmptyEntries);

                IIdentifiable identifiable;
                if (commandArgs.Length == 3)
                {
                    string name = commandArgs[0];
                    int age = int.Parse(commandArgs[1]);
                    string id = commandArgs[2];

                    identifiable = new Citizen(name, age, id);
                }
                else
                {
                    string model = commandArgs[0];
                    string id = commandArgs[1];

                    identifiable = new Robot(model, id);
                }

                identifiables.Add(identifiable);
            }

            string fakeDigits = reader.ReadLine();

            foreach (var identifiable in identifiables.Where(iden => iden.ID.EndsWith(fakeDigits)))
            {
                writer.WriteLine(identifiable.ID);
            }
        }
    }
}
