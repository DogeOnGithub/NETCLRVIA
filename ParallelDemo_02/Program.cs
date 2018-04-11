using System;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace ParallelDemo_02
{
    class Program
    {
        static void Main(string[] args)
        {
            Stopwatch stopwatch = new Stopwatch();

            stopwatch.Start();
            long b = DirectoryBytes(@"E:\MySpeciality", "*.*", SearchOption.AllDirectories);
            stopwatch.Stop();
            Console.WriteLine(stopwatch.Elapsed.Milliseconds);
            Console.WriteLine(b);

            Console.ReadKey();

            stopwatch.Restart();
            long b2 = DirectoryBytesSync(@"E:\MySpeciality", "*.*", SearchOption.AllDirectories);
            stopwatch.Stop();
            Console.WriteLine(stopwatch.Elapsed.Milliseconds);
            Console.WriteLine(b2);

            Console.ReadKey();
        }

        static long DirectoryBytes(string path, string searchPattern, SearchOption searchOption)
        {
            var files = Directory.EnumerateFiles(path, searchPattern, searchOption);

            long masterTotal = 0;

            //该方法接受3个委托，分别是初始化委托，主体委托，主体委托执行后的终结委托
            var result = Parallel.ForEach<string, long>(files,
                //初始化委托
                () => { return 0; },
                //主体委托，其中taskLocalTotal是初始化委托中的返回值
                (file, loopState, index, taskLocalTotal) =>
                {
                    //Console.WriteLine(file);
                    long fileLength = 0;
                    FileStream fileStream = null;
                    try
                    {
                        fileStream = File.OpenRead(file);
                        fileLength = fileStream.Length;
                    }
                    catch (IOException)
                    {
                        //忽略所有无法访问的文件
                        //throw;
                    }
                    finally
                    {
                        if (fileStream != null)
                        {
                            fileStream.Dispose();
                        }
                    }
                    return taskLocalTotal + fileLength;
                },
                //终结委托，其中taskLocalTotal是主体委托执行后的返回值
                taskLocalTotal => { Interlocked.Add(ref masterTotal, taskLocalTotal); }
                );

            return masterTotal;
        }

        static long DirectoryBytesSync(string path, string searchPattern, SearchOption searchOption)
        {
            var files = Directory.EnumerateFiles(path, searchPattern, searchOption);

            long masterTotal = 0;

            foreach (var file in files)
            {
                FileStream fileStream = null;
                try
                {
                    fileStream = File.OpenRead(file);
                    masterTotal += fileStream.Length;
                }
                catch (IOException)
                {

                }
                finally
                {
                    if (fileStream != null)
                    {
                        fileStream.Dispose();
                    }
                }
            }

            return masterTotal;
        }
    }
}
