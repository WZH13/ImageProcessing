using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace smooth
{
    public partial class stati : Form
    {
        public stati()
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

        public bool GetWindows
        {
            get
            {
                return five.Checked;
            }
        }

        public double GetThresholding
        {
            get
            {
                return Convert.ToDouble(thresh.Text);
            }
        }
    }
}