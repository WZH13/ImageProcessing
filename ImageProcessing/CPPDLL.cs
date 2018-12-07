using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace ImageProcessing
{
    public class CPPDLL
    {
        [DllImport("CSharpInvokeCPP.dll")]
        public static extern int ThinnerHilditch(byte[] img, int imgwidth, int imgheight);
    }
}
