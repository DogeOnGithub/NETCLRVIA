using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ThreadDemo_02
{
    class Program
    {
        static void Main(string[] args)
        {
            ThreadPool.QueueUserWorkItem(Func, "TjSanshao");
            Task task = new Task(() => Console.WriteLine("Task"));
            task.Start();
            Thread.Sleep(1000);
            Console.ReadKey();
        }

        static void Func(Object state)
        {
            Console.WriteLine("Func:" + state);
        }
    }
}
