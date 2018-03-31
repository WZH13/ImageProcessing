using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace geometric
{
    public partial class rotation : Form
    {
        public rotation()
        {
            InitializeComponent();
        }

        private void startRot_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }

        private void close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public string GetDegree
        {
            get
            {
                //得到所要旋转的角度
                return degree.Text;
            }
        }
    }
}
