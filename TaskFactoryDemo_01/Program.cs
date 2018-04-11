using System;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TaskFactoryDemo_01
{
    class Program
    {
        static void Main(string[] args)
        {
            Task parent = new Task(() =>
            {
                var cts = new CancellationTokenSource();
                var tf = new TaskFactory<int>(cts.Token, TaskCreationOptions.AttachedToParent, TaskContinuationOptions.ExecuteSynchronously, TaskScheduler.Default);

                //parent任务使用任务工厂启动3个子任务
                var childtasks = new[]
                {
                    tf.StartNew(() => { Thread.Sleep(1000);return 10000; }),
                    tf.StartNew(() => { Thread.Sleep(1000);return 10001; }),
                    tf.StartNew(() => { Thread.Sleep(1000);return 10002; }),
                };

                //如果有一个子任务出现异常，就取消其他子任务
                for (int i = 0; i < childtasks.Length; i++)
                {
                    childtasks[i].ContinueWith((t) => cts.Cancel(), TaskContinuationOptions.OnlyOnFaulted);
                }

                //当所有任务顺利完成，获取返回值最大的任务，并将这个返回值最大的任务开启一个新的任务来显示最大值
                tf.ContinueWhenAll(childtasks, completedTasks => completedTasks.Where(t => t.IsCompleted && !t.IsCanceled && !t.IsFaulted).Max(t => t.Result), CancellationToken.None)
                .ContinueWith(t => Console.WriteLine("The Max is {0}", t.Result), TaskContinuationOptions.ExecuteSynchronously);
            });

            //当parent任务出错时，运行该延续任务，输出异常
            parent.ContinueWith(pTask =>
            {
                StringBuilder stringBuilder = new StringBuilder("The following exceptions occurred:" + Environment.NewLine);
                foreach (var e in pTask.Exception.Flatten().InnerExceptions)
                {
                    stringBuilder.Append(" " + e.GetType().ToString());
                }
                Console.WriteLine(stringBuilder.ToString());
            }, TaskContinuationOptions.OnlyOnFaulted);

            parent.Start();

            Console.ReadKey();
        }
    }
}
