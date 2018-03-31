using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace color
{
    public partial class colorSpace : Form
    {
        public colorSpace()
        {
            InitializeComponent();
            component = 0;
        }

        private void close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void start_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }

        private void redC_CheckedChanged(object sender, EventArgs e)
        {
            component = 0;
        }

        private void greenC_CheckedChanged(object sender, EventArgs e)
        {
            component = 1;
        }

        private void blueC_CheckedChanged(object sender, EventArgs e)
        {
            component = 2;
        }

        private void hueC_CheckedChanged(object sender, EventArgs e)
        {
            component = 3;
        }

        private void satC_CheckedChanged(object sender, EventArgs e)
        {
            component = 4;
        }

        private void intC_CheckedChanged(object sender, EventArgs e)
        {
            component = 5;
        }

        public byte GetCom
        {
            get
            {
                return component;
            }
        }
    }
}