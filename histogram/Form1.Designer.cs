namespace histogram
{
    partial class Form1
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
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.close = new System.Windows.Forms.Button();
            this.open = new System.Windows.Forms.Button();
            this.histogram = new System.Windows.Forms.Button();
            this.linearPO = new System.Windows.Forms.Button();
            this.stretch = new System.Windows.Forms.Button();
            this.equalization = new System.Windows.Forms.Button();
            this.shaping = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // close
            // 
            this.close.Location = new System.Drawing.Point(12, 63);
            this.close.Name = "close";
            this.close.Size = new System.Drawing.Size(75, 23);
            this.close.TabIndex = 5;
            this.close.Text = "关闭";
            this.close.UseVisualStyleBackColor = true;
            this.close.Click += new System.EventHandler(this.close_Click);
            // 
            // open
            // 
            this.open.Location = new System.Drawing.Point(12, 12);
            this.open.Name = "open";
            this.open.Size = new System.Drawing.Size(75, 23);
            this.open.TabIndex = 3;
            this.open.Text = "打开图像";
            this.open.UseVisualStyleBackColor = true;
            this.open.Click += new System.EventHandler(this.open_Click);
            // 
            // histogram
            // 
            this.histogram.Location = new System.Drawing.Point(13, 111);
            this.histogram.Name = "histogram";
            this.histogram.Size = new System.Drawing.Size(75, 23);
            this.histogram.TabIndex = 6;
            this.histogram.Text = "绘制直方图";
            this.histogram.UseVisualStyleBackColor = true;
            this.histogram.Click += new System.EventHandler(this.histogram_Click);
            // 
            // linearPO
            // 
            this.linearPO.Location = new System.Drawing.Point(13, 160);
            this.linearPO.Name = "linearPO";
            this.linearPO.Size = new System.Drawing.Size(75, 23);
            this.linearPO.TabIndex = 7;
            this.linearPO.Text = "线性点运算";
            this.linearPO.UseVisualStyleBackColor = true;
            this.linearPO.Click += new System.EventHandler(this.linearPO_Click);
            // 
            // stretch
            // 
            this.stretch.Location = new System.Drawing.Point(12, 209);
            this.stretch.Name = "stretch";
            this.stretch.Size = new System.Drawing.Size(75, 23);
            this.stretch.TabIndex = 8;
            this.stretch.Text = "灰度拉伸";
            this.stretch.UseVisualStyleBackColor = true;
            this.stretch.Click += new System.EventHandler(this.stretch_Click);
            // 
            // equalization
            // 
            this.equalization.Location = new System.Drawing.Point(13, 258);
            this.equalization.Name = "equalization";
            this.equalization.Size = new System.Drawing.Size(75, 23);
            this.equalization.TabIndex = 9;
            this.equalization.Text = "直方图均衡化";
            this.equalization.UseVisualStyleBackColor = true;
            this.equalization.Click += new System.EventHandler(this.equalization_Click);
            // 
            // shaping
            // 
            this.shaping.Location = new System.Drawing.Point(13, 306);
            this.shaping.Name = "shaping";
            this.shaping.Size = new System.Drawing.Size(75, 23);
            this.shaping.TabIndex = 10;
            this.shaping.Text = "直方图匹配";
            this.shaping.UseVisualStyleBackColor = true;
            this.shaping.Click += new System.EventHandler(this.shaping_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 561);
            this.Controls.Add(this.shaping);
            this.Controls.Add(this.equalization);
            this.Controls.Add(this.stretch);
            this.Controls.Add(this.linearPO);
            this.Controls.Add(this.histogram);
            this.Controls.Add(this.close);
            this.Controls.Add(this.open);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.Form1_Paint);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button close;
        private System.Windows.Forms.Button open;
        private System.Windows.Forms.Button histogram;
        private System.Windows.Forms.Button linearPO;
        private System.Windows.Forms.Button stretch;
        private System.Windows.Forms.Button equalization;
        private System.Windows.Forms.Button shaping;
    }
}

