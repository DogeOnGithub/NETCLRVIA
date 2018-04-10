using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TaskDemo_02
{
    class Program
    {
        static void Main(string[] args)
        {
            CancellationTokenSource cts = new CancellationTokenSource();
            Task<int> t = new Task<int>(() => Sum(cts, 10000), cts.Token);
            t.Start();

            Console.ReadKey();

            cts.Cancel();
            try
            {
                Console.WriteLine(t.Result);
            }
            catch (AggregateException e)
            {
                e.Handle(ex => ex is OperationCanceledException);
                Console.WriteLine("Sum was Canceled");
            }

            Console.ReadKey();
        }

        static int Sum(CancellationTokenSource cts, int n)
        {
            for (int i = 0; i < 10000; i++)
            {
                cts.Token.ThrowIfCancellationRequested();

                Thread.Sleep(100);

                checked
                {
                    n += i;
                }

                Console.WriteLine(n);
            }

            return n;
        }
    }
}
