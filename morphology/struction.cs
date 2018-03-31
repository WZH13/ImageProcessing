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
            temp = 0x11;
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

        private void showPic(byte pic)
        {
            byte sPic = pic;
            
            switch (sPic)
            {
                case 0x11:
                    struPic.Image = System.Drawing.Image.FromFile(Application.StartupPath + "\\images\\row3.jpg");
                    break;
                case 0x12:
                    struPic.Image = System.Drawing.Image.FromFile(Application.StartupPath + "\\images\\col3.jpg");
                    break;
                case 0x14:
                    struPic.Image = System.Drawing.Image.FromFile(Application.StartupPath + "\\images\\cross3.jpg");
                    break;
                case 0x18:
                    struPic.Image = System.Drawing.Image.FromFile(Application.StartupPath + "\\images\\square3.jpg");
                    break;
                case 0x21:
                    struPic.Image = System.Drawing.Image.FromFile(Application.StartupPath + "\\images\\row5.jpg");
                    break;
                case 0x22:
                    struPic.Image = System.Drawing.Image.FromFile(Application.StartupPath + "\\images\\col5.jpg");
                    break;
                case 0x24:
                    struPic.Image = System.Drawing.Image.FromFile(Application.StartupPath + "\\images\\cross5.jpg");
                    break;
                case 0x28:
                    struPic.Image = System.Drawing.Image.FromFile(Application.StartupPath + "\\images\\square5.jpg");
                    break;
                default:
                    break;
            }
        }

        private void row_CheckedChanged(object sender, EventArgs e)
        {
            temp = (byte)((temp & 0xf0) | 0x01);
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
                return temp;
            }
        }
    }
}