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

            //int[,] array = new int[9, 3];
            //array[0, 1] = 1;
            //{ 4, 5, 6 }, { 7, 8, 9 }, { 7, 8, 9 } } ;//定义一个4行3列的二维数组 
            //int row = array.Rank; // 获取行数 
            //int col = array.GetLength(1);//获取指定维中的元 个数，这里也就是列数了。（1表示的是第二维，0是第一维） 
            //col = array.GetUpperBound(0) + 1;//获取指定维度的上限，在 上一个1就是列数 
            //int num = array.Length;//获取整个二维数组的长度，即所有元 的个数
            //row = array.GetLength(0); // 返回第一维的长度（所谓的“行数”）
            //col = array.GetLength(1); // 返回第二维的长度（所谓的“列数”）

            string[,] a = new string[9, 3];
a[0, 0] = "a";
a[2, 0] = "b";
a[4, 0] = "c";
a[6, 0] = "d";
a[8, 0] = "e";

var arr = GetHasValueRowIndex(a);
Console.WriteLine("HasValue Row Count:" + arr.Count);
arr.ForEach(o => Console.WriteLine(o));

            Console.ReadKey();
        }

        public static List<int> GetHasValueRowIndex(string[,] arr)
        {
            var hasValueRowIndex = new List<int>();
            for (var i = 0; i < arr.GetLength(0); i++)
            {
                for (var j = 0; j < arr.GetLength(1); j++)
                {
                    if (!string.IsNullOrEmpty(arr[i, j]))
                    {
                        hasValueRowIndex.Add(i);
                        break;
                    }
                }
            }
            return hasValueRowIndex;
        }


    }

}
