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
                var value = field.GetValue(instance);

                sb.AppendLine($"{field.Name} = {value}");
            }

            return sb.ToString();
        }

        public string AnalyzeAccessModifiers(string className)
        {
            StringBuilder sb = new StringBuilder();

            Type classType = Type.GetType(className);

            FieldInfo[] fields = classType.GetFields(BindingFlags.Instance | BindingFlags.Public);

            foreach (FieldInfo field in fields)
            {
                sb.AppendLine($"{field.Name} must be private!");
            }

            PropertyInfo[] properties =
                classType.GetProperties(BindingFlags.Instance 
                | BindingFlags.Public 
                | BindingFlags.NonPublic);

            foreach (PropertyInfo property in properties)
            {
                MethodInfo getMethod = property.GetGetMethod(true);

                if (getMethod != null && !getMethod.IsPublic)
                {
                    sb.AppendLine($"{getMethod.Name} have to be public!");
                }

                MethodInfo setMethod = property.GetSetMethod();

                if (setMethod != null && !setMethod.IsPrivate)
                {
                    sb.AppendLine($"{setMethod.Name} have to be private!");
                }
            }

            return sb.ToString();
        }

        public string RevealPrivateMethods(string className)
        {
            StringBuilder sb = new StringBuilder();

            Type classType = Type.GetType(className);

            sb.AppendLine($"All Private Methods of Class: {className}");
            sb.AppendLine($"Base Class: {classType.BaseType.Name}");

            MethodInfo[] methods =
                classType.GetMethods(BindingFlags.Instance
                | BindingFlags.Static
                | BindingFlags.NonPublic);

            foreach (MethodInfo method in methods)
            {
                sb.AppendLine(method.Name);
            }

            return sb.ToString();
        }

        public string CollectGettersAndSetters(string className)
        {
            StringBuilder sb = new StringBuilder();

            Type classType = Type.GetType(className);

            MethodInfo[] methods =
                classType.GetMethods(BindingFlags.Instance
                | BindingFlags.Static
                | BindingFlags.Public
                | BindingFlags.NonPublic);

            foreach (MethodInfo method in methods)
            {
                if (method.Name.StartsWith("get")) 
                    sb.AppendLine($"{method.Name} will return {method.ReturnType}");
            }

            foreach (MethodInfo method in methods)
            {
                if (method.Name.StartsWith("set"))
                    sb.AppendLine($"{method.Name} will set field of {method.ReturnType}");
            }

            return sb.ToString();
        }
    }
}
