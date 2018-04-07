using System;
using System.IO;
using System.Reflection;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Permissions;

namespace SerializeDemo_02
{
    class Program
    {
        static void Main(string[] args)
        {
            MemoryStream memoryStream = new MemoryStream();
            BinaryFormatter binaryFormatter = new BinaryFormatter();

            Programmer programmer = new Programmer();

            binaryFormatter.Serialize(memoryStream, programmer);

            memoryStream.Position = 0;

            Programmer programmer2 = (Programmer)binaryFormatter.Deserialize(memoryStream);

            Console.WriteLine(programmer2.ToString());

            Console.ReadKey();
        }
    }

    [Serializable]
    class Person
    {
        protected string name = "TjSanshao";

        public Person()
        {

        }
    }

    /// <summary>
    /// 通过实现ISerializable进行序列化
    /// </summary>
    [Serializable]
    class Programmer : Person, ISerializable
    {
        private DateTime dateTime = DateTime.Now;

        public Programmer()
        {

        }

        /// <summary>
        /// 如果这个构造器不存在，会引发SerializationException异常
        /// 如果这个不是密封类，访问权限应该是protected
        /// </summary>
        /// <param name="info"></param>
        /// <param name="context"></param>
        [SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
        protected Programmer(SerializationInfo info, StreamingContext context)
        {
            Type baseType = this.GetType().BaseType;
            MemberInfo[] infos = FormatterServices.GetSerializableMembers(baseType, context);

            for (int i = 0; i < infos.Length; i++)
            {
                FieldInfo fieldInfo = (FieldInfo)infos[i];
                fieldInfo.SetValue(this, info.GetValue(baseType.FullName + "+" + fieldInfo.Name, fieldInfo.FieldType));
            }

            this.dateTime = info.GetDateTime("dateTime");
        }

        /// <summary>
        /// 如果基类没有实现ISerializable，那么就要手动添加基类需要序列化的字段到SerializationInfo
        /// 如果实现，则可以直接base.GetObjectData()
        /// </summary>
        /// <param name="info"></param>
        /// <param name="context"></param>
        [SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
        public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("dateTime", this.dateTime);

            Type baseType = this.GetType().BaseType;
            MemberInfo[] infos = FormatterServices.GetSerializableMembers(baseType, context);

            for (int i = 0; i < infos.Length; i++)
            {
                info.AddValue(baseType.FullName + "+" + infos[i].Name, ((FieldInfo)infos[i]).GetValue(this));
            }
        }

        public override string ToString()
        {
            return string.Format("name = {0}, DateTime = {1}", name, dateTime);
        }
    }
}
