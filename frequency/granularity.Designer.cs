namespace frequency
{
    partial class granularity
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
            this.low = new System.Windows.Forms.RadioButton();
            this.mid = new System.Windows.Forms.RadioButton();
            this.high = new System.Windows.Forms.RadioButton();
            this.label1 = new System.Windows.Forms.Label();
            this.radius1 = new System.Windows.Forms.NumericUpDown();
            this.radius2 = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.midStop = new System.Windows.Forms.RadioButton();
            ((System.ComponentModel.ISupportInitialize)(this.radius1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radius2)).BeginInit();
            this.SuspendLayout();
            // 
            // start
            // 
            this.start.Location = new System.Drawing.Point(80, 340);
            this.start.Name = "start";
            this.start.Size = new System.Drawing.Size(75, 23);
            this.start.TabIndex = 0;
            this.start.Text = "确定";
            this.start.UseVisualStyleBackColor = true;
            this.start.Click += new System.EventHandler(this.start_Click);
            // 
            // close
            // 
            this.close.Location = new System.Drawing.Point(296, 340);
            this.close.Name = "close";
            this.close.Size = new System.Drawing.Size(75, 23);
            this.close.TabIndex = 1;
            this.close.Text = "退出";
            this.close.UseVisualStyleBackColor = true;
            this.close.Click += new System.EventHandler(this.close_Click);
            // 
            // low
            // 
            this.low.AutoSize = true;
            this.low.Checked = true;
            this.low.Location = new System.Drawing.Point(49, 59);
            this.low.Name = "low";
            this.low.Size = new System.Drawing.Size(71, 16);
            this.low.TabIndex = 2;
            this.low.TabStop = true;
            this.low.Text = "低通滤波";
            this.low.UseVisualStyleBackColor = true;
            this.low.CheckedChanged += new System.EventHandler(this.low_CheckedChanged);
            // 
            // mid
            // 
            this.mid.AutoSize = true;
            this.mid.Location = new System.Drawing.Point(49, 153);
            this.mid.Name = "mid";
            this.mid.Size = new System.Drawing.Size(71, 16);
            this.mid.TabIndex = 3;
            this.mid.Text = "带通滤波";
            this.mid.UseVisualStyleBackColor = true;
            this.mid.CheckedChanged += new System.EventHandler(this.mid_CheckedChanged);
            // 
            // high
            // 
            this.high.AutoSize = true;
            this.high.Location = new System.Drawing.Point(49, 200);
            this.high.Name = "high";
            this.high.Size = new System.Drawing.Size(71, 16);
            this.high.TabIndex = 4;
            this.high.Text = "高通滤波";
            this.high.UseVisualStyleBackColor = true;
            this.high.CheckedChanged += new System.EventHandler(this.high_CheckedChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(178, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 12);
            this.label1.TabIndex = 6;
            this.label1.Text = "掩码图预览：";
            // 
            // radius1
            // 
            this.radius1.Location = new System.Drawing.Point(196, 254);
            this.radius1.Name = "radius1";
            this.radius1.Size = new System.Drawing.Size(58, 21);
            this.radius1.TabIndex = 7;
            this.radius1.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.radius1.ValueChanged += new System.EventHandler(this.radius1_ValueChanged);
            // 
            // radius2
            // 
            this.radius2.Location = new System.Drawing.Point(196, 293);
            this.radius2.Name = "radius2";
            this.radius2.Size = new System.Drawing.Size(58, 21);
            this.radius2.TabIndex = 8;
            this.radius2.Visible = false;
            this.radius2.ValueChanged += new System.EventHandler(this.radius2_ValueChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(47, 256);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(119, 12);
            this.label2.TabIndex = 9;
            this.label2.Text = "低通掩码半径（%）：";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(47, 295);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(0, 12);
            this.label3.TabIndex = 10;
            this.label3.Visible = false;
            // 
            // midStop
            // 
            this.midStop.AutoSize = true;
            this.midStop.Location = new System.Drawing.Point(49, 106);
            this.midStop.Name = "midStop";
            this.midStop.Size = new System.Drawing.Size(71, 16);
            this.midStop.TabIndex = 11;
            this.midStop.TabStop = true;
            this.midStop.Text = "带阻滤波";
            this.midStop.UseVisualStyleBackColor = true;
            this.midStop.CheckedChanged += new System.EventHandler(this.midStop_CheckedChanged);
            // 
            // granularity
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(452, 395);
            this.ControlBox = false;
            this.Controls.Add(this.midStop);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.radius2);
            this.Controls.Add(this.radius1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.high);
            this.Controls.Add(this.mid);
            this.Controls.Add(this.low);
            this.Controls.Add(this.close);
            this.Controls.Add(this.start);
            this.Name = "granularity";
            this.Text = "频率成分滤波";
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.granularity_Paint);
            ((System.ComponentModel.ISupportInitialize)(this.radius1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radius2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button start;
        private System.Windows.Forms.Button close;
        private System.Windows.Forms.RadioButton low;
        private System.Windows.Forms.RadioButton mid;
        private System.Windows.Forms.RadioButton midStop;
        private System.Windows.Forms.RadioButton high;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown radius1;
        private System.Windows.Forms.NumericUpDown radius2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;

        private byte tempFlag;
        private byte[] radius;        
    }
}