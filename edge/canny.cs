using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace edge
{
    public partial class canny : Form
    {
        public canny()
        {
            InitializeComponent();
            thresh = new byte[] { 100, 50 };
        }

        private void start_Click(object sender, EventArgs e)
        {
            thresh[0] = Convert.ToByte(threshHigh.Text);
            thresh[1] = Convert.ToByte(threshLow.Text);
            this.DialogResult = DialogResult.OK;
        }

        private void close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public double GetSigma
        {
            get
            {
                return Convert.ToDouble(sigma.Text);
            }
        }

        public byte[] GetThresh
        {
            get
            {
                return thresh;
            }
        }
    }
}