using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TaskDemo_01
{
    class Program
    {
        static void Main(string[] args)
        {
            ThreadPool.QueueUserWorkItem(Print, "TjSanshao");

            Task task = new Task(Print, "TjSanshao");
            task.Start();

            Task.Run(() => Print("TjSanshao"));

            //该类继承自Task，泛型参数指的是返回类型
            Task<int> t = new Task<int>((n) => Count((int)n), 1000);
            t.Start();
            //调用Result时，内部会使用Task.Wait()方法
            Console.WriteLine(t.Result);

            Console.ReadKey();
        }

        static void Print(object name)
        {
            Console.WriteLine("Name : " + name);
        }

        static int Count(int n)
        {
            Thread.Sleep(3000);
            return n + 1000;
        }
    }
}
