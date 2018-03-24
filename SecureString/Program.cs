using System;
using System.Runtime.InteropServices;
using System.Security;

namespace SecureStringDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            using (SecureString secureString = new SecureString())
            {
                Console.WriteLine("Please enter password:");
                while (true)
                {
                    ConsoleKeyInfo info = Console.ReadKey(true);
                    if (info.Key == ConsoleKey.Enter)
                    {
                        break;
                    }
                    else
                    {
                        secureString.AppendChar(info.KeyChar);
                        Console.Write("*");
                    }
                }
                Console.WriteLine();
                DisplaySecureString(secureString);
            }
            Console.ReadKey();
        }

        unsafe static void DisplaySecureString(SecureString secureString)
        {
            char* ch = null;
            try
            {
                ch = (char*)Marshal.SecureStringToCoTaskMemUnicode(secureString);
                for (int i = 0; ch[i] != 0; i++)
                {
                    Console.WriteLine(ch[i]);
                }
            }
            finally
            {
                if (ch != null)
                {
                    Marshal.ZeroFreeCoTaskMemUnicode((IntPtr)ch);
                }
            }
        }
    }
}
