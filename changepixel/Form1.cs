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

namespace changepixel
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
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
            if (opnDlg.ShowDialog() == DialogResult.OK)
            {
                //读取当前选中的文件名
                curFileName = opnDlg.FileName;
                //使用Image.FromFile创建图像对象
                try
                {
                    curBitmap = (Bitmap)Image.FromFile(curFileName);
                }
                catch (Exception exp)
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
                //180,20：显示在主窗体内，图像左上角的坐标
                //curBitmap.Width, curBitmap.Height图像的宽度和高度
                g.DrawImage(curBitmap, 180, 20, curBitmap.Width, curBitmap.Height);
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
                        curBitmap.Save(filename, ImageFormat.Bmp);
                        break;
                    case "jpg":
                        curBitmap.Save(filename, ImageFormat.Jpeg);
                        break;
                    case "gif":
                        curBitmap.Save(filename, ImageFormat.Gif);
                        break;
                    case "tif":
                        curBitmap.Save(filename, ImageFormat.Tiff);
                        break;
                    case "png":
                        curBitmap.Save(filename, ImageFormat.Png);
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
        /// 更改任意一点的像素值
        /// </summary>
        private void change_Click(object sender, EventArgs e)
        {
            if (curBitmap != null)
            {
                Rectangle rect = new Rectangle(0, 0, curBitmap.Width, curBitmap.Height);
                BitmapData bmpData = curBitmap.LockBits(rect, ImageLockMode.ReadWrite, curBitmap.PixelFormat);
                IntPtr ptr = bmpData.Scan0;
                int bytes = 0;
                //判断是8位图像还是24位图像，给相应的大小
                if (curBitmap.PixelFormat == PixelFormat.Format8bppIndexed)
                {
                    bytes = curBitmap.Width * curBitmap.Height;
                }
                else if (curBitmap.PixelFormat == PixelFormat.Format24bppRgb)
                {
                    bytes = curBitmap.Width * curBitmap.Height * 3;
                }
                byte[] pixelValues = new byte[bytes];
                Marshal.Copy(ptr, pixelValues, 0, bytes);

                int x = 0;
                int y = 0;
                byte changePixelR = 0;
                byte changePixelG = 0;
                byte changePixelB = 0;
                x = Convert.ToInt32(xtxb.Text);
                y = Convert.ToInt32(ytxb.Text);
                changePixelR = Convert.ToByte(pixeltxbR.Text);
                changePixelG = Convert.ToByte(pixeltxbG.Text);
                changePixelB = Convert.ToByte(pixeltxbB.Text);
                if (curBitmap.PixelFormat == PixelFormat.Format8bppIndexed)
                {
                    ColorPalette colorPalette = curBitmap.Palette;
                    byte colorIndex=pixelValues[x + y * curBitmap.Width];//该点在调色板上的索引位置
                    bool isHaveColor = false;//指示调色板是否已经有要改的颜色
                    for (int j = 0; j < colorPalette.Entries.Length; j++)
                    {
                        if (colorPalette.Entries[j].R == changePixelR && colorPalette.Entries[j].G == changePixelG && colorPalette.Entries[j].B == changePixelB)
                        {
                            pixelValues[x + y * curBitmap.Width] = Convert.ToByte(j);
                            isHaveColor = true;
                        }
                    }

                    if (!isHaveColor)
                    {
                        colorPalette.Entries[colorIndex] = Color.FromArgb(changePixelR, changePixelG, changePixelB);//设置调色板（更改了调色板这一位置的值）
                        for (int i = 0; i < pixelValues.Length; i++)
                        {//如果有其他像素与要更改的点是一索引值，则更改这个点使用它上一行的索引值
                            if (i != (x + y * curBitmap.Width))
                            {//除去需要更改的点，不改其索引值
                                if (pixelValues[i] == colorIndex)
                                {//与要更改的点的索引值相等
                                    if (i > curBitmap.Width)
                                    {//判断i - curBitmap.Width是否合法
                                        pixelValues[i] = pixelValues[i - curBitmap.Width];
                                    }
                                    else
                                    {
                                        pixelValues[i] = pixelValues[i + curBitmap.Width];
                                    }
                                }
                            }
                        }
                            curBitmap.Palette = colorPalette;
                    }
                }
                else if (curBitmap.PixelFormat == PixelFormat.Format24bppRgb)
                {
                    pixelValues[x*3 + y * curBitmap.Width * 3] = changePixelB;
                    pixelValues[x*3 + y * curBitmap.Width * 3+1] = changePixelG;
                    pixelValues[x*3 + y * curBitmap.Width * 3+2] = changePixelR; 
                }
                Marshal.Copy(pixelValues, 0, ptr, bytes);
                curBitmap.UnlockBits(bmpData);
                Invalidate();
            }
        }

        /// <summary>
        /// 灰度化图像
        /// </summary>
        private void gray_Click(object sender, EventArgs e)
        {
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
                Marshal.Copy(ptr, rgbValues, 0, bytes);

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
                Marshal.Copy(rgbValues, 0, ptr, bytes);
                //解锁位图像素
                curBitmap.UnlockBits(bmpData);
                //对窗体进行重新绘制，这将强制执行Paint事件处理程序
                Invalidate();
            }
        }

        /// <summary>
        /// 双击获取坐标
        /// </summary>
        private void Form1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (curBitmap != null)
            {
                Point formPoint = this.PointToClient(Control.MousePosition);

                if (formPoint.X < 180 || formPoint.X > (180 + curBitmap.Width) || formPoint.Y < 20 || formPoint.Y > (20 + curBitmap.Height))
                {
                    MessageBox.Show("请选择图片上的点");
                }
                else
                {
                    xtxb.Text = (formPoint.X - 180).ToString();
                    ytxb.Text = (formPoint.Y - 20).ToString();
                }
            }
        }
    }
}
