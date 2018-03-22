using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CastDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            Person person = new Person(22);
            Console.WriteLine(person.Name + person.Age);
            Console.WriteLine(person.ToInt32());

            Console.WriteLine("___________________________");

            Person person2 = 11;
            Person person3 = (Person)"TjSanshao";

            Console.WriteLine(person2.Name + person2.Age);

            Console.WriteLine("___________________________");

            Console.WriteLine(person3.Name + person3.Age);

            Console.ReadKey();
        }
    }

    class Person
    {
        public string Name { get; set; }
        public int Age { get; set; }

        public Person(int age)
        {
            this.Age = age;
            this.Name = "TjSanshao";
        }
        
        public int ToInt32()
        {
            return this.Age;
        }

        public static implicit operator Person(int age)
        {
            return new Person(age);
        }

        public static explicit operator Person(string name)
        {
            return new Person(22);
        }
    }
}
