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
    public partial class translation : Form
    {
        public translation()
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

        public string GetXOFFset
        {
            get
            {   //水平平移量
                return xOffset.Text;
            }
        }
        public string GetYOffset
        {
            get
            {   //纵向平移量
                return yOffset.Text;
            }
        }
    }
}
