namespace edge
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
            this.mask = new System.Windows.Forms.Button();
            this.gaussian = new System.Windows.Forms.Button();
            this.canny = new System.Windows.Forms.Button();
            this.morph = new System.Windows.Forms.Button();
            this.wavelet = new System.Windows.Forms.Button();
            this.pyramid = new System.Windows.Forms.Button();
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
            // mask
            // 
            this.mask.Location = new System.Drawing.Point(37, 150);
            this.mask.Name = "mask";
            this.mask.Size = new System.Drawing.Size(75, 23);
            this.mask.TabIndex = 2;
            this.mask.Text = "模板算子法";
            this.mask.UseVisualStyleBackColor = true;
            this.mask.Click += new System.EventHandler(this.mask_Click);
            // 
            // gaussian
            // 
            this.gaussian.Location = new System.Drawing.Point(37, 196);
            this.gaussian.Name = "gaussian";
            this.gaussian.Size = new System.Drawing.Size(75, 23);
            this.gaussian.TabIndex = 3;
            this.gaussian.Text = "高斯算子";
            this.gaussian.UseVisualStyleBackColor = true;
            this.gaussian.Click += new System.EventHandler(this.gaussian_Click);
            // 
            // canny
            // 
            this.canny.Location = new System.Drawing.Point(37, 242);
            this.canny.Name = "canny";
            this.canny.Size = new System.Drawing.Size(75, 23);
            this.canny.TabIndex = 4;
            this.canny.Text = "Canny算子";
            this.canny.UseVisualStyleBackColor = true;
            this.canny.Click += new System.EventHandler(this.canny_Click);
            // 
            // morph
            // 
            this.morph.Location = new System.Drawing.Point(37, 288);
            this.morph.Name = "morph";
            this.morph.Size = new System.Drawing.Size(75, 23);
            this.morph.TabIndex = 5;
            this.morph.Text = "灰度形态学";
            this.morph.UseVisualStyleBackColor = true;
            this.morph.Click += new System.EventHandler(this.morph_Click);
            // 
            // wavelet
            // 
            this.wavelet.Location = new System.Drawing.Point(37, 334);
            this.wavelet.Name = "wavelet";
            this.wavelet.Size = new System.Drawing.Size(75, 23);
            this.wavelet.TabIndex = 6;
            this.wavelet.Text = "小波变换";
            this.wavelet.UseVisualStyleBackColor = true;
            this.wavelet.Click += new System.EventHandler(this.wavelet_Click);
            // 
            // pyramid
            // 
            this.pyramid.Location = new System.Drawing.Point(37, 380);
            this.pyramid.Name = "pyramid";
            this.pyramid.Size = new System.Drawing.Size(75, 23);
            this.pyramid.TabIndex = 7;
            this.pyramid.Text = "金字塔";
            this.pyramid.UseVisualStyleBackColor = true;
            this.pyramid.Click += new System.EventHandler(this.pyramid_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(792, 565);
            this.Controls.Add(this.pyramid);
            this.Controls.Add(this.wavelet);
            this.Controls.Add(this.morph);
            this.Controls.Add(this.canny);
            this.Controls.Add(this.gaussian);
            this.Controls.Add(this.mask);
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
        private System.Windows.Forms.Button mask;
        private System.Windows.Forms.Button gaussian;
        private System.Windows.Forms.Button canny;
        private System.Windows.Forms.Button morph;
        private System.Windows.Forms.Button wavelet;
        private System.Windows.Forms.Button pyramid;
    }
}

