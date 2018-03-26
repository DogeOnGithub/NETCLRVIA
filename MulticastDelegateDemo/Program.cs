using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MulticastDelegateDemo
{
    class Program
    {
        public delegate string GetStatus();

        static void Main(string[] args)
        {
            GetStatus getStatus = null;

            getStatus += new GetStatus(new Light().Switch);
            getStatus += new GetStatus(new Light().Switch);
            getStatus += new GetStatus(new Fan().Speed);
            getStatus += new GetStatus(new Fan().Speed);

            /* 委托类型的Invoke方法包含了对数组中的所有项进行遍历的代码
             * 这是一个很简单的算法，尽管这个简单的算法足以应付很多情形，但是也有局限
             * 这个简单的算法只是顺序调用链中的每一个委托，所以一个委托对象出现问题，链中的所有后续对象都无法调用
             * 因此，MulticastDelegate类提供了一个实例方法GetInvocationList，用于显式调用链中每一个委托
             */

            Delegate[] delegates = getStatus.GetInvocationList();
            foreach (GetStatus status in delegates)
            {
                try
                {
                    Console.WriteLine(status());
                }
                catch (InvalidOperationException e)
                {
                    Console.WriteLine(e.Message);
                    Console.WriteLine(status.Target);
                }
            }

            Console.ReadKey();
        }
    }

    class Light
    {
        public string Switch()
        {
            return "Light Switch";
        }
    }

    class Fan
    {
        public string Speed()
        {
            throw new InvalidOperationException("Fan Broken");
        }
    }
}
