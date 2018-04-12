using System;
using System.Linq;
using System.Reflection;

namespace PLINQDemo_01
{
    class Program
    {
        static void Main(string[] args)
        {
            foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                ObsoleteMethods(assembly);
            }
            Console.ReadKey();
        }

        static void ObsoleteMethods(Assembly assembly)
        {
            var query = from type in assembly.GetExportedTypes().AsParallel()
                        from method in type.GetMethods(BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static)
                        let obsoleteAttrType = typeof(ObsoleteAttribute)
                        where Attribute.IsDefined(method, obsoleteAttrType)
                        orderby type.FullName
                        let obsoleteAttrObj = (ObsoleteAttribute)Attribute.GetCustomAttribute(method, obsoleteAttrType)
                        select String.Format("Type = {0}\nMethod = {1}\nMessage = {2}\n", type.FullName, method.ToString(), obsoleteAttrObj.Message);

            foreach (var result in query)
            {
                Console.WriteLine(result);
            }
        }
    }
}
