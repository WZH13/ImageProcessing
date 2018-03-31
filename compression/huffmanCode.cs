using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace compression
{
    public partial class huffmanCode : Form
    {
        public huffmanCode(Bitmap bmp)
        {
            InitializeComponent();
            bmpHuffman = bmp;
            huffmanData.Columns.Add("灰度值", 50);
            huffmanData.Columns.Add("出现概率", 130, HorizontalAlignment.Center);
            huffmanData.Columns.Add("哈夫曼编码", 130, HorizontalAlignment.Center);
            huffmanData.Columns.Add("码字长", 60, HorizontalAlignment.Center);
        }

        private void close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void huffmanCode_Load(object sender, EventArgs e)
        {
            Rectangle rect = new Rectangle(0, 0, bmpHuffman.Width, bmpHuffman.Height);
            System.Drawing.Imaging.BitmapData bmpData = bmpHuffman.LockBits(rect, System.Drawing.Imaging.ImageLockMode.ReadWrite, bmpHuffman.PixelFormat);
            IntPtr ptr = bmpData.Scan0;
            int bytes = bmpHuffman.Width * bmpHuffman.Height;
            byte[] grayValues = new byte[bytes];
            System.Runtime.InteropServices.Marshal.Copy(ptr, grayValues, 0, bytes);

            double[] hist = new double[256];
            double[] feq = new double[256];
            int[] hMap = new int[256];
            string[] strCode = new string[256];
            double entr = 0.0;
            double codeLeng = 0.0;

            for (int i = 0; i < 256; i++)
            {
                hist[i] = 0.0;
                hMap[i] = i;
            }

            for (int i = 0; i < bytes; i++)
            {
                hist[grayValues[i]] += 1;
            }

            for (int i = 0; i < 256; i++)
            {
                hist[i] /= (double)bytes;
                feq[i] = hist[i];
            }

            double temp = 0;
            for (int i = 0; i < 255; i++)
            {
                for (int j = 0; j < 255 - i; j++)
                {
                    if (hist[j] > hist[j + 1])
                    {
                        temp = hist[j];
                        hist[j] = hist[j + 1];
                        hist[j + 1] = temp;

                        for (int k = 0; k < 256; k++)
                        {
                            if (hMap[k] == j)
                                hMap[k] = j + 1;
                            else if (hMap[k] == j + 1)
                                hMap[k] = j;
                        }
                    }
                }
            }

            for (int i = 0; i < 255; i++)
            {
                if (hist[i] == 0.0)
                    continue;

                for (int j = i; j < 255; j++)
                {
                    for (int k = 0; k < 256; k++)
                    {
                        if (hMap[k] == j)
                            strCode[k] = "1" + strCode[k];
                        else if (hMap[k] == j + 1)
                            strCode[k] = "0" + strCode[k];
                    }

                    hist[j + 1] += hist[j];

                    for (int k = 0; k < 256; k++)
                    {
                        if (hMap[k] == j)
                            hMap[k] = j + 1;
                    }

                    for (int m = j + 1; m < 255; m++)
                    {
                        if (hist[m] > hist[m + 1])
                        {
                            temp = hist[m];
                            hist[m] = hist[m + 1];
                            hist[m + 1] = temp;

                            for (int k = 0; k < 256; k++)
                            {
                                if (hMap[k] == m)
                                    hMap[k] = m + 1;
                                else if (hMap[k] == m + 1)
                                    hMap[k] = m;
                            }
                        }
                        else
                            break;
                    }
                }

                break;
            }

            for (int i = 0; i < 256; i++)
            {
                ListViewItem li = new ListViewItem();
                li.Text = i.ToString();
                li.SubItems.Add(feq[i].ToString("F10"));
                li.SubItems.Add(strCode[i]);
                if (strCode[i] == null)
                    li.SubItems.Add("0");
                else
                {
                    li.SubItems.Add(strCode[i].Length.ToString());
                    codeLeng += feq[i] * strCode[i].Length;
                    entr -= feq[i] * Math.Log(feq[i], 2);
                }
                huffmanData.Items.Add(li);
            }

            entropy.Text = entr.ToString("F8");
            averLength.Text = codeLeng.ToString("F8");
            double codeEff = entr / codeLeng * 100;
            efficiency.Text = codeEff.ToString("F4");

            System.Runtime.InteropServices.Marshal.Copy(grayValues, 0, ptr, bytes);
            bmpHuffman.UnlockBits(bmpData);
        }
    }
}