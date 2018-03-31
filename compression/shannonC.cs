using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace compression
{
    public partial class shannonC : Form
    {
        public shannonC(Bitmap bmp)
        {
            InitializeComponent();
            bmpShannon = bmp;
            shannonData.Columns.Add("灰度值", 50);
            shannonData.Columns.Add("出现概率", 130, HorizontalAlignment.Center);
            shannonData.Columns.Add("香农编码", 130, HorizontalAlignment.Center);
            shannonData.Columns.Add("码字长", 60, HorizontalAlignment.Center);
        }

        private void close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void shannonC_Load(object sender, EventArgs e)
        {
            Rectangle rect = new Rectangle(0, 0, bmpShannon.Width, bmpShannon.Height);
            System.Drawing.Imaging.BitmapData bmpData = bmpShannon.LockBits(rect, System.Drawing.Imaging.ImageLockMode.ReadWrite, bmpShannon.PixelFormat);
            IntPtr ptr = bmpData.Scan0;
            int bytes = bmpShannon.Width * bmpShannon.Height;
            byte[] grayValues = new byte[bytes];
            System.Runtime.InteropServices.Marshal.Copy(ptr, grayValues, 0, bytes);

            double[] hist = new double[256];
            double[] feq = new double[256];
            int[] hMap = new int[256];
            string[] strCode = new string[256];
            double entr = 0.0;
            int[] codeLeng = new int[256];
            double averCL = 0.0;
            double[] accP = new double[256];

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
                if (hist[i] > 0.0)
                    codeLeng[i] = Convert.ToInt16(1 - Math.Log(hist[i], 2));
                else
                    codeLeng[i] = 0;
            }

            double fTemp = 0.0;
            int iTemp = 0;

            for (int i = 0; i < 255; i++)
            {
                for (int j = 0; j < 255 - i; j++)
                {
                    if (hist[j] < hist[j + 1])
                    {
                        fTemp = hist[j];
                        hist[j] = hist[j + 1];
                        hist[j + 1] = fTemp;

                        iTemp = hMap[j];
                        hMap[j] = hMap[j + 1];
                        hMap[j + 1] = iTemp;
                    }
                }
            }

            for (int i = 0; i < 255; i++)
            {
                if (hist[i] != 0.0)
                    continue;

                accP[0] = 0.0;
                for (int k = 0; k < codeLeng[hMap[0]]; k++)
                    strCode[hMap[0]] += "0";
                for (int j = 1; j < i; j++)
                {
                    for (int k = 0; k < j; k++)
                    {
                        accP[j] += hist[k];
                    }

                    for (int k = 0; k < codeLeng[hMap[j]]; k++)
                    {
                        accP[j] *= 2;
                        if (accP[j] >= 1)
                        {
                            strCode[hMap[j]] += "1";
                            accP[j] -= 1;
                        }
                        else
                            strCode[hMap[j]] += "0";
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
                    li.SubItems.Add(codeLeng[i].ToString());
                    averCL += feq[i] * strCode[i].Length;
                    entr -= feq[i] * Math.Log(feq[i], 2);
                }
                shannonData.Items.Add(li);
            }

            entropy.Text = entr.ToString("F8");
            averLength.Text = averCL.ToString("F8");
            double codeEff = entr / averCL * 100;
            efficiency.Text = codeEff.ToString("F4");

            System.Runtime.InteropServices.Marshal.Copy(grayValues, 0, ptr, bytes);
            bmpShannon.UnlockBits(bmpData);
        }
    }
}