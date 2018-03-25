using System;

namespace StackallocDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            StackallocDemo();
            InlineArrayDemo();
            Console.ReadKey();
        }

        static void StackallocDemo()
        {
            unsafe
            {
                const int width = 20;
                char* ch = stackalloc char[width];//在栈上分配数组
                string str = "TjSanshao";
                for (int index = 0; index < width; index++)
                {
                    ch[width - index - 1] = (index < str.Length) ? str[index] : '.';
                }
                Console.WriteLine(new string(ch, 0, width));
            }
        }

        static void InlineArrayDemo()
        {
            unsafe
            {
                CharArray charArray;//在栈上分配数组
                int widthInBytes = sizeof(CharArray);
                int width = widthInBytes / 2;
                string str = "TjSanshao";
                for (int index = 0; index < width; index++)
                {
                    charArray.Characters[width - index - 1] = (index < str.Length) ? str[index] : '.';
                }
                Console.WriteLine(new string(charArray.Characters, 0, width));
            }
        }
    }

    unsafe struct CharArray
    {
        /// <summary>
        /// 数组内联到结构中
        /// 通常，由于数组是引用类型，所以结构中定义的数组字段实际只是指向数组的指针或引用，数组本身在结构的内存的外部
        /// 在结构中嵌入数组需满足以下几个条件：
        /// 1.类型必须是结构（值类型），不能在类（引用类型）中嵌入数组
        /// 2.字段或其定义结构必须用unsafe关键字标记
        /// 3.数组字段必须用fixed关键字标记
        /// 4.数组必须是一维0基数组
        /// 5.数组的元素必须是以下类型之一：Boolean、Char、SByte、Byte、Int16、Int32、UInt16、UInt32、Int64、Single、Double
        /// </summary>
        public fixed Char Characters[20];
    }
}
