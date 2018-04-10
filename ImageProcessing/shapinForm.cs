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

namespace ImageProcessing
{
    public partial class shapinForm : Form
    {
        private string shapingFileName;
        private Bitmap shapingBitmap;
        private int[] shapingPixel;
        private double[] cumHist;
        private int shapingSize;
        private int maxPixel;
        public shapinForm()
        {
            InitializeComponent();
            shapingPixel = new int[256];
            cumHist = new double[256];
        }

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
                shapingFileName = opnDlg.FileName;
                //使用Image.FromFile创建图像对象
                try
                {
                    shapingBitmap = (Bitmap)Image.FromFile(shapingFileName);
                }
                catch (Exception exp)
                {
                    MessageBox.Show(exp.Message);
                }

                //计算各个灰度级所具有的像素个数
                Rectangle rect = new Rectangle(0, 0, shapingBitmap.Width, shapingBitmap.Height);
                BitmapData bmpData = shapingBitmap.LockBits(rect,
                    ImageLockMode.ReadWrite, shapingBitmap.PixelFormat);
                IntPtr ptr = bmpData.Scan0;
                shapingSize = shapingBitmap.Width * shapingBitmap.Height;
                byte[] grayValues = new byte[shapingSize];
                Marshal.Copy(ptr, grayValues, 0, shapingSize);//灰度值数据存入grayValues中

                byte temp = 0;
                maxPixel = 0;
                //灰度等级数组清零
                Array.Clear(shapingPixel, 0, 256);
                //计算各个灰度级的像素个数
                for (int i = 0; i < shapingSize; i++)
                {
                    //灰度级
                    temp = grayValues[i];
                    //计数加1
                    shapingPixel[temp]++;
                    if (shapingPixel[temp] > maxPixel)
                    {
                        //找到灰度频率最大的像素数，用于绘制直方图
                        maxPixel = shapingPixel[temp];
                    }
                }

                //解锁
                Marshal.Copy(grayValues, 0, ptr, shapingSize);
                shapingBitmap.UnlockBits(bmpData);
            }
            Invalidate();
        }

        private void startShaping_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }

        private void close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// 绘制直方图
        /// </summary>
        private void shapinForm_Paint(object sender, PaintEventArgs e)
        {
            if (shapingBitmap != null)
            {

                //获取Graphics对象
                Graphics g = e.Graphics;

                //创建一个宽度为1的黑色钢笔
                Pen curPen = new Pen(Brushes.Black, 1);

                //绘制坐标轴
                g.DrawLine(curPen, 50, 240, 320, 240);//横坐标
                g.DrawLine(curPen, 50, 240, 50, 30);//纵坐标

                //绘制并标识坐标刻度
                g.DrawLine(curPen, 100, 240, 100, 242);
                g.DrawLine(curPen, 150, 240, 150, 242);
                g.DrawLine(curPen, 200, 240, 200, 242);
                g.DrawLine(curPen, 250, 240, 250, 242);
                g.DrawLine(curPen, 300, 240, 300, 242);
                g.DrawString("0", new Font("New Timer", 8), Brushes.Black, new PointF(46, 242));
                g.DrawString("50", new Font("New Timer", 8), Brushes.Black, new PointF(92, 242));
                g.DrawString("100", new Font("New Timer", 8), Brushes.Black, new PointF(139, 242));
                g.DrawString("150", new Font("New Timer", 8), Brushes.Black, new PointF(189, 242));
                g.DrawString("200", new Font("New Timer", 8), Brushes.Black, new PointF(239, 242));
                g.DrawString("250", new Font("New Timer", 8), Brushes.Black, new PointF(289, 242));
                g.DrawLine(curPen, 48, 40, 50, 40);
                g.DrawString("0", new Font("New Timer", 8), Brushes.Black, new PointF(34, 234));
                g.DrawString(maxPixel.ToString(), new Font("New Timer", 8), Brushes.Black, new PointF(18, 34));

                //绘制直方图
                double temp = 0;
                int[] tempArray = new int[256];
                for (int i = 0; i < 256; i++)
                {
                    //纵坐标长度
                    temp = 200.0 * (double)shapingPixel[i] / (double)maxPixel;
                    g.DrawLine(curPen, 50 + i, 240, 50 + i, 240 - (int)temp);

                    //计算各个图像分量的累计概率分布
                    if (i != 0)
                    {
                        tempArray[i] = tempArray[i - 1] + shapingPixel[i];
                    }
                    else
                    {
                        tempArray[0] = shapingPixel[0];
                    }
                    cumHist[i] = (double)tempArray[i] / (double)shapingSize;
                }

                //释放对象
                curPen.Dispose();
            }
        }
    

        public double[] ApplicationP
        {
            get
            {
                return cumHist;
            }
        }

        public Bitmap GetmatchingBmp
        {
            get
            {
                return shapingBitmap;
            }
        }


    }
}
