using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlagsDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            Actions action = Actions.RW;
            Console.WriteLine(action.ToString());
            action = Actions.Read | Actions.Delete;
            Console.WriteLine(action);
            Console.ReadKey();
        }

        [Flags]
        enum Actions
        {
            None = 0,
            Read = 0x0001,
            Write = 0x0002,
            RW = Actions.Read | Actions.Write,
            Delete = 0x0004
        }
    }
}
