using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace morphology
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

        private void erode_Click(object sender, EventArgs e)
        {
            if (curBitmap != null)
            {
                struction struForm = new struction();
                struForm.Text = "腐蚀运算结构元素";
                if (struForm.ShowDialog() == DialogResult.OK)
                {
                    Rectangle rect = new Rectangle(0, 0, curBitmap.Width, curBitmap.Height);
                    System.Drawing.Imaging.BitmapData bmpData = curBitmap.LockBits(rect, System.Drawing.Imaging.ImageLockMode.ReadWrite, curBitmap.PixelFormat);
                    IntPtr ptr = bmpData.Scan0;
                    int bytes = curBitmap.Width * curBitmap.Height;
                    byte[] grayValues = new byte[bytes];
                    System.Runtime.InteropServices.Marshal.Copy(ptr, grayValues, 0, bytes);

                    byte flagStru = struForm.GetStruction;

                    byte[] tempArray = new byte[bytes];
                    for (int i = 0; i < bytes; i++)
                    {
                        tempArray[i] = 255;
                    }

                    switch (flagStru)
                    {
                        case 0x11:
                            for (int i = 0; i < curBitmap.Height; i++)
                            {
                                for (int j = 1; j < curBitmap.Width - 1; j++)
                                {
                                    if (grayValues[i * curBitmap.Width + j] == 0 && 
                                        grayValues[i * curBitmap.Width + j + 1] == 0 && 
                                        grayValues[i * curBitmap.Width + j - 1] == 0)
                                    {
                                        tempArray[i * curBitmap.Width + j] = 0;
                                    }

                                }
                            }
                            break;
                        case 0x21:
                            for (int i = 0; i < curBitmap.Height; i++)
                            {
                                for (int j = 2; j < curBitmap.Width - 2; j++)
                                {
                                    if (grayValues[i * curBitmap.Width + j] == 0 && 
                                        grayValues[i * curBitmap.Width + j + 1] == 0 && 
                                        grayValues[i * curBitmap.Width + j - 1] == 0 && 
                                        grayValues[i * curBitmap.Width + j + 2] == 0 && 
                                        grayValues[i * curBitmap.Width + j - 2] == 0)
                                    {
                                        tempArray[i * curBitmap.Width + j] = 0;
                                    }

                                }
                            }
                            break;
                        case 0x12:
                            for (int i = 1; i < curBitmap.Height - 1; i++)
                            {
                                for (int j = 0; j < curBitmap.Width; j++)
                                {
                                    if (grayValues[i * curBitmap.Width + j] == 0 && 
                                        grayValues[(i - 1) * curBitmap.Width + j] == 0 && 
                                        grayValues[(i + 1) * curBitmap.Width + j] == 0)
                                    {
                                        tempArray[i * curBitmap.Width + j] = 0;
                                    }

                                }
                            }
                            break;
                        case 0x22:
                            for (int i = 2; i < curBitmap.Height - 2; i++)
                            {
                                for (int j = 0; j < curBitmap.Width; j++)
                                {
                                    if (grayValues[i * curBitmap.Width + j] == 0 && 
                                        grayValues[(i - 1) * curBitmap.Width + j] == 0 && 
                                        grayValues[(i + 1) * curBitmap.Width + j] == 0 && 
                                        grayValues[(i - 2) * curBitmap.Width + j] == 0 && 
                                        grayValues[(i + 2) * curBitmap.Width + j] == 0)
                                    {
                                        tempArray[i * curBitmap.Width + j] = 0;
                                    }

                                }
                            }
                            break;
                        case 0x14:
                            for (int i = 1; i < curBitmap.Height - 1; i++)
                            {
                                for (int j = 1; j < curBitmap.Width - 1; j++)
                                {
                                    if (grayValues[i * curBitmap.Width + j] == 0 && 
                                        grayValues[(i - 1) * curBitmap.Width + j] == 0 && 
                                        grayValues[(i + 1) * curBitmap.Width + j] == 0 && 
                                        grayValues[i * curBitmap.Width + j + 1] == 0 && 
                                        grayValues[i * curBitmap.Width + j - 1] == 0)
                                    {
                                        tempArray[i * curBitmap.Width + j] = 0;
                                    }

                                }
                            }
                            break;
                        case 0x24:
                            for (int i = 2; i < curBitmap.Height - 2; i++)
                            {
                                for (int j = 2; j < curBitmap.Width - 2; j++)
                                {
                                    if (grayValues[i * curBitmap.Width + j] == 0 && 
                                        grayValues[(i - 1) * curBitmap.Width + j] == 0 && 
                                        grayValues[(i + 1) * curBitmap.Width + j] == 0 && 
                                        grayValues[(i - 2) * curBitmap.Width + j] == 0 && 
                                        grayValues[(i + 2) * curBitmap.Width + j] == 0 && 
                                        grayValues[i * curBitmap.Width + j + 1] == 0 && 
                                        grayValues[i * curBitmap.Width + j - 1] == 0 && 
                                        grayValues[i * curBitmap.Width + j + 2] == 0 && 
                                        grayValues[i * curBitmap.Width + j - 2] == 0)
                                    {
                                        tempArray[i * curBitmap.Width + j] = 0;
                                    }

                                }
                            }
                            break;
                        case 0x18:
                            for (int i = 1; i < curBitmap.Height - 1; i++)
                            {
                                for (int j = 1; j < curBitmap.Width - 1; j++)
                                {
                                    if (grayValues[i * curBitmap.Width + j] == 0 && 
                                        grayValues[(i - 1) * curBitmap.Width + j] == 0 && 
                                        grayValues[(i + 1) * curBitmap.Width + j] == 0 && 
                                        grayValues[i * curBitmap.Width + j + 1] == 0 && 
                                        grayValues[i * curBitmap.Width + j - 1] == 0 && 
                                        grayValues[(i - 1) * curBitmap.Width + j - 1] == 0 && 
                                        grayValues[(i + 1) * curBitmap.Width + j - 1] == 0 && 
                                        grayValues[(i - 1) * curBitmap.Width + j + 1] == 0 && 
                                        grayValues[(i + 1) * curBitmap.Width + j + 1] == 0)
                                    {
                                        tempArray[i * curBitmap.Width + j] = 0;
                                    }

                                }
                            }
                            break;
                        case 0x28:
                            for (int i = 2; i < curBitmap.Height - 2; i++)
                            {
                                for (int j = 2; j < curBitmap.Width - 2; j++)
                                {
                                    if (grayValues[(i - 2) * curBitmap.Width + j - 2] == 0 &&
                                        grayValues[(i - 2) * curBitmap.Width + j - 1] == 0 &&
                                        grayValues[(i - 2) * curBitmap.Width + j] == 0 &&
                                        grayValues[(i - 2) * curBitmap.Width + j + 1] == 0 &&
                                        grayValues[(i - 2) * curBitmap.Width + j + 2] == 0 &&
                                        grayValues[(i - 1) * curBitmap.Width + j - 2] == 0 &&
                                        grayValues[(i - 1) * curBitmap.Width + j - 1] == 0 &&
                                        grayValues[(i - 1) * curBitmap.Width + j] == 0 &&
                                        grayValues[(i - 1) * curBitmap.Width + j + 1] == 0 &&
                                        grayValues[(i - 1) * curBitmap.Width + j + 2] == 0 &&
                                        grayValues[i * curBitmap.Width + j - 2] == 0 &&
                                        grayValues[i * curBitmap.Width + j - 1] == 0 &&
                                        grayValues[i * curBitmap.Width + j] == 0 &&
                                        grayValues[i * curBitmap.Width + j + 1] == 0 &&
                                        grayValues[i * curBitmap.Width + j + 2] == 0 &&
                                        grayValues[(i + 2) * curBitmap.Width + j - 2] == 0 &&
                                        grayValues[(i + 2) * curBitmap.Width + j - 1] == 0 &&
                                        grayValues[(i + 2) * curBitmap.Width + j] == 0 &&
                                        grayValues[(i + 2) * curBitmap.Width + j + 1] == 0 &&
                                        grayValues[(i + 2) * curBitmap.Width + j + 2] == 0 &&
                                        grayValues[(i + 1) * curBitmap.Width + j - 2] == 0 &&
                                        grayValues[(i + 1) * curBitmap.Width + j - 1] == 0 &&
                                        grayValues[(i + 1) * curBitmap.Width + j] == 0 &&
                                        grayValues[(i + 1) * curBitmap.Width + j + 1] == 0 &&
                                        grayValues[(i + 1) * curBitmap.Width + j + 2] == 0)
                                    {
                                        tempArray[i * curBitmap.Width + j] = 0;
                                    }
                                    
                                }
                            }
                            break;
                        default:
                            MessageBox.Show("错误的结构元素！");
                            break;
                    }


                    grayValues = (byte[])tempArray.Clone();

                    System.Runtime.InteropServices.Marshal.Copy(grayValues, 0, ptr, bytes);
                    curBitmap.UnlockBits(bmpData);
                }

                Invalidate();
            }
        }

        private void dilate_Click(object sender, EventArgs e)
        {
            if (curBitmap != null)
            {
                struction struForm = new struction();
                struForm.Text = "膨胀运算结构元素";
                if (struForm.ShowDialog() == DialogResult.OK)
                {
                    Rectangle rect = new Rectangle(0, 0, curBitmap.Width, curBitmap.Height);
                    System.Drawing.Imaging.BitmapData bmpData = curBitmap.LockBits(rect, System.Drawing.Imaging.ImageLockMode.ReadWrite, curBitmap.PixelFormat);
                    IntPtr ptr = bmpData.Scan0;
                    int bytes = curBitmap.Width * curBitmap.Height;
                    byte[] grayValues = new byte[bytes];
                    System.Runtime.InteropServices.Marshal.Copy(ptr, grayValues, 0, bytes);

                    byte flagStru = struForm.GetStruction;

                    byte[] tempArray = new byte[bytes];
                    for (int i = 0; i < bytes; i++)
                    {
                        tempArray[i] = 255;
                    }

                    switch (flagStru)
                    {
                        case 0x11:
                            for (int i = 0; i < curBitmap.Height; i++)
                            {
                                for (int j = 1; j < curBitmap.Width - 1; j++)
                                {
                                    if (grayValues[i * curBitmap.Width + j] == 0 ||
                                        grayValues[i * curBitmap.Width + j + 1] == 0 ||
                                        grayValues[i * curBitmap.Width + j - 1] == 0)
                                    {
                                        tempArray[i * curBitmap.Width + j] = 0;
                                    }

                                }
                            }
                            break;
                        case 0x21:
                            for (int i = 0; i < curBitmap.Height; i++)
                            {
                                for (int j = 2; j < curBitmap.Width - 2; j++)
                                {
                                    if (grayValues[i * curBitmap.Width + j] == 0 ||
                                        grayValues[i * curBitmap.Width + j + 1] == 0 ||
                                        grayValues[i * curBitmap.Width + j - 1] == 0 ||
                                        grayValues[i * curBitmap.Width + j + 2] == 0 ||
                                        grayValues[i * curBitmap.Width + j - 2] == 0)
                                    {
                                        tempArray[i * curBitmap.Width + j] = 0;
                                    }

                                }
                            }
                            break;
                        case 0x12:
                            for (int i = 1; i < curBitmap.Height - 1; i++)
                            {
                                for (int j = 0; j < curBitmap.Width; j++)
                                {
                                    if (grayValues[i * curBitmap.Width + j] == 0 ||
                                        grayValues[(i - 1) * curBitmap.Width + j] == 0 ||
                                        grayValues[(i + 1) * curBitmap.Width + j] == 0)
                                    {
                                        tempArray[i * curBitmap.Width + j] = 0;
                                    }

                                }
                            }
                            break;
                        case 0x22:
                            for (int i = 2; i < curBitmap.Height - 2; i++)
                            {
                                for (int j = 0; j < curBitmap.Width; j++)
                                {
                                    if (grayValues[i * curBitmap.Width + j] == 0 ||
                                        grayValues[(i - 1) * curBitmap.Width + j] == 0 ||
                                        grayValues[(i + 1) * curBitmap.Width + j] == 0 ||
                                        grayValues[(i - 2) * curBitmap.Width + j] == 0 ||
                                        grayValues[(i + 2) * curBitmap.Width + j] == 0)
                                    {
                                        tempArray[i * curBitmap.Width + j] = 0;
                                    }

                                }
                            }
                            break;
                        case 0x14:
                            for (int i = 1; i < curBitmap.Height - 1; i++)
                            {
                                for (int j = 1; j < curBitmap.Width - 1; j++)
                                {
                                    if (grayValues[i * curBitmap.Width + j] == 0 ||
                                        grayValues[(i - 1) * curBitmap.Width + j] == 0 ||
                                        grayValues[(i + 1) * curBitmap.Width + j] == 0 ||
                                        grayValues[i * curBitmap.Width + j + 1] == 0 ||
                                        grayValues[i * curBitmap.Width + j - 1] == 0)
                                    {
                                        tempArray[i * curBitmap.Width + j] = 0;
                                    }

                                }
                            }
                            break;
                        case 0x24:
                            for (int i = 2; i < curBitmap.Height - 2; i++)
                            {
                                for (int j = 2; j < curBitmap.Width - 2; j++)
                                {
                                    if (grayValues[i * curBitmap.Width + j] == 0 ||
                                        grayValues[(i - 1) * curBitmap.Width + j] == 0 ||
                                        grayValues[(i + 1) * curBitmap.Width + j] == 0 ||
                                        grayValues[(i - 2) * curBitmap.Width + j] == 0 ||
                                        grayValues[(i + 2) * curBitmap.Width + j] == 0 ||
                                        grayValues[i * curBitmap.Width + j + 1] == 0 ||
                                        grayValues[i * curBitmap.Width + j - 1] == 0 ||
                                        grayValues[i * curBitmap.Width + j + 2] == 0 ||
                                        grayValues[i * curBitmap.Width + j - 2] == 0)
                                    {
                                        tempArray[i * curBitmap.Width + j] = 0;
                                    }

                                }
                            }
                            break;
                        case 0x18:
                            for (int i = 1; i < curBitmap.Height - 1; i++)
                            {
                                for (int j = 1; j < curBitmap.Width - 1; j++)
                                {
                                    if (grayValues[i * curBitmap.Width + j] == 0 ||
                                        grayValues[(i - 1) * curBitmap.Width + j] == 0 ||
                                        grayValues[(i + 1) * curBitmap.Width + j] == 0 ||
                                        grayValues[i * curBitmap.Width + j + 1] == 0 ||
                                        grayValues[i * curBitmap.Width + j - 1] == 0 ||
                                        grayValues[(i - 1) * curBitmap.Width + j - 1] == 0 ||
                                        grayValues[(i + 1) * curBitmap.Width + j - 1] == 0 ||
                                        grayValues[(i - 1) * curBitmap.Width + j + 1] == 0 ||
                                        grayValues[(i + 1) * curBitmap.Width + j + 1] == 0)
                                    {
                                        tempArray[i * curBitmap.Width + j] = 0;
                                    }

                                }
                            }
                            break;
                        case 0x28:
                            for (int i = 2; i < curBitmap.Height - 2; i++)
                            {
                                for (int j = 2; j < curBitmap.Width - 2; j++)
                                {
                                    if (grayValues[(i - 2) * curBitmap.Width + j - 2] == 0 ||
                                        grayValues[(i - 2) * curBitmap.Width + j - 1] == 0 ||
                                        grayValues[(i - 2) * curBitmap.Width + j] == 0 ||
                                        grayValues[(i - 2) * curBitmap.Width + j + 1] == 0 ||
                                        grayValues[(i - 2) * curBitmap.Width + j + 2] == 0 ||
                                        grayValues[(i - 1) * curBitmap.Width + j - 2] == 0 ||
                                        grayValues[(i - 1) * curBitmap.Width + j - 1] == 0 ||
                                        grayValues[(i - 1) * curBitmap.Width + j] == 0 ||
                                        grayValues[(i - 1) * curBitmap.Width + j + 1] == 0 ||
                                        grayValues[(i - 1) * curBitmap.Width + j + 2] == 0 ||
                                        grayValues[i * curBitmap.Width + j - 2] == 0 ||
                                        grayValues[i * curBitmap.Width + j - 1] == 0 ||
                                        grayValues[i * curBitmap.Width + j] == 0 ||
                                        grayValues[i * curBitmap.Width + j + 1] == 0 ||
                                        grayValues[i * curBitmap.Width + j + 2] == 0 ||
                                        grayValues[(i + 2) * curBitmap.Width + j - 2] == 0 ||
                                        grayValues[(i + 2) * curBitmap.Width + j - 1] == 0 ||
                                        grayValues[(i + 2) * curBitmap.Width + j] == 0 ||
                                        grayValues[(i + 2) * curBitmap.Width + j + 1] == 0 ||
                                        grayValues[(i + 2) * curBitmap.Width + j + 2] == 0 ||
                                        grayValues[(i + 1) * curBitmap.Width + j - 2] == 0 ||
                                        grayValues[(i + 1) * curBitmap.Width + j - 1] == 0 ||
                                        grayValues[(i + 1) * curBitmap.Width + j] == 0 ||
                                        grayValues[(i + 1) * curBitmap.Width + j + 1] == 0 ||
                                        grayValues[(i + 1) * curBitmap.Width + j + 2] == 0)
                                    {
                                        tempArray[i * curBitmap.Width + j] = 0;
                                    }

                                }
                            }
                            break;
                        default:
                            MessageBox.Show("错误的结构元素！");
                            break;
                    }


                    grayValues = (byte[])tempArray.Clone();

                    System.Runtime.InteropServices.Marshal.Copy(grayValues, 0, ptr, bytes);
                    curBitmap.UnlockBits(bmpData);
                }

                Invalidate();
            }
        }

        private void opening_Click(object sender, EventArgs e)
        {
            if (curBitmap != null)
            {
                struction struForm = new struction();
                struForm.Text = "开运算结构元素";
                if (struForm.ShowDialog() == DialogResult.OK)
                {
                    Rectangle rect = new Rectangle(0, 0, curBitmap.Width, curBitmap.Height);
                    System.Drawing.Imaging.BitmapData bmpData = curBitmap.LockBits(rect, System.Drawing.Imaging.ImageLockMode.ReadWrite, curBitmap.PixelFormat);
                    IntPtr ptr = bmpData.Scan0;
                    int bytes = curBitmap.Width * curBitmap.Height;
                    byte[] grayValues = new byte[bytes];
                    System.Runtime.InteropServices.Marshal.Copy(ptr, grayValues, 0, bytes);

                    byte flagStru = struForm.GetStruction;

                    byte[] temp1Array = new byte[bytes];
                    byte[] tempArray = new byte[bytes];
                    for (int i = 0; i < bytes; i++)
                    {
                        tempArray[i] = temp1Array[i] = 255;
                    }

                    switch (flagStru)
                    {
                        case 0x11:
                            //腐蚀运算
                            for (int i = 0; i < curBitmap.Height; i++)
                            {
                                for (int j = 1; j < curBitmap.Width - 1; j++)
                                {
                                    if (grayValues[i * curBitmap.Width + j] == 0 &&
                                        grayValues[i * curBitmap.Width + j + 1] == 0 &&
                                        grayValues[i * curBitmap.Width + j - 1] == 0)
                                    {
                                        temp1Array[i * curBitmap.Width + j] = 0;
                                    }

                                }
                            }
                            //膨胀运算
                            for (int i = 0; i < curBitmap.Height; i++)
                            {
                                for (int j = 1; j < curBitmap.Width - 1; j++)
                                {
                                    if (temp1Array[i * curBitmap.Width + j] == 0 ||
                                        temp1Array[i * curBitmap.Width + j + 1] == 0 ||
                                        temp1Array[i * curBitmap.Width + j - 1] == 0)
                                    {
                                        tempArray[i * curBitmap.Width + j] = 0;
                                    }

                                }
                            }
                            break;
                        case 0x21:
                            //腐蚀运算
                            for (int i = 0; i < curBitmap.Height; i++)
                            {
                                for (int j = 2; j < curBitmap.Width - 2; j++)
                                {
                                    if (grayValues[i * curBitmap.Width + j] == 0 &&
                                        grayValues[i * curBitmap.Width + j + 1] == 0 &&
                                        grayValues[i * curBitmap.Width + j - 1] == 0 &&
                                        grayValues[i * curBitmap.Width + j + 2] == 0 &&
                                        grayValues[i * curBitmap.Width + j - 2] == 0)
                                    {
                                        temp1Array[i * curBitmap.Width + j] = 0;
                                    }

                                }
                            }
                            //膨胀运算
                            for (int i = 0; i < curBitmap.Height; i++)
                            {
                                for (int j = 2; j < curBitmap.Width - 2; j++)
                                {
                                    if (temp1Array[i * curBitmap.Width + j] == 0 ||
                                        temp1Array[i * curBitmap.Width + j + 1] == 0 ||
                                        temp1Array[i * curBitmap.Width + j - 1] == 0 ||
                                        temp1Array[i * curBitmap.Width + j + 2] == 0 ||
                                        temp1Array[i * curBitmap.Width + j - 2] == 0)
                                    {
                                        tempArray[i * curBitmap.Width + j] = 0;
                                    }

                                }
                            }
                            break;
                        case 0x12:
                            //腐蚀运算
                            for (int i = 1; i < curBitmap.Height - 1; i++)
                            {
                                for (int j = 0; j < curBitmap.Width; j++)
                                {
                                    if (grayValues[i * curBitmap.Width + j] == 0 &&
                                        grayValues[(i - 1) * curBitmap.Width + j] == 0 &&
                                        grayValues[(i + 1) * curBitmap.Width + j] == 0)
                                    {
                                        temp1Array[i * curBitmap.Width + j] = 0;
                                    }

                                }
                            }
                            //膨胀运算
                            for (int i = 1; i < curBitmap.Height - 1; i++)
                            {
                                for (int j = 0; j < curBitmap.Width; j++)
                                {
                                    if (temp1Array[i * curBitmap.Width + j] == 0 ||
                                        temp1Array[(i - 1) * curBitmap.Width + j] == 0 ||
                                        temp1Array[(i + 1) * curBitmap.Width + j] == 0)
                                    {
                                        tempArray[i * curBitmap.Width + j] = 0;
                                    }

                                }
                            }
                            break;
                        case 0x22:
                            //腐蚀运算
                            for (int i = 2; i < curBitmap.Height - 2; i++)
                            {
                                for (int j = 0; j < curBitmap.Width; j++)
                                {
                                    if (grayValues[i * curBitmap.Width + j] == 0 &&
                                        grayValues[(i - 1) * curBitmap.Width + j] == 0 &&
                                        grayValues[(i + 1) * curBitmap.Width + j] == 0 &&
                                        grayValues[(i - 2) * curBitmap.Width + j] == 0 &&
                                        grayValues[(i + 2) * curBitmap.Width + j] == 0)
                                    {
                                        temp1Array[i * curBitmap.Width + j] = 0;
                                    }

                                }
                            }
                            //膨胀运算
                            for (int i = 2; i < curBitmap.Height - 2; i++)
                            {
                                for (int j = 0; j < curBitmap.Width; j++)
                                {
                                    if (temp1Array[i * curBitmap.Width + j] == 0 ||
                                        temp1Array[(i - 1) * curBitmap.Width + j] == 0 ||
                                        temp1Array[(i + 1) * curBitmap.Width + j] == 0 ||
                                        temp1Array[(i - 2) * curBitmap.Width + j] == 0 ||
                                        temp1Array[(i + 2) * curBitmap.Width + j] == 0)
                                    {
                                        tempArray[i * curBitmap.Width + j] = 0;
                                    }

                                }
                            }
                            break;
                        case 0x14:
                            //腐蚀运算
                            for (int i = 1; i < curBitmap.Height - 1; i++)
                            {
                                for (int j = 1; j < curBitmap.Width - 1; j++)
                                {
                                    if (grayValues[i * curBitmap.Width + j] == 0 &&
                                        grayValues[(i - 1) * curBitmap.Width + j] == 0 &&
                                        grayValues[(i + 1) * curBitmap.Width + j] == 0 &&
                                        grayValues[i * curBitmap.Width + j + 1] == 0 &&
                                        grayValues[i * curBitmap.Width + j - 1] == 0)
                                    {
                                        temp1Array[i * curBitmap.Width + j] = 0;
                                    }

                                }
                            }
                            //膨胀运算
                            for (int i = 1; i < curBitmap.Height - 1; i++)
                            {
                                for (int j = 1; j < curBitmap.Width - 1; j++)
                                {
                                    if (temp1Array[i * curBitmap.Width + j] == 0 ||
                                        temp1Array[(i - 1) * curBitmap.Width + j] == 0 ||
                                        temp1Array[(i + 1) * curBitmap.Width + j] == 0 ||
                                        temp1Array[i * curBitmap.Width + j + 1] == 0 ||
                                        temp1Array[i * curBitmap.Width + j - 1] == 0)
                                    {
                                        tempArray[i * curBitmap.Width + j] = 0;
                                    }

                                }
                            }
                            break;
                        case 0x24:
                            //腐蚀运算
                            for (int i = 2; i < curBitmap.Height - 2; i++)
                            {
                                for (int j = 2; j < curBitmap.Width - 2; j++)
                                {
                                    if (grayValues[i * curBitmap.Width + j] == 0 &&
                                        grayValues[(i - 1) * curBitmap.Width + j] == 0 &&
                                        grayValues[(i + 1) * curBitmap.Width + j] == 0 &&
                                        grayValues[(i - 2) * curBitmap.Width + j] == 0 &&
                                        grayValues[(i + 2) * curBitmap.Width + j] == 0 &&
                                        grayValues[i * curBitmap.Width + j + 1] == 0 &&
                                        grayValues[i * curBitmap.Width + j - 1] == 0 &&
                                        grayValues[i * curBitmap.Width + j + 2] == 0 &&
                                        grayValues[i * curBitmap.Width + j - 2] == 0)
                                    {
                                        temp1Array[i * curBitmap.Width + j] = 0;
                                    }

                                }
                            }
                            //膨胀运算
                            for (int i = 2; i < curBitmap.Height - 2; i++)
                            {
                                for (int j = 2; j < curBitmap.Width - 2; j++)
                                {
                                    if (temp1Array[i * curBitmap.Width + j] == 0 ||
                                        temp1Array[(i - 1) * curBitmap.Width + j] == 0 ||
                                        temp1Array[(i + 1) * curBitmap.Width + j] == 0 ||
                                        temp1Array[(i - 2) * curBitmap.Width + j] == 0 ||
                                        temp1Array[(i + 2) * curBitmap.Width + j] == 0 ||
                                        temp1Array[i * curBitmap.Width + j + 1] == 0 ||
                                        temp1Array[i * curBitmap.Width + j - 1] == 0 ||
                                        temp1Array[i * curBitmap.Width + j + 2] == 0 ||
                                        temp1Array[i * curBitmap.Width + j - 2] == 0)
                                    {
                                        tempArray[i * curBitmap.Width + j] = 0;
                                    }

                                }
                            }
                            break;
                        case 0x18:
                            //腐蚀运算
                            for (int i = 1; i < curBitmap.Height - 1; i++)
                            {
                                for (int j = 1; j < curBitmap.Width - 1; j++)
                                {
                                    if (grayValues[i * curBitmap.Width + j] == 0 &&
                                        grayValues[(i - 1) * curBitmap.Width + j] == 0 &&
                                        grayValues[(i + 1) * curBitmap.Width + j] == 0 &&
                                        grayValues[i * curBitmap.Width + j + 1] == 0 &&
                                        grayValues[i * curBitmap.Width + j - 1] == 0 &&
                                        grayValues[(i - 1) * curBitmap.Width + j - 1] == 0 &&
                                        grayValues[(i + 1) * curBitmap.Width + j - 1] == 0 &&
                                        grayValues[(i - 1) * curBitmap.Width + j + 1] == 0 &&
                                        grayValues[(i + 1) * curBitmap.Width + j + 1] == 0)
                                    {
                                        temp1Array[i * curBitmap.Width + j] = 0;
                                    }

                                }
                            }
                            //膨胀运算
                            for (int i = 1; i < curBitmap.Height - 1; i++)
                            {
                                for (int j = 1; j < curBitmap.Width - 1; j++)
                                {
                                    if (temp1Array[i * curBitmap.Width + j] == 0 ||
                                        temp1Array[(i - 1) * curBitmap.Width + j] == 0 ||
                                        temp1Array[(i + 1) * curBitmap.Width + j] == 0 ||
                                        temp1Array[i * curBitmap.Width + j + 1] == 0 ||
                                        temp1Array[i * curBitmap.Width + j - 1] == 0 ||
                                        temp1Array[(i - 1) * curBitmap.Width + j - 1] == 0 ||
                                        temp1Array[(i + 1) * curBitmap.Width + j - 1] == 0 ||
                                        temp1Array[(i - 1) * curBitmap.Width + j + 1] == 0 ||
                                        temp1Array[(i + 1) * curBitmap.Width + j + 1] == 0)
                                    {
                                        tempArray[i * curBitmap.Width + j] = 0;
                                    }

                                }
                            }
                            break;
                        case 0x28:
                            //腐蚀运算
                            for (int i = 2; i < curBitmap.Height - 2; i++)
                            {
                                for (int j = 2; j < curBitmap.Width - 2; j++)
                                {
                                    if (grayValues[(i - 2) * curBitmap.Width + j - 2] == 0 &&
                                        grayValues[(i - 2) * curBitmap.Width + j - 1] == 0 &&
                                        grayValues[(i - 2) * curBitmap.Width + j] == 0 &&
                                        grayValues[(i - 2) * curBitmap.Width + j + 1] == 0 &&
                                        grayValues[(i - 2) * curBitmap.Width + j + 2] == 0 &&
                                        grayValues[(i - 1) * curBitmap.Width + j - 2] == 0 &&
                                        grayValues[(i - 1) * curBitmap.Width + j - 1] == 0 &&
                                        grayValues[(i - 1) * curBitmap.Width + j] == 0 &&
                                        grayValues[(i - 1) * curBitmap.Width + j + 1] == 0 &&
                                        grayValues[(i - 1) * curBitmap.Width + j + 2] == 0 &&
                                        grayValues[i * curBitmap.Width + j - 2] == 0 &&
                                        grayValues[i * curBitmap.Width + j - 1] == 0 &&
                                        grayValues[i * curBitmap.Width + j] == 0 &&
                                        grayValues[i * curBitmap.Width + j + 1] == 0 &&
                                        grayValues[i * curBitmap.Width + j + 2] == 0 &&
                                        grayValues[(i + 2) * curBitmap.Width + j - 2] == 0 &&
                                        grayValues[(i + 2) * curBitmap.Width + j - 1] == 0 &&
                                        grayValues[(i + 2) * curBitmap.Width + j] == 0 &&
                                        grayValues[(i + 2) * curBitmap.Width + j + 1] == 0 &&
                                        grayValues[(i + 2) * curBitmap.Width + j + 2] == 0 &&
                                        grayValues[(i + 1) * curBitmap.Width + j - 2] == 0 &&
                                        grayValues[(i + 1) * curBitmap.Width + j - 1] == 0 &&
                                        grayValues[(i + 1) * curBitmap.Width + j] == 0 &&
                                        grayValues[(i + 1) * curBitmap.Width + j + 1] == 0 &&
                                        grayValues[(i + 1) * curBitmap.Width + j + 2] == 0)
                                    {
                                        temp1Array[i * curBitmap.Width + j] = 0;
                                    }

                                }
                            }
                            //膨胀运算
                            for (int i = 2; i < curBitmap.Height - 2; i++)
                            {
                                for (int j = 2; j < curBitmap.Width - 2; j++)
                                {
                                    if (temp1Array[(i - 2) * curBitmap.Width + j - 2] == 0 ||
                                        temp1Array[(i - 2) * curBitmap.Width + j - 1] == 0 ||
                                        temp1Array[(i - 2) * curBitmap.Width + j] == 0 ||
                                        temp1Array[(i - 2) * curBitmap.Width + j + 1] == 0 ||
                                        temp1Array[(i - 2) * curBitmap.Width + j + 2] == 0 ||
                                        temp1Array[(i - 1) * curBitmap.Width + j - 2] == 0 ||
                                        temp1Array[(i - 1) * curBitmap.Width + j - 1] == 0 ||
                                        temp1Array[(i - 1) * curBitmap.Width + j] == 0 ||
                                        temp1Array[(i - 1) * curBitmap.Width + j + 1] == 0 ||
                                        temp1Array[(i - 1) * curBitmap.Width + j + 2] == 0 ||
                                        temp1Array[i * curBitmap.Width + j - 2] == 0 ||
                                        temp1Array[i * curBitmap.Width + j - 1] == 0 ||
                                        temp1Array[i * curBitmap.Width + j] == 0 ||
                                        temp1Array[i * curBitmap.Width + j + 1] == 0 ||
                                        temp1Array[i * curBitmap.Width + j + 2] == 0 ||
                                        temp1Array[(i + 2) * curBitmap.Width + j - 2] == 0 ||
                                        temp1Array[(i + 2) * curBitmap.Width + j - 1] == 0 ||
                                        temp1Array[(i + 2) * curBitmap.Width + j] == 0 ||
                                        temp1Array[(i + 2) * curBitmap.Width + j + 1] == 0 ||
                                        temp1Array[(i + 2) * curBitmap.Width + j + 2] == 0 ||
                                        temp1Array[(i + 1) * curBitmap.Width + j - 2] == 0 ||
                                        temp1Array[(i + 1) * curBitmap.Width + j - 1] == 0 ||
                                        temp1Array[(i + 1) * curBitmap.Width + j] == 0 ||
                                        temp1Array[(i + 1) * curBitmap.Width + j + 1] == 0 ||
                                        temp1Array[(i + 1) * curBitmap.Width + j + 2] == 0)
                                    {
                                        tempArray[i * curBitmap.Width + j] = 0;
                                    }

                                }
                            }
                            break;
                        default:
                            MessageBox.Show("错误的结构元素！");
                            break;
                    }


                    grayValues = (byte[])tempArray.Clone();

                    System.Runtime.InteropServices.Marshal.Copy(grayValues, 0, ptr, bytes);
                    curBitmap.UnlockBits(bmpData);
                }

                Invalidate();
            }
        }

        private void closing_Click(object sender, EventArgs e)
        {
            if (curBitmap != null)
            {
                struction struForm = new struction();
                struForm.Text = "闭运算结构元素";
                if (struForm.ShowDialog() == DialogResult.OK)
                {
                    Rectangle rect = new Rectangle(0, 0, curBitmap.Width, curBitmap.Height);
                    System.Drawing.Imaging.BitmapData bmpData = curBitmap.LockBits(rect, System.Drawing.Imaging.ImageLockMode.ReadWrite, curBitmap.PixelFormat);
                    IntPtr ptr = bmpData.Scan0;
                    int bytes = curBitmap.Width * curBitmap.Height;
                    byte[] grayValues = new byte[bytes];
                    System.Runtime.InteropServices.Marshal.Copy(ptr, grayValues, 0, bytes);

                    byte flagStru = struForm.GetStruction;

                    byte[] temp1Array = new byte[bytes];
                    byte[] tempArray = new byte[bytes];
                    for (int i = 0; i < bytes; i++)
                    {
                        tempArray[i] = temp1Array[i] = 255;
                    }

                    switch (flagStru)
                    {
                        case 0x11:
                            //膨胀运算
                            for (int i = 0; i < curBitmap.Height; i++)
                            {
                                for (int j = 1; j < curBitmap.Width - 1; j++)
                                {
                                    if (grayValues[i * curBitmap.Width + j] == 0 ||
                                        grayValues[i * curBitmap.Width + j + 1] == 0 ||
                                        grayValues[i * curBitmap.Width + j - 1] == 0)
                                    {
                                        temp1Array[i * curBitmap.Width + j] = 0;
                                    }

                                }
                            }
                            //腐蚀运算
                            for (int i = 0; i < curBitmap.Height; i++)
                            {
                                for (int j = 1; j < curBitmap.Width - 1; j++)
                                {
                                    if (temp1Array[i * curBitmap.Width + j] == 0 &&
                                        temp1Array[i * curBitmap.Width + j + 1] == 0 &&
                                        temp1Array[i * curBitmap.Width + j - 1] == 0)
                                    {
                                        tempArray[i * curBitmap.Width + j] = 0;
                                    }

                                }
                            }
                            break;
                        case 0x21:
                            //膨胀运算
                            for (int i = 0; i < curBitmap.Height; i++)
                            {
                                for (int j = 2; j < curBitmap.Width - 2; j++)
                                {
                                    if (grayValues[i * curBitmap.Width + j] == 0 ||
                                        grayValues[i * curBitmap.Width + j + 1] == 0 ||
                                        grayValues[i * curBitmap.Width + j - 1] == 0 ||
                                        grayValues[i * curBitmap.Width + j + 2] == 0 ||
                                        grayValues[i * curBitmap.Width + j - 2] == 0)
                                    {
                                        temp1Array[i * curBitmap.Width + j] = 0;
                                    }

                                }
                            }
                            //腐蚀运算
                            for (int i = 0; i < curBitmap.Height; i++)
                            {
                                for (int j = 2; j < curBitmap.Width - 2; j++)
                                {
                                    if (temp1Array[i * curBitmap.Width + j] == 0 &&
                                        temp1Array[i * curBitmap.Width + j + 1] == 0 &&
                                        temp1Array[i * curBitmap.Width + j - 1] == 0 &&
                                        temp1Array[i * curBitmap.Width + j + 2] == 0 &&
                                        temp1Array[i * curBitmap.Width + j - 2] == 0)
                                    {
                                        tempArray[i * curBitmap.Width + j] = 0;
                                    }

                                }
                            }
                            break;
                        case 0x12:
                            //膨胀运算
                            for (int i = 1; i < curBitmap.Height - 1; i++)
                            {
                                for (int j = 0; j < curBitmap.Width; j++)
                                {
                                    if (grayValues[i * curBitmap.Width + j] == 0 ||
                                        grayValues[(i - 1) * curBitmap.Width + j] == 0 ||
                                        grayValues[(i + 1) * curBitmap.Width + j] == 0)
                                    {
                                        temp1Array[i * curBitmap.Width + j] = 0;
                                    }

                                }
                            }
                            //腐蚀运算
                            for (int i = 1; i < curBitmap.Height - 1; i++)
                            {
                                for (int j = 0; j < curBitmap.Width; j++)
                                {
                                    if (temp1Array[i * curBitmap.Width + j] == 0 &&
                                        temp1Array[(i - 1) * curBitmap.Width + j] == 0 &&
                                        temp1Array[(i + 1) * curBitmap.Width + j] == 0)
                                    {
                                        tempArray[i * curBitmap.Width + j] = 0;
                                    }

                                }
                            }
                            break;
                        case 0x22:
                            //膨胀运算
                            for (int i = 2; i < curBitmap.Height - 2; i++)
                            {
                                for (int j = 0; j < curBitmap.Width; j++)
                                {
                                    if (grayValues[i * curBitmap.Width + j] == 0 ||
                                        grayValues[(i - 1) * curBitmap.Width + j] == 0 ||
                                        grayValues[(i + 1) * curBitmap.Width + j] == 0 ||
                                        grayValues[(i - 2) * curBitmap.Width + j] == 0 ||
                                        grayValues[(i + 2) * curBitmap.Width + j] == 0)
                                    {
                                        temp1Array[i * curBitmap.Width + j] = 0;
                                    }

                                }
                            }
                            //腐蚀运算
                            for (int i = 2; i < curBitmap.Height - 2; i++)
                            {
                                for (int j = 0; j < curBitmap.Width; j++)
                                {
                                    if (temp1Array[i * curBitmap.Width + j] == 0 &&
                                        temp1Array[(i - 1) * curBitmap.Width + j] == 0 &&
                                        temp1Array[(i + 1) * curBitmap.Width + j] == 0 &&
                                        temp1Array[(i - 2) * curBitmap.Width + j] == 0 &&
                                        temp1Array[(i + 2) * curBitmap.Width + j] == 0)
                                    {
                                        tempArray[i * curBitmap.Width + j] = 0;
                                    }

                                }
                            }
                            break;
                        case 0x14:
                            //膨胀运算
                            for (int i = 1; i < curBitmap.Height - 1; i++)
                            {
                                for (int j = 1; j < curBitmap.Width - 1; j++)
                                {
                                    if (grayValues[i * curBitmap.Width + j] == 0 ||
                                        grayValues[(i - 1) * curBitmap.Width + j] == 0 ||
                                        grayValues[(i + 1) * curBitmap.Width + j] == 0 ||
                                        grayValues[i * curBitmap.Width + j + 1] == 0 ||
                                        grayValues[i * curBitmap.Width + j - 1] == 0)
                                    {
                                        temp1Array[i * curBitmap.Width + j] = 0;
                                    }

                                }
                            }
                            //腐蚀运算
                            for (int i = 1; i < curBitmap.Height - 1; i++)
                            {
                                for (int j = 1; j < curBitmap.Width - 1; j++)
                                {
                                    if (temp1Array[i * curBitmap.Width + j] == 0 &&
                                        temp1Array[(i - 1) * curBitmap.Width + j] == 0 &&
                                        temp1Array[(i + 1) * curBitmap.Width + j] == 0 &&
                                        temp1Array[i * curBitmap.Width + j + 1] == 0 &&
                                        temp1Array[i * curBitmap.Width + j - 1] == 0)
                                    {
                                        tempArray[i * curBitmap.Width + j] = 0;
                                    }

                                }
                            }
                            break;
                        case 0x24:
                            //膨胀运算
                            for (int i = 2; i < curBitmap.Height - 2; i++)
                            {
                                for (int j = 2; j < curBitmap.Width - 2; j++)
                                {
                                    if (grayValues[i * curBitmap.Width + j] == 0 ||
                                        grayValues[(i - 1) * curBitmap.Width + j] == 0 ||
                                        grayValues[(i + 1) * curBitmap.Width + j] == 0 ||
                                        grayValues[(i - 2) * curBitmap.Width + j] == 0 ||
                                        grayValues[(i + 2) * curBitmap.Width + j] == 0 ||
                                        grayValues[i * curBitmap.Width + j + 1] == 0 ||
                                        grayValues[i * curBitmap.Width + j - 1] == 0 ||
                                        grayValues[i * curBitmap.Width + j + 2] == 0 ||
                                        grayValues[i * curBitmap.Width + j - 2] == 0)
                                    {
                                        temp1Array[i * curBitmap.Width + j] = 0;
                                    }

                                }
                            }
                            //腐蚀运算
                            for (int i = 2; i < curBitmap.Height - 2; i++)
                            {
                                for (int j = 2; j < curBitmap.Width - 2; j++)
                                {
                                    if (temp1Array[i * curBitmap.Width + j] == 0 &&
                                        temp1Array[(i - 1) * curBitmap.Width + j] == 0 &&
                                        temp1Array[(i + 1) * curBitmap.Width + j] == 0 &&
                                        temp1Array[(i - 2) * curBitmap.Width + j] == 0 &&
                                        temp1Array[(i + 2) * curBitmap.Width + j] == 0 &&
                                        temp1Array[i * curBitmap.Width + j + 1] == 0 &&
                                        temp1Array[i * curBitmap.Width + j - 1] == 0 &&
                                        temp1Array[i * curBitmap.Width + j + 2] == 0 &&
                                        temp1Array[i * curBitmap.Width + j - 2] == 0)
                                    {
                                        tempArray[i * curBitmap.Width + j] = 0;
                                    }

                                }
                            }
                            break;
                        case 0x18:
                            //膨胀运算
                            for (int i = 1; i < curBitmap.Height - 1; i++)
                            {
                                for (int j = 1; j < curBitmap.Width - 1; j++)
                                {
                                    if (grayValues[i * curBitmap.Width + j] == 0 ||
                                        grayValues[(i - 1) * curBitmap.Width + j] == 0 ||
                                        grayValues[(i + 1) * curBitmap.Width + j] == 0 ||
                                        grayValues[i * curBitmap.Width + j + 1] == 0 ||
                                        grayValues[i * curBitmap.Width + j - 1] == 0 ||
                                        grayValues[(i - 1) * curBitmap.Width + j - 1] == 0 ||
                                        grayValues[(i + 1) * curBitmap.Width + j - 1] == 0 ||
                                        grayValues[(i - 1) * curBitmap.Width + j + 1] == 0 ||
                                        grayValues[(i + 1) * curBitmap.Width + j + 1] == 0)
                                    {
                                        temp1Array[i * curBitmap.Width + j] = 0;
                                    }

                                }
                            }
                            //腐蚀运算
                            for (int i = 1; i < curBitmap.Height - 1; i++)
                            {
                                for (int j = 1; j < curBitmap.Width - 1; j++)
                                {
                                    if (temp1Array[i * curBitmap.Width + j] == 0 &&
                                        temp1Array[(i - 1) * curBitmap.Width + j] == 0 &&
                                        temp1Array[(i + 1) * curBitmap.Width + j] == 0 &&
                                        temp1Array[i * curBitmap.Width + j + 1] == 0 &&
                                        temp1Array[i * curBitmap.Width + j - 1] == 0 &&
                                        temp1Array[(i - 1) * curBitmap.Width + j - 1] == 0 &&
                                        temp1Array[(i + 1) * curBitmap.Width + j - 1] == 0 &&
                                        temp1Array[(i - 1) * curBitmap.Width + j + 1] == 0 &&
                                        temp1Array[(i + 1) * curBitmap.Width + j + 1] == 0)
                                    {
                                        tempArray[i * curBitmap.Width + j] = 0;
                                    }

                                }
                            }
                            break;
                        case 0x28:
                            //膨胀运算
                            for (int i = 2; i < curBitmap.Height - 2; i++)
                            {
                                for (int j = 2; j < curBitmap.Width - 2; j++)
                                {
                                    if (grayValues[(i - 2) * curBitmap.Width + j - 2] == 0 ||
                                        grayValues[(i - 2) * curBitmap.Width + j - 1] == 0 ||
                                        grayValues[(i - 2) * curBitmap.Width + j] == 0 ||
                                        grayValues[(i - 2) * curBitmap.Width + j + 1] == 0 ||
                                        grayValues[(i - 2) * curBitmap.Width + j + 2] == 0 ||
                                        grayValues[(i - 1) * curBitmap.Width + j - 2] == 0 ||
                                        grayValues[(i - 1) * curBitmap.Width + j - 1] == 0 ||
                                        grayValues[(i - 1) * curBitmap.Width + j] == 0 ||
                                        grayValues[(i - 1) * curBitmap.Width + j + 1] == 0 ||
                                        grayValues[(i - 1) * curBitmap.Width + j + 2] == 0 ||
                                        grayValues[i * curBitmap.Width + j - 2] == 0 ||
                                        grayValues[i * curBitmap.Width + j - 1] == 0 ||
                                        grayValues[i * curBitmap.Width + j] == 0 ||
                                        grayValues[i * curBitmap.Width + j + 1] == 0 ||
                                        grayValues[i * curBitmap.Width + j + 2] == 0 ||
                                        grayValues[(i + 2) * curBitmap.Width + j - 2] == 0 ||
                                        grayValues[(i + 2) * curBitmap.Width + j - 1] == 0 ||
                                        grayValues[(i + 2) * curBitmap.Width + j] == 0 ||
                                        grayValues[(i + 2) * curBitmap.Width + j + 1] == 0 ||
                                        grayValues[(i + 2) * curBitmap.Width + j + 2] == 0 ||
                                        grayValues[(i + 1) * curBitmap.Width + j - 2] == 0 ||
                                        grayValues[(i + 1) * curBitmap.Width + j - 1] == 0 ||
                                        grayValues[(i + 1) * curBitmap.Width + j] == 0 ||
                                        grayValues[(i + 1) * curBitmap.Width + j + 1] == 0 ||
                                        grayValues[(i + 1) * curBitmap.Width + j + 2] == 0)
                                    {
                                        temp1Array[i * curBitmap.Width + j] = 0;
                                    }

                                }
                            }
                            //腐蚀运算
                            for (int i = 2; i < curBitmap.Height - 2; i++)
                            {
                                for (int j = 2; j < curBitmap.Width - 2; j++)
                                {
                                    if (temp1Array[(i - 2) * curBitmap.Width + j - 2] == 0 &&
                                        temp1Array[(i - 2) * curBitmap.Width + j - 1] == 0 &&
                                        temp1Array[(i - 2) * curBitmap.Width + j] == 0 &&
                                        temp1Array[(i - 2) * curBitmap.Width + j + 1] == 0 &&
                                        temp1Array[(i - 2) * curBitmap.Width + j + 2] == 0 &&
                                        temp1Array[(i - 1) * curBitmap.Width + j - 2] == 0 &&
                                        temp1Array[(i - 1) * curBitmap.Width + j - 1] == 0 &&
                                        temp1Array[(i - 1) * curBitmap.Width + j] == 0 &&
                                        temp1Array[(i - 1) * curBitmap.Width + j + 1] == 0 &&
                                        temp1Array[(i - 1) * curBitmap.Width + j + 2] == 0 &&
                                        temp1Array[i * curBitmap.Width + j - 2] == 0 &&
                                        temp1Array[i * curBitmap.Width + j - 1] == 0 &&
                                        temp1Array[i * curBitmap.Width + j] == 0 &&
                                        temp1Array[i * curBitmap.Width + j + 1] == 0 &&
                                        temp1Array[i * curBitmap.Width + j + 2] == 0 &&
                                        temp1Array[(i + 2) * curBitmap.Width + j - 2] == 0 &&
                                        temp1Array[(i + 2) * curBitmap.Width + j - 1] == 0 &&
                                        temp1Array[(i + 2) * curBitmap.Width + j] == 0 &&
                                        temp1Array[(i + 2) * curBitmap.Width + j + 1] == 0 &&
                                        temp1Array[(i + 2) * curBitmap.Width + j + 2] == 0 &&
                                        temp1Array[(i + 1) * curBitmap.Width + j - 2] == 0 &&
                                        temp1Array[(i + 1) * curBitmap.Width + j - 1] == 0 &&
                                        temp1Array[(i + 1) * curBitmap.Width + j] == 0 &&
                                        temp1Array[(i + 1) * curBitmap.Width + j + 1] == 0 &&
                                        temp1Array[(i + 1) * curBitmap.Width + j + 2] == 0)
                                    {
                                        tempArray[i * curBitmap.Width + j] = 0;
                                    }

                                }
                            }
                            break;
                        default:
                            MessageBox.Show("错误的结构元素！");
                            break;
                    }


                    grayValues = (byte[])tempArray.Clone();

                    System.Runtime.InteropServices.Marshal.Copy(grayValues, 0, ptr, bytes);
                    curBitmap.UnlockBits(bmpData);
                }

                Invalidate();
            }
        }

        private void hitMiss_Click(object sender, EventArgs e)
        {
            if (curBitmap != null)
            {
                hitmiss hitAndMiss = new hitmiss();
                if (hitAndMiss.ShowDialog() == DialogResult.OK)
                {
                    Rectangle rect = new Rectangle(0, 0, curBitmap.Width, curBitmap.Height);
                    System.Drawing.Imaging.BitmapData bmpData = curBitmap.LockBits(rect, System.Drawing.Imaging.ImageLockMode.ReadWrite, curBitmap.PixelFormat);
                    IntPtr ptr = bmpData.Scan0;
                    int bytes = curBitmap.Width * curBitmap.Height;
                    byte[] grayValues = new byte[bytes];
                    System.Runtime.InteropServices.Marshal.Copy(ptr, grayValues, 0, bytes);

                    bool[] hitStru = hitAndMiss.GetHitStruction;
                    bool[] missStru = hitAndMiss.GetMissStruction;

                    byte[] tempArray = new byte[bytes];
                    byte[] temp1Array = new byte[bytes];
                    byte[] temp2Array = new byte[bytes];
                    for (int i = 0; i < bytes; i++)
                    {
                        tempArray[i] = (byte)(255 - grayValues[i]);
                        temp1Array[i] = 255;
                        temp2Array[i] = 255;
                    }

                    for (int i = 1; i < curBitmap.Height - 1; i++)
                    {
                        for (int j = 1; j < curBitmap.Width - 1; j++)
                        {
                            if ((grayValues[(i - 1) * curBitmap.Width + j - 1] == 0 || hitStru[0] == false) &&
                                (grayValues[(i - 1) * curBitmap.Width + j] == 0 || hitStru[1] == false) &&
                                (grayValues[(i - 1) * curBitmap.Width + j + 1] == 0 || hitStru[2] == false) &&
                                (grayValues[i * curBitmap.Width + j - 1] == 0 || hitStru[3] == false) &&
                                (grayValues[i * curBitmap.Width + j] == 0 || hitStru[4] == false) &&
                                (grayValues[i * curBitmap.Width + j + 1] == 0 || hitStru[5] == false) &&
                                (grayValues[(i + 1) * curBitmap.Width + j - 1] == 0 || hitStru[6] == false) &&
                                (grayValues[(i + 1) * curBitmap.Width + j] == 0 || hitStru[7] == false) &&
                                (grayValues[(i + 1) * curBitmap.Width + j + 1] == 0 || hitStru[8] == false))
                            {
                                temp1Array[i * curBitmap.Width + j] = 0;
                            }

                        }
                    }

                    for (int i = 1; i < curBitmap.Height - 1; i++)
                    {
                        for (int j = 1; j < curBitmap.Width - 1; j++)
                        {
                            if ((tempArray[(i - 1) * curBitmap.Width + j - 1] == 0 || missStru[0] == false) &&
                                (tempArray[(i - 1) * curBitmap.Width + j] == 0 || missStru[1] == false) &&
                                (tempArray[(i - 1) * curBitmap.Width + j + 1] == 0 || missStru[2] == false) &&
                                (tempArray[i * curBitmap.Width + j - 1] == 0 || missStru[3] == false) &&
                                (tempArray[i * curBitmap.Width + j] == 0 || missStru[4] == false) &&
                                (tempArray[i * curBitmap.Width + j + 1] == 0 || missStru[5] == false) &&
                                (tempArray[(i + 1) * curBitmap.Width + j - 1] == 0 || missStru[6] == false) &&
                                (tempArray[(i + 1) * curBitmap.Width + j] == 0 || missStru[7] == false) &&
                                (tempArray[(i + 1) * curBitmap.Width + j + 1] == 0 || missStru[8] == false))
                            {
                                temp2Array[i * curBitmap.Width + j] = 0;
                            }

                        }
                    }

                    for (int i = 0; i < bytes; i++)
                    {
                        if(temp1Array[i] == 0 && temp2Array[i] == 0)
                        {
                            tempArray[i] = 0;
                        }
                        else
                        {
                            tempArray[i] = 255;
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