namespace segmentatioin
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
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.open = new System.Windows.Forms.Button();
            this.close = new System.Windows.Forms.Button();
            this.hough = new System.Windows.Forms.Button();
            this.threshold = new System.Windows.Forms.Button();
            this.clus = new System.Windows.Forms.Button();
            this.overRelax = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // open
            // 
            this.open.Location = new System.Drawing.Point(37, 46);
            this.open.Name = "open";
            this.open.Size = new System.Drawing.Size(75, 23);
            this.open.TabIndex = 0;
            this.open.Text = "打开图像";
            this.open.UseVisualStyleBackColor = true;
            this.open.Click += new System.EventHandler(this.open_Click);
            // 
            // close
            // 
            this.close.Location = new System.Drawing.Point(37, 92);
            this.close.Name = "close";
            this.close.Size = new System.Drawing.Size(75, 23);
            this.close.TabIndex = 1;
            this.close.Text = "关闭";
            this.close.UseVisualStyleBackColor = true;
            this.close.Click += new System.EventHandler(this.close_Click);
            // 
            // hough
            // 
            this.hough.Location = new System.Drawing.Point(37, 150);
            this.hough.Name = "hough";
            this.hough.Size = new System.Drawing.Size(75, 23);
            this.hough.TabIndex = 2;
            this.hough.Text = "Hough变换";
            this.hough.UseVisualStyleBackColor = true;
            this.hough.Click += new System.EventHandler(this.hough_Click);
            // 
            // threshold
            // 
            this.threshold.Location = new System.Drawing.Point(37, 196);
            this.threshold.Name = "threshold";
            this.threshold.Size = new System.Drawing.Size(75, 23);
            this.threshold.TabIndex = 3;
            this.threshold.Text = "阈值法";
            this.threshold.UseVisualStyleBackColor = true;
            this.threshold.Click += new System.EventHandler(this.threshold_Click);
            // 
            // clus
            // 
            this.clus.Location = new System.Drawing.Point(37, 242);
            this.clus.Name = "clus";
            this.clus.Size = new System.Drawing.Size(75, 23);
            this.clus.TabIndex = 4;
            this.clus.Text = "空间聚类法";
            this.clus.UseVisualStyleBackColor = true;
            this.clus.Click += new System.EventHandler(this.clus_Click);
            // 
            // overRelax
            // 
            this.overRelax.Location = new System.Drawing.Point(37, 288);
            this.overRelax.Name = "overRelax";
            this.overRelax.Size = new System.Drawing.Size(75, 23);
            this.overRelax.TabIndex = 5;
            this.overRelax.Text = "松弛迭代法";
            this.overRelax.UseVisualStyleBackColor = true;
            this.overRelax.Click += new System.EventHandler(this.overRelax_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(792, 565);
            this.Controls.Add(this.overRelax);
            this.Controls.Add(this.clus);
            this.Controls.Add(this.threshold);
            this.Controls.Add(this.hough);
            this.Controls.Add(this.close);
            this.Controls.Add(this.open);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.Form1_Paint);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button open;
        private System.Windows.Forms.Button close;
        private string curFileName = null;
        private System.Drawing.Bitmap curBitmap = null;
        private System.Windows.Forms.Button hough;
        private System.Windows.Forms.Button threshold;
        private System.Windows.Forms.Button clus;
        private System.Windows.Forms.Button overRelax;
    }
}

