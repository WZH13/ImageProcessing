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

namespace geometric
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
        /// 图像平移
        /// </summary>
        private void translation_Click(object sender, EventArgs e)
        {
            if (curBitmap!=null)
            {
                translation traForm = new translation();
                if (traForm.ShowDialog()==DialogResult.OK)
                {
                    Rectangle rect = new Rectangle(0, 0, curBitmap.Width, curBitmap.Height);
                    BitmapData bmpData = curBitmap.LockBits(rect, ImageLockMode.ReadWrite, curBitmap.PixelFormat);
                    IntPtr ptr = bmpData.Scan0;
                    int bytes = bmpData.Stride * bmpData.Height;
                    byte[] grayValues = new byte[bytes];
                    Marshal.Copy(ptr, grayValues, 0, bytes);

                    //得到两个方向的图像平移量
                    int x = Convert.ToInt32(traForm.GetXOFFset);
                    int y = Convert.ToInt32(traForm.GetYOffset);

                    byte[] tempArray = new byte[bytes];
                    //临时初始化为白色（255）像素
                    for (int i = 0; i < bytes; i++)
                    {
                        tempArray[i] = 255;
                    }
                    
                    for (int j = 0; j < curBitmap.Height; j++)
                    {//保证纵向平移不出界
                        if ((j + y) < curBitmap.Height && (j + y) > 0)
                        {
                            for (int i = 0; i < curBitmap.Width * 3; i += 3)
                            {
                                if ((i + x * 3) < curBitmap.Width * 3 && (i + x * 3) > 0)
                                {//保证横向平移不出界
                                    tempArray[(i + x * 3) + 0 + (j + y) * bmpData.Stride] = grayValues[i + 0 + j * bmpData.Stride];
                                    tempArray[i + x * 3 + 1 + (j + y) * bmpData.Stride] = grayValues[i + 1 + j * bmpData.Stride];
                                    tempArray[i + x * 3 + 2 + (j + y) * bmpData.Stride] = grayValues[i + 2 + j * bmpData.Stride];
                                }
                            }
                        }
                    }


                    //数组复制，返回平移图像
                    grayValues = (byte[])tempArray.Clone();
                    Marshal.Copy(grayValues, 0, ptr, bytes);
                    curBitmap.UnlockBits(bmpData);
                }
                Invalidate();
            }
        }

        /// <summary>
        /// 图像镜像
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mirror_Click(object sender, EventArgs e)
        {
            if (curBitmap!=null)
            {
                mirror mirForm = new mirror();
                if (mirForm.ShowDialog()==DialogResult.OK)
                {
                    Rectangle rect = new Rectangle(0, 0, curBitmap.Width, curBitmap.Height);
                    BitmapData bmpData = curBitmap.LockBits(rect, ImageLockMode.ReadWrite, curBitmap.PixelFormat);
                    IntPtr ptr = bmpData.Scan0;
                    int bytes = 0;
                    ////判断是灰度色图像还是彩色图像，给相应的大小
                    if (curBitmap.PixelFormat==PixelFormat.Format8bppIndexed)
                    {
                        bytes= curBitmap.Width * curBitmap.Height;
                    }
                    else if (curBitmap.PixelFormat == PixelFormat.Format24bppRgb)
                    {
                        bytes = curBitmap.Width * curBitmap.Height * 3;
                    }
                    byte[] pixelValues = new byte[bytes];
                    Marshal.Copy(ptr, pixelValues, 0, bytes);

                    //水平中轴
                    int halfWidth = curBitmap.Width / 2;
                    //垂直中轴
                    int halfHeight = curBitmap.Height / 2;
                    byte temp;
                    byte temp1;
                    byte temp2;
                    //byte temp3;
                    if (curBitmap.PixelFormat == PixelFormat.Format8bppIndexed)
                    {
                        if (mirForm.GetMirror)
                        {
                            for (int i = 0; i < curBitmap.Height; i++)
                                for (int j = 0; j < halfWidth; j++)
                                {
                                    temp = pixelValues[i * curBitmap.Width + j];
                                    pixelValues[i * curBitmap.Width + j] = pixelValues[(i + 1) * curBitmap.Width - j - 1];
                                    pixelValues[(i + 1) * curBitmap.Width - j - 1] = temp;
                                }
                        }
                        else
                        {
                            for (int j = 0; j < curBitmap.Width; j++)
                            {
                                for (int i = 0; i < halfHeight; i++)
                                {
                                    temp = pixelValues[i * curBitmap.Width + j];
                                    pixelValues[i * curBitmap.Width + j] = pixelValues[(curBitmap.Height - i - 1) * curBitmap.Width + j];
                                    pixelValues[(curBitmap.Height - i - 1) * curBitmap.Width + j] = temp;
                                }
                            }
                        }
                    }
                    else if (curBitmap.PixelFormat == PixelFormat.Format24bppRgb)
                    {
                        if (mirForm.GetMirror)
                        {
                            //水平镜像处理  
                            for (int i = 0; i < curBitmap.Height; i++)
                            {
                                //每个像素的三个字节在水平镜像时顺序不能变，所以这个方法不能用
                                //for (int j = 0; j < halfWidth; j++)
                                //{
                                //    //以水平中轴线为对称轴，两边像素值交换  
                                //    temp = pixelValues[i * curBitmap.Width * 3 + j * 3];
                                //    pixelValues[i * curBitmap.Width * 3 + j * 3] = pixelValues[(i + 1) * curBitmap.Width * 3 - 1 - j * 3];
                                //    pixelValues[(i + 1) * curBitmap.Width * 3 - 1 - j * 3] = temp;
                                //}
                                for (int j = 0; j < halfWidth; j++)
                                {//每三个字节组成一个像素，顺序不能乱
                                    temp = pixelValues[0 + i * curBitmap.Width * 3 + j * 3];
                                    temp1 = pixelValues[1 + i * curBitmap.Width * 3 + j * 3];
                                    temp2 = pixelValues[2 + i * curBitmap.Width * 3 + j * 3];
                                    pixelValues[0 + i * curBitmap.Width * 3 + j * 3] = pixelValues[0 + (i + 1) * curBitmap.Width * 3 - (j + 1) * 3];
                                    pixelValues[1 + i * curBitmap.Width * 3 + j * 3] = pixelValues[1 + (i + 1) * curBitmap.Width * 3 - (j + 1) * 3];
                                    pixelValues[2 + i * curBitmap.Width * 3 + j * 3] = pixelValues[2 + (i + 1) * curBitmap.Width * 3 - (j + 1) * 3];
                                    pixelValues[0 + (i + 1) * curBitmap.Width * 3 - (j + 1) * 3] = temp;
                                    pixelValues[1 + (i + 1) * curBitmap.Width * 3 - (j + 1) * 3] = temp1;
                                    pixelValues[2 + (i + 1) * curBitmap.Width * 3 - (j + 1) * 3] = temp2;
                                }
                            }
                        }
                        else
                        {
                            //垂直镜像处理  
                            for (int i = 0; i < curBitmap.Width * 3; i++)
                            {
                                for (int j = 0; j < halfHeight; j++)
                                {
                                    //以垂直中轴线为对称轴。两边像素值互换  
                                    temp = pixelValues[j * curBitmap.Width * 3 + i];
                                    pixelValues[j * curBitmap.Width * 3 + i] = pixelValues[(curBitmap.Height - j - 1) * curBitmap.Width * 3 + i];
                                    pixelValues[(curBitmap.Height - j - 1) * curBitmap.Width * 3 + i] = temp;
                                }
                            }
                        }
                    }

                        Marshal.Copy(pixelValues, 0, ptr, bytes);
                    curBitmap.UnlockBits(bmpData);
                }
                Invalidate();
            }
        }

        /// <summary>
        /// 图像缩放
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void zoom_Click(object sender, EventArgs e)
        {
            if (curBitmap != null)
            {
                zoom zoomForm = new zoom();
                if (zoomForm.ShowDialog() == DialogResult.OK)
                {
                    //得到横向和纵向缩放量
                    double x = Convert.ToDouble(zoomForm.GetXZoom);
                    double y = Convert.ToDouble(zoomForm.GetYZoom);
                    bool zoomResult = Zoom(curBitmap, x, y, out curBitmap, zoomForm.GetNearOrBil);
                    Invalidate();
                }
            }
        }

        /// <summary>
        /// 图像缩放
        /// </summary>
        /// <param name="srcBmp">原始图像</param>
        /// <param name="width">目标图像宽度</param>
        /// <param name="height">目标图像高度</param>
        /// <param name="dstBmp">目标图像</param>
        /// <param name="GetNearOrBil">缩放选用的算法</param>
        /// <returns>处理成功 true 失败 false</returns>
        public static bool Zoom(Bitmap srcBmp, double ratioW, double ratioH, out Bitmap dstBmp, bool GetNearOrBil)
        {//ZoomType为自定义的枚举类型
            if (srcBmp == null)
            {
                dstBmp = null;
                return false;
            }
            //若缩放大小与原图一样，则返回原图不做处理
            if ((ratioW == 1.0) && ratioH == 1.0)
            {
                dstBmp = new Bitmap(srcBmp);
                return true;
            }
            //计算缩放高宽
            double height = ratioH * (double)srcBmp.Height;
            double width = ratioW * (double)srcBmp.Width;
            dstBmp = new Bitmap((int)width, (int)height);

            BitmapData srcBmpData = srcBmp.LockBits(new Rectangle(0, 0, srcBmp.Width, srcBmp.Height), ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);
            BitmapData dstBmpData = dstBmp.LockBits(new Rectangle(0, 0, dstBmp.Width, dstBmp.Height), ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);
            unsafe
            {
                byte* srcPtr = null;
                byte* dstPtr = null;
                int srcI = 0;
                int srcJ = 0;
                double srcdI = 0;
                double srcdJ = 0;
                double a = 0;
                double b = 0;
                double F1 = 0;//横向插值所得数值
                double F2 = 0;//纵向插值所得数值
                if (GetNearOrBil)
                {//邻近插值法

                    for (int i = 0; i < dstBmp.Height; i++)
                    {
                        srcI = (int)(i / ratioH);//srcI是此时的i对应的原图像的高
                        srcPtr = (byte*)srcBmpData.Scan0 + srcI * srcBmpData.Stride;
                        dstPtr = (byte*)dstBmpData.Scan0 + i * dstBmpData.Stride;
                        for (int j = 0; j < dstBmp.Width; j++)
                        {
                            dstPtr[j * 3] = srcPtr[(int)(j / ratioW) * 3];//j / ratioW求出此时j对应的原图像的宽
                            dstPtr[j * 3 + 1] = srcPtr[(int)(j / ratioW) * 3 + 1];
                            dstPtr[j * 3 + 2] = srcPtr[(int)(j / ratioW) * 3 + 2];
                        }
                    }
                }
                else if (!GetNearOrBil)
                {//双线性插值法
                    byte* srcPtrNext = null;
                    for (int i = 0; i < dstBmp.Height; i++)
                    {
                        srcdI = i / ratioH;
                        srcI = (int)srcdI;//当前行对应原始图像的行数
                        srcPtr = (byte*)srcBmpData.Scan0 + srcI * srcBmpData.Stride;//指原始图像的当前行
                        srcPtrNext = (byte*)srcBmpData.Scan0 + (srcI + 1) * srcBmpData.Stride;//指向原始图像的下一行
                        dstPtr = (byte*)dstBmpData.Scan0 + i * dstBmpData.Stride;//指向当前图像的当前行
                        for (int j = 0; j < dstBmp.Width; j++)
                        {
                            srcdJ = j / ratioW;
                            srcJ = (int)srcdJ;//指向原始图像的列
                            if (srcdJ < 1 || srcdJ > srcBmp.Width - 1 || srcdI < 1 || srcdI > srcBmp.Height - 1)
                            {//避免溢出（也可使用循环延拓）
                                dstPtr[j * 3] = 255;
                                dstPtr[j * 3 + 1] = 255;
                                dstPtr[j * 3 + 2] = 255;
                                continue;
                            }
                            a = srcdI - srcI;//计算插入的像素与原始像素距离（决定相邻像素的灰度所占的比例）
                            b = srcdJ - srcJ;
                            for (int k = 0; k < 3; k++)
                            {//插值    公式：f(i+p,j+q)=(1-p)(1-q)f(i,j)+(1-p)qf(i,j+1)+p(1-q)f(i+1,j)+pqf(i+1, j + 1)
                                F1 = (1 - b) * srcPtr[srcJ * 3 + k] + b * srcPtr[(srcJ + 1) * 3 + k];
                                F2 = (1 - b) * srcPtrNext[srcJ * 3 + k] + b * srcPtrNext[(srcJ + 1) * 3 + k];
                                dstPtr[j * 3 + k] = (byte)((1 - a) * F1 + a * F2);
                            }
                        }
                    }
                }
            }
            srcBmp.UnlockBits(srcBmpData);
            dstBmp.UnlockBits(dstBmpData);
            return true;
        }

        #region 书上例子-图像缩放
        //private void zoom_Click(object sender, EventArgs e)
        //{
        //    if (curBitmap != null)
        //    {
        //        zoom zoomForm = new zoom();
        //        if (zoomForm.ShowDialog() == DialogResult.OK)
        //        {
        //            Rectangle rect = new Rectangle(0, 0, curBitmap.Width, curBitmap.Height);
        //            BitmapData bmpData = curBitmap.LockBits(rect, ImageLockMode.ReadWrite, curBitmap.PixelFormat);
        //            IntPtr ptr = bmpData.Scan0;
        //            int bytes = curBitmap.Width * curBitmap.Height;
        //            byte[] grayValues = new byte[bytes];
        //            Marshal.Copy(ptr, grayValues, 0, bytes);

        //            //得到横向和纵向缩放量
        //            double x = Convert.ToDouble(zoomForm.GetXZoom);
        //            double y = Convert.ToDouble(zoomForm.GetYZoom);

        //            //图像的几何中心
        //            int halfWidth = (int)(curBitmap.Width / 2);
        //            int halfHeight = (int)(curBitmap.Height / 2);

        //            int xz = 0;
        //            int yz = 0;
        //            int tempWidth = 0;
        //            int tempHeight = 0;

        //            byte[] tempArray = new byte[bytes];

        //            if (zoomForm.GetNearOrBil == true)
        //            {
        //                //最近邻插值法
        //                for (int i = 0; i < curBitmap.Height; i++)
        //                {
        //                    for (int j = 0; j < curBitmap.Width; j++)
        //                    {
        //                        //以图像的几何中心为坐标原点进行坐标变换
        //                        //按逆向映射得到输入图像的坐标
        //                        tempHeight = i - halfHeight;
        //                        tempWidth = j - halfWidth;//以图像的几何中心为坐标原点的横坐标

        //                        //在不同象限进行四舍五入处理
        //                        if (tempWidth > 0)
        //                        {
        //                            //进行缩放
        //                            xz = (int)(tempWidth / x + 0.5);
        //                        }
        //                        else
        //                        {//tempWidth < 0 ,则-0.5才能四舍五入
        //                            xz = (int)(tempWidth / x - 0.5);
        //                        }
        //                        if (tempHeight > 0)
        //                        {
        //                            yz = (int)(tempHeight / y + 0.5);
        //                        }
        //                        else
        //                        {
        //                            yz = (int)(tempHeight / y - 0.5);
        //                        }

        //                        //坐标逆变换（坐标原点变回原来的（0,0）点了）
        //                        //经过缩放的坐标，映射到原来以（0,0）点为坐标原点的坐标系上
        //                        tempWidth = xz + halfWidth;
        //                        tempHeight = yz + halfHeight;
        //                        //得到输出图像像素值
        //                        if (tempWidth < 0 || tempWidth >= curBitmap.Width || tempHeight < 0 || tempHeight >= curBitmap.Height)
        //                        {
        //                            //缩放后留下的空白部分用白色像素代替
        //                            tempArray[i * curBitmap.Width + j] = 255;
        //                        }
        //                        else
        //                        {
        //                            //tempArray[i * curBitmap.Width + j]是缩放后的坐标
        //                            //grayValues[tempHeight * curBitmap.Width + tempWidth]是缩放后对应的原图像素坐标
        //                            tempArray[i * curBitmap.Width + j] = grayValues[tempHeight * curBitmap.Width + tempWidth];
        //                        }
        //                    }
        //                }
        //            }
        //            else
        //            {
        //                //双线性插值法
        //                double tempX, tempY, p, q;
        //                for (int i = 0; i < curBitmap.Height; i++)
        //                {
        //                    for (int j = 0; j < curBitmap.Width; j++)
        //                    {
        //                        //以图像的几何中心为坐标原点进行坐标变换
        //                        //按逆向映射得到输入图像的坐标
        //                        tempHeight = i - halfHeight;
        //                        tempWidth = j - halfWidth;
        //                        tempX = tempWidth / x;
        //                        tempY = tempHeight / y;

        //                        //在不同象限进行四舍五入处理
        //                        if (tempWidth > 0)
        //                        {
        //                            //进行缩放
        //                            xz = (int)tempX;
        //                        }
        //                        else
        //                        {
        //                            xz = (int)(tempX - 1);
        //                        }
        //                        if (tempHeight > 0)
        //                        {
        //                            yz = (int)tempY;
        //                        }
        //                        else
        //                        {
        //                            yz = (int)(tempY - 1);
        //                        }

        //                        //f(i+p,j+q)=(1-p)(1-q)f(i,j)+(1-p)qf(i,j+1)+p(1-q)f(i+1,j)+pqf(i+1, j + 1)
        //                        //得到公式中的p和q
        //                        p = tempX - xz;
        //                        q = tempY - yz;

        //                        //坐标逆变换
        //                        tempWidth = xz + halfWidth;
        //                        tempHeight = yz + halfHeight;
        //                        if (tempWidth < 0 || (tempWidth + 1) >= curBitmap.Width || tempHeight < 0 || (tempHeight + 1) >= curBitmap.Height)
        //                        {
        //                            //缩放后留下的空白部分用白色像素代替
        //                            tempArray[i * curBitmap.Width + j] = 255;
        //                        }
        //                        else
        //                        {
        //                            //应用公式得到双线性插值
        //                            tempArray[i * curBitmap.Width + j] = (byte)((1.0 - q) * ((1.0 - p) * grayValues[tempHeight * curBitmap.Width + tempWidth] + p * grayValues[tempHeight * curBitmap.Width + tempWidth + 1]) +
        //                                q * ((1.0 - p) * grayValues[(tempHeight + 1) * curBitmap.Width + tempWidth] + p * grayValues[(tempHeight + 1) * curBitmap.Width + 1 + tempWidth]));
        //                        }
        //                    }
        //                }

        //            }

        //            grayValues = (byte[])tempArray.Clone();

        //            Marshal.Copy(grayValues, 0, ptr, bytes);
        //            curBitmap.UnlockBits(bmpData);
        //        }

        //        Invalidate();

        //    }
        //}
        #endregion
    }
}
