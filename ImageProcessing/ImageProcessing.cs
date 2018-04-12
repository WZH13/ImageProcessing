using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;

namespace ImageProcessing
{
    public class ImageProcessing
    {
        Bitmap bitmap;
        public ImageProcessing(Bitmap sourceBitmap)
        {
            bitmap = sourceBitmap;
        }
                
        #region 灰度直方图

        //灰度等级
        private int[] countPixel= new int[256];
        //记录最大的灰度级个数
        private int maxPixel;

        /// <summary>
        /// 获取像素值
        /// </summary>
        /// <param name="x">需要获取的横坐标</param>
        /// <param name="y">需要获取的纵坐标</param>
        /// <returns>ARGB 颜色（alpha、红色、绿色、蓝色）</returns>
        public Color GetPixel(int x, int y, Bitmap srcBitmap)
        {
            Rectangle rect = new Rectangle(0, 0, srcBitmap.Width, srcBitmap.Height);
            BitmapData bmpData = srcBitmap.LockBits(rect,
                ImageLockMode.ReadWrite, srcBitmap.PixelFormat);
            IntPtr Iptr = bmpData.Scan0;
            int Depth = Bitmap.GetPixelFormatSize(srcBitmap.PixelFormat);
            unsafe
            {
                byte* ptr = (byte*)Iptr;
                ptr = ptr + bmpData.Stride * y;
                ptr += Depth * x / 8;
                Color c = Color.Empty;
                if (Depth == 32)
                {
                    int a = ptr[3];
                    int r = ptr[2];
                    int g = ptr[1];
                    int b = ptr[0];
                    c = Color.FromArgb(a, r, g, b);
                }
                else if (Depth == 24)
                {
                    int r = ptr[2];
                    int g = ptr[1];
                    int b = ptr[0];
                    c = Color.FromArgb(r, g, b);
                }
                else if (Depth == 8)
                {
                    int r = ptr[0];
                    c = Color.FromArgb(r, r, r);
                }
                srcBitmap.UnlockBits(bmpData);
                return c;
            }
        }

        /// <summary>
        /// 判断图像是否是8位灰度图像
        /// </summary>
        /// <param name="srcBitmap">源图像</param>
        /// <returns></returns>
        public bool is8BitGray(Bitmap srcBitmap)
        {
            Rectangle rect = new Rectangle(0, 0, srcBitmap.Width, srcBitmap.Height);
            BitmapData bmpData = srcBitmap.LockBits(rect,
                ImageLockMode.ReadWrite, srcBitmap.PixelFormat);
            int Depth = Bitmap.GetPixelFormatSize(srcBitmap.PixelFormat);
            Color color = Color.Empty;
            if (Depth!=8)
            {
                srcBitmap.UnlockBits(bmpData);
                return false;
            }
            else
            {
                for (int i = 0; i < bmpData.Height; i++)
                {
                    for (int j = 0; j < bmpData.Width; j++)
                    {
                        color = GetPixel(i, j,srcBitmap);
                        if (color.R!= color.G ||color.G != color.B||color.R!=color.B)
                        {
                            srcBitmap.UnlockBits(bmpData);
                            return false;
                        }
                    }
                }
                srcBitmap.UnlockBits(bmpData);
                return true;
            }
        }

