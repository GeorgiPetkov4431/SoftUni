using CommandPattern.Core.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CommandPattern.Core
{
    public class CommandInterpreter : ICommandInterpreter
    {
        public string Read(string args)
        {
            string[] argsArray = args.Split(' ', StringSplitOptions.RemoveEmptyEntries);

            string commandName = argsArray[0];

            string[] commandArgs = argsArray.Skip(1).ToArray();

            Type assembly = Assembly.GetEntryAssembly()
                .GetTypes()
                .FirstOrDefault(t => t.Name == $"{commandName}Command");

            ICommand command = Activator.CreateInstance(assembly) as ICommand;

            string result = command.Execute(commandArgs);

            return result;
        }
    }
}
