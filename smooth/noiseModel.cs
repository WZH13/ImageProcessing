using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace smooth
{
    public partial class noiseModel : Form
    {
        public noiseModel()
        {
            InitializeComponent();
            flagN = 0;
            paraN = new double[2] { 0, 20 };
        }

        private void start_Click(object sender, EventArgs e)
        {
            switch (flagN)
            {
                case 0:
                    paraN[0] = Convert.ToDouble(mean.Text);
                    paraN[1] = Convert.ToDouble(stanDev.Text);
                    break;
                case 1:
                    paraN[0] = Convert.ToDouble(paraRA.Text);
                    paraN[1] = Convert.ToDouble(paraRB.Text);
                    break;
                case 2:
                    paraN[0] = Convert.ToDouble(paraEA.Text);
                    if (paraN[0] <= 0)
                    {
                        paraEA.Text = "0.1";
                        return;
                    }
                    break;
                case 3:
                    paraN[0] = Convert.ToDouble(pepper.Text);
                    paraN[1] = Convert.ToDouble(salt.Text);
                    if (paraN[0] +paraN[1] >= 1)
                    {
                        pepper.Text = "0.02";
                        salt.Text = "0.02";
                        return;
                    }
                    break;
                default:
                    break;

            }
            this.DialogResult = DialogResult.OK;
        }

        private void close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public double[] GetParaN
        {
            get
            {
                return paraN;
            }
        }

        public byte GetFlag
        {
            get
            {
                return flagN;
            }
        }

        private void gaussian_CheckedChanged(object sender, EventArgs e)
        {
            flagN = 0;
            mean.Enabled = true;
            stanDev.Enabled = true;
            paraRA.Enabled = false;
            paraRB.Enabled = false;
            paraEA.Enabled = false;
            pepper.Enabled = false;
            salt.Enabled = false;
        }

        private void rayleigh_CheckedChanged(object sender, EventArgs e)
        {
            flagN = 1;
            mean.Enabled = false;
            stanDev.Enabled = false;
            paraRA.Enabled = true;
            paraRB.Enabled = true;
            paraEA.Enabled = false;
            pepper.Enabled = false;
            salt.Enabled = false;
        }

        private void exponential_CheckedChanged(object sender, EventArgs e)
        {
            flagN = 2;
            mean.Enabled = false;
            stanDev.Enabled = false;
            paraRA.Enabled = false;
            paraRB.Enabled = false;
            paraEA.Enabled = true;
            pepper.Enabled = false;
            salt.Enabled = false;
        }

        private void saltpepper_CheckedChanged(object sender, EventArgs e)
        {
            flagN = 3;
            mean.Enabled = false;
            stanDev.Enabled = false;
            paraRA.Enabled = false;
            paraRB.Enabled = false;
            paraEA.Enabled = false;
            pepper.Enabled = true;
            salt.Enabled = true;
        }

    }
}