        /// <summary>
        /// 绘制灰度直方图
        /// </summary>
        /// <param name="width">直方图画布宽</param>
        /// <param name="height">直方图画布高</param>
        public void histogram(int width, int height)
        {
            Rectangle rect = new Rectangle(0, 0, bitmap.Width, bitmap.Height);
            BitmapData bmpData = bitmap.LockBits(rect,
                ImageLockMode.ReadWrite, bitmap.PixelFormat);
            IntPtr ptr = bmpData.Scan0;
            int bytes = bmpData.Stride * bitmap.Height;
            byte[] grayValues = new byte[bytes];
            Marshal.Copy(ptr, grayValues, 0, bytes);//灰度值数据存入grayValues中
            int offset = bmpData.Stride - bmpData.Width;

            byte temp = 0;
            maxPixel = 0;
            //灰度等级数组清零
            Array.Clear(countPixel, 0, 256);

            //判断图像是否是灰度图，如果不是就先转化为8位灰度图再画出灰度直方图
            if (!is8BitGray(bitmap))
            {
                RgbToGrayScale(bitmap);
            }

            //计算各个灰度级的像素个数
            for (int i = 0; i < bytes; i++)
            {
                //灰度级
                temp = grayValues[i];
                //计数加1
                countPixel[temp]++;
                if (countPixel[temp] > maxPixel)
                {
                    //找到灰度频率最大的像素数，用于绘制直方图
                    maxPixel = countPixel[temp];
                }
                //跳过未用空间
                if ((i + 1) % bmpData.Width == 0)
                {
                    i += offset;
                }
            }


            //解锁
            Marshal.Copy(grayValues, 0, ptr, bytes);
            bitmap.UnlockBits(bmpData);

            Bitmap image = new Bitmap(width, height);
            Graphics g = Graphics.FromImage(image);
            g.Clear(Color.White);

            //创建一个宽度为1的黑色钢笔
            Pen curPen = new Pen(Brushes.Black, 1);

            //绘制坐标轴
            g.DrawLine(curPen, 50, 240, 320, 240);//横坐标
            g.DrawLine(curPen, 50, 240, 50, 30);//纵坐标

            //绘制并标识坐标刻度
            g.DrawLine(curPen, 100, 240, 100, 242);
            g.DrawLine(curPen, 150, 240, 150, 242);
            g.DrawLine(curPen, 200, 240, 200, 242);
            g.DrawLine(curPen, 250, 240, 250, 242);
            g.DrawLine(curPen, 300, 240, 300, 242);
            g.DrawString("0", new Font("New Timer", 8), Brushes.Black, new PointF(46, 242));
            g.DrawString("50", new Font("New Timer", 8), Brushes.Black, new PointF(92, 242));
            g.DrawString("100", new Font("New Timer", 8), Brushes.Black, new PointF(139, 242));
            g.DrawString("150", new Font("New Timer", 8), Brushes.Black, new PointF(189, 242));
            g.DrawString("200", new Font("New Timer", 8), Brushes.Black, new PointF(239, 242));
            g.DrawString("250", new Font("New Timer", 8), Brushes.Black, new PointF(289, 242));
            g.DrawLine(curPen, 48, 40, 50, 40);
            g.DrawString("0", new Font("New Timer", 8), Brushes.Black, new PointF(34, 234));
            g.DrawString(maxPixel.ToString(), new Font("New Timer", 8), Brushes.Black, new PointF(18, 34));

            //绘制直方图
            double temp1 = 0;
            for (int i = 0; i < 256; i++)
            {
                //纵坐标长度
                temp1 = 200.0 * countPixel[i] / maxPixel;
                g.DrawLine(curPen, 50 + i, 240, 50 + i, 240 - (int)temp1);
            }
            //释放对象
            curPen.Dispose();
        }

        #endregion

        #region 线性点运算

        /// <summary>
        /// 线性点运算
        /// </summary>
        /// <param name="scaling">斜率</param>
        /// <param name="offset">偏移量</param>
        public void linearPO(double scaling,double offset)
        {
            if (bitmap != null)
            {
                Rectangle rect = new Rectangle(0, 0, bitmap.Width, bitmap.Height);
                BitmapData bmpData = bitmap.LockBits(rect, ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);
                int temp0 = 0;
                int temp1 = 0;
                int temp2 = 0;
                unsafe
                {
                    for (int i = 0; i < bmpData.Height; i++)
                    {
                        byte* ptr = (byte*)bmpData.Scan0 + i * bmpData.Stride;
                        for (int j = 0; j < bmpData.Width; j++)
                        {
                            //根据公式计算线性点运算
                            //加0.5表示四舍五入
                            *(ptr + j * 3) = (byte)(scaling * *(ptr + j * 3) + offset + 0.5);
                            *(ptr + j * 3 + 1) = (byte)(scaling * *(ptr + j * 3 + 1) + offset + 0.5);
                            *(ptr + j * 3 + 2) = (byte)(scaling * *(ptr + j * 3 + 2) + offset + 0.5);
                            //灰度值限制在0~255之间
                            //大于255，则为255；小于0则为0
                            if (temp0 > 255)
                                *(ptr + j * 3) = 255;
                            else if (temp0 < 0)
                                *(ptr + j * 3) = 0;
                            else
                                *(ptr + j * 3) = (byte)temp0;

                            if (temp1 > 255)
                                *(ptr + j * 3) = 255;
                            else if (temp1 < 0)
                                *(ptr + j * 3) = 0;
                            else
                                *(ptr + j * 3) = (byte)temp1;

                            if (temp2 > 255)
                                *(ptr + j * 3) = 255;
                            else if (temp2 < 0)
                                *(ptr + j * 3) = 0;
                            else
                                *(ptr + j * 3) = (byte)temp2;
                        }
                    }
                }
                bitmap.UnlockBits(bmpData);
            }
        }

