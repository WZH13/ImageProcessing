using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test
{
    class Program
    {
        public static void Main()
        {
            double x = 1.0;
            double y = 2.0;
            double angle;
            double radians;
            double result;

            // Calculate the tangent of 30 degrees.
            angle = 30;
            radians = angle * (Math.PI / 180);
            result = Math.Tan(radians);
            Console.WriteLine("The tangent of 30 degrees is {0}.", result);

            // Calculate the arctangent of the previous tangent.
            radians = Math.Atan(result);
            angle = radians * (180 / Math.PI);
            Console.WriteLine("The previous tangent is equivalent to {0} degrees.", angle);

            // Calculate the arctangent of an angle.
            String line1 = "{0}The arctangent of the angle formed by the x-axis and ";
            String line2 = "a vector to point ({0},{1}) is {2}, ";
            String line3 = "which is equivalent to {0} degrees.";

            radians = Math.Atan2(y, x);
            angle = radians * (180 / Math.PI);

            Console.WriteLine(line1, Environment.NewLine);
            Console.WriteLine(line2, x, y, radians);
            Console.WriteLine(line3, angle);




            int i = 1;
            long lg = 1;
            Console.WriteLine("0x{0:x}", i << 1);
            Console.WriteLine("0x{0:x}", i << 33);
            Console.WriteLine("0x{0:x}", lg << 33);

            int j = -30;
            Console.WriteLine(j >> 3);




            Console.ReadKey();
        }
    }
}
