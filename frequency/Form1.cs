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

        private Complex[] fft(Complex[] sourceData, int countN)
        {
            int r = Convert.ToInt32(Math.Log(countN, 2));//求fft的级数

            //求加权系数W
            Complex[] w = new Complex[countN / 2];
            Complex[] interVar1 = new Complex[countN];
            Complex[] interVar2 = new Complex[countN];
            
            interVar1 = (Complex[])sourceData.Clone();

            for (int i = 0; i < countN / 2; i++)
            {
                double angle = -i * Math.PI * 2 / countN;
                w[i] = new Complex(Math.Cos(angle), Math.Sin(angle));
            }

            //核心部分
            for (int i = 0; i < r; i++)
            {
                int interval = 1 << i;
                int halfN = 1 << (r - i);
                for (int j = 0; j < interval; j++)
                {
                    int gap = j * halfN;
                    for (int k = 0; k < halfN / 2; k++)
                    {
                        interVar2[k + gap] = interVar1[k + gap] + interVar1[k + gap + halfN / 2];
                        interVar2[k + halfN / 2 + gap] = (interVar1[k + gap] - interVar1[k + gap + halfN / 2]) * w[k * interval];
                    }
                }
                interVar1 = (Complex[])interVar2.Clone();
            }

            for (uint j = 0; j < countN; j++)
            {
                uint rev = 0;
                uint num = j;
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

        private Complex[] ifft(Complex[] sourceData, int countN)
        {
            for (int i = 0; i < countN; i++)
            {
                sourceData[i] = sourceData[i].Conjugate();
            }

            Complex[] interVar = new Complex[countN];
            interVar = fft(sourceData, countN);

            for (int i = 0; i < countN; i++)
            {
                interVar[i] = new Complex(interVar[i].Real / countN, -interVar[i].Imaginary / countN);
            }

            return interVar;
        }

        private Complex[] fft2(byte[] imageData, int imageWidth, int imageHeight, bool inv)
        {
            int bytes = imageWidth * imageHeight;
            byte[] bmpValues = new byte[bytes];
            Complex[] tempCom1 = new Complex[bytes];
            
            bmpValues = (byte[])imageData.Clone();
            
            for (int i = 0; i < bytes; i++)
            {
                if (inv == true)
                {
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
                    tempCom1[i] = new Complex(bmpValues[i], 0);
                }
            }

            Complex[] tempCom2 = new Complex[imageWidth];
            Complex[] tempCom3 = new Complex[imageWidth];
            for (int i = 0; i < imageHeight; i++)//水平方向
            {
                for (int j = 0; j < imageWidth; j++)
                {
                    tempCom2[j] = tempCom1[i * imageWidth + j];
                }
                tempCom3 = fft(tempCom2, imageWidth);
                for (int j = 0; j < imageWidth; j++)
                {
                    tempCom1[i * imageWidth + j] = tempCom3[j];
                }
            }
                        
            Complex[] tempCom4 = new Complex[imageHeight];
            Complex[] tempCom5 = new Complex[imageHeight];
            for (int i = 0; i < imageWidth; i++)//垂直方向
            {
                for (int j = 0; j < imageHeight; j++)
                {
                    tempCom4[j] = tempCom1[j * imageWidth + i];
                }
                tempCom5 = fft(tempCom4, imageHeight);
                for (int j = 0; j < imageHeight; j++)
                {
                    tempCom1[j * imageHeight + i] = tempCom5[j];
                }
            }

            return tempCom1;
        }

        private byte[] ifft2(Complex[] freData, int imageWidth, int imageHeight, bool inv)
        {
            int bytes = imageWidth * imageHeight;
            byte[] bmpValues = new byte[bytes];
            Complex[] tempCom1 = new Complex[bytes];
            
            tempCom1 = (Complex[])freData.Clone();
            
            Complex[] tempCom2 = new Complex[imageWidth];
            Complex[] tempCom3 = new Complex[imageWidth];
            for (int i = 0; i < imageHeight; i++)//水平方向
            {
                for (int j = 0; j < imageWidth; j++)
                {
                    tempCom2[j] = tempCom1[i * imageWidth + j];
                }
                tempCom3 = ifft(tempCom2, imageWidth);
                for (int j = 0; j < imageWidth; j++)
                {
                    tempCom1[i * imageWidth + j] = tempCom3[j];
                }
            }

            Complex[] tempCom4 = new Complex[imageHeight];
            Complex[] tempCom5 = new Complex[imageHeight];
            for (int i = 0; i < imageWidth; i++)//垂直方向
            {
                for (int j = 0; j < imageHeight; j++)
                {
                    tempCom4[j] = tempCom1[j * imageWidth + i];
                }
                tempCom5 = ifft(tempCom4, imageHeight);
                for (int j = 0; j < imageHeight; j++)
                {
                    tempCom1[j * imageHeight + i] = tempCom5[j];
                }
            }

            double tempDouble;
            for (int i = 0; i < bytes; i++)
            {
                if (inv == true)
                {
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

                freDom = fft2(grayValues, curBitmap.Width, curBitmap.Height, true);
                
                for (int i = 0; i < bytes; i++)
                {
                    tempArray[i] = Math.Log(1 + freDom[i].Abs(), 2);                   
                }

                //灰度级拉伸
                double  a = 1000.0, b = 0.0;
                double p;
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
                freDom = fft2(grayValues, curBitmap.Width, curBitmap.Height, false);

                for (int i = 0; i < bytes; i++)
                {
                    tempArray[i] = freDom[i].Angle() + 2 * Math.PI;
                }

                //灰度级拉伸
                double a = 1000.0, b = 0.0;
                double p;
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