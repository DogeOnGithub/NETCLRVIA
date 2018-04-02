using System;
using System.Collections.Generic;

namespace ReflectionDemo_02
{
    class Program
    {
        static void Main(string[] args)
        {
            Type openType = typeof(Dictionary<,>);

            Type closeType = openType.MakeGenericType(typeof(string), typeof(int));

            object obj = Activator.CreateInstance(closeType);

            Console.WriteLine(obj.GetType());

            Console.ReadKey();
        }
    }
}
