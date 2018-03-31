using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace compression
{
    public partial class transCode : Form
    {
        public transCode()
        {
            InitializeComponent();
            sizeNum = 4;
            ratioNum = 2;
        }

        private void start_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }

        private void close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void size4_CheckedChanged(object sender, EventArgs e)
        {
            sizeNum = 4;
        }

        private void size8_CheckedChanged(object sender, EventArgs e)
        {
            sizeNum = 8;
        }

        private void size16_CheckedChanged(object sender, EventArgs e)
        {
            sizeNum = 16;
        }

        private void ratio2_CheckedChanged(object sender, EventArgs e)
        {
            ratioNum = 2;
        }

        private void ratio4_CheckedChanged(object sender, EventArgs e)
        {
            ratioNum = 4;
        }

        private void ratio8_CheckedChanged(object sender, EventArgs e)
        {
            ratioNum = 8;
        }

        public byte GetSize
        {
            get
            {
                return sizeNum;
            }
        }

        public byte GetRatio
        {
            get
            {
                return ratioNum;
            }
        }
    }
}