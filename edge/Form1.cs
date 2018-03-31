using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace edge
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

        private void mask_Click(object sender, EventArgs e)
        {
            if (curBitmap != null)
            {
                mask operatorMask = new mask();
                if (operatorMask.ShowDialog() == DialogResult.OK)
                {
                    Rectangle rect = new Rectangle(0, 0, curBitmap.Width, curBitmap.Height);
                    System.Drawing.Imaging.BitmapData bmpData = curBitmap.LockBits(rect, System.Drawing.Imaging.ImageLockMode.ReadWrite, curBitmap.PixelFormat);
                    IntPtr ptr = bmpData.Scan0;
                    int bytes = curBitmap.Width * curBitmap.Height;
                    byte[] grayValues = new byte[bytes];
                    System.Runtime.InteropServices.Marshal.Copy(ptr, grayValues, 0, bytes);

                    int thresholding = operatorMask.GetThresholding;
                    byte flagMask = operatorMask.GetMask;
                    double[] tempArray = new double[bytes];
                    double gradX, gradY, grad;

                    switch (flagMask)
                    {
                        case 0://Roberts
                            for (int i = 0; i < curBitmap.Height; i++)
                            {
                                for (int j = 0; j < curBitmap.Width; j++)
                                {
                                    gradX = grayValues[((Math.Abs(i - 1)) % curBitmap.Height) * curBitmap.Width + ((j + 1) % curBitmap.Width)] - grayValues[i * curBitmap.Width + j];
                                    gradY = grayValues[((Math.Abs(i - 1)) % curBitmap.Height) * curBitmap.Width + j] - grayValues[i * curBitmap.Width + ((j + 1) % curBitmap.Width)];
                                    grad = Math.Sqrt(gradX * gradX + gradY * gradY);
                                    tempArray[i * curBitmap.Width + j] = grad;
                                }
                            }
                            break;
                        case 1://Prewitt
                            for (int i = 0; i < curBitmap.Height; i++)
                            {
                                for (int j = 0; j < curBitmap.Width; j++)
                                {
                                    gradX = grayValues[((Math.Abs(i - 1)) % curBitmap.Height) * curBitmap.Width + ((j + 1) % curBitmap.Width)] + grayValues[i * curBitmap.Width + ((j + 1) % curBitmap.Width)] + grayValues[((i + 1) % curBitmap.Height) * curBitmap.Width + ((j + 1) % curBitmap.Width)] - 
                                        grayValues[((Math.Abs(i - 1)) % curBitmap.Height) * curBitmap.Width + (Math.Abs(j - 1) % curBitmap.Width)] - grayValues[i * curBitmap.Width + (Math.Abs(j - 1) % curBitmap.Width)] - grayValues[((i + 1) % curBitmap.Height) * curBitmap.Width + (Math.Abs(j - 1) % curBitmap.Width)];
                                    gradY = grayValues[((Math.Abs(i - 1)) % curBitmap.Height) * curBitmap.Width + (Math.Abs(j - 1) % curBitmap.Width)] + grayValues[((Math.Abs(i - 1)) % curBitmap.Height) * curBitmap.Width + j] + grayValues[((Math.Abs(i - 1)) % curBitmap.Height) * curBitmap.Width + ((j + 1) % curBitmap.Width)] - 
                                        grayValues[((i + 1) % curBitmap.Height) * curBitmap.Width + (Math.Abs(j - 1) % curBitmap.Width)] - grayValues[((i + 1) % curBitmap.Height) * curBitmap.Width + j] - grayValues[((i + 1) % curBitmap.Height) * curBitmap.Width + ((j + 1) % curBitmap.Width)];
                                    grad = Math.Sqrt(gradX * gradX + gradY * gradY);
                                    tempArray[i * curBitmap.Width + j] = grad;
                                }
                            }
                            break;
                        case 2://Sobel
                            for (int i = 0; i < curBitmap.Height; i++)
                            {
                                for (int j = 0; j < curBitmap.Width; j++)
                                {
                                    gradX = grayValues[((Math.Abs(i - 1)) % curBitmap.Height) * curBitmap.Width + ((j + 1) % curBitmap.Width)] + 2 * grayValues[i * curBitmap.Width + ((j + 1) % curBitmap.Width)] + grayValues[((i + 1) % curBitmap.Height) * curBitmap.Width + ((j + 1) % curBitmap.Width)] - 
                                        grayValues[((Math.Abs(i - 1)) % curBitmap.Height) * curBitmap.Width + ((Math.Abs(j - 1)) % curBitmap.Width)] - 2 * grayValues[i * curBitmap.Width + ((Math.Abs(j - 1)) % curBitmap.Width)] - grayValues[((i + 1) % curBitmap.Height) * curBitmap.Width + ((Math.Abs(j - 1)) % curBitmap.Width)];
                                    gradY = grayValues[((Math.Abs(i - 1)) % curBitmap.Height) * curBitmap.Width + ((Math.Abs(j - 1)) % curBitmap.Width)] + 2 * grayValues[((Math.Abs(i - 1)) % curBitmap.Height) * curBitmap.Width + j] + grayValues[((Math.Abs(i - 1)) % curBitmap.Height) * curBitmap.Width + ((j + 1) % curBitmap.Width)] - 
                                        grayValues[((i + 1) % curBitmap.Height) * curBitmap.Width + ((Math.Abs(j - 1)) % curBitmap.Width)] - 2 * grayValues[((i + 1) % curBitmap.Height) * curBitmap.Width + j] - grayValues[((i + 1) % curBitmap.Height) * curBitmap.Width + ((j + 1) % curBitmap.Width)];
                                    grad = Math.Sqrt(gradX * gradX + gradY * gradY);
                                    tempArray[i * curBitmap.Width + j] = grad;
                                }
                            }
                            break;
                        case 3://Laplacian1公式(8.4)
                            for (int i = 0; i < curBitmap.Height; i++)
                            {
                                for (int j = 0; j < curBitmap.Width; j++)
                                {
                                    grad = grayValues[((Math.Abs(i - 1)) % curBitmap.Height) * curBitmap.Width + j] + grayValues[i * curBitmap.Width + ((Math.Abs(j - 1)) % curBitmap.Width)] + grayValues[i * curBitmap.Width + ((j + 1) % curBitmap.Width)] + grayValues[((i + 1) % curBitmap.Height) * curBitmap.Width + j] - 4 * grayValues[i * curBitmap.Width + j];
                                    tempArray[i * curBitmap.Width + j] = grad;
                                }
                            }
                            break;
                        case 4://Laplacian2公式(8.5)
                            for (int i = 0; i < curBitmap.Height; i++)
                            {
                                for (int j = 0; j < curBitmap.Width; j++)
                                {
                                    grad = grayValues[((Math.Abs(i - 1)) % curBitmap.Height) * curBitmap.Width + ((Math.Abs(j - 1)) % curBitmap.Width)] + grayValues[((Math.Abs(i - 1)) % curBitmap.Height) * curBitmap.Width + j] + grayValues[((Math.Abs(i - 1)) % curBitmap.Height) * curBitmap.Width + ((j + 1) % curBitmap.Width)] + grayValues[i * curBitmap.Width + ((Math.Abs(j - 1)) % curBitmap.Width)] + 
                                        grayValues[i * curBitmap.Width + ((j + 1) % curBitmap.Width)] + grayValues[((i + 1) % curBitmap.Height) * curBitmap.Width + ((Math.Abs(j - 1)) % curBitmap.Width)] + grayValues[((i + 1) % curBitmap.Height) * curBitmap.Width + j] + grayValues[((i + 1) % curBitmap.Height) * curBitmap.Width + ((j + 1) % curBitmap.Width)] - 8 * grayValues[i * curBitmap.Width + j];
                                    tempArray[i * curBitmap.Width + j] = grad;
                                }
                            }
                            break;
                        case 5://Laplacian3公式(8.6)
                            for (int i = 0; i < curBitmap.Height; i++)
                            {
                                for (int j = 0; j < curBitmap.Width; j++)
                                {
                                    grad = -1 * grayValues[((Math.Abs(i - 1)) % curBitmap.Height) * curBitmap.Width + ((Math.Abs(j - 1)) % curBitmap.Width)] + 2 * grayValues[((Math.Abs(i - 1)) % curBitmap.Height) * curBitmap.Width + j] - grayValues[((Math.Abs(i - 1)) % curBitmap.Height) * curBitmap.Width + ((j + 1) % curBitmap.Width)] + 2 * grayValues[i * curBitmap.Width + ((Math.Abs(j - 1)) % curBitmap.Width)] + 
                                        2 * grayValues[i * curBitmap.Width + ((j + 1) % curBitmap.Width)] - grayValues[((i + 1) % curBitmap.Height) * curBitmap.Width + ((Math.Abs(j - 1)) % curBitmap.Width)] + 2 * grayValues[((i + 1) % curBitmap.Height) * curBitmap.Width + j] - grayValues[((i + 1) % curBitmap.Height) * curBitmap.Width + ((j + 1) % curBitmap.Width)] - 4 * grayValues[i * curBitmap.Width + j];
                                    tempArray[i * curBitmap.Width + j] = grad;
                                }
                            }
                            break;
                        case 6://Kirsch
                            for (int i = 0; i < curBitmap.Height; i++)
                            {
                                for (int j = 0; j < curBitmap.Width; j++)
                                {
                                    grad = 0;

                                    gradX = -5 * grayValues[((Math.Abs(i - 1)) % curBitmap.Height) * curBitmap.Width + ((Math.Abs(j - 1)) % curBitmap.Width)] + 3 * grayValues[((Math.Abs(i - 1)) % curBitmap.Height) * curBitmap.Width + j] + 3 * grayValues[((Math.Abs(i - 1)) % curBitmap.Height) * curBitmap.Width + ((j + 1) % curBitmap.Width)] - 5 * grayValues[i * curBitmap.Width + ((Math.Abs(j - 1)) % curBitmap.Width)] + 
                                        3 * grayValues[i * curBitmap.Width + ((j + 1) % curBitmap.Width)] - 5 * grayValues[((i + 1) % curBitmap.Height) * curBitmap.Width + ((Math.Abs(j - 1)) % curBitmap.Width)] + 3 * grayValues[((i + 1) % curBitmap.Height) * curBitmap.Width + j] + 3 * grayValues[((i + 1) % curBitmap.Height) * curBitmap.Width + ((j + 1) % curBitmap.Width)];
                                    if (gradX > grad)
                                        grad = gradX;

                                    gradX = 3 * grayValues[((Math.Abs(i - 1)) % curBitmap.Height) * curBitmap.Width + ((Math.Abs(j - 1)) % curBitmap.Width)] + 3 * grayValues[((Math.Abs(i - 1)) % curBitmap.Height) * curBitmap.Width + j] + 3 * grayValues[((Math.Abs(i - 1)) % curBitmap.Height) * curBitmap.Width + ((j + 1) % curBitmap.Width)] - 5 * grayValues[i * curBitmap.Width + ((Math.Abs(j - 1)) % curBitmap.Width)] + 
                                        3 * grayValues[i * curBitmap.Width + ((j + 1) % curBitmap.Width)] - 5 * grayValues[((i + 1) % curBitmap.Height) * curBitmap.Width + ((Math.Abs(j - 1)) % curBitmap.Width)] - 5 * grayValues[((i + 1) % curBitmap.Height) * curBitmap.Width + j] + 3 * grayValues[((i + 1) % curBitmap.Height) * curBitmap.Width + ((j + 1) % curBitmap.Width)];
                                    if (gradX > grad)
                                        grad = gradX;

                                    gradX = 3 * grayValues[((Math.Abs(i - 1)) % curBitmap.Height) * curBitmap.Width + ((Math.Abs(j - 1)) % curBitmap.Width)] + 3 * grayValues[((Math.Abs(i - 1)) % curBitmap.Height) * curBitmap.Width + j] + 3 * grayValues[((Math.Abs(i - 1)) % curBitmap.Height) * curBitmap.Width + ((j + 1) % curBitmap.Width)] + 3 * grayValues[i * curBitmap.Width + ((Math.Abs(j - 1)) % curBitmap.Width)] +
                                        3 * grayValues[i * curBitmap.Width + ((j + 1) % curBitmap.Width)] - 5 * grayValues[((i + 1) % curBitmap.Height) * curBitmap.Width + ((Math.Abs(j - 1)) % curBitmap.Width)] - 5 * grayValues[((i + 1) % curBitmap.Height) * curBitmap.Width + j] - 5 * grayValues[((i + 1) % curBitmap.Height) * curBitmap.Width + ((j + 1) % curBitmap.Width)];
                                    if (gradX > grad)
                                        grad = gradX;

                                    gradX = 3 * grayValues[((Math.Abs(i - 1)) % curBitmap.Height) * curBitmap.Width + ((Math.Abs(j - 1)) % curBitmap.Width)] + 3 * grayValues[((Math.Abs(i - 1)) % curBitmap.Height) * curBitmap.Width + j] + 3 * grayValues[((Math.Abs(i - 1)) % curBitmap.Height) * curBitmap.Width + ((j + 1) % curBitmap.Width)] + 3 * grayValues[i * curBitmap.Width + ((Math.Abs(j - 1)) % curBitmap.Width)] -
                                        5 * grayValues[i * curBitmap.Width + ((j + 1) % curBitmap.Width)] + 3 * grayValues[((i + 1) % curBitmap.Height) * curBitmap.Width + ((Math.Abs(j - 1)) % curBitmap.Width)] - 5 * grayValues[((i + 1) % curBitmap.Height) * curBitmap.Width + j] - 5 * grayValues[((i + 1) % curBitmap.Height) * curBitmap.Width + ((j + 1) % curBitmap.Width)];
                                    if (gradX > grad)
                                        grad = gradX;

                                    gradX = 3 * grayValues[((Math.Abs(i - 1)) % curBitmap.Height) * curBitmap.Width + ((Math.Abs(j - 1)) % curBitmap.Width)] + 3 * grayValues[((Math.Abs(i - 1)) % curBitmap.Height) * curBitmap.Width + j] - 5 * grayValues[((Math.Abs(i - 1)) % curBitmap.Height) * curBitmap.Width + ((j + 1) % curBitmap.Width)] + 3 * grayValues[i * curBitmap.Width + ((Math.Abs(j - 1)) % curBitmap.Width)] -
                                        5 * grayValues[i * curBitmap.Width + ((j + 1) % curBitmap.Width)] + 3 * grayValues[((i + 1) % curBitmap.Height) * curBitmap.Width + ((Math.Abs(j - 1)) % curBitmap.Width)] + 3 * grayValues[((i + 1) % curBitmap.Height) * curBitmap.Width + j] - 5 * grayValues[((i + 1) % curBitmap.Height) * curBitmap.Width + ((j + 1) % curBitmap.Width)];
                                    if (gradX > grad)
                                        grad = gradX;

                                    gradX = 3 * grayValues[((Math.Abs(i - 1)) % curBitmap.Height) * curBitmap.Width + ((Math.Abs(j - 1)) % curBitmap.Width)] - 5 * grayValues[((Math.Abs(i - 1)) % curBitmap.Height) * curBitmap.Width + j] - 5 * grayValues[((Math.Abs(i - 1)) % curBitmap.Height) * curBitmap.Width + ((j + 1) % curBitmap.Width)] + 3 * grayValues[i * curBitmap.Width + ((Math.Abs(j - 1)) % curBitmap.Width)] -
                                        5 * grayValues[i * curBitmap.Width + ((j + 1) % curBitmap.Width)] + 3 * grayValues[((i + 1) % curBitmap.Height) * curBitmap.Width + ((Math.Abs(j - 1)) % curBitmap.Width)] + 3 * grayValues[((i + 1) % curBitmap.Height) * curBitmap.Width + j] + 3 * grayValues[((i + 1) % curBitmap.Height) * curBitmap.Width + ((j + 1) % curBitmap.Width)];
                                    if (gradX > grad)
                                        grad = gradX;

                                    gradX = -5 * grayValues[((Math.Abs(i - 1)) % curBitmap.Height) * curBitmap.Width + ((Math.Abs(j - 1)) % curBitmap.Width)] - 5 * grayValues[((Math.Abs(i - 1)) % curBitmap.Height) * curBitmap.Width + j] - 5 * grayValues[((Math.Abs(i - 1)) % curBitmap.Height) * curBitmap.Width + ((j + 1) % curBitmap.Width)] + 3 * grayValues[i * curBitmap.Width + ((Math.Abs(j - 1)) % curBitmap.Width)] +
                                        3 * grayValues[i * curBitmap.Width + ((j + 1) % curBitmap.Width)] + 3 * grayValues[((i + 1) % curBitmap.Height) * curBitmap.Width + ((Math.Abs(j - 1)) % curBitmap.Width)] + 3 * grayValues[((i + 1) % curBitmap.Height) * curBitmap.Width + j] + 3 * grayValues[((i + 1) % curBitmap.Height) * curBitmap.Width + ((j + 1) % curBitmap.Width)];
                                    if (gradX > grad)
                                        grad = gradX;

                                    gradX = -5 * grayValues[((Math.Abs(i - 1)) % curBitmap.Height) * curBitmap.Width + ((Math.Abs(j - 1)) % curBitmap.Width)] - 5 * grayValues[((Math.Abs(i - 1)) % curBitmap.Height) * curBitmap.Width + j] + 3 * grayValues[((Math.Abs(i - 1)) % curBitmap.Height) * curBitmap.Width + ((j + 1) % curBitmap.Width)] - 5 * grayValues[i * curBitmap.Width + ((Math.Abs(j - 1)) % curBitmap.Width)] +
                                        3 * grayValues[i * curBitmap.Width + ((j + 1) % curBitmap.Width)] + 3 * grayValues[((i + 1) % curBitmap.Height) * curBitmap.Width + ((Math.Abs(j - 1)) % curBitmap.Width)] + 3 * grayValues[((i + 1) % curBitmap.Height) * curBitmap.Width + j] + 3 * grayValues[((i + 1) % curBitmap.Height) * curBitmap.Width + ((j + 1) % curBitmap.Width)];
                                    if (gradX > grad)
                                        grad = gradX; 

                                    tempArray[i * curBitmap.Width + j] = grad;
                                }
                            }
                            break;
                        default:
                            MessageBox.Show("无效！");
                            break;
                    }

                    if (thresholding == 0)//不进行阈值处理
                    {
                        for (int i = 0; i < bytes; i++)
                        {
                            if (tempArray[i] < 0)
                                grayValues[i] = 0;
                            else
                            {
                                if (tempArray[i] > 255)
                                    grayValues[i] = 255;
                                else
                                    grayValues[i] = Convert.ToByte(tempArray[i]);
                            }
                        }
                    }
                    else//阈值处理，生成二值边缘图像
                    {
                        if (flagMask == 3 || flagMask == 4 || flagMask == 5)
                        {
                            zerocross(ref tempArray, out grayValues, thresholding);
                        }
                        else
                        {
                            for (int i = 0; i < bytes; i++)
                            {
                                if (tempArray[i] > thresholding)
                                    grayValues[i] = 255;
                                else
                                    grayValues[i] = 0;
                            }
                        }                        
                    }

                    System.Runtime.InteropServices.Marshal.Copy(grayValues, 0, ptr, bytes);
                    curBitmap.UnlockBits(bmpData);
                }

                Invalidate();
            }
        }

        private void gaussian_Click(object sender, EventArgs e)
        {
            if (curBitmap != null)
            {
                gaussian gaussFilter = new gaussian();
                if (gaussFilter.ShowDialog() == DialogResult.OK)
                {
                    Rectangle rect = new Rectangle(0, 0, curBitmap.Width, curBitmap.Height);
                    System.Drawing.Imaging.BitmapData bmpData = curBitmap.LockBits(rect, System.Drawing.Imaging.ImageLockMode.ReadWrite, curBitmap.PixelFormat);
                    IntPtr ptr = bmpData.Scan0;
                    int bytes = curBitmap.Width * curBitmap.Height;
                    byte[] grayValues = new byte[bytes];
                    System.Runtime.InteropServices.Marshal.Copy(ptr, grayValues, 0, bytes);

                    double thresholding = gaussFilter.GetThresholding;
                    double sigma = gaussFilter.GetSigma;
                    bool flag = gaussFilter.GetFlag;

                    double[] filt, tempArray;
                    createFilter(out filt, sigma, flag);
                    conv2(ref grayValues, ref filt, out tempArray);
                    zerocross(ref tempArray, out grayValues, thresholding);
                    
                    System.Runtime.InteropServices.Marshal.Copy(grayValues, 0, ptr, bytes);
                    curBitmap.UnlockBits(bmpData);
                }

                Invalidate();
            }
            
        }

        private void zerocross(ref double[] inputImage, out byte[] outImage, double thresh)
        {
            outImage = new byte[curBitmap.Width * curBitmap.Height];
            for (int i = 0; i < curBitmap.Height; i++)
            {
                for (int j = 0; j < curBitmap.Width; j++)
                {
                    if (inputImage[i * curBitmap.Width + j] < 0 && inputImage[((i + 1) % curBitmap.Height) * curBitmap.Width + j] > 0 && Math.Abs(inputImage[i * curBitmap.Width + j] - inputImage[((i + 1) % curBitmap.Height) * curBitmap.Width + j]) > thresh)
                    {
                        outImage[i * curBitmap.Width + j] = 255;
                    }
                    else if (inputImage[i * curBitmap.Width + j] < 0 && inputImage[((Math.Abs(i - 1)) % curBitmap.Height) * curBitmap.Width + j] > 0 && Math.Abs(inputImage[i * curBitmap.Width + j] - inputImage[((Math.Abs(i - 1)) % curBitmap.Height) * curBitmap.Width + j]) > thresh)
                    {
                        outImage[i * curBitmap.Width + j] = 255;
                    }
                    else if (inputImage[i * curBitmap.Width + j] < 0 && inputImage[i * curBitmap.Width + ((j + 1) % curBitmap.Width)] > 0 && Math.Abs(inputImage[i * curBitmap.Width + j] - inputImage[i * curBitmap.Width + ((j + 1) % curBitmap.Width)]) > thresh)
                    {
                        outImage[i * curBitmap.Width + j] = 255;
                    }
                    else if (inputImage[i * curBitmap.Width + j] < 0 && inputImage[i * curBitmap.Width + ((Math.Abs(j - 1)) % curBitmap.Width)] > 0 && Math.Abs(inputImage[i * curBitmap.Width + j] - inputImage[i * curBitmap.Width + ((Math.Abs(j - 1)) % curBitmap.Width)]) > thresh)
                    {
                        outImage[i * curBitmap.Width + j] = 255;
                    }
                    else if (inputImage[i * curBitmap.Width + j] == 0)
                    {
                        if (inputImage[((i + 1) % curBitmap.Height) * curBitmap.Width + j] > 0 && inputImage[((Math.Abs(i - 1)) % curBitmap.Height) * curBitmap.Width + j] < 0 && Math.Abs(inputImage[((Math.Abs(i - 1)) % curBitmap.Height) * curBitmap.Width + j] - inputImage[((i + 1) % curBitmap.Height) * curBitmap.Width + j]) > 2 * thresh)
                        {
                            outImage[i * curBitmap.Width + j] = 255;
                        }
                        else if (inputImage[((i + 1) % curBitmap.Height) * curBitmap.Width + j] < 0 && inputImage[((Math.Abs(i - 1)) % curBitmap.Height) * curBitmap.Width + j] > 0 && Math.Abs(inputImage[((Math.Abs(i - 1)) % curBitmap.Height) * curBitmap.Width + j] - inputImage[((i + 1) % curBitmap.Height) * curBitmap.Width + j]) > 2 * thresh)
                        {
                            outImage[i * curBitmap.Width + j] = 255;
                        }
                        else if (inputImage[i * curBitmap.Width + ((j + 1) % curBitmap.Width)] > 0 && inputImage[i * curBitmap.Width + ((Math.Abs(j - 1)) % curBitmap.Width)] < 0 && Math.Abs(inputImage[i * curBitmap.Width + ((j + 1) % curBitmap.Width)] - inputImage[i * curBitmap.Width + ((Math.Abs(j - 1)) % curBitmap.Width)]) > 2 * thresh)
                        {
                            outImage[i * curBitmap.Width + j] = 255;
                        }
                        else if (inputImage[i * curBitmap.Width + ((j + 1) % curBitmap.Width)] < 0 && inputImage[i * curBitmap.Width + ((Math.Abs(j - 1)) % curBitmap.Width)] > 0 && Math.Abs(inputImage[i * curBitmap.Width + ((j + 1) % curBitmap.Width)] - inputImage[i * curBitmap.Width + ((Math.Abs(j - 1)) % curBitmap.Width)]) > 2 * thresh)
                        {
                            outImage[i * curBitmap.Width + j] = 255;
                        }
                        else
                        {
                            outImage[i * curBitmap.Width + j] = 0;
                        }
                    }
                    else
                    {
                        outImage[i * curBitmap.Width + j] = 0;
                    }
                }
            }
        }

        private void createFilter(out double[] filter, double sigma, bool lod)
        {
            double std2 = 2 * sigma * sigma;
            int radius = Convert.ToInt16(Math.Ceiling(3 * sigma));
            int filterWidth = 2 * radius + 1;
            filter = new double[filterWidth * filterWidth];
            double sum = 0, average = 0;
            if (lod == false)
            {
                for (int i = 0; i < radius; i++)
                {
                    for (int j = 0; j < radius; j++)
                    {
                        int xx = (j - radius) * (j - radius);
                        int yy = (i - radius) * (i - radius);
                        filter[i * filterWidth + j] = (xx + yy - std2) * Math.Exp(-(xx + yy) / std2);
                        sum += 4 * filter[i * filterWidth + j];
                    }
                }
                for (int i = 0; i < radius; i++)
                {
                    int xx = (i - radius) * (i - radius);
                    filter[i * filterWidth + radius] = (xx - std2) * Math.Exp(-xx / std2);
                    sum += 2 * filter[i * filterWidth + radius];
                }
                for (int j = 0; j < radius; j++)
                {
                    int yy = (j - radius) * (j - radius);
                    filter[radius * filterWidth + j] = (yy - std2) * Math.Exp(-yy / std2);
                    sum += 2 * filter[radius * filterWidth + j];
                }
                filter[radius * filterWidth + radius] = -std2;
                sum += filter[radius * filterWidth + radius];
                average = sum / filter.Length;
                for (int i = 0; i < radius; i++)
                {
                    for (int j = 0; j < radius; j++)
                    {
                        filter[i * filterWidth + j] = filter[i * filterWidth + j] - average;
                        filter[filterWidth - 1 - j + i * filterWidth] = filter[i * filterWidth + j];
                        filter[filterWidth - 1 - j + (filterWidth - 1 - i) * filterWidth] = filter[i * filterWidth + j];
                        filter[j + (filterWidth - 1 - i) * filterWidth] = filter[i * filterWidth + j];
                    }
                }
                for (int i = 0; i < radius; i++)
                {
                    filter[i * filterWidth + radius] = filter[i * filterWidth + radius] - average;
                    filter[(filterWidth - 1 - i) * filterWidth + radius] = filter[i * filterWidth + radius];
                }
                for (int j = 0; j < radius; j++)
                {
                    filter[radius * filterWidth + j] = filter[radius * filterWidth + j] - average;
                    filter[radius * filterWidth + filterWidth - 1 - j] = filter[radius * filterWidth + j];

                }
                filter[radius * filterWidth + radius] = filter[radius * filterWidth + radius] - average;
                
            }
            else
            {
                for (int i = 0; i < radius; i++)
                {
                    for (int j = 0; j < radius; j++)
                    {
                        int xx = (j - radius) * (j - radius);
                        int yy = (i - radius) * (i - radius);
                        filter[i * filterWidth + j] = 1.6 * Math.Exp(-(xx + yy) * 1.6 * 1.6 / std2) / sigma - Math.Exp(-(xx + yy) / std2) / sigma;
                        sum += 4 * filter[i * filterWidth + j];
                    }
                }
                for (int i = 0; i < radius; i++)
                {
                    int xx = (i - radius) * (i - radius);
                    filter[i * filterWidth + radius] = 1.6 * Math.Exp(-xx * 1.6 * 1.6 / std2) / sigma - Math.Exp(-xx / std2) / sigma; 
                    sum += 2 * filter[i * filterWidth + radius];
                }
                for (int j = 0; j < radius; j++)
                {
                    int yy = (j - radius) * (j - radius);
                    filter[radius * filterWidth + j] = 1.6 * Math.Exp(-yy * 1.6 * 1.6 / std2) / sigma - Math.Exp(-yy / std2) / sigma;
                    sum += 2 * filter[radius * filterWidth + j];
                }
                filter[radius * filterWidth + radius] = 1.6 / sigma - 1 / sigma;
                sum += filter[radius * filterWidth + radius];
                average = sum / filter.Length;
                for (int i = 0; i < radius; i++)
                {
                    for (int j = 0; j < radius; j++)
                    {
                        filter[i * filterWidth + j] = filter[i * filterWidth + j] - average;
                        filter[filterWidth - 1 - j + i * filterWidth] = filter[i * filterWidth + j];
                        filter[filterWidth - 1 - j + (filterWidth - 1 - i) * filterWidth] = filter[i * filterWidth + j];
                        filter[j + (filterWidth - 1 - i) * filterWidth] = filter[i * filterWidth + j];
                    }
                }
                for (int i = 0; i < radius; i++)
                {
                    filter[i * filterWidth + radius] = filter[i * filterWidth + radius] - average;
                    filter[(filterWidth - 1 - i) * filterWidth + radius] = filter[i * filterWidth + radius];
                }
                for (int j = 0; j < radius; j++)
                {
                    filter[radius * filterWidth + j] = filter[radius * filterWidth + j] - average;
                    filter[radius * filterWidth + filterWidth - 1 - j] = filter[radius * filterWidth + j];

                }
                filter[radius * filterWidth + radius] = filter[radius * filterWidth + radius] - average;
            }
        }

        private void conv2(ref byte[] inputImage, ref double[] mask, out double[] outImage)
        {
            int windWidth = Convert.ToInt16(Math.Sqrt(mask.Length));
            int radius = windWidth / 2;
            double temp;
            outImage = new double[curBitmap.Width * curBitmap.Height];

            for (int i = 0; i < curBitmap.Height; i++)
            {
                for (int j = 0; j < curBitmap.Width; j++)
                {
                    temp = 0;
                    for (int x = -radius; x <= radius; x++)
                    {
                        for (int y = -radius; y <= radius; y++)
                        {
                            temp += inputImage[((Math.Abs(i + x)) % curBitmap.Height) * curBitmap.Width + (Math.Abs(j + y)) % curBitmap.Width] * mask[(x + radius) * windWidth + y + radius];
                        }
                    }
                    outImage[i * curBitmap.Width + j] = temp;
                }
            }
        }

        private void canny_Click(object sender, EventArgs e)
        {
            if (curBitmap != null)
            {
                canny cannyOp = new canny();
                if (cannyOp.ShowDialog() == DialogResult.OK)
                {
                    Rectangle rect = new Rectangle(0, 0, curBitmap.Width, curBitmap.Height);
                    System.Drawing.Imaging.BitmapData bmpData = curBitmap.LockBits(rect, System.Drawing.Imaging.ImageLockMode.ReadWrite, curBitmap.PixelFormat);
                    IntPtr ptr = bmpData.Scan0;
                    int bytes = curBitmap.Width * curBitmap.Height;
                    byte[] grayValues = new byte[bytes];
                    System.Runtime.InteropServices.Marshal.Copy(ptr, grayValues, 0, bytes);

                    byte[] thresholding = new byte[2];
                    thresholding = cannyOp.GetThresh;
                    double sigma = cannyOp.GetSigma;

                    double[] tempArray;// = new double[bytes];
                    double[] tempImage = new double[bytes];
                    double[] grad = new double[bytes];
                    byte[] aLabel = new byte[bytes];
                    double[] edgeMap = new double[bytes];
                    Array.Clear(edgeMap, 0, bytes);
                    double gradX, gradY, angle;
                    int rad = Convert.ToInt16(Math.Ceiling(3 * sigma));
                    for (int i = 0; i < bytes; i++)
                        tempImage[i] = Convert.ToDouble(grayValues[i]);

                    gaussSmooth(tempImage, out tempArray, sigma);
                    for (int i = 0; i < curBitmap.Height; i++)
                    {
                        for (int j = 0; j < curBitmap.Width; j++)
                        {
                            gradX = tempArray[((Math.Abs(i - 1)) % curBitmap.Height) * curBitmap.Width + ((j + 1) % curBitmap.Width)] + 2 * tempArray[i * curBitmap.Width + ((j + 1) % curBitmap.Width)] + tempArray[((i + 1) % curBitmap.Height) * curBitmap.Width + ((j + 1) % curBitmap.Width)] -
                                        tempArray[((Math.Abs(i - 1)) % curBitmap.Height) * curBitmap.Width + ((Math.Abs(j - 1)) % curBitmap.Width)] - 2 * tempArray[i * curBitmap.Width + ((Math.Abs(j - 1)) % curBitmap.Width)] - tempArray[((i + 1) % curBitmap.Height) * curBitmap.Width + ((Math.Abs(j - 1)) % curBitmap.Width)];
                                    
                            gradY = tempArray[((Math.Abs(i - 1)) % curBitmap.Height) * curBitmap.Width + ((Math.Abs(j - 1)) % curBitmap.Width)] + 2 * tempArray[((Math.Abs(i - 1)) % curBitmap.Height) * curBitmap.Width + j] + tempArray[((Math.Abs(i - 1)) % curBitmap.Height) * curBitmap.Width + ((j + 1) % curBitmap.Width)] -
                                        tempArray[((i + 1) % curBitmap.Height) * curBitmap.Width + ((Math.Abs(j - 1)) % curBitmap.Width)] - 2 * tempArray[((i + 1) % curBitmap.Height) * curBitmap.Width + j] - tempArray[((i + 1) % curBitmap.Height) * curBitmap.Width + ((j + 1) % curBitmap.Width)];
                                    
                            grad[i * curBitmap.Width + j] = Math.Sqrt(gradX * gradX + gradY * gradY);

                            angle = Math.Atan2(gradY, gradX);
                            if ((angle >= -1.178097 && angle < 1.178097) || angle >= 2.748894 || angle < -2.748894)
                                aLabel[i * curBitmap.Width + j] = 0;
                            else if ((angle >= 0.392699 && angle < 1.178097) || (angle >= -2.748894 && angle < -1.963495))
                                aLabel[i * curBitmap.Width + j] = 1;
                            else if ((angle >= -1.178097 && angle < -0.392699) || (angle >= 1.963495 && angle < 2.748894))
                                aLabel[i * curBitmap.Width + j] = 2;
                            else 
                                aLabel[i * curBitmap.Width + j] = 3;
                        }
                    }
                    for (int i = 0; i < curBitmap.Height; i++)
                    {
                        for (int j = 0; j < curBitmap.Width; j++)
                        {
                            switch (aLabel[i * curBitmap.Width + j])
                            {
                                case 3://水平方向
                                    if (grad[i * curBitmap.Width + j] > grad[((Math.Abs(i - 1))%curBitmap.Height) * curBitmap.Width + j] && grad[i * curBitmap.Width + j] > grad[((i + 1)%curBitmap.Height) * curBitmap.Width + j])
                                        edgeMap[i * curBitmap.Width + j] = grad[i * curBitmap.Width + j];
                                    break;
                                case 2://正45度方向
                                    if (grad[i * curBitmap.Width + j] > grad[((Math.Abs(i - 1)) % curBitmap.Height) * curBitmap.Width + (Math.Abs(j - 1) % curBitmap.Width)] && grad[i * curBitmap.Width + j] > grad[((i + 1) % curBitmap.Height) * curBitmap.Width + ((j + 1) % curBitmap.Width)])
                                        edgeMap[i * curBitmap.Width + j] = grad[i * curBitmap.Width + j];
                                    break;
                                case 1://负45度方向
                                    if (grad[i * curBitmap.Width + j] > grad[((Math.Abs(i - 1)) % curBitmap.Height) * curBitmap.Width + ((j + 1) % curBitmap.Width)] && grad[i * curBitmap.Width + j] > grad[((i + 1) % curBitmap.Height) * curBitmap.Width + (Math.Abs(j - 1) % curBitmap.Width)])
                                        edgeMap[i * curBitmap.Width + j] = grad[i * curBitmap.Width + j];
                                    break;
                                case 0://垂直方向
                                    if (grad[i * curBitmap.Width + j] > grad[i * curBitmap.Width + (Math.Abs(j - 1) % curBitmap.Width)] && grad[i * curBitmap.Width + j] > grad[i * curBitmap.Width + ((j + 1) % curBitmap.Width)])
                                        edgeMap[i * curBitmap.Width + j] = grad[i * curBitmap.Width + j];
                                    break;
                                default:
                                    return;
                            }
                        }
                    }

                    Array.Clear(grayValues, 0, bytes);
                    for (int i = 0; i < curBitmap.Height; i++)
                    {
                        for (int j =0; j < curBitmap.Width; j++)
                        {
                            if (edgeMap[i * curBitmap.Width + j] > thresholding[0])
                            {
                                grayValues[i * curBitmap.Width + j] = 255;
                                traceEdge(i, j, edgeMap, ref grayValues, thresholding[1]);
                            }
                        }
                    }
                    System.Runtime.InteropServices.Marshal.Copy(grayValues, 0, ptr, bytes);
                    curBitmap.UnlockBits(bmpData);
                }

                Invalidate();
            }
        }

        private void gaussSmooth(double[] inputImage, out double[] outputImage, double sigma)
        {
            double std2 = 2 * sigma * sigma;
            int radius = Convert.ToInt16(Math.Ceiling(3 * sigma));
            int filterWidth = 2 * radius + 1;
            double[] filter = new double[filterWidth];
            outputImage = new double[inputImage.Length];
            int length = Convert.ToInt16(Math.Sqrt(inputImage.Length));
            double[] tempImage = new double[inputImage.Length];

            double sum = 0;
            for (int i = 0; i < filterWidth; i++)
            {
                int xx = (i - radius) * (i - radius);
                filter[i] = Math.Exp(-xx / std2);
                sum += filter[i];
            }
            for (int i = 0; i < filterWidth; i++)
            {
                filter[i] = filter[i] / sum;
            }

            for (int i = 0; i < length; i++)
            {
                for (int j = 0; j < length; j++)
                {
                    double temp = 0;
                    for (int k = -radius; k <= radius; k++)
                    {
                        int rem = (Math.Abs(j + k)) % length;
                        temp += inputImage[i * length + rem] * filter[k + radius];
                    }
                    tempImage[i * length + j] = temp;
                }
            }
            for (int j = 0; j < length; j++)
            {
                for (int i = 0; i < length; i++)
                {
                    double temp = 0;
                    for (int k = -radius; k <= radius; k++)
                    {
                        int rem = (Math.Abs(i + k)) % length;
                        temp += tempImage[rem * length + j] * filter[k + radius];
                    }
                    outputImage[i * length + j] = temp;
                }
            }
        }

        private void traceEdge(int k, int l, double[] inputImage, ref byte[] outputImage, byte thrLow)
        {
            int[] kOffset = new int[] { 1, 1, 0, -1, -1, -1, 0, 1 };
            int[] lOffset = new int[] { 0, 1, 1, 1, 0, -1, -1, -1 };
            int kk, ll;
            for (int p = 0; p < 8; p++)
            {
                kk = k + kOffset[p];
                kk = Math.Abs(kk) % curBitmap.Height;
                ll = l + lOffset[p];
                ll = Math.Abs(ll) % curBitmap.Width;
                if (outputImage[ll * curBitmap.Width + kk] != 255)
                {
                    if (inputImage[ll * curBitmap.Width + kk] > thrLow)
                    {
                        outputImage[ll * curBitmap.Width + kk] = 255;
                        traceEdge(ll, kk, inputImage, ref outputImage, thrLow);
                    }
                }
            }
        }

        private void morph_Click(object sender, EventArgs e)
        {
            if (curBitmap != null)
            {
                morphologic grayMor = new morphologic();
                if (grayMor.ShowDialog() == DialogResult.OK)
                {
                    Rectangle rect = new Rectangle(0, 0, curBitmap.Width, curBitmap.Height);
                    System.Drawing.Imaging.BitmapData bmpData = curBitmap.LockBits(rect, System.Drawing.Imaging.ImageLockMode.ReadWrite, curBitmap.PixelFormat);
                    IntPtr ptr = bmpData.Scan0;
                    int bytes = curBitmap.Width * curBitmap.Height;
                    byte[] grayValues = new byte[bytes];
                    System.Runtime.InteropServices.Marshal.Copy(ptr, grayValues, 0, bytes);

                    byte[] tempArray1 = new byte[bytes];
                    byte[] tempArray2 = new byte[bytes];
                    bool flag = grayMor.GetMethod;
                    double thresh = grayMor.GetThresh;
                    byte[] struEle = new byte[25];
                    struEle = grayMor.GetStruction;
                    int temp;

                    tempArray1 = grayDelation(grayValues, struEle, curBitmap.Height, curBitmap.Width);
                    tempArray2 = grayErode(grayValues, struEle, curBitmap.Height, curBitmap.Width);

                    for (int i = 0; i < bytes; i++)
                    {
                        if (flag == false)
                            temp = (tempArray1[i] - tempArray2[i]) / 2;
                        else
                            temp = (tempArray1[i] + tempArray2[i] - 2 * grayValues[i]) / 2;

                        if (temp > thresh)
                            grayValues[i] = 255;
                        else
                            grayValues[i] = 0;
                    }

                    System.Runtime.InteropServices.Marshal.Copy(grayValues, 0, ptr, bytes);
                    curBitmap.UnlockBits(bmpData);
                }

                Invalidate();
            }
        }

        private byte[] grayDelation(byte[] grayImage, byte[] se, int tHeight, int tWidth)
        {
            byte[] tempImage = new byte[grayImage.Length];

            for (int i = 0; i < tHeight; i++)
            {
                for (int j = 0; j < tWidth; j++)
                {
                    int[] cou = new int[]{grayImage[((Math.Abs(i - 2)) % tHeight) * tWidth + (Math.Abs(j - 2)) % tWidth] * se[0],
								        grayImage[((Math.Abs(i - 2)) % tHeight) * tWidth + (Math.Abs(j - 1)) % tWidth] * se[1],
								        grayImage[((Math.Abs(i - 2)) % tHeight) * tWidth + j] * se[2],
								        grayImage[((Math.Abs(i - 2)) % tHeight) * tWidth + ((j + 1) % tWidth)] * se[3],
								        grayImage[((Math.Abs(i - 2)) % tHeight) * tWidth + ((j + 2) % tWidth)] * se[4],
								        grayImage[((Math.Abs(i - 1)) % tHeight) * tWidth + (Math.Abs(j - 2) % tWidth)] * se[5],
								        grayImage[((Math.Abs(i - 1)) % tHeight) * tWidth + (Math.Abs(j - 1) % tWidth)] * se[6],
								        grayImage[((Math.Abs(i - 1)) % tHeight) * tWidth + j] * se[7],
								        grayImage[((Math.Abs(i - 1)) % tHeight) * tWidth + ((j + 1) % tWidth)] * se[8],
								        grayImage[((Math.Abs(i - 1)) % tHeight) * tWidth + ((j + 2) % tWidth)] * se[9],
								        grayImage[i * tWidth + (Math.Abs(j - 2) % tWidth)] * se[10],
								        grayImage[i * tWidth + (Math.Abs(j - 1) % tWidth)] * se[11],
								        grayImage[i * tWidth + j] * se[12],
								        grayImage[i * tWidth + ((j + 1) % tWidth)] * se[13],
								        grayImage[i * tWidth + ((j + 2) % tWidth)] * se[14],								        
								        grayImage[((i + 1) % tHeight) * tWidth + (Math.Abs(j - 2) % tWidth)] * se[15],
								        grayImage[((i + 1) % tHeight) * tWidth + (Math.Abs(j - 1) % tWidth)] * se[16],
								        grayImage[((i + 1) % tHeight) * tWidth + j] * se[17],
								        grayImage[((i + 1) % tHeight) * tWidth + ((j + 1) % tWidth)] * se[18],
								        grayImage[((i + 1) % tHeight) * tWidth + ((j + 2) % tWidth)] * se[19],
                                        grayImage[((i + 2) % tHeight) * tWidth + (Math.Abs(j - 2) % tWidth)] * se[20],
								        grayImage[((i + 2) % tHeight) * tWidth + (Math.Abs(j - 1) % tWidth)] * se[21],
								        grayImage[((i + 2) % tHeight) * tWidth + j] * se[22],
								        grayImage[((i + 2) % tHeight) * tWidth + ((j + 1) % tWidth)] * se[23],
								        grayImage[((i + 2) % tHeight) * tWidth + ((j + 2) % tWidth)] * se[24]};
                    int maxim = cou[0];
                    for (int k = 1; k < 25; k++)
                    {
                        if (cou[k] > maxim)
                        {
                            maxim = cou[k];
                        }
                    }
                    tempImage[i * tWidth + j] = (byte)maxim;
                }
            }

            return tempImage;
        }

        private byte[] grayErode(byte[] grayImage, byte[] se, int tHeight, int tWidth)
        {
            byte[] tempImage = new byte[grayImage.Length];

            byte[] tempSe = new byte[25];
            tempSe = (byte[])se.Clone();
            for (int k = 0; k < 25; k++)
            {
                if (tempSe[k] == 0)
                    tempSe[k] = 255;
            }
            for (int i = 0; i < tHeight; i++)
            {
                for (int j = 0; j < tWidth; j++)
                {
                    int[] cou = new int[]{grayImage[((Math.Abs(i - 2)) % tHeight) * tWidth + (Math.Abs(j - 2)) % tWidth] * tempSe[0],
								        grayImage[((Math.Abs(i - 2)) % tHeight) * tWidth + (Math.Abs(j - 1)) % tWidth] * tempSe[1],
								        grayImage[((Math.Abs(i - 2)) % tHeight) * tWidth + j] * tempSe[2],
								        grayImage[((Math.Abs(i - 2)) % tHeight) * tWidth + ((j + 1) % tWidth)] * tempSe[3],
								        grayImage[((Math.Abs(i - 2)) % tHeight) * tWidth + ((j + 2) % tWidth)] * tempSe[4],
								        grayImage[((Math.Abs(i - 1)) % tHeight) * tWidth + (Math.Abs(j - 2) % tWidth)] * tempSe[5],
								        grayImage[((Math.Abs(i - 1)) % tHeight) * tWidth + (Math.Abs(j - 1) % tWidth)] * tempSe[6],
								        grayImage[((Math.Abs(i - 1)) % tHeight) * tWidth + j] * tempSe[7],
								        grayImage[((Math.Abs(i - 1)) % tHeight) * tWidth + ((j + 1) % tWidth)] * tempSe[8],
								        grayImage[((Math.Abs(i - 1)) % tHeight) * tWidth + ((j + 2) % tWidth)] * tempSe[9],
								        grayImage[i * tWidth + (Math.Abs(j - 2) % tWidth)] * tempSe[10],
								        grayImage[i * tWidth + (Math.Abs(j - 1) % tWidth)] * tempSe[11],
								        grayImage[i * tWidth + j] * tempSe[12],
								        grayImage[i * tWidth + ((j + 1) % tWidth)] * tempSe[13],
								        grayImage[i * tWidth + ((j + 2) % tWidth)] * tempSe[14],								        
								        grayImage[((i + 1) % tHeight) * tWidth + (Math.Abs(j - 2) % tWidth)] * tempSe[15],
								        grayImage[((i + 1) % tHeight) * tWidth + (Math.Abs(j - 1) % tWidth)] * tempSe[16],
								        grayImage[((i + 1) % tHeight) * tWidth + j] * tempSe[17],
								        grayImage[((i + 1) % tHeight) * tWidth + ((j + 1) % tWidth)] * tempSe[18],
								        grayImage[((i + 1) % tHeight) * tWidth + ((j + 2) % tWidth)] * tempSe[19],
                                        grayImage[((i + 2) % tHeight) * tWidth + (Math.Abs(j - 2) % tWidth)] * tempSe[20],
								        grayImage[((i + 2) % tHeight) * tWidth + (Math.Abs(j - 1) % tWidth)] * tempSe[21],
								        grayImage[((i + 2) % tHeight) * tWidth + j] * tempSe[22],
								        grayImage[((i + 2) % tHeight) * tWidth + ((j + 1) % tWidth)] * tempSe[23],
								        grayImage[((i + 2) % tHeight) * tWidth + ((j + 2) % tWidth)] * tempSe[24]};
                    int minimum = cou[0];
                    for (int k = 1; k < 25; k++)
                    {
                        if (cou[k] < minimum)
                        {
                            minimum = cou[k];
                        }
                    }
                    tempImage[i * tWidth + j] = (byte)minimum;
                }
            }

            return tempImage;
        }

        private void wavelet_Click(object sender, EventArgs e)
        {
            if (curBitmap != null)
            {
                wvl wavelet = new wvl();
                if (wavelet.ShowDialog() == DialogResult.OK)
                {
                    Rectangle rect = new Rectangle(0, 0, curBitmap.Width, curBitmap.Height);
                    System.Drawing.Imaging.BitmapData bmpData = curBitmap.LockBits(rect, System.Drawing.Imaging.ImageLockMode.ReadWrite, curBitmap.PixelFormat);
                    IntPtr ptr = bmpData.Scan0;
                    int bytes = curBitmap.Width * curBitmap.Height;
                    byte[] grayValues = new byte[bytes];
                    System.Runtime.InteropServices.Marshal.Copy(ptr, grayValues, 0, bytes);

                    double[] tempArray1 = new double[bytes];
                    double[] tempArray2 = new double[bytes];
                    double[] tempArray3 = new double[bytes];
                    double[] gradX = new double[bytes];
                    double[] gradY = new double[bytes];

                    byte multiscale = wavelet.GetScale;
                    byte[] thresholding = new byte[2];
                    thresholding = wavelet.GetThresh;
                    
                    for (int i = 0; i < bytes; i++)
                    {
                        tempArray1[i] = Convert.ToDouble(grayValues[i]);
                    }

                    for (int z = 0; z <= multiscale; z++)
                    {
                        double[] p = null;
                        double[] q = null;
                        switch (z)
                        {
                            case 0:
                                p = new double[] { 0.125, 0.375, 0.375, 0.125 };
                                q = new double[] { -2, 2 };
                                break;
                            case 1:
                                p = new double[] { 0.125, 0, 0.375, 0, 0.375, 0, 0.125 };
                                q = new double[] { -2, 0, 2 };
                                break;
                            case 2:
                                p = new double[] { 0.125, 0, 0, 0, 0.375, 0, 0, 0, 0.375, 0, 0, 0, 0.125 };
                                q = new double[] { -2, 0, 0, 0, 2 };
                                break;
                            case 3:
                                p = new double[] { 0.125, 0, 0, 0, 0, 0, 0, 0, 0.375, 0, 0, 0, 0, 0, 0, 0, 0.375, 0, 0, 0, 0, 0, 0, 0, 0.125 };
                                q = new double[] { -2, 0, 0, 0, 0, 0, 0, 0, 2 };
                                break;
                            default:
                                return;
                        }

                        int coff = Convert.ToInt16(Math.Pow(2, z) - 1);

                        for (int i = 0; i < curBitmap.Height; i++)
                        {
                            for (int j = 0; j < curBitmap.Width; j++)
                            {
                                double[] scl = new double[curBitmap.Width];
                                double[] wvl = new double[curBitmap.Width];
                                int temp;

                                scl[j] = 0.0;
                                wvl[j] = 0.0;
                                for (int x = -2 - 2 * coff; x < p.Length - 2 - 2 * coff; x++)
                                {
                                    temp = (Math.Abs(j + x)) % curBitmap.Width;
                                    scl[j] += p[1 + coff - x] * tempArray1[i * curBitmap.Width + temp];
                                }
                                for (int x = -1 - coff; x < q.Length - 1 - coff; x++)
                                {
                                    temp = (Math.Abs(j + x)) % curBitmap.Width;
                                    wvl[j] += q[-x] * tempArray1[i * curBitmap.Width + temp];
                                }
                           
                                tempArray2[i * curBitmap.Width + j] = scl[j];
                                gradX[i * curBitmap.Width + j] = wvl[j];
                            }
                        }

                        for (int i = 0; i < curBitmap.Width; i++)
                        {
                            for (int j = 0; j < curBitmap.Height; j++)
                            {
                                double[] scl = new double[curBitmap.Height];
                                double[] wvl = new double[curBitmap.Height];
                                int temp;

                                scl[j] = 0.0;
                                wvl[j] = 0.0;
                                for (int x = -2 - 2 * coff; x < p.Length - 2 - 2 * coff; x++)
                                {
                                    temp = (Math.Abs(j + x)) % curBitmap.Height;
                                    scl[j] += p[1 + coff - x] * tempArray2[temp * curBitmap.Width + i];
                                }
                                for (int x = -1 - coff; x < q.Length - 1 - coff; x++)
                                {
                                    temp = (Math.Abs(j + x)) % curBitmap.Height;
                                    wvl[j] += q[-x] * tempArray1[temp * curBitmap.Width + i];
                                }

                                tempArray3[j * curBitmap.Width + i] = scl[j];
                                gradY[j * curBitmap.Width + i] = wvl[j];
                            }
                        }

                        tempArray1 = (double[])tempArray3.Clone();
                    }

                    double angle;
                    for (int i = 0; i < curBitmap.Height; i++)
                    {
                        for (int j = 0; j < curBitmap.Width; j++)
                        {
                            tempArray1[i * curBitmap.Width + j] = Math.Sqrt(gradX[i * curBitmap.Width + j] * gradX[i * curBitmap.Width + j] + gradY[i * curBitmap.Width + j] * gradY[i * curBitmap.Width + j]);
                            angle = Math.Atan2(gradY[i * curBitmap.Width + j], gradX[i * curBitmap.Width + j]);
                            if ((angle >= -1.178097 && angle < 1.178097) || angle >= 2.748894 || angle < -2.748894)
                                tempArray2[i * curBitmap.Width + j] = 0;
                            else if ((angle >= 0.392699 && angle < 1.178097) || (angle >= -2.748894 && angle < -1.963495))
                                tempArray2[i * curBitmap.Width + j] = 1;
                            else if ((angle >= -1.178097 && angle < -0.392699) || (angle >= 1.963495 && angle < 2.748894))
                                tempArray2[i * curBitmap.Width + j] = 2;
                            else
                                tempArray2[i * curBitmap.Width + j] = 3;
                        }
                    }


                    Array.Clear(tempArray3, 0, bytes);
                    for (int i = 0; i < curBitmap.Height; i++)
                    {
                        for (int j = 0; j < curBitmap.Width; j++)
                        {
                            switch (Convert.ToInt16(tempArray2[i * curBitmap.Width + j]))
                            {
                                case 3://水平方向
                                    if (tempArray1[i * curBitmap.Width + j] > tempArray1[((Math.Abs(i - 1)) % curBitmap.Height) * curBitmap.Width + j] && tempArray1[i * curBitmap.Width + j] > tempArray1[((i + 1) % curBitmap.Height) * curBitmap.Width + j])                                        
                                        tempArray3[i * curBitmap.Width + j] = tempArray1[i * curBitmap.Width + j];
                                    break;
                                case 1://正45度方向
                                    if (tempArray1[i * curBitmap.Width + j] > tempArray1[((Math.Abs(i - 1)) % curBitmap.Height) * curBitmap.Width + (Math.Abs(j - 1) % curBitmap.Width)] && tempArray1[i * curBitmap.Width + j] > tempArray1[((i + 1) % curBitmap.Height) * curBitmap.Width + ((j + 1) % curBitmap.Width)])
                                        tempArray3[i * curBitmap.Width + j] = tempArray1[i * curBitmap.Width + j];
                                    break;
                                case 2://负45度方向
                                    if (tempArray1[i * curBitmap.Width + j] > tempArray1[((Math.Abs(i - 1)) % curBitmap.Height) * curBitmap.Width + ((j + 1) % curBitmap.Width)] && tempArray1[i * curBitmap.Width + j] > tempArray1[((i + 1) % curBitmap.Height) * curBitmap.Width + (Math.Abs(j - 1) % curBitmap.Width)])
                                        tempArray3[i * curBitmap.Width + j] = tempArray1[i * curBitmap.Width + j];
                                    break;
                                case 0://垂直方向
                                    if (tempArray1[i * curBitmap.Width + j] > tempArray1[i * curBitmap.Width + (Math.Abs(j - 1) % curBitmap.Width)] && tempArray1[i * curBitmap.Width + j] > tempArray1[i * curBitmap.Width + ((j + 1) % curBitmap.Width)])
                                        tempArray3[i * curBitmap.Width + j] = tempArray1[i * curBitmap.Width + j];
                                    break;
                                default:
                                    return;
                            }
                        }
                    }

                    Array.Clear(grayValues, 0, bytes);
                    for (int i = 0; i < curBitmap.Height; i++)
                    {
                        for (int j = 0; j < curBitmap.Width; j++)
                        {
                            if (tempArray3[i * curBitmap.Width + j] > thresholding[0])
                            {
                                grayValues[i * curBitmap.Width + j] = 255;
                                traceEdge(i, j, tempArray3, ref grayValues, thresholding[1]);
                            }
                        }
                    }
                                        
                    System.Runtime.InteropServices.Marshal.Copy(grayValues, 0, ptr, bytes);
                    curBitmap.UnlockBits(bmpData);
                }

                Invalidate();
            }
        }

        private void pyramid_Click(object sender, EventArgs e)
        {
            if (curBitmap != null)
            {
                int series = Convert.ToInt16(Math.Log(curBitmap.Width, 2));
                glp pyramid = new glp(series);
                if (pyramid.ShowDialog() == DialogResult.OK)
                {
                    Rectangle rect = new Rectangle(0, 0, curBitmap.Width, curBitmap.Height);
                    System.Drawing.Imaging.BitmapData bmpData = curBitmap.LockBits(rect, System.Drawing.Imaging.ImageLockMode.ReadWrite, curBitmap.PixelFormat);
                    IntPtr ptr = bmpData.Scan0;
                    int bytes = curBitmap.Width * curBitmap.Height;
                    byte[] grayValues = new byte[bytes];
                    System.Runtime.InteropServices.Marshal.Copy(ptr, grayValues, 0, bytes);

                    double thresh = pyramid.GetThresh;
                    byte level = pyramid.GetLevel;
                    double sigma = pyramid.GetSigma;

                    double[][] pyramidImage = new double[level + 1][];
                    double[][] passImage = new double[level + 1][];
                    int levelBytes = bytes;
                    for (int k = 0; k < level + 1; k++)
                    {
                        passImage[k] = new double[levelBytes];
                        pyramidImage[k] = new double[levelBytes];
                        levelBytes = levelBytes / 4;
                    }
                    for (int i = 0; i < bytes; i++)
                        pyramidImage[0][i] = Convert.ToDouble(grayValues[i]);

                    for (int k = 0; k < level; k++)
                    {
                        double[] tempImage = null;
                        gaussSmooth(pyramidImage[k], out tempImage, sigma);
                        int coff = pyramidImage[k].Length;
                        for (int i = 0; i < coff; i++)
                        {
                            passImage[k][i] = pyramidImage[k][i] - tempImage[i];
                            int div = i / Convert.ToInt16(Math.Sqrt(coff));
                            int rem = i % Convert.ToInt16(Math.Sqrt(coff));
                            if (div % 2 == 0 && rem % 2 == 0)
                            {
                                int j = (int)((div / 2) * Math.Sqrt(pyramidImage[k + 1].Length) + rem / 2);
                                pyramidImage[k + 1][j] = tempImage[i];
                            }
                        }
                    }

                    for (int k = level - 1; k >= 0; k--)
                    {
                        int coff = pyramidImage[k].Length;
                        for (int i = 0; i < coff; i++)
                        {
                            int div = i / Convert.ToInt16(Math.Sqrt(coff));
                            int rem = i % Convert.ToInt16(Math.Sqrt(coff));
                            int j = (int)((div / 2) * Math.Sqrt(pyramidImage[k + 1].Length) + rem / 2);
                            if (div % 2 == 0 && rem % 2 == 0)
                                pyramidImage[k][i] = pyramidImage[k + 1][j];
                            else
                                pyramidImage[k][i] = 0;
                        }
                        double[] tempImage = null;
                        gaussSmooth(pyramidImage[k], out tempImage, 1);
                        for (int i = 0; i < coff; i++)
                            pyramidImage[k][i] = tempImage[i] + passImage[k][i];
                    }

                    zerocross(ref pyramidImage[0], out grayValues,thresh);
                    
                    System.Runtime.InteropServices.Marshal.Copy(grayValues, 0, ptr, bytes);
                    curBitmap.UnlockBits(bmpData);
                }

                Invalidate();
            }
        }
    }
}