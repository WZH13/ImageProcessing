using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace smooth
{
    public partial class gauss : Form
    {
        public gauss()
        {
            InitializeComponent();
        }

        private void start_Click(object sender, EventArgs e)
        {
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
                return Convert.ToDouble(sigmaValue.Text);
            }
        }
    }
}