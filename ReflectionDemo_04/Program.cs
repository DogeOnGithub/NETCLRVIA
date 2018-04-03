using System;
using System.Linq;
using System.Reflection;

namespace ReflectionDemo_04
{
    class Program
    {
        static void Main(string[] args)
        {
            BindToMemberThenInvokeTheMember(typeof(Person));
            Console.ReadKey();
            BindToMemberCreateDelegateToMemberThenInvokeTheMember(typeof(Person));
            Console.ReadKey();
            UseDynamicToBindInvokeTheMember(typeof(Person));
            Console.ReadKey();
        }

        static void EventCallback(object o, EventArgs e)
        {
            Console.WriteLine("Callback");
        }

        static void BindToMemberThenInvokeTheMember(Type type)
        {
            Console.WriteLine("BindToMemberThenInvokeTheMember");

            ConstructorInfo constructorInfo = type.GetTypeInfo().DeclaredConstructors.First(c => c.GetParameters()[0].ParameterType == typeof(Int32) && c.GetParameters()[1].ParameterType == typeof(String));
            object[] args = new object[] { 22, "TjSanshao" };
            object obj = constructorInfo.Invoke(args);
            Console.WriteLine(obj.GetType());
            Console.WriteLine(((Person)obj).Name);

            Console.WriteLine("已创建对象______________________________________");

            FieldInfo fieldInfo = obj.GetType().GetTypeInfo().GetDeclaredField("public_age");
            Console.WriteLine("FieldInfo: " + fieldInfo.Name + " , " + fieldInfo.GetValue(obj));
            Console.WriteLine("_______________________________________________");

            MethodInfo methodInfo = obj.GetType().GetTypeInfo().GetDeclaredMethod("ToString");
            Console.WriteLine("MethodInfo: " + methodInfo.Name + " , " + methodInfo.Invoke(obj, null));
            Console.WriteLine("_______________________________________________");

            PropertyInfo propertyInfo = obj.GetType().GetTypeInfo().GetProperty("Name");
            try
            {
                propertyInfo.SetValue(obj, "TjSanshaoChange");
            }
            catch (TargetInvocationException)
            {

                throw;
            }
            Console.WriteLine("PropertyInfo: " + propertyInfo.Name + " , " + propertyInfo.GetValue(obj));
            Console.WriteLine("_______________________________________________");

            EventInfo eventInfo = obj.GetType().GetTypeInfo().GetDeclaredEvent("PersonEvent");
            eventInfo.AddEventHandler(obj, new EventHandler(EventCallback));
            ((Person)obj).OnEvent();
            Console.WriteLine("_______________________________________________");
        }

        static void BindToMemberCreateDelegateToMemberThenInvokeTheMember(Type type)
        {
            Console.WriteLine("BindToMemberCreateDelegateToMemberThenInvokeTheMember");

            object[] args = new object[] { 22, "TjSanshao" };
            object obj = Activator.CreateInstance(type, args);
            if (obj is Person person)
            {
                Console.WriteLine("已创建对象______________________________________");
            }

            MethodInfo methodInfo = obj.GetType().GetTypeInfo().GetDeclaredMethod("ToString");
            var toString = methodInfo.CreateDelegate(typeof(Func<string>), obj);
            Console.WriteLine(toString.DynamicInvoke(null));
            Console.WriteLine("_______________________________________________");

            PropertyInfo propertyInfo = obj.GetType().GetTypeInfo().GetDeclaredProperty("Name");
            var getName = propertyInfo.SetMethod.CreateDelegate(typeof(Action<string>), obj);
            getName.DynamicInvoke("TjSanshaoChange");
            Console.WriteLine(((Person)obj).Name);
            Console.WriteLine("_______________________________________________");

            EventInfo eventInfo = obj.GetType().GetTypeInfo().GetDeclaredEvent("PersonEvent");
            var setEvent = eventInfo.AddMethod.CreateDelegate(typeof(Action<EventHandler>), obj);
            setEvent.DynamicInvoke(new EventHandler(EventCallback));
            ((Person)obj).OnEvent();
            Console.WriteLine("_______________________________________________");
        }

        static void UseDynamicToBindInvokeTheMember(Type type)
        {
            Console.WriteLine("UseDynamicToBindInvokeTheMember");

            object[] args = new object[] { 22, "TjSanshao" };
            dynamic obj = Activator.CreateInstance(type, args);

            //dynamic类型的obj可以像普通对象一样调用方法，但是VS没有提示
            Console.WriteLine(obj.GetType());
        }
    }

    class Person
    {
        private int age;

        public string Name { get; set; }

        public Person(int age, string name)
        {
            this.age = age;
            this.Name = name;
            this.public_age = age;
        }

        public override string ToString()
        {
            return base.ToString();
        }

        public void OnEvent()
        {
            this.PersonEvent.Invoke(new object(), new EventArgs());
        }

        public event EventHandler PersonEvent;

        public int public_age;
    }
}
