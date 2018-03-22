using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParticalMethodDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            Person person = new Person { Name = "tj" };
            Console.WriteLine(person.Name);

            person.Name = "tj2";
            Console.WriteLine(person.Name);

            Console.ReadKey();
        }
    }

    /// <summary>
    /// 假设这里是用工具生成的代码
    /// </summary>
    partial class Person
    {
        private string name;

        public string Name
        {
            get { return name; }
            set { OnNameChanging(value.ToUpper()); name = value; }
        }

        partial void OnNameChanging(string value);
    }

    /// <summary>
    /// 假设这里是自己写的代码
    /// </summary>
    partial class Person
    {
        partial void OnNameChanging(string value)
        {
            Console.WriteLine("jjjjjj");
        }
    }
}
