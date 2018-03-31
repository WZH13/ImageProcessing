using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace smooth
{
    public partial class meanMedian : Form
    {
        public meanMedian()
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

        public byte GetLength
        {
            get
            {
                return (byte)length.Value;
            }
        }

        public bool GetFlag
        {
            get
            {
                return median.Checked;
            }
        }
    }
}