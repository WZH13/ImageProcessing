using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace segmentatioin
{
    public partial class hough : Form
    {
        public hough(Bitmap bmp)
        {
            InitializeComponent();
            bmpHough = bmp;
            maxHough = 0;
            houghMap = new int[180, 180];
        }

        private void hough_Load(object sender, EventArgs e)
        {
            Rectangle rect = new Rectangle(0, 0, bmpHough.Width, bmpHough.Height);
            System.Drawing.Imaging.BitmapData bmpData = bmpHough.LockBits(rect, System.Drawing.Imaging.ImageLockMode.ReadWrite, bmpHough.PixelFormat);
            IntPtr ptr = bmpData.Scan0;
            int bytes = bmpHough.Width * bmpHough.Height;
            byte[] grayValues = new byte[bytes];
            System.Runtime.InteropServices.Marshal.Copy(ptr, grayValues, 0, bytes);
            bmpHough.UnlockBits(bmpData);

            Array.Clear(houghMap, 0, 32400);
            for (int i = 0; i < bmpHough.Height - 1; i++)
            {
                for (int j = 0; j < bmpHough.Width - 1; j++)
                {
                    if (grayValues[i * bmpHough.Width + j] == 255)
                    {
                        for (int thet = 0; thet < 180; thet++)
                        {
                            double arc = thet * Math.PI / 180;
                            int rho = Convert.ToInt16((j * Math.Cos(arc) + i * Math.Sin(arc)) / 8) + 90; 
                            houghMap[thet, rho]++;
                            if (maxHough < houghMap[thet, rho])
                                maxHough = houghMap[thet, rho];
                        }
                    }
                }
            }            
        }

        private void close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void hough_Paint(object sender, PaintEventArgs e)
        {
            Bitmap houghImage = new Bitmap(180, 180, System.Drawing.Imaging.PixelFormat.Format8bppIndexed);
            System.Drawing.Imaging.ColorPalette cp = houghImage.Palette;
            for (int i = 0; i < 256; i++)
            {
                cp.Entries[i] = Color.FromArgb(i, i, i);
            }
            houghImage.Palette = cp;

            Rectangle rectHough = new Rectangle(0, 0, 180, 180);
            System.Drawing.Imaging.BitmapData houghData = houghImage.LockBits(rectHough, System.Drawing.Imaging.ImageLockMode.ReadWrite, houghImage.PixelFormat);
            IntPtr ptr = houghData.Scan0;
            int bytes = 180 * 180;
            byte[] grayHough = new byte[bytes];

            for (int i = 0; i < 180; i++)
            {
                for (int j = 0; j < 180; j++)
                {
                    grayHough[i * 180 + j] = Convert.ToByte(houghMap[i, j] * 255 / maxHough);
                }
            }

            System.Runtime.InteropServices.Marshal.Copy(grayHough, 0, ptr, bytes);
            houghImage.UnlockBits(houghData);

            Graphics g = e.Graphics;
            g.DrawImage(houghImage, 40, 20, 180, 180);

            g.FillRectangle(Brushes.Black, 250, 20, 180, 180);

            double thresholding = maxHough * 0.6;
            double k, b;
            int x1, x2, y1, y2;
            
            for (int i = 0; i < 180; i++)
            {
                for (int j = 0; j < 180; j++)
                {
                    if (houghMap[i, j] > thresholding)
                    {
                        if (i == 90)
                        {
                            g.DrawLine(Pens.White, 250, 200, 250, 20);
                        }
                        else if (i == 0)
                            g.DrawLine(Pens.White, 250, 430, 250, 20);
                        else
                        {
                            k = (-1 / Math.Tan(i * Math.PI / 180));
                            b = ((j - 90) * 8 / Math.Sin(i * Math.PI / 180)) *180 / 512;
                            
                            if (b >= 0 && b <= 180)
                            {
                                x1 = 0;
                                y1 = Convert.ToInt16(b);
                                double temp = -b / k;
                                if (temp <= 180 && temp >= 0)
                                {
                                    x2 = Convert.ToInt16(temp);
                                    y2 = 0;
                                }
                                else if (temp >= -180 && temp < 0)
                                {
                                    x2 = Convert.ToInt16((180 - b) / k);
                                    y2 = 180;
                                }
                                else
                                {
                                    x2 = 180;
                                    y2 = Convert.ToInt16(180 * k + b);
                                }
                            }
                            else if (b < 0)
                            {
                                x1 = Convert.ToInt16(-b / k);
                                y1 = 0;
                                double temp = k * 180 + b;
                                if (temp >= 0 && temp <= 180)
                                {
                                    x2 = 180;
                                    y2 = Convert.ToInt16(temp);
                                }
                                else
                                {
                                    x2 = Convert.ToInt16((180 - b) / k);
                                    y2 = 180;
                                }
                            }
                            else
                            {
                                x1 = Convert.ToInt16((180 - b) / k);
                                y1 = 180;
                                double temp = k * 180 + b;
                                if (temp >= 0 && temp <= 180)
                                {
                                    x2 = 180;
                                    y2 = Convert.ToInt16(temp);
                                }
                                else
                                {
                                    x2 = Convert.ToInt16(-b / k);
                                    y2 = 0;
                                }
                            }
                            g.DrawLine(Pens.White, x1 + 250, y1 + 20, x2 + 250, y2 + 20);

                        }
                    }
                }                
            }
            g.DrawString("Hough变换映射图像", new Font("Arial", 8), Brushes.Black, 80, 210);
            g.DrawString("Hough反变换图像", new Font("Arial", 8), Brushes.Black, 290, 210);
        }
    }
}