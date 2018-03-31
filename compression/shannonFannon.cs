using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace compression
{
    public partial class shannonFannon : Form
    {
        public shannonFannon(Bitmap bmp)
        {
            InitializeComponent();
            bmpSF = bmp;
            sfData.Columns.Add("灰度值", 50);
            sfData.Columns.Add("出现概率", 130, HorizontalAlignment.Center);
            sfData.Columns.Add("香农-弗诺编码", 130, HorizontalAlignment.Center);
            sfData.Columns.Add("码字长", 60, HorizontalAlignment.Center);
        }

        private void close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void shannonFannon_Load(object sender, EventArgs e)
        {
            Rectangle rect = new Rectangle(0, 0, bmpSF.Width, bmpSF.Height);
            System.Drawing.Imaging.BitmapData bmpData = bmpSF.LockBits(rect, System.Drawing.Imaging.ImageLockMode.ReadWrite, bmpSF.PixelFormat);
            IntPtr ptr = bmpData.Scan0;
            int bytes = bmpSF.Width * bmpSF.Height;
            byte[] grayValues = new byte[bytes];
            System.Runtime.InteropServices.Marshal.Copy(ptr, grayValues, 0, bytes);


            double[] hist = new double[256];
            double[] feq = new double[256];
            int[] hMap = new int[256];
            string[] strCode = new string[256];
            double entr = 0.0;
            double codeLeng = 0.0;
            bool[] bFlag = new bool[256];
            double feqSum = 0.0;
            double feqTotal = 0.0;

            for (int i = 0; i < 256; i++)
            {
                hist[i] = 0.0;
                hMap[i] = i;
                bFlag[i] = false;
            }

            for (int i = 0; i < bytes; i++)
            {
                hist[grayValues[i]] += 1;
            }

            for (int i = 0; i < 256; i++)
            {
                hist[i] /= (double)bytes;
                feq[i] = hist[i];
                feqTotal += feq[i];
            }

            double fTemp = 0;
            int iTemp = 0;
            for (int i = 0; i < 255; i++)
            {
                for (int j = 0; j < 255 - i; j++)
                {
                    if (hist[j] > hist[j + 1])
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
                if (hist[i] == 0.0)
                    continue;

                int gCount = 0;

                while (gCount < 256)
                {
                    gCount = i;

                    for (int j = i; j < 256; j++)
                    {
                        if (bFlag[j] == false)
                        {
                            feqSum += hist[j];
                            if (feqSum > feqTotal / 2)
                                strCode[hMap[j]] = strCode[hMap[j]] + "0";
                            else
                                strCode[hMap[j]] = strCode[hMap[j]] + "1";

                            if (feqTotal == feqSum)
                            {
                                feqSum = 0.0;
                                feqTotal = 0.0;
                                int k;
                                if (j == 255)
                                    k = i;
                                else
                                    k = j + 1;
                                int m;
                                for (m = k; m < 256; m++)
                                {
                                    if ((strCode[hMap[m]].Substring((strCode[hMap[m]].Length) - 1) != (strCode[hMap[k]].Substring((strCode[hMap[k]].Length) - 1))
                                        || (strCode[hMap[m]].Length) != (strCode[hMap[k]].Length)))
                                        break;
                                    feqTotal += hist[m];
                                }
                                if (k + 1 == m)
                                    bFlag[k] = true;
                            }
                        }
                        else
                        {
                            gCount++;

                            feqSum = 0.0;
                            feqTotal = 0.0;
                            int k;
                            if (j == 255)
                                k = i;
                            else
                                k = j + 1;
                            int m;
                            for (m = k; m < 256; m++)
                            {
                                if ((strCode[hMap[m]].Substring((strCode[hMap[m]].Length) - 1) != (strCode[hMap[k]].Substring((strCode[hMap[k]].Length) - 1))
                                    || (strCode[hMap[m]].Length) != (strCode[hMap[k]].Length)))
                                    break;
                                feqTotal += hist[m];
                            }
                            if (k + 1 == m)
                                bFlag[k] = true;
                        }
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
                sfData.Items.Add(li);
            }

            entropy.Text = entr.ToString("F8");
            averLength.Text = codeLeng.ToString("F8");
            double codeEff = entr / codeLeng * 100;
            efficiency.Text = codeEff.ToString("F4");

            System.Runtime.InteropServices.Marshal.Copy(grayValues, 0, ptr, bytes);
            bmpSF.UnlockBits(bmpData);
        }
    }
}