namespace smooth
{
    partial class wavelet
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
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.thresValue = new System.Windows.Forms.NumericUpDown();
            this.hardTh = new System.Windows.Forms.RadioButton();
            this.softTh = new System.Windows.Forms.RadioButton();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.daubechies6 = new System.Windows.Forms.RadioButton();
            this.daubechies5 = new System.Windows.Forms.RadioButton();
            this.daubechies4 = new System.Windows.Forms.RadioButton();
            this.daubechies3 = new System.Windows.Forms.RadioButton();
            this.daubechies2 = new System.Windows.Forms.RadioButton();
            this.haar = new System.Windows.Forms.RadioButton();
            this.waveletSeries = new System.Windows.Forms.NumericUpDown();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.thresValue)).BeginInit();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.waveletSeries)).BeginInit();
            this.SuspendLayout();
            // 
            // start
            // 
            this.start.Location = new System.Drawing.Point(40, 345);
            this.start.Name = "start";
            this.start.Size = new System.Drawing.Size(75, 23);
            this.start.TabIndex = 0;
            this.start.Text = "确定";
            this.start.UseVisualStyleBackColor = true;
            this.start.Click += new System.EventHandler(this.start_Click);
            // 
            // close
            // 
            this.close.Location = new System.Drawing.Point(215, 345);
            this.close.Name = "close";
            this.close.Size = new System.Drawing.Size(75, 23);
            this.close.TabIndex = 1;
            this.close.Text = "退出";
            this.close.UseVisualStyleBackColor = true;
            this.close.Click += new System.EventHandler(this.close_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(63, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(89, 12);
            this.label1.TabIndex = 2;
            this.label1.Text = "小波变换级数：";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.thresValue);
            this.groupBox1.Controls.Add(this.hardTh);
            this.groupBox1.Controls.Add(this.softTh);
            this.groupBox1.Location = new System.Drawing.Point(40, 59);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(250, 95);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "阈值";
            // 
            // thresValue
            // 
            this.thresValue.Location = new System.Drawing.Point(138, 41);
            this.thresValue.Minimum = new decimal(new int[] {
            3,
            0,
            0,
            0});
            this.thresValue.Name = "thresValue";
            this.thresValue.Size = new System.Drawing.Size(51, 21);
            this.thresValue.TabIndex = 2;
            this.thresValue.Value = new decimal(new int[] {
            20,
            0,
            0,
            0});
            // 
            // hardTh
            // 
            this.hardTh.AutoSize = true;
            this.hardTh.Location = new System.Drawing.Point(25, 60);
            this.hardTh.Name = "hardTh";
            this.hardTh.Size = new System.Drawing.Size(59, 16);
            this.hardTh.TabIndex = 1;
            this.hardTh.Text = "硬阈值";
            this.hardTh.UseVisualStyleBackColor = true;
            this.hardTh.CheckedChanged += new System.EventHandler(this.hardTh_CheckedChanged);
            // 
            // softTh
            // 
            this.softTh.AutoSize = true;
            this.softTh.Checked = true;
            this.softTh.Location = new System.Drawing.Point(25, 20);
            this.softTh.Name = "softTh";
            this.softTh.Size = new System.Drawing.Size(59, 16);
            this.softTh.TabIndex = 0;
            this.softTh.TabStop = true;
            this.softTh.Text = "软阈值";
            this.softTh.UseVisualStyleBackColor = true;
            this.softTh.CheckedChanged += new System.EventHandler(this.softTh_CheckedChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.daubechies6);
            this.groupBox2.Controls.Add(this.daubechies5);
            this.groupBox2.Controls.Add(this.daubechies4);
            this.groupBox2.Controls.Add(this.daubechies3);
            this.groupBox2.Controls.Add(this.daubechies2);
            this.groupBox2.Controls.Add(this.haar);
            this.groupBox2.Location = new System.Drawing.Point(40, 177);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(250, 146);
            this.groupBox2.TabIndex = 4;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "低通滤波器";
            // 
            // daubechies6
            // 
            this.daubechies6.AutoSize = true;
            this.daubechies6.Location = new System.Drawing.Point(138, 111);
            this.daubechies6.Name = "daubechies6";
            this.daubechies6.Size = new System.Drawing.Size(89, 16);
            this.daubechies6.TabIndex = 5;
            this.daubechies6.Text = "Daubechies6";
            this.daubechies6.UseVisualStyleBackColor = true;
            this.daubechies6.CheckedChanged += new System.EventHandler(this.daubechies6_CheckedChanged);
            // 
            // daubechies5
            // 
            this.daubechies5.AutoSize = true;
            this.daubechies5.Location = new System.Drawing.Point(138, 71);
            this.daubechies5.Name = "daubechies5";
            this.daubechies5.Size = new System.Drawing.Size(89, 16);
            this.daubechies5.TabIndex = 4;
            this.daubechies5.Text = "Daubechies5";
            this.daubechies5.UseVisualStyleBackColor = true;
            this.daubechies5.CheckedChanged += new System.EventHandler(this.daubechies5_CheckedChanged);
            // 
            // daubechies4
            // 
            this.daubechies4.AutoSize = true;
            this.daubechies4.Location = new System.Drawing.Point(138, 31);
            this.daubechies4.Name = "daubechies4";
            this.daubechies4.Size = new System.Drawing.Size(89, 16);
            this.daubechies4.TabIndex = 3;
            this.daubechies4.Text = "Daubechies4";
            this.daubechies4.UseVisualStyleBackColor = true;
            this.daubechies4.CheckedChanged += new System.EventHandler(this.daubechies4_CheckedChanged);
            // 
            // daubechies3
            // 
            this.daubechies3.AutoSize = true;
            this.daubechies3.Location = new System.Drawing.Point(25, 111);
            this.daubechies3.Name = "daubechies3";
            this.daubechies3.Size = new System.Drawing.Size(89, 16);
            this.daubechies3.TabIndex = 2;
            this.daubechies3.Text = "Daubechies3";
            this.daubechies3.UseVisualStyleBackColor = true;
            this.daubechies3.CheckedChanged += new System.EventHandler(this.daubechies3_CheckedChanged);
            // 
            // daubechies2
            // 
            this.daubechies2.AutoSize = true;
            this.daubechies2.Location = new System.Drawing.Point(25, 71);
            this.daubechies2.Name = "daubechies2";
            this.daubechies2.Size = new System.Drawing.Size(89, 16);
            this.daubechies2.TabIndex = 1;
            this.daubechies2.Text = "Daubechies2";
            this.daubechies2.UseVisualStyleBackColor = true;
            this.daubechies2.CheckedChanged += new System.EventHandler(this.daubechies2_CheckedChanged);
            // 
            // haar
            // 
            this.haar.AutoSize = true;
            this.haar.Checked = true;
            this.haar.Location = new System.Drawing.Point(25, 31);
            this.haar.Name = "haar";
            this.haar.Size = new System.Drawing.Size(47, 16);
            this.haar.TabIndex = 0;
            this.haar.TabStop = true;
            this.haar.Text = "Haar";
            this.haar.UseVisualStyleBackColor = true;
            this.haar.CheckedChanged += new System.EventHandler(this.haar_CheckedChanged);
            // 
            // waveletSeries
            // 
            this.waveletSeries.Location = new System.Drawing.Point(178, 22);
            this.waveletSeries.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.waveletSeries.Name = "waveletSeries";
            this.waveletSeries.Size = new System.Drawing.Size(51, 21);
            this.waveletSeries.TabIndex = 5;
            this.waveletSeries.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // wavelet
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(329, 392);
            this.ControlBox = false;
            this.Controls.Add(this.waveletSeries);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.close);
            this.Controls.Add(this.start);
            this.Name = "wavelet";
            this.Text = "小波变换去噪";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.thresValue)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.waveletSeries)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button start;
        private System.Windows.Forms.Button close;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.NumericUpDown thresValue;
        private System.Windows.Forms.RadioButton hardTh;
        private System.Windows.Forms.RadioButton softTh;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.NumericUpDown waveletSeries;
        private System.Windows.Forms.RadioButton daubechies3;
        private System.Windows.Forms.RadioButton daubechies2;
        private System.Windows.Forms.RadioButton haar;        
        private System.Windows.Forms.RadioButton daubechies6;
        private System.Windows.Forms.RadioButton daubechies5;
        private System.Windows.Forms.RadioButton daubechies4;
        
        private byte flagV;
    }
}