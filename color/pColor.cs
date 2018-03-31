using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace color
{
    public partial class pColor : Form
    {
        public pColor()
        {
            InitializeComponent();
            pseMethod = false;
            numSeg = 4;
            intNum.Items.Add("分 4 层");
            intNum.Items.Add("分 8 层");
            intNum.Items.Add("分 16 层");
            intNum.Items.Add("分 32 层");
            intNum.SelectedIndex = 0;
        }

        private void start_Click(object sender, EventArgs e)
        {
            numSeg = (byte)(Math.Pow(2, 2 + intNum.SelectedIndex));
            this.DialogResult = DialogResult.OK;
        }

        private void close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void intSeg_CheckedChanged(object sender, EventArgs e)
        {
            pseMethod = false;
            intNum.Enabled = true;
        }

        private void gcTrans_CheckedChanged(object sender, EventArgs e)
        {
            pseMethod = true;
            intNum.Enabled = false;
        }

        public bool GetMethod
        {
            get
            {
                return pseMethod;
            }
        }

        public byte GetSeg
        {
            get
            {
                return numSeg;
            }
        }
    }
}