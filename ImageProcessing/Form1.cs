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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            //this.skinEngine1 = new Sunisoft.IrisSkin.SkinEngine(this);
            //this.skinEngine1.SkinFile = Application.StartupPath + "\\Skins\\MSN.ssk";
            skinEngine1.SkinFile = Environment.CurrentDirectory + "\\Skins\\MSN.ssk";  //选择皮肤文件MidsummerColor1  MSN   
            //btn_open.Tag = 9999;//设置不需要被渲染的控件Tag值为9999
        }
        //1.声明自适应类实例  
        AutoSizeFormClass asc = new AutoSizeFormClass();

        //文件名
        private string curFileName;
        //图像对象
        private Bitmap curBitmap;
        //点坐标
        int x = 0;
        int y = 0;
        //类对象
        PointBitmap pointbmp;
        ImageProcessing imageProcessing;

        private void btn_open_Click(object sender, EventArgs e)
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
                    //实例化类
                    pointbmp = new PointBitmap(curBitmap);
                    imageProcessing= new ImageProcessing(curBitmap);
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
                g.DrawImage(curBitmap, 20, 99, curBitmap.Width, curBitmap.Height);
            }
        }

        private void btn_restore_Click(object sender, EventArgs e)
        {
            try
            {
                curBitmap = (Bitmap)Image.FromFile(curFileName);
                //实例化类
                pointbmp = new PointBitmap(curBitmap);
                imageProcessing = new ImageProcessing(curBitmap);
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message);
            }
            Invalidate();
        }

        private void btn_save_Click(object sender, EventArgs e)
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

        /// <summary>
        /// 更改任意一点的像素值
        /// </summary>
        private void btn_change_Click(object sender, EventArgs e)
        {
            try {
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
                    byte colorIndex = pixelValues[x + y * curBitmap.Width];//该点在调色板上的索引位置
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
                    pixelValues[x * 3 + y * curBitmap.Width * 3] = changePixelB;
                    pixelValues[x * 3 + y * curBitmap.Width * 3 + 1] = changePixelG;
                    pixelValues[x * 3 + y * curBitmap.Width * 3 + 2] = changePixelR;
                }
                Marshal.Copy(pixelValues, 0, ptr, bytes);
                curBitmap.UnlockBits(bmpData);
                Invalidate();
            }
            catch (NullReferenceException)
            {
                MessageBox.Show("请选择图片");
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message);
            }
        }

        /// <summary>
        /// 双击获取坐标、像素值
        /// </summary>
        private void Form1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (curBitmap != null)
            {
                Point formPoint = this.PointToClient(Control.MousePosition);

                if (formPoint.X < 20 || formPoint.X > (20 + curBitmap.Width) || formPoint.Y < 99 || formPoint.Y > (99 + curBitmap.Height))
                {
                    MessageBox.Show("请选择图片上的点");
                }
                else
                {
                    x = formPoint.X - 20;
                    y = formPoint.Y - 99;

                    //坐标显示
                    xtxb.Text = x.ToString();
                    ytxb.Text = y.ToString();

                    //锁定Bitmap，通过Pixel访问颜色
                    pointbmp.LockBits();

                    //获取颜色
                    Color color = pointbmp.GetPixel(x, y);

                    //像素显示
                    pixeltxbR.Text = color.R.ToString();
                    pixeltxbG.Text = color.G.ToString();
                    pixeltxbB.Text = color.B.ToString();


                    //从内存解锁Bitmap
                    pointbmp.UnlockBits();
                }
            }
        }

        private void btn_gray_Click(object sender, EventArgs e)
        {
            try
            {
                curBitmap = imageProcessing.RgbToGrayScale(curBitmap);
                Invalidate();
            }
            catch (NullReferenceException)
            {
                MessageBox.Show("请选择图片");
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message);
            }
        }

        private void btn_histogram_Click(object sender, EventArgs e)
        {
            if (curBitmap != null)
            {
                //定义并实例化新窗体，并把图像数据传递给它
                histForm histoGram = new histForm(curBitmap);
                histoGram.ShowDialog();
                Invalidate();
            }
            else
            {
                MessageBox.Show("请选择图片");
            }
        }

        private void btn_binaryzation_Click(object sender, EventArgs e)
        {

            try
            {
                curBitmap = imageProcessing.OtsuThreshold();
                Invalidate();
            }
            catch (NullReferenceException)
            {   
                MessageBox.Show("请选择图片");
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message);
            }
        }

        private void btn_linearPO_Click(object sender, EventArgs e)
        {
            try
            {
                //实例化linearPOForm窗体
                linearPOForm linearForm = new linearPOForm();
                //点击确定按钮
                if (linearForm.ShowDialog() == DialogResult.OK)
                {
                    //得到斜率
                    double scaling = Convert.ToDouble(linearForm.GetScaling);
                    //得到偏移量
                    double offset = Convert.ToDouble(linearForm.GetOffset);
                    imageProcessing.linearPO(scaling, offset);
                }
                Invalidate();
            }
            catch (NullReferenceException exp)
            {
                MessageBox.Show("请选择图片");
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message);
            }
            }

        private void btn_stretch_Click(object sender, EventArgs e)
        {
            try
            {
                imageProcessing.Stretch(curBitmap, out curBitmap);
                Invalidate();
            }
            catch (NullReferenceException exp)
            {
                MessageBox.Show("请选择图片");
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message);
            }
        }

        private void btn_equalization_Click(object sender, EventArgs e)
        {
            try
            {
                imageProcessing.Balance(curBitmap, out curBitmap);
                Invalidate();
            }
            catch (NullReferenceException exp)
            {
                MessageBox.Show("请选择图片");
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message);
            }
        }

        private void btn_shaping_Click(object sender, EventArgs e)
        {
            try
            {

                shapinForm sForm = new shapinForm();

                if (sForm.ShowDialog() == DialogResult.OK)
                {
                    imageProcessing.HistogramMatching(curBitmap, sForm.GetmatchingBmp, out curBitmap);
                }
                Invalidate();
            }
            catch (NullReferenceException exp)
            {
                MessageBox.Show("请选择图片");
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message);
            }
        }

        private void btn_exit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        #region 窗体自适应

        //2. 为窗体添加Load事件，并在其方法Form1_Load中，调用类的初始化方法，记录窗体和其控件的初始位置和大小  
        private void Form1_Load(object sender, EventArgs e)
        {
            asc.controllInitializeSize(this);
        }

        //3.为窗体添加SizeChanged事件，并在其方法Form1_SizeChanged中，调用类的自适应方法，完成自适应  
        private void Form1_SizeChanged(object sender, EventArgs e)
        {
            asc.controlAutoSize(this);
        }

        #endregion
    }
}
