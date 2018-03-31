using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace smooth
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

        private void noise_Click(object sender, EventArgs e)
        {
            if (curBitmap != null)
            {
                noiseModel noise = new noiseModel();
                if (noise.ShowDialog() == DialogResult.OK)
                {
                    Rectangle rect = new Rectangle(0, 0, curBitmap.Width, curBitmap.Height);
                    System.Drawing.Imaging.BitmapData bmpData = curBitmap.LockBits(rect, System.Drawing.Imaging.ImageLockMode.ReadWrite, curBitmap.PixelFormat);
                    IntPtr ptr = bmpData.Scan0;
                    int bytes = curBitmap.Width * curBitmap.Height;
                    byte[] grayValues = new byte[bytes];
                    System.Runtime.InteropServices.Marshal.Copy(ptr, grayValues, 0, bytes);
                    double temp = 0;
                    byte flagNoise = noise.GetFlag;
                    double[] paraNoise = new double[2];
                    paraNoise = noise.GetParaN;

                    Random r1, r2;
                    double v1, v2;
                    r1 = new Random(unchecked((int)DateTime.Now.Ticks));
                    r2 = new Random(~unchecked((int)DateTime.Now.Ticks));

                    for (int i = 0; i < bytes; i++)
                    {
                        switch (flagNoise)
                        {
                            case 0:
                                do
                                {
                                    v1 = r1.NextDouble();
                                }
                                while (v1 <= 0.00000000001);
                                v2 = r2.NextDouble();
                                temp = Math.Sqrt(-2 * Math.Log(v1)) * Math.Cos(2 * Math.PI * v2) * paraNoise[1] + paraNoise[0];
                                break;
                            case 1:
                                do
                                {
                                    v1 = r1.NextDouble();
                                }
                                while (v1 >= 0.9999999999);
                                temp = paraNoise[0] + Math.Sqrt(-1 * paraNoise[1] * Math.Log(1 - v1));
                                break;
                            case 2:
                                do
                                {
                                    v1 = r1.NextDouble();
                                }
                                while (v1 >= 0.9999999999);
                                temp = -1 * Math.Log(1 - v1) / paraNoise[0];
                                break;
                            case 3:
                                v1 = r1.NextDouble();
                                if (v1 <= paraNoise[0])
                                    temp = -500;
                                else if (v1 >= (1 - paraNoise[1]))
                                    temp = 500;
                                else
                                    temp = 0;
                                break;
                            default:
                                MessageBox.Show("无效！");
                                break;
                        }
                        temp = temp + grayValues[i];

                        if (temp > 255)
                        {
                            grayValues[i] = 255;
                        }
                        else if (temp < 0)
                        {
                            grayValues[i] = 0;
                        }
                        else
                            grayValues[i] = Convert.ToByte(temp);
                    }
                    System.Runtime.InteropServices.Marshal.Copy(grayValues, 0, ptr, bytes);
                    curBitmap.UnlockBits(bmpData);
                }
                noise.Close();
                Invalidate();
            }
        }

        private void meanMedian_Click(object sender, EventArgs e)
        {
            if (curBitmap != null)
            {
                meanMedian meaAndMed = new meanMedian();
                if (meaAndMed.ShowDialog() == DialogResult.OK)
                {
                    Rectangle rect = new Rectangle(0, 0, curBitmap.Width, curBitmap.Height);
                    System.Drawing.Imaging.BitmapData bmpData = curBitmap.LockBits(rect, System.Drawing.Imaging.ImageLockMode.ReadWrite, curBitmap.PixelFormat);
                    IntPtr ptr = bmpData.Scan0;
                    int bytes = curBitmap.Width * curBitmap.Height;
                    byte[] grayValues = new byte[bytes];
                    System.Runtime.InteropServices.Marshal.Copy(ptr, grayValues, 0, bytes);

                    byte[] tempArray = new byte[bytes];

                    byte sideLength = meaAndMed.GetLength;
                    byte halfLength = (byte)(sideLength / 2);
                    bool flagM = meaAndMed.GetFlag;

                    for (int i = 0; i < curBitmap.Height; i++)
                    {
                        for (int j = 0; j < curBitmap.Width; j++)
                        {
                            switch (sideLength)
                            {
                                case 3:
                                    if (flagM == false)
                                    {
                                        tempArray[i * curBitmap.Width + j] = (byte)((grayValues[i * curBitmap.Width + j] +
                                            grayValues[((Math.Abs(i - 1)) % curBitmap.Height) * curBitmap.Width + j] +
                                            grayValues[((i + 1) % curBitmap.Height) * curBitmap.Width + j] +
                                            grayValues[i * curBitmap.Width + ((j + 1) % curBitmap.Width)] +
                                            grayValues[i * curBitmap.Width + ((Math.Abs(j - 1)) % curBitmap.Width)] +
                                            grayValues[((Math.Abs(i - 1)) % curBitmap.Height) * curBitmap.Width + ((Math.Abs(j - 1)) % curBitmap.Width)] +
                                            grayValues[((i + 1) % curBitmap.Height) * curBitmap.Width + ((Math.Abs(j - 1)) % curBitmap.Width)] +
                                            grayValues[((Math.Abs(i - 1)) % curBitmap.Height) * curBitmap.Width + ((j + 1) % curBitmap.Width)] +
                                            grayValues[((i + 1) % curBitmap.Height) * curBitmap.Width + ((j + 1) % curBitmap.Width)]) / 9);

                                    }
                                    else
                                    {
                                        byte[] sortArray = new byte[] {grayValues[i * curBitmap.Width + j],
                                            grayValues[((Math.Abs(i - 1)) % curBitmap.Height) * curBitmap.Width + j],
                                            grayValues[((i + 1) % curBitmap.Height) * curBitmap.Width + j],
                                            grayValues[i * curBitmap.Width + ((j + 1) % curBitmap.Width)],
                                            grayValues[i * curBitmap.Width + ((Math.Abs(j - 1)) % curBitmap.Width)],
                                            grayValues[((Math.Abs(i - 1)) % curBitmap.Height) * curBitmap.Width + ((Math.Abs(j - 1)) % curBitmap.Width)],
                                            grayValues[((i + 1) % curBitmap.Height) * curBitmap.Width + ((Math.Abs(j - 1)) % curBitmap.Width)],
                                            grayValues[((Math.Abs(i - 1)) % curBitmap.Height) * curBitmap.Width + ((j + 1) % curBitmap.Width)],
                                            grayValues[((i + 1) % curBitmap.Height) * curBitmap.Width + ((j + 1) % curBitmap.Width)]};
                                        Sort(sortArray);
                                        tempArray[i * curBitmap.Width + j] = sortArray[4];
                                    }
                                    break;
                                case 5:
                                    if (flagM == false)
                                    {
                                        tempArray[i * curBitmap.Width + j] = (byte)((grayValues[((Math.Abs(i - 2)) % curBitmap.Height) * curBitmap.Width + (Math.Abs(j - 2)) % curBitmap.Width] +
                                            grayValues[((Math.Abs(i - 2)) % curBitmap.Height) * curBitmap.Width + (Math.Abs(j - 1)) % curBitmap.Width] +
                                            grayValues[((Math.Abs(i - 2)) % curBitmap.Height) * curBitmap.Width + j] +
                                            grayValues[((Math.Abs(i - 2)) % curBitmap.Height) * curBitmap.Width + ((j + 1) % curBitmap.Width)] +
                                            grayValues[((Math.Abs(i - 2)) % curBitmap.Height) * curBitmap.Width + ((j + 2) % curBitmap.Width)] +
                                            grayValues[((Math.Abs(i - 1)) % curBitmap.Height) * curBitmap.Width + (Math.Abs(j - 2) % curBitmap.Width)] +
                                            grayValues[((Math.Abs(i - 1)) % curBitmap.Height) * curBitmap.Width + (Math.Abs(j - 1) % curBitmap.Width)] +
                                            grayValues[((Math.Abs(i - 1)) % curBitmap.Height) * curBitmap.Width + j] +
                                            grayValues[((Math.Abs(i - 1)) % curBitmap.Height) * curBitmap.Width + ((j + 1) % curBitmap.Width)] +
                                            grayValues[((Math.Abs(i - 1)) % curBitmap.Height) * curBitmap.Width + ((j + 2) % curBitmap.Width)] +
                                            grayValues[i * curBitmap.Width + (Math.Abs(j - 2) % curBitmap.Width)] +
                                            grayValues[i * curBitmap.Width + (Math.Abs(j - 1) % curBitmap.Width)] +
                                            grayValues[i * curBitmap.Width + j] +
                                            grayValues[i * curBitmap.Width + ((j + 1) % curBitmap.Width)] +
                                            grayValues[i * curBitmap.Width + ((j + 2) % curBitmap.Width)] +
                                            grayValues[((i + 2) % curBitmap.Height) * curBitmap.Width + (Math.Abs(j - 2) % curBitmap.Width)] +
                                            grayValues[((i + 2) % curBitmap.Height) * curBitmap.Width + (Math.Abs(j - 1) % curBitmap.Width)] +
                                            grayValues[((i + 2) % curBitmap.Height) * curBitmap.Width + j] +
                                            grayValues[((i + 2) % curBitmap.Height) * curBitmap.Width + ((j + 1) % curBitmap.Width)] +
                                            grayValues[((i + 2) % curBitmap.Height) * curBitmap.Width + ((j + 2) % curBitmap.Width)] +
                                            grayValues[((i + 1) % curBitmap.Height) * curBitmap.Width + (Math.Abs(j - 2) % curBitmap.Width)] +
                                            grayValues[((i + 1) % curBitmap.Height) * curBitmap.Width + (Math.Abs(j - 1) % curBitmap.Width)] +
                                            grayValues[((i + 1) % curBitmap.Height) * curBitmap.Width + j] +
                                            grayValues[((i + 1) % curBitmap.Height) * curBitmap.Width + ((j + 1) % curBitmap.Width)] +
                                            grayValues[((i + 1) % curBitmap.Height) * curBitmap.Width + ((j + 2) % curBitmap.Width)]) / 25);
                                    }
                                    else
                                    {
                                        byte[] sortArray = new byte[] {grayValues[((Math.Abs(i - 2)) % curBitmap.Height) * curBitmap.Width + (Math.Abs(j - 2)) % curBitmap.Width],
                                            grayValues[((Math.Abs(i - 2)) % curBitmap.Height) * curBitmap.Width + (Math.Abs(j - 1)) % curBitmap.Width],
                                            grayValues[((Math.Abs(i - 2)) % curBitmap.Height) * curBitmap.Width + j],
                                            grayValues[((Math.Abs(i - 2)) % curBitmap.Height) * curBitmap.Width + ((j + 1) % curBitmap.Width)],
                                            grayValues[((Math.Abs(i - 2)) % curBitmap.Height) * curBitmap.Width + ((j + 2) % curBitmap.Width)],
                                            grayValues[((Math.Abs(i - 1)) % curBitmap.Height) * curBitmap.Width + (Math.Abs(j - 2) % curBitmap.Width)],
                                            grayValues[((Math.Abs(i - 1)) % curBitmap.Height) * curBitmap.Width + (Math.Abs(j - 1) % curBitmap.Width)],
                                            grayValues[((Math.Abs(i - 1)) % curBitmap.Height) * curBitmap.Width + j],
                                            grayValues[((Math.Abs(i - 1)) % curBitmap.Height) * curBitmap.Width + ((j + 1) % curBitmap.Width)],
                                            grayValues[((Math.Abs(i - 1)) % curBitmap.Height) * curBitmap.Width + ((j + 2) % curBitmap.Width)],
                                            grayValues[i * curBitmap.Width + (Math.Abs(j - 2) % curBitmap.Width)],
                                            grayValues[i * curBitmap.Width + (Math.Abs(j - 1) % curBitmap.Width)],
                                            grayValues[i * curBitmap.Width + j],
                                            grayValues[i * curBitmap.Width + ((j + 1) % curBitmap.Width)],
                                            grayValues[i * curBitmap.Width + ((j + 2) % curBitmap.Width)],
                                            grayValues[((i + 2) % curBitmap.Height) * curBitmap.Width + (Math.Abs(j - 2) % curBitmap.Width)],
                                            grayValues[((i + 2) % curBitmap.Height) * curBitmap.Width + (Math.Abs(j - 1) % curBitmap.Width)],
                                            grayValues[((i + 2) % curBitmap.Height) * curBitmap.Width + j],
                                            grayValues[((i + 2) % curBitmap.Height) * curBitmap.Width + ((j + 1) % curBitmap.Width)],
                                            grayValues[((i + 2) % curBitmap.Height) * curBitmap.Width + ((j + 2) % curBitmap.Width)],
                                            grayValues[((i + 1) % curBitmap.Height) * curBitmap.Width + (Math.Abs(j - 2) % curBitmap.Width)],
                                            grayValues[((i + 1) % curBitmap.Height) * curBitmap.Width + (Math.Abs(j - 1) % curBitmap.Width)],
                                            grayValues[((i + 1) % curBitmap.Height) * curBitmap.Width + j],
                                            grayValues[((i + 1) % curBitmap.Height) * curBitmap.Width + ((j + 1) % curBitmap.Width)],
                                            grayValues[((i + 1) % curBitmap.Height) * curBitmap.Width + ((j + 2) % curBitmap.Width)]};
                                        Sort(sortArray);
                                        tempArray[i * curBitmap.Width + j] = sortArray[12];
                                    }
                                    break;
                                case 7:
                                    if (flagM == false)
                                    {
                                        tempArray[i * curBitmap.Width + j] = (byte)((grayValues[((Math.Abs(i - 2)) % curBitmap.Height) * curBitmap.Width + (Math.Abs(j - 2)) % curBitmap.Width] +
                                            grayValues[((Math.Abs(i - 2)) % curBitmap.Height) * curBitmap.Width + (Math.Abs(j - 1)) % curBitmap.Width] +
                                            grayValues[((Math.Abs(i - 2)) % curBitmap.Height) * curBitmap.Width + j] +
                                            grayValues[((Math.Abs(i - 2)) % curBitmap.Height) * curBitmap.Width + ((j + 1) % curBitmap.Width)] +
                                            grayValues[((Math.Abs(i - 2)) % curBitmap.Height) * curBitmap.Width + ((j + 2) % curBitmap.Width)] +
                                            grayValues[((Math.Abs(i - 1)) % curBitmap.Height) * curBitmap.Width + (Math.Abs(j - 2) % curBitmap.Width)] +
                                            grayValues[((Math.Abs(i - 1)) % curBitmap.Height) * curBitmap.Width + (Math.Abs(j - 1) % curBitmap.Width)] +
                                            grayValues[((Math.Abs(i - 1)) % curBitmap.Height) * curBitmap.Width + j] +
                                            grayValues[((Math.Abs(i - 1)) % curBitmap.Height) * curBitmap.Width + ((j + 1) % curBitmap.Width)] +
                                            grayValues[((Math.Abs(i - 1)) % curBitmap.Height) * curBitmap.Width + ((j + 2) % curBitmap.Width)] +
                                            grayValues[i * curBitmap.Width + (Math.Abs(j - 2) % curBitmap.Width)] +
                                            grayValues[i * curBitmap.Width + (Math.Abs(j - 1) % curBitmap.Width)] +
                                            grayValues[i * curBitmap.Width + j] +
                                            grayValues[i * curBitmap.Width + ((j + 1) % curBitmap.Width)] +
                                            grayValues[i * curBitmap.Width + ((j + 2) % curBitmap.Width)] +
                                            grayValues[((i + 2) % curBitmap.Height) * curBitmap.Width + (Math.Abs(j - 2) % curBitmap.Width)] +
                                            grayValues[((i + 2) % curBitmap.Height) * curBitmap.Width + (Math.Abs(j - 1) % curBitmap.Width)] +
                                            grayValues[((i + 2) % curBitmap.Height) * curBitmap.Width + j] +
                                            grayValues[((i + 2) % curBitmap.Height) * curBitmap.Width + ((j + 1) % curBitmap.Width)] +
                                            grayValues[((i + 2) % curBitmap.Height) * curBitmap.Width + ((j + 2) % curBitmap.Width)] +
                                            grayValues[((i + 1) % curBitmap.Height) * curBitmap.Width + (Math.Abs(j - 2) % curBitmap.Width)] +
                                            grayValues[((i + 1) % curBitmap.Height) * curBitmap.Width + (Math.Abs(j - 1) % curBitmap.Width)] +
                                            grayValues[((i + 1) % curBitmap.Height) * curBitmap.Width + j] +
                                            grayValues[((i + 1) % curBitmap.Height) * curBitmap.Width + ((j + 1) % curBitmap.Width)] +
                                            grayValues[((i + 1) % curBitmap.Height) * curBitmap.Width + ((j + 2) % curBitmap.Width)] +
                                            grayValues[((Math.Abs(i - 3)) % curBitmap.Height) * curBitmap.Width + (Math.Abs(j - 2) % curBitmap.Width)] +
                                            grayValues[((Math.Abs(i - 3)) % curBitmap.Height) * curBitmap.Width + (Math.Abs(j - 1) % curBitmap.Width)] +
                                            grayValues[((Math.Abs(i - 3)) % curBitmap.Height) * curBitmap.Width + j] +
                                            grayValues[((Math.Abs(i - 3)) % curBitmap.Height) * curBitmap.Width + ((j + 1) % curBitmap.Width)] +
                                            grayValues[((Math.Abs(i - 3)) % curBitmap.Height) * curBitmap.Width + ((j + 2) % curBitmap.Width)] +
                                            grayValues[((Math.Abs(i - 3)) % curBitmap.Height) * curBitmap.Width + (Math.Abs(j - 3) % curBitmap.Width)] +
                                            grayValues[((Math.Abs(i - 3)) % curBitmap.Height) * curBitmap.Width + ((j + 3) % curBitmap.Width)] +
                                            grayValues[((Math.Abs(i - 2)) % curBitmap.Height) * curBitmap.Width + ((j + 3) % curBitmap.Width)] +
                                            grayValues[((i + 2) % curBitmap.Height) * curBitmap.Width + ((j + 3) % curBitmap.Width)] +
                                            grayValues[((Math.Abs(i - 1)) % curBitmap.Height) * curBitmap.Width + ((j + 3) % curBitmap.Width)] +
                                            grayValues[((i + 1) % curBitmap.Height) * curBitmap.Width + ((j + 3) % curBitmap.Width)] +
                                            grayValues[i * curBitmap.Width + ((j + 3) % curBitmap.Width)] +
                                            grayValues[i * curBitmap.Width + (Math.Abs(j - 3) % curBitmap.Width)] +
                                            grayValues[((i + 1) % curBitmap.Height) * curBitmap.Width + (Math.Abs(j - 3) % curBitmap.Width)] +
                                            grayValues[((Math.Abs(i - 1)) % curBitmap.Height) * curBitmap.Width + (Math.Abs(j - 3) % curBitmap.Width)] +
                                            grayValues[((i + 2) % curBitmap.Height) * curBitmap.Width + (Math.Abs(j - 3) % curBitmap.Width)] +
                                            grayValues[((Math.Abs(i - 2)) % curBitmap.Height) * curBitmap.Width + (Math.Abs(j - 3) % curBitmap.Width)] +
                                            grayValues[((i + 3) % curBitmap.Height) * curBitmap.Width + ((j + 1) % curBitmap.Width)] +
                                            grayValues[((i + 3) % curBitmap.Height) * curBitmap.Width + ((j + 2) % curBitmap.Width)] +
                                            grayValues[((i + 3) % curBitmap.Height) * curBitmap.Width + (Math.Abs(j - 2) % curBitmap.Width)] +
                                            grayValues[((i + 3) % curBitmap.Height) * curBitmap.Width + (Math.Abs(j - 1) % curBitmap.Width)] +
                                            grayValues[((i + 3) % curBitmap.Height) * curBitmap.Width + j] +
                                            grayValues[((i + 3) % curBitmap.Height) * curBitmap.Width + ((j + 3) % curBitmap.Width)] +
                                            grayValues[((i + 3) % curBitmap.Height) * curBitmap.Width + (Math.Abs(j - 3) % curBitmap.Width)]) / 49);
                                    }
                                    else
                                    {
                                        byte[] sortArray = new byte[] {grayValues[((Math.Abs(i - 2)) % curBitmap.Height) * curBitmap.Width + (Math.Abs(j - 2)) % curBitmap.Width],
                                            grayValues[((Math.Abs(i - 2)) % curBitmap.Height) * curBitmap.Width + (Math.Abs(j - 1)) % curBitmap.Width],
                                            grayValues[((Math.Abs(i - 2)) % curBitmap.Height) * curBitmap.Width + j],
                                            grayValues[((Math.Abs(i - 2)) % curBitmap.Height) * curBitmap.Width + ((j + 1) % curBitmap.Width)],
                                            grayValues[((Math.Abs(i - 2)) % curBitmap.Height) * curBitmap.Width + ((j + 2) % curBitmap.Width)],
                                            grayValues[((Math.Abs(i - 1)) % curBitmap.Height) * curBitmap.Width + (Math.Abs(j - 2) % curBitmap.Width)],
                                            grayValues[((Math.Abs(i - 1)) % curBitmap.Height) * curBitmap.Width + (Math.Abs(j - 1) % curBitmap.Width)],
                                            grayValues[((Math.Abs(i - 1)) % curBitmap.Height) * curBitmap.Width + j],
                                            grayValues[((Math.Abs(i - 1)) % curBitmap.Height) * curBitmap.Width + ((j + 1) % curBitmap.Width)],
                                            grayValues[((Math.Abs(i - 1)) % curBitmap.Height) * curBitmap.Width + ((j + 2) % curBitmap.Width)],
                                            grayValues[i * curBitmap.Width + (Math.Abs(j - 2) % curBitmap.Width)],
                                            grayValues[i * curBitmap.Width + (Math.Abs(j - 1) % curBitmap.Width)],
                                            grayValues[i * curBitmap.Width + j],
                                            grayValues[i * curBitmap.Width + ((j + 1) % curBitmap.Width)],
                                            grayValues[i * curBitmap.Width + ((j + 2) % curBitmap.Width)],
                                            grayValues[((i + 2) % curBitmap.Height) * curBitmap.Width + (Math.Abs(j - 2) % curBitmap.Width)],
                                            grayValues[((i + 2) % curBitmap.Height) * curBitmap.Width + (Math.Abs(j - 1) % curBitmap.Width)],
                                            grayValues[((i + 2) % curBitmap.Height) * curBitmap.Width + j],
                                            grayValues[((i + 2) % curBitmap.Height) * curBitmap.Width + ((j + 1) % curBitmap.Width)],
                                            grayValues[((i + 2) % curBitmap.Height) * curBitmap.Width + ((j + 2) % curBitmap.Width)],
                                            grayValues[((i + 1) % curBitmap.Height) * curBitmap.Width + (Math.Abs(j - 2) % curBitmap.Width)],
                                            grayValues[((i + 1) % curBitmap.Height) * curBitmap.Width + (Math.Abs(j - 1) % curBitmap.Width)],
                                            grayValues[((i + 1) % curBitmap.Height) * curBitmap.Width + j],
                                            grayValues[((i + 1) % curBitmap.Height) * curBitmap.Width + ((j + 1) % curBitmap.Width)],
                                            grayValues[((i + 1) % curBitmap.Height) * curBitmap.Width + ((j + 2) % curBitmap.Width)],
                                            grayValues[((Math.Abs(i - 3)) % curBitmap.Height) * curBitmap.Width + (Math.Abs(j - 2) % curBitmap.Width)],
                                            grayValues[((Math.Abs(i - 3)) % curBitmap.Height) * curBitmap.Width + (Math.Abs(j - 1) % curBitmap.Width)],
                                            grayValues[((Math.Abs(i - 3)) % curBitmap.Height) * curBitmap.Width + j],
                                            grayValues[((Math.Abs(i - 3)) % curBitmap.Height) * curBitmap.Width + ((j + 1) % curBitmap.Width)],
                                            grayValues[((Math.Abs(i - 3)) % curBitmap.Height) * curBitmap.Width + ((j + 2) % curBitmap.Width)],
                                            grayValues[((Math.Abs(i - 3)) % curBitmap.Height) * curBitmap.Width + (Math.Abs(j - 3) % curBitmap.Width)],
                                            grayValues[((Math.Abs(i - 3)) % curBitmap.Height) * curBitmap.Width + ((j + 3) % curBitmap.Width)],
                                            grayValues[((Math.Abs(i - 2)) % curBitmap.Height) * curBitmap.Width + ((j + 3) % curBitmap.Width)],
                                            grayValues[((i + 2) % curBitmap.Height) * curBitmap.Width + ((j + 3) % curBitmap.Width)],
                                            grayValues[((Math.Abs(i - 1)) % curBitmap.Height) * curBitmap.Width + ((j + 3) % curBitmap.Width)],
                                            grayValues[((i + 1) % curBitmap.Height) * curBitmap.Width + ((j + 3) % curBitmap.Width)],
                                            grayValues[i * curBitmap.Width + ((j + 3) % curBitmap.Width)],
                                            grayValues[i * curBitmap.Width + (Math.Abs(j - 3) % curBitmap.Width)],
                                            grayValues[((i + 1) % curBitmap.Height) * curBitmap.Width + (Math.Abs(j - 3) % curBitmap.Width)],
                                            grayValues[((Math.Abs(i - 1)) % curBitmap.Height) * curBitmap.Width + (Math.Abs(j - 3) % curBitmap.Width)],
                                            grayValues[((i + 2) % curBitmap.Height) * curBitmap.Width + (Math.Abs(j - 3) % curBitmap.Width)],
                                            grayValues[((Math.Abs(i - 2)) % curBitmap.Height) * curBitmap.Width + (Math.Abs(j - 3) % curBitmap.Width)],
                                            grayValues[((i + 3) % curBitmap.Height) * curBitmap.Width + ((j + 1) % curBitmap.Width)],
                                            grayValues[((i + 3) % curBitmap.Height) * curBitmap.Width + ((j + 2) % curBitmap.Width)],
                                            grayValues[((i + 3) % curBitmap.Height) * curBitmap.Width + (Math.Abs(j - 2) % curBitmap.Width)],
                                            grayValues[((i + 3) % curBitmap.Height) * curBitmap.Width + (Math.Abs(j - 1) % curBitmap.Width)],
                                            grayValues[((i + 3) % curBitmap.Height) * curBitmap.Width + j],
                                            grayValues[((i + 3) % curBitmap.Height) * curBitmap.Width + ((j + 3) % curBitmap.Width)],
                                            grayValues[((i + 3) % curBitmap.Height) * curBitmap.Width + (Math.Abs(j - 3) % curBitmap.Width)]};
                                        Sort(sortArray);
                                        tempArray[i * curBitmap.Width + j] = sortArray[24];
                                    }
                                    break;
                                default:
                                    MessageBox.Show("无效！");
                                    break;
                            }
                        }
                    }

                    grayValues = (byte[])tempArray.Clone();
                                        
                    System.Runtime.InteropServices.Marshal.Copy(grayValues, 0, ptr, bytes);
                    curBitmap.UnlockBits(bmpData);
                }
                
                Invalidate();
            }
        }

        private void Sort(byte[] list)
        {
            int min;
            for (int i = 0; i < list.Length - 1; i++)
            {
                min = i;
                for (int j = i + 1; j < list.Length; j++)
                {
                    if (list[j] < list[min])
                        min = j;
                }
                byte t = list[min];
                list[min] = list[i];
                list[i] = t;
            }
        }

        private void grayMor_Click(object sender, EventArgs e)
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

                    byte[] tempArray = new byte[bytes];
                    byte[] struEle = new byte[25];
                    struEle = grayMor.GetStruction;

                    tempArray = grayClose(grayValues, struEle, curBitmap.Height, curBitmap.Width);
                    grayValues = grayOpen(tempArray, struEle, curBitmap.Height, curBitmap.Width);

                    System.Runtime.InteropServices.Marshal.Copy(grayValues, 0, ptr, bytes);
                    curBitmap.UnlockBits(bmpData);
                }

                Invalidate();
            }
        }

        private byte[] grayDelation(byte[] grayImage, byte[] se,int tHeight,int tWidth)
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

        private byte[] grayOpen(byte[] grayImage, byte[] se, int tHeight, int tWidth)
        {
            byte[] tempImage = new byte[grayImage.Length];
            tempImage = grayErode(grayImage, se, tHeight, tWidth);
            return (grayDelation(tempImage, se, tHeight, tWidth));
        }

        private byte[] grayClose(byte[] grayImage, byte[] se, int tHeight, int tWidth)
        {
            byte[] tempImage = new byte[grayImage.Length];
            tempImage = grayDelation(grayImage, se, tHeight, tWidth);
            return (grayErode(tempImage, se, tHeight, tWidth));
        }

        private void wavelet_Click(object sender, EventArgs e)
        {
            if (curBitmap != null)
            {
                wavelet dwt = new wavelet();
                if (dwt.ShowDialog() == DialogResult.OK)
                {
                    Rectangle rect = new Rectangle(0, 0, curBitmap.Width, curBitmap.Height);
                    System.Drawing.Imaging.BitmapData bmpData = curBitmap.LockBits(rect, System.Drawing.Imaging.ImageLockMode.ReadWrite, curBitmap.PixelFormat);
                    IntPtr ptr = bmpData.Scan0;
                    int bytes = curBitmap.Width * curBitmap.Height;
                    byte[] grayValues = new byte[bytes];
                    System.Runtime.InteropServices.Marshal.Copy(ptr, grayValues, 0, bytes);

                    double[] tempA = new double[bytes];
                    double[] tempB = new double[bytes];
                    for (int i = 0; i < bytes; i++)
                    {
                        tempA[i] = Convert.ToDouble(grayValues[i]);
                    }

                    byte thresholding = dwt.GetThresholding;
                    byte dwtSeries = dwt.GetSeries;
                    byte flagFilter = dwt.GetFlagV;
                    double[] lowFilter = null;
                    double[] highFilter = null;

                    switch (flagFilter & 0x0f)
                    {
                        case 0://haar
                            lowFilter = new double[] { 0.70710678118655, 0.70710678118655 };                           
                            break;
                        case 1://daubechies2
                            lowFilter = new double[] { 0.48296291314453, 0.83651630373780, 0.22414386804201, -0.12940952255126 };
                            break;
                        case 2://daubechies3
                            lowFilter = new double[] { 0.33267055295008, 0.80689150931109, 0.45987750211849, -0.13501102001025, -0.08544127388203, 0.03522629188571 };
                            break;
                        case 3://daubechies4
                            lowFilter = new double[] { 0.23037781330889, 0.71484657055291, 0.63088076792986, -0.02798376941686, -0.18703481171909, 0.03084138183556, 0.03288301166689, -0.01059740178507 };
                            break;
                        case 4://daubechies5
                            lowFilter = new double[] { 0.16010239797419, 0.60382926979719, 0.72430852843778, 0.13842814590132, -0.24229488706638, -0.03224486958464, 0.07757149384005, -0.00624149021280, -0.01258075199908, 0.00333572528547 };
                            break;
                        case 5://daubechies6
                            lowFilter = new double[] { 0.11154074335011, 0.49462389039845, 0.75113390802110, 0.31525035170920, -0.22626469396544, -0.12976686756726, 0.09750160558732, 0.02752286553031, -0.03158203931849, 0.00055384220116, 0.00477725751195,	-0.00107730108531 };
                            break;
                        default:
                            MessageBox.Show("无效！");
                            break;
                    }

                    highFilter = new double[lowFilter.Length];
                    for (int i = 0; i < lowFilter.Length; i++)
                    {
                        highFilter[i] = Math.Pow(-1, i) * lowFilter[lowFilter.Length - 1 - i];
                    }

                    for (int k = 0; k < dwtSeries; k++)
                    {
                        int coef = (int)Math.Pow(2, k);
                        for (int i = 0; i < curBitmap.Height; i++)
                        {
                            if (i < curBitmap.Height / coef)
                            {
                                for (int j = 0; j < curBitmap.Width; j++)
                                {
                                    if (j < curBitmap.Width / coef)
                                    {
                                        tempB[i * curBitmap.Width / coef + j] = tempA[i * curBitmap.Width + j];
                                    }
                                }
                            }
                        }
                        wavelet2D(ref tempB, lowFilter, highFilter, coef);
                        for (int i = 0; i < curBitmap.Height; i++)
                        {
                            if (i < curBitmap.Height / coef)
                            {
                                for (int j = 0; j < curBitmap.Width; j++)
                                {
                                    if (j < curBitmap.Width / coef)
                                    {
                                        tempA[i * curBitmap.Width + j] = tempB[i * curBitmap.Width / coef + j];
                                    }
                                }
                            }
                        }

                       if ((flagFilter & 0xf0) == 0x10)
                        {
                            for (int l = 0; l < bytes; l++)
                            {
                                if (tempA[l] < thresholding && tempA[l] > -thresholding)
                                    tempA[l] = 0;
                            }
                        }
                        else
                        {
                            for (int l = 0; l < bytes; l++)
                            {
                                if (tempA[l] >= thresholding)
                                    tempA[l] = tempA[l] - thresholding;
                                else
                                {
                                    if (tempA[l] <= -thresholding)
                                        tempA[l] = tempA[l] + thresholding;
                                    else
                                        tempA[l] = 0;
                                }
                            }
                        }
                    }

                    for (int k = dwtSeries - 1; k >= 0; k--)
                    {
                        int coef = (int)Math.Pow(2, k);
                        for (int i = 0; i < curBitmap.Height; i++)
                        {
                            if (i < curBitmap.Height / coef)
                            {
                                for (int j = 0; j < curBitmap.Width; j++)
                                {
                                    if (j < curBitmap.Width / coef)
                                    {
                                        tempB[i * curBitmap.Width / coef + j] = tempA[i * curBitmap.Width + j];
                                    }
                                }
                            }
                        }
                        iwavelet2D(ref tempB, lowFilter, highFilter, coef);
                        for (int i = 0; i < curBitmap.Height; i++)
                        {
                            if (i < curBitmap.Height / coef)
                            {
                                for (int j = 0; j < curBitmap.Width; j++)
                                {
                                    if (j < curBitmap.Width / coef)
                                    {
                                        tempA[i * curBitmap.Width + j] = tempB[i * curBitmap.Width / coef + j];
                                    }
                                }
                            }
                        }
                    }

                    for (int i = 0; i < bytes; i++)
                    {
                        if (tempA[i] >= 255)
                            tempA[i] = 255;
                        if (tempA[i] <= 0)
                            tempA[i] = 0;
                        grayValues[i] = Convert.ToByte(tempA[i]);
                    }

                    System.Runtime.InteropServices.Marshal.Copy(grayValues, 0, ptr, bytes);
                    curBitmap.UnlockBits(bmpData);
                }

                Invalidate();
            }
        }

        private void wavelet1D(double[] scl0, double[] p, double[] q, out  double[] scl1, out double[] wvl1)
        {
            int temp;
            int sclLen = scl0.Length;
            int pLen = p.Length;
            scl1 = new double[sclLen / 2];
            wvl1 = new double[sclLen / 2];

            for (int i = 0; i < sclLen / 2; i++)
            {
                scl1[i] = 0.0;
                wvl1[i] = 0.0;
                for (int j = 0; j < pLen; j++)
                {
                    temp = (j + i * 2) % sclLen;
                    scl1[i] += p[j] * scl0[temp];
                    wvl1[i] += q[j] * scl0[temp];
                }
            }
        }

        private void iwavelet1D(out double[] scl0, double[] p, double[] q, double[] scl1, double[] wvl1)
        {
            int temp;
            int sclLen = scl1.Length;
            int pLen = p.Length;
            scl0 = new double[sclLen * 2];

            for (int i = 0; i < sclLen; i++)
            {
                scl0[2 * i + 1] = 0.0;
                scl0[2 * i] = 0.0;
                for (int j = 0; j < pLen / 2; j++)
                {
                    temp = (i - j + sclLen) % sclLen;
                    scl0[2 * i + 1] += p[2 * j + 1] * scl1[temp] + q[2 * j + 1] * wvl1[temp];
                    scl0[2 * i] += p[2 * j] * scl1[temp] + q[2 * j] * wvl1[temp];
                }
            }
        }

        private void wavelet2D(ref double[] dataImage, double[] p, double[] q, int series/*out double[] scl1, out double[] wvl1*/)
        {
            double[] s = new double[curBitmap.Width / series];
            double[] s1 = new double[curBitmap.Width / (2 * series)];
            double[] w1 = new double[curBitmap.Width / (2 * series)];
            for (int i = 0; i < curBitmap.Height / series; i++)
            {
                for (int j = 0; j < curBitmap.Width / series; j++)
                {
                    s[j] = dataImage[i * curBitmap.Width / series + j];
                }
                wavelet1D(s, p, q, out s1, out w1);

                for (int j = 0; j < curBitmap.Width / series; j++)
                {
                    if (j < curBitmap.Width / (2 * series))
                        dataImage[i * curBitmap.Width / series + j] = s1[j];
                    else
                        dataImage[i * curBitmap.Width / series + j] = w1[j - curBitmap.Width / (2 * series)];
                }
            }

            for (int i = 0; i < curBitmap.Width / series; i++)
            {
                for (int j = 0; j < curBitmap.Height / series; j++)
                {
                    s[j] = dataImage[j * curBitmap.Width / series + i];
                }
                wavelet1D(s, p, q, out s1, out w1);
                for (int j = 0; j < curBitmap.Height / series; j++)
                {
                    if (j < curBitmap.Height / (2 * series))
                        dataImage[j * curBitmap.Width / series + i] = s1[j];
                    else
                        dataImage[j * curBitmap.Width / series + i] = w1[j - curBitmap.Height / (2 * series)];
                }
            }
        }

        private void iwavelet2D(ref double[] dataImage, double[] p, double[] q, int series)
        {
            double[] s = new double[curBitmap.Width / series];
            double[] s1 = new double[curBitmap.Width / (2 * series)];
            double[] w1 = new double[curBitmap.Width / (2 * series)];
            for (int i = 0; i < curBitmap.Width / series; i++)
            {
                for (int j = 0; j < curBitmap.Height / series; j++)
                {
                    if (j < curBitmap.Height / (2 * series))
                        s1[j] = dataImage[j * curBitmap.Width / series + i];
                    else
                        w1[j - curBitmap.Height / (2 * series)] = dataImage[j * curBitmap.Width / series + i];
                }
                iwavelet1D(out s, p, q, s1, w1);
                for (int j = 0; j < curBitmap.Height / series; j++)
                {
                    dataImage[j * curBitmap.Width / series + i] = s[j];
                }
            }
            for (int i = 0; i < curBitmap.Height / series; i++)
            {
                for (int j = 0; j < curBitmap.Width / series; j++)
                {
                    if (j < curBitmap.Width / (2 * series))
                        s1[j] = dataImage[i * curBitmap.Width / series + j];
                    else
                        w1[j - curBitmap.Width / (2 * series)] = dataImage[i * curBitmap.Width / series + j];
                }
                iwavelet1D(out s, p, q, s1, w1);
                for (int j = 0; j < curBitmap.Width / series; j++)
                {
                    dataImage[i * curBitmap.Width / series + j] = s[j];
                }
            }
        }

        private void gauss_Click(object sender, EventArgs e)
        {
            if (curBitmap != null)
            {
                gauss gaussFilter = new gauss();
                if (gaussFilter.ShowDialog() == DialogResult.OK)
                {
                    Rectangle rect = new Rectangle(0, 0, curBitmap.Width, curBitmap.Height);
                    System.Drawing.Imaging.BitmapData bmpData = curBitmap.LockBits(rect, System.Drawing.Imaging.ImageLockMode.ReadWrite, curBitmap.PixelFormat);
                    IntPtr ptr = bmpData.Scan0;
                    int bytes = curBitmap.Width * curBitmap.Height;
                    byte[] grayValues = new byte[bytes];
                    System.Runtime.InteropServices.Marshal.Copy(ptr, grayValues, 0, bytes);

                    double[] tempArray;
                    double[] tempImage = new double[bytes];
                    double sigma = gaussFilter.GetSigma;
                    for (int i = 0; i < bytes; i++)
                    {
                        tempImage[i] = Convert.ToDouble(grayValues[i]);
                    }

                    gaussSmooth(tempImage, out tempArray, sigma);

                    for (int i = 0; i < bytes; i++)
                    {
                        if (tempArray[i] > 255)
                            grayValues[i] = 255;
                        else if (tempArray[i] < 0)
                            grayValues[i] = 0;
                        else
                            grayValues[i] = Convert.ToByte(tempArray[i]);
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

        private void statistic_Click(object sender, EventArgs e)
        {
            if (curBitmap != null)
            {
                stati staticSmooth = new stati();
                if (staticSmooth.ShowDialog() == DialogResult.OK)
                {
                    Rectangle rect = new Rectangle(0, 0, curBitmap.Width, curBitmap.Height);
                    System.Drawing.Imaging.BitmapData bmpData = curBitmap.LockBits(rect, System.Drawing.Imaging.ImageLockMode.ReadWrite, curBitmap.PixelFormat);
                    IntPtr ptr = bmpData.Scan0;
                    int bytes = curBitmap.Width * curBitmap.Height;
                    byte[] grayValues = new byte[bytes];
                    System.Runtime.InteropServices.Marshal.Copy(ptr, grayValues, 0, bytes);

                    byte[] tempArray = new byte[bytes];
                    double thresholding = staticSmooth.GetThresholding;
                    bool flag = staticSmooth.GetWindows;

                    if (flag == false)
                    {
                        for (int i = 0; i < curBitmap.Height; i++)
                        {
                            for (int j = 0; j < curBitmap.Width; j++)
                            {
                                double mu = 0, sigma = 0;
                                mu = (grayValues[i * curBitmap.Width + j] +
                                        grayValues[((Math.Abs(i - 1)) % curBitmap.Height) * curBitmap.Width + j] +
                                        grayValues[((i + 1) % curBitmap.Height) * curBitmap.Width + j] +
                                        grayValues[i * curBitmap.Width + ((j + 1) % curBitmap.Width)] +
                                        grayValues[i * curBitmap.Width + ((Math.Abs(j - 1)) % curBitmap.Width)] +
                                        grayValues[((Math.Abs(i - 1)) % curBitmap.Height) * curBitmap.Width + ((Math.Abs(j - 1)) % curBitmap.Width)] +
                                        grayValues[((i + 1) % curBitmap.Height) * curBitmap.Width + ((Math.Abs(j - 1)) % curBitmap.Width)] +
                                        grayValues[((Math.Abs(i - 1)) % curBitmap.Height) * curBitmap.Width + ((j + 1) % curBitmap.Width)] +
                                        grayValues[((i + 1) % curBitmap.Height) * curBitmap.Width + ((j + 1) % curBitmap.Width)]) / 9;

                                sigma = Math.Sqrt((Math.Pow((grayValues[i * curBitmap.Width + j] - mu), 2) +
                                        Math.Pow((grayValues[((Math.Abs(i - 1)) % curBitmap.Height) * curBitmap.Width + j] - mu), 2) +
                                        Math.Pow((grayValues[((i + 1) % curBitmap.Height) * curBitmap.Width + j] - mu), 2) +
                                        Math.Pow((grayValues[i * curBitmap.Width + ((j + 1) % curBitmap.Width)] - mu), 2) +
                                        Math.Pow((grayValues[i * curBitmap.Width + ((Math.Abs(j - 1)) % curBitmap.Width)] - mu), 2) +
                                        Math.Pow((grayValues[((Math.Abs(i - 1)) % curBitmap.Height) * curBitmap.Width + ((Math.Abs(j - 1)) % curBitmap.Width)] - mu), 2) +
                                        Math.Pow((grayValues[((i + 1) % curBitmap.Height) * curBitmap.Width + ((Math.Abs(j - 1)) % curBitmap.Width)] - mu), 2) +
                                        Math.Pow((grayValues[((Math.Abs(i - 1)) % curBitmap.Height) * curBitmap.Width + ((j + 1) % curBitmap.Width)] - mu), 2) +
                                        Math.Pow((grayValues[((i + 1) % curBitmap.Height) * curBitmap.Width + ((j + 1) % curBitmap.Width)] - mu), 2)) / 9);

                                if (Math.Abs(grayValues[i * curBitmap.Width + j] - mu) < sigma * thresholding)
                                    tempArray[i * curBitmap.Width + j] = grayValues[i * curBitmap.Width + j];
                                else
                                    tempArray[i * curBitmap.Width + j] = Convert.ToByte(mu);

                            }
                        }
                    }
                    else
                    {
                        for (int i = 0; i < curBitmap.Height; i++)
                        {
                            for (int j = 0; j < curBitmap.Width; j++)
                            {
                                double mu = 0, sigma = 0;
                                mu = (grayValues[((Math.Abs(i - 2)) % curBitmap.Height) * curBitmap.Width + (Math.Abs(j - 2)) % curBitmap.Width] +
                                        grayValues[((Math.Abs(i - 2)) % curBitmap.Height) * curBitmap.Width + (Math.Abs(j - 1)) % curBitmap.Width] +
                                        grayValues[((Math.Abs(i - 2)) % curBitmap.Height) * curBitmap.Width + j] +
                                        grayValues[((Math.Abs(i - 2)) % curBitmap.Height) * curBitmap.Width + ((j + 1) % curBitmap.Width)] +
                                        grayValues[((Math.Abs(i - 2)) % curBitmap.Height) * curBitmap.Width + ((j + 2) % curBitmap.Width)] +
                                        grayValues[((Math.Abs(i - 1)) % curBitmap.Height) * curBitmap.Width + (Math.Abs(j - 2) % curBitmap.Width)] +
                                        grayValues[((Math.Abs(i - 1)) % curBitmap.Height) * curBitmap.Width + (Math.Abs(j - 1) % curBitmap.Width)] +
                                        grayValues[((Math.Abs(i - 1)) % curBitmap.Height) * curBitmap.Width + j] +
                                        grayValues[((Math.Abs(i - 1)) % curBitmap.Height) * curBitmap.Width + ((j + 1) % curBitmap.Width)] +
                                        grayValues[((Math.Abs(i - 1)) % curBitmap.Height) * curBitmap.Width + ((j + 2) % curBitmap.Width)] +
                                        grayValues[i * curBitmap.Width + (Math.Abs(j - 2) % curBitmap.Width)] +
                                        grayValues[i * curBitmap.Width + (Math.Abs(j - 1) % curBitmap.Width)] +
                                        grayValues[i * curBitmap.Width + j] +
                                        grayValues[i * curBitmap.Width + ((j + 1) % curBitmap.Width)] +
                                        grayValues[i * curBitmap.Width + ((j + 2) % curBitmap.Width)] +
                                        grayValues[((i + 2) % curBitmap.Height) * curBitmap.Width + (Math.Abs(j - 2) % curBitmap.Width)] +
                                        grayValues[((i + 2) % curBitmap.Height) * curBitmap.Width + (Math.Abs(j - 1) % curBitmap.Width)] +
                                        grayValues[((i + 2) % curBitmap.Height) * curBitmap.Width + j] +
                                        grayValues[((i + 2) % curBitmap.Height) * curBitmap.Width + ((j + 1) % curBitmap.Width)] +
                                        grayValues[((i + 2) % curBitmap.Height) * curBitmap.Width + ((j + 2) % curBitmap.Width)] +
                                        grayValues[((i + 1) % curBitmap.Height) * curBitmap.Width + (Math.Abs(j - 2) % curBitmap.Width)] +
                                        grayValues[((i + 1) % curBitmap.Height) * curBitmap.Width + (Math.Abs(j - 1) % curBitmap.Width)] +
                                        grayValues[((i + 1) % curBitmap.Height) * curBitmap.Width + j] +
                                        grayValues[((i + 1) % curBitmap.Height) * curBitmap.Width + ((j + 1) % curBitmap.Width)] +
                                        grayValues[((i + 1) % curBitmap.Height) * curBitmap.Width + ((j + 2) % curBitmap.Width)]) / 25;

                                sigma = Math.Sqrt((Math.Pow((grayValues[((Math.Abs(i - 2)) % curBitmap.Height) * curBitmap.Width + (Math.Abs(j - 2)) % curBitmap.Width] - mu), 2) +
                                        Math.Pow((grayValues[((Math.Abs(i - 2)) % curBitmap.Height) * curBitmap.Width + (Math.Abs(j - 1)) % curBitmap.Width] - mu), 2) +
                                        Math.Pow((grayValues[((Math.Abs(i - 2)) % curBitmap.Height) * curBitmap.Width + j] - mu), 2) +
                                        Math.Pow((grayValues[((Math.Abs(i - 2)) % curBitmap.Height) * curBitmap.Width + ((j + 1) % curBitmap.Width)] - mu), 2) +
                                        Math.Pow((grayValues[((Math.Abs(i - 2)) % curBitmap.Height) * curBitmap.Width + ((j + 2) % curBitmap.Width)] - mu), 2) +
                                        Math.Pow((grayValues[((Math.Abs(i - 1)) % curBitmap.Height) * curBitmap.Width + (Math.Abs(j - 2) % curBitmap.Width)] - mu), 2) +
                                        Math.Pow((grayValues[((Math.Abs(i - 1)) % curBitmap.Height) * curBitmap.Width + (Math.Abs(j - 1) % curBitmap.Width)] - mu), 2) +
                                        Math.Pow((grayValues[((Math.Abs(i - 1)) % curBitmap.Height) * curBitmap.Width + j] - mu), 2) +
                                        Math.Pow((grayValues[((Math.Abs(i - 1)) % curBitmap.Height) * curBitmap.Width + ((j + 1) % curBitmap.Width)] - mu), 2) +
                                        Math.Pow((grayValues[((Math.Abs(i - 1)) % curBitmap.Height) * curBitmap.Width + ((j + 2) % curBitmap.Width)] - mu), 2) +
                                        Math.Pow((grayValues[i * curBitmap.Width + (Math.Abs(j - 2) % curBitmap.Width)] - mu), 2) +
                                        Math.Pow((grayValues[i * curBitmap.Width + (Math.Abs(j - 1) % curBitmap.Width)] - mu), 2) +
                                        Math.Pow((grayValues[i * curBitmap.Width + j] - mu), 2) +
                                        Math.Pow((grayValues[i * curBitmap.Width + ((j + 1) % curBitmap.Width)] - mu), 2) +
                                        Math.Pow((grayValues[i * curBitmap.Width + ((j + 2) % curBitmap.Width)] - mu), 2) +
                                        Math.Pow((grayValues[((i + 2) % curBitmap.Height) * curBitmap.Width + (Math.Abs(j - 2) % curBitmap.Width)] - mu), 2) +
                                        Math.Pow((grayValues[((i + 2) % curBitmap.Height) * curBitmap.Width + (Math.Abs(j - 1) % curBitmap.Width)] - mu), 2) +
                                        Math.Pow((grayValues[((i + 2) % curBitmap.Height) * curBitmap.Width + j] - mu), 2) +
                                        Math.Pow((grayValues[((i + 2) % curBitmap.Height) * curBitmap.Width + ((j + 1) % curBitmap.Width)] - mu), 2) +
                                        Math.Pow((grayValues[((i + 2) % curBitmap.Height) * curBitmap.Width + ((j + 2) % curBitmap.Width)] - mu), 2) +
                                        Math.Pow((grayValues[((i + 1) % curBitmap.Height) * curBitmap.Width + (Math.Abs(j - 2) % curBitmap.Width)] - mu), 2) +
                                        Math.Pow((grayValues[((i + 1) % curBitmap.Height) * curBitmap.Width + (Math.Abs(j - 1) % curBitmap.Width)] - mu), 2) +
                                        Math.Pow((grayValues[((i + 1) % curBitmap.Height) * curBitmap.Width + j] - mu), 2) +
                                        Math.Pow((grayValues[((i + 1) % curBitmap.Height) * curBitmap.Width + ((j + 1) % curBitmap.Width)] - mu), 2) +
                                        Math.Pow((grayValues[((i + 1) % curBitmap.Height) * curBitmap.Width + ((j + 2) % curBitmap.Width)] - mu), 2)) / 25);

                                if (Math.Abs(grayValues[i * curBitmap.Width + j] - mu) < sigma * thresholding)
                                    tempArray[i * curBitmap.Width + j] = grayValues[i * curBitmap.Width + j];
                                else
                                    tempArray[i * curBitmap.Width + j] = Convert.ToByte(mu);

                            }
                        }
                    }
                    grayValues = (byte[])tempArray.Clone();

                    System.Runtime.InteropServices.Marshal.Copy(grayValues, 0, ptr, bytes);
                    curBitmap.UnlockBits(bmpData);
                }

                Invalidate();
            }
        }
    }
}