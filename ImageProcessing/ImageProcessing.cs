using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.Windows.Media.Imaging;
using System.IO;
using System.Windows.Forms;

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
        private int[] countPixel = new int[256];
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
            int depth = Bitmap.GetPixelFormatSize(srcBitmap.PixelFormat);
            Color color = Color.Empty;
            if (depth != 8)
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
                        color = GetPixel(i, j, srcBitmap);
                        if (color.R != color.G || color.G != color.B || color.R != color.B)
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
        public void linearPO(double scaling, double offset)
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

        //#region Otsu阈值法二值化模块   

        ///// <summary>   
        ///// Otsu阈值   
        ///// </summary>   
        ///// <param name="b">位图流</param>   
        ///// <returns></returns>   
        //public Bitmap OtsuThreshold()
        //{
        //    // 图像灰度化   
        //    // b = Gray(b);   
        //    int width = bitmap.Width;
        //    int height = bitmap.Height;
        //    byte threshold = 0;
        //    int[] hist = new int[256];

        //    int AllPixelNumber = 0, PixelNumberSmall = 0, PixelNumberBig = 0;

        //    double MaxValue, AllSum = 0, SumSmall = 0, SumBig, ProbabilitySmall, ProbabilityBig, Probability;
        //    BitmapData bmpData = bitmap.LockBits(new Rectangle(0, 0, width, height), ImageLockMode.ReadWrite, PixelFormat.Format32bppArgb);

        //    unsafe
        //    {
        //        byte* p = (byte*)bmpData.Scan0;
        //        int offset = bmpData.Stride - width * 4;
        //        for (int j = 0; j < height; j++)
        //        {
        //            for (int i = 0; i < width; i++)
        //            {
        //                hist[p[0]]++;
        //                p += 4;
        //            }
        //            p += offset;
        //        }
        //        bitmap.UnlockBits(bmpData);
        //    }
        //    //计算灰度为I的像素出现的概率   
        //    for (int i = 0; i < 256; i++)
        //    {
        //        AllSum += i * hist[i];     //   质量矩   
        //        AllPixelNumber += hist[i];  //  质量       
        //    }
        //    MaxValue = -1.0;
        //    for (int i = 0; i < 256; i++)
        //    {
        //        PixelNumberSmall += hist[i];
        //        PixelNumberBig = AllPixelNumber - PixelNumberSmall;
        //        if (PixelNumberBig == 0)
        //        {
        //            break;
        //        }

        //        SumSmall += i * hist[i];
        //        SumBig = AllSum - SumSmall;
        //        ProbabilitySmall = SumSmall / PixelNumberSmall;
        //        ProbabilityBig = SumBig / PixelNumberBig;
        //        Probability = PixelNumberSmall * ProbabilitySmall * ProbabilitySmall + PixelNumberBig * ProbabilityBig * ProbabilityBig;
        //        if (Probability > MaxValue)
        //        {
        //            MaxValue = Probability;
        //            threshold = (byte)i;
        //        }
        //    }
        //    this.Threshoding(bitmap, threshold);
        //    bitmap = twoBit(bitmap);
        //    return bitmap; ;
        //} 
        //#endregion

        //#region      固定阈值法二值化模块
        //public Bitmap Threshoding(Bitmap b, byte threshold)
        //{
        //    int width = b.Width;
        //    int height = b.Height;
        //    BitmapData data = b.LockBits(new Rectangle(0, 0, width, height), ImageLockMode.ReadWrite, PixelFormat.Format32bppArgb);
        //    unsafe
        //    {
        //        byte* p = (byte*)data.Scan0;
        //        int offset = data.Stride - width * 4;
        //        byte R, G, B, gray;
        //        for (int y = 0; y < height; y++)
        //        {
        //            for (int x = 0; x < width; x++)
        //            {
        //                R = p[2];
        //                G = p[1];
        //                B = p[0];
        //                gray = (byte)((R * 19595 + G * 38469 + B * 7472) >> 16);
        //                if (gray >= threshold)
        //                {
        //                    p[0] = p[1] = p[2] = 255;
        //                }
        //                else
        //                {
        //                    p[0] = p[1] = p[2] = 0;
        //                }
        //                p += 4;
        //            }
        //            p += offset;
        //        }
        //        b.UnlockBits(data);
        //        return b;

        //    }

        //}
        //#endregion

        //#region 创建1位图像

        ///// <summary>
        ///// 创建1位图像
        ///// </summary>
        ///// <param name="srcBitmap"></param>
        ///// <returns></returns>
        //public Bitmap twoBit(Bitmap srcBitmap)
        //{
        //    int midrgb = Color.FromArgb(128, 128, 128).ToArgb();
        //    int stride;//简单公式((width/8)+3)&(~3)
        //    stride = (srcBitmap.Width % 8) == 0 ? (srcBitmap.Width / 8) : (srcBitmap.Width / 8) + 1;
        //    stride = (stride % 4) == 0 ? stride : ((stride / 4) + 1) * 4;
        //    int k = srcBitmap.Height * stride;
        //    byte[] buf = new byte[k];
        //    int x = 0, ab = 0;
        //    for (int i = 0; i < srcBitmap.Height; i++)
        //    {
        //        k = i * stride;//因图像宽度不同、有的可能有填充字节需要跳越
        //        x = 0;
        //        ab = 0;     //ab标识当前字节的值
        //        for (int j = 0; j < srcBitmap.Width; j++)
        //        {
        //            //从灰度变单色（下法如果直接从彩色变单色效果不太好，不过反相也可以在这里控制）
        //            if ((srcBitmap.GetPixel(j, i)).ToArgb() > midrgb)
        //            {
        //                ab = ab * 2 + 1;        //ab * 2 + 1   二进制位记1
        //            }
        //            else
        //            {
        //                ab = ab * 2;        //ab * 2   二进制位记0
        //            }
        //            x++;        //x标识当前字节共赋值了几位
        //            if (x == 8)
        //            {
        //                buf[k++] = (byte)ab;//每字节给数组buf赋值一次
        //                ab = 0;
        //                x = 0;
        //            }
        //        }
        //        if (x > 0)
        //        {
        //            //循环实现：剩余有效数据不满1字节的情况下须把它们移往字节的高位部分,如11011移位成11011000
        //            for (int t = x; t < 8; t++) ab = ab * 2;
        //            buf[k++] = (byte)ab;
        //        }
        //    }
        //    int width = srcBitmap.Width;
        //    int height = srcBitmap.Height;

        //    Bitmap dstBitmap = new Bitmap(width, height, PixelFormat.Format1bppIndexed);
        //    BitmapData dt = dstBitmap.LockBits(new Rectangle(0, 0, width, dstBitmap.Height), ImageLockMode.ReadWrite, dstBitmap.PixelFormat);
        //    Marshal.Copy(buf, 0, dt.Scan0, buf.Length);
        //    dstBitmap.UnlockBits(dt);
        //    return dstBitmap;
        //}

        //#endregion

        #endregion

        #region 二值化02

        public Bitmap binaryzation(Bitmap srcBitmap, Bitmap dstBitmap)
        {
            int threshold = 0;
            Byte[,] BinaryArray = ToBinaryArray(srcBitmap, out threshold);
            dstBitmap = BinaryArrayToBinaryBitmap(BinaryArray);
            return dstBitmap;
        }

        /// <summary>
        /// 全局阈值图像二值化
        /// </summary>
        /// <param name="bmp">原始图像</param>
        /// <param name="method">二值化方法</param>
        /// <param name="threshold">输出：全局阈值</param>
        /// <returns>二值化后的图像数组</returns>       
        public static Byte[,] ToBinaryArray(Bitmap bmp, out int threshold)
        {   // 位图转换为灰度数组
            Byte[,] GrayArray = ToGrayArray(bmp);

            // 计算全局阈值
            threshold = OtsuThreshold(GrayArray);

            // 根据阈值进行二值化
            int PixelHeight = bmp.Height;
            int PixelWidth = bmp.Width;
            Byte[,] BinaryArray = new Byte[PixelHeight, PixelWidth];
            for (int i = 0; i < PixelHeight; i++)
            {
                for (int j = 0; j < PixelWidth; j++)
                {
                    BinaryArray[i, j] = Convert.ToByte((GrayArray[i, j] > threshold) ? 255 : 0);
                }
            }

            return BinaryArray;
        }

        /// <summary>
        /// 大津法计算阈值
        /// </summary>
        /// <param name="grayArray">灰度数组</param>
        /// <returns>二值化阈值</returns>
        public static int OtsuThreshold(Byte[,] grayArray)
        {   // 建立统计直方图
            int[] Histogram = new int[256];
            Array.Clear(Histogram, 0, 256);     // 初始化
            foreach (Byte b in grayArray)
            {
                Histogram[b]++;                 // 统计直方图
            }

            // 总的质量矩和图像点数
            int SumC = grayArray.Length;    // 总的图像点数
            Double SumU = 0;                  // 双精度避免方差运算中数据溢出
            for (int i = 1; i < 256; i++)
            {
                SumU += i * Histogram[i];     // 总的质量矩               
            }

            // 灰度区间
            int MinGrayLevel = Array.FindIndex(Histogram, NonZero);       // 最小灰度值
            int MaxGrayLevel = Array.FindLastIndex(Histogram, NonZero);   // 最大灰度值

            // 计算最大类间方差
            int Threshold = MinGrayLevel;
            Double MaxVariance = 0.0;       // 初始最大方差
            Double U0 = 0;                  // 初始目标质量矩
            int C0 = 0;                   // 初始目标点数
            for (int i = MinGrayLevel; i < MaxGrayLevel; i++)
            {
                if (Histogram[i] == 0) continue;

                // 目标的质量矩和点数               
                U0 += i * Histogram[i];
                C0 += Histogram[i];

                // 计算目标和背景的类间方差
                Double Diference = U0 * SumC - SumU * C0;
                Double Variance = Diference * Diference / C0 / (SumC - C0); // 方差
                if (Variance > MaxVariance)
                {
                    MaxVariance = Variance;
                    Threshold = i;
                }
            }

            // 返回类间方差最大阈值
            return Threshold;
        }

        /// <summary>
        /// 检测非零值
        /// </summary>
        /// <param name="value">要检测的数值</param>
        /// <returns>
        ///     true：非零
        ///     false：零
        /// </returns>
        private static Boolean NonZero(int value)
        {
            return (value != 0) ? true : false;
        }

        /// <summary>
        /// 将位图转换为灰度数组（256级灰度）
        /// </summary>
        /// <param name="bmp">原始位图</param>
        /// <returns>灰度数组</returns>
        public static Byte[,] ToGrayArray(Bitmap bmp)
        {
            int PixelHeight = bmp.Height; // 图像高度
            int PixelWidth = bmp.Width;   // 图像宽度
            int Stride = ((PixelWidth * 3 + 3) >> 2) << 2;    // 跨距宽度
            Byte[] Pixels = new Byte[PixelHeight * Stride];

            // 锁定位图到系统内存
            BitmapData bmpData = bmp.LockBits(new Rectangle(0, 0, PixelWidth, PixelHeight), ImageLockMode.ReadOnly, PixelFormat.Format24bppRgb);
            Marshal.Copy(bmpData.Scan0, Pixels, 0, Pixels.Length);  // 从非托管内存拷贝数据到托管内存
            bmp.UnlockBits(bmpData);    // 从系统内存解锁位图

            // 将像素数据转换为灰度数组
            Byte[,] GrayArray = new Byte[PixelHeight, PixelWidth];
            for (int i = 0; i < PixelHeight; i++)
            {
                int Index = i * Stride;
                for (int j = 0; j < PixelWidth; j++)
                {
                    GrayArray[i, j] = Convert.ToByte((Pixels[Index + 2] * 19595 + Pixels[Index + 1] * 38469 + Pixels[Index] * 7471 + 32768) >> 16);
                    Index += 3;
                }
            }

            return GrayArray;
        }

        /// <summary>
        /// 将二值化数组转换为二值化图像
        /// </summary>
        /// <param name="binaryArray">二值化数组</param>
        /// <returns>二值化图像</returns>
        public static Bitmap BinaryArrayToBinaryBitmap(Byte[,] binaryArray)
        {   // 将二值化数组转换为二值化数据
            int PixelHeight = binaryArray.GetLength(0);
            int PixelWidth = binaryArray.GetLength(1);
            int Stride = ((PixelWidth + 31) >> 5) << 2;
            Byte[] Pixels = new Byte[PixelHeight * Stride];
            for (int i = 0; i < PixelHeight; i++)
            {
                int Base = i * Stride;
                for (int j = 0; j < PixelWidth; j++)
                {
                    if (binaryArray[i, j] != 0)
                    {
                        Pixels[Base + (j >> 3)] |= Convert.ToByte(0x80 >> (j & 0x7));
                    }
                }
            }

            // 创建黑白图像
            Bitmap BinaryBmp = new Bitmap(PixelWidth, PixelHeight, PixelFormat.Format1bppIndexed);

            // 设置调色表
            ColorPalette cp = BinaryBmp.Palette;
            cp.Entries[0] = Color.Black;    // 黑色
            cp.Entries[1] = Color.White;    // 白色
            BinaryBmp.Palette = cp;

            // 设置位图图像特性
            BitmapData BinaryBmpData = BinaryBmp.LockBits(new Rectangle(0, 0, PixelWidth, PixelHeight), ImageLockMode.WriteOnly, PixelFormat.Format1bppIndexed);
            Marshal.Copy(Pixels, 0, BinaryBmpData.Scan0, Pixels.Length);
            BinaryBmp.UnlockBits(BinaryBmpData);

            return BinaryBmp;
        }

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

        #region 跳转法_由位图求X跳转

        /// <summary>
        /// 由位图求X跳转(二值图像)
        /// </summary>
        /// <param name="srcBitmap">源图像</param>
        /// <returns>保存X跳的数组</returns>
        public int[,] XJumpBitMethod(Bitmap srcBitmap)
        {
            Rectangle rect = new Rectangle(0, 0, srcBitmap.Width, srcBitmap.Height);
            BitmapData bmpData = srcBitmap.LockBits(rect,
                ImageLockMode.ReadWrite, srcBitmap.PixelFormat);
            int depth = Bitmap.GetPixelFormatSize(srcBitmap.PixelFormat);
            //位深度不为1，返回
            if (depth != 1)
            {

            }
            IntPtr ptr = bmpData.Scan0;
            int bytes = bmpData.Stride * bitmap.Height;
            byte[] bmpValues = new byte[bytes];
            Marshal.Copy(ptr, bmpValues, 0, bytes);

            int[,] Xbmp = new int[bmpData.Height, 200];//X跳数组
            bool is1 = false;//记录当前位是否是1
            int t = 0;//记录总跳数
            bool isFirst1 = true;//是否是上跳的第一个1,isFirst1=true：是上跳；isFirst1=false:不是第一个1
            bool isFirst0 = false;//是否是下跳的第一个0,isFirst0=true：是上跳；isFirst0=false:不是第一个1
            int k1 = 0;
            int j1 = 0;

            for (int i = 0; i < bmpData.Height; i++)
            {
                for (int j = 0; j < bmpData.Stride; j++)
                {
                    for (int k = 0; k < 8; k++)
                    {//按字节处理
                        is1 = ByteGetBit(bmpValues[j + i * bmpData.Stride], k);//调用ByteGetBit判断当前位是否是1
                        if (is1)
                        {//当前位是1
                            if (isFirst1)
                            {//是否是上跳位置
                                t++;
                                Xbmp[i, t] = j * 8 + k;
                                isFirst1 = false;
                                isFirst0 = true;
                            }
                        }
                        else
                        {
                            if (isFirst0)
                            {
                                t++;
                                Xbmp[i, t] = j * 8 + k;
                                isFirst0 = false;
                                isFirst1 = true;
                            }
                        }
                        k1 = k;
                    }
                    j1 = j;
                }
                if (isFirst0 == true)     //如果有上跳没有下跳则行尾增加下跳
                {
                    t++;
                    Xbmp[i, t] = j1 * 8 + k1;
                }
                Xbmp[i, 0] = t;      //总跳数赋值
                t = 0;

                isFirst1 = true;    //下一行恢复默认值
                isFirst0 = false;
            }
            srcBitmap.UnlockBits(bmpData);
            return Xbmp;
        }

        /// <summary>
        /// 由位图求X跳转
        /// </summary>
        /// <param name="srcBitmap">源图像</param>
        /// <returns>保存X跳的数组</returns>
        public int[,] XJumpMethod(Bitmap srcBitmap)
        {
            int threshold = 0;
            Byte[,] BinaryArray = ToBinaryArray(srcBitmap, out threshold);

            int[,] Xbmp = new int[srcBitmap.Height, 200];//X跳数组

            int t = 0;//记录总跳数
            bool isFirst1 = true;//是否是上跳的第一个1,isFirst1=true：是上跳；isFirst1=false:不是第一个1
            bool isFirst0 = false;//是否是下跳的第一个0,isFirst0=true：是上跳；isFirst0=false:不是第一个1

            int j1 = 0;

            for (int i = 0; i < srcBitmap.Height; i++)
            {
                for (int j = 0; j < srcBitmap.Width; j++)
                {
                    //is1 = ByteGetBit();//调用ByteGetBit判断当前位是否是1
                    if (BinaryArray[i, j] == 0)
                    {//当前位是1
                        if (isFirst1)
                        {//是否是上跳位置
                            t++;
                            Xbmp[i, t] = j;
                            isFirst1 = false;
                            isFirst0 = true;
                        }
                    }
                    else
                    {
                        if (isFirst0)
                        {
                            t++;
                            Xbmp[i, t] = j;
                            isFirst0 = false;
                            isFirst1 = true;
                        }
                    }
                    j1 = j;
                }
                if (isFirst0 == true)     //如果有上跳没有下跳则行尾增加下跳
                {
                    t++;
                    Xbmp[i, t] = j1;
                }
                Xbmp[i, 0] = t;      //总跳数赋值
                t = 0;

                isFirst1 = true;    //下一行恢复默认值
                isFirst0 = false;
            }

            return Xbmp;
        }

        /// <summary>
        /// 判断某个byte的某个位是否为1
        /// </summary>
        /// <param name="pos">第几位，大于等于1</param>
        public static bool ByteGetBit(byte b, int pos)
        {
            byte[] BITS = { 0x01, 0x02, 0x04, 0x08, 0x10, 0x20, 0x40, 0x80 };
            int temp = BITS[pos];
            return (b & temp) != 0;
        }

        #endregion

        #region 跳转法_由位图求Y跳转（未完成）

        //Y跳要判断垂直方向对应每个字节每一位是否发生上跳下跳
        public int[,] YJumpMethod(Bitmap srcBitmap)
        {
            Rectangle rect = new Rectangle(0, 0, srcBitmap.Width, srcBitmap.Height);
            BitmapData bmpData = srcBitmap.LockBits(rect,
                ImageLockMode.ReadWrite, srcBitmap.PixelFormat);
            int depth = Bitmap.GetPixelFormatSize(srcBitmap.PixelFormat);
            //位深度不为1，返回
            if (depth != 1)
            {

            }
            IntPtr ptr = bmpData.Scan0;
            int bytes = bmpData.Stride * bitmap.Height;
            byte[] bmpValues = new byte[bytes];
            Marshal.Copy(ptr, bmpValues, 0, bytes);

            int[,] Xbmp = new int[bmpData.Height, 200];//X跳数组
            bool is1 = false;//记录当前位是否是1
            int t = 0;//记录总跳数
            bool isFirst1 = true;//是否是上跳的第一个1,isFirst1=true：是上跳；isFirst1=false:不是第一个1
            bool isFirst0 = false;//是否是下跳的第一个0,isFirst0=true：是上跳；isFirst0=false:不是第一个1
            int k1 = 0;
            int j1 = 0;

            for (int i = 0; i < bmpData.Height; i++)
            {
                for (int j = 0; j < bmpData.Stride; j++)
                {
                    for (int k = 0; k < 8; k++)
                    {//按字节处理
                        is1 = ByteGetBit(bmpValues[j + i * bmpData.Stride], k);//调用ByteGetBit判断当前位是否是1
                        if (is1)
                        {//当前位是1
                            if (isFirst1)
                            {//是否是上跳位置
                                t++;
                                Xbmp[i, t] = j * 8 + k;
                                isFirst1 = false;
                                isFirst0 = true;
                            }
                        }
                        else
                        {
                            if (isFirst0)
                            {
                                t++;
                                Xbmp[i, t] = j * 8 + k;
                                isFirst0 = false;
                                isFirst1 = true;
                            }
                        }
                        k1 = k;
                    }
                    j1 = j;
                }
                if (isFirst0 == true)     //如果有上跳没有下跳则行尾增加下跳
                {
                    t++;
                    Xbmp[i, t] = j1 * 8 + k1;
                }
                Xbmp[i, 0] = t;      //总跳数赋值
                t = 0;

                isFirst1 = true;    //下一行恢复默认值
                isFirst0 = false;
            }
            srcBitmap.UnlockBits(bmpData);
            return Xbmp;
        }


        #endregion

        #region 跳转法_由X跳转求位图



        #endregion

        #region Hough变换

        /// <summary>
        /// Hough transform of line detectting process.
        /// </summary>
        /// <param name="src">The source image.</param>
        /// <param name="threshould">The threshould to adjust the number of lines.</param>
        /// <returns></returns>
        //public static WriteableBitmap HoughLineDetect(WriteableBitmap src, int threshould)////2 Hough 变换直线检测
        //{
        //    if (src != null)
        //    {
        //        int w = src.PixelWidth;
        //        int h = src.PixelHeight;
        //        WriteableBitmap srcImage = new WriteableBitmap(src);
        //        byte[] temp = src.PixelBuffer.ToArray();
        //        int roMax = (int)Math.Sqrt(w * w + h * h) + 1;
        //        int[,] mark = new int[roMax, 180];
        //        double[] theta = new double[180];
        //        for (int i = 0; i < 180; i++)
        //        {
        //            theta[i] = (double)i * Math.PI / 180.0;
        //        }
        //        double roValue = 0.0;
        //        int transValue = 0;
        //        for (int y = 0; y < h; y++)
        //        {
        //            for (int x = 0; x < w; x++)
        //            {
        //                if (temp[x * 4 + y * w * 4] == 0)
        //                {
        //                    for (int k = 0; k < 180; k++)
        //                    {
        //                        roValue = (double)x * Math.Cos(theta[k]) + (double)y * Math.Sin(theta[k]);
        //                        transValue = (int)Math.Round(roValue / 2 + roMax / 2);
        //                        mark[transValue, k]++;
        //                    }
        //                }
        //            }
        //        }
        //        for (int y = 0; y < h; y++)
        //        {
        //            for (int x = 0; x < w; x++)
        //            {
        //                int T = x * 4 + y * w * 4;
        //                if (temp[T] == 0)
        //                {
        //                    for (int k = 0; k < 180; k++)
        //                    {
        //                        roValue = (double)x * Math.Cos(theta[k]) + (double)y * Math.Sin(theta[k]);
        //                        transValue = (int)Math.Round(roValue / 2 + roMax / 2);
        //                        if (mark[transValue, k] > threshould)
        //                        {
        //                            temp[T + 2] = (byte)255;
        //                        }
        //                    }
        //                }
        //            }
        //        }
        //        Stream sTemp = srcImage.PixelBuffer.AsStream();
        //        sTemp.Seek(0, SeekOrigin.Begin);
        //        sTemp.Write(temp, 0, w * 4 * h);
        //        return srcImage;
        //    }
        //    else
        //    {
        //        return null;
        //    }
        //}

        /// <summary>
        /// 检测直线
        /// </summary>
        /// <param name="cross_num">hough变换后的曲线交点个数，取值越大，找出的直线越少</param>
        public Bitmap hough_line(Bitmap bmpobj, int cross_num)
        {
            int x = bmpobj.Width;
            int y = bmpobj.Height;
            int rho_max = (int)Math.Floor(Math.Sqrt(x * x + y * y)) + 1; //由原图数组坐标算出ρ最大值，并取整数部分加1
                                                                         //此值作为ρ，θ坐标系ρ最大值
            int[,] accarray = new int[rho_max, 180]; //定义ρ，θ坐标系的数组，初值为0。
                                                     //θ的最大值，180度

            double[] Theta = new double[180];
            //定义θ数组，确定θ取值范围
            double i = 0;
            for (int index = 0; index < 180; index++)
            {
                Theta[index] = i;
                i += Math.PI / 180;     //radians = angle * (Math.PI/180); i为每增加一度增加的弧度
            }

            double rho;
            int rho_int;
            for (int n = 0; n < x; n++)
            {
                for (int m = 0; m < y; m++)
                {
                    Color pixel = bmpobj.GetPixel(n, m);
                    if (pixel.R == 0)
                    {
                        for (int k = 0; k < 180; k++)
                        {
                            //将θ值代入hough变换方程，求ρ值
                            rho = (m * Math.Sin(Theta[k])) + (n * Math.Cos(Theta[k]));
                            //将ρ值与ρ最大值的和的一半作为ρ的坐标值（数组坐标），这样做是为了防止ρ值出现负数
                            rho_int = (int)Math.Round(rho / 2 + rho_max / 2);
                            //在ρθ坐标（数组）中标识点，即计数累加
                            accarray[rho_int, k]++;
                        }
                    }
                }
            }

            //=======利用hough变换提取直线======

            int[] case_accarray_n = new int[rho_max];
            int[] case_accarray_m = new int[rho_max];
            int K = 0; //存储数组计数器
            for (int rho_n = 0; rho_n < rho_max; rho_n++) //在hough变换后的数组中搜索
            {
                for (int theta_m = 0; theta_m < 180; theta_m++)
                {
                    if (accarray[rho_n, theta_m] >= cross_num && K < rho_max) //cross_num直线的最小长度
                    {
                        case_accarray_n[K] = rho_n; //存储搜索出的数组下标
                        case_accarray_m[K] = theta_m;
                        K = K + 1;
                    }
                }
            }

            //把这些点构成的直线提取出来,输出图像数组为I_out
            //I_out=ones(x,y).*255;
            Bitmap I_out = new Bitmap(x, y);
            for (int n = 0; n < x; n++)
            {
                for (int m = 0; m < y; m++)
                {
                    //首先设置为白色
                    I_out.SetPixel(n, m, Color.White);
                    Color pixel = bmpobj.GetPixel(n, m);
                    if (pixel.R == 0)
                    {
                        for (int k = 0; k < 180; k++)
                        {
                            rho = (m * Math.Sin(Theta[k])) + (n * Math.Cos(Theta[k]));
                            rho_int = (int)Math.Round(rho / 2 + rho_max / 2);
                            //如果正在计算的点属于100像素以上点，则把它提取出来
                            for (int a = 0; a < K - 1; a++)
                            {
                                //if rho_int==case_accarray_n(a) && k==case_accarray_m(a)%%%==gai==%%% k==case_accarray_m(a)&rho_int==case_accarray_n(a)
                                if (rho_int == case_accarray_n[a] && k == case_accarray_m[a])
                                    //I_out.SetPixel(n, m, Color.Red);
                                    bmpobj.SetPixel(n, m, Color.Red);
                            }
                        }
                    }
                }
            }
            //return I_out;
            return bmpobj;
        }

        #endregion

        #region 行字切分_投影法

        /// <summary>
        /// 投影法
        /// </summary>
        /// <param name="imageSrc">Bitmap</param>
        public Bitmap Projection(Bitmap bmp)
        {
            int imgWidth = bmp.Width;

            int imgHeight = bmp.Height;

            #region 水平投影

            //用于存储当前纵坐标垂直方向上的有效像素点数量(组成字符的像素点)
            int[] horizontalProjection = new int[imgHeight];
            Array.Clear(horizontalProjection, 0, imgHeight);

            int threshold = 0;

            //为增强本函数的通用性，先将原图像进行二值化，得到其二值化的数组
            Byte[,] BinaryArray = ToBinaryArray(bmp, out threshold);

            //用于存储水平投影后的二值化数组
            Byte[,] horizontalProArray = new Byte[imgHeight, imgWidth];
            //先将该二值化数组初始化为白色
            for (int x = 0; x < imgHeight; x++)
            {
                for (int y = 0; y < imgWidth; y++)
                {
                    horizontalProArray[x, y] = 255;
                }
            }

            //统计源图像的二值化数组中在每一个纵坐标的垂直方向所包含的像素点数
            for (int x = 0; x < imgHeight; x++)
            {
                for (int y = 0; y < imgWidth; y++)
                {
                    if (0 == BinaryArray[x, y])
                    {
                        horizontalProjection[x]++;
                    }
                }
            }

            //将源图像中纵坐标垂直方向上所包含的像素点按水平方向依次从imgHeight开始叠放在水平投影二值化数组中
            for (int x = 0; x < imgHeight; x++)
            {
                for (int y = 0; y < horizontalProjection[x]; y++)
                {
                    horizontalProArray[x, y] = 0;
                }
            }

            //将水平投影的二值化数组转换为二值化图像并保存
            //Bitmap verBmp = BinaryArrayToBinaryBitmap(horizontalProArray);
            //verBmp.Save(imageDestPath, System.Drawing.Imaging.ImageFormat.Jpeg);

            //bool is1 = false;//标识是否是1（白像素）
            int[,] lineNum = new int[100, 4];//记录行信息：0行开始的纵坐标；1行结束的纵坐标；2这一行的起始字符位置(横坐标)；3这一行结束字符位置(横坐标)
            int row = 0;
            bool isLineStart = true;//标识新出现的0是否是行开始位置
            bool isLineEnd = false;//标识新出现的0是否是行结束位置

            //for (int y = 0; y < imgHeight;y++)    作用是找出每一行的起始位置和结束位置的纵坐标
            for (int h = 0; h < imgHeight; h++)
            {
                //is1 = ByteGetBit(horizontalProArray[h, 0], 0);//调用ByteGetBit判断第一位是否是1
                //0是黑色,255是白色
                if (horizontalProArray[h, 0] == 0)//出现不是1的点，行开始
                {
                    if (isLineStart)
                    {
                        lineNum[row, 0] = h;
                        isLineStart = false;
                        isLineEnd = true;
                    }

                }
                else//出现是255的位，接着判断是不是行结束位置
                {
                    if (isLineEnd)//是行结束位置，记录下行结束纵坐标
                    {
                        lineNum[row, 1] = h;
                        isLineStart = true;
                        isLineEnd = false;
                        row++;
                    }
                }
            }

            bool isBlackPixel = false;//标识是否是黑色像素
            int headLine = imgWidth;//行首
            int tailLine = 0;//行尾
            bool isHeadLine = true;//标识新出现的0是否是行首位置
            bool isTailLine = true;//标识新出现的0是否是行尾位置

            for (int i = 0; i < row; i++)
            {
                for (int h = lineNum[i, 0]; h < lineNum[i, 1]; h++)
                {
                    for (int w = 0; w < imgWidth; w++)
                    {
                        if (BinaryArray[h, w] == 0 && isHeadLine)
                        {
                            if (w < headLine)
                            {
                                headLine = w;
                            }
                            isHeadLine = false;//判断完行首将标识是否为行首isHeadLine置为false
                        }
                    }
                    isHeadLine = true;
                    for (int w = imgWidth - 1; w >= 0; w--)
                    {
                        if (BinaryArray[h, w] == 0 && isTailLine)
                        {
                            if (w > tailLine)
                            {
                                tailLine = w;
                            }
                            isTailLine = false;//判断完行首将标识是否为行首isHeadLine置为false
                        }
                    }
                    isTailLine = true;
                    lineNum[i, 2] = headLine;
                    lineNum[i, 3] = tailLine;
                }
            }

            //lineNum存储水平投影结果，其中：0行开始的纵坐标；1行结束的纵坐标；2这一行的起始字符位置(横坐标)；3这一行结束字符位置(横坐标)

            #endregion

            #region 垂直投影

            //用于存储当前横坐标垂直方向上的有效像素点数量(组成字符的像素点)
            int[] verticalPoints = new int[imgWidth];
            //用于存储竖直投影后的二值化数组
            Byte[,] verticalProArray = new Byte[imgHeight, imgWidth];
            int[,,] characters = new int[row, 200, 4];     //[行][字][位置信息]      位置信息：0：字开始的横坐标；1：字结束的横坐标；2：字开始的纵坐标；3：字结束的纵坐标

            bool isChStart = true;//标识新出现的0是否是字开始位置
            bool isChEnd = false;//标识新出现的0是否是字结束位置
            int chNum = 0;//标记第几个字

            bool isChTop = true;//标识是否是一个字开始
            bool isChBottom = false;//标识是否是一个字结束

            for (int r = 0; r < row; r++)
            {

                //先将该二值化数组初始化为白色
                for (int w = 0; w < imgWidth; w++)
                {
                    for (int h = 0; h < imgHeight; h++)
                    {
                        verticalProArray[h, w] = 255;
                    }
                }

                //统计源图像的二值化数组中在每一个横坐标的垂直方向所包含的像素点数
                for (int w = lineNum[r, 2]; w < lineNum[r, 3] + 1; w++)
                {
                    for (int h = lineNum[r, 0]; h < lineNum[r, 1] + 1; h++)
                    {
                        if (0 == BinaryArray[h, w])
                        {
                            verticalPoints[w]++;
                        }
                    }
                }

                //将源图像中横坐标垂直方向上所包含的像素点按垂直方向依次从imgWidth开始叠放在竖直投影二值化数组中
                for (int w = 0; w < imgWidth; w++)
                {
                    for (int h = (imgHeight - 1); h > (imgHeight - verticalPoints[w] - 1); h--)
                    {
                        verticalProArray[h, w] = 0;
                    }
                }

                //Bitmap verBmp = BinaryArrayToBinaryBitmap(verticalProArray);

                //verticalPoints = new int[imgWidth];
                Array.Clear(verticalPoints, 0, imgWidth);


                //************************************  填充characters数组characters[row][ch][0]和characters[1]  ******************************************

                chNum = 0;
                for (int w = 0; w < imgWidth; w++)
                {
                    if (verticalProArray[imgHeight - 1, w] == 0)
                    {
                        if (isChStart)
                        {
                            characters[r, chNum, 0] = w;
                            isChStart = false;
                            isChEnd = true;
                        }
                    }
                    else
                    {
                        if (isChEnd)
                        {
                            characters[r, chNum, 1] = w;
                            isChStart = true;
                            isChEnd = false;
                            chNum++;
                        }
                    }
                }
                //************************************  填充characters数组characters[row][ch][0]和characters[1]  ******************************************


                //************************************  填充characters数组characters[row][ch][2]和characters[3]  ******************************************

                for (int ch = 0; ch < chNum; ch++)
                {
                    isChTop = true;
                    isChBottom = false;

                    for (int h = lineNum[r, 0]; h <= lineNum[r, 1]; h++)
                    {
                        for (int w = characters[r, ch, 0]; w <= characters[r, ch, 1]; w++)
                        {
                            if (BinaryArray[h, w] == 0)
                            {
                                if (isChTop)
                                {
                                    characters[r, ch, 2] = h;
                                    isChTop = false;
                                    isChBottom = true;
                                }
                                if (isChBottom)
                                {
                                    characters[r, ch, 3] = h;
                                }
                            }
                        }
                    }

                }

                //characters储存对每一行进行垂直投影的结果，其中：[行][字][位置信息]      位置信息包括：0：字开始的横坐标；1：字结束的横坐标；2：字开始的纵坐标；3：字结束的纵坐标

                //************************************  填充characters数组characters[row][ch][2]和characters[3]  ******************************************

                #region 画出矩形框

                for (int ch = 0; ch < chNum; ch++)
                {
                    for (int h = characters[r, ch, 2] - 1; h <= characters[r, ch, 3] + 1; h++)
                    {
                        BinaryArray[h, characters[r, ch, 0] - 1] = 0;
                        BinaryArray[h, characters[r, ch, 1] + 1] = 0;
                    }
                    for (int w = characters[r, ch, 0] - 1; w <= characters[r, ch, 1] + 1; w++)
                    {
                        BinaryArray[characters[r, ch, 2] - 1, w] = 0;
                        BinaryArray[characters[r, ch, 3] + 1, w] = 0;
                    }
                }

                #endregion


            }

            #endregion


            Bitmap dstBmp = BinaryArrayToBinaryBitmap(BinaryArray);

            return dstBmp;

        }

        ///// <summary>
        ///// 全局阈值图像二值化
        ///// </summary>
        ///// <param name="bmp">原始图像</param>
        ///// <param name="method">二值化方法</param>
        ///// <param name="threshold">输出：全局阈值</param>
        ///// <returns>二值化后的图像数组</returns>       
        //public static Byte[,] ToBinaryArray(Bitmap bmp,  out int threshold)
        //{   // 位图转换为灰度数组
        //    Byte[,] GrayArray = ToGrayArray(bmp);

        //    // 计算全局阈值
        //        threshold = OtsuThreshold(GrayArray);

        //    // 根据阈值进行二值化
        //    int PixelHeight = bmp.Height;
        //    int PixelWidth = bmp.Width;
        //    Byte[,] BinaryArray = new Byte[PixelHeight, PixelWidth];
        //    for (int i = 0; i < PixelHeight; i++)
        //    {
        //        for (int j = 0; j < PixelWidth; j++)
        //        {
        //            BinaryArray[i, j] = Convert.ToByte((GrayArray[i, j] > threshold) ? 255 : 0);
        //        }
        //    }

        //    return BinaryArray;
        //}

        ///// <summary>
        ///// 大津法计算阈值
        ///// </summary>
        ///// <param name="grayArray">灰度数组</param>
        ///// <returns>二值化阈值</returns>
        //public static int OtsuThreshold(Byte[,] grayArray)
        //{   // 建立统计直方图
        //    int[] Histogram = new int[256];
        //    Array.Clear(Histogram, 0, 256);     // 初始化
        //    foreach (Byte b in grayArray)
        //    {
        //        Histogram[b]++;                 // 统计直方图
        //    }

        //    // 总的质量矩和图像点数
        //    int SumC = grayArray.Length;    // 总的图像点数
        //    Double SumU = 0;                  // 双精度避免方差运算中数据溢出
        //    for (int i = 1; i < 256; i++)
        //    {
        //        SumU += i * Histogram[i];     // 总的质量矩               
        //    }

        //    // 灰度区间
        //    int MinGrayLevel = Array.FindIndex(Histogram, NonZero);       // 最小灰度值
        //    int MaxGrayLevel = Array.FindLastIndex(Histogram, NonZero);   // 最大灰度值

        //    // 计算最大类间方差
        //    int Threshold = MinGrayLevel;
        //    Double MaxVariance = 0.0;       // 初始最大方差
        //    Double U0 = 0;                  // 初始目标质量矩
        //    int C0 = 0;                   // 初始目标点数
        //    for (int i = MinGrayLevel; i < MaxGrayLevel; i++)
        //    {
        //        if (Histogram[i] == 0) continue;

        //        // 目标的质量矩和点数               
        //        U0 += i * Histogram[i];
        //        C0 += Histogram[i];

        //        // 计算目标和背景的类间方差
        //        Double Diference = U0 * SumC - SumU * C0;
        //        Double Variance = Diference * Diference / C0 / (SumC - C0); // 方差
        //        if (Variance > MaxVariance)
        //        {
        //            MaxVariance = Variance;
        //            Threshold = i;
        //        }
        //    }

        //    // 返回类间方差最大阈值
        //    return Threshold;
        //}

        ///// <summary>
        ///// 检测非零值
        ///// </summary>
        ///// <param name="value">要检测的数值</param>
        ///// <returns>
        /////     true：非零
        /////     false：零
        ///// </returns>
        //private static Boolean NonZero(int value)
        //{
        //    return (value != 0) ? true : false;
        //}

        ///// <summary>
        ///// 将位图转换为灰度数组（256级灰度）
        ///// </summary>
        ///// <param name="bmp">原始位图</param>
        ///// <returns>灰度数组</returns>
        //public static Byte[,] ToGrayArray(Bitmap bmp)
        //{
        //    int PixelHeight = bmp.Height; // 图像高度
        //    int PixelWidth = bmp.Width;   // 图像宽度
        //    int Stride = ((PixelWidth * 3 + 3) >> 2) << 2;    // 跨距宽度
        //    Byte[] Pixels = new Byte[PixelHeight * Stride];

        //    // 锁定位图到系统内存
        //    BitmapData bmpData = bmp.LockBits(new Rectangle(0, 0, PixelWidth, PixelHeight), ImageLockMode.ReadOnly, PixelFormat.Format24bppRgb);
        //    Marshal.Copy(bmpData.Scan0, Pixels, 0, Pixels.Length);  // 从非托管内存拷贝数据到托管内存
        //    bmp.UnlockBits(bmpData);    // 从系统内存解锁位图

        //    // 将像素数据转换为灰度数组
        //    Byte[,] GrayArray = new Byte[PixelHeight, PixelWidth];
        //    for (int i = 0; i < PixelHeight; i++)
        //    {
        //        int Index = i * Stride;
        //        for (int j = 0; j < PixelWidth; j++)
        //        {
        //            GrayArray[i, j] = Convert.ToByte((Pixels[Index + 2] * 19595 + Pixels[Index + 1] * 38469 + Pixels[Index] * 7471 + 32768) >> 16);
        //            Index += 3;
        //        }
        //    }

        //    return GrayArray;
        //}

        ///// <summary>
        ///// 将二值化数组转换为二值化图像
        ///// </summary>
        ///// <param name="binaryArray">二值化数组</param>
        ///// <returns>二值化图像</returns>
        //public static Bitmap BinaryArrayToBinaryBitmap(Byte[,] binaryArray)
        //{   // 将二值化数组转换为二值化数据
        //    int PixelHeight = binaryArray.GetLength(0);
        //    int PixelWidth = binaryArray.GetLength(1);
        //    int Stride = ((PixelWidth + 31) >> 5) << 2;
        //    Byte[] Pixels = new Byte[PixelHeight * Stride];
        //    for (int i = 0; i < PixelHeight; i++)
        //    {
        //        int Base = i * Stride;
        //        for (int j = 0; j < PixelWidth; j++)
        //        {
        //            if (binaryArray[i, j] != 0)
        //            {
        //                Pixels[Base + (j >> 3)] |= Convert.ToByte(0x80 >> (j & 0x7));
        //            }
        //        }
        //    }

        //    // 创建黑白图像
        //    Bitmap BinaryBmp = new Bitmap(PixelWidth, PixelHeight, PixelFormat.Format1bppIndexed);

        //    // 设置调色表
        //    ColorPalette cp = BinaryBmp.Palette;
        //    cp.Entries[0] = Color.Black;    // 黑色
        //    cp.Entries[1] = Color.White;    // 白色
        //    BinaryBmp.Palette = cp;

        //    // 设置位图图像特性
        //    BitmapData BinaryBmpData = BinaryBmp.LockBits(new Rectangle(0, 0, PixelWidth, PixelHeight), ImageLockMode.WriteOnly, PixelFormat.Format1bppIndexed);
        //    Marshal.Copy(Pixels, 0, BinaryBmpData.Scan0, Pixels.Length);
        //    BinaryBmp.UnlockBits(BinaryBmpData);

        //    return BinaryBmp;
        //}

        #endregion

        #region 连通域

        public Bitmap CalConnections(Bitmap bmp)
        {
            int imgWidth = bmp.Width;

            int imgHeight = bmp.Height;

            int threshold = 0;

            //为增强本函数的通用性，先将原图像进行二值化，得到其二值化的数组
            byte[,] BinaryArray = ToBinaryArray(bmp, out threshold);
            byte[,] data = (byte[,])BinaryArray.Clone();        //开辟新内存复制
            //Array.Copy(BinaryArray, data, BinaryArray.Length);  //复制的是引用

            #region
            //一种标记的点的个数
            Dictionary<int, List<Point>> dic_label_p = new Dictionary<int, List<Point>>();
            //标记
            byte label = 1;
            for (int y = 0; y < data.GetLength(0); y++)
            {
                for (int x = 0; x < data.GetLength(1); x++)
                {
                    //如果该数据不为0
                    if (data[y, x] != 255)
                    {
                        List<int> ContainsLabel = new List<int>();
                        #region 第一行
                        if (y == 0)//第一行只看左边
                        {
                            //第一行第一列，如果不为0，那么填入标记
                            if (x == 0)
                            {
                                data[y, x] = label;
                                label++;
                            }
                            //第一行，非第一列
                            else
                            {
                                //如果该列的左侧数据不为0，那么该数据标记填充为左侧的标记
                                if (data[y, x - 1] != 255)
                                {
                                    data[y, x] = data[y, x - 1];
                                }
                                //否则，填充自增标记
                                else
                                {
                                    data[y, x] = label;
                                    label++;
                                }
                            }
                        }
                        #endregion
                        #region 非第一行
                        else
                        {
                            if (x == 0)//最左边  --->不可能出现衔接情况
                            {
                                /*分析上和右上*/
                                //如果上方数据不为0，则该数据填充上方数据的标记
                                if (data[y - 1, x] != 255)
                                {
                                    data[y, x] = data[y - 1, x];
                                }
                                //上方数据为0，右上方数据不为0，则该数据填充右上方数据的标记
                                else if (data[y - 1, x + 1] != 255)
                                {
                                    data[y, x] = data[y - 1, x + 1];
                                }
                                //都为0，则填充自增标记
                                else
                                {
                                    data[y, x] = label;
                                    label++;
                                }
                            }
                            else if (x == data.GetLength(1) - 1)//最右边   --->不可能出现衔接情况
                            {
                                /*分析左上和上*/
                                //如果左上数据不为0，则则该数据填充左上方数据的标记
                                if (data[y - 1, x - 1] != 255)
                                {
                                    data[y, x] = data[y - 1, x - 1];
                                }
                                //左上方数据为0，上方数据不为0，则该数据填充上方数据的标记
                                else if (data[y - 1, x] != 255)
                                {
                                    data[y, x] = data[y - 1, x];
                                }
                                //左上和上都为0
                                else
                                {
                                    //如果左侧数据不为0，则该数据填充左侧数据的标记
                                    if (data[y, x - 1] != 255)
                                    {
                                        data[y, x] = data[y, x - 1];
                                    }
                                    //否则填充自增标记
                                    else
                                    {
                                        data[y, x] = label;
                                        label++;
                                    }
                                }
                            }
                            else//中间    --->可能出现衔接情况
                            {
                                //重新实例化需要改变的标记
                                ContainsLabel = new List<int>();
                                /*分析左上、上和右上*/
                                //上方数据不为0（中间数据），直接填充上方标记
                                if (data[y - 1, x] != 255)
                                {
                                    data[y, x] = data[y - 1, x];
                                }
                                //上方数据为0
                                else
                                {
                                    //左上和右上都不为0，填充左上标记
                                    if (data[y - 1, x - 1] != 255 && data[y - 1, x + 1] != 255)
                                    {
                                        data[y, x] = data[y - 1, x - 1];
                                        //如果右上和左上数据标记不同，则右上标记需要更改
                                        if (data[y - 1, x + 1] != data[y - 1, x - 1])
                                        {
                                            ContainsLabel.Add(data[y - 1, x + 1]);
                                        }
                                    }
                                    //左上为0，右上不为0
                                    else if (data[y - 1, x - 1] == 255 && data[y - 1, x + 1] != 255)
                                    {
                                        //左侧不为0，则填充左侧标记
                                        if (data[y, x - 1] != 255)
                                        {
                                            data[y, x] = data[y, x - 1];
                                            //如果左侧和右上标记不同，，则右上标记需要更改
                                            if (data[y - 1, x + 1] != data[y, x - 1])
                                            {
                                                ContainsLabel.Add(data[y - 1, x + 1]);
                                            }
                                        }
                                        //左侧为0，则直接填充右上标记
                                        else
                                        {
                                            data[y, x] = data[y - 1, x + 1];
                                        }
                                    }
                                    //左上不为0，右上为0，填充左上标记
                                    else if (data[y - 1, x - 1] != 255 && data[y - 1, x + 1] == 255)
                                    {
                                        data[y, x] = data[y - 1, x - 1];
                                    }
                                    //左上和右上都为0
                                    else if (data[y - 1, x - 1] == 255 && data[y - 1, x + 1] == 255)
                                    {
                                        //如果左侧不为0，则填充左侧标记
                                        if (data[y, x - 1] != 255)
                                        {
                                            data[y, x] = data[y, x - 1];
                                        }
                                        //否则填充自增标记
                                        else
                                        {
                                            data[y, x] = label;
                                            label++;
                                        }

                                    }
                                }

                            }
                        }
                        #endregion

                        //如果当前字典不存在该标记，那么创建该标记的Key
                        if (!dic_label_p.ContainsKey(data[y, x]))
                        {
                            dic_label_p.Add(data[y, x], new List<Point>());
                        }
                        //添加当前标记的点位
                        dic_label_p[data[y, x]].Add(new Point(x, y));

                        //备份需要更改标记的位置
                        List<Point> NeedChangedPoints = new List<Point>();
                        //如果有需要更改的标记
                        for (int i = 0; i < ContainsLabel.Count; i++)
                        {
                            for (int pcount = 0; pcount < dic_label_p[ContainsLabel[i]].Count;)
                            {
                                Point p = dic_label_p[ContainsLabel[i]][pcount];
                                NeedChangedPoints.Add(p);
                                data[p.Y, p.X] = data[y, x];
                                dic_label_p[ContainsLabel[i]].Remove(p);
                                dic_label_p[data[y, x]].Add(p);
                            }
                            dic_label_p.Remove(ContainsLabel[i]);
                        }
                        
                    }
                }
            }
            #endregion

            int up = imgHeight;
            int down = 0;
            int left = imgWidth;
            int right = 0;

            int count = 0;
            //矩形框坐标
            //Dictionary<int, List<Point>> rect_Poingts = new Dictionary<int, List<Point>>();
            //一个连通域
            foreach (var lines in dic_label_p.Values)
            {
                up = imgHeight;
                down = 0;
                left = imgWidth;
                right = 0;
                foreach (var points in lines)
                {
                    if (points.X < left)
                    {
                        left = points.X;
                    }
                    if (points.X > right)
                    {
                        right = points.X;
                    }
                    if (points.Y < up)
                    {
                        up = points.Y;
                    }
                    if (points.Y > down)
                    {
                        down = points.Y;
                    }
                }

                //rect_Poingts.Add(count, new List<Point>());
                //rect_Poingts[count].Add(new Point(up, left));//左上
                //rect_Poingts[count].Add(new Point(up, right));//右上
                //rect_Poingts[count].Add(new Point(down, left));//左下
                //rect_Poingts[count].Add(new Point(down, right));//右下
                //count++;
                for (int rectW = left; rectW <= right; rectW++)
                {
                    BinaryArray[up, rectW] = 0;
                    BinaryArray[down, rectW] = 0;
                }
                for (int rectH = up; rectH <= down; rectH++)
                {
                    BinaryArray[rectH, left] = 0;
                    BinaryArray[rectH, right] = 0;
                }
            }
            Bitmap dstBmp = BinaryArrayToBinaryBitmap(BinaryArray);

            return dstBmp;
        }


        #endregion

        #region 笔画密度特征提取

        /// <summary>
        /// 笔画密度特征提取
        /// </summary>
        /// <param name="bmp">源图像</param>
        /// <param name="m">//每个方向被分成大小相等的m个区间</param>
        public void StrokeDensity(Bitmap bmp, int m)
        {
            int imgWidth = bmp.Width;
            int imgHeight = bmp.Height;

            int threshold = 0;

            //为增强本函数的通用性，先将原图像进行二值化，得到其二值化的数组
            Byte[,] BinaryArray = ToBinaryArray(bmp, out threshold);

            //最终结果是m*4维的特征空间向量,用数组features存储
            int[,] features = new int[m, 4];

            //设水平方向，垂直方向，＋45°方向和－45°方向每个区间内的包含扫面线数分别为n1,n2,n3,n4(也就是每个区间的宽度)
            int n1 = 0, n2 = 0, n3 = 0, n4 = 0;
            n1 = 128 / m;
            n2 = n1;
            n3 = 128 * 2 / m;
            n4 = n3;

            //设水平方向和垂直方向每个区间内每条扫面线上穿透的笔划数目分别为strokeNumber,s2
            //＋45°方向和－45°方向每个区间内每条扫面线上穿透的笔划数目分别为s3,s4
            int strokeNumber = 0;

            //水平方向
            int section = 1;//标识第几个区间
            bool isStart = true;//标识是否是穿过的笔画的开始
            for (int h = 0; h < imgHeight; h++)
            {
                //while (h < imgHeight)
                //{
                for (int w = 0; w < imgWidth; w++)
                {
                    if (BinaryArray[h, w] == 0)
                    {
                        if (isStart)
                        {
                            strokeNumber++;       //对穿过的笔画数进行累加
                            isStart = false;
                        }
                    }
                    if (BinaryArray[h, w] == 255 && isStart == false)
                    {
                        isStart = true;
                    }
                }

                if (h == n1 * section - 1)
                {
                    features[section - 1, 0] = strokeNumber / n1;
                    strokeNumber = 0;
                    section++;//进入下一区间
                }
                //}
            }

            //垂直方向
            section = 1;//标识第几个区间
            isStart = true;//标识是否是穿过的笔画的开始
            strokeNumber = 0;
            for (int w = 0; w < imgWidth; w++)
            {
                //while (w < imgWidth)
                //{
                for (int h = 0; h < imgHeight; h++)
                {
                    if (BinaryArray[h, w] == 0)
                    {
                        if (isStart)
                        {
                            strokeNumber++;       //对穿过的笔画数进行累加
                            isStart = false;
                        }
                    }
                    if (BinaryArray[h, w] == 255 && isStart == false)
                    {
                        isStart = true;
                    }
                }

                if (w == n2 * section - 1)
                {
                    features[section - 1, 1] = strokeNumber / n2;
                    strokeNumber = 0;
                    section++;//进入下一区间
                }
                //}
            }

            //＋45°方向
            isStart = true;//标识是否是穿过的笔画的开始
            strokeNumber = 0;
            int h1 = 0;
            //第一部分（对角线上方的m/2个区间）
            for (int sec = 0; sec < m / 2; sec++)
            {
                for (int a = n3 * sec; a < n3 * (sec + 1); a++)
                {
                    for (int w1 = 0; w1 < a; w1++)
                    {
                        h1 = a - w1;//坐标计算
                        if (BinaryArray[h1, w1] == 0)
                        {
                            if (isStart)
                            {
                                strokeNumber++;       //对穿过的笔画数进行累加
                                isStart = false;
                            }
                        }
                        if (BinaryArray[h1, w1] == 255 && isStart == false)
                        {
                            isStart = true;
                        }
                    }

                }
                features[sec, 2] = strokeNumber / n3;
                strokeNumber = 0;
            }


            //第二部分（对角线下方的m/2个区间）
            isStart = true;//标识是否是穿过的笔画的开始
            strokeNumber = 0;
            int h2 = 0;
            for (int sec = m / 2; sec < m; sec++)
            {
                for (int a = n3 * sec; a < n3 * (sec + 1); a++)
                {
                    for (int w2 = a - imgHeight; w2 < imgWidth; w2++)
                    {
                        h2 = a - 1 - w2;//坐标计算
                        if (BinaryArray[h2, w2] == 0)
                        {
                            if (isStart)
                            {
                                strokeNumber++;       //对穿过的笔画数进行累加
                                isStart = false;
                            }
                        }
                        if (BinaryArray[h2, w2] == 255 && isStart == false)
                        {
                            isStart = true;
                        }
                    }
                }
                features[sec, 2] = strokeNumber / n3;
                strokeNumber = 0;
            }

            //－45°方向
            isStart = true;//标识是否是穿过的笔画的开始
            strokeNumber = 0;
            int w3 = 0;
            //第一部分（对角线下方的m/2个区间）
            for (int sec = 0; sec < m / 2; sec++)
            {
                for (int a = n4 * sec; a < n4 * (sec + 1); a++)
                {
                    for (int h3 = a; h3 < imgHeight; h3++)
                    {
                        w3 = h3 - a;//坐标计算
                        if (BinaryArray[h3, w3] == 0)
                        {
                            if (isStart)
                            {
                                strokeNumber++;       //对穿过的笔画数进行累加
                                isStart = false;
                            }
                        }
                        if (BinaryArray[h3, w3] == 255 && isStart == false)
                        {
                            isStart = true;
                        }
                    }

                }
                features[sec, 3] = strokeNumber / n4;
                strokeNumber = 0;
            }


            //第二部分（对角线下方的m/2个区间）
            isStart = true;//标识是否是穿过的笔画的开始
            strokeNumber = 0;
            int h4 = 0;
            for (int sec = m / 2; sec < m; sec++)
            {
                for (int a = n4 * (sec - 8); a < n4 * (sec - 8 + 1); a++)
                {
                    for (int w4 = a; w4 < imgWidth; w4++)
                    {
                        h4 = w4 - a;//坐标计算
                        if (BinaryArray[h4, w4] == 0)
                        {
                            if (isStart)
                            {
                                strokeNumber++;       //对穿过的笔画数进行累加
                                isStart = false;
                            }
                        }
                        if (BinaryArray[h4, w4] == 255 && isStart == false)
                        {
                            isStart = true;
                        }
                    }
                }
                features[sec, 3] = strokeNumber / n4;
                strokeNumber = 0;
            }


        }

        #endregion
    }
}
