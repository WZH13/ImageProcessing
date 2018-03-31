using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace color
{
    public partial class changeCom : Form
    {
        public changeCom(Form1 pF)
        {
            InitializeComponent();
            masterF = pF;
        }

        private void close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void redTB_Scroll(object sender, EventArgs e)
        {
            redUD.Value = redTB.Value;
            masterF.adjustCom(0, redTB.Value, greenTB.Value, blueTB.Value);
        }

        private void redUD_ValueChanged(object sender, EventArgs e)
        {
            redTB.Value = (int)redUD.Value;
            masterF.adjustCom(0, redTB.Value, greenTB.Value, blueTB.Value);
        }

        private void greenTB_Scroll(object sender, EventArgs e)
        {
            greenUD.Value = greenTB.Value;
            masterF.adjustCom(0, redTB.Value, greenTB.Value, blueTB.Value);
        }

        private void greenUD_ValueChanged(object sender, EventArgs e)
        {
            greenTB.Value = (int)greenUD.Value;
            masterF.adjustCom(0, redTB.Value, greenTB.Value, blueTB.Value);
        }

        private void blueTB_Scroll(object sender, EventArgs e)
        {
            blueUD.Value = blueTB.Value;
            masterF.adjustCom(0, redTB.Value, greenTB.Value, blueTB.Value);
        }

        private void blueUD_ValueChanged(object sender, EventArgs e)
        {
            blueTB.Value = (int)blueUD.Value;
            masterF.adjustCom(0, redTB.Value, greenTB.Value, blueTB.Value);
        }

        private void hueTB_Scroll(object sender, EventArgs e)
        {
            hueUD.Value = hueTB.Value;
            masterF.adjustCom(1, hueTB.Value, satTB.Value, intTB.Value);
        }

        private void hueUD_ValueChanged(object sender, EventArgs e)
        {
            hueTB.Value = (int)hueUD.Value;
            masterF.adjustCom(1, hueTB.Value, satTB.Value, intTB.Value);
        }

        private void satTB_Scroll(object sender, EventArgs e)
        {
            satUD.Value = satTB.Value;
            masterF.adjustCom(1, hueTB.Value, satTB.Value, intTB.Value);
        }

        private void satUD_ValueChanged(object sender, EventArgs e)
        {
            satTB.Value = (int)satUD.Value;
            masterF.adjustCom(1, hueTB.Value, satTB.Value, intTB.Value);
        }

        private void intTB_Scroll(object sender, EventArgs e)
        {
            intUD.Value = intTB.Value;
            masterF.adjustCom(1, hueTB.Value, satTB.Value, intTB.Value);
        }

        private void intUD_ValueChanged(object sender, EventArgs e)
        {
            intTB.Value = (int)intUD.Value;
            masterF.adjustCom(1, hueTB.Value, satTB.Value, intTB.Value);
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (this.tabControl1.SelectedIndex)
            {
                case 0:
                    masterF.adjustCom(0, redTB.Value, greenTB.Value, blueTB.Value);
                    break;
                case 1:
                    masterF.adjustCom(1, hueTB.Value, satTB.Value, intTB.Value);
                    break;
            } 
        }
    }
}