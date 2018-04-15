using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test
{
    class Program
    {
        static void Main(string[] args)
        {
            byte[] b = { 4, 46, 58, 127, 239, 255 };
            bool bol = ByteUtil.ByteGetBit(b[1], 3);
            Console.WriteLine(bol);
            Console.ReadKey();
        }
    }
}