        #endregion

        #region 全等级灰度拉伸

        /// <summary>
        /// 全等级灰度拉伸 （图像增强）
        /// </summary>
        /// <param name="srcBmp">原图像</param>
        /// <param name="dstBmp">处理后图像</param>
        /// <returns>处理成功 true 失败 false</returns>
        public bool Stretch(Bitmap srcBmp, out Bitmap dstBmp)
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
        /// 直方图均衡化 直方图均衡化就是对图像进行非线性拉伸，重新分配图像像素值，使一定灰度范围内的像素数量大致相同
        /// 增大对比度，从而达到图像增强的目的。是图像处理领域中利用图像直方图对对比度进行调整的方法
        /// </summary>
        /// <param name="srcBmp">原始图像</param>
        /// <param name="dstBmp">处理后图像</param>
        /// <returns>处理成功 true 失败 false</returns>
        public bool Balance(Bitmap srcBmp, out Bitmap dstBmp)
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
        /// <param name="srcBmp">原始图像</param>
        /// <param name="matchingBmp">匹配图像</param>
        /// <param name="dstBmp">处理后图像</param>
        /// <returns>处理成功 true 失败 false</returns>
        public bool HistogramMatching(Bitmap srcBmp, Bitmap matchingBmp, out Bitmap dstBmp)
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
        public void getCumulativeProbabilityRGB(Bitmap srcBmp, out double[] cpR, out double[] cpG, out double[] cpB)
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
        public void getHistogramRGB(Bitmap srcBmp, out int[] hR, out int[] hG, out int[] hB)
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

        #region 二值化

        #region Otsu阈值法二值化模块   

        /// <summary>   
        /// Otsu阈值   
        /// </summary>   
        /// <param name="b">位图流</param>   
        /// <returns></returns>   
        public Bitmap OtsuThreshold()
        {
            // 图像灰度化   
            // b = Gray(b);   
            int width = bitmap.Width;
            int height = bitmap.Height;
            byte threshold = 0;
            int[] hist = new int[256];

            int AllPixelNumber = 0, PixelNumberSmall = 0, PixelNumberBig = 0;

            double MaxValue, AllSum = 0, SumSmall = 0, SumBig, ProbabilitySmall, ProbabilityBig, Probability;
            BitmapData bmpData = bitmap.LockBits(new Rectangle(0, 0, width, height), ImageLockMode.ReadWrite, PixelFormat.Format32bppArgb);

            unsafe
            {
                byte* p = (byte*)bmpData.Scan0;
                int offset = bmpData.Stride - width * 4;
                for (int j = 0; j < height; j++)
                {
                    for (int i = 0; i < width; i++)
                    {
                        hist[p[0]]++;
                        p += 4;
                    }
                    p += offset;
                }
                bitmap.UnlockBits(bmpData);
            }
            //计算灰度为I的像素出现的概率   
            for (int i = 0; i < 256; i++)
            {
                AllSum += i * hist[i];     //   质量矩   
                AllPixelNumber += hist[i];  //  质量       
            }
            MaxValue = -1.0;
            for (int i = 0; i < 256; i++)
            {
                PixelNumberSmall += hist[i];
                PixelNumberBig = AllPixelNumber - PixelNumberSmall;
                if (PixelNumberBig == 0)
                {
                    break;
                }

                SumSmall += i * hist[i];
                SumBig = AllSum - SumSmall;
                ProbabilitySmall = SumSmall / PixelNumberSmall;
                ProbabilityBig = SumBig / PixelNumberBig;
                Probability = PixelNumberSmall * ProbabilitySmall * ProbabilitySmall + PixelNumberBig * ProbabilityBig * ProbabilityBig;
                if (Probability > MaxValue)
                {
                    MaxValue = Probability;
                    threshold = (byte)i;
                }
            }
            this.Threshoding(bitmap, threshold);
            bitmap = twoBit(bitmap);
            return bitmap; ;
        } 
        #endregion

