using System;
using System.Collections.Generic;
using System.Linq;

namespace PokemonTrainer
{
    public class StartUp
    {
        public static void Main(string[] args)
        {
            List<Trainer> trainers = new List<Trainer>();

            string command;

            while ((command = Console.ReadLine()) != "Tournament")
            {
                string[] commandArgs = command.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                Pokemon pokemon = new Pokemon(commandArgs[1], commandArgs[2], int.Parse(commandArgs[3]));

                Trainer trainer = trainers.FirstOrDefault(t => t.Name == commandArgs[0]);
                if (trainer == null)
                {
                    trainer = new Trainer(commandArgs[0]);
                    trainers.Add(trainer);
                }

                trainer.Pokemon.Add(pokemon);
            }

            //string[] elements = { "Fire", "Water", "Electricity" };

            while ((command = Console.ReadLine()) != "End")
            {
                //if (!elements.Contains(command)) continue;
                foreach (var trainer in trainers)
                {
                    trainer.CheckPokemon(command);
                }
            }

            trainers = trainers.OrderByDescending(t => t.BadgeNumber).ToList();

            Console.WriteLine(string.Join('\n', trainers));
        }
    }
}
