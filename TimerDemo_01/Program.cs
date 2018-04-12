using System;
using System.Threading;
using System.Threading.Tasks;

namespace TimerDemo_01
{
    class Program
    {
        private static Timer timer;

        static void Main(string[] args)
        {
            Console.WriteLine("Checking");

            //创建一个不启动的计时器，确保私有成员timer在线程池线程调用Status方法之前引用该计时器
            timer = new Timer(Status, null, Timeout.Infinite, Timeout.Infinite);

            timer.Change(0, Timeout.Infinite);

            Status();

            Console.ReadKey();
        }

        static void Status(object state)
        {
            Console.WriteLine("In status");
            Thread.Sleep(1000);
            timer.Change(2000, Timeout.Infinite);
        }

        static async void Status()
        {
            while (true)
            {
                Console.WriteLine("Checking time = {0}", DateTime.Now);
                //await后面的代码会由一个新的线程继续执行，Delay表示2000ms后返回一个任务，await表示等待返回
                await Task.Delay(2000);
            }
        }
    }
}
