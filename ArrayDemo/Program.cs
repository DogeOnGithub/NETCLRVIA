using System;

namespace ArrayDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] lowerBounds = { 2005, 1 };
            int[] lengths = { 5, 4 };

            //利用Array类型的静态方法，可以创建下标起始不为0的数组
            decimal[,] quarterlyRevenue = (decimal[,])Array.CreateInstance(typeof(decimal), lengths, lowerBounds);

            Console.WriteLine("{0,4}{1,9}{2,9}{3,9}{4,9}", "Year", "Q1", "Q2", "Q3", "Q4");

            int firstYear = quarterlyRevenue.GetLowerBound(0);
            int lastYear = quarterlyRevenue.GetUpperBound(0);
            int firstQ = quarterlyRevenue.GetLowerBound(1);
            int lastQ = quarterlyRevenue.GetUpperBound(1);

            for(int year = firstYear; year <= lastYear; year++)
            {
                Console.Write(year + " ");
                for(int quarter = firstQ; quarter <= lastQ; quarter++)
                {
                    Console.Write("{0, 9:C}", quarterlyRevenue[year, quarter]);
                }
                Console.WriteLine();
            }

            Console.ReadKey();
        }
    }
}
