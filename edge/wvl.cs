using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace edge
{
    public partial class wvl : Form
    {
        public wvl()
        {
            InitializeComponent();
            thresh = new byte[] { 100, 50 };
        }

        private void start_Click(object sender, EventArgs e)
        {
            thresh[0] = Convert.ToByte(highT.Text);
            thresh[1] = Convert.ToByte(lowT.Text);
            this.DialogResult = DialogResult.OK;
        }

        private void close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public byte GetScale
        {
            get
            {
                return Convert.ToByte(multiscale.Value);
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