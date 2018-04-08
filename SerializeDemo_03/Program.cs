using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Permissions;

namespace SerializeDemo_03
{
    class Program
    {
        static void Main(string[] args)
        {
            Test();
            Console.ReadKey();
        }

        static void Test()
        {
            Singleton[] singletons = { Singleton.GetInstance(), Singleton.GetInstance() };
            Console.WriteLine(singletons[0] == singletons[1]);//True

            using (var stream = new MemoryStream())
            {
                BinaryFormatter formatter = new BinaryFormatter();
                formatter.Serialize(stream, singletons);

                stream.Position = 0;

                Singleton[] singletons2 = (Singleton[])formatter.Deserialize(stream);

                Console.WriteLine(singletons2[0] == singletons[1]);//True
            }
        }
    }

    [Serializable]
    class Singleton : ISerializable
    {
        private static readonly Singleton _instance = new Singleton();

        public string name = "TjSanshao";
        public DateTime dateTime = DateTime.Now;

        private Singleton()
        {

        }

        public static Singleton GetInstance()
        {
            return _instance;
        }

        //序列化时调用的方法
        [SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.SetType(typeof(SingletionSerializationHelper));
        }

        [Serializable]
        private sealed class SingletionSerializationHelper : IObjectReference
        {
            //这个方法在对象发序列化后调用
            public object GetRealObject(StreamingContext context)
            {
                return Singleton.GetInstance();
            }
        }
    }
}
