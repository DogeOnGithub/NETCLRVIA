using System;
using System.Reflection;

namespace ReflectionDemo_03
{
    class Program
    {
        static void Main(string[] args)
        {
            Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();
            foreach (var assembly in assemblies)
            {
                Console.WriteLine("Assembly:" + assembly.FullName);
                foreach (var type in assembly.DefinedTypes)
                {
                    Console.WriteLine("Type:" + type.FullName);
                    foreach (var member in type.GetMembers())
                    {
                        Console.WriteLine("Member:" + member.Name);
                    }
                }
            }
            Console.ReadKey();
        }
    }
}
