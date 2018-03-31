namespace smooth
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
            this.noise = new System.Windows.Forms.Button();
            this.meanMedian = new System.Windows.Forms.Button();
            this.grayMor = new System.Windows.Forms.Button();
            this.wavelet = new System.Windows.Forms.Button();
            this.gauss = new System.Windows.Forms.Button();
            this.statistic = new System.Windows.Forms.Button();
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
            // noise
            // 
            this.noise.Location = new System.Drawing.Point(37, 150);
            this.noise.Name = "noise";
            this.noise.Size = new System.Drawing.Size(75, 23);
            this.noise.TabIndex = 2;
            this.noise.Text = "噪声模型";
            this.noise.UseVisualStyleBackColor = true;
            this.noise.Click += new System.EventHandler(this.noise_Click);
            // 
            // meanMedian
            // 
            this.meanMedian.Location = new System.Drawing.Point(37, 196);
            this.meanMedian.Name = "meanMedian";
            this.meanMedian.Size = new System.Drawing.Size(75, 23);
            this.meanMedian.TabIndex = 3;
            this.meanMedian.Text = "均值与中值";
            this.meanMedian.UseVisualStyleBackColor = true;
            this.meanMedian.Click += new System.EventHandler(this.meanMedian_Click);
            // 
            // grayMor
            // 
            this.grayMor.Location = new System.Drawing.Point(37, 242);
            this.grayMor.Name = "grayMor";
            this.grayMor.Size = new System.Drawing.Size(75, 23);
            this.grayMor.TabIndex = 4;
            this.grayMor.Text = "灰度形态学";
            this.grayMor.UseVisualStyleBackColor = true;
            this.grayMor.Click += new System.EventHandler(this.grayMor_Click);
            // 
            // wavelet
            // 
            this.wavelet.Location = new System.Drawing.Point(37, 288);
            this.wavelet.Name = "wavelet";
            this.wavelet.Size = new System.Drawing.Size(75, 23);
            this.wavelet.TabIndex = 5;
            this.wavelet.Text = "小波变换";
            this.wavelet.UseVisualStyleBackColor = true;
            this.wavelet.Click += new System.EventHandler(this.wavelet_Click);
            // 
            // gauss
            // 
            this.gauss.Location = new System.Drawing.Point(37, 334);
            this.gauss.Name = "gauss";
            this.gauss.Size = new System.Drawing.Size(75, 23);
            this.gauss.TabIndex = 6;
            this.gauss.Text = "高斯滤波";
            this.gauss.UseVisualStyleBackColor = true;
            this.gauss.Click += new System.EventHandler(this.gauss_Click);
            // 
            // statistic
            // 
            this.statistic.Location = new System.Drawing.Point(37, 380);
            this.statistic.Name = "statistic";
            this.statistic.Size = new System.Drawing.Size(75, 23);
            this.statistic.TabIndex = 7;
            this.statistic.Text = "统计方法";
            this.statistic.UseVisualStyleBackColor = true;
            this.statistic.Click += new System.EventHandler(this.statistic_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(792, 565);
            this.Controls.Add(this.statistic);
            this.Controls.Add(this.gauss);
            this.Controls.Add(this.wavelet);
            this.Controls.Add(this.grayMor);
            this.Controls.Add(this.meanMedian);
            this.Controls.Add(this.noise);
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
        private System.Windows.Forms.Button noise;
        private System.Windows.Forms.Button meanMedian;
        private System.Windows.Forms.Button grayMor;
        private System.Windows.Forms.Button wavelet;
        private System.Windows.Forms.Button gauss;
        private System.Windows.Forms.Button statistic;
    }
}

