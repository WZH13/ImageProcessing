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

namespace changepixel2
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
        //类对象
        PointBitmap pointbmp;
        //点坐标
        int x = 0;
        int y = 0;



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
                    //实例化PointBitmap类
                    pointbmp = new PointBitmap(curBitmap);
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
        
        private void change_Click(object sender, EventArgs e)
        {
            Color clr =Color.FromArgb(Convert.ToInt32(pixeltxbR.Text), Convert.ToInt32(pixeltxbG.Text), Convert.ToInt32(pixeltxbB.Text));
            pointbmp.SetPixel(x, y, clr);
            Invalidate();
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

        private void close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// 双击获取坐标、像素值
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
                    x = formPoint.X - 180;
                    y = formPoint.Y - 20;

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
    }
}
