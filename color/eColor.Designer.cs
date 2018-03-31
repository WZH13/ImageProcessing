namespace color
{
    partial class eColor
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
            this.rgbEqu = new System.Windows.Forms.RadioButton();
            this.intEqu = new System.Windows.Forms.RadioButton();
            this.SuspendLayout();
            // 
            // start
            // 
            this.start.Location = new System.Drawing.Point(23, 117);
            this.start.Name = "start";
            this.start.Size = new System.Drawing.Size(75, 23);
            this.start.TabIndex = 0;
            this.start.Text = "确定";
            this.start.UseVisualStyleBackColor = true;
            this.start.Click += new System.EventHandler(this.start_Click);
            // 
            // close
            // 
            this.close.Location = new System.Drawing.Point(119, 117);
            this.close.Name = "close";
            this.close.Size = new System.Drawing.Size(75, 23);
            this.close.TabIndex = 1;
            this.close.Text = "退出";
            this.close.UseVisualStyleBackColor = true;
            this.close.Click += new System.EventHandler(this.close_Click);
            // 
            // rgbEqu
            // 
            this.rgbEqu.AutoSize = true;
            this.rgbEqu.Checked = true;
            this.rgbEqu.Location = new System.Drawing.Point(33, 34);
            this.rgbEqu.Name = "rgbEqu";
            this.rgbEqu.Size = new System.Drawing.Size(137, 16);
            this.rgbEqu.TabIndex = 2;
            this.rgbEqu.TabStop = true;
            this.rgbEqu.Text = "RGB空间分量均衡化法";
            this.rgbEqu.UseVisualStyleBackColor = true;
            // 
            // intEqu
            // 
            this.intEqu.AutoSize = true;
            this.intEqu.Location = new System.Drawing.Point(33, 75);
            this.intEqu.Name = "intEqu";
            this.intEqu.Size = new System.Drawing.Size(137, 16);
            this.intEqu.TabIndex = 3;
            this.intEqu.Text = "HSI空间亮度均衡化法";
            this.intEqu.UseVisualStyleBackColor = true;
            // 
            // eColor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(226, 165);
            this.ControlBox = false;
            this.Controls.Add(this.intEqu);
            this.Controls.Add(this.rgbEqu);
            this.Controls.Add(this.close);
            this.Controls.Add(this.start);
            this.Name = "eColor";
            this.Text = "彩色图像直方图均衡化";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button start;
        private System.Windows.Forms.Button close;
        private System.Windows.Forms.RadioButton rgbEqu;
        private System.Windows.Forms.RadioButton intEqu;
    }
}