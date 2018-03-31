using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace segmentatioin
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

        private void hough_Click(object sender, EventArgs e)
        {
            if (curBitmap != null)
            {
                hough houghtran = new hough(curBitmap);
                houghtran.ShowDialog();
            }
        }

        private void threshold_Click(object sender, EventArgs e)
        {
            if (curBitmap != null)
            {
                thresholding thrMethod = new thresholding();
                if (thrMethod.ShowDialog() == DialogResult.OK)
                {
                    Rectangle rect = new Rectangle(0, 0, curBitmap.Width, curBitmap.Height);
                    System.Drawing.Imaging.BitmapData bmpData = curBitmap.LockBits(rect, System.Drawing.Imaging.ImageLockMode.ReadWrite, curBitmap.PixelFormat);
                    IntPtr ptr = bmpData.Scan0;
                    int bytes = curBitmap.Width * curBitmap.Height;
                    byte[] grayValues = new byte[bytes];
                    System.Runtime.InteropServices.Marshal.Copy(ptr, grayValues, 0, bytes);

                    byte method = thrMethod.GetMethod;

                    byte T = 0, S = 0;
                    byte[] neighb = new byte[bytes];
                    byte temp = 0;
                    byte maxGray = 0;
                    byte minGray = 255;
                    int[] countPixel = new int[256];
                    for (int i = 0; i < grayValues.Length; i++)
                    {
                        temp = grayValues[i];
                        countPixel[temp]++;
                        if (temp > maxGray)
                        {
                            maxGray = temp;
                        }
                        if (temp < minGray)
                        {
                            minGray = temp;
                        }
                    }
                    double mu1, mu2;
                    int numerator, denominator;
                    double sigma;
                    double tempMax = 0;
                    switch (method)
                    {
                        case 0://迭代法
                            byte oldT;                             
                            T = oldT = Convert.ToByte((maxGray + minGray) / 2);
                                                        
                            do
                            {
                                oldT = T;
                                numerator = denominator = 0;
                                for (int i = minGray; i < T; i++)
                                {
                                    numerator += i * countPixel[i];
                                    denominator += countPixel[i];
                                }
                                mu1 = numerator / denominator;

                                numerator = denominator = 0;
                                for (int i = T; i <= maxGray; i++)
                                {
                                    numerator += i * countPixel[i];
                                    denominator += countPixel[i];
                                }
                                mu2 = numerator / denominator;

                                T = Convert.ToByte((mu1 + mu2) / 2);
                            }
                            while (T != oldT);
                            break;
                        case 1://Otsu法
                            double w1 = 0, w2 = 0;                            
                            double sum = 0;
                            numerator = 0;
                            for (int i = minGray; i <= maxGray; i++)
                            {
                                sum += i * countPixel[i];
                            }
                            for (int i = minGray; i < maxGray; i++)
                            {
                                w1 += countPixel[i];
                                numerator += i * countPixel[i];
                                mu1 = numerator / w1;
                                w2 = grayValues.Length - w1;
                                mu2 = (sum - numerator) / w2;
                                sigma = w1 * w2 * (mu1 - mu2) * (mu1 - mu2);

                                if (sigma > tempMax)
                                {
                                    tempMax = sigma;
                                    T = Convert.ToByte(i); 
                                }
                            }
                            break;
                        case 2://一维最大熵法
                            double Ht = 0.0, Hl = 0.0, p = 0.0, pt = 0.0;
                            for (int i = minGray; i <= maxGray; i++)
                            {
                                p = (double)countPixel[i] / grayValues.Length;
                                if (p < 0.00000000000000001)
                                    continue;
                                Hl += -p * Math.Log10(p);
                            }
                            for (int i = minGray; i <= maxGray; i++)
                            {
                                p = (double)countPixel[i] / grayValues.Length;
                                pt += p;
                                if (p < 0.00000000000000001)
                                    continue;
                                Ht += -p * Math.Log10(p);
                                sigma = Math.Log10(pt * (1 - pt)) + Ht / pt + (Hl - Ht) / (1 - pt);
                                if (sigma > tempMax)
                                {
                                    tempMax = sigma;
                                    T = Convert.ToByte(i);
                                }
                            }
                            break;
                        case 3://二维最大熵
                            
                            double[,] pap = new double[256, 256];
                            double Hl2D = 0.0, Pa = 0.0, Ha = 0.0;
                            for (int i = 0; i < bytes; i++)
                            {
                                neighb[i] = Convert.ToByte((grayValues[(i + 1) % bytes] + grayValues[Math.Abs(i - 1) % bytes] + grayValues[(i + curBitmap.Width) % bytes] + grayValues[Math.Abs(i - curBitmap.Width) % bytes]) / 4);                                
                            }
                            for (int i = 0; i < bytes; i++)
                            {
                                for (int j = 0; j < bytes; j++)
                                {
                                    int ii = grayValues[i];
                                    int jj = neighb[j];
                                    pap[ii, jj]++;
                                }                                    
                            }
                            
                            for (int i = 0; i < 256; i++)
                            {
                                for (int j = 0; j < 256; j++)
                                {
                                    pap[i, j] = ((double)pap[i, j] / bytes) / bytes;
                                    if (pap[i, j] < 0.00000000000000001)
                                        continue;
                                    Hl2D += -pap[i, j] * Math.Log10(pap[i, j]);                                    
                                }
                            }
                            for (int i = 0; i <= 255; i++)
                            {
                                for (int j = 0; j <= 255; j++)
                                {
                                    Pa += pap[i, j];
                                    if (pap[i, j] < 0.00000000000000001)
                                        continue;
                                    Ha += -pap[i, j] * Math.Log10(pap[i, j]);
                                    sigma = Math.Log10(Pa * (1 - Pa)) + Ha / Pa + (Hl2D - Ha) / (1 - Pa);
                                    if (sigma > tempMax)
                                    {
                                        tempMax = sigma;
                                        S = Convert.ToByte(i);
                                        T = Convert.ToByte(j);
                                    }
                                }
                            }
                            break;
                        case 4://简单统计法
                            int[] ee = new int[bytes];
                            int ex, ey, ef = 0, esum = 0;
                            for (int i = 0; i < bytes; i++)
                            {
                                ex = Math.Abs(grayValues[(i + 1) % bytes] - grayValues[Math.Abs(i - 1) % bytes]);
                                ey = Math.Abs(grayValues[(i + curBitmap.Width) % bytes] - grayValues[Math.Abs(i - curBitmap.Width) % bytes]);
                                ee[i] = Math.Max(ex, ey);
                                ef += ee[i] * grayValues[i];
                                esum += ee[i];
                            }
                            T = Convert.ToByte(ef / esum);
                            break;
                        default:
                            break;
                    }

                    for (int i = 0; i < bytes; i++)
                    {
                        if (method == 3)
                        {
                            if (grayValues[i] < S && neighb[i] < T)
                                grayValues[i] = 0;
                            else
                                grayValues[i] = 255;
                        }
                        else
                        {
                            if (grayValues[i] < T)
                                grayValues[i] = 0;
                            else
                                grayValues[i] = 255;
                        }
                    }

                    System.Runtime.InteropServices.Marshal.Copy(grayValues, 0, ptr, bytes);
                    curBitmap.UnlockBits(bmpData);
                }

                Invalidate();
            }
        }

        private void clus_Click(object sender, EventArgs e)
        {
            if (curBitmap != null)
            {
                cluster cluMethod = new cluster();
                if (cluMethod.ShowDialog() == DialogResult.OK)
                {
                    Rectangle rect = new Rectangle(0, 0, curBitmap.Width, curBitmap.Height);
                    System.Drawing.Imaging.BitmapData bmpData = curBitmap.LockBits(rect, System.Drawing.Imaging.ImageLockMode.ReadWrite, curBitmap.PixelFormat);
                    IntPtr ptr = bmpData.Scan0;
                    int bytes = curBitmap.Width * curBitmap.Height;
                    byte[] grayValues = new byte[bytes];
                    System.Runtime.InteropServices.Marshal.Copy(ptr, grayValues, 0, bytes);

                    bool method = cluMethod.GetMethod;
                    int numbers = cluMethod.GetNumber;

                    if (method == false)//k-mean
                    {                        
                        int[] kNum = new int[numbers];
                        int[] kAver = new int[numbers];
                        int[] kOldAver = new int[numbers];
                        int[] kSum = new int[numbers];
                        int[] kTemp = new int[numbers];
                        byte[] segmentMap = new byte[bytes];
                        //初始化聚类均值
                        for (int i = 0; i < numbers; i++)
                        {
                            //kNum[i] = 0;
                            //kSum[i] = 0;
                            kAver[i] = kOldAver[i] = Convert.ToInt16(i * 255 / (numbers - 1));
                        }

                        while (true)
                        {
                            int order = 0;
                            for (int i = 0; i < numbers; i++)
                            {
                                kAver[i] = kOldAver[i];
                                kNum[i] = 0;
                                kSum[i] = 0;
                            }
                            //归属聚类
                            for (int i = 0; i < bytes; i++)
                            {
                                for (int j = 0; j < numbers; j++)
                                {
                                    kTemp[j] = Math.Abs(grayValues[i] - kAver[j]);
                                }
                                int temp = 255;

                                for (int j = 0; j < numbers; j++)
                                {
                                    if (kTemp[j] < temp)
                                    {
                                        temp = kTemp[j];
                                        order = j;
                                    }
                                }
                                kNum[order]++;
                                kSum[order] += grayValues[i];
                                segmentMap[i] = Convert.ToByte(order);
                            }
                            for (int i = 0; i < numbers; i++)
                            {
                                if (kNum[i] != 0)
                                    kOldAver[i] = Convert.ToInt16(kSum[i] / kNum[i]);
                            }
                            
                            int kkk = 0;
                            for (int i = 0; i < numbers; i++)
                            {
                                if (kAver[i] == kOldAver[i])
                                    kkk++;
                            }
                            if (kkk == numbers)
                                break;
                        }

                        for (int i = 0; i < bytes; i++)
                        {
                            for (int j = 0; j < numbers; j++)
                            {
                                if (segmentMap[i] == j)
                                {
                                    grayValues[i] = Convert.ToByte(kAver[j]);
                                }
                            }
                        }
                    }
                    else//ISODATA
                    {
                        int k = 2 * numbers;
                        byte[] segmentMap = new byte[bytes];
                        List<int> kTemp = new List<int>();
                        List<int> kNum = new List<int>();
                        List<int> kAver = new List<int>();
                        List<int> kSum = new List<int>();
                        kAver.Clear();
                        kNum.Clear();
                        kTemp.Clear();
                        kSum.Clear();
                        for (int i = 0; i < k; i++)
                        {
                            kAver.Add(Convert.ToInt16(i * 255 / (k - 1)));
                            kNum.Add(0);
                            kTemp.Add(0);
                            kSum.Add(0);
                        }

                        while (true)
                        {
                            int temp;
                            for (int i = 0; i < bytes; i++)
                            {
                                kTemp.Clear();
                                int order = 0;
                                for (int j = 0; j < k; j++)
                                {
                                    kTemp.Add(Math.Abs(grayValues[i] - kAver[j]));
                                }
                                temp = 255;

                                for (int j = 0; j < k; j++)
                                {
                                    if (kTemp[j] < temp)
                                    {
                                        temp = kTemp[j];
                                        order = j;
                                    }
                                }
                                int num = kNum[order] + 1;
                                kNum.RemoveAt(order);
                                kNum.Insert(order, num);
                                int sum = kSum[order] + grayValues[i];
                                kSum.RemoveAt(order);
                                kSum.Insert(order, sum);
                                segmentMap[i] = Convert.ToByte(order);
                            }
                            
                            for (int i = 0; i < k; i++)
                            {
                                if (kNum[i] == 0)
                                {
                                    kNum.RemoveAt(i);
                                    kAver.RemoveAt(i);
                                    kSum.RemoveAt(i);
                                    i--;
                                    k--;
                                }
                            }
                            
                            kAver.Clear();
                            for (int i = 0; i < k; i++)
                            {                               
                                kAver.Add(Convert.ToInt16(kSum[i] / kNum[i]));
                            }
                            if (k <= numbers)
                                break;
                            temp = 255;
                            int removeI = 0, removeJ = 0;
                            for (int i = 0; i < k; i++)
                            {
                                for (int j = i + 1; j < k; j++)
                                {
                                    int distanceIJ = Math.Abs(kAver[i] - kAver[j]);
                                    if (distanceIJ < temp)
                                    {
                                        temp = distanceIJ;
                                        removeI = i;
                                        removeJ = j;
                                    }
                                }
                            }
                            k--;
                            kNum.Add(kNum[removeI] + kNum[removeJ]);
                            kAver.Add(Convert.ToInt16((kNum[removeI] * kAver[removeI] + kNum[removeJ] * kAver[removeJ]) / (kNum[removeI] + kNum[removeJ])));
                            kSum.Add(kNum[removeI] * kAver[removeI] + kNum[removeJ] * kAver[removeJ]);
                            kNum.RemoveAt(removeI);
                            kNum.RemoveAt(removeJ);
                            kAver.RemoveAt(removeI);
                            kAver.RemoveAt(removeJ);
                            kSum.RemoveAt(removeI);
                            kSum.RemoveAt(removeJ);
                        }

                        for (int i = 0; i < bytes; i++)
                        {
                            for (int j = 0; j < numbers; j++)
                            {
                                if (segmentMap[i] == j)
                                {
                                    grayValues[i] = Convert.ToByte(kAver[j]);
                                }
                            }
                        }
                    }

                    System.Runtime.InteropServices.Marshal.Copy(grayValues, 0, ptr, bytes);
                    curBitmap.UnlockBits(bmpData);
                }

                Invalidate();
            }
        }

        private void overRelax_Click(object sender, EventArgs e)
        {
            if (curBitmap != null)
            {
                ORI overRelaxIter = new ORI();
                if (overRelaxIter.ShowDialog() == DialogResult.OK)
                {
                    Rectangle rect = new Rectangle(0, 0, curBitmap.Width, curBitmap.Height);
                    System.Drawing.Imaging.BitmapData bmpData = curBitmap.LockBits(rect, System.Drawing.Imaging.ImageLockMode.ReadWrite, curBitmap.PixelFormat);
                    IntPtr ptr = bmpData.Scan0;
                    int bytes = curBitmap.Width * curBitmap.Height;
                    byte[] grayValues = new byte[bytes];
                    System.Runtime.InteropServices.Marshal.Copy(ptr, grayValues, 0, bytes);

                    int segNumber = overRelaxIter.GetSegNum;
                    int iterNumber = overRelaxIter.GetIterNum;

                    int[] kSum = new int[segNumber];
                    double[] kAver = new double[segNumber];
                    double[] kVar = new double[segNumber];
                    Array.Clear(kAver, 0, segNumber);
                    Array.Clear(kSum, 0, segNumber);
                    Array.Clear(kVar, 0, segNumber);

                    int[] imageMap = new int[bytes];

                    for (int i = 0; i < bytes; i++)
                    {
                        for (int j = 1; j <= segNumber; j++)
                        {
                            if (grayValues[i] < Convert.ToByte(255 * j / segNumber))
                            {
                                imageMap[i] = j - 1;
                                kAver[j - 1] += grayValues[i];
                                kSum[j - 1] += 1;
                                break;
                            }
                        }
                    }

                    for (int i = 0; i < segNumber; i++)
                    {
                        if (kSum[i] != 0)
                            kAver[i] /= kSum[i];
                    }
                    for (int i = 0; i < bytes; i++)
                    {
                        kVar[imageMap[i]] += Convert.ToInt16(Math.Pow(grayValues[i] - kAver[imageMap[i]], 2));
                    }
                    for (int i = 0; i < segNumber; i++)
                    {
                        if (kSum[i] != 0)
                            kVar[i] /= kSum[i];
                    }

                    double[] d = new double[bytes * segNumber];
                    double[] kProb = new double[bytes * segNumber];
                    Array.Clear(d, 0, bytes * segNumber);
                    Array.Clear(kProb, 0, bytes * segNumber);

                    for (int i = 0; i < bytes; i++)
                    {
                        for (int j = 0; j < segNumber; j++)
                        {
                            if (kVar[j] == 0)
                                kVar[j] = 0.00000005;
                            d[i * segNumber + j] = Math.Pow(kAver[j] - grayValues[i], 2) / kVar[j];
                            if (d[i * segNumber + j] == 0)
                                d[i * segNumber + j] = 0.00000005;
                        }
                    }

                    double tempSum = 0;
                    for (int i = 0; i < bytes; i++)
                    {
                        tempSum = 0;
                        for (int j = 0; j < segNumber; j++)
                            tempSum += 1 / d[i * segNumber + j];
                        for (int j = 0; j < segNumber; j++)
                            kProb[i * segNumber + j] = 1 / (tempSum * d[i * segNumber + j]);
                    }

                    while (iterNumber != 0)
                    {
                        iterNumber--;

                        for (int i = 0; i < bytes; i++)
                        {
                            for (int j = 0; j < segNumber; j++)
                            {
                                tempSum = kProb[(Math.Abs(i + 1 - curBitmap.Width) % bytes) * segNumber + j] + kProb[(Math.Abs(i - curBitmap.Width) % bytes) * segNumber + j] + kProb[(Math.Abs(i - 1 - curBitmap.Width) % bytes) * segNumber + j] + kProb[(Math.Abs(i - 1) % bytes) * segNumber + j] + kProb[((i - 1 + curBitmap.Width) % bytes) * segNumber + j] + kProb[((i + curBitmap.Width) % bytes) * segNumber + j] + kProb[((i + 1 + curBitmap.Width) % bytes) * segNumber + j] + kProb[((i + 1) % bytes) * segNumber + j];
                                d[i * segNumber + j] = tempSum / 8;
                                tempSum = 0;
                                for (int k = 0; k < segNumber; k++)
                                    tempSum += kProb[i * segNumber + k] * (1 + d[i * segNumber + k]);
                                kProb[i * segNumber + j] *= (1 + d[i * segNumber + j]) / tempSum;
                            }
                        }
                    }

                    for (int i = 0; i < bytes; i++)
                    {
                        double tempMax = 0;
                        int m = 0;
                        for (int j = 0; j < segNumber; j++)
                        {
                            if (kProb[i * segNumber + j] > tempMax)
                            {
                                tempMax = kProb[i * segNumber + j];
                                m = j;
                            }
                        }
                        grayValues[i] = Convert.ToByte(m * 255 / segNumber);
                    }

                    System.Runtime.InteropServices.Marshal.Copy(grayValues, 0, ptr, bytes);
                    curBitmap.UnlockBits(bmpData);
                }

                Invalidate();
            }
        }
    }
}