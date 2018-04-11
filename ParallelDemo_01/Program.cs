using System;
using System.Threading;
using System.Threading.Tasks;

namespace ParallelDemo_01
{
    class Program
    {
        static void Main(string[] args)
        {
            Parallel.For(0, 1000, i => DoWork(i));

            int[] ints = { 1, 2, 3, 4, 5 };
            Parallel.ForEach(ints, i => DoWork(i));

            Parallel.Invoke(Method1, Method2, Method3);

            Console.ReadKey();
        }

        static void DoWork(int n)
        {
            for (int i = 0; i < n; i++)
            {
                Thread.Sleep(1000);
                Console.WriteLine(i);
            }
        }

        static void Method1()
        {
            Console.WriteLine("Method1");
        }

        static void Method2()
        {
            Console.WriteLine("Method2");
        }

        static void Method3()
        {
            Console.WriteLine("Method3");
        }
    }
}
