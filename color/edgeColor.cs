using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace color
{
    public partial class edgeColor : Form
    {
        public edgeColor()
        {
            InitializeComponent();
            methodF = 0;
        }

        private void start_Click(object sender, EventArgs e)
        {
            if (vectEdge1.Checked == true)
                methodF = 0;
            else if (vectEdge2.Checked == true)
                methodF = 1;
            else
                methodF = 2;
            this.DialogResult = DialogResult.OK;
        }

        private void close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public byte GetMethod
        {
            get
            {
                return methodF;
            }
        }

        public int GetThresholding
        {
            get
            {
                return (int)threshUD.Value;
            }
        }
    }
}