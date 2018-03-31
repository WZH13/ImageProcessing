using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace compression
{
    public partial class wlTrans : Form
    {
        public wlTrans()
        {
            InitializeComponent();
            wlBase = 0;
        }

        private void start_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }

        private void close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void haar_CheckedChanged(object sender, EventArgs e)
        {
            wlBase = 0;
        }

        private void daubechies2_CheckedChanged(object sender, EventArgs e)
        {
            wlBase = 1;
        }

        private void daubechies3_CheckedChanged(object sender, EventArgs e)
        {
            wlBase = 2;
        }

        private void daubechies4_CheckedChanged(object sender, EventArgs e)
        {
            wlBase = 3;
        }

        private void daubechies5_CheckedChanged(object sender, EventArgs e)
        {
            wlBase = 4;
        }

        private void daubechies6_CheckedChanged(object sender, EventArgs e)
        {
            wlBase = 5;
        }

        public byte GetSeries
        {
            get
            {
                return (byte)waveletSeries.Value;
            }
        }

        public byte GetBase
        {
            get
            {
                return wlBase;
            }
        }
    }
}