        #region      固定阈值法二值化模块
        public Bitmap Threshoding(Bitmap b, byte threshold)
        {
            int width = b.Width;
            int height = b.Height;
            BitmapData data = b.LockBits(new Rectangle(0, 0, width, height), ImageLockMode.ReadWrite, PixelFormat.Format32bppArgb);
            unsafe
            {
                byte* p = (byte*)data.Scan0;
                int offset = data.Stride - width * 4;
                byte R, G, B, gray;
                for (int y = 0; y < height; y++)
                {
                    for (int x = 0; x < width; x++)
                    {
                        R = p[2];
                        G = p[1];
                        B = p[0];
                        gray = (byte)((R * 19595 + G * 38469 + B * 7472) >> 16);
                        if (gray >= threshold)
                        {
                            p[0] = p[1] = p[2] = 255;
                        }
                        else
                        {
                            p[0] = p[1] = p[2] = 0;
                        }
                        p += 4;
                    }
                    p += offset;
                }
                b.UnlockBits(data);
                return b;

            }

        }
        #endregion

        #region 创建1位图像

        /// <summary>
        /// 创建1位图像
        /// </summary>
        /// <param name="srcBitmap"></param>
        /// <returns></returns>
        public Bitmap twoBit(Bitmap srcBitmap)
        {
            int midrgb = Color.FromArgb(128, 128, 128).ToArgb();
            int stride;//简单公式((width/8)+3)&(~3)
            stride = (srcBitmap.Width % 8) == 0 ? (srcBitmap.Width / 8) : (srcBitmap.Width / 8) + 1;
            stride = (stride % 4) == 0 ? stride : ((stride / 4) + 1) * 4;
            int k = srcBitmap.Height * stride;
            byte[] buf = new byte[k];
            int x = 0, ab = 0;
            for (int j = 0; j < srcBitmap.Height; j++)
            {
                k = j * stride;//因图像宽度不同、有的可能有填充字节需要跳越
                x = 0;
                ab = 0;     //ab标识当前字节的值
                for (int i = 0; i < srcBitmap.Width; i++)
                {
                    //从灰度变单色（下法如果直接从彩色变单色效果不太好，不过反相也可以在这里控制）
                    if ((srcBitmap.GetPixel(i, j)).ToArgb() > midrgb)
                    {
                        ab = ab * 2 + 1;        //ab * 2 + 1   二进制位记1
                    }
                    else
                    {
                        ab = ab * 2;        //ab * 2   二进制位记0
                    }
                    x++;        //x标识当前字节共赋值了几位
                    if (x == 8)
                    {
                        buf[k++] = (byte)ab;//每字节给数组buf赋值一次
                        ab = 0;
                        x = 0;
                    }
                }
                if (x > 0)
                {
                    //循环实现：剩余有效数据不满1字节的情况下须把它们移往字节的高位部分,如11011移位成11011000
                    for (int t = x; t < 8; t++) ab = ab * 2;
                    buf[k++] = (byte)ab;
                }
            }
            int width = srcBitmap.Width;
            int height = srcBitmap.Height;
            //int stride=
            Bitmap dstBitmap = new Bitmap(width, height, PixelFormat.Format1bppIndexed);
            BitmapData dt = dstBitmap.LockBits(new Rectangle(0, 0, width, dstBitmap.Height), ImageLockMode.ReadWrite, dstBitmap.PixelFormat);
            Marshal.Copy(buf, 0, dt.Scan0, buf.Length);
            dstBitmap.UnlockBits(dt);
            return dstBitmap;
        }

        #endregion

        #endregion

