using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace edge
{
    public partial class gaussian : Form
    {
        public gaussian()
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
                
        public double GetThresholding
        {
            get
            {
                return Convert.ToDouble(thresdValue.Text);
            }
        }

        public double GetSigma
        {
            get
            {
                return Convert.ToDouble(sigmaValue.Text);
            }
        }

        public bool GetFlag
        {
            get
            {
                return dog.Checked;
            }
        }
    }
}