using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace segmentatioin
{
    public partial class ORI : Form
    {
        public ORI()
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

        public int GetSegNum
        {
            get
            {
                return Convert.ToInt16(segNum.Value);
            }
        }

        public int GetIterNum
        {
            get
            {
                return Convert.ToInt16(iterNum.Value);
            }
        }
    }
}