using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace segmentatioin
{
    public partial class thresholding : Form
    {
        public thresholding()
        {
            InitializeComponent();
            thr = 0;
        }

        private void start_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }

        private void close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public byte GetMethod
        {
            get
            {
                return thr;
            }
        }

        private void iteration_CheckedChanged(object sender, EventArgs e)
        {
            thr = 0;
        }

        private void otsu_CheckedChanged(object sender, EventArgs e)
        {
            thr = 1;
        }

        private void entropy1D_CheckedChanged(object sender, EventArgs e)
        {
            thr = 2;
        }

        private void entropy2D_CheckedChanged(object sender, EventArgs e)
        {
            thr = 3;
        }

        private void statis_CheckedChanged(object sender, EventArgs e)
        {
            thr = 4;
        }
    }
}