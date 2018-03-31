namespace color
{
    partial class sharpColor
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
            this.rgbSha = new System.Windows.Forms.RadioButton();
            this.hsiSha = new System.Windows.Forms.RadioButton();
            this.start = new System.Windows.Forms.Button();
            this.close = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // rgbSha
            // 
            this.rgbSha.AutoSize = true;
            this.rgbSha.Checked = true;
            this.rgbSha.Location = new System.Drawing.Point(33, 34);
            this.rgbSha.Name = "rgbSha";
            this.rgbSha.Size = new System.Drawing.Size(113, 16);
            this.rgbSha.TabIndex = 0;
            this.rgbSha.TabStop = true;
            this.rgbSha.Text = "RGB空间锐化处理";
            this.rgbSha.UseVisualStyleBackColor = true;
            // 
            // hsiSha
            // 
            this.hsiSha.AutoSize = true;
            this.hsiSha.Location = new System.Drawing.Point(33, 75);
            this.hsiSha.Name = "hsiSha";
            this.hsiSha.Size = new System.Drawing.Size(89, 16);
            this.hsiSha.TabIndex = 1;
            this.hsiSha.Text = "HSI锐化处理";
            this.hsiSha.UseVisualStyleBackColor = true;
            // 
            // start
            // 
            this.start.Location = new System.Drawing.Point(23, 117);
            this.start.Name = "start";
            this.start.Size = new System.Drawing.Size(75, 23);
            this.start.TabIndex = 2;
            this.start.Text = "确定";
            this.start.UseVisualStyleBackColor = true;
            this.start.Click += new System.EventHandler(this.start_Click);
            // 
            // close
            // 
            this.close.Location = new System.Drawing.Point(119, 117);
            this.close.Name = "close";
            this.close.Size = new System.Drawing.Size(75, 23);
            this.close.TabIndex = 3;
            this.close.Text = "退出";
            this.close.UseVisualStyleBackColor = true;
            this.close.Click += new System.EventHandler(this.close_Click);
            // 
            // sharpColor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(226, 165);
            this.ControlBox = false;
            this.Controls.Add(this.close);
            this.Controls.Add(this.start);
            this.Controls.Add(this.hsiSha);
            this.Controls.Add(this.rgbSha);
            this.Name = "sharpColor";
            this.Text = "彩色图像锐化处理";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RadioButton rgbSha;
        private System.Windows.Forms.RadioButton hsiSha;
        private System.Windows.Forms.Button start;
        private System.Windows.Forms.Button close;
    }
}