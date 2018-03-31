using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace edge
{
    public partial class mask : Form
    {
        public mask()
        {
            InitializeComponent();
            operMask = 0;
        }

        private void close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void start_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }

        private void roberts_CheckedChanged(object sender, EventArgs e)
        {
            operMask = 0;
        }

        private void prewitt_CheckedChanged(object sender, EventArgs e)
        {
            operMask = 1;
        }

        private void sobel_CheckedChanged(object sender, EventArgs e)
        {
            operMask = 2;
        }

        private void laplacian1_CheckedChanged(object sender, EventArgs e)
        {
            operMask = 3;
        }

        private void laplacian2_CheckedChanged(object sender, EventArgs e)
        {
            operMask = 4;
        }

        private void laplacian3_CheckedChanged(object sender, EventArgs e)
        {
            operMask = 5;
        }

        private void kirsch_CheckedChanged(object sender, EventArgs e)
        {
            operMask = 6;
        }

        public int GetThresholding
        {
            get
            {
                return (int)thresholding.Value;
            }
        }

        public byte GetMask
        {
            get
            {
                return operMask;
            }
        }

    }
}