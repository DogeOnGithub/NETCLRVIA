using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObjectRef
{
    class Program
    {
        static void Main(string[] args)
        {
            Person p1 = new Person { Name = "TjSanshao" };
            Person p2 = p1;
            Console.WriteLine(p1 == p2);

            Person p3 = new Person { Name = "TjSanshao" };
            Console.WriteLine(p1 == p3);

            Compare(ref p1, ref p2);
            Compare(p1, p2);

            Compare(ref p1, ref p3);
            Compare(p1, p3);
            Compare(ref p3, ref p3);

            Console.ReadKey();
        }

        static void Compare(ref Person p1, ref Person p2)
        {
            Console.WriteLine(p1 == p2);
        }

        static void Compare(Person p1, Person p2)
        {
            Console.WriteLine(p1 == p2);
        }
    }

    class Person
    {
        public string Name { get; set; }
    }
}
