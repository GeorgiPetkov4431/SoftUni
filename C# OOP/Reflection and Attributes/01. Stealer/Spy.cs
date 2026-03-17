using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Stealer
{
    public class Spy
    {
        public string StealFieldInfo(string className, params string[] fieldNames)
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine($"Class under investigation: {className}");

            Type classType = Type.GetType(className);

            FieldInfo[] fields =
                classType.GetFields(BindingFlags.Instance 
                | BindingFlags.NonPublic
                | BindingFlags.Public)
                .Where(f => fieldNames.Contains(f.Name)).ToArray();

            object instance = Activator.CreateInstance(classType);

            foreach (FieldInfo field in fields)
            {
                object value = field.GetValue(instance);

                sb.AppendLine($"{field.Name} = {value}");
            }

            return sb.ToString();
        }
    }
}
