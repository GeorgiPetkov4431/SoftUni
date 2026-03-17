using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AuthorProblem
{
    public class Tracker
    {
        public void PrintMethodsByAuthor()
        {
            // Type[] allTypes = Assembly.GetExecutingAssembly().GetTypes();
            Type[] allTypes = typeof(Tracker).Assembly.GetTypes();

            foreach (Type type in allTypes)
            {
                MethodInfo[] methods =
                    type.GetMethods(BindingFlags.Public
                    | BindingFlags.NonPublic
                    | BindingFlags.Instance
                    | BindingFlags.Static);

                foreach (MethodInfo method in methods)
                {
                    string[] authorAttribute = method.GetCustomAttributes<AuthorAttribute>()
                        .Where(attr => attr != null)
                        .Select(attr => attr.Name)
                        .ToArray();

                    if (authorAttribute.Any()) Console.WriteLine($"{method.Name} is written by {string.Join(", ", authorAttribute)}");
                }
            }
        }
    }
}
