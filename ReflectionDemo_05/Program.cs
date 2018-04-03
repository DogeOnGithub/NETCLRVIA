using System;
using System.Collections.Generic;
using System.Reflection;

namespace ReflectionDemo_05
{
    class Program
    {
        private const BindingFlags flags = BindingFlags.FlattenHierarchy | BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic;

        static void Main(string[] args)
        {
            Show("Before doing anything");

            List<MethodBase> methodBases = new List<MethodBase>();
            //加载所有方法到内存
            foreach (var t in typeof(object).Assembly.GetExportedTypes())
            {
                if (t.IsGenericTypeDefinition)
                {
                    continue;
                }

                MethodBase[] methods = t.GetMethods();
                methodBases.AddRange(methods);
            }

            Console.WriteLine("# of methods = {0:N0}", methodBases.Count);
            Show("After building cache of MethodInfo objects");
            //将所有方法转成运行时句柄
            List<RuntimeMethodHandle> runtimeMethodHandles = methodBases.ConvertAll<RuntimeMethodHandle>(m => m.MethodHandle);

            Show("Holding MethodInfo and RuntimeMethodHandle");

            GC.KeepAlive(methodBases);

            methodBases = null;

            GC.Collect();

            Show("After freeing MethodInfo objects");

            methodBases = runtimeMethodHandles.ConvertAll(rm => MethodBase.GetMethodFromHandle(rm));

            Show("Recreated MethodInfo objects");

            GC.KeepAlive(runtimeMethodHandles);
            GC.KeepAlive(methodBases);

            runtimeMethodHandles = null;
            methodBases = null;

            Show("After freeing MethodInfos and Handles");

            Console.ReadKey();
        }
        /// <summary>
        /// 显示即时内存信息
        /// </summary>
        /// <param name="str">需要显示的额外信息</param>
        static void Show(string str)
        {
            Console.WriteLine("Heap size = {0, 12:N0} - {1}", GC.GetTotalMemory(true), str);
        }
    }
}
