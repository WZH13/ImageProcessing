using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace morphology
{
    public partial class hitmiss : Form
    {
        public hitmiss()
        {
            InitializeComponent();
            flagHit = new bool[9];
            flagMiss = new bool[9];
            //数组清零（设置为bool）
            Array.Clear(flagHit, 0, 9);
            Array.Clear(flagMiss, 0, 9);
        }

        //击中结构元素组中的第一个按钮空间的Click事件
        private void hit0_Click(object sender, EventArgs e)
        {
            if (flagHit[0] == false)
            {
                //以前没有被选中
                //设置相关变量标识
                flagHit[0] = true;
                //按钮背景色设置为黑色
                hit0.BackColor = Color.Black;
                this.start.Focus();
            }
            else
            {
                //以前被选中
                //清除相关变量标识
                flagHit[0] = false;
                //按钮背景色设置为白色
                hit0.BackColor = Color.White;
                this.start.Focus();
            }
        }

        private void hit1_Click(object sender, EventArgs e)
        {
            if (flagHit[1] == false)
            {
                flagHit[1] = true;
                hit1.BackColor = Color.Black;
                this.start.Focus();
            }
            else
            {
                flagHit[1] = false;
                hit1.BackColor = Color.White;
                this.start.Focus();
            }
        }

        private void hit2_Click(object sender, EventArgs e)
        {
            if (flagHit[2] == false)
            {
                flagHit[2] = true;
                hit2.BackColor = Color.Black;
                this.start.Focus();
            }
            else
            {
                flagHit[2] = false;
                hit2.BackColor = Color.White;
                this.start.Focus();
            }
        }

        private void hit3_Click(object sender, EventArgs e)
        {
            if (flagHit[3] == false)
            {
                flagHit[3] = true;
                hit3.BackColor = Color.Black;
                this.start.Focus();
            }
            else
            {
                flagHit[3] = false;
                hit3.BackColor = Color.White;
                this.start.Focus();
            }
        }

        private void hit4_Click(object sender, EventArgs e)
        {
            if (flagHit[4] == false)
            {
                flagHit[4] = true;
                hit4.BackColor = Color.Black;
                this.start.Focus();
            }
            else
            {
                flagHit[4] = false;
                hit4.BackColor = Color.White;
                this.start.Focus();
            }
        }

        private void hit5_Click(object sender, EventArgs e)
        {
            if (flagHit[5] == false)
            {
                flagHit[5] = true;
                hit5.BackColor = Color.Black;
                this.start.Focus();
            }
            else
            {
                flagHit[5] = false;
                hit5.BackColor = Color.White;
                this.start.Focus();
            }
        }

        private void hit6_Click(object sender, EventArgs e)
        {
            if (flagHit[6] == false)
            {
                flagHit[6] = true;
                hit6.BackColor = Color.Black;
                this.start.Focus();
            }
            else
            {
                flagHit[6] = false;
                hit6.BackColor = Color.White;
                this.start.Focus();
            }
        }

        private void hit7_Click(object sender, EventArgs e)
        {
            if (flagHit[7] == false)
            {
                flagHit[7] = true;
                hit7.BackColor = Color.Black;
                this.start.Focus();
            }
            else
            {
                flagHit[7] = false;
                hit7.BackColor = Color.White;
                this.start.Focus();
            }
        }

        private void hit8_Click(object sender, EventArgs e)
        {
            if (flagHit[8] == false)
            {
                flagHit[8] = true;
                hit8.BackColor = Color.Black;
                this.start.Focus();
            }
            else
            {
                flagHit[8] = false;
                hit8.BackColor = Color.White;
                this.start.Focus();
            }
        }

        //击不中结构元素组中的第一个按钮空间的Click事件
        private void miss0_Click(object sender, EventArgs e)
        {
            if (flagMiss[0] == false)
            {
                //以前没有被选中
                //设置相关变量标识
                flagMiss[0] = true;
                //按钮背景色设置为黑色
                miss0.BackColor = Color.Black;
                this.start.Focus();
            }
            else
            {
                //以前被选中
                //清除相关变量标识
                flagMiss[0] = false;
                //按钮背景色设置为白色
                miss0.BackColor = Color.White;
                this.start.Focus();
            }
        }

        private void miss1_Click(object sender, EventArgs e)
        {
            if (flagMiss[1] == false)
            {
                flagMiss[1] = true;
                miss1.BackColor = Color.Black;
                this.start.Focus();
            }
            else
            {
                flagMiss[1] = false;
                miss1.BackColor = Color.White;
                this.start.Focus();
            }
        }

        private void miss2_Click(object sender, EventArgs e)
        {
            if (flagMiss[2] == false)
            {
                flagMiss[2] = true;
                miss2.BackColor = Color.Black;
                this.start.Focus();
            }
            else
            {
                flagMiss[2] = false;
                miss2.BackColor = Color.White;
                this.start.Focus();
            }
        }

        private void miss3_Click(object sender, EventArgs e)
        {
            if (flagMiss[3] == false)
            {
                flagMiss[3] = true;
                miss3.BackColor = Color.Black;
                this.start.Focus();
            }
            else
            {
                flagMiss[3] = false;
                miss3.BackColor = Color.White;
                this.start.Focus();
            }
        }

        private void miss4_Click(object sender, EventArgs e)
        {
            if (flagMiss[4] == false)
            {
                flagMiss[4] = true;
                miss4.BackColor = Color.Black;
                this.start.Focus();
            }
            else
            {
                flagMiss[4] = false;
                miss4.BackColor = Color.White;
                this.start.Focus();
            }
        }

        private void miss5_Click(object sender, EventArgs e)
        {
            if (flagMiss[5] == false)
            {
                flagMiss[5] = true;
                miss5.BackColor = Color.Black;
                this.start.Focus();
            }
            else
            {
                flagMiss[5] = false;
                miss5.BackColor = Color.White;
                this.start.Focus();
            }
        }

        private void miss6_Click(object sender, EventArgs e)
        {
            if (flagMiss[6] == false)
            {
                flagMiss[6] = true;
                miss6.BackColor = Color.Black;
                this.start.Focus();
            }
            else
            {
                flagMiss[6] = false;
                miss6.BackColor = Color.White;
                this.start.Focus();
            }
        }

        private void miss7_Click(object sender, EventArgs e)
        {
            if (flagMiss[7] == false)
            {
                flagMiss[7] = true;
                miss7.BackColor = Color.Black;
                this.start.Focus();
            }
            else
            {
                flagMiss[7] = false;
                miss7.BackColor = Color.White;
                this.start.Focus();
            }
        }

        private void miss8_Click(object sender, EventArgs e)
        {
            if (flagMiss[8] == false)
            {
                flagMiss[8] = true;
                miss8.BackColor = Color.Black;
                this.start.Focus();
            }
            else
            {
                flagMiss[8] = false;
                miss8.BackColor = Color.White;
                this.start.Focus();
            }
        }

        private void start_Click(object sender, EventArgs e)
        {
            //判断两个结构元素是否有相交的部分
            if ((flagHit[0] == true && flagMiss[0] == true) ||
                (flagHit[1] == true && flagMiss[1] == true) ||
                (flagHit[2] == true && flagMiss[2] == true) ||
                (flagHit[3] == true && flagMiss[3] == true) ||
                (flagHit[4] == true && flagMiss[4] == true) ||
                (flagHit[5] == true && flagMiss[5] == true) ||
                (flagHit[6] == true && flagMiss[6] == true) ||
                (flagHit[7] == true && flagMiss[7] == true) ||
                (flagHit[8] == true && flagMiss[8] == true))
            {
                MessageBox.Show("击中与击不中的结构元素不应相交！");
            }
            else
            {
                this.DialogResult = DialogResult.OK;
            }
        }

        private void close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        //为了和主窗体传递数组变量
        public bool[] GetHitStruction
        {
            get
            {
                return flagHit;
            }
        }
        public bool[] GetMissStruction
        {
            get
            {
                return flagMiss;
            }
        }
    }
}