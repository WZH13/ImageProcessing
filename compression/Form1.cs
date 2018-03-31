using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Collections;

namespace compression
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

        private void huffman_Click(object sender, EventArgs e)
        {
            if (curBitmap != null)
            {
                huffmanCode huffmanCoding = new huffmanCode(curBitmap);
                huffmanCoding.ShowDialog();
            }
        }

        private void shanFan_Click(object sender, EventArgs e)
        {
            if (curBitmap != null)
            {
                shannonFannon shFCoding = new shannonFannon(curBitmap);
                shFCoding.ShowDialog();
            }
        }

        private void shannon_Click(object sender, EventArgs e)
        {
            if (curBitmap != null)
            {
                shannonC shannonCoding = new shannonC(curBitmap);
                shannonCoding.ShowDialog();
            }
        }

        private void rle_Click(object sender, EventArgs e)
        {
            rleCode rleDC = new rleCode();
            if (rleDC.ShowDialog() == DialogResult.OK)
            {
                bool rleDorC = rleDC.GetDorC;
                if (rleDorC == false)
                {
                    if (curBitmap == null)
                    {
                        MessageBox.Show("请先打开图像！");
                        return;
                    }

                    Rectangle rect = new Rectangle(0, 0, curBitmap.Width, curBitmap.Height);
                    System.Drawing.Imaging.BitmapData bmpData = curBitmap.LockBits(rect, System.Drawing.Imaging.ImageLockMode.ReadWrite, curBitmap.PixelFormat);
                    IntPtr ptr = bmpData.Scan0;
                    int bytes = curBitmap.Width * curBitmap.Height;
                    byte[] grayValues = new byte[bytes];
                    System.Runtime.InteropServices.Marshal.Copy(ptr, grayValues, 0, bytes);
                    curBitmap.UnlockBits(bmpData);

                    char[] rleData = new char[bytes * 2];
                    int rleLength = 0;
                    int position = 0;
                    char oldData, newData;
                    byte sameCount = 0;
                    char[] rleHeader = new char[6];

                    rleHeader[0] = Convert.ToChar(0x0B);
                    rleHeader[1] = Convert.ToChar(0x08);
                    rleHeader[2] = Convert.ToChar(curBitmap.Height / 0xFF);
                    rleHeader[3] = Convert.ToChar(curBitmap.Height % 0xFF);
                    rleHeader[4] = Convert.ToChar(curBitmap.Width / 0xFF);
                    rleHeader[5] = Convert.ToChar(curBitmap.Width % 0xFF);

                    for (int i = 0; i < curBitmap.Height; i++)
                    {
                        position = curBitmap.Width * i;
                        oldData = Convert.ToChar(grayValues[position]);
                        sameCount = 1;
                        for (int j = 1; j < curBitmap.Width; j++)
                        {
                            position++;
                            newData = Convert.ToChar(grayValues[position]);

                            if ((newData == oldData) && (sameCount < 63))
                                sameCount++;
                            else
                            {
                                if ((sameCount > 1) || (oldData >= 0xC0))
                                {
                                    rleData[rleLength] = Convert.ToChar(sameCount | 0xC0);
                                    rleData[rleLength + 1] = oldData;
                                    rleLength += 2;
                                }
                                else
                                {
                                    rleData[rleLength] = oldData;
                                    rleLength++;
                                }
                                oldData = newData;
                                sameCount = 1;
                            }
                        }

                        if ((sameCount > 1) || (oldData >= 0xC0))
                        {
                            rleData[rleLength] = Convert.ToChar(sameCount | 0xC0);
                            rleData[rleLength + 1] = oldData;
                            rleLength += 2;
                        }
                        else
                        {
                            rleData[rleLength] = oldData;
                            rleLength++;
                        }
                    }
                    
                    SaveFileDialog sf = new SaveFileDialog();
                    //设置标题
                    sf.Title = "保存文件";
                    //设置文件保存类型
                    sf.Filter = "自定义图像格式(*.zrle)|*.zrle";
                    //如果用户没有输入扩展名，自动追加后缀
                    sf.AddExtension = true;
                    //如果用户单击了保存按钮
                    if (sf.ShowDialog() == DialogResult.OK)
                    {
                        //实例化一个文件流
                        FileStream fs = new FileStream(sf.FileName, FileMode.Create);
                        //实例化一个StreamWriter
                        StreamWriter sw = new StreamWriter(fs);

                        //开始写入
                        //char nc = Convert.ToChar(0xc4);
                        sw.Write(rleHeader, 0, 6);
                        sw.Write(rleData, 0, rleLength);

                        //清空缓冲区
                        sw.Flush();
                        //关闭流
                        sw.Close();
                        fs.Close();
                    }

                }
                else
                {
                    OpenFileDialog of = new OpenFileDialog();
                    //设置标题
                    of.Title = "打开文件";
                    //设置文件保存类型
                    of.Filter = "自定义图像格式(*.zrle)|*.zrle";
                    //如果用户单击了保存按钮
                    if (of.ShowDialog() == DialogResult.OK)
                    {
                        //实例化一个文件流
                        FileStream fs = new FileStream(of.FileName, FileMode.Open);
                        //实例化一个StreamReader
                        StreamReader sr = new StreamReader(fs);

                        //开始写入

                        //清空缓冲区
                        //sr.Flush();
                        int rleTemp = sr.Read();
                        if (rleTemp != 0x0B)
                        {
                            MessageBox.Show("不是zrle文件格式！");
                            sr.Close();
                            fs.Close();
                            return;
                        }
                        rleTemp = sr.Read();
                        if (rleTemp != 0x08)
                        {
                            MessageBox.Show("本实例只能处理8位灰度图像！");
                            sr.Close();
                            fs.Close();
                            return;
                        }

                        int heightRle = sr.Read();
                        rleTemp = sr.Read();
                        heightRle = heightRle * 255 + rleTemp;
                        int widthRle = sr.Read();
                        rleTemp = sr.Read();
                        widthRle = widthRle * 255 + rleTemp;

                        string rleString = sr.ReadToEnd();
                        byte[] rleData = new byte[heightRle * widthRle];

                        int count = 0;
                        int sameCount = 0;
                        int totalCount = 0;
                        char bPix;
                        for (int i = 0; i < heightRle; i++)
                        {
                            count = 0;
                            while (count < widthRle)
                            {
                                bPix = rleString[totalCount];
                                totalCount++;
                                if ((bPix & 0xC0) == 0xC0)
                                {
                                    sameCount = bPix & 0x3F;
                                    bPix = rleString[totalCount];
                                    totalCount++;
                                    for (int j = 0; j < sameCount; j++)
                                        rleData[i * widthRle + count + j] = Convert.ToByte(bPix);
                                    count += sameCount;
                                }
                                else
                                {
                                    rleData[i * widthRle + count] = Convert.ToByte(bPix);
                                    count++;
                                }
                            }
                        }

                        curBitmap = new Bitmap(widthRle, heightRle, System.Drawing.Imaging.PixelFormat.Format8bppIndexed);
                        System.Drawing.Imaging.ColorPalette cp = curBitmap.Palette;
                        for (int i = 0; i < 256; i++)
                        {
                            cp.Entries[i] = Color.FromArgb(i, i, i);
                        }
                        curBitmap.Palette = cp;

                        Rectangle rectRle = new Rectangle(0, 0, widthRle, heightRle);
                        System.Drawing.Imaging.BitmapData rData = curBitmap.LockBits(rectRle, System.Drawing.Imaging.ImageLockMode.ReadWrite, curBitmap.PixelFormat);
                        IntPtr ptr = rData.Scan0;

                        System.Runtime.InteropServices.Marshal.Copy((byte[])rleData, 0, ptr, widthRle * heightRle);
                        curBitmap.UnlockBits(rData);
                        //关闭流
                        sr.Close();
                        fs.Close();
                        Invalidate();
                    }
                }
            }
        }

        private void lzw_Click(object sender, EventArgs e)
        {
            lzwCode lzwDC = new lzwCode();
            if (lzwDC.ShowDialog() == DialogResult.OK)
            {
                bool lzwDorC = lzwDC.GetDorC;
                if (lzwDorC == false)
                {
                    if (curBitmap == null)
                    {
                        MessageBox.Show("请先打开图像！");
                        return;
                    }

                    Rectangle rect = new Rectangle(0, 0, curBitmap.Width, curBitmap.Height);
                    System.Drawing.Imaging.BitmapData bmpData = curBitmap.LockBits(rect, System.Drawing.Imaging.ImageLockMode.ReadWrite, curBitmap.PixelFormat);
                    IntPtr ptr = bmpData.Scan0;
                    int bytes = curBitmap.Width * curBitmap.Height;
                    byte[] grayValues = new byte[bytes];
                    System.Runtime.InteropServices.Marshal.Copy(ptr, grayValues, 0, bytes);
                    curBitmap.UnlockBits(bmpData);

                    ArrayList codingData = new ArrayList();
                    Hashtable codingDic = new Hashtable();

                    //初始化
                    codingData.Clear();
                    codingDic.Clear();
                    for (int i = 0; i < 256; i++)
                        codingDic.Add(i.ToString() + "|", i.ToString());
                    int valueDic = 256;

                    string prefix, suffix, fullcode;
                    string prefixIndex = null;

                    prefix = grayValues[0].ToString() + "|";
                    for (int i = 1; i < bytes; i++)
                    {
                        suffix = grayValues[i].ToString() + "|";
                        fullcode = prefix + suffix;
                        int dicLength = codingDic.Count;

                        if (codingDic.ContainsKey(fullcode))
                        {
                            prefix += suffix;
                        }
                        else
                        {
                            prefixIndex = codingDic[prefix].ToString();
                            codingData.Add(prefixIndex);
                            codingDic.Add(fullcode, valueDic.ToString());
                            valueDic++;
                            prefix = suffix;
                        }

                    }
                    prefixIndex = codingDic[prefix].ToString();
                    codingData.Add(prefixIndex);

                    SaveFileDialog sf = new SaveFileDialog();
                    //设置标题
                    sf.Title = "保存文件";
                    //设置文件保存类型
                    sf.Filter = "自定义图像格式(*.zlzw)|*.zlzw";
                    //如果用户没有输入扩展名，自动追加后缀
                    sf.AddExtension = true;
                    //如果用户单击了保存按钮
                    if (sf.ShowDialog() == DialogResult.OK)
                    {
                        //实例化一个文件流
                        FileStream fs = new FileStream(sf.FileName, FileMode.Create);
                        //实例化一个BinaryWriter
                        BinaryWriter bw = new BinaryWriter(fs);

                        //开始写入
                        int dataLength = codingData.Count;
                        bw.Write("zlzw");
                        bw.Write("8b");
                        bw.Write(curBitmap.Height.ToString());
                        bw.Write(curBitmap.Width.ToString());
                        bw.Write(dataLength.ToString());

                        for (int i = 0; i < dataLength; i++)
                            bw.Write(codingData[i].ToString());

                        //清空缓冲区
                        bw.Flush();
                        //关闭流
                        bw.Close();
                        fs.Close();
                    }
                }
                else
                {
                    OpenFileDialog of = new OpenFileDialog();
                    //设置标题
                    of.Title = "打开文件";
                    //设置文件保存类型
                    of.Filter = "自定义图像格式(*.zlzw)|*.zlzw";
                    //如果用户单击了保存按钮
                    if (of.ShowDialog() == DialogResult.OK)
                    {
                        //实例化一个文件流
                        FileStream fs = new FileStream(of.FileName, FileMode.Open);
                        //实例化一个BinaryReader
                        BinaryReader br = new BinaryReader(fs);

                        //开始写入
                        ArrayList deCodingData = new ArrayList();
                        Hashtable deCodingDic = new Hashtable();
                        deCodingData.Clear();
                        deCodingDic.Clear();
                        for (int i = 0; i < 256; i++)
                            deCodingDic.Add(i.ToString(), i.ToString() + "|");
                        int valueDic = 256;

                        string prefix = null, suffix = null, fullcode = null;
                        string readData, readOldData;

                        br.BaseStream.Position = 0;
                        readData = br.ReadString();
                        if (readData != "zlzw")
                        {
                            MessageBox.Show("不是zlzw文件格式！");
                            return;
                        }
                        readData = br.ReadString();
                        if (readData != "8b")
                        {
                            MessageBox.Show("本实例只能处理8位灰度图像！");
                            return;
                        }

                        int heightLzw, widthLzw;
                        heightLzw = Convert.ToInt32(br.ReadString());
                        widthLzw = Convert.ToInt32(br.ReadString());
                        int dataL = Convert.ToInt32(br.ReadString());

                        readData = br.ReadString();
                        deCodingData.Add(deCodingDic[readData]);
                        readOldData = readData;
                        for (int i = 1; i < dataL; i++)
                        {
                            readData = br.ReadString();
                            if (deCodingDic.ContainsKey(readData))
                            {
                                deCodingData.Add(deCodingDic[readData]);
                                prefix = deCodingDic[readOldData].ToString();
                                string tempFix = deCodingDic[readData].ToString();
                                for (int j = 0; j < tempFix.Length; j++)
                                {
                                    if (tempFix[j] == '|')
                                    {
                                        suffix = tempFix.Substring(0, j + 1);
                                        break;
                                    }
                                }
                                fullcode = prefix + suffix;
                                deCodingDic.Add(valueDic.ToString(), fullcode);
                                valueDic++;
                            }
                            else
                            {
                                prefix = deCodingDic[readOldData].ToString();
                                for (int j = 0; j < prefix.Length; j++)
                                {
                                    if (prefix[j] == '|')
                                    {
                                        suffix = prefix.Substring(0, j + 1);
                                        break;
                                    }
                                }
                                fullcode = prefix + suffix;
                                deCodingData.Add(fullcode);
                                deCodingDic.Add(valueDic.ToString(), fullcode);
                                valueDic++;
                            }
                            readOldData = readData;
                        }

                        curBitmap = new Bitmap(widthLzw, heightLzw, System.Drawing.Imaging.PixelFormat.Format8bppIndexed);
                        System.Drawing.Imaging.ColorPalette cp = curBitmap.Palette;
                        for (int i = 0; i < 256; i++)
                        {
                            cp.Entries[i] = Color.FromArgb(i, i, i);
                        }
                        curBitmap.Palette = cp;

                        Rectangle rectLzw = new Rectangle(0, 0, widthLzw, heightLzw);
                        System.Drawing.Imaging.BitmapData lData = curBitmap.LockBits(rectLzw, System.Drawing.Imaging.ImageLockMode.ReadWrite, curBitmap.PixelFormat);
                        IntPtr ptr = lData.Scan0;
                        int bytesD = widthLzw * heightLzw;
                        byte[] grayLzw = new byte[bytesD];

                        int k = 0;
                        for (int i = 0; i < deCodingData.Count; i++)
                        {
                            string decodingStr = deCodingData[i].ToString();
                            int m = 0;
                            for (int j = 0; j < decodingStr.Length; j++)
                            {
                                if (decodingStr[j] == '|')
                                {
                                    int subL = j - m;
                                    grayLzw[k] = Convert.ToByte(decodingStr.Substring(m, subL));
                                    m = j + 1;
                                    k++;
                                }
                            }
                        }

                        System.Runtime.InteropServices.Marshal.Copy(grayLzw, 0, ptr, bytesD);
                        curBitmap.UnlockBits(lData);
                        //关闭流
                        br.Close();
                        fs.Close();
                        Invalidate();
                    }
                }
            }
        }

        private void dpcm_Click(object sender, EventArgs e)
        {
            dpcmCode dpcmDC = new dpcmCode();
            if (dpcmDC.ShowDialog() == DialogResult.OK)
            {
                bool dpcmDorC = dpcmDC.GetDorC;
                if (dpcmDorC == false)
                {
                    if (curBitmap == null)
                    {
                        MessageBox.Show("请先打开图像！");
                        return;
                    }

                    Rectangle rect = new Rectangle(0, 0, curBitmap.Width, curBitmap.Height);
                    System.Drawing.Imaging.BitmapData bmpData = curBitmap.LockBits(rect, System.Drawing.Imaging.ImageLockMode.ReadWrite, curBitmap.PixelFormat);
                    IntPtr ptr = bmpData.Scan0;
                    int bytes = curBitmap.Width * curBitmap.Height;
                    byte[] grayValues = new byte[bytes];
                    System.Runtime.InteropServices.Marshal.Copy(ptr, grayValues, 0, bytes);
                    curBitmap.UnlockBits(bmpData);

                    int[] dpcmData = new int[bytes];
                    byte methodD = dpcmDC.GetMethod;
                    int tempV = 0;
                    switch (methodD)
                    {
                        case 0:                            
                            for (int i = 0; i < curBitmap.Height; i++)
                            {
                                for (int j = 0; j < curBitmap.Width; j++)
                                {
                                    if (j == 0)
                                        tempV = 128;
                                    else
                                        tempV = grayValues[i * curBitmap.Width + j - 1];
                                    dpcmData[i * curBitmap.Width + j] = grayValues[i * curBitmap.Width + j] - tempV;
                                }
                            }
                            break;
                        case 1:
                            for (int i = 0; i < curBitmap.Height; i++)
                            {
                                if (i == 0)
                                {
                                    for (int j = 0; j < curBitmap.Width; j++)
                                    {
                                        if (j == 0)
                                            tempV = 128;
                                        else
                                            tempV = (grayValues[i * curBitmap.Width + j - 1] + 128) / 2;
                                        dpcmData[i * curBitmap.Width + j] = grayValues[i * curBitmap.Width + j] - tempV;
                                    }
                                }
                                else
                                {
                                    for (int j = 0; j < curBitmap.Width; j++)
                                    {
                                        if (j == 0)
                                            tempV = (128 + grayValues[(i - 1) * curBitmap.Width + j]) / 2;
                                        else
                                            tempV = (grayValues[(i - 1) * curBitmap.Width + j] + grayValues[i * curBitmap.Width + j - 1]) / 2;
                                        dpcmData[i * curBitmap.Width + j] = grayValues[i * curBitmap.Width + j] - tempV;
                                    }
                                }
                            }
                            break;
                        case 2:
                            for (int i = 0; i < curBitmap.Height; i++)
                            {
                                if (i == 0)
                                {
                                    for (int j = 0; j < curBitmap.Width; j++)
                                    {
                                        if (j == 0)
                                            tempV = 64;
                                        else
                                            tempV = grayValues[i * curBitmap.Width + j - 1] + 64;
                                        dpcmData[i * curBitmap.Width + j] = grayValues[i * curBitmap.Width + j] - tempV;
                                    }
                                }
                                else
                                {
                                    for (int j = 0; j < curBitmap.Width; j++)
                                    {
                                        if (j == 0)
                                            tempV = 64 + grayValues[(i - 1) * curBitmap.Width + j];
                                        else
                                            tempV = grayValues[(i - 1) * curBitmap.Width + j] + grayValues[i * curBitmap.Width + j - 1] - grayValues[(i - 1) * curBitmap.Width + j - 1];
                                        dpcmData[i * curBitmap.Width + j] = grayValues[i * curBitmap.Width + j] - tempV;
                                    }
                                }
                            }
                            break;
                        case 3:
                            for (int i = 0; i < curBitmap.Height; i++)
                            {
                                if (i == 0)
                                {
                                    for (int j = 0; j < curBitmap.Width; j++)
                                    {
                                        if (j == 0)
                                            tempV = 128;
                                        else
                                            tempV = (grayValues[i * curBitmap.Width + j - 1] + 128) / 2;
                                        dpcmData[i * curBitmap.Width + j] = grayValues[i * curBitmap.Width + j] - tempV;
                                    }
                                }
                                else
                                {
                                    for (int j = 0; j < curBitmap.Width; j++)
                                    {
                                        if (j == 0)
                                            tempV = (128 + grayValues[(i - 1) * curBitmap.Width + j + 1]) / 2;
                                        else if (j == curBitmap.Width - 1)
                                            tempV = (128 + grayValues[i * curBitmap.Width + j - 1]) / 2;
                                        else
                                            tempV = (grayValues[(i - 1) * curBitmap.Width + j + 1] + grayValues[i * curBitmap.Width + j - 1]) / 2;
                                        dpcmData[i * curBitmap.Width + j] = grayValues[i * curBitmap.Width + j] - tempV;
                                    }
                                }
                            }
                            break;
                        default:
                            break;
                    }
                    SaveFileDialog sf = new SaveFileDialog();
                    //设置标题
                    sf.Title = "保存文件";
                    //设置文件保存类型
                    sf.Filter = "自定义图像格式(*.zdpcm)|*.zdpcm";
                    //如果用户没有输入扩展名，自动追加后缀
                    sf.AddExtension = true;
                    //如果用户单击了保存按钮
                    if (sf.ShowDialog() == DialogResult.OK)
                    {
                        //实例化一个文件流
                        FileStream fs = new FileStream(sf.FileName, FileMode.Create);
                        //实例化一个BinaryWriter
                        BinaryWriter bw = new BinaryWriter(fs);

                        //开始写入
                        bw.Write("zdpcm");
                        bw.Write("8b");
                        bw.Write(methodD);
                        bw.Write(Convert.ToInt16(curBitmap.Height));
                        bw.Write(Convert.ToInt16(curBitmap.Width));
                        for (int i = 0; i < bytes; i++)
                            bw.Write(Convert.ToInt16(dpcmData[i]));

                        //清空缓冲区
                        bw.Flush();
                        //关闭流
                        bw.Close();
                        fs.Close();
                    }
                }
                else
                { 
                    OpenFileDialog of = new OpenFileDialog();
                    //设置标题
                    of.Title = "打开文件";
                    //设置文件保存类型
                    of.Filter = "自定义图像格式(*.zdpcm)|*.zdpcm";
                    //如果用户单击了保存按钮
                    if (of.ShowDialog() == DialogResult.OK)
                    {
                        //实例化一个文件流
                        FileStream fs = new FileStream(of.FileName, FileMode.Open);
                        //实例化一个BinaryReader
                        BinaryReader br = new BinaryReader(fs);

                        br.BaseStream.Position = 0;
                        string readData = br.ReadString();
                        if (readData != "zdpcm")
                        {
                            MessageBox.Show("不是zdpcm文件格式！");
                            return;
                        }
                        readData = br.ReadString();
                        if (readData != "8b")
                        {
                            MessageBox.Show("本实例只能处理8位灰度图像！");
                            return;
                        }
                        byte methodOFDpcm = br.ReadByte();
                        int heightDpcm = br.ReadInt16();
                        int widthDpcm = br.ReadInt16();
                        int bytesD = widthDpcm * heightDpcm;
                        byte[] grayDpcm = new byte[bytesD];
                        int[] dpcmDecode = new int[bytesD];
                        for (int i = 0; i < bytesD; i++)
                        {
                            dpcmDecode[i] = br.ReadInt16();
                        }

                        int tempDV = 0;
                        switch (methodOFDpcm)
                        {
                            case 0:
                                for (int i = 0; i < heightDpcm; i++)
                                {
                                    for (int j = 0; j < widthDpcm; j++)
                                    {
                                        if (j == 0)
                                            tempDV = 128;
                                        else
                                            tempDV = grayDpcm[i * widthDpcm + j - 1];
                                        grayDpcm[i * widthDpcm + j] = (byte)(dpcmDecode[i * widthDpcm + j] + tempDV);
                                    }
                                }
                                break;
                            case 1:
                                for (int i = 0; i < heightDpcm; i++)
                                {
                                    if (i == 0)
                                    {
                                        for (int j = 0; j < widthDpcm; j++)
                                        {
                                            if (j == 0)
                                                tempDV = 128;
                                            else
                                                tempDV = (grayDpcm[i * widthDpcm + j - 1] + 128) / 2;
                                            grayDpcm[i * widthDpcm + j] = (byte)(dpcmDecode[i * widthDpcm + j] + tempDV);
                                        }
                                    }
                                    else 
                                    {
                                        for (int j = 0; j < widthDpcm; j++)
                                        {
                                            if (j == 0)
                                                tempDV = (128 + grayDpcm[(i - 1) * widthDpcm + j]) / 2;
                                            else
                                                tempDV = (grayDpcm[i * widthDpcm + j - 1] + grayDpcm[(i - 1) * widthDpcm + j]) / 2;
                                            grayDpcm[i * widthDpcm + j] = (byte)(dpcmDecode[i * widthDpcm + j] + tempDV);
                                        }
                                    }
                                }
                                break;
                            case 2:
                                for (int i = 0; i < heightDpcm; i++)
                                {
                                    if (i == 0)
                                    {
                                        for (int j = 0; j < widthDpcm; j++)
                                        {
                                            if (j == 0)
                                                tempDV = 64;
                                            else
                                                tempDV = grayDpcm[i * widthDpcm + j - 1] + 64;
                                            grayDpcm[i * widthDpcm + j] = (byte)(dpcmDecode[i * widthDpcm + j] + tempDV);
                                        }
                                    }
                                    else
                                    {
                                        for (int j = 0; j < widthDpcm; j++)
                                        {
                                            if (j == 0)
                                                tempDV = 64 + grayDpcm[(i - 1) * widthDpcm + j];
                                            else
                                                tempDV = grayDpcm[i * widthDpcm + j - 1] + grayDpcm[(i - 1) * widthDpcm + j] - grayDpcm[(i - 1) * widthDpcm + j - 1];
                                            grayDpcm[i * widthDpcm + j] = (byte)(dpcmDecode[i * widthDpcm + j] + tempDV);
                                        }
                                    }
                                }
                                break;
                            case 3:
                                for (int i = 0; i < heightDpcm; i++)
                                {
                                    if (i == 0)
                                    {
                                        for (int j = 0; j < widthDpcm; j++)
                                        {
                                            if (j == 0)
                                                tempDV = 128;
                                            else
                                                tempDV = (grayDpcm[i * widthDpcm + j - 1] + 128) / 2;
                                            grayDpcm[i * widthDpcm + j] = (byte)(dpcmDecode[i * widthDpcm + j] + tempDV);
                                        }
                                    }
                                    else
                                    {
                                        for (int j = 0; j < widthDpcm; j++)
                                        {
                                            if (j == 0)
                                                tempDV = (128 + grayDpcm[(i - 1) * widthDpcm + j + 1]) / 2;
                                            else if (j == widthDpcm - 1)
                                                tempDV = (128 + grayDpcm[i * widthDpcm + j - 1]) / 2;
                                            else
                                                tempDV = (grayDpcm[i * widthDpcm + j - 1] + grayDpcm[(i - 1) * widthDpcm + j + 1]) / 2;
                                            grayDpcm[i * widthDpcm + j] = (byte)(dpcmDecode[i * widthDpcm + j] + tempDV);
                                        }
                                    }
                                }
                                break;
                            default:
                                break;
                        }

                        curBitmap = new Bitmap(widthDpcm, heightDpcm, System.Drawing.Imaging.PixelFormat.Format8bppIndexed);
                        System.Drawing.Imaging.ColorPalette cp = curBitmap.Palette;
                        for (int i = 0; i < 256; i++)
                        {
                            cp.Entries[i] = Color.FromArgb(i, i, i);
                        }
                        curBitmap.Palette = cp;

                        Rectangle rectDpcm = new Rectangle(0, 0, widthDpcm, heightDpcm);
                        System.Drawing.Imaging.BitmapData dData = curBitmap.LockBits(rectDpcm, System.Drawing.Imaging.ImageLockMode.ReadWrite, curBitmap.PixelFormat);
                        IntPtr ptr = dData.Scan0;
                        
                        System.Runtime.InteropServices.Marshal.Copy(grayDpcm, 0, ptr, bytesD);
                        curBitmap.UnlockBits(dData);
                        //关闭流
                        br.Close();
                        fs.Close();
                        Invalidate();
                    }
                }
            }
        }

        private void transform_Click(object sender, EventArgs e)
        {
            if (curBitmap != null)
            {
                transCode fftCode = new transCode();
                if (fftCode.ShowDialog() == DialogResult.OK)
                {
                    Rectangle rect = new Rectangle(0, 0, curBitmap.Width, curBitmap.Height);
                    System.Drawing.Imaging.BitmapData bmpData = curBitmap.LockBits(rect, System.Drawing.Imaging.ImageLockMode.ReadWrite, curBitmap.PixelFormat);
                    IntPtr ptr = bmpData.Scan0;
                    int bytes = curBitmap.Width * curBitmap.Height;
                    byte[] grayValues = new byte[bytes];
                    System.Runtime.InteropServices.Marshal.Copy(ptr, grayValues, 0, bytes);

                    byte sizeN = fftCode.GetSize;
                    byte ratioN = fftCode.GetRatio;

                    int sizeSI = sizeN * sizeN;
                    int col = curBitmap.Width / sizeN;
                    int row = curBitmap.Height / sizeN;
                    byte[,] splitImage = new byte[col * row, sizeSI];
                    int snum = sizeSI - sizeSI / ratioN;
                    int a, b, c, d;
                    for (int i = 0; i < curBitmap.Height; i++)
                    {
                        for (int j = 0; j < curBitmap.Width; j++)
                        {
                            a = j / sizeN;
                            b = j % sizeN;
                            c = i / sizeN;
                            d = i % sizeN;
                            splitImage[c * col + a, d * sizeN + b] = grayValues[i * curBitmap.Width + j];
                        }
                    }

                    Complex[] freDom = new Complex[sizeSI];
                    byte[] tempImage = new byte[sizeSI];
                    double[] temp1D = new double[sizeSI];

                    for (int i = 0; i < col * row; i++)
                    {
                        for (int j = 0; j < sizeSI; j++)
                            tempImage[j] = splitImage[i, j];
                        freDom = fft2(tempImage, sizeN, sizeN, false);
                        for (int j = 0; j < sizeSI; j++)
                            temp1D[j] = freDom[j].Abs();

                        double tempD = 0;

                        for (int m = 0; m < sizeSI - 1; m++)
                        {
                            for (int n = 0; n < sizeSI - 1 - m; n++)
                            {
                                if (temp1D[n] > temp1D[n + 1])
                                {
                                    tempD = temp1D[n];
                                    temp1D[n] = temp1D[n + 1];
                                    temp1D[n + 1] = tempD;
                                }
                            }
                        }
                        tempD = temp1D[snum - 1];
                        for (int j = 0; j < sizeSI; j++)
                        {
                            if (freDom[j].Abs() <= tempD)
                                freDom[j] = new Complex(0.0, 0.0);
                        }
                        tempImage = ifft2(freDom, sizeN, sizeN, false);
                        for (int j = 0; j < sizeSI; j++)
                            splitImage[i, j] = tempImage[j];
                    }

                    for (int i = 0; i < curBitmap.Height; i++)
                    {
                        for (int j = 0; j < curBitmap.Width; j++)
                        {
                            a = j / sizeN;
                            b = j % sizeN;
                            c = i / sizeN;
                            d = i % sizeN;
                            grayValues[i * curBitmap.Width + j] = splitImage[c * col + a, d * sizeN + b];
                        }
                    }

                    System.Runtime.InteropServices.Marshal.Copy(grayValues, 0, ptr, bytes);
                    curBitmap.UnlockBits(bmpData);
                    Invalidate();
                }
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

        private void wavelet_Click(object sender, EventArgs e)
        {
            if (curBitmap != null)
            {
                wlTrans waveletCode = new wlTrans();
                if (waveletCode.ShowDialog() == DialogResult.OK)
                {
                    Rectangle rect = new Rectangle(0, 0, curBitmap.Width, curBitmap.Height);
                    System.Drawing.Imaging.BitmapData bmpData = curBitmap.LockBits(rect, System.Drawing.Imaging.ImageLockMode.ReadWrite, curBitmap.PixelFormat);
                    IntPtr ptr = bmpData.Scan0;
                    int bytes = curBitmap.Width * curBitmap.Height;
                    byte[] grayValues = new byte[bytes];
                    System.Runtime.InteropServices.Marshal.Copy(ptr, grayValues, 0, bytes);

                    byte wlSeries = waveletCode.GetSeries;
                    byte waveletBase = waveletCode.GetBase;

                    double[] tempA = new double[bytes];
                    double[] tempB = new double[bytes];
                    for (int i = 0; i < bytes; i++)
                    {
                        tempA[i] = Convert.ToDouble(grayValues[i]);
                    }
                    double[] lowFilter = null;
                    double[] highFilter = null;

                    switch (waveletBase)
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
                            lowFilter = new double[] { 0.11154074335011, 0.49462389039845, 0.75113390802110, 0.31525035170920, -0.22626469396544, -0.12976686756726, 0.09750160558732, 0.02752286553031, -0.03158203931849, 0.00055384220116, 0.00477725751195, -0.00107730108531 };
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

                    for (int k = 0; k < wlSeries; k++)
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
                    }

                    int col = curBitmap.Width / (int)Math.Pow(2, wlSeries);
                    int row = curBitmap.Height / (int)Math.Pow(2, wlSeries);
                    for (int i = 0; i < curBitmap.Height; i++)
                    {
                        for (int j = 0; j < curBitmap.Width; j++)
                        {
                            if (i >= row || j >= col)
                                tempA[i * curBitmap.Width + j] = 0.0;
                        }
                    }

                    for (int k = wlSeries - 1; k >= 0; k--)
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
                    Invalidate();
                }
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

        private void wavelet2D(ref double[] dataImage, double[] p, double[] q, int series)
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
    }
}