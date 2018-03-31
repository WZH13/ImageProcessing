using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace smooth
{
    public partial class wavelet : Form
    {
        public wavelet()
        {
            InitializeComponent();
            flagV = 0x00;
        }

        private void start_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }

        private void close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void softTh_CheckedChanged(object sender, EventArgs e)
        {
            flagV = (byte)(flagV & 0x0f);
        }

        private void hardTh_CheckedChanged(object sender, EventArgs e)
        {
            flagV = (byte)(flagV & 0x0f | 0x10);
        }

        private void haar_CheckedChanged(object sender, EventArgs e)
        {
            flagV = (byte)(flagV & 0xf0);
        }

        private void daubechies2_CheckedChanged(object sender, EventArgs e)
        {
            flagV = (byte)(flagV & 0xf0 | 0x01);
        }

        private void daubechies3_CheckedChanged(object sender, EventArgs e)
        {
            flagV = (byte)(flagV & 0xf0 | 0x02);
        }

        private void daubechies4_CheckedChanged(object sender, EventArgs e)
        {
            flagV = (byte)(flagV & 0xf0 | 0x03);
        }

        private void daubechies5_CheckedChanged(object sender, EventArgs e)
        {
            flagV = (byte)(flagV & 0xf0 | 0x04);
        }

        private void daubechies6_CheckedChanged(object sender, EventArgs e)
        {
            flagV = (byte)(flagV & 0xf0 | 0x05);
        }

        public byte GetFlagV
        {
            get
            {
                return flagV;
            }
        }

        public byte GetSeries
        {
            get
            {
                return (byte)waveletSeries.Value;
            }
        }

        public byte GetThresholding
        {
            get
            {
                return (byte)thresValue.Value;
            }
        }
    }
}