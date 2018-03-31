using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace frequency
{
    public partial class granularity : Form
    {
        public granularity()
        {
            InitializeComponent();
            tempFlag = 0;
            radius = new byte[6] { 10, 50, 15, 60, 18, 20 };
        }

        private void start_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }

        private void close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void low_CheckedChanged(object sender, EventArgs e)
        {
            tempFlag = 0;
            label2.Text = "低通掩码半径（%）：";
            radius1.Value = radius[0];
            label3.Visible = false;
            radius2.Visible = false;
            Invalidate();
        }

        private void mid_CheckedChanged(object sender, EventArgs e)
        {
            tempFlag = 2;
            label2.Text = "带通掩码外圆半径（%）：";
            label3.Text = "带通掩码内圆半径（%）：";
            label3.Visible = true;
            radius1.Value = radius[3];
            radius2.Value = radius[4];
            radius2.Visible = true;
            Invalidate();
        }

        private void midStop_CheckedChanged(object sender, EventArgs e)
        {            
            tempFlag = 1;
            label2.Text = "带阻掩码外圆半径（%）：";
            label3.Text = "带阻掩码内圆半径（%）：";
            label3.Visible = true;
            radius1.Value = radius[1];
            radius2.Value = radius[2];
            radius2.Visible = true;
            Invalidate();
        }
        
        private void high_CheckedChanged(object sender, EventArgs e)
        {
            tempFlag = 3;
            label2.Text = "高通掩码半径（%）：";
            radius1.Value = radius[5];
            label3.Visible = false;
            radius2.Visible = false;
            Invalidate();
        }

        private void granularity_Paint(object sender, PaintEventArgs e)
        {
            SolidBrush blackBrush = new SolidBrush(Color.Black);
            SolidBrush whiteBrush = new SolidBrush(Color.White);
            Pen blackPen=new Pen(Color.Black,1.5F);
            Graphics g = e.Graphics;
            
            switch(tempFlag)
            {
                case 0:
                    g.FillRectangle(blackBrush, 180, 40, 200, 200);
                    g.FillEllipse(whiteBrush, 280 - radius[0], 140 - radius[0], 2 * radius[0], 2 * radius[0]);
                    break;
                case 1:
                    g.FillRectangle(whiteBrush, 180, 40, 200, 200);
                    g.FillEllipse(blackBrush, 280 - radius[1], 140 - radius[1], 2 * radius[1], 2 * radius[1]);
                    g.FillEllipse(whiteBrush, 280 - radius[2], 140 - radius[2], 2 * radius[2], 2 * radius[2]);
                    g.DrawRectangle(blackPen, 180, 40, 200, 200);
                    break;
                case 2:
                    g.FillRectangle(blackBrush, 180, 40, 200, 200);
                    g.FillEllipse(whiteBrush, 280 - radius[3], 140 - radius[3], 2 * radius[3], 2 * radius[3]);
                    g.FillEllipse(blackBrush, 280 - radius[4], 140 - radius[4], 2 * radius[4], 2 * radius[4]);
                    break;
                case 3:
                    g.FillRectangle(whiteBrush, 180, 40, 200, 200);
                    g.FillEllipse(blackBrush, 280 - radius[5], 140 - radius[5], 2 * radius[5], 2 * radius[5]);
                    g.DrawRectangle(blackPen, 180, 40, 200, 200);
                    break;
                default:
                    break;
            }

            blackBrush.Dispose();
            whiteBrush.Dispose();
            blackPen.Dispose();
        }

        private void radius1_ValueChanged(object sender, EventArgs e)
        {
            byte tempValue = (byte)radius1.Value;
            switch (tempFlag)
            {
                case 0:
                    radius[0] = tempValue;
                    break;
                case 1:                    
                    if (tempValue <= radius[2])
                    {
                        radius1.Value = radius[1];
                        break;
                    }
                    radius[1] = tempValue;
                    break;
                case 2:                    
                    if (tempValue <= radius[4])
                    {
                        radius1.Value = radius[3];
                        break;
                    }
                    radius[3] = tempValue;
                    break;
                case 3:
                    radius[5] = tempValue;
                    break;
                default:
                    break;
            }
            Invalidate();
        }

        private void radius2_ValueChanged(object sender, EventArgs e)
        {
            byte tempValue = (byte)radius2.Value;
            switch (tempFlag)
            {
                case 1:
                    if (tempValue >= radius[1])
                    {
                        radius2.Value = radius[2];
                        break;
                    }
                    radius[2] = tempValue;
                    break;
                case 2:
                    if (tempValue >= radius[3])
                    {
                        radius2.Value = radius[4];
                        break;
                    }
                    radius[4] = tempValue;
                    break;
                default:
                    break;
            }
            Invalidate();
        }

        public byte[] GetRadius
        {
            get
            {
                return radius;
            }
        }

        public byte GetFlag
        {
            get
            {
                return tempFlag;
            }
        }        
    }
}