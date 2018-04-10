using System;
using System.Threading;

namespace ThreadDemo_04
{
    class Program
    {
        static void Main(string[] args)
        {
            CancellationDemo.Go();
        }
    }

    class CancellationDemo
    {
        public static void Go()
        {
            CancellationTokenSource cts = new CancellationTokenSource();

            cts.Token.Register(() => Console.WriteLine("TjSanshao"));

            ThreadPool.QueueUserWorkItem((o) => Count(cts.Token, 1000));

            Console.ReadKey();

            cts.Cancel();

            Console.ReadKey();
        }

        private static void Count(CancellationToken token, int countTo)
        {
            for (int i = 0; i < countTo; i++)
            {
                if (token.IsCancellationRequested)
                {
                    Console.WriteLine("Count is canceled");
                    break;
                }
                Console.WriteLine(i);
                Thread.Sleep(1000);
            }

            Console.WriteLine("Count is done");
        }
    }
}
