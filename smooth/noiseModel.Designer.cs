namespace smooth
{
    partial class noiseModel
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.start = new System.Windows.Forms.Button();
            this.close = new System.Windows.Forms.Button();
            this.gaussian = new System.Windows.Forms.RadioButton();
            this.rayleigh = new System.Windows.Forms.RadioButton();
            this.exponential = new System.Windows.Forms.RadioButton();
            this.saltpepper = new System.Windows.Forms.RadioButton();
            this.mean = new System.Windows.Forms.TextBox();
            this.stanDev = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.paraRA = new System.Windows.Forms.TextBox();
            this.paraRB = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.paraEA = new System.Windows.Forms.TextBox();
            this.pepper = new System.Windows.Forms.TextBox();
            this.salt = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // start
            // 
            this.start.Location = new System.Drawing.Point(80, 225);
            this.start.Name = "start";
            this.start.Size = new System.Drawing.Size(75, 23);
            this.start.TabIndex = 0;
            this.start.Text = "确定";
            this.start.UseVisualStyleBackColor = true;
            this.start.Click += new System.EventHandler(this.start_Click);
            // 
            // close
            // 
            this.close.Location = new System.Drawing.Point(250, 225);
            this.close.Name = "close";
            this.close.Size = new System.Drawing.Size(75, 23);
            this.close.TabIndex = 1;
            this.close.Text = "退出";
            this.close.UseVisualStyleBackColor = true;
            this.close.Click += new System.EventHandler(this.close_Click);
            // 
            // gaussian
            // 
            this.gaussian.AutoSize = true;
            this.gaussian.Checked = true;
            this.gaussian.Location = new System.Drawing.Point(41, 33);
            this.gaussian.Name = "gaussian";
            this.gaussian.Size = new System.Drawing.Size(71, 16);
            this.gaussian.TabIndex = 2;
            this.gaussian.TabStop = true;
            this.gaussian.Text = "高斯噪声";
            this.gaussian.UseVisualStyleBackColor = true;
            this.gaussian.CheckedChanged += new System.EventHandler(this.gaussian_CheckedChanged);
            // 
            // rayleigh
            // 
            this.rayleigh.AutoSize = true;
            this.rayleigh.Location = new System.Drawing.Point(41, 79);
            this.rayleigh.Name = "rayleigh";
            this.rayleigh.Size = new System.Drawing.Size(71, 16);
            this.rayleigh.TabIndex = 3;
            this.rayleigh.TabStop = true;
            this.rayleigh.Text = "瑞利噪声";
            this.rayleigh.UseVisualStyleBackColor = true;
            this.rayleigh.CheckedChanged += new System.EventHandler(this.rayleigh_CheckedChanged);
            // 
            // exponential
            // 
            this.exponential.AutoSize = true;
            this.exponential.Location = new System.Drawing.Point(41, 125);
            this.exponential.Name = "exponential";
            this.exponential.Size = new System.Drawing.Size(71, 16);
            this.exponential.TabIndex = 4;
            this.exponential.TabStop = true;
            this.exponential.Text = "指数噪声";
            this.exponential.UseVisualStyleBackColor = true;
            this.exponential.CheckedChanged += new System.EventHandler(this.exponential_CheckedChanged);
            // 
            // saltpepper
            // 
            this.saltpepper.AutoSize = true;
            this.saltpepper.Location = new System.Drawing.Point(41, 171);
            this.saltpepper.Name = "saltpepper";
            this.saltpepper.Size = new System.Drawing.Size(71, 16);
            this.saltpepper.TabIndex = 5;
            this.saltpepper.TabStop = true;
            this.saltpepper.Text = "椒盐噪声";
            this.saltpepper.UseVisualStyleBackColor = true;
            this.saltpepper.CheckedChanged += new System.EventHandler(this.saltpepper_CheckedChanged);
            // 
            // mean
            // 
            this.mean.Location = new System.Drawing.Point(210, 32);
            this.mean.Name = "mean";
            this.mean.Size = new System.Drawing.Size(40, 21);
            this.mean.TabIndex = 7;
            this.mean.Text = "0";
            // 
            // stanDev
            // 
            this.stanDev.Location = new System.Drawing.Point(344, 32);
            this.stanDev.Name = "stanDev";
            this.stanDev.Size = new System.Drawing.Size(40, 21);
            this.stanDev.TabIndex = 8;
            this.stanDev.Text = "20";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(133, 35);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 12);
            this.label1.TabIndex = 9;
            this.label1.Text = "均值（μ）：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(255, 35);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(89, 12);
            this.label2.TabIndex = 10;
            this.label2.Text = "均方差（σ）：";
            // 
            // paraRA
            // 
            this.paraRA.Enabled = false;
            this.paraRA.Location = new System.Drawing.Point(180, 78);
            this.paraRA.Name = "paraRA";
            this.paraRA.Size = new System.Drawing.Size(40, 21);
            this.paraRA.TabIndex = 11;
            this.paraRA.Text = "0";
            // 
            // paraRB
            // 
            this.paraRB.Enabled = false;
            this.paraRB.Location = new System.Drawing.Point(294, 78);
            this.paraRB.Name = "paraRB";
            this.paraRB.Size = new System.Drawing.Size(40, 21);
            this.paraRB.TabIndex = 12;
            this.paraRB.Text = "250";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(133, 81);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(47, 12);
            this.label3.TabIndex = 13;
            this.label3.Text = "参数a：";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(247, 81);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(47, 12);
            this.label4.TabIndex = 14;
            this.label4.Text = "参数b：";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(133, 127);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(77, 12);
            this.label5.TabIndex = 15;
            this.label5.Text = "参数a(a>0)：";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(133, 173);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(53, 12);
            this.label6.TabIndex = 16;
            this.label6.Text = "含椒量：";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(255, 173);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(53, 12);
            this.label7.TabIndex = 17;
            this.label7.Text = "含盐量：";
            // 
            // paraEA
            // 
            this.paraEA.Enabled = false;
            this.paraEA.Location = new System.Drawing.Point(214, 124);
            this.paraEA.Name = "paraEA";
            this.paraEA.Size = new System.Drawing.Size(40, 21);
            this.paraEA.TabIndex = 18;
            this.paraEA.Text = "0.1";
            // 
            // pepper
            // 
            this.pepper.Enabled = false;
            this.pepper.Location = new System.Drawing.Point(192, 170);
            this.pepper.Name = "pepper";
            this.pepper.Size = new System.Drawing.Size(40, 21);
            this.pepper.TabIndex = 19;
            this.pepper.Text = "0.02";
            // 
            // salt
            // 
            this.salt.Enabled = false;
            this.salt.Location = new System.Drawing.Point(314, 170);
            this.salt.Name = "salt";
            this.salt.Size = new System.Drawing.Size(40, 21);
            this.salt.TabIndex = 20;
            this.salt.Text = "0.02";
            // 
            // noiseModel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(402, 275);
            this.ControlBox = false;
            this.Controls.Add(this.salt);
            this.Controls.Add(this.pepper);
            this.Controls.Add(this.paraEA);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.paraRB);
            this.Controls.Add(this.paraRA);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.stanDev);
            this.Controls.Add(this.mean);
            this.Controls.Add(this.saltpepper);
            this.Controls.Add(this.exponential);
            this.Controls.Add(this.rayleigh);
            this.Controls.Add(this.gaussian);
            this.Controls.Add(this.close);
            this.Controls.Add(this.start);
            this.Name = "noiseModel";
            this.Text = "噪声模型";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button start;
        private System.Windows.Forms.Button close;
        private System.Windows.Forms.RadioButton gaussian;
        private System.Windows.Forms.RadioButton rayleigh;
        private System.Windows.Forms.RadioButton exponential;
        private System.Windows.Forms.RadioButton saltpepper;
        private System.Windows.Forms.TextBox mean;
        private System.Windows.Forms.TextBox stanDev;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private byte flagN;
        private double [] paraN;
        private System.Windows.Forms.TextBox paraRA;
        private System.Windows.Forms.TextBox paraRB;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox paraEA;
        private System.Windows.Forms.TextBox pepper;
        private System.Windows.Forms.TextBox salt;
    }
}