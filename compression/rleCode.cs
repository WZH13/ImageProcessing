using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace compression
{
    public partial class rleCode : Form
    {
        public rleCode()
        {
            InitializeComponent();
        }

        private void close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void start_Click(object sender, EventArgs e)
        {            
            this.DialogResult = DialogResult.OK;
        }

        public bool GetDorC
        {
            get
            {
                return rleDecoding.Checked;
            }
        }
    }
}