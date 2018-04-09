using System;
using System.Threading;

namespace ThreadDemo_01
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Main Start");

            Thread thread = new Thread(ThreadFunc);
            thread.Start("TjSansaho");

            Thread.Sleep(1000);

            thread.Join();

            Console.WriteLine("Press Any Key...");
            Console.ReadKey();
        }

        static void ThreadFunc(Object state)
        {
            Console.WriteLine("In ThreadFunc, State = {0}", state);
            Thread.Sleep(3000);
        }
    }
}
