using System;
using System.Linq;
using System.Reflection;

namespace ReflectionDemo_01
{
    class Program
    {
        static void Main(string[] args)
        {
            Assembly exeAssembly = Assembly.GetExecutingAssembly();
            foreach (var c in exeAssembly.DefinedTypes)
            {
                Console.WriteLine(c.FullName);
                Console.WriteLine(c.DeclaredMethods.FirstOrDefault());
                Console.WriteLine("--------------------------------------");
            }
            Console.ReadKey();
        }
    }

    public class Person
    {

    }
}
