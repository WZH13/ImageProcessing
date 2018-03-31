using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace edge
{
    public partial class glp : Form
    {
        public glp(int maxLevel)
        {
            InitializeComponent();
            level.Maximum = maxLevel;
        }

        private void start_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }

        private void close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public byte GetLevel
        {
            get
            {
                return Convert.ToByte(level.Value);
            }
        }

        public double GetThresh
        {
            get
            {
                return Convert.ToDouble(thresh.Text);
            }
        }

        public double GetSigma
        {
            get
            {
                return Convert.ToDouble(sigma.Text);
            }
        }
    }
}