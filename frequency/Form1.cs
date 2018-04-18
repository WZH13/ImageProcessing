using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace frequency
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void open_Click(object sender, EventArgs e)
        {
            OpenFileDialog opnDlg = new OpenFileDialog();
            opnDlg.Filter = "所有图像文件 | *.bmp; *.pcx; *.png; *.jpg; *.gif;" +
                "*.tif; *.ico; *.dxf; *.cgm; *.cdr; *.wmf; *.eps; *.emf|" +
                "位图( *.bmp; *.jpg; *.png;...) | *.bmp; *.pcx; *.png; *.jpg; *.gif; *.tif; *.ico|" +
                "矢量图( *.wmf; *.eps; *.emf;...) | *.dxf; *.cgm; *.cdr; *.wmf; *.eps; *.emf";
            opnDlg.Title = "打开图像文件";
            opnDlg.ShowHelp = true;
            if (opnDlg.ShowDialog() == DialogResult.OK)
            {
                curFileName = opnDlg.FileName;
                try
                {
                    curBitmap = (Bitmap)Image.FromFile(curFileName);
                }
                catch (Exception exp)
                {
                    MessageBox.Show(exp.Message);
                }
            }
            Invalidate();
        }

        private void close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            if (curBitmap != null)
            {
                g.DrawImage(curBitmap, 160, 20, curBitmap.Width, curBitmap.Height);
            }
        }

        /// <summary>
        /// 快速傅里叶变换
        /// </summary>
        /// <param name="sourceData">待变换的序列</param>
        /// <param name="countN">序列长度</param>
        /// <returns>变换后的序列</returns>
        private Complex[] fft(Complex[] sourceData, int countN)
        {
            int r = Convert.ToInt32(Math.Log(countN, 2));//求fft的级数
            // Math.Log(double a, double newBase)  摘要:返回指定数字在使用指定底时的对数。参数:{a: 要查找其对数的数字。newBase: 对数的底。}

            Complex[] w = new Complex[countN / 2];
            Complex[] interVar1 = new Complex[countN];
            Complex[] interVar2 = new Complex[countN];
            
            interVar1 = (Complex[])sourceData.Clone();

            //求加权系数W
            for (int i = 0; i < countN / 2; i++)
            {
                double angle = -i * Math.PI * 2 / countN;
                w[i] = new Complex(Math.Cos(angle), Math.Sin(angle));
            }

            //核心部分：蝶形运算
            for (int i = 0; i < r; i++)
            {
                int interval = 1 << i;
                int halfN = 1 << (r - i);
                //对每级的每一组点循环
                for (int j = 0; j < interval; j++)
                {
                    int gap = j * halfN;
                    //对每级的每一组点循环
                    for (int k = 0; k < halfN / 2; k++)
                    {
                        //进行蝶形算法
                        interVar2[k + gap] = interVar1[k + gap] + interVar1[k + gap + halfN / 2];
                        interVar2[k + halfN / 2 + gap] = (interVar1[k + gap] - interVar1[k + gap + halfN / 2]) * w[k * interval];
                    }
                }
                interVar1 = (Complex[])interVar2.Clone();
            }

            //按位取反
            for (uint j = 0; j < countN; j++)
            {
                uint rev = 0;
                uint num = j;
                //重新排序
                for (int i = 0; i < r; i++)
                {
                    rev <<= 1;
                    rev |= num & 1;
                    num >>= 1;
                }
                interVar2[rev] = interVar1[j];
            }
            return interVar2;
        }

        /// <summary>
        /// 快速傅里叶逆变换
        /// </summary>
        /// <param name="sourceData">待变换的序列</param>
        /// <param name="countN">序列长度</param>
        /// <returns>变换后的序列</returns>
        private Complex[] ifft(Complex[] sourceData, int countN)
        {
            //共轭变换
            for (int i = 0; i < countN; i++)
            {
                sourceData[i] = sourceData[i].Conjugate();
            }

            Complex[] interVar = new Complex[countN];
            //调用快速傅里叶变换
            interVar = fft(sourceData, countN);

            //共轭变换，并除以长度
            for (int i = 0; i < countN; i++)
            {
                interVar[i] = new Complex(interVar[i].Real / countN, -interVar[i].Imaginary / countN);
            }

            return interVar;
        }

        /// <summary>
        /// 用于图像处理的二维快速傅里叶变换
        /// </summary>
        /// <param name="imageData">图像序列</param>
        /// <param name="imageWidth">图像的宽度</param>
        /// <param name="imageHeight">图像的长度</param>
        /// <param name="inv">标识是否进行坐标位移变换
        ///                         true：进行坐标位移变换 false：不进行坐标位移变换</param>
        /// <returns>变换后的频域数据</returns>
        private Complex[] fft2(byte[] imageData, int imageWidth, int imageHeight, bool inv)
        {
            int bytes = imageWidth * imageHeight;
            byte[] bmpValues = new byte[bytes];
            Complex[] tempCom1 = new Complex[bytes];
            
            bmpValues = (byte[])imageData.Clone();
            
            //赋值：把实数变为复数，即虚部为0 
            for (int i = 0; i < bytes; i++)
            {
                if (inv == true)
                {
                    //进行频域坐标位移
                    if ((i / imageWidth + i % imageWidth) % 2 == 0)
                    {
                        tempCom1[i] = new Complex(bmpValues[i], 0);
                    }
                    else
                    {
                        tempCom1[i] = new Complex(-bmpValues[i], 0);
                    }
                }
                else
                {
                    //不进行频域坐标位移
                    tempCom1[i] = new Complex(bmpValues[i], 0);
                }
            }

            //水平方向快速傅里叶变换
            Complex[] tempCom2 = new Complex[imageWidth];
            Complex[] tempCom3 = new Complex[imageWidth];
            for (int i = 0; i < imageHeight; i++)//水平方向
            {
                //得到水平方向复数序列
                for (int j = 0; j < imageWidth; j++)
                {
                    tempCom2[j] = tempCom1[i * imageWidth + j];
                }
                //调用一维傅里叶变换
                tempCom3 = fft(tempCom2, imageWidth);
                //把结果赋值回去
                for (int j = 0; j < imageWidth; j++)
                {
                    tempCom1[i * imageWidth + j] = tempCom3[j];
                }
            }
            
            //垂直方向快速傅里叶变换
            Complex[] tempCom4 = new Complex[imageHeight];
            Complex[] tempCom5 = new Complex[imageHeight];
            for (int i = 0; i < imageWidth; i++)//垂直方向
            {
                //得到垂直方向复数序列
                for (int j = 0; j < imageHeight; j++)
                {
                    tempCom4[j] = tempCom1[j * imageWidth + i];
                }

                //调用一维傅里叶变换
                tempCom5 = fft(tempCom4, imageHeight);
                
                //把结果赋值回去
                for (int j = 0; j < imageHeight; j++)
                {
                    tempCom1[j * imageHeight + i] = tempCom5[j];
                }
            }

            return tempCom1;
        }

        /// <summary>
        /// 用于图像处理的二维快速傅里叶逆变换
        /// </summary>
        /// <param name="freData">频域数据</param>
        /// <param name="imageWidth">图像宽度</param>
        /// <param name="imageHeight">图像长度</param>
        /// <param name="inv">标识是否进行坐标位移变换，要与二维快速傅里叶正变换一致
        ///                         true：进行坐标位移变换 false：不进行坐标位移变换</param>
        /// <returns>变换后的空间域数据（即图像数据）</returns>
        private byte[] ifft2(Complex[] freData, int imageWidth, int imageHeight, bool inv)
        {
            int bytes = imageWidth * imageHeight;
            byte[] bmpValues = new byte[bytes];
            Complex[] tempCom1 = new Complex[bytes];
            
            tempCom1 = (Complex[])freData.Clone();
            
            //水平方向快速傅里叶逆变换
            Complex[] tempCom2 = new Complex[imageWidth];
            Complex[] tempCom3 = new Complex[imageWidth];
            for (int i = 0; i < imageHeight; i++)//水平方向
            {
                //得到水平方向复数序列
                for (int j = 0; j < imageWidth; j++)
                {
                    tempCom2[j] = tempCom1[i * imageWidth + j];
                }

                //调用一维傅里叶变换
                tempCom3 = ifft(tempCom2, imageWidth);

                //把结果赋值回去
                for (int j = 0; j < imageWidth; j++)
                {
                    tempCom1[i * imageWidth + j] = tempCom3[j];
                }
            }

            //垂直方向快速傅里叶逆变换
            Complex[] tempCom4 = new Complex[imageHeight];
            Complex[] tempCom5 = new Complex[imageHeight];
            for (int i = 0; i < imageWidth; i++)//垂直方向
            {
                //得到垂直方向复数序列
                for (int j = 0; j < imageHeight; j++)
                {
                    tempCom4[j] = tempCom1[j * imageWidth + i];
                }

                //调用一维傅里叶变换
                tempCom5 = ifft(tempCom4, imageHeight);

                //把结果赋值回去
                for (int j = 0; j < imageHeight; j++)
                {
                    tempCom1[j * imageHeight + i] = tempCom5[j];
                }
            }

            //赋值：把复数转换为实数，只保留复数的实数部分
            double tempDouble;
            for (int i = 0; i < bytes; i++)
            {
                if (inv == true)
                {
                    //进行坐标位移
                    if ((i / curBitmap.Width + i % curBitmap.Width) % 2 == 0)
                    {
                        tempDouble = tempCom1[i].Real;
                    }
                    else
                    {
                        tempDouble = -tempCom1[i].Real;
                    }
                }
                else
                {
                    //不进行坐标位移
                    tempDouble = tempCom1[i].Real;
                }

                if (tempDouble > 255)
                {
                    bmpValues[i] = 255;
                }
                else
                {
                    if (tempDouble < 0)
                    {
                        bmpValues[i] = 0;
                    }
                    else
                    {
                        bmpValues[i] = Convert.ToByte(tempDouble);
                    }
                }
            }

            return bmpValues;
        }

        /// <summary>
        /// 幅度图像
        /// </summary>
        private void amplitude_Click(object sender, EventArgs e)
        {
            if (curBitmap != null)
            {
                Rectangle rect = new Rectangle(0, 0, curBitmap.Width, curBitmap.Height);
                System.Drawing.Imaging.BitmapData bmpData = curBitmap.LockBits(rect, System.Drawing.Imaging.ImageLockMode.ReadWrite, curBitmap.PixelFormat);
                IntPtr ptr = bmpData.Scan0;
                int bytes = curBitmap.Width * curBitmap.Height;
                byte[] grayValues = new byte[bytes];
                System.Runtime.InteropServices.Marshal.Copy(ptr, grayValues, 0, bytes);

                Complex[] freDom = new Complex[bytes];
                double[] tempArray = new double[bytes];

                //调用二维傅里叶变换，需要进行坐标位移
                freDom = fft2(grayValues, curBitmap.Width, curBitmap.Height, true);
                
                //变量变换，并取幅度系数
                for (int i = 0; i < bytes; i++)
                {
                    tempArray[i] = Math.Log(1 + freDom[i].Abs(), 2);                   
                }

                //灰度级拉伸
                double  a = 1000.0, b = 0.0;
                double p;
                //找到最大值和最小值
                for (int i = 0; i < bytes; i++)
                {
                    if (a > tempArray[i])
                    {
                        a = tempArray[i];
                    }
                    if (b < tempArray[i])
                    {
                        b = tempArray[i];
                    }
                }
                //得到比例系数
                p = 255.0 / (b - a);
                for (int i = 0; i < bytes; i++)
                {
                    grayValues[i] = (byte)(p * (tempArray[i] - a) + 0.5);
                }


                System.Runtime.InteropServices.Marshal.Copy(grayValues, 0, ptr, bytes);
                curBitmap.UnlockBits(bmpData);

                Invalidate();
            }
        }

        /// <summary>
        /// 相位图像
        /// </summary>
        private void phase_Click(object sender, EventArgs e)
        {
            if (curBitmap != null)
            {
                Rectangle rect = new Rectangle(0, 0, curBitmap.Width, curBitmap.Height);
                System.Drawing.Imaging.BitmapData bmpData = curBitmap.LockBits(rect, System.Drawing.Imaging.ImageLockMode.ReadWrite, curBitmap.PixelFormat);
                IntPtr ptr = bmpData.Scan0;
                int bytes = curBitmap.Width * curBitmap.Height;
                byte[] grayValues = new byte[bytes];
                System.Runtime.InteropServices.Marshal.Copy(ptr, grayValues, 0, bytes);

                Complex[] freDom = new Complex[bytes];
                double[] tempArray = new double[bytes];
                //调用二维傅里叶变换，不进行坐标位移
                freDom = fft2(grayValues, curBitmap.Width, curBitmap.Height, false);

                //取相位系数，并进行了变量变换
                for (int i = 0; i < bytes; i++)
                {
                    tempArray[i] = freDom[i].Angle() + 2 * Math.PI;
                }

                //灰度级拉伸
                double a = 1000.0, b = 0.0;
                double p;
                //找到最大值和最小值
                for (int i = 0; i < bytes; i++)
                {
                    if (a > tempArray[i])
                    {
                        a = tempArray[i];
                    }
                    if (b < tempArray[i])
                    {
                        b = tempArray[i];
                    }
                }
                //得到比例系数
                p = 255.0 / (b - a);
                for (int i = 0; i < bytes; i++)
                {
                    grayValues[i] = (byte)(p * (tempArray[i] - a) + 0.5);
                }

                System.Runtime.InteropServices.Marshal.Copy(grayValues, 0, ptr, bytes);
                curBitmap.UnlockBits(bmpData);

                Invalidate();
            }
        }

        private void freGran_Click(object sender, EventArgs e)
        {
            if (curBitmap != null)
            {
                granularity granForm = new granularity();
                if (granForm.ShowDialog() == DialogResult.OK)
                {
                    Rectangle rect = new Rectangle(0, 0, curBitmap.Width, curBitmap.Height);
                    System.Drawing.Imaging.BitmapData bmpData = curBitmap.LockBits(rect, System.Drawing.Imaging.ImageLockMode.ReadWrite, curBitmap.PixelFormat);
                    IntPtr ptr = bmpData.Scan0;
                    int bytes = curBitmap.Width * curBitmap.Height;
                    byte[] grayValues = new byte[bytes];
                    System.Runtime.InteropServices.Marshal.Copy(ptr, grayValues, 0, bytes);

                    Complex[] freDom = new Complex[bytes];
                    Complex[] tempDD = new Complex[bytes];
                    byte[] tempArray = new byte[bytes];

                    byte[] tempRadius = new byte[6];
                    tempRadius = granForm.GetRadius;
                    byte flag = granForm.GetFlag;
                    int minLen=Math.Min(curBitmap.Width,curBitmap.Height);
                    double[] radius = new double[6];
                    for(int i = 0; i < 6; i++)
                    {
                        radius[i] = tempRadius[i] * minLen / 100;
                    }
                    
                    freDom = fft2(grayValues, curBitmap.Width, curBitmap.Height, true);

                    for (int i = 0; i < curBitmap.Height; i++)
                    {
                        for (int j = 0; j < curBitmap.Width; j++)
                        {
                            double distance = (double)((j - curBitmap.Width / 2) * (j - curBitmap.Width / 2) + (i - curBitmap.Height / 2) * (i - curBitmap.Height / 2));
                            distance = Math.Sqrt(distance);

                            switch (flag)
                            {
                                case 0:
                                    if (distance > radius[0])
                                    {
                                        tempDD[i * curBitmap.Width + j] = new Complex(0.0, 0.0);
                                    }
                                    else
                                        tempDD[i * curBitmap.Width + j] = new Complex(1.0, 1.0);
                                    break;
                                case 1:
                                    if (distance < radius[1] && distance > radius[2])
                                    {
                                        tempDD[i * curBitmap.Width + j] = new Complex(0.0, 0.0);
                                    }
                                    else
                                        tempDD[i * curBitmap.Width + j] = new Complex(1.0, 1.0);
                                    break;
                                case 2:
                                    if (distance > radius[3] || distance < radius[4])
                                    {
                                        tempDD[i * curBitmap.Width + j] = new Complex(0.0, 0.0);
                                    }
                                    else
                                        tempDD[i * curBitmap.Width + j] = new Complex(1.0, 1.0);
                                    break;
                                case 3:
                                    if (distance < radius[5])
                                    {
                                        tempDD[i * curBitmap.Width + j] = new Complex(0.0, 0.0);
                                    }
                                    else
                                        tempDD[i * curBitmap.Width + j] = new Complex(1.0, 1.0);
                                    break;
                                default:
                                    MessageBox.Show("无效！");
                                    break;
                            }
                        }
                    }

                    for (int i = 0; i < bytes; i++)
                        freDom[i] *= tempDD[i];

                        tempArray = ifft2(freDom, curBitmap.Width, curBitmap.Height, true);
                    grayValues = (byte[])tempArray.Clone();

                    System.Runtime.InteropServices.Marshal.Copy(grayValues, 0, ptr, bytes);
                    curBitmap.UnlockBits(bmpData);
                }

                Invalidate();
            }
        }

        private void freOri_Click(object sender, EventArgs e)
        {
            if (curBitmap != null)
            {
                orientation orieForm = new orientation();
                if (orieForm.ShowDialog() == DialogResult.OK)
                {
                    Rectangle rect = new Rectangle(0, 0, curBitmap.Width, curBitmap.Height);
                    System.Drawing.Imaging.BitmapData bmpData = curBitmap.LockBits(rect, System.Drawing.Imaging.ImageLockMode.ReadWrite, curBitmap.PixelFormat);
                    IntPtr ptr = bmpData.Scan0;
                    int bytes = curBitmap.Width * curBitmap.Height;
                    byte[] grayValues = new byte[bytes];
                    System.Runtime.InteropServices.Marshal.Copy(ptr, grayValues, 0, bytes);

                    Complex[] freDom = new Complex[bytes];
                    byte[] tempArray = new byte[bytes];

                    int[] tempOrient = new int[2];
                    tempOrient = orieForm.GetOrient;
                    byte flag = 1;
                    if (tempOrient[1] <= 0)
                    {
                        flag = 1;
                    }
                    if(tempOrient[0] <= 0 && tempOrient[1] > 0)
                    {
                        flag = 2;
                    }
                    if (tempOrient[0] > 0 && tempOrient[1] < 180)
                    {
                        flag = 3;
                    }
                    if (tempOrient[1] > 180)
                    {
                        flag = 4;
                    }
                    freDom = fft2(grayValues, curBitmap.Width, curBitmap.Height, true);

                    double tempD;

                    for (int i = 0; i < curBitmap.Height; i++)
                    {
                        for (int j = 0; j < curBitmap.Width; j++)
                        {
                            tempD = (Math.Atan2(curBitmap.Height / 2 - i, j - curBitmap.Width / 2)) * 180 / Math.PI;
                            
                            switch (flag)
                            {
                                case 1:
                                    if ((tempD <= tempOrient[1] && tempD >= tempOrient[0]) || (tempD <= (tempOrient[1] + 180) && tempD >= (tempOrient[0] + 180)))
                                    {
                                        freDom[i * curBitmap.Width + j] = new Complex(0.0, 0.0); 
                                    }
                                    break;
                                case 2:
                                    if ((tempD <= tempOrient[1] && tempD >= tempOrient[0] /*&& tempD != 0*/) || (tempD <= tempOrient[1] - 180 && tempD > -180) || (tempD > tempOrient[0] + 180 && tempD <= 180))
                                    {
                                        freDom[i * curBitmap.Width + j] = new Complex(0.0, 0.0); 
                                    }
                                    break;
                                case 3:
                                    if ((tempD <= tempOrient[1] && tempD >= tempOrient[0]) || (tempD <= tempOrient[1] - 180 && tempD >= tempOrient[0] - 180))
                                    {
                                        freDom[i * curBitmap.Width + j] = new Complex(0.0, 0.0); 
                                    }
                                    break;
                                case 4:
                                    if (((tempD <= tempOrient[1] - 180) && (tempD >= tempOrient[0] - 180)) || (tempD <= tempOrient[1] - 360 && tempD >= -180) || (tempD >= tempOrient[0] && tempD <= 180))
                                    {
                                        freDom[i * curBitmap.Width + j] = new Complex(0.0, 0.0); 
                                    }
                                    break;
                                default:
                                    MessageBox.Show("无效！");
                                    break;
                            }
                                                        
                        }
                    }

                    tempArray = ifft2(freDom, curBitmap.Width, curBitmap.Height, true);
                    grayValues = (byte[])tempArray.Clone();

                    System.Runtime.InteropServices.Marshal.Copy(grayValues, 0, ptr, bytes);
                    curBitmap.UnlockBits(bmpData);
                }

                Invalidate();
            }
        }
    }
}