using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;

namespace gray
{
    public partial class Form1 : Form
    {
        private HiPerfTimer myTimer;
        public Form1()
        {
            InitializeComponent();
            myTimer = new gray.HiPerfTimer();
        }

        //文件名
        private string curFileName;
        //图像对象
        private Bitmap curBitmap;

        /// <summary>
        /// 打开图像文件
        /// </summary>
        private void open_Click(object sender, EventArgs e)
        {
            //创建OpenFileDialog
            OpenFileDialog opnDlg = new OpenFileDialog();
            //为图像选择一个筛选器
            opnDlg.Filter = "所有图像文件|*.bmp;*.pcx;*.png;*.jpg;*.gif;" +
                "*.tif;*.ico;*.dxf;*.cgm;*.cdr;*.wmf;*.eps;*.emf|" +
                "位图(*.bmp;*.jpg;*.png;...)|*.bmp;*.pcx;*.png;*.jpg;*.gif;*.tif;*.ico|" +
                "矢量图(*.wmf;*.eps;*.emf;...)|*.dxf;*.cgm;*.cdr;*.wmf;*.eps;*.emf";
            //设置对话框标题
            opnDlg.Title = "打开图像文件";
            //启用“帮助”按钮
            opnDlg.ShowHelp = true;

            //如果结果为“打开”，选定文件
            if(opnDlg.ShowDialog()==DialogResult.OK)
            {
                //读取当前选中的文件名
                curFileName = opnDlg.FileName;
                //使用Image.FromFile创建图像对象
                try
                {
                    curBitmap = (Bitmap)Image.FromFile(curFileName);
                }
                catch(Exception exp)
                {
                    MessageBox.Show(exp.Message);
                }
            }
            //对窗体进行重新绘制，这将强制执行paint事件处理程序
            Invalidate();
        }

        //在控件需要重新绘制时发生
        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            //获取Graphics对象
            Graphics g = e.Graphics;
            if (curBitmap != null)
            {
                //使用DrawImage方法绘制图像
                //160,20：显示在主窗体内，图像左上角的坐标
                //curBitmap.Width, curBitmap.Height图像的宽度和高度
                g.DrawImage(curBitmap, 160, 20, curBitmap.Width, curBitmap.Height);
            }
        }

        /// <summary>
        /// 保存图像文件
        /// </summary>
        private void save_Click(object sender, EventArgs e)
        {
            //如果没有创建图像，则退出
            if (curBitmap == null)
                return;

            //调用SaveFileDialog
            SaveFileDialog saveDlg = new SaveFileDialog();
            //设置对话框标题
            saveDlg.Title = "保存为";
            //改写已存在文件时提示用户
                saveDlg.OverwritePrompt = true;
            //为图像选择一个筛选器
                saveDlg.Filter = "BMP文件(*.bmp)|*.bmp|" + "Gif文件(*.gif)|*.gif|" + "JPEG文件(*.jpg)|*.jpg|" + "PNG文件(*.png)|*.png";
            //启用“帮助”按钮
                saveDlg.ShowHelp = true;

            //如果选择了格式，则保存图像
            if (saveDlg.ShowDialog() == DialogResult.OK)
            {
                //获取用户选择的文件名
                string filename = saveDlg.FileName;
                string strFilExtn = filename.Remove(0, filename.Length - 3);

                //保存文件
                switch (strFilExtn)
                {
                    //以指定格式保存
                    case "bmp":
                        curBitmap.Save(filename, System.Drawing.Imaging.ImageFormat.Bmp);
                        break;
                    case "jpg":
                        curBitmap.Save(filename, System.Drawing.Imaging.ImageFormat.Jpeg);
                        break;
                    case "gif":
                        curBitmap.Save(filename, System.Drawing.Imaging.ImageFormat.Gif);
                        break;
                        case "tif":
                        curBitmap.Save(filename, System.Drawing.Imaging.ImageFormat.Tiff);
                        break;
                    case "png":
                        curBitmap.Save(filename, System.Drawing.Imaging.ImageFormat.Png);
                        break;
                    default:
                        break;
                }
            }
        }

        //关闭窗体 
        private void close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// 提取像素法
        /// </summary>
        private void pixel_Click(object sender, EventArgs e)
        {
            //启动计时器
            myTimer.Start();
            if (curBitmap != null)
            {
                Color curColor;
                int ret;

                //二维图像数组循环
                for(int i = 0; i < curBitmap.Width; i++)
                {
                    for(int j = 0; j < curBitmap.Height; j++)
                    {
                        //获取该像素点的RGB颜色值
                        curColor = curBitmap.GetPixel(i, j);
                        //利用公式计算灰度值
                        ret = (int)(curColor.R * 0.299 + curColor.G * 0.587 + curColor.B * 0.114);
                        //设置该像素点的灰度值，R=G=B=ret
                        curBitmap.SetPixel(i, j, Color.FromArgb(ret, ret, ret));
                    }
                }
                //关闭计时器
                myTimer.Stop();
                //在TextBox内显示计时时间
                timeBox.Text = myTimer.Duration.ToString("####.##") + "毫秒";
                //对窗体进行重新绘制，这将强制执行Paint事件处理程序
                Invalidate();
            }
        }