        #region 灰度处理
        /// <summary>
        /// 将源图像灰度化，并转化为8位灰度图像。
        /// </summary>
        /// <param name="original"> 源图像。 </param>
        /// <returns> 8位灰度图像。 </returns>
        public Bitmap RgbToGrayScale(Bitmap original)
        {
            if (original != null)
            {
                // 将源图像内存区域锁定
                Rectangle rect = new Rectangle(0, 0, original.Width, original.Height);
                BitmapData bmpData = original.LockBits(rect, ImageLockMode.ReadOnly,
                        PixelFormat.Format24bppRgb);

                // 获取图像参数
                int width = bmpData.Width;
                int height = bmpData.Height;
                int stride = bmpData.Stride;  // 扫描线的宽度,比实际图片要大
                int offset = stride - width * 3;  // 显示宽度与扫描线宽度的间隙
                IntPtr ptr = bmpData.Scan0;   // 获取bmpData的内存起始位置的指针
                int scanBytesLength = stride * height;  // 用stride宽度，表示这是内存区域的大小

                // 分别设置两个位置指针，指向源数组和目标数组
                int posScan = 0, posDst = 0;
                byte[] rgbValues = new byte[scanBytesLength];  // 为目标数组分配内存
                Marshal.Copy(ptr, rgbValues, 0, scanBytesLength);  // 将图像数据拷贝到rgbValues中
                // 分配灰度数组
                byte[] grayValues = new byte[width * height]; // 不含未用空间。
                // 计算灰度数组

                byte blue, green, red, YUI;



                for (int i = 0; i < height; i++)
                {
                    for (int j = 0; j < width; j++)
                    {

                        blue = rgbValues[posScan];
                        green = rgbValues[posScan + 1];
                        red = rgbValues[posScan + 2];
                        YUI = (byte)(0.229 * red + 0.587 * green + 0.144 * blue);
                        //grayValues[posDst] = (byte)((blue + green + red) / 3);
                        grayValues[posDst] = YUI;
                        posScan += 3;
                        posDst++;

                    }
                    // 跳过图像数据每行未用空间的字节，length = stride - width * bytePerPixel
                    posScan += offset;
                }

                // 内存解锁
                Marshal.Copy(rgbValues, 0, ptr, scanBytesLength);
                original.UnlockBits(bmpData);  // 解锁内存区域

                // 构建8位灰度位图
                Bitmap retBitmap = BuiltGrayBitmap(grayValues, width, height);
                return retBitmap;
            }
            else
            {
                return original;
            }

        }

        /// <summary>
        /// 用灰度数组新建一个8位灰度图像。
        /// </summary>
        /// <param name="rawValues"> 灰度数组(length = width * height)。 </param>
        /// <param name="width"> 图像宽度。 </param>
        /// <param name="height"> 图像高度。 </param>
        /// <returns> 新建的8位灰度位图。 </returns>
        private static Bitmap BuiltGrayBitmap(byte[] rawValues, int width, int height)
        {
            // 新建一个8位灰度位图，并锁定内存区域操作
            Bitmap bitmap = new Bitmap(width, height, PixelFormat.Format8bppIndexed);
            BitmapData bmpData = bitmap.LockBits(new Rectangle(0, 0, width, height),
                 ImageLockMode.WriteOnly, PixelFormat.Format8bppIndexed);

            // 计算图像参数
            int offset = bmpData.Stride - bmpData.Width;        // 计算每行未用空间字节数
            IntPtr ptr = bmpData.Scan0;                         // 获取首地址
            int scanBytes = bmpData.Stride * bmpData.Height;    // 图像字节数 = 扫描字节数 * 高度
            byte[] grayValues = new byte[scanBytes];            // 为图像数据分配内存

            // 为图像数据赋值
            int posSrc = 0, posScan = 0;                        // rawValues和grayValues的索引
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    grayValues[posScan++] = rawValues[posSrc++];
                }
                // 跳过图像数据每行未用空间的字节，length = stride - width * bytePerPixel
                posScan += offset;
            }

            // 内存解锁
            Marshal.Copy(grayValues, 0, ptr, scanBytes);
            bitmap.UnlockBits(bmpData);  // 解锁内存区域

            // 修改生成位图的索引表，从伪彩修改为灰度
            ColorPalette palette;
            // 获取一个Format8bppIndexed格式图像的Palette对象
            using (Bitmap bmp = new Bitmap(1, 1, PixelFormat.Format8bppIndexed))
            {
                palette = bmp.Palette;
            }
            for (int i = 0; i < 256; i++)
            {
                palette.Entries[i] = Color.FromArgb(i, i, i);
            }
            // 修改生成位图的索引表
            bitmap.Palette = palette;

            return bitmap;
        }
        #endregion

        

    }
}
