using System;
using System.Reflection;
using System.Runtime.Remoting;
using System.Threading;

namespace CrossAppDomain
{
    class Program
    {
        static void Main(string[] args)
        {
            Marshaling();
            Console.ReadKey();
        }

        static void Marshaling()
        {
            //获取当前线程所在的域
            AppDomain adCallingThreadDomain = Thread.GetDomain();

            string callingDomainName = adCallingThreadDomain.FriendlyName;
            Console.WriteLine("Default AppDomain's FriendlyName = {0}", callingDomainName);

            //获取Main方法所在的程序集
            string exeAssembly = Assembly.GetEntryAssembly().FullName;
            Console.WriteLine("Main assembly = {0}", exeAssembly);

            AppDomain appDomain2 = null;

            Console.WriteLine("{0} Demo #1", Environment.NewLine);
            appDomain2 = AppDomain.CreateDomain("appDomain2", null, null);
            MarshalByRefType marshalByRefType = null;
            marshalByRefType = (MarshalByRefType)appDomain2.CreateInstanceAndUnwrap(exeAssembly, typeof(MarshalByRefType).ToString());
            Console.WriteLine("Type = {0}", marshalByRefType.GetType());
            Console.WriteLine("Is Proxy = {0}", RemotingServices.IsTransparentProxy(marshalByRefType));
            //看起来像是MarshalByRefType上调用一个方法，实则不是
            //这是在代理类型调用一个方法，代理使线程切换到拥有对象的AppDomain，并在真实的对象上调用这个方法
            marshalByRefType.SomeMethod();
            AppDomain.Unload(appDomain2);
            //在卸载了域之后，试图调用方法，会抛出异常
            try
            {
                marshalByRefType.SomeMethod();
                Console.WriteLine("Call Success");
            }
            catch (AppDomainUnloadedException)
            {
                Console.WriteLine("Call Failed");                
            }

            Console.WriteLine("{0} Demo #2", Environment.NewLine);
            appDomain2 = AppDomain.CreateDomain("appDomain2", null, null);
            marshalByRefType = (MarshalByRefType)appDomain2.CreateInstanceAndUnwrap(exeAssembly, typeof(MarshalByRefType).ToString());
            MarshalByValType marshalByValType = marshalByRefType.MethodWithReturn();
            Console.WriteLine("Is Proxy = {0}", RemotingServices.IsTransparentProxy(marshalByValType));
            //在该域下的对象中调用方法
            Console.WriteLine("Returned object created " + marshalByValType.ToString());
            AppDomain.Unload(appDomain2);
            try
            {
                Console.WriteLine("Returned object created " + marshalByValType.ToString());
                Console.WriteLine("Call Success");
            }
            catch (AppDomainUnloadedException)
            {
                Console.WriteLine("Call Failed");
            }

            Console.WriteLine("{0} Demo #3", Environment.NewLine);
            appDomain2 = AppDomain.CreateDomain("appDomain2", null, null);
            marshalByRefType = (MarshalByRefType)appDomain2.CreateInstanceAndUnwrap(exeAssembly, typeof(MarshalByRefType).ToString());
            NonMarshalableType nonMarshalableType = marshalByRefType.MethodArgAndReturn(callingDomainName);
            Console.WriteLine("Is Proxy = {0}", RemotingServices.IsTransparentProxy(nonMarshalableType));
        }
    }

    /// <summary>
    /// 该类的实例可以跨越AppDomain的边界按引用封送
    /// </summary>
    public class MarshalByRefType : MarshalByRefObject
    {
        public MarshalByRefType()
        {
            Console.WriteLine("{0} ctor running in {1}", this.GetType().ToString(), Thread.GetDomain().FriendlyName);
        }

        public void SomeMethod()
        {
            Console.WriteLine("Executing in " + Thread.GetDomain().FriendlyName);
        }

        public MarshalByValType MethodWithReturn()
        {
            Console.WriteLine("Executing in " + Thread.GetDomain().FriendlyName);
            MarshalByValType t = new MarshalByValType();
            Thread.Sleep(3000);
            return t;
        }

        public NonMarshalableType MethodArgAndReturn(string callingDomainName)
        {
            Console.WriteLine("Calling from '{0}' to '{1}'", callingDomainName, Thread.GetDomain().FriendlyName);
            NonMarshalableType t = new NonMarshalableType();
            return t;
        }
    }

    /// <summary>
    /// 该类的实例可以跨越AppDomain的边界按值封送
    /// </summary>
    [Serializable]
    public class MarshalByValType : Object
    {
        private DateTime m_createionTime = DateTime.Now;

        public MarshalByValType()
        {
            Console.WriteLine("{0} ctor running in {1}, Create on {2:D}", this.GetType().ToString(), Thread.GetDomain().FriendlyName, m_createionTime);
        }

        public override string ToString()
        {
            return m_createionTime.ToLongTimeString();
        }
    }

    /// <summary>
    /// 该类的实例不可以跨越AppDomain边界进行封送
    /// </summary>
    [Serializable]
    public class NonMarshalableType : Object
    {
        public NonMarshalableType()
        {
            Console.WriteLine("Executing in " + Thread.GetDomain().FriendlyName);
        }
    }
}