using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingSpree.Models
{
    public class Person
    {
        private string name;
        private decimal money;
        private List<Product> products;

        public Person(string name, decimal money)
        {
            this.Name = name;
            this.Money = money;

            products = new List<Product>();
        }

        public string Name
        {
            get { return name; }
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException(ExceptionMessages.NameEmpty);
                }

                name = value;
            }
        }

        public decimal Money
        {
            get { return money; }
            private set
            {
                if (value < 0)
                {
                    throw new ArgumentException(ExceptionMessages.MoneyNegative);
                }

                money = value;
            }
        }

        public string Add(Product newProduct)
        {
            if (Money < newProduct.Cost)
            {
                return $"{this.Name} can't afford {newProduct.Name}";
            }

            products.Add(newProduct);

            this.Money -= newProduct.Cost;

            return $"{this.Name} bought {newProduct.Name}";
        }

        public override string ToString()
        {
            //string productsString;

            /*if (products.Any())
            {
                productsString = string.Join(", ", products.Select(p => p.Name));
            }
            else
            {
                productsString = "Nothing bought";
            }*/

            string productsString = products.Any() ? string.Join(", ", products.Select(p => p.Name)) : "Nothing bought";

            return $"{this.Name} - {productsString}";
        }
    }
}
