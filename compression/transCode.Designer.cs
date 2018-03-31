namespace compression
{
    partial class transCode
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.size16 = new System.Windows.Forms.RadioButton();
            this.size8 = new System.Windows.Forms.RadioButton();
            this.size4 = new System.Windows.Forms.RadioButton();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.ratio8 = new System.Windows.Forms.RadioButton();
            this.ratio4 = new System.Windows.Forms.RadioButton();
            this.ratio2 = new System.Windows.Forms.RadioButton();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // start
            // 
            this.start.Location = new System.Drawing.Point(51, 218);
            this.start.Name = "start";
            this.start.Size = new System.Drawing.Size(75, 23);
            this.start.TabIndex = 0;
            this.start.Text = "确定";
            this.start.UseVisualStyleBackColor = true;
            this.start.Click += new System.EventHandler(this.start_Click);
            // 
            // close
            // 
            this.close.Location = new System.Drawing.Point(225, 218);
            this.close.Name = "close";
            this.close.Size = new System.Drawing.Size(75, 23);
            this.close.TabIndex = 1;
            this.close.Text = "退出";
            this.close.UseVisualStyleBackColor = true;
            this.close.Click += new System.EventHandler(this.close_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.size16);
            this.groupBox1.Controls.Add(this.size8);
            this.groupBox1.Controls.Add(this.size4);
            this.groupBox1.Location = new System.Drawing.Point(26, 25);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(125, 164);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "子图像尺寸大小";
            // 
            // size16
            // 
            this.size16.AutoSize = true;
            this.size16.Location = new System.Drawing.Point(25, 122);
            this.size16.Name = "size16";
            this.size16.Size = new System.Drawing.Size(59, 16);
            this.size16.TabIndex = 2;
            this.size16.Text = "16×16";
            this.size16.UseVisualStyleBackColor = true;
            this.size16.CheckedChanged += new System.EventHandler(this.size16_CheckedChanged);
            // 
            // size8
            // 
            this.size8.AutoSize = true;
            this.size8.Location = new System.Drawing.Point(25, 78);
            this.size8.Name = "size8";
            this.size8.Size = new System.Drawing.Size(47, 16);
            this.size8.TabIndex = 1;
            this.size8.Text = "8×8";
            this.size8.UseVisualStyleBackColor = true;
            this.size8.CheckedChanged += new System.EventHandler(this.size8_CheckedChanged);
            // 
            // size4
            // 
            this.size4.AutoSize = true;
            this.size4.Checked = true;
            this.size4.Location = new System.Drawing.Point(25, 34);
            this.size4.Name = "size4";
            this.size4.Size = new System.Drawing.Size(47, 16);
            this.size4.TabIndex = 0;
            this.size4.TabStop = true;
            this.size4.Text = "4×4";
            this.size4.UseVisualStyleBackColor = true;
            this.size4.CheckedChanged += new System.EventHandler(this.size4_CheckedChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.ratio8);
            this.groupBox2.Controls.Add(this.ratio4);
            this.groupBox2.Controls.Add(this.ratio2);
            this.groupBox2.Location = new System.Drawing.Point(204, 25);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(125, 164);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "压缩比";
            // 
            // ratio8
            // 
            this.ratio8.AutoSize = true;
            this.ratio8.Location = new System.Drawing.Point(30, 122);
            this.ratio8.Name = "ratio8";
            this.ratio8.Size = new System.Drawing.Size(47, 16);
            this.ratio8.TabIndex = 2;
            this.ratio8.Text = "8：1";
            this.ratio8.UseVisualStyleBackColor = true;
            this.ratio8.CheckedChanged += new System.EventHandler(this.ratio8_CheckedChanged);
            // 
            // ratio4
            // 
            this.ratio4.AutoSize = true;
            this.ratio4.Location = new System.Drawing.Point(30, 78);
            this.ratio4.Name = "ratio4";
            this.ratio4.Size = new System.Drawing.Size(47, 16);
            this.ratio4.TabIndex = 1;
            this.ratio4.Text = "4：1";
            this.ratio4.UseVisualStyleBackColor = true;
            this.ratio4.CheckedChanged += new System.EventHandler(this.ratio4_CheckedChanged);
            // 
            // ratio2
            // 
            this.ratio2.AutoSize = true;
            this.ratio2.Checked = true;
            this.ratio2.Location = new System.Drawing.Point(30, 34);
            this.ratio2.Name = "ratio2";
            this.ratio2.Size = new System.Drawing.Size(47, 16);
            this.ratio2.TabIndex = 0;
            this.ratio2.TabStop = true;
            this.ratio2.Text = "2：1";
            this.ratio2.UseVisualStyleBackColor = true;
            this.ratio2.CheckedChanged += new System.EventHandler(this.ratio2_CheckedChanged);
            // 
            // transCode
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(357, 265);
            this.ControlBox = false;
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.close);
            this.Controls.Add(this.start);
            this.Name = "transCode";
            this.Text = "傅里叶变换编码";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button start;
        private System.Windows.Forms.Button close;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton size16;
        private System.Windows.Forms.RadioButton size8;
        private System.Windows.Forms.RadioButton size4;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RadioButton ratio8;
        private System.Windows.Forms.RadioButton ratio4;
        private System.Windows.Forms.RadioButton ratio2;
        private byte sizeNum;
        private byte ratioNum;
    }
}