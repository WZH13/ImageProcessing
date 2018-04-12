using System;
using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;

namespace histogram
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        //文件名
        private string curFileName;
        //图像对象
        private Bitmap curBitmap;

        /// <summary>
        /// 打开图像文件
        /// </summary>
        private void open_Click(object sender, EventArgs e)
        {
            //创建OpenFileDialog
            OpenFileDialog opnDlg = new OpenFileDialog();
            //为图像选择一个筛选器
            opnDlg.Filter = "所有图像文件|*.bmp;*.pcx;*.png;*.jpg;*.gif;" +
                "*.tif;*.ico;*.dxf;*.cgm;*.cdr;*.wmf;*.eps;*.emf|" +
                "位图(*.bmp;*.jpg;*.png;...)|*.bmp;*.pcx;*.png;*.jpg;*.gif;*.tif;*.ico|" +
                "矢量图(*.wmf;*.eps;*.emf;...)|*.dxf;*.cgm;*.cdr;*.wmf;*.eps;*.emf";
            //设置对话框标题
            opnDlg.Title = "打开图像文件";
            //启用“帮助”按钮
            opnDlg.ShowHelp = true;

            //如果结果为“打开”，选定文件
            if (opnDlg.ShowDialog() == DialogResult.OK)
            {
                //读取当前选中的文件名
                curFileName = opnDlg.FileName;
                //使用Image.FromFile创建图像对象
                try
                {
                    curBitmap = (Bitmap)Image.FromFile(curFileName);
                }
                catch (Exception exp)
                {
                    MessageBox.Show(exp.Message);
                }
            }
            //对窗体进行重新绘制，这将强制执行paint事件处理程序
            Invalidate();
        }


        /// <summary>
        /// 在控件需要重新绘制时发生
        /// </summary>
        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            //获取Graphics对象
            Graphics g = e.Graphics;
            if (curBitmap != null)
            {
                //使用DrawImage方法绘制图像
                //160,20：显示在主窗体内，图像左上角的坐标
                //curBitmap.Width, curBitmap.Height图像的宽度和高度
                g.DrawImage(curBitmap, 160, 20, curBitmap.Width, curBitmap.Height);
            }
        }


        /// <summary>
        /// 关闭窗体 
        /// </summary>
        private void close_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        /// <summary>
        /// 绘制直方图
        /// </summary>
        private void histogram_Click(object sender, EventArgs e)
        {
            if (curBitmap != null)
            {
                //定义并实例化新窗体，并把图像数据传递给它
                histForm histoGram = new histForm(curBitmap);
                histoGram.ShowDialog();
            }
        }

        #region 线性点运算

        /// <summary>
        /// 线性点运算
        /// </summary>
        private void linearPO_Click(object sender, EventArgs e)
        {
            if (curBitmap!=null)
            {
                //实例化linearPOForm窗体
                linearPOForm linearForm = new linearPOForm();

                //点击确定按钮
                if (linearForm.ShowDialog()==DialogResult.OK)
                {
                    Rectangle rect = new Rectangle(0, 0, curBitmap.Width, curBitmap.Height);
                    BitmapData bmpData = curBitmap.LockBits(rect,ImageLockMode.ReadWrite, curBitmap.PixelFormat);
                    IntPtr ptr = bmpData.Scan0;
                    int bytes = curBitmap.Width * curBitmap.Height;
                    byte[] grayValues = new byte[bytes];
                    Marshal.Copy(ptr, grayValues, 0, bytes);

                    int temp = 0;
                    //得到斜率
                    double a = Convert.ToDouble(linearForm.GetScaling);
                    //得到偏移量
                    double b = Convert.ToDouble(linearForm.GetOffset);

                    for (int i = 0; i < bytes; i++)
                    {
                        //根据公式计算线性点运算
                        //加0.5表示四舍五入
                        temp = (int)(a * grayValues[i] + b + 0.5);

                        //灰度值限制在0~255之间
                        //大于255，则为255；小于0则为0
                        if (temp>255)
                        {
                            grayValues[i] = 255;
                        }
                        else if (temp<0)
                        {
                            grayValues[i] = 0;
                        }
                        else
                        {
                            grayValues[i] = (byte)temp;
                        }
                    }
                    Marshal.Copy(grayValues, 0, ptr, bytes);
                    curBitmap.UnlockBits(bmpData);
                }
                Invalidate();
            }
        }

        #endregion

        #region 全等级灰度拉伸

        /// <summary>
        /// 全等级灰度拉伸
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void stretch_Click(object sender, EventArgs e)
        {
            #region 书上例子：只适用于512*512的24位图像
            //if (curBitmap != null)
            //{
            //    Rectangle rect = new Rectangle(0, 0, curBitmap.Width, curBitmap.Height);
            //    BitmapData bmpData = curBitmap.LockBits(rect,ImageLockMode.ReadWrite, curBitmap.PixelFormat);
            //    IntPtr ptr = bmpData.Scan0;
            //    int bytes = curBitmap.Width * curBitmap.Height;
            //    byte[] grayValues = new byte[bytes];
            //    Marshal.Copy(ptr, grayValues, 0, bytes);

            //    byte a = 255, b = 0;
            //    double p;
            //    //计算最大和最小灰度级
            //    for (int i = 0; i < bytes; i++)
            //    {
            //        //最小灰度级
            //        if (a>grayValues[i])
            //        {
            //            a = grayValues[i];
            //        }
            //        //最大灰度级
            //        if (b<grayValues[i])
            //        {
            //            b = grayValues[i];
            //        }
            //    }

            //    //得到斜率
            //    p = 255.0 / (b - a);

            //    //灰度拉伸
            //    for (int i = 0; i < bytes; i++)
            //    {
            //        grayValues[i] = (byte)(p * (grayValues[i] - a) + 0.5);//加0.5表示四舍五入
            //    }
            //    System.Runtime.InteropServices.Marshal.Copy(grayValues, 0, ptr, bytes);
            //    curBitmap.UnlockBits(bmpData);

            //    Invalidate();  

            //}

            #endregion

            Stretch(curBitmap, out curBitmap);
            Invalidate();
        }
        /// <summary>
        /// 全等级灰度拉伸 （图像增强）
        /// </summary>
        /// <param name="srcBmp">原图像</param>
        /// <param name="dstBmp">处理后图像</param>
        /// <returns>处理成功 true 失败 false</returns>
        public static bool Stretch(Bitmap srcBmp, out Bitmap dstBmp)
        {
            if (srcBmp == null)
            {
                dstBmp = null;
                return false;
            }
            double pR = 0.0;//斜率
            double pG = 0.0;//斜率
            double pB = 0.0;//斜率
            byte minGrayDegreeR = 255;
            byte maxGrayDegreeR = 0;
            byte minGrayDegreeG = 255;
            byte maxGrayDegreeG = 0;
            byte minGrayDegreeB = 255;
            byte maxGrayDegreeB = 0;
            dstBmp = new Bitmap(srcBmp);
            Rectangle rt = new Rectangle(0, 0, dstBmp.Width, dstBmp.Height);
            BitmapData bmpData = dstBmp.LockBits(rt, ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);

            unsafe
            {
                for (int i = 0; i < bmpData.Height; i++)
                {
                    byte* ptr = (byte*)bmpData.Scan0 + i * bmpData.Stride;
                    for (int j = 0; j < bmpData.Width; j++)
                    {
                        if (minGrayDegreeR > *(ptr + j * 3 + 2))
                            minGrayDegreeR = *(ptr + j * 3 + 2);
                        if (maxGrayDegreeR < *(ptr + j * 3 + 2))
                            maxGrayDegreeR = *(ptr + j * 3 + 2);
                        if (minGrayDegreeG > *(ptr + j * 3 + 1))
                            minGrayDegreeG = *(ptr + j * 3 + 1);
                        if (maxGrayDegreeG < *(ptr + j * 3 + 1))
                            maxGrayDegreeG = *(ptr + j * 3 + 1);
                        if (minGrayDegreeB > *(ptr + j * 3))
                            minGrayDegreeB = *(ptr + j * 3);
                        if (maxGrayDegreeB < *(ptr + j * 3))
                            maxGrayDegreeB = *(ptr + j * 3);
                    }
                }
                pR = 255.0 / (maxGrayDegreeR - minGrayDegreeR);
                pG = 255.0 / (maxGrayDegreeG - minGrayDegreeG);
                pB = 255.0 / (maxGrayDegreeB - minGrayDegreeB);
                for (int i = 0; i < bmpData.Height; i++)
                {
                    byte* ptr1 = (byte*)bmpData.Scan0 + i * bmpData.Stride;
                    for (int j = 0; j < bmpData.Width; j++)
                    {
                        *(ptr1 + j * 3) = (byte)((*(ptr1 + j * 3) - minGrayDegreeB) * pB + 0.5);
                        *(ptr1 + j * 3 + 1) = (byte)((*(ptr1 + j * 3 + 1) - minGrayDegreeG) * pG + 0.5);
                        *(ptr1 + j * 3 + 2) = (byte)((*(ptr1 + j * 3 + 2) - minGrayDegreeR) * pR + 0.5);
                    }
                }
            }
            dstBmp.UnlockBits(bmpData);
            return true;
        }

        #endregion

        #region 直方图均衡化

        /// <summary>
        /// 直方图均衡化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void equalization_Click(object sender, EventArgs e)
        {
            #region 书上例子：只适用于512*512的24位图像

            //if (curBitmap != null)
            //{
            //    Rectangle rect = new Rectangle(0, 0, curBitmap.Width, curBitmap.Height);
            //    BitmapData bmpData = curBitmap.LockBits(rect,ImageLockMode.ReadWrite, curBitmap.PixelFormat);
            //    IntPtr ptr = bmpData.Scan0;
            //    int bytes = curBitmap.Width * curBitmap.Height;
            //    byte[] grayValues = new byte[bytes];
            //    Marshal.Copy(ptr, grayValues, 0, bytes);

            //    byte temp;
            //    int[] tempArray = new int[256];
            //    int[] countPixel = new int[256];
            //    byte[] pixelMap = new byte[256];

            //    //计算各灰度级的像素个数
            //    for (int i = 0; i < bytes; i++)
            //    {
            //        //灰度级
            //        temp = grayValues[i];
            //        //计数加1
            //        countPixel[temp]++;
            //    }

            //    //计算各灰度级的累计分布函数
            //    for (int i = 0; i < 256; i++)
            //    {
            //        if (i!=0)
            //        {
            //            tempArray[i] = tempArray[i - 1] + countPixel[i];
            //        }
            //        else
            //        {
            //            tempArray[0] = countPixel[0];
            //        }
            //        //计算累计概率函数，并把值扩展为0~255范围内
            //        pixelMap[i] = (byte)(255.0 * tempArray[i] / bytes + 0.5);
            //    }
            //    //灰度等级映射转换处理
            //    for (int i = 0; i < bytes; i++)
            //    {
            //        temp = grayValues[i];
            //        grayValues[i]= pixelMap[temp];
            //    }

            //    Marshal.Copy(grayValues, 0, ptr, bytes);
            //    curBitmap.UnlockBits(bmpData);

            //    Invalidate();
            //}
            #endregion

            Balance(curBitmap, out curBitmap);
            Invalidate();

        }

        /// <summary>
        /// 直方图均衡化 直方图均衡化就是对图像进行非线性拉伸，重新分配图像像素值，使一定灰度范围内的像素数量大致相同
        /// 增大对比度，从而达到图像增强的目的。是图像处理领域中利用图像直方图对对比度进行调整的方法
        /// </summary>
        /// <param name="srcBmp">原始图像</param>
        /// <param name="dstBmp">处理后图像</param>
        /// <returns>处理成功 true 失败 false</returns>
        public static bool Balance(Bitmap srcBmp, out Bitmap dstBmp)
        {
            if (srcBmp == null)
            {
                dstBmp = null;
                return false;
            }
            int[] histogramArrayR = new int[256];//各个灰度级的像素数R
            int[] histogramArrayG = new int[256];//各个灰度级的像素数G
            int[] histogramArrayB = new int[256];//各个灰度级的像素数B
            int[] tempArrayR = new int[256];
            int[] tempArrayG = new int[256];
            int[] tempArrayB = new int[256];
            byte[] pixelMapR = new byte[256];
            byte[] pixelMapG = new byte[256];
            byte[] pixelMapB = new byte[256];
            dstBmp = new Bitmap(srcBmp);
            Rectangle rt = new Rectangle(0, 0, srcBmp.Width, srcBmp.Height);
            BitmapData bmpData = dstBmp.LockBits(rt, ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);
            unsafe
            {
                //统计各个灰度级的像素个数
                for (int i = 0; i < bmpData.Height; i++)
                {
                    byte* ptr = (byte*)bmpData.Scan0 + i * bmpData.Stride;
                    for (int j = 0; j < bmpData.Width; j++)
                    {
                        histogramArrayB[*(ptr + j * 3)]++;
                        histogramArrayG[*(ptr + j * 3 + 1)]++;
                        histogramArrayR[*(ptr + j * 3 + 2)]++;
                    }
                }
                //计算各个灰度级的累计分布函数
                for (int i = 0; i < 256; i++)
                {
                    if (i != 0)
                    {
                        tempArrayB[i] = tempArrayB[i - 1] + histogramArrayB[i];
                        tempArrayG[i] = tempArrayG[i - 1] + histogramArrayG[i];
                        tempArrayR[i] = tempArrayR[i - 1] + histogramArrayR[i];
                    }
                    else
                    {
                        tempArrayB[0] = histogramArrayB[0];
                        tempArrayG[0] = histogramArrayG[0];
                        tempArrayR[0] = histogramArrayR[0];
                    }
                    //计算累计概率函数，并将值放缩至0~255范围内
                    pixelMapB[i] = (byte)(255.0 * tempArrayB[i] / (bmpData.Width * bmpData.Height) + 0.5);//加0.5为了四舍五入取整
                    pixelMapG[i] = (byte)(255.0 * tempArrayG[i] / (bmpData.Width * bmpData.Height) + 0.5);
                    pixelMapR[i] = (byte)(255.0 * tempArrayR[i] / (bmpData.Width * bmpData.Height) + 0.5);
                }
                //灰度等级映射转换处理
                for (int i = 0; i < bmpData.Height; i++)
                {
                    byte* ptr = (byte*)bmpData.Scan0 + i * bmpData.Stride;
                    for (int j = 0; j < bmpData.Width; j++)
                    {
                        *(ptr + j * 3) = pixelMapB[*(ptr + j * 3)];
                        *(ptr + j * 3 + 1) = pixelMapG[*(ptr + j * 3 + 1)];
                        *(ptr + j * 3 + 2) = pixelMapR[*(ptr + j * 3 + 2)];
                    }
                }
            }
            dstBmp.UnlockBits(bmpData);
            return true;
        }

        #endregion

        #region 直方图匹配

        /// <summary>
        /// 直方图匹配
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void shaping_Click(object sender, EventArgs e)
        {
            #region 
            //if (curBitmap != null)
            //{
            //    //实例化shapingForm窗体
            //    shapinForm sForm = new shapinForm();

            //    if (sForm.ShowDialog() == DialogResult.OK)
            //    {
            //        Rectangle rect = new Rectangle(0, 0, curBitmap.Width, curBitmap.Height);
            //        BitmapData bmpData = curBitmap.LockBits(rect, ImageLockMode.ReadWrite, curBitmap.PixelFormat);
            //        IntPtr ptr = bmpData.Scan0;
            //        int bytes = curBitmap.Width * curBitmap.Height;
            //        byte[] grayValues = new byte[bytes];
            //        Marshal.Copy(ptr, grayValues, 0, bytes);

            //        byte temp = 0;
            //        double[] PPixel = new double[256];
            //        double[] QPixel = new double[256];
            //        int[] qPixel = new int[256];
            //        int[] tempArray = new int[256];
            //        //计算原图像各个灰度级所具有的像素个数
            //        for (int i = 0; i < grayValues.Length; i++)
            //        {
            //            temp = grayValues[i];
            //            qPixel[temp]++;
            //        }

            //        //计算各灰度级的累计分布函数
            //        for (int i = 0; i < 256; i++)
            //        {
            //            if (i != 0)
            //            {
            //                tempArray[i] = tempArray[i - 1] + qPixel[i];
            //            }
            //            else
            //            {
            //                tempArray[0] = qPixel[0];
            //            }
            //            QPixel[i] = (double)tempArray[i] / (double)bytes;
            //        }

            //        //得到被匹配的直方图的累计分布函数
            //        PPixel = sForm.ApplicationP;

            //        double diffA, diffB;
            //        byte k = 0;
            //        byte[] mapPixel = new byte[256];
            //        //直方图匹配
            //        for (int i = 0; i < 256; i++)
            //        {
            //            diffB = 1;
            //            for (int j = 0; j < 256; j++)
            //            {
            //                //找到两个累积分布函数中最相似的位置
            //                diffA = Math.Abs(QPixel[i] - PPixel[j]);
            //                if (diffA-diffB<1.0E-08)
            //                {
            //                    //记录下差值
            //                    diffB = diffA;
            //                    k = (byte)j;
            //                }
            //                else
            //                {
            //                    //找到了，记录下位置，并退出内层循环
            //                    k = (byte)(j - 1);
            //                    break;
            //                }
            //            }

            //            //达到最大灰度级，标识未处理灰度级，并退出外循环
            //            if (k==255)
            //            {
            //                for (int l = 0; l < 256; l++)
            //                {
            //                    mapPixel[l] = k;
            //                }
            //                break;
            //            }

            //            //得到映射关系
            //            mapPixel[i] = k;

            //        }
            //        //灰度映射处理
            //        for (int i = 0; i < bytes; i++)
            //        {
            //            temp = grayValues[i];
            //            grayValues[i] = mapPixel[temp];
            //        }

            //        Marshal.Copy(grayValues, 0, ptr, bytes);
            //        curBitmap.UnlockBits(bmpData);
            //    }
            //    Invalidate();
            //}
            #endregion

            if (curBitmap != null)
            {
                shapinForm sForm = new shapinForm();

                if (sForm.ShowDialog() == DialogResult.OK)
                {
                    HistogramMatching(curBitmap, sForm.GetmatchingBmp, out curBitmap);
                }
            }
            Invalidate();
        }

        /// <summary>
        /// 直方图匹配
        /// </summary>
        /// <param name="srcBmp">原始图像</param>
        /// <param name="matchingBmp">匹配图像</param>
        /// <param name="dstBmp">处理后图像</param>
        /// <returns>处理成功 true 失败 false</returns>
        public static bool HistogramMatching(Bitmap srcBmp, Bitmap matchingBmp, out Bitmap dstBmp)
        {
            if (srcBmp == null || matchingBmp == null)
            {
                dstBmp = null;
                return false;
            }
            dstBmp = new Bitmap(srcBmp);
            Bitmap tempSrcBmp = new Bitmap(srcBmp);
            Bitmap tempMatchingBmp = new Bitmap(matchingBmp);
            double[] srcCpR = null;
            double[] srcCpG = null;
            double[] srcCpB = null;
            double[] matchCpB = null;
            double[] matchCpG = null;
            double[] matchCpR = null;
            //分别计算两幅图像的累计概率分布
            getCumulativeProbabilityRGB(tempSrcBmp, out srcCpR, out srcCpG, out srcCpB);
            getCumulativeProbabilityRGB(tempMatchingBmp, out matchCpR, out matchCpG, out matchCpB);

            double diffAR = 0, diffBR = 0, diffAG = 0, diffBG = 0, diffAB = 0, diffBB = 0;
            byte kR = 0, kG = 0, kB = 0;
            //逆映射函数
            byte[] mapPixelR = new byte[256];
            byte[] mapPixelG = new byte[256];
            byte[] mapPixelB = new byte[256];
            //分别计算RGB三个分量的逆映射函数
            //R
            for (int i = 0; i < 256; i++)
            {
                diffBR = 1;
                for (int j = kR; j < 256; j++)
                {
                    //找到两个累计分布函数中最相似的位置
                    diffAR = Math.Abs(srcCpR[i] - matchCpR[j]);
                    if (diffAR - diffBR < 1.0E-08)
                    {//当两概率之差小于0.000000001时可近似认为相等
                        diffBR = diffAR;
                        //记录下此时的灰度级
                        kR = (byte)j;
                    }
                    else
                    {
                        kR = (byte)Math.Abs(j - 1);
                        break;
                    }
                }
                if (kR == 255)
                {
                    for (int l = i; l < 256; l++)
                    {
                        mapPixelR[l] = kR;
                    }
                    break;
                }
                mapPixelR[i] = kR;
            }
            //G
            for (int i = 0; i < 256; i++)
            {
                diffBG = 1;
                for (int j = kG; j < 256; j++)
                {
                    diffAG = Math.Abs(srcCpG[i] - matchCpG[j]);
                    if (diffAG - diffBG < 1.0E-08)
                    {
                        diffBG = diffAG;
                        kG = (byte)j;
                    }
                    else
                    {
                        kG = (byte)Math.Abs(j - 1);
                        break;
                    }
                }
                if (kG == 255)
                {
                    for (int l = i; l < 256; l++)
                    {
                        mapPixelG[l] = kG;
                    }
                    break;
                }
                mapPixelG[i] = kG;
            }
            //B
            for (int i = 0; i < 256; i++)
            {
                diffBB = 1;
                for (int j = kB; j < 256; j++)
                {
                    diffAB = Math.Abs(srcCpB[i] - matchCpB[j]);
                    if (diffAB - diffBB < 1.0E-08)
                    {
                        diffBB = diffAB;
                        kB = (byte)j;
                    }
                    else
                    {
                        kB = (byte)Math.Abs(j - 1);
                        break;
                    }
                }
                if (kB == 255)
                {
                    for (int l = i; l < 256; l++)
                    {
                        mapPixelB[l] = kB;
                    }
                    break;
                }
                mapPixelB[i] = kB;
            }
            //映射变换
            BitmapData bmpData = dstBmp.LockBits(new Rectangle(0, 0, dstBmp.Width, dstBmp.Height), ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);
            unsafe
            {
                byte* ptr = null;
                for (int i = 0; i < dstBmp.Height; i++)
                {
                    ptr = (byte*)bmpData.Scan0 + i * bmpData.Stride;
                    for (int j = 0; j < dstBmp.Width; j++)
                    {
                        ptr[j * 3 + 2] = mapPixelR[ptr[j * 3 + 2]];
                        ptr[j * 3 + 1] = mapPixelG[ptr[j * 3 + 1]];
                        ptr[j * 3] = mapPixelB[ptr[j * 3]];
                    }
                }
            }
            dstBmp.UnlockBits(bmpData);
            return true;
        }

        /// <summary>
        /// 计算各个图像分量的累计概率分布
        /// </summary>
        /// <param name="srcBmp">原始图像</param>
        /// <param name="cpR">R分量累计概率分布</param>
        /// <param name="cpG">G分量累计概率分布</param>
        /// <param name="cpB">B分量累计概率分布</param>
        private static void getCumulativeProbabilityRGB(Bitmap srcBmp, out double[] cpR, out double[] cpG, out double[] cpB)
        {
            if (srcBmp == null)
            {
                cpB = cpG = cpR = null;
                return;
            }
            cpR = new double[256];
            cpG = new double[256];
            cpB = new double[256];
            int[] hR = null;
            int[] hG = null;
            int[] hB = null;
            double[] tempR = new double[256];
            double[] tempG = new double[256];
            double[] tempB = new double[256];
            getHistogramRGB(srcBmp, out hR, out hG, out hB);
            int totalPxl = srcBmp.Width * srcBmp.Height;
            for (int i = 0; i < 256; i++)
            {
                if (i != 0)
                {
                    tempR[i] = tempR[i - 1] + hR[i];
                    tempG[i] = tempG[i - 1] + hG[i];
                    tempB[i] = tempB[i - 1] + hB[i];
                }
                else
                {
                    tempR[0] = hR[0];
                    tempG[0] = hG[0];
                    tempB[0] = hB[0];
                }
                cpR[i] = (tempR[i] / totalPxl);
                cpG[i] = (tempG[i] / totalPxl);
                cpB[i] = (tempB[i] / totalPxl);
            }
        }

        /// <summary>
        /// 获取图像三个分量的直方图数据
        /// </summary>
        /// <param name="srcBmp">图像</param>
        /// <param name="hR">R分量直方图数据</param>
        /// <param name="hG">G分量直方图数据</param>
        /// <param name="hB">B分量直方图数据</param>
        public static void getHistogramRGB(Bitmap srcBmp, out int[] hR, out int[] hG, out int[] hB)
        {
            if (srcBmp == null)
            {
                hR = hB = hG = null;
                return;
            }
            hR = new int[256];
            hB = new int[256];
            hG = new int[256];
            BitmapData bmpData = srcBmp.LockBits(new Rectangle(0, 0, srcBmp.Width, srcBmp.Height), ImageLockMode.ReadOnly, PixelFormat.Format24bppRgb);
            unsafe
            {
                byte* ptr = null;
                for (int i = 0; i < srcBmp.Height; i++)
                {
                    ptr = (byte*)bmpData.Scan0 + i * bmpData.Stride;
                    for (int j = 0; j < srcBmp.Width; j++)
                    {
                        hB[ptr[j * 3]]++;
                        hG[ptr[j * 3 + 1]]++;
                        hR[ptr[j * 3 + 2]]++;
                    }
                }
            }
            srcBmp.UnlockBits(bmpData);
            return;
        }

        #endregion
    }
}
