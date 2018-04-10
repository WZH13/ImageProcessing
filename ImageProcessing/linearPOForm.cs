using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ImageProcessing
{
    public partial class linearPOForm : Form
    {
        public linearPOForm()
        {
            InitializeComponent();
        }

        private void startLinear_Click(object sender, EventArgs e)
        {
            //设置DialogResult属性
            this.DialogResult = DialogResult.OK;
        }

        private void close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        //设置两个get访问器
        public string GetScaling
        {
            get
            {
                //得到斜率
                return scaling.Text;
            }
        }
        public string GetOffset
        {
            get
            {
                //得到偏移量
                return offset.Text;
            }
        }
    }
}
