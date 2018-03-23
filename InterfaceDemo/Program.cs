using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterfaceDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            IPerson person = new Person();
            person.Print();
            Console.ReadKey();

            Person person2 = new Person();
            person2.Print();
            Console.ReadKey();
        }
    }

    interface IPerson
    {
        void Print();
    }

    class Person : IPerson
    {
        void IPerson.Print()
        {
            Console.WriteLine("Private IPerson");
        }

        public void Print()
        {
            Console.WriteLine("Public Person");
        }
    }
}
