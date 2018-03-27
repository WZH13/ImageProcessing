using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace geometric
{
    public partial class zoom : Form
    {
        public zoom()
        {
            InitializeComponent();
        }

        private void startZoom_Click(object sender, EventArgs e)
        {
            //缩放量不能为0
            if (xZoom.Text == "0" || yZoom.Text == "0")
            {
                MessageBox.Show("缩放量不能为0！\n请重新正确填写。", "警告", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                this.DialogResult = DialogResult.OK;
            }
        }

        private void close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public bool GetNearOrBil
        {
            get
            {
                //得到的是最近邻插值法还是双线性插值法
                return nearestNeigh.Checked;
            }
        }

        public string GetXZoom
        {
            get
            {
                //得到横向缩放量
                return xZoom.Text;
            }
        }

        public string GetYZoom
        {
            get
            {
                //得到纵向缩放量
                return yZoom.Text;
            }
        }
    }
}