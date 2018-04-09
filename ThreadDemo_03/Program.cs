using System;
using System.Runtime.Remoting.Messaging;
using System.Threading;

namespace ThreadDemo_03
{
    class Program
    {
        static void Main(string[] args)
        {
            //Main线程的逻辑调用上下文
            CallContext.LogicalSetData("Name", "TjSanshao");

            ThreadPool.QueueUserWorkItem((state) => Console.WriteLine("Name = {0}", CallContext.LogicalGetData("Name")));

            //停止线程间执行上下文的流动
            ExecutionContext.SuppressFlow();

            ThreadPool.QueueUserWorkItem((state) => Console.WriteLine("Name = {0}", CallContext.LogicalGetData("Name")));

            //恢复上下文的流动
            ExecutionContext.RestoreFlow();

            Console.ReadKey();
        }
    }
}
