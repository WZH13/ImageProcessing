using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace color
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            redCom = 0;
            greenCom = 0;
            blueCom = 0;
            hueCom = 0;
            satCom = 0;
            intCom = 0;
            tempArray = null;
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

        private void tranSpace_Click(object sender, EventArgs e)
        {
            if (curBitmap != null)
            {
                colorSpace clS = new colorSpace();
                if (clS.ShowDialog() == DialogResult.OK)
                {
                    Rectangle rect = new Rectangle(0, 0, curBitmap.Width, curBitmap.Height);
                    System.Drawing.Imaging.BitmapData bmpData = curBitmap.LockBits(rect, System.Drawing.Imaging.ImageLockMode.ReadWrite, curBitmap.PixelFormat);
                    IntPtr ptr = bmpData.Scan0;
                    int bytes = curBitmap.Width * curBitmap.Height;
                    byte[] rgbValues = new byte[bytes * 3];
                    System.Runtime.InteropServices.Marshal.Copy(ptr, rgbValues, 0, bytes * 3);
                    curBitmap.UnlockBits(bmpData);

                    byte colorCom = clS.GetCom;
                    byte[] grayValues = new byte[bytes];
                    byte tempB;
                    double tempD;
                    switch (colorCom)
                    {
                        case 0://red
                            for (int i = 0; i < bytes; i++)
                                grayValues[i] = rgbValues[i * 3 + 2];                                
                            break;
                        case 1://green
                            for (int i = 0; i < bytes; i++)
                                grayValues[i] = rgbValues[i * 3 + 1];
                            break;
                        case 2://blue;
                            for (int i = 0; i < bytes; i++)
                                grayValues[i] = rgbValues[i * 3];
                            break;
                        case 3://hue
                            double theta;
                            for (int i = 0; i < bytes; i++)
                            {
                                theta = Math.Acos(0.5 * ((rgbValues[i * 3 + 2] - rgbValues[i * 3 + 1]) + (rgbValues[i * 3 + 2] - rgbValues[i * 3])) / Math.Sqrt((rgbValues[i * 3 + 2] - rgbValues[i * 3 + 1]) * (rgbValues[i * 3 + 2] - rgbValues[i * 3 + 1]) + (rgbValues[i * 3 + 2] - rgbValues[i * 3]) * (rgbValues[i * 3 + 1] - rgbValues[i * 3]) + 0.0000000001)) / (2 * Math.PI);
                                tempD = (rgbValues[i * 3] <= rgbValues[i * 3 + 1]) ? theta : (1 - theta);
                                grayValues[i] = (byte)(tempD * 255);
                            }
                                break;
                        case 4://saturation
                            for (int i = 0; i < bytes; i++)
                            {
                                tempB = Math.Min(rgbValues[i * 3 + 2], rgbValues[i * 3 + 1]);
                                tempB = Math.Min(tempB, rgbValues[i * 3]);
                                tempD = 1.0 - 3.0 * tempB / (rgbValues[i * 3 + 2] + rgbValues[i * 3 + 1] + rgbValues[i * 3] + 0.0000000001);
                                grayValues[i] = (byte)(tempD * 255);
                            }
                            break;
                        case 5://intensity
                            for (int i = 0; i < bytes; i++)
                                grayValues[i] = (byte)((rgbValues[i * 3] + rgbValues[i * 3 + 1] + rgbValues[i * 3 + 2]) / 3);
                            break;
                        default:
                            break;
                    }

                    curBitmap = new Bitmap(curBitmap.Width, curBitmap.Height, System.Drawing.Imaging.PixelFormat.Format8bppIndexed);
                    System.Drawing.Imaging.ColorPalette cp = curBitmap.Palette;
                    for (int i = 0; i < 256; i++)
                    {
                        cp.Entries[i] = Color.FromArgb(i, i, i);
                    }
                    curBitmap.Palette = cp;
                    bmpData = curBitmap.LockBits(rect, System.Drawing.Imaging.ImageLockMode.ReadWrite, curBitmap.PixelFormat);
                    ptr = bmpData.Scan0;
                    System.Runtime.InteropServices.Marshal.Copy(grayValues, 0, ptr, bytes);
                    curBitmap.UnlockBits(bmpData);
                    Invalidate();
                }
            }
        }

        private void chCom_Click(object sender, EventArgs e)
        {
            if (curBitmap != null)
            {
                changeCom adjCom = new changeCom(this);
                adjustCom(255,0,0,0);
                adjCom.Show();
            }
        }
                
        public void adjustCom(byte changTab, int numCom1, int numCom2, int numCom3)
        {
            Rectangle rect = new Rectangle(0, 0, curBitmap.Width, curBitmap.Height);
            System.Drawing.Imaging.BitmapData bmpData = curBitmap.LockBits(rect, System.Drawing.Imaging.ImageLockMode.ReadWrite, curBitmap.PixelFormat);
            IntPtr ptr = bmpData.Scan0;
            int bytes = curBitmap.Width * curBitmap.Height;
            byte[] rgbValues = new byte[bytes * 3];
            System.Runtime.InteropServices.Marshal.Copy(ptr, rgbValues, 0, bytes * 3);

            if (changTab == 255)
            {
                tempArray = new byte[bytes * 3];
                tempArray = (byte[])rgbValues.Clone();
                System.Runtime.InteropServices.Marshal.Copy(rgbValues, 0, ptr, bytes * 3);
                curBitmap.UnlockBits(bmpData);
                return;
            }


            double valueCom1 = numCom1 / 100.0;
            double valueCom2 = numCom2 / 100.0;
            double valueCom3 = numCom3 / 100.0;
            double hue, sat, inten;
            double r, g, b;
            if (changTab == 0)
            {
                for (int i = 0; i < bytes; i ++)
                {
                    if (valueCom1 <= 0)
                        rgbValues[i * 3 + 2] = (byte)(tempArray[i * 3 + 2] * (1.0 + valueCom1));
                    else
                        rgbValues[i * 3 + 2] = (byte)(tempArray[i * 3 + 2] + (255.0 - tempArray[i * 3 + 2]) * valueCom1);

                    if (valueCom2 <= 0)
                        rgbValues[i * 3 + 1] = (byte)(tempArray[i * 3 + 1] * (1.0 + valueCom2));
                    else
                        rgbValues[i * 3 + 1] = (byte)(tempArray[i * 3 + 1] + (255.0 - tempArray[i * 3 + 1]) * valueCom2);

                    if (valueCom3 <= 0)
                        rgbValues[i * 3] = (byte)(tempArray[i * 3] * (1.0 + valueCom3));
                    else
                        rgbValues[i * 3] = (byte)(tempArray[i * 3] + (255.0 - tempArray[i * 3]) * valueCom3);
                }
            }
            else
            {
                valueCom1 = valueCom1 * Math.PI / 1.8;
                for (int i = 0; i < bytes; i++)
                {
                    r = tempArray[i * 3 + 2] / 255.0;
                    g = tempArray[i * 3 + 1] / 255.0;
                    b = tempArray[i * 3] / 255.0;

                    double theta = Math.Acos(0.5 * ((r - g) + (r - b)) / Math.Sqrt((r - g) * (r - g) + (r - b) * (g - b) + 0.0000001)) / (2 * Math.PI);

                    hue = (b <= g) ? theta : (1 - theta);

                    sat = 1.0 - 3.0 * Math.Min(Math.Min(r, g), b) / (r + g + b + 0.0000001);

                    inten = (r + g + b) / 3.0;

                    hue = hue * 2 * Math.PI;                    
                    hue = (hue + valueCom1 + 2 * Math.PI) % (2 * Math.PI);

                    if (valueCom2 <= 0)
                        sat = sat * (1.0 + valueCom2);
                    else
                        sat = sat + (1.0 - sat) * valueCom2;

                    if (valueCom3 <= 0)
                        inten = inten * (1.0 + valueCom3);
                    else
                    {
                        inten = inten + (1.0 - inten) * valueCom3;
                        if (sat == 1.0)
                            sat = sat * (1 - valueCom3);
                    }

                    if (sat == 0.0)
                        hue = 0;                    

                    if (hue >= 0 && hue < 2 * Math.PI / 3)
                    {
                        b = inten * (1 - sat);
                        r = inten * (1 + sat * Math.Cos(hue) / Math.Cos(Math.PI / 3 - hue));
                        g = 3 * inten - (r + b);
                    }
                    else if (hue >= 2 * Math.PI / 3 && hue < 4 * Math.PI / 3)
                    {
                        r = inten * (1 - sat);
                        g = inten * (1 + sat * Math.Cos(hue - 2 * Math.PI / 3) / Math.Cos(Math.PI - hue));
                        b = 3 * inten - (r + g);
                    }
                    else //if (h >= 4 * Math.PI / 3 && h <= 2 * Math.PI)
                    {
                        g = inten * (1 - sat);
                        b = inten * (1 + sat * Math.Cos(hue - 4 * Math.PI / 3) / Math.Cos(5 * Math.PI / 3 - hue));
                        r = 3 * inten - (g + b);
                    }
                    if (r > 1)
                        r = 1;
                    if (g > 1)
                        g = 1;
                    if (b > 1)
                        b = 1;

                    rgbValues[i * 3 + 2] = (byte)(r * 255);
                    rgbValues[i * 3 + 1] = (byte)(g * 255);
                    rgbValues[i * 3] = (byte)(b * 255);

                }
            }
            System.Runtime.InteropServices.Marshal.Copy(rgbValues, 0, ptr, bytes * 3);
            curBitmap.UnlockBits(bmpData);
            Invalidate();
        }

        private void pseudoC_Click(object sender, EventArgs e)
        {
            if (curBitmap != null)
            {
                pColor pc = new pColor();
                if (pc.ShowDialog() == DialogResult.OK)
                {
                    Rectangle rect = new Rectangle(0, 0, curBitmap.Width, curBitmap.Height);
                    System.Drawing.Imaging.BitmapData bmpData = curBitmap.LockBits(rect, System.Drawing.Imaging.ImageLockMode.ReadWrite, curBitmap.PixelFormat);
                    IntPtr ptr = bmpData.Scan0;
                    int bytes = curBitmap.Width * curBitmap.Height;
                    byte[] grayValues = new byte[bytes];
                    System.Runtime.InteropServices.Marshal.Copy(ptr, grayValues, 0, bytes);
                    curBitmap.UnlockBits(bmpData);

                    bool method = pc.GetMethod;
                    byte seg = pc.GetSeg;
                    byte[] rgbValues = new byte[bytes * 3];
                    Array.Clear(rgbValues, 0, bytes * 3);
                    byte tempB;

                    if (method == false)
                    {                      
                        for (int i = 0; i < bytes; i++)
                        {
                            byte ser = (byte)(256 / seg);
                            tempB = (byte)(grayValues[i] / ser);
                            rgbValues[i * 3 + 1] = (byte)(tempB * ser);
                            rgbValues[i * 3] = (byte)((seg - 1 - tempB) * ser);
                            rgbValues[i * 3 + 2] = 0;
                        }
                    }
                    else
                    {
                        for (int i = 0; i < bytes; i++)
                        {
                            if (grayValues[i] < 64)
                            {
                                rgbValues[i * 3 + 2] = 0;
                                rgbValues[i * 3 + 1] = (byte)(4 * grayValues[i]);
                                rgbValues[i * 3] = 255;
                            }
                            else if (grayValues[i] < 128)
                            {
                                rgbValues[i * 3 + 2] = 0;
                                rgbValues[i * 3 + 1] = 255;
                                rgbValues[i * 3] = (byte)(-4 * grayValues[i] + 2 * 255);
                            }
                            else if (grayValues[i] < 192)
                            {
                                rgbValues[i * 3 + 2] = (byte)(4 * grayValues[i] - 2 * 255);
                                rgbValues[i * 3 + 1] = 255;
                                rgbValues[i * 3] = 0;
                            }
                            else
                            {
                                rgbValues[i * 3 + 2] = 255;
                                rgbValues[i * 3 + 1] = (byte)(-4 * grayValues[i] + 4 * 255);
                                rgbValues[i * 3] = 0;
                            }
                        }
                    }

                    curBitmap = new Bitmap(curBitmap.Width, curBitmap.Height, System.Drawing.Imaging.PixelFormat.Format24bppRgb);
                    bmpData = curBitmap.LockBits(rect, System.Drawing.Imaging.ImageLockMode.ReadWrite, curBitmap.PixelFormat);
                    ptr = bmpData.Scan0;
                    System.Runtime.InteropServices.Marshal.Copy(rgbValues, 0, ptr, bytes * 3);
                    curBitmap.UnlockBits(bmpData);
                    Invalidate();
                }
            }
        }

        private void equC_Click(object sender, EventArgs e)
        {
            if (curBitmap != null)
            {
                eColor equC = new eColor();
                if (equC.ShowDialog() == DialogResult.OK)
                {
                    Rectangle rect = new Rectangle(0, 0, curBitmap.Width, curBitmap.Height);
                    System.Drawing.Imaging.BitmapData bmpData = curBitmap.LockBits(rect, System.Drawing.Imaging.ImageLockMode.ReadWrite, curBitmap.PixelFormat);
                    IntPtr ptr = bmpData.Scan0;
                    int bytes = curBitmap.Width * curBitmap.Height;
                    byte[] rgbValues = new byte[bytes * 3];
                    System.Runtime.InteropServices.Marshal.Copy(ptr, rgbValues, 0, bytes * 3);

                    bool method = equC.GetMethod;
                    if (method == false)
                    {
                        byte[] rValues = new byte[bytes];
                        byte[] gValues = new byte[bytes];
                        byte[] bValues = new byte[bytes];

                        for (int i = 0; i < bytes; i++)
                        {
                            rValues[i] = rgbValues[i * 3 + 2];
                            gValues[i] = rgbValues[i * 3 + 1];
                            bValues[i] = rgbValues[i * 3];
                        }

                        rValues = equalization(rValues);
                        gValues = equalization(gValues);
                        bValues = equalization(bValues);

                        for (int i = 0; i < bytes; i++)
                        {
                            rgbValues[i * 3 + 2] = rValues[i];
                            rgbValues[i * 3 + 1] = gValues[i];
                            rgbValues[i * 3] = bValues[i];
                        }
                    }
                    else
                    {
                        double[] hue = new double[bytes];
                        double[] sat = new double[bytes];
                        byte[] inten = new byte[bytes];
                        double r, g, b;

                        for (int i = 0; i < bytes; i++)
                        {
                            r = rgbValues[i * 3 + 2];
                            g = rgbValues[i * 3 + 1];
                            b = rgbValues[i * 3];

                            double theta = Math.Acos(0.5 * ((r - g) + (r - b)) / Math.Sqrt((r - g) * (r - g) + (r - b) * (g - b) + 1)) / (2 * Math.PI);

                            hue[i] = ((b <= g) ? theta : (1 - theta));

                            sat[i] = 1.0 - 3.0 * Math.Min(Math.Min(r, g), b) / (r + g + b + 1);

                            inten[i] = (byte)((r + g + b) / 3);
                        }

                        inten = equalization(inten);

                        for (int i = 0; i < bytes; i++)
                        {
                            r = rgbValues[i * 3 + 2];
                            g = rgbValues[i * 3 + 1];
                            b = rgbValues[i * 3];

                            hue[i] = hue[i] * 2 * Math.PI;
                            if (hue[i] >= 0 && hue[i] < 2 * Math.PI / 3)
                            {
                                b = inten[i] * (1 - sat[i]);
                                r = inten[i] * (1 + sat[i] * Math.Cos(hue[i]) / Math.Cos(Math.PI / 3 - hue[i]));
                                g = 3 * inten[i] - (r + b);
                            }
                            else if (hue[i] >= 2 * Math.PI / 3 && hue[i] < 4 * Math.PI / 3)
                            {
                                r = inten[i] * (1 - sat[i]);
                                g = inten[i] * (1 + sat[i] * Math.Cos(hue[i] - 2 * Math.PI / 3) / Math.Cos(Math.PI - hue[i]));
                                b = 3 * inten[i] - (r + g);
                            }
                            else //if (h >= 4 * Math.PI / 3 && h <= 2 * Math.PI)
                            {
                                g = inten[i] * (1 - sat[i]);
                                b = inten[i] * (1 + sat[i] * Math.Cos(hue[i] - 4 * Math.PI / 3) / Math.Cos(5 * Math.PI / 3 - hue[i]));
                                r = 3 * inten[i] - (g + b);
                            }
                            if (r > 255)
                                r = 255;
                            if (g > 255)
                                g = 255;
                            if (b > 255)
                                b = 255;

                            rgbValues[i * 3 + 2] = (byte)r;
                            rgbValues[i * 3 + 1] = (byte)g;
                            rgbValues[i * 3] = (byte)b;
                        }
                    }
                    System.Runtime.InteropServices.Marshal.Copy(rgbValues, 0, ptr, bytes * 3);
                    curBitmap.UnlockBits(bmpData);
                    Invalidate();
                }
            }
        }

        private byte[] equalization(byte[] colorArray)
        {
            byte[] comValues = new byte[colorArray.Length];
            comValues = (byte[])colorArray.Clone();
            int[] countPixel = new int[256];
            byte temp;
            int[] tempArray = new int[256];
            Array.Clear(tempArray, 0, 256);
            byte[] pixelMap = new byte[256];
            for (int i = 0; i < comValues.Length; i++)
            {
                temp = comValues[i];
                countPixel[temp]++;
            }

            // 计算各灰度级的累计分布函数
            for (int i = 0; i < 256; i++)
            {
                if (i != 0)
                {
                    tempArray[i] = tempArray[i - 1] + countPixel[i];
                }
                else
                {
                    tempArray[0] = countPixel[0];
                }

                pixelMap[i] = (byte)(255.0 * tempArray[i] / comValues.Length + 0.5);
            }

            // 灰度等级映射处理
            for (int i = 0; i < comValues.Length; i++)
            {
                temp = comValues[i];
                comValues[i] = pixelMap[temp];
            }
            return comValues;
        }

        private void smoC_Click(object sender, EventArgs e)
        {
            if (curBitmap != null)
            {
                smoothColor smoothC = new smoothColor();
                if (smoothC.ShowDialog() == DialogResult.OK)
                {
                    Rectangle rect = new Rectangle(0, 0, curBitmap.Width, curBitmap.Height);
                    System.Drawing.Imaging.BitmapData bmpData = curBitmap.LockBits(rect, System.Drawing.Imaging.ImageLockMode.ReadWrite, curBitmap.PixelFormat);
                    IntPtr ptr = bmpData.Scan0;
                    int bytes = curBitmap.Width * curBitmap.Height;
                    byte[] rgbValues = new byte[bytes * 3];
                    System.Runtime.InteropServices.Marshal.Copy(ptr, rgbValues, 0, bytes * 3);

                    bool method = smoothC.GetMethod;
                    byte sideL = smoothC.GetLength;

                    if (method == false)
                    {
                        byte[] rValues = new byte[bytes];
                        byte[] gValues = new byte[bytes];
                        byte[] bValues = new byte[bytes];

                        for (int i = 0; i < bytes; i++)
                        {
                            rValues[i] = rgbValues[i * 3 + 2];
                            gValues[i] = rgbValues[i * 3 + 1];
                            bValues[i] = rgbValues[i * 3];
                        }

                        rValues = smooth(rValues, sideL);
                        gValues = smooth(gValues, sideL);
                        bValues = smooth(bValues, sideL);

                        for (int i = 0; i < bytes; i++)
                        {
                            rgbValues[i * 3 + 2] = rValues[i];
                            rgbValues[i * 3 + 1] = gValues[i];
                            rgbValues[i * 3] = bValues[i];
                        }
                    }
                    else
                    {
                        double[] hue = new double[bytes];
                        double[] sat = new double[bytes];
                        byte[] inten = new byte[bytes];
                        double r, g, b;

                        for (int i = 0; i < bytes; i++)
                        {
                            r = rgbValues[i * 3 + 2];
                            g = rgbValues[i * 3 + 1];
                            b = rgbValues[i * 3];

                            double theta = Math.Acos(0.5 * ((r - g) + (r - b)) / Math.Sqrt((r - g) * (r - g) + (r - b) * (g - b) + 1)) / (2 * Math.PI);

                            hue[i] = ((b <= g) ? theta : (1 - theta));

                            sat[i] = 1.0 - 3.0 * Math.Min(Math.Min(r, g), b) / (r + g + b + 1);

                            inten[i] = (byte)((r + g + b) / 3);
                        }

                        inten = smooth(inten, sideL);

                        for (int i = 0; i < bytes; i++)
                        {
                            r = rgbValues[i * 3 + 2];
                            g = rgbValues[i * 3 + 1];
                            b = rgbValues[i * 3];

                            hue[i] = hue[i] * 2 * Math.PI;
                            if (hue[i] >= 0 && hue[i] < 2 * Math.PI / 3)
                            {
                                b = inten[i] * (1 - sat[i]);
                                r = inten[i] * (1 + sat[i] * Math.Cos(hue[i]) / Math.Cos(Math.PI / 3 - hue[i]));
                                g = 3 * inten[i] - (r + b);
                            }
                            else if (hue[i] >= 2 * Math.PI / 3 && hue[i] < 4 * Math.PI / 3)
                            {
                                r = inten[i] * (1 - sat[i]);
                                g = inten[i] * (1 + sat[i] * Math.Cos(hue[i] - 2 * Math.PI / 3) / Math.Cos(Math.PI - hue[i]));
                                b = 3 * inten[i] - (r + g);
                            }
                            else //if (h >= 4 * Math.PI / 3 && h <= 2 * Math.PI)
                            {
                                g = inten[i] * (1 - sat[i]);
                                b = inten[i] * (1 + sat[i] * Math.Cos(hue[i] - 4 * Math.PI / 3) / Math.Cos(5 * Math.PI / 3 - hue[i]));
                                r = 3 * inten[i] - (g + b);
                            }
                            if (r > 255)
                                r = 255;
                            if (g > 255)
                                g = 255;
                            if (b > 255)
                                b = 255;

                            rgbValues[i * 3 + 2] = (byte)r;
                            rgbValues[i * 3 + 1] = (byte)g;
                            rgbValues[i * 3] = (byte)b;
                        }
                    }
                    
                    System.Runtime.InteropServices.Marshal.Copy(rgbValues, 0, ptr, bytes * 3);
                    curBitmap.UnlockBits(bmpData);
                    Invalidate();
                }
            }
        }

        private byte[] smooth(byte[] comValues, byte sideLength)
        {
            byte halfLength = (byte)(sideLength / 2);
            byte[] comS = new byte[comValues.Length];
            comS = (byte[])comValues.Clone();
            byte[] comD = new byte[comS.Length];
            Array.Clear(comD, 0, comD.Length);
            switch (sideLength)
            {
                case 3:
                    for (int i = 0; i < curBitmap.Height; i++)
                    {
                        for (int j = 0; j < curBitmap.Width; j++)
                        {
                            comD[i * curBitmap.Width + j] = (byte)((comS[i  * curBitmap.Width + j] +
                                            comS[((Math.Abs(i - 1)) % curBitmap.Height) * curBitmap.Width + j] +
                                            comS[((i + 1) % curBitmap.Height) * curBitmap.Width + j] +
                                            comS[i * curBitmap.Width + ((j + 1) % curBitmap.Width)] +
                                            comS[i * curBitmap.Width + ((Math.Abs(j - 1)) % curBitmap.Width)] +
                                            comS[((Math.Abs(i - 1)) % curBitmap.Height) * curBitmap.Width + ((Math.Abs(j - 1)) % curBitmap.Width)] +
                                            comS[((i + 1) % curBitmap.Height) * curBitmap.Width + ((Math.Abs(j - 1)) % curBitmap.Width)] +
                                            comS[((Math.Abs(i - 1)) % curBitmap.Height) * curBitmap.Width + ((j + 1) % curBitmap.Width)] +
                                            comS[((i + 1) % curBitmap.Height) * curBitmap.Width + ((j + 1) % curBitmap.Width)]) / 9);
                        }
                    }                   
                    break;
                case 5:
                    for (int i = 0; i < curBitmap.Height; i++)
                    {
                        for (int j = 0; j < curBitmap.Width; j++)
                        {
                            comD[i * curBitmap.Width + j] = (byte)((comS[((Math.Abs(i - 2)) % curBitmap.Height) * curBitmap.Width + (Math.Abs(j - 2)) % curBitmap.Width] +
                                            comS[((Math.Abs(i - 2)) % curBitmap.Height) * curBitmap.Width + (Math.Abs(j - 1)) % curBitmap.Width] +
                                            comS[((Math.Abs(i - 2)) % curBitmap.Height) * curBitmap.Width + j] +
                                            comS[((Math.Abs(i - 2)) % curBitmap.Height) * curBitmap.Width + ((j + 1) % curBitmap.Width)] +
                                            comS[((Math.Abs(i - 2)) % curBitmap.Height) * curBitmap.Width + ((j + 2) % curBitmap.Width)] +
                                            comS[((Math.Abs(i - 1)) % curBitmap.Height) * curBitmap.Width + (Math.Abs(j - 2) % curBitmap.Width)] +
                                            comS[((Math.Abs(i - 1)) % curBitmap.Height) * curBitmap.Width + (Math.Abs(j - 1) % curBitmap.Width)] +
                                            comS[((Math.Abs(i - 1)) % curBitmap.Height) * curBitmap.Width + j] +
                                            comS[((Math.Abs(i - 1)) % curBitmap.Height) * curBitmap.Width + ((j + 1) % curBitmap.Width)] +
                                            comS[((Math.Abs(i - 1)) % curBitmap.Height) * curBitmap.Width + ((j + 2) % curBitmap.Width)] +
                                            comS[i * curBitmap.Width + (Math.Abs(j - 2) % curBitmap.Width)] +
                                            comS[i * curBitmap.Width + (Math.Abs(j - 1) % curBitmap.Width)] +
                                            comS[i * curBitmap.Width + j] +
                                            comS[i * curBitmap.Width + ((j + 1) % curBitmap.Width)] +
                                            comS[i * curBitmap.Width + ((j + 2) % curBitmap.Width)] +
                                            comS[((i + 2) % curBitmap.Height) * curBitmap.Width + (Math.Abs(j - 2) % curBitmap.Width)] +
                                            comS[((i + 2) % curBitmap.Height) * curBitmap.Width + (Math.Abs(j - 1) % curBitmap.Width)] +
                                            comS[((i + 2) % curBitmap.Height) * curBitmap.Width + j] +
                                            comS[((i + 2) % curBitmap.Height) * curBitmap.Width + ((j + 1) % curBitmap.Width)] +
                                            comS[((i + 2) % curBitmap.Height) * curBitmap.Width + ((j + 2) % curBitmap.Width)] +
                                            comS[((i + 1) % curBitmap.Height) * curBitmap.Width + (Math.Abs(j - 2) % curBitmap.Width)] +
                                            comS[((i + 1) % curBitmap.Height) * curBitmap.Width + (Math.Abs(j - 1) % curBitmap.Width)] +
                                            comS[((i + 1) % curBitmap.Height) * curBitmap.Width + j] +
                                            comS[((i + 1) % curBitmap.Height) * curBitmap.Width + ((j + 1) % curBitmap.Width)] +
                                            comS[((i + 1) % curBitmap.Height) * curBitmap.Width + ((j + 2) % curBitmap.Width)]) / 25);
                        }
                    }
                    break;
                case 7:
                    for (int i = 0; i < curBitmap.Height; i++)
                    {
                        for (int j = 0; j < curBitmap.Width; j++)
                        {
                            comD[i * curBitmap.Width + j] = (byte)((comS[((Math.Abs(i - 2)) % curBitmap.Height) * curBitmap.Width + (Math.Abs(j - 2)) % curBitmap.Width] +
                                            comS[((Math.Abs(i - 2)) % curBitmap.Height) * curBitmap.Width + (Math.Abs(j - 1)) % curBitmap.Width] +
                                            comS[((Math.Abs(i - 2)) % curBitmap.Height) * curBitmap.Width + j] +
                                            comS[((Math.Abs(i - 2)) % curBitmap.Height) * curBitmap.Width + ((j + 1) % curBitmap.Width)] +
                                            comS[((Math.Abs(i - 2)) % curBitmap.Height) * curBitmap.Width + ((j + 2) % curBitmap.Width)] +
                                            comS[((Math.Abs(i - 1)) % curBitmap.Height) * curBitmap.Width + (Math.Abs(j - 2) % curBitmap.Width)] +
                                            comS[((Math.Abs(i - 1)) % curBitmap.Height) * curBitmap.Width + (Math.Abs(j - 1) % curBitmap.Width)] +
                                            comS[((Math.Abs(i - 1)) % curBitmap.Height) * curBitmap.Width + j] +
                                            comS[((Math.Abs(i - 1)) % curBitmap.Height) * curBitmap.Width + ((j + 1) % curBitmap.Width)] +
                                            comS[((Math.Abs(i - 1)) % curBitmap.Height) * curBitmap.Width + ((j + 2) % curBitmap.Width)] +
                                            comS[i * curBitmap.Width + (Math.Abs(j - 2) % curBitmap.Width)] +
                                            comS[i * curBitmap.Width + (Math.Abs(j - 1) % curBitmap.Width)] +
                                            comS[i * curBitmap.Width + j] +
                                            comS[i * curBitmap.Width + ((j + 1) % curBitmap.Width)] +
                                            comS[i * curBitmap.Width + ((j + 2) % curBitmap.Width)] +
                                            comS[((i + 2) % curBitmap.Height) * curBitmap.Width + (Math.Abs(j - 2) % curBitmap.Width)] +
                                            comS[((i + 2) % curBitmap.Height) * curBitmap.Width + (Math.Abs(j - 1) % curBitmap.Width)] +
                                            comS[((i + 2) % curBitmap.Height) * curBitmap.Width + j] +
                                            comS[((i + 2) % curBitmap.Height) * curBitmap.Width + ((j + 1) % curBitmap.Width)] +
                                            comS[((i + 2) % curBitmap.Height) * curBitmap.Width + ((j + 2) % curBitmap.Width)] +
                                            comS[((i + 1) % curBitmap.Height) * curBitmap.Width + (Math.Abs(j - 2) % curBitmap.Width)] +
                                            comS[((i + 1) % curBitmap.Height) * curBitmap.Width + (Math.Abs(j - 1) % curBitmap.Width)] +
                                            comS[((i + 1) % curBitmap.Height) * curBitmap.Width + j] +
                                            comS[((i + 1) % curBitmap.Height) * curBitmap.Width + ((j + 1) % curBitmap.Width)] +
                                            comS[((i + 1) % curBitmap.Height) * curBitmap.Width + ((j + 2) % curBitmap.Width)] +
                                            comS[((Math.Abs(i - 3)) % curBitmap.Height) * curBitmap.Width + (Math.Abs(j - 2) % curBitmap.Width)] +
                                            comS[((Math.Abs(i - 3)) % curBitmap.Height) * curBitmap.Width + (Math.Abs(j - 1) % curBitmap.Width)] +
                                            comS[((Math.Abs(i - 3)) % curBitmap.Height) * curBitmap.Width + j] +
                                            comS[((Math.Abs(i - 3)) % curBitmap.Height) * curBitmap.Width + ((j + 1) % curBitmap.Width)] +
                                            comS[((Math.Abs(i - 3)) % curBitmap.Height) * curBitmap.Width + ((j + 2) % curBitmap.Width)] +
                                            comS[((Math.Abs(i - 3)) % curBitmap.Height) * curBitmap.Width + (Math.Abs(j - 3) % curBitmap.Width)] +
                                            comS[((Math.Abs(i - 3)) % curBitmap.Height) * curBitmap.Width + ((j + 3) % curBitmap.Width)] +
                                            comS[((Math.Abs(i - 2)) % curBitmap.Height) * curBitmap.Width + ((j + 3) % curBitmap.Width)] +
                                            comS[((i + 2) % curBitmap.Height) * curBitmap.Width + ((j + 3) % curBitmap.Width)] +
                                            comS[((Math.Abs(i - 1)) % curBitmap.Height) * curBitmap.Width + ((j + 3) % curBitmap.Width)] +
                                            comS[((i + 1) % curBitmap.Height) * curBitmap.Width + ((j + 3) % curBitmap.Width)] +
                                            comS[i * curBitmap.Width + ((j + 3) % curBitmap.Width)] +
                                            comS[i * curBitmap.Width + (Math.Abs(j - 3) % curBitmap.Width)] +
                                            comS[((i + 1) % curBitmap.Height) * curBitmap.Width + (Math.Abs(j - 3) % curBitmap.Width)] +
                                            comS[((Math.Abs(i - 1)) % curBitmap.Height) * curBitmap.Width + (Math.Abs(j - 3) % curBitmap.Width)] +
                                            comS[((i + 2) % curBitmap.Height) * curBitmap.Width + (Math.Abs(j - 3) % curBitmap.Width)] +
                                            comS[((Math.Abs(i - 2)) % curBitmap.Height) * curBitmap.Width + (Math.Abs(j - 3) % curBitmap.Width)] +
                                            comS[((i + 3) % curBitmap.Height) * curBitmap.Width + ((j + 1) % curBitmap.Width)] +
                                            comS[((i + 3) % curBitmap.Height) * curBitmap.Width + ((j + 2) % curBitmap.Width)] +
                                            comS[((i + 3) % curBitmap.Height) * curBitmap.Width + (Math.Abs(j - 2) % curBitmap.Width)] +
                                            comS[((i + 3) % curBitmap.Height) * curBitmap.Width + (Math.Abs(j - 1) % curBitmap.Width)] +
                                            comS[((i + 3) % curBitmap.Height) * curBitmap.Width + j] +
                                            comS[((i + 3) % curBitmap.Height) * curBitmap.Width + ((j + 3) % curBitmap.Width)] +
                                            comS[((i + 3) % curBitmap.Height) * curBitmap.Width + (Math.Abs(j - 3) % curBitmap.Width)]) / 49);
                        }
                    }
                    break;
                default:
                    break;
            }
            return comD;
        }

        private void shaC_Click(object sender, EventArgs e)
        {
            if (curBitmap != null)
            {
                sharpColor sharpC = new sharpColor();
                if (sharpC.ShowDialog() == DialogResult.OK)
                {
                    Rectangle rect = new Rectangle(0, 0, curBitmap.Width, curBitmap.Height);
                    System.Drawing.Imaging.BitmapData bmpData = curBitmap.LockBits(rect, System.Drawing.Imaging.ImageLockMode.ReadWrite, curBitmap.PixelFormat);
                    IntPtr ptr = bmpData.Scan0;
                    int bytes = curBitmap.Width * curBitmap.Height;
                    byte[] rgbValues = new byte[bytes * 3];
                    System.Runtime.InteropServices.Marshal.Copy(ptr, rgbValues, 0, bytes * 3);

                    bool method = sharpC.GetMethod;

                    if (method == false)
                    {
                        byte[] rValues = new byte[bytes];
                        byte[] gValues = new byte[bytes];
                        byte[] bValues = new byte[bytes];

                        for (int i = 0; i < bytes; i++)
                        {
                            rValues[i] = rgbValues[i * 3 + 2];
                            gValues[i] = rgbValues[i * 3 + 1];
                            bValues[i] = rgbValues[i * 3];
                        }

                        rValues = sharp(rValues);
                        gValues = sharp(gValues);
                        bValues = sharp(bValues);

                        for (int i = 0; i < bytes; i++)
                        {
                            rgbValues[i * 3 + 2] = rValues[i];
                            rgbValues[i * 3 + 1] = gValues[i];
                            rgbValues[i * 3] = bValues[i];
                        }
                    }
                    else
                    {
                        double[] hue = new double[bytes];
                        double[] sat = new double[bytes];
                        byte[] inten = new byte[bytes];
                        double r, g, b;

                        for (int i = 0; i < bytes; i++)
                        {
                            r = rgbValues[i * 3 + 2];
                            g = rgbValues[i * 3 + 1];
                            b = rgbValues[i * 3];

                            double theta = Math.Acos(0.5 * ((r - g) + (r - b)) / Math.Sqrt((r - g) * (r - g) + (r - b) * (g - b) + 1)) / (2 * Math.PI);

                            hue[i] = ((b <= g) ? theta : (1 - theta));

                            sat[i] = 1.0 - 3.0 * Math.Min(Math.Min(r, g), b) / (r + g + b + 1);

                            inten[i] = (byte)((r + g + b) / 3);
                        }

                        inten = sharp(inten);

                        for (int i = 0; i < bytes; i++)
                        {
                            r = rgbValues[i * 3 + 2];
                            g = rgbValues[i * 3 + 1];
                            b = rgbValues[i * 3];

                            hue[i] = hue[i] * 2 * Math.PI;
                            if (hue[i] >= 0 && hue[i] < 2 * Math.PI / 3)
                            {
                                b = inten[i] * (1 - sat[i]);
                                r = inten[i] * (1 + sat[i] * Math.Cos(hue[i]) / Math.Cos(Math.PI / 3 - hue[i]));
                                g = 3 * inten[i] - (r + b);
                            }
                            else if (hue[i] >= 2 * Math.PI / 3 && hue[i] < 4 * Math.PI / 3)
                            {
                                r = inten[i] * (1 - sat[i]);
                                g = inten[i] * (1 + sat[i] * Math.Cos(hue[i] - 2 * Math.PI / 3) / Math.Cos(Math.PI - hue[i]));
                                b = 3 * inten[i] - (r + g);
                            }
                            else //if (h >= 4 * Math.PI / 3 && h <= 2 * Math.PI)
                            {
                                g = inten[i] * (1 - sat[i]);
                                b = inten[i] * (1 + sat[i] * Math.Cos(hue[i] - 4 * Math.PI / 3) / Math.Cos(5 * Math.PI / 3 - hue[i]));
                                r = 3 * inten[i] - (g + b);
                            }
                            if (r > 255)
                                r = 255;
                            if (g > 255)
                                g = 255;
                            if (b > 255)
                                b = 255;

                            rgbValues[i * 3 + 2] = (byte)r;
                            rgbValues[i * 3 + 1] = (byte)g;
                            rgbValues[i * 3] = (byte)b;
                        }
                    }

                    System.Runtime.InteropServices.Marshal.Copy(rgbValues, 0, ptr, bytes * 3);
                    curBitmap.UnlockBits(bmpData);
                    Invalidate();
                }
            }
        }

        private byte[] sharp(byte[] comArray)
        {
            byte[] comValues = new byte[comArray.Length];

            for (int i = 0; i < curBitmap.Height; i++)
            {
                for (int j = 0; j < curBitmap.Width; j++)
                {
                    comValues[i * curBitmap.Width + j] = (byte)(5 * comArray[i * curBitmap.Width + j] - 
                        (comArray[((Math.Abs(i - 1)) % curBitmap.Height) * curBitmap.Width + j] + 
                        comArray[((i + 1) % curBitmap.Height) * curBitmap.Width + j] + 
                        comArray[i * curBitmap.Width + ((j + 1) % curBitmap.Width)] + 
                        comArray[i * curBitmap.Width + ((Math.Abs(j - 1)) % curBitmap.Width)]));
                }
            }            
            return comValues;
        }

        private void edgeC_Click(object sender, EventArgs e)
        {
            if (curBitmap != null)
            {
                edgeColor edgedetC = new edgeColor();
                if (edgedetC.ShowDialog() == DialogResult.OK)
                {
                    Rectangle rect = new Rectangle(0, 0, curBitmap.Width, curBitmap.Height);
                    System.Drawing.Imaging.BitmapData bmpData = curBitmap.LockBits(rect, System.Drawing.Imaging.ImageLockMode.ReadWrite, curBitmap.PixelFormat);
                    IntPtr ptr = bmpData.Scan0;
                    int bytes = curBitmap.Width * curBitmap.Height;
                    byte[] rgbValues = new byte[bytes * 3];
                    System.Runtime.InteropServices.Marshal.Copy(ptr, rgbValues, 0, bytes * 3);

                    byte method = edgedetC.GetMethod;
                    int thresh = edgedetC.GetThresholding;

                    byte[] grayValues = new byte[bytes];
                    double[] tempArray = new double[bytes];
                    byte[] rValues = new byte[bytes];
                    byte[] gValues = new byte[bytes];
                    byte[] bValues = new byte[bytes];
                    for (int i = 0; i < bytes; i++)
                    {
                        rValues[i] = rgbValues[i * 3 + 2];
                        gValues[i] = rgbValues[i * 3 + 1];
                        bValues[i] = rgbValues[i * 3];
                    }
                    double maxV = 0.0;
                    double minV = 1000.0;

                    switch(method)
                    {
                        case 0:
                            double grh, ggh, gbh;
                            double grv, ggv, gbv;
                            double gxx, gyy, gxy;
                            double ge1, ge2;
                            double theta;

                            for (int i = 0; i < curBitmap.Height; i++)
                            {
                                for (int j = 0; j < curBitmap.Width; j++)
                                {
                                    grh = rValues[((Math.Abs(i - 1)) % curBitmap.Height) * curBitmap.Width + ((j + 1) % curBitmap.Width)] + 2 * rValues[i * curBitmap.Width + ((j + 1) % curBitmap.Width)] + rValues[((i + 1) % curBitmap.Height) * curBitmap.Width + ((j + 1) % curBitmap.Width)] -
                                        rValues[((Math.Abs(i - 1)) % curBitmap.Height) * curBitmap.Width + ((Math.Abs(j - 1)) % curBitmap.Width)] - 2 * rValues[i * curBitmap.Width + ((Math.Abs(j - 1)) % curBitmap.Width)] - rValues[((i + 1) % curBitmap.Height) * curBitmap.Width + ((Math.Abs(j - 1)) % curBitmap.Width)];
                                    ggh = gValues[((Math.Abs(i - 1)) % curBitmap.Height) * curBitmap.Width + ((j + 1) % curBitmap.Width)] + 2 * gValues[i * curBitmap.Width + ((j + 1) % curBitmap.Width)] + gValues[((i + 1) % curBitmap.Height) * curBitmap.Width + ((j + 1) % curBitmap.Width)] -
                                        gValues[((Math.Abs(i - 1)) % curBitmap.Height) * curBitmap.Width + ((Math.Abs(j - 1)) % curBitmap.Width)] - 2 * gValues[i * curBitmap.Width + ((Math.Abs(j - 1)) % curBitmap.Width)] - gValues[((i + 1) % curBitmap.Height) * curBitmap.Width + ((Math.Abs(j - 1)) % curBitmap.Width)];
                                    gbh = bValues[((Math.Abs(i - 1)) % curBitmap.Height) * curBitmap.Width + ((j + 1) % curBitmap.Width)] + 2 * bValues[i * curBitmap.Width + ((j + 1) % curBitmap.Width)] + bValues[((i + 1) % curBitmap.Height) * curBitmap.Width + ((j + 1) % curBitmap.Width)] -
                                        bValues[((Math.Abs(i - 1)) % curBitmap.Height) * curBitmap.Width + ((Math.Abs(j - 1)) % curBitmap.Width)] - 2 * bValues[i * curBitmap.Width + ((Math.Abs(j - 1)) % curBitmap.Width)] - bValues[((i + 1) % curBitmap.Height) * curBitmap.Width + ((Math.Abs(j - 1)) % curBitmap.Width)];

                                    grv = rValues[((Math.Abs(i - 1)) % curBitmap.Height) * curBitmap.Width + ((Math.Abs(j - 1)) % curBitmap.Width)] + 2 * rValues[((Math.Abs(i - 1)) % curBitmap.Height) * curBitmap.Width + j] + rValues[((Math.Abs(i - 1)) % curBitmap.Height) * curBitmap.Width + ((j + 1) % curBitmap.Width)] -
                                        rValues[((i + 1) % curBitmap.Height) * curBitmap.Width + ((Math.Abs(j - 1)) % curBitmap.Width)] - 2 * rValues[((i + 1) % curBitmap.Height) * curBitmap.Width + j] - rValues[((i + 1) % curBitmap.Height) * curBitmap.Width + ((j + 1) % curBitmap.Width)];
                                    ggv = gValues[((Math.Abs(i - 1)) % curBitmap.Height) * curBitmap.Width + ((Math.Abs(j - 1)) % curBitmap.Width)] + 2 * gValues[((Math.Abs(i - 1)) % curBitmap.Height) * curBitmap.Width + j] + gValues[((Math.Abs(i - 1)) % curBitmap.Height) * curBitmap.Width + ((j + 1) % curBitmap.Width)] -
                                        gValues[((i + 1) % curBitmap.Height) * curBitmap.Width + ((Math.Abs(j - 1)) % curBitmap.Width)] - 2 * gValues[((i + 1) % curBitmap.Height) * curBitmap.Width + j] - gValues[((i + 1) % curBitmap.Height) * curBitmap.Width + ((j + 1) % curBitmap.Width)];
                                    gbv = bValues[((Math.Abs(i - 1)) % curBitmap.Height) * curBitmap.Width + ((Math.Abs(j - 1)) % curBitmap.Width)] + 2 * bValues[((Math.Abs(i - 1)) % curBitmap.Height) * curBitmap.Width + j] + bValues[((Math.Abs(i - 1)) % curBitmap.Height) * curBitmap.Width + ((j + 1) % curBitmap.Width)] -
                                        bValues[((i + 1) % curBitmap.Height) * curBitmap.Width + ((Math.Abs(j - 1)) % curBitmap.Width)] - 2 * bValues[((i + 1) % curBitmap.Height) * curBitmap.Width + j] - bValues[((i + 1) % curBitmap.Height) * curBitmap.Width + ((j + 1) % curBitmap.Width)];
                                    
                                    gxx = grh * grh + ggh * ggh + gbh * gbh;
                                    gyy = grv * grv + ggv * ggv + gbv * gbv;
                                    gxy = grh * grv + ggh * ggv + gbh * gbv;

                                    theta = Math.Atan(2 * gxy / (gxx - gyy + 0.000000001)) / 2;
                                    ge1 = ((gxx + gyy) + (gxx - gyy) * Math.Cos(2 * theta) + 2 * gxy * Math.Sin(2 * theta)) / 2;
                                    theta = theta + Math.PI / 2;
                                    ge2 = ((gxx + gyy) + (gxx - gyy) * Math.Cos(2 * theta) + 2 * gxy * Math.Sin(2 * theta)) / 2;
                                    tempArray[i * curBitmap.Width + j] = Math.Max(Math.Sqrt(ge1), Math.Sqrt(ge2));

                                    if (tempArray[i * curBitmap.Width + j] > maxV)
                                        maxV = tempArray[i * curBitmap.Width + j];
                                    else if (tempArray[i * curBitmap.Width + j] < minV)
                                        minV = tempArray[i * curBitmap.Width + j];

                                }
                            }

                            for (int i = 0; i < bytes; i++)
                            {
                                grayValues[i] = (byte)((tempArray[i] - minV) * 255 / (maxV - minV));
                            }
                            break;
                        case 1:
                            double gr, gg, gb;
                            double de1, de2;

                            for (int i = 0; i < curBitmap.Height; i++)
                            {
                                for (int j = 0; j < curBitmap.Width; j++)
                                {                                  
                                    gr = rValues[((Math.Abs(i - 1)) % curBitmap.Height) * curBitmap.Width + ((j + 1) % curBitmap.Width)] + 2 * rValues[i * curBitmap.Width + ((j + 1) % curBitmap.Width)] + rValues[((i + 1) % curBitmap.Height) * curBitmap.Width + ((j + 1) % curBitmap.Width)] -
                                        rValues[((Math.Abs(i - 1)) % curBitmap.Height) * curBitmap.Width + ((Math.Abs(j - 1)) % curBitmap.Width)] - 2 * rValues[i * curBitmap.Width + ((Math.Abs(j - 1)) % curBitmap.Width)] - rValues[((i + 1) % curBitmap.Height) * curBitmap.Width + ((Math.Abs(j - 1)) % curBitmap.Width)];
                                    gg = gValues[((Math.Abs(i - 1)) % curBitmap.Height) * curBitmap.Width + ((j + 1) % curBitmap.Width)] + 2 * gValues[i * curBitmap.Width + ((j + 1) % curBitmap.Width)] + gValues[((i + 1) % curBitmap.Height) * curBitmap.Width + ((j + 1) % curBitmap.Width)] -
                                        gValues[((Math.Abs(i - 1)) % curBitmap.Height) * curBitmap.Width + ((Math.Abs(j - 1)) % curBitmap.Width)] - 2 * gValues[i * curBitmap.Width + ((Math.Abs(j - 1)) % curBitmap.Width)] - gValues[((i + 1) % curBitmap.Height) * curBitmap.Width + ((Math.Abs(j - 1)) % curBitmap.Width)];
                                    gb = bValues[((Math.Abs(i - 1)) % curBitmap.Height) * curBitmap.Width + ((j + 1) % curBitmap.Width)] + 2 * bValues[i * curBitmap.Width + ((j + 1) % curBitmap.Width)] + bValues[((i + 1) % curBitmap.Height) * curBitmap.Width + ((j + 1) % curBitmap.Width)] -
                                        bValues[((Math.Abs(i - 1)) % curBitmap.Height) * curBitmap.Width + ((Math.Abs(j - 1)) % curBitmap.Width)] - 2 * bValues[i * curBitmap.Width + ((Math.Abs(j - 1)) % curBitmap.Width)] - bValues[((i + 1) % curBitmap.Height) * curBitmap.Width + ((Math.Abs(j - 1)) % curBitmap.Width)];

                                    de1 = Math.Sqrt(gr * gr + gg * gg + gb * gb);

                                    gr = rValues[((Math.Abs(i - 1)) % curBitmap.Height) * curBitmap.Width + ((Math.Abs(j - 1)) % curBitmap.Width)] + 2 * rValues[((Math.Abs(i - 1)) % curBitmap.Height) * curBitmap.Width + j] + rValues[((Math.Abs(i - 1)) % curBitmap.Height) * curBitmap.Width + ((j + 1) % curBitmap.Width)] -
                                        rValues[((i + 1) % curBitmap.Height) * curBitmap.Width + ((Math.Abs(j - 1)) % curBitmap.Width)] - 2 * rValues[((i + 1) % curBitmap.Height) * curBitmap.Width + j] - rValues[((i + 1) % curBitmap.Height) * curBitmap.Width + ((j + 1) % curBitmap.Width)];
                                    gg = gValues[((Math.Abs(i - 1)) % curBitmap.Height) * curBitmap.Width + ((Math.Abs(j - 1)) % curBitmap.Width)] + 2 * gValues[((Math.Abs(i - 1)) % curBitmap.Height) * curBitmap.Width + j] + gValues[((Math.Abs(i - 1)) % curBitmap.Height) * curBitmap.Width + ((j + 1) % curBitmap.Width)] -
                                        gValues[((i + 1) % curBitmap.Height) * curBitmap.Width + ((Math.Abs(j - 1)) % curBitmap.Width)] - 2 * gValues[((i + 1) % curBitmap.Height) * curBitmap.Width + j] - gValues[((i + 1) % curBitmap.Height) * curBitmap.Width + ((j + 1) % curBitmap.Width)];
                                    gb = bValues[((Math.Abs(i - 1)) % curBitmap.Height) * curBitmap.Width + ((Math.Abs(j - 1)) % curBitmap.Width)] + 2 * bValues[((Math.Abs(i - 1)) % curBitmap.Height) * curBitmap.Width + j] + bValues[((Math.Abs(i - 1)) % curBitmap.Height) * curBitmap.Width + ((j + 1) % curBitmap.Width)] -
                                        bValues[((i + 1) % curBitmap.Height) * curBitmap.Width + ((Math.Abs(j - 1)) % curBitmap.Width)] - 2 * bValues[((i + 1) % curBitmap.Height) * curBitmap.Width + j] - bValues[((i + 1) % curBitmap.Height) * curBitmap.Width + ((j + 1) % curBitmap.Width)];
                                    
                                    de2 = Math.Sqrt(gr * gr + gg * gg + gb * gb);
                                    tempArray[i * curBitmap.Width + j] = Math.Max(de1, de2);

                                    if (tempArray[i * curBitmap.Width + j] > maxV)
                                        maxV = tempArray[i * curBitmap.Width + j];
                                    else if (tempArray[i * curBitmap.Width + j] < minV)
                                        minV = tempArray[i * curBitmap.Width + j];
                                }
                            }

                            for (int i = 0; i < bytes; i++)
                            {
                                grayValues[i] = (byte)((tempArray[i] - minV) * 255 / (maxV - minV));
                            }
                            break;
                        case 2:
                            double[] rvg = new double[bytes];
                            double[] gvg = new double[bytes];
                            double[] bvg = new double[bytes];
                            double gh, gv;
                            double[] maxValue = new double[3] { 0.0, 0.0, 0.0 };
                            double[] minValue = new double[3] { 1000.0, 1000.0, 1000.0 };

                            for (int i = 0; i < curBitmap.Height; i++)
                            {
                                for (int j = 0; j < curBitmap.Width; j++)
                                {
                                    gh = rValues[((Math.Abs(i - 1)) % curBitmap.Height) * curBitmap.Width + ((j + 1) % curBitmap.Width)] + 2 * rValues[i * curBitmap.Width + ((j + 1) % curBitmap.Width)] + rValues[((i + 1) % curBitmap.Height) * curBitmap.Width + ((j + 1) % curBitmap.Width)] -
                                        rValues[((Math.Abs(i - 1)) % curBitmap.Height) * curBitmap.Width + ((Math.Abs(j - 1)) % curBitmap.Width)] - 2 * rValues[i * curBitmap.Width + ((Math.Abs(j - 1)) % curBitmap.Width)] - rValues[((i + 1) % curBitmap.Height) * curBitmap.Width + ((Math.Abs(j - 1)) % curBitmap.Width)];
                                    gv = rValues[((Math.Abs(i - 1)) % curBitmap.Height) * curBitmap.Width + ((Math.Abs(j - 1)) % curBitmap.Width)] + 2 * rValues[((Math.Abs(i - 1)) % curBitmap.Height) * curBitmap.Width + j] + rValues[((Math.Abs(i - 1)) % curBitmap.Height) * curBitmap.Width + ((j + 1) % curBitmap.Width)] -
                                        rValues[((i + 1) % curBitmap.Height) * curBitmap.Width + ((Math.Abs(j - 1)) % curBitmap.Width)] - 2 * rValues[((i + 1) % curBitmap.Height) * curBitmap.Width + j] - rValues[((i + 1) % curBitmap.Height) * curBitmap.Width + ((j + 1) % curBitmap.Width)];                                    
                                    rvg[i * curBitmap.Width + j] = Math.Sqrt(gh * gh + gv * gv);
                                    if (rvg[i * curBitmap.Width + j] > maxValue[0])
                                        maxValue[0] = rvg[i * curBitmap.Width + j];
                                    else if (rvg[i * curBitmap.Width + j] < minValue[0])
                                        minValue[0] = rvg[i * curBitmap.Width + j];

                                    gh = gValues[((Math.Abs(i - 1)) % curBitmap.Height) * curBitmap.Width + ((j + 1) % curBitmap.Width)] + 2 * gValues[i * curBitmap.Width + ((j + 1) % curBitmap.Width)] + gValues[((i + 1) % curBitmap.Height) * curBitmap.Width + ((j + 1) % curBitmap.Width)] -
                                        gValues[((Math.Abs(i - 1)) % curBitmap.Height) * curBitmap.Width + ((Math.Abs(j - 1)) % curBitmap.Width)] - 2 * gValues[i * curBitmap.Width + ((Math.Abs(j - 1)) % curBitmap.Width)] - gValues[((i + 1) % curBitmap.Height) * curBitmap.Width + ((Math.Abs(j - 1)) % curBitmap.Width)];
                                    gv = gValues[((Math.Abs(i - 1)) % curBitmap.Height) * curBitmap.Width + ((Math.Abs(j - 1)) % curBitmap.Width)] + 2 * gValues[((Math.Abs(i - 1)) % curBitmap.Height) * curBitmap.Width + j] + gValues[((Math.Abs(i - 1)) % curBitmap.Height) * curBitmap.Width + ((j + 1) % curBitmap.Width)] -
                                        gValues[((i + 1) % curBitmap.Height) * curBitmap.Width + ((Math.Abs(j - 1)) % curBitmap.Width)] - 2 * gValues[((i + 1) % curBitmap.Height) * curBitmap.Width + j] - gValues[((i + 1) % curBitmap.Height) * curBitmap.Width + ((j + 1) % curBitmap.Width)];
                                    gvg[i * curBitmap.Width + j] = Math.Sqrt(gh * gh + gv * gv);
                                    if (gvg[i * curBitmap.Width + j] > maxValue[1])
                                        maxValue[1] = gvg[i * curBitmap.Width + j];
                                    else if (gvg[i * curBitmap.Width + j] < minValue[1])
                                        minValue[1] = gvg[i * curBitmap.Width + j];

                                    gh = bValues[((Math.Abs(i - 1)) % curBitmap.Height) * curBitmap.Width + ((j + 1) % curBitmap.Width)] + 2 * bValues[i * curBitmap.Width + ((j + 1) % curBitmap.Width)] + bValues[((i + 1) % curBitmap.Height) * curBitmap.Width + ((j + 1) % curBitmap.Width)] -
                                        bValues[((Math.Abs(i - 1)) % curBitmap.Height) * curBitmap.Width + ((Math.Abs(j - 1)) % curBitmap.Width)] - 2 * bValues[i * curBitmap.Width + ((Math.Abs(j - 1)) % curBitmap.Width)] - bValues[((i + 1) % curBitmap.Height) * curBitmap.Width + ((Math.Abs(j - 1)) % curBitmap.Width)];
                                    gv = bValues[((Math.Abs(i - 1)) % curBitmap.Height) * curBitmap.Width + ((Math.Abs(j - 1)) % curBitmap.Width)] + 2 * bValues[((Math.Abs(i - 1)) % curBitmap.Height) * curBitmap.Width + j] + bValues[((Math.Abs(i - 1)) % curBitmap.Height) * curBitmap.Width + ((j + 1) % curBitmap.Width)] -
                                        bValues[((i + 1) % curBitmap.Height) * curBitmap.Width + ((Math.Abs(j - 1)) % curBitmap.Width)] - 2 * bValues[((i + 1) % curBitmap.Height) * curBitmap.Width + j] - bValues[((i + 1) % curBitmap.Height) * curBitmap.Width + ((j + 1) % curBitmap.Width)];                                    
                                    bvg[i * curBitmap.Width + j] = Math.Sqrt(gh * gh + gv * gv);
                                    if (bvg[i * curBitmap.Width + j] > maxValue[2])
                                        maxValue[2] = bvg[i * curBitmap.Width + j];
                                    else if (bvg[i * curBitmap.Width + j] < minValue[2])
                                        minValue[2] = rvg[i * curBitmap.Width + j];
                                }
                            }

                            for (int i = 0; i < bytes; i++)
                            {
                                rgbValues[i * 3 + 2] = (byte)((rvg[i] - minValue[0]) * 255 / (maxValue[0] - minValue[0]));
                                rgbValues[i * 3 + 1] = (byte)((gvg[i] - minValue[1]) * 255 / (maxValue[1] - minValue[1]));
                                rgbValues[i * 3] = (byte)((bvg[i] - minValue[2]) * 255 / (maxValue[2] - minValue[2]));
                                gh = Math.Max((rvg[i] - minValue[0]) * 255 / (maxValue[0] - minValue[0]), (gvg[i] - minValue[1]) * 255 / (maxValue[1] - minValue[1]));
                                gv = Math.Max(gh, (bvg[i] - minValue[2]) * 255 / (maxValue[2] - minValue[2]));
                                grayValues[i] = (byte)(gv);
                            }
                            break;
                        default:
                            break;
                    }
                    if (thresh != 0)
                    {
                        for (int i = 0; i < bytes; i++)
                        {
                            if (grayValues[i] > thresh)
                                grayValues[i] = 255;
                            else
                                grayValues[i] = 0;
                        }
                    }
                    
                    curBitmap = new Bitmap(curBitmap.Width, curBitmap.Height, System.Drawing.Imaging.PixelFormat.Format8bppIndexed);
                    System.Drawing.Imaging.ColorPalette cp = curBitmap.Palette;
                    for (int i = 0; i < 256; i++)
                    {
                        cp.Entries[i] = Color.FromArgb(i, i, i);
                    }
                    curBitmap.Palette = cp;
                    bmpData = curBitmap.LockBits(rect, System.Drawing.Imaging.ImageLockMode.ReadWrite, curBitmap.PixelFormat);
                    ptr = bmpData.Scan0;
                    System.Runtime.InteropServices.Marshal.Copy(grayValues, 0, ptr, bytes);
                    curBitmap.UnlockBits(bmpData);
                    Invalidate();
                }
            }
        }

        private void segC_Click(object sender, EventArgs e)
        {
            if (curBitmap != null)
            {
                segColor segmentationC = new segColor();
                if (segmentationC.ShowDialog() == DialogResult.OK)
                {
                    Rectangle rect = new Rectangle(0, 0, curBitmap.Width, curBitmap.Height);
                    System.Drawing.Imaging.BitmapData bmpData = curBitmap.LockBits(rect, System.Drawing.Imaging.ImageLockMode.ReadWrite, curBitmap.PixelFormat);
                    IntPtr ptr = bmpData.Scan0;
                    int bytes = curBitmap.Width * curBitmap.Height;
                    byte[] rgbValues = new byte[bytes * 3];
                    System.Runtime.InteropServices.Marshal.Copy(ptr, rgbValues, 0, bytes * 3);

                    byte numbers = segmentationC.GetNum;

                    int[] kNum = new int[numbers];
                    int[] kAver = new int[numbers * 3];
                    int[] kOldAver = new int[numbers * 3];
                    int[] kSum = new int[numbers * 3];
                    double[] kTemp = new double[numbers];
                    byte[] segmentMap = new byte[bytes * 3];
                    //初始化聚类均值
                    for (int i = 0; i < numbers; i++)
                    {

                        kAver[i * 3 + 2] = kOldAver[i * 3 + 2] = Convert.ToInt16(i * 255 / (numbers - 1));
                        kAver[i * 3 + 1] = kOldAver[i * 3 + 1] = Convert.ToInt16(i * 255 / (numbers - 1));
                        kAver[i * 3] = kOldAver[i * 3] = Convert.ToInt16(i * 255 / (numbers - 1));
                    }
                    int count = 0;
                    while (true)
                    {
                        int order = 0;
                        for (int i = 0; i < numbers; i++)
                        {
                            kNum[i] = 0;
                            kSum[i * 3 + 2] = kSum[i * 3 + 1] = kSum[i * 3] = 0;
                            kAver[i * 3 + 2] = kOldAver[i * 3 + 2];
                            kAver[i * 3 + 1] = kOldAver[i * 3 + 1];
                            kAver[i * 3] = kOldAver[i * 3];
                        }
                        //归属聚类
                        for (int i = 0; i < bytes; i++)
                        {
                            for (int j = 0; j < numbers; j++)
                            {
                                kTemp[j] = Math.Pow(rgbValues[i * 3 + 2] - kAver[j * 3 + 2], 2) + Math.Pow(rgbValues[i * 3 + 1] - kAver[j * 3 + 1], 2) + Math.Pow(rgbValues[i * 3] - kAver[j * 3], 2);
                            }
                            double temp = 100000;

                            for (int j = 0; j < numbers; j++)
                            {
                                if (kTemp[j] < temp)
                                {
                                    temp = kTemp[j];
                                    order = j;
                                }
                            }
                            kNum[order]++;
                            kSum[order * 3 + 2] += rgbValues[i * 3 + 2];
                            kSum[order * 3 + 1] += rgbValues[i * 3 + 1];
                            kSum[order * 3] += rgbValues[i * 3];
                            segmentMap[i] = Convert.ToByte(order);
                        }
                        for (int i = 0; i < numbers; i++)
                        {
                            if (kNum[i] != 0)
                            {
                                kOldAver[i * 3 + 2] = Convert.ToInt16(kSum[i * 3 + 2] / kNum[i]);
                                kOldAver[i * 3 + 1] = Convert.ToInt16(kSum[i * 3 + 1] / kNum[i]);
                                kOldAver[i * 3] = Convert.ToInt16(kSum[i * 3] / kNum[i]);
                            }
                        }

                        int kkk = 0;
                        count++;
                        for (int i = 0; i < numbers; i++)
                        {
                            if (kAver[i * 3 + 2] == kOldAver[i * 3 + 2] && kAver[i * 3 + 1] == kOldAver[i * 3 + 1] && kAver[i * 3] == kOldAver[i * 3])
                                kkk++;
                        }
                        if (kkk == numbers || count == 100)
                            break;
                    }

                    for (int i = 0; i < bytes; i++)
                    {
                        for (int j = 0; j < numbers; j++)
                        {
                            if (segmentMap[i] == j)
                            {
                                rgbValues[i * 3 + 2] = Convert.ToByte(kAver[j * 3 + 2]);
                                rgbValues[i * 3 + 1] = Convert.ToByte(kAver[j * 3 + 1]);
                                rgbValues[i * 3] = Convert.ToByte(kAver[j * 3]);
                            }
                        }
                    }

                    System.Runtime.InteropServices.Marshal.Copy(rgbValues, 0, ptr, bytes * 3);
                    curBitmap.UnlockBits(bmpData);
                    Invalidate();
                }
            }
        }
    }
}