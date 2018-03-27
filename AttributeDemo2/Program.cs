using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

[assembly: CLSCompliant(true)]

namespace AttributeDemo2
{ 
    [Serializable]
    [DebuggerDisplay("sanshao")]
    class Program
    {
        [CLSCompliant(true)]
        [STAThread]
        static void Main(string[] args)
        {
            ShowAttributes(typeof(Program));
            Console.ReadKey();

            var members = from m in typeof(Program).GetTypeInfo().DeclaredMembers.OfType<MethodBase>()
                          where m.IsPublic
                          select m;

            foreach (MemberInfo member in members)
            {
                ShowAttributes(member);
            }
            Console.ReadKey();
        }

        [Conditional("Debug")]
        public void DoSomething()
        {

        }

        static void ShowAttributes(MemberInfo attributeTarget)
        {
            var attributes = attributeTarget.GetCustomAttributes<Attribute>();
            foreach (var attribute in attributes)
            {
                Console.WriteLine(attribute.GetType().ToString());
                if (attribute is DefaultMemberAttribute)
                {
                    Console.WriteLine(((DefaultMemberAttribute)attribute).MemberName);
                }
                if (attribute is ConditionalAttribute)
                {
                    Console.WriteLine(((ConditionalAttribute)attribute).ConditionString);
                }
                if (attribute is CLSCompliantAttribute)
                {
                    Console.WriteLine(((CLSCompliantAttribute)attribute).IsCompliant);
                }
            }
        }
    }
}
