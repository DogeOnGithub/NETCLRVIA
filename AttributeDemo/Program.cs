using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AttributeDemo
{
    class Program
    {
        static void Main(string[] args)
        {

        }
    }

    [AttributeUsage(AttributeTargets.All)]
    class SomeAttribute : Attribute
    {
        public SomeAttribute(string name, object obj, Type[] types)
        {

        }
    }

    enum Color
    {
        Red,
        Black
    }

    [Some("TjSanshao", Color.Red, new Type[] { typeof(string), typeof(Color) })]
    class Test
    {

    }
}