        /// <summary>
        /// 内存法(适用于任意大小的24位彩色图像)
        /// </summary>
        private void memory_Click(object sender, EventArgs e)
        {
            //启动计时器
            myTimer.Start();
            if (curBitmap != null)
            {
                //位图矩形
                Rectangle rect = new Rectangle(0, 0, curBitmap.Width, curBitmap.Height);
                //以可读写的方式锁定全部位图像素
                System.Drawing.Imaging.BitmapData bmpData = curBitmap.LockBits(rect, System.Drawing.Imaging.ImageLockMode.ReadWrite, curBitmap.PixelFormat);
                //得到首地址
                IntPtr ptr = bmpData.Scan0;

                //定义被锁定的数组大小，由位图数据与未用空间组成的
                int bytes = bmpData.Stride * bmpData.Height;
                //定义位图数组
                byte[] rgbValues = new byte[bytes];
                //复制被锁定的位图像素值到该数组内
                System.Runtime.InteropServices.Marshal.Copy(ptr, rgbValues, 0, bytes);

                //灰度化
                double colorTemp = 0;
                for (int i = 0; i < bmpData.Height; i++)
                {
                    //只处理每行中是图像像素的数据，舍弃未用空间
                    for (int j = 0; j < bmpData.Width * 3; j += 3)
                    {
                        //利用公式计算灰度值
                        colorTemp = rgbValues[i * bmpData.Stride + j + 2] * 0.299 + rgbValues[i * bmpData.Stride + j + 1] * 0.587 + rgbValues[i * bmpData.Stride + j] * 0.114;
                        //R=G=B
                        rgbValues[i * bmpData.Stride + j] = rgbValues[i * bmpData.Stride + j + 1] = rgbValues[i * bmpData.Stride + j + 2] = (byte)colorTemp;
                    }
                }

                //把数组复制回位图
                System.Runtime.InteropServices.Marshal.Copy(rgbValues, 0, ptr, bytes);
                //解锁位图像素
                curBitmap.UnlockBits(bmpData);
                //关闭计时器
                myTimer.Stop();
                //在TextBox内显示计时时间
                timeBox.Text = myTimer.Duration.ToString("####.##") + "毫秒";
                //对窗体进行重新绘制，这将强制执行Paint事件处理程序
                Invalidate();
            }
        }

        /// <summary>
        /// 指针法
        /// </summary>
        private void pointer_Click(object sender, EventArgs e)
        {
            //启动计时器
            myTimer.Start();
            if (curBitmap != null)
            {
                //位图矩形
                Rectangle rect = new Rectangle(0, 0, curBitmap.Width, curBitmap.Height);
                //以可读写的方式锁定全部位图像素
                System.Drawing.Imaging.BitmapData bmpData = curBitmap.LockBits(rect, System.Drawing.Imaging.ImageLockMode.ReadWrite, curBitmap.PixelFormat);

                byte temp = 0;
                //启用不安全模式
                unsafe
                {
                    //得到首地址
                    byte* ptr = (byte*)(bmpData.Scan0);
                    //二维图像循环
                    for (int i = 0; i < bmpData.Height; i++)
                    {
                        for (int j = 0; j < bmpData.Width; j++)
                        {
                            //利用公式计算灰度值
                            temp = (byte)(0.299 * ptr[2] + 0.587 * ptr[1] + 0.114 * ptr[0]);
                            //R=G=B
                            ptr[0] = ptr[1] = ptr[2] = temp;
                            //指向下一个像素
                            ptr += 3;
                        }
                        //指向下一行数组的首个字节
                        ptr += bmpData.Stride - bmpData.Width * 3;
                    }
                }
                //解锁位图像素
                curBitmap.UnlockBits(bmpData);
                //关闭计时器
                myTimer.Stop();
                //在TextBox内显示计时时间
                timeBox.Text = myTimer.Duration.ToString("####.##") + "毫秒";
                //对窗体进行重新绘制，这将强制执行Paint事件处理程序
                Invalidate();
            }
        }

        /// <summary>
        /// 内存法(仅适用于512*512的图像)
        /// </summary>
        //private void memory_Click(object sender, EventArgs e)
        //{
        //    if (curBitmap != null)
        //    {
        //        //位图矩形
        //        Rectangle rect = new Rectangle(0, 0, curBitmap.Width, curBitmap.Height);
        //        //以可读写的方式锁定全部位图像素
        //        System.Drawing.Imaging.BitmapData bmpData = curBitmap.LockBits(rect, System.Drawing.Imaging.ImageLockMode.ReadWrite, curBitmap.PixelFormat);
        //        //得到首地址
        //        IntPtr ptr = bmpData.Scan0;

        //        //24位bmp位图字节数
        //        int bytes = curBitmap.Width * curBitmap.Height * 3;
        //        //定义位图数组
        //        byte[] rgbValues = new byte[bytes];
        //        //复制被锁定的位图像素值到该数组内
        //        System.Runtime.InteropServices.Marshal.Copy(ptr, rgbValues, 0, bytes);

        //        //灰度化
        //        double colorTemp = 0;
        //        for(int i = 0; i < rgbValues.Length; i += 3)
        //        {
        //            //利用公式计算灰度值
        //            colorTemp = rgbValues[i + 2] * 0.299 + rgbValues[i + 1] * 0.587 + rgbValues[i] * 0.114;
        //            //R=G=B
        //            rgbValues[i]=rgbValues[i+1]=rgbValues[i+2]=(byte)colorTemp;
        //        }

        //        //把数组复制回位图
        //        System.Runtime.InteropServices.Marshal.Copy(rgbValues, 0, ptr, bytes);
        //        //解锁位图像素
        //        curBitmap.UnlockBits(bmpData);
        //        //对窗体进行重新绘制，这将强制执行Paint事件处理程序
        //        Invalidate();
        //    }
        //}


    }
}
