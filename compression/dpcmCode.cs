using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace compression
{
    public partial class dpcmCode : Form
    {
        public dpcmCode()
        {
            InitializeComponent();
            DorC = false;
            methodDPCM = 0;
            methodBox.Items.Clear();
            methodBox.Items.Add("· X'=X1");
            methodBox.Items.Add("· X'=(X1+X3)/2");
            methodBox.Items.Add("· X'=X3-X2+X1");
            methodBox.Items.Add("· X'=(X1+X4)/2");
            methodBox.SelectedIndex = 0;
        }

        private void start_Click(object sender, EventArgs e)
        {
            methodDPCM = Convert.ToByte(methodBox.SelectedIndex);
            this.DialogResult = DialogResult.OK;
        }

        private void close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public bool GetDorC
        {
            get
            {
                return DorC;
            }
        }

        public byte GetMethod
        {
            get
            {
                return methodDPCM;
            }
        }

        private void dpcmEncoding_CheckedChanged(object sender, EventArgs e)
        {
            DorC = false;
            methodBox.Enabled = true;
        }

        private void dpcmDecoding_CheckedChanged(object sender, EventArgs e)
        {
            DorC = true;
            methodBox.Enabled = false;
        }
    }
}