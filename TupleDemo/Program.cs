using System;

namespace TupleDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            Tuple<int, int> tuple = Min(10, 20);
            Console.WriteLine(tuple.Item1);
            Console.WriteLine(tuple.Item2);
            Console.ReadKey();
        }

        static Tuple<int, int> Min(int a, int b)
        {
            return new Tuple<int, int>(Math.Min(a, b), Math.Max(a, b));
        }
    }
}
