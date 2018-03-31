using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace edge
{
    public partial class morphologic : Form
    {
        public morphologic()
        {
            InitializeComponent();
            se = new byte[25];
            Array.Clear(se, 0, 25);
        }

        private void start_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }

        private void close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void se1_Click(object sender, EventArgs e)
        {
            if (se[0] == 0)
            {
                se[0] = 1;
                se1.BackColor = Color.Black;
                this.start.Focus();
            }
            else
            {
                se[0] = 0;
                se1.BackColor = Color.White;
                this.start.Focus();
            }
        }

        private void se2_Click(object sender, EventArgs e)
        {
            if (se[1] == 0)
            {
                se[1] = 1;
                se2.BackColor = Color.Black;
                this.start.Focus();
            }
            else
            {
                se[1] = 0;
                se2.BackColor = Color.White;
                this.start.Focus();
            }
        }

        private void se3_Click(object sender, EventArgs e)
        {
            if (se[2] == 0)
            {
                se[2] = 1;
                se3.BackColor = Color.Black;
                this.start.Focus();
            }
            else
            {
                se[2] = 0;
                se3.BackColor = Color.White;
                this.start.Focus();
            }
        }

        private void se4_Click(object sender, EventArgs e)
        {
            if (se[3] == 0)
            {
                se[3] = 1;
                se4.BackColor = Color.Black;
                this.start.Focus();
            }
            else
            {
                se[3] = 0;
                se4.BackColor = Color.White;
                this.start.Focus();
            }
        }

        private void se5_Click(object sender, EventArgs e)
        {
            if (se[4] == 0)
            {
                se[4] = 1;
                se5.BackColor = Color.Black;
                this.start.Focus();
            }
            else
            {
                se[4] = 0;
                se5.BackColor = Color.White;
                this.start.Focus();
            }
        }

        private void se6_Click(object sender, EventArgs e)
        {
            if (se[5] == 0)
            {
                se[5] = 1;
                se6.BackColor = Color.Black;
                this.start.Focus();
            }
            else
            {
                se[5] = 0;
                se6.BackColor = Color.White;
                this.start.Focus();
            }
        }

        private void se7_Click(object sender, EventArgs e)
        {
            if (se[6] == 0)
            {
                se[6] = 1;
                se7.BackColor = Color.Black;
                this.start.Focus();
            }
            else
            {
                se[6] = 0;
                se7.BackColor = Color.White;
                this.start.Focus();
            }
        }

        private void se8_Click(object sender, EventArgs e)
        {
            if (se[7] == 0)
            {
                se[7] = 1;
                se8.BackColor = Color.Black;
                this.start.Focus();
            }
            else
            {
                se[7] = 0;
                se8.BackColor = Color.White;
                this.start.Focus();
            }
        }

        private void se9_Click(object sender, EventArgs e)
        {
            if (se[8] == 0)
            {
                se[8] = 1;
                se9.BackColor = Color.Black;
                this.start.Focus();
            }
            else
            {
                se[8] = 0;
                se9.BackColor = Color.White;
                this.start.Focus();
            }
        }

        private void se10_Click(object sender, EventArgs e)
        {
            if (se[9] == 0)
            {
                se[9] = 1;
                se10.BackColor = Color.Black;
                this.start.Focus();
            }
            else
            {
                se[9] = 0;
                se10.BackColor = Color.White;
                this.start.Focus();
            }
        }

        private void se11_Click(object sender, EventArgs e)
        {
            if (se[10] == 0)
            {
                se[10] = 1;
                se11.BackColor = Color.Black;
                this.start.Focus();
            }
            else
            {
                se[10] = 0;
                se11.BackColor = Color.White;
                this.start.Focus();
            }
        }

        private void se12_Click(object sender, EventArgs e)
        {
            if (se[11] == 0)
            {
                se[11] = 1;
                se12.BackColor = Color.Black;
                this.start.Focus();
            }
            else
            {
                se[11] = 0;
                se12.BackColor = Color.White;
                this.start.Focus();
            }
        }

        private void se13_Click(object sender, EventArgs e)
        {
            if (se[12] == 0)
            {
                se[12] = 1;
                se13.BackColor = Color.Black;
                this.start.Focus();
            }
            else
            {
                se[12] = 0;
                se13.BackColor = Color.White;
                this.start.Focus();
            }
        }

        private void se14_Click(object sender, EventArgs e)
        {
            if (se[13] == 0)
            {
                se[13] = 1;
                se14.BackColor = Color.Black;
                this.start.Focus();
            }
            else
            {
                se[13] = 0;
                se14.BackColor = Color.White;
                this.start.Focus();
            }
        }

        private void se15_Click(object sender, EventArgs e)
        {
            if (se[14] == 0)
            {
                se[14] = 1;
                se15.BackColor = Color.Black;
                this.start.Focus();
            }
            else
            {
                se[14] = 0;
                se15.BackColor = Color.White;
                this.start.Focus();
            }
        }

        private void se16_Click(object sender, EventArgs e)
        {
            if (se[15] == 0)
            {
                se[15] = 1;
                se16.BackColor = Color.Black;
                this.start.Focus();
            }
            else
            {
                se[15] = 0;
                se16.BackColor = Color.White;
                this.start.Focus();
            }
        }

        private void se17_Click(object sender, EventArgs e)
        {
            if (se[16] == 0)
            {
                se[16] = 1;
                se17.BackColor = Color.Black;
                this.start.Focus();
            }
            else
            {
                se[16] = 0;
                se17.BackColor = Color.White;
                this.start.Focus();
            }
        }

        private void se18_Click(object sender, EventArgs e)
        {
            if (se[17] == 0)
            {
                se[17] = 1;
                se18.BackColor = Color.Black;
                this.start.Focus();
            }
            else
            {
                se[17] = 0;
                se18.BackColor = Color.White;
                this.start.Focus();
            }
        }

        private void se19_Click(object sender, EventArgs e)
        {
            if (se[18] == 0)
            {
                se[18] = 1;
                se19.BackColor = Color.Black;
                this.start.Focus();
            }
            else
            {
                se[18] = 0;
                se19.BackColor = Color.White;
                this.start.Focus();
            }
        }

        private void se20_Click(object sender, EventArgs e)
        {
            if (se[19] == 0)
            {
                se[19] = 1;
                se20.BackColor = Color.Black;
                this.start.Focus();
            }
            else
            {
                se[19] = 0;
                se20.BackColor = Color.White;
                this.start.Focus();
            }
        }

        private void se21_Click(object sender, EventArgs e)
        {
            if (se[20] == 0)
            {
                se[20] = 1;
                se21.BackColor = Color.Black;
                this.start.Focus();
            }
            else
            {
                se[20] = 0;
                se21.BackColor = Color.White;
                this.start.Focus();
            }
        }

        private void se22_Click(object sender, EventArgs e)
        {
            if (se[21] == 0)
            {
                se[21] = 1;
                se22.BackColor = Color.Black;
                this.start.Focus();
            }
            else
            {
                se[21] = 0;
                se22.BackColor = Color.White;
                this.start.Focus();
            }
        }

        private void se23_Click(object sender, EventArgs e)
        {
            if (se[22] == 0)
            {
                se[22] = 1;
                se23.BackColor = Color.Black;
                this.start.Focus();
            }
            else
            {
                se[22] = 0;
                se23.BackColor = Color.White;
                this.start.Focus();
            }
        }

        private void se24_Click(object sender, EventArgs e)
        {
            if (se[23] == 0)
            {
                se[23] = 1;
                se24.BackColor = Color.Black;
                this.start.Focus();
            }
            else
            {
                se[23] = 0;
                se24.BackColor = Color.White;
                this.start.Focus();
            }
        }

        private void se25_Click(object sender, EventArgs e)
        {
            if (se[24] == 0)
            {
                se[24] = 1;
                se25.BackColor = Color.Black;
                this.start.Focus();
            }
            else
            {
                se[24] = 0;
                se25.BackColor = Color.White;
                this.start.Focus();
            }
        }

        public byte[] GetStruction
        {
            get
            {
                return se;
            }
        }

        public bool GetMethod
        {
            get
            {
                return laplacian.Checked;
            }
        }

        public double GetThresh
        {
            get
            {
                return Convert.ToDouble(thresh.Text);
            }
        }
    }
}