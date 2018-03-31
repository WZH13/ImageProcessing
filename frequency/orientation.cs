using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace frequency
{
    public partial class orientation : Form
    {
        public orientation()
        {
            InitializeComponent();
            flag = 0x11;
            orient = new int[2] { 45, 90 };
            leng1 = Convert.ToInt16(Math.Tan(orient[0] * (Math.PI / 180)) * 100);
            leng2 = Convert.ToInt16(Math.Tan(orient[1] * (Math.PI / 180) - Math.PI / 2) * 100);
            sOrie.Value = orient[0];
            fOrie.Value = orient[1];
        }

        private void start_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }

        private void close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void orientation_Paint(object sender, PaintEventArgs e)
        {
            SolidBrush blackBrush = new SolidBrush(Color.Black);
            SolidBrush whiteBrush = new SolidBrush(Color.White);
            Pen blackPen = new Pen(Color.Black, 1.5F);
            Graphics g = e.Graphics;
            
            g.FillRectangle(whiteBrush, 60, 30, 200, 200); 
            g.DrawRectangle(blackPen, 60, 30, 200, 200);

            Point point1, point2, point3, point4;

            switch (flag)
            {
                case 0x11:
                    point1 = new Point(260, 130 - leng1);
                    point2 = new Point(260, 130 - leng2);
                    point3 = new Point(60, 130 + leng1);
                    point4 = new Point(60, 130 + leng2);
                    Point[] curvePoints1 = { point1, point2, new Point(160, 130), point3, point4, new Point(160, 130) };
                    g.FillPolygon(blackBrush, curvePoints1);
                    break;
                case 0x21:
                    point1 = new Point(260, 130 - leng1);
                    point2 = new Point(160 - leng2, 30);
                    point3 = new Point(60, 130 + leng1);
                    point4 = new Point(160 + leng2, 230);
                    Point[] curvePoints2 = { point1, new Point(260, 30), point2, new Point(160, 130), point3, new Point(60, 230), point4, new Point(160, 130) };
                    g.FillPolygon(blackBrush, curvePoints2);
                    break;
                case 0x22:
                    point1 = new Point(160 - leng1, 30);
                    point2 = new Point(160 - leng2, 30);
                    point3 = new Point(160 + leng1, 230);
                    point4 = new Point(160 + leng2, 230);
                    Point[] curvePoints3 = { point1, point2, new Point(160, 130), point3, point4, new Point(160, 130) };
                    g.FillPolygon(blackBrush, curvePoints3);
                    break;
                case 0x42:
                    point1 = new Point(160 - leng1, 30);
                    point2 = new Point(60, 130 + leng2);
                    point3 = new Point(160 + leng1, 230);
                    point4 = new Point(260, 130 - leng2);
                    Point[] curvePoints4 = { point1, new Point(60, 30), point2, new Point(160, 130), point3, new Point(260, 230), point4, new Point(160, 130) };
                    g.FillPolygon(blackBrush, curvePoints4);
                    break;
                default:
                    MessageBox.Show("无效！");
                    break;
            }
                        
            blackBrush.Dispose();
            whiteBrush.Dispose();
            blackPen.Dispose();
        }

        private void sOrie_ValueChanged(object sender, EventArgs e)
        {
            if (sOrie.Value >= orient[1] || orient[1] - sOrie.Value > 90)
            {
                sOrie.Value = orient[0];
                return;
            }
            orient[0] = Convert.ToInt16(sOrie.Value);
            if (orient[0] >= -45 && orient[0] <= 45)
            {
                flag = (byte)((flag & 0xf0) | 0x01);
                leng1 = Convert.ToInt16(Math.Tan(orient[0] * (Math.PI / 180)) * 100);
            }
            else
            {
                flag = (byte)((flag & 0xf0) | 0x02);
                leng1 = Convert.ToInt16(Math.Tan(orient[0] * (Math.PI / 180) - Math.PI / 2) * 100);
            }
            Invalidate();
        }

        private void fOrie_ValueChanged(object sender, EventArgs e)
        {
            if (fOrie.Value <= orient[0] || fOrie.Value - orient[0] > 90)
            {
                fOrie.Value = orient[1];
                return;
            }
            orient[1] = Convert.ToInt16(fOrie.Value);
            if (orient[1] >= -45 && orient[1] <= 45)
            {
                flag = (byte)((flag & 0x0f) | 0x10);
                leng2 = Convert.ToInt16(Math.Tan(orient[1] * (Math.PI / 180)) * 100);
            }
            else
            {
                if (orient[1] > 45 && orient[1] <= 135)
                {
                    flag = (byte)((flag & 0x0f) | 0x20);
                    leng2 = Convert.ToInt16(Math.Tan(orient[1] * (Math.PI / 180) - Math.PI / 2) * 100);
                }
                else
                {
                    flag = (byte)((flag & 0x0f) | 0x40);
                    leng2 = Convert.ToInt16(Math.Tan(orient[1] * (Math.PI / 180) - Math.PI) * 100);
                }
            }
            Invalidate();
        }

        public int[] GetOrient
        {
            get
            {
                return orient;
            }
        }
    }
}