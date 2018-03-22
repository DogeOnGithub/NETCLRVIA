using System;

namespace PropDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            BitArray bitArray = new BitArray(14);
            for (int i = 0; i < 14; i++)
            {
                bitArray[i] = true;
            }
            for (int i = 0; i < 14; i++)
            {
                Console.WriteLine("bitArray[" + i + "] is " + bitArray[i]);
            }
            Console.ReadKey();
        }
    }

    class BitArray
    {
        private byte[] byteArray;
        private int bits;

        public BitArray(int bits)
        {
            if (bits < 0)
            {
                throw new ArgumentOutOfRangeException("bits must be > 0");
            }

            this.bits = bits;
            byteArray = new byte[(this.bits + 7) / 8];
        }

        public bool this[int pos]
        {
            set
            {
                if (pos >= this.bits || pos < 0)
                {
                    throw new ArgumentOutOfRangeException("index must be < bits");
                }

                if (value)
                {
                    this.byteArray[pos / 8] = (byte)((this.byteArray[pos / 8]) | (1 << (pos % 8)));
                }
                else
                {
                    this.byteArray[pos / 8] = (byte)((this.byteArray[pos / 8]) & ~(1 << (pos % 8)));
                }
            }

            get
            {
                if (pos >= this.bits || pos < 0)
                {
                    throw new ArgumentOutOfRangeException("index must be < bits");
                }

                return (this.byteArray[pos / 8] & (1 << (pos % 8))) != 0;
            }
        }
    }
}
