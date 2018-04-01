using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace morphology
{
    public partial class struction : Form
    {
        public struction()
        {
            InitializeComponent();
            temp = 0x11;//3位水平形状
            showPic(temp);
        }

        private void start_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }

        private void close_Click(object sender, EventArgs e)
        {
            this.Close(); 
        }

        /// <summary>
        /// 用于显示结构元素形状
        /// </summary>
        /// <param name="pic">标识结构元素</param>
        private void showPic(byte pic)
        {
            byte sPic = pic;
            
            switch (sPic)
            {
                case 0x11:
                    //3位水平形状
                    struPic.Image = Image.FromFile(Application.StartupPath + "\\images\\row3.jpg");
                    break;
                case 0x12:
                    //3位垂直形状
                    struPic.Image = Image.FromFile(Application.StartupPath + "\\images\\col3.jpg");
                    break;
                case 0x14:
                    //3位“十”字形状
                    struPic.Image =  Image.FromFile(Application.StartupPath + "\\images\\cross3.jpg");
                    break;
                case 0x18:
                    //3位方形
                    struPic.Image =  Image.FromFile(Application.StartupPath + "\\images\\square3.jpg");
                    break;
                case 0x21:
                    //5位水平形状
                    struPic.Image =  Image.FromFile(Application.StartupPath + "\\images\\row5.jpg");
                    break;
                case 0x22:
                    //5位垂直形状
                    struPic.Image =  Image.FromFile(Application.StartupPath + "\\images\\col5.jpg");
                    break;
                case 0x24:
                    //5位十字形
                    struPic.Image =  Image.FromFile(Application.StartupPath + "\\images\\cross5.jpg");
                    break;
                case 0x28:
                    //5位方形
                    struPic.Image =  Image.FromFile(Application.StartupPath + "\\images\\square5.jpg");
                    break;
                default:
                    break;
            }
        }

        //temp用于标识结构元素，每一位标识的含义如下：
        //结构元素形状：
        //0:水平形状    1：垂直形状      2：“十”字形     3：方形
        //结构元素位数：
        //4：位数3     5：位数5

        private void row_CheckedChanged(object sender, EventArgs e)
        {
            temp = (byte)((temp & 0xf0) | 0x01);//0x0f换算成二进制：1111
            showPic(temp);
        }

        private void column_CheckedChanged(object sender, EventArgs e)
        {
            temp = (byte)((temp & 0xf0) | 0x02);
            showPic(temp);
        }

        private void cross_CheckedChanged(object sender, EventArgs e)
        {
            temp = (byte)((temp & 0xf0) | 0x04);
            showPic(temp);
        }

        private void square_CheckedChanged(object sender, EventArgs e)
        {
            temp = (byte)((temp & 0xf0) | 0x08);
            showPic(temp);
        }

        private void three_CheckedChanged(object sender, EventArgs e)
        {
            temp = (byte)((temp & 0x0f) | 0x10);
            showPic(temp);
        }

        private void five_CheckedChanged(object sender, EventArgs e)
        {
            temp = (byte)((temp & 0x0f) | 0x20);
            showPic(temp);
        }

        public byte GetStruction
        {
            get
            {
                //得到结构元素标识
                return temp;
            }
        }
    }
}