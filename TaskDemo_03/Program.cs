using System;
using System.Threading.Tasks;

namespace TaskDemo_03
{
    class Program
    {
        static void Main(string[] args)
        {
            Task task1 = Task.Run(() => Console.WriteLine("TjSanshao"));
            Task task2 = task1.ContinueWith((t) => Console.WriteLine("Tj2" + t.Id));
            Task.WaitAll();
            Console.ReadKey();
        }
    }
}
