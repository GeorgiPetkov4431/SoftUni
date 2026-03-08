using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WildFarm.Core.Interfaces;
using WildFarm.Factories.Interfaces;
using WildFarm.IO.Interfaces;
using WildFarm.Models.Interfaces;

namespace WildFarm.Core
{
    public class Engine : IEngine
    {
        private readonly IReader reader;
        private readonly IWriter writer;
        private readonly IFoodFactory foodFactory;
        private readonly IAnimalFactory animalFactory;

        private readonly List<IAnimal> animals;

        public Engine(IReader reader, IWriter writer, IFoodFactory foodFactory, IAnimalFactory animalFactory)
        {
            this.reader = reader;
            this.writer = writer;
            this.foodFactory = foodFactory;
            this.animalFactory = animalFactory;

            animals = new List<IAnimal>();
        }

        public void Run()
        {
            string command;
            while ((command = reader.ReadLine()) != "End")
            {
                try
                {
                    string[] animalInfo = command.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                    IAnimal animal = animalFactory.CreateAnimal(animalInfo);

                    writer.WriteLine(animal.ProduceSound());

                    string[] foodInfo = reader.ReadLine().Split(' ', StringSplitOptions.RemoveEmptyEntries);
                    string foodType = foodInfo[0];
                    int foodQuantity = int.Parse(foodInfo[1]);
                    IFood food = foodFactory.CreateFood(foodType, foodQuantity);

                    this.animals.Add(animal);
                    animal.Eat(food);
                }
                catch (ArgumentException ex)
                {
                    writer.WriteLine(ex.Message);
                }
            }

            foreach (var animal in this.animals)
            {
                writer.WriteLine(animal.ToString());
            }
        }
    }
}
