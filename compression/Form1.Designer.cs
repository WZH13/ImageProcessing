namespace compression
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
            this.huffman = new System.Windows.Forms.Button();
            this.shanFan = new System.Windows.Forms.Button();
            this.shannon = new System.Windows.Forms.Button();
            this.rle = new System.Windows.Forms.Button();
            this.lzw = new System.Windows.Forms.Button();
            this.dpcm = new System.Windows.Forms.Button();
            this.transform = new System.Windows.Forms.Button();
            this.wavelet = new System.Windows.Forms.Button();
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
            // huffman
            // 
            this.huffman.Location = new System.Drawing.Point(37, 150);
            this.huffman.Name = "huffman";
            this.huffman.Size = new System.Drawing.Size(75, 23);
            this.huffman.TabIndex = 2;
            this.huffman.Text = "哈夫曼编码";
            this.huffman.UseVisualStyleBackColor = true;
            this.huffman.Click += new System.EventHandler(this.huffman_Click);
            // 
            // shanFan
            // 
            this.shanFan.Location = new System.Drawing.Point(37, 242);
            this.shanFan.Name = "shanFan";
            this.shanFan.Size = new System.Drawing.Size(75, 23);
            this.shanFan.TabIndex = 3;
            this.shanFan.Text = "香-弗编码";
            this.shanFan.UseVisualStyleBackColor = true;
            this.shanFan.Click += new System.EventHandler(this.shanFan_Click);
            // 
            // shannon
            // 
            this.shannon.Location = new System.Drawing.Point(37, 196);
            this.shannon.Name = "shannon";
            this.shannon.Size = new System.Drawing.Size(75, 23);
            this.shannon.TabIndex = 4;
            this.shannon.Text = "香农编码";
            this.shannon.UseVisualStyleBackColor = true;
            this.shannon.Click += new System.EventHandler(this.shannon_Click);
            // 
            // rle
            // 
            this.rle.Location = new System.Drawing.Point(37, 288);
            this.rle.Name = "rle";
            this.rle.Size = new System.Drawing.Size(75, 23);
            this.rle.TabIndex = 5;
            this.rle.Text = "行程运算";
            this.rle.UseVisualStyleBackColor = true;
            this.rle.Click += new System.EventHandler(this.rle_Click);
            // 
            // lzw
            // 
            this.lzw.Location = new System.Drawing.Point(37, 334);
            this.lzw.Name = "lzw";
            this.lzw.Size = new System.Drawing.Size(75, 23);
            this.lzw.TabIndex = 6;
            this.lzw.Text = "LZW运算";
            this.lzw.UseVisualStyleBackColor = true;
            this.lzw.Click += new System.EventHandler(this.lzw_Click);
            // 
            // dpcm
            // 
            this.dpcm.Location = new System.Drawing.Point(37, 380);
            this.dpcm.Name = "dpcm";
            this.dpcm.Size = new System.Drawing.Size(75, 23);
            this.dpcm.TabIndex = 7;
            this.dpcm.Text = "预测编码";
            this.dpcm.UseVisualStyleBackColor = true;
            this.dpcm.Click += new System.EventHandler(this.dpcm_Click);
            // 
            // transform
            // 
            this.transform.Location = new System.Drawing.Point(37, 426);
            this.transform.Name = "transform";
            this.transform.Size = new System.Drawing.Size(75, 23);
            this.transform.TabIndex = 8;
            this.transform.Text = "傅里叶变换";
            this.transform.UseVisualStyleBackColor = true;
            this.transform.Click += new System.EventHandler(this.transform_Click);
            // 
            // wavelet
            // 
            this.wavelet.Location = new System.Drawing.Point(37, 472);
            this.wavelet.Name = "wavelet";
            this.wavelet.Size = new System.Drawing.Size(75, 23);
            this.wavelet.TabIndex = 9;
            this.wavelet.Text = "小波变换";
            this.wavelet.UseVisualStyleBackColor = true;
            this.wavelet.Click += new System.EventHandler(this.wavelet_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(792, 565);
            this.Controls.Add(this.wavelet);
            this.Controls.Add(this.transform);
            this.Controls.Add(this.dpcm);
            this.Controls.Add(this.lzw);
            this.Controls.Add(this.rle);
            this.Controls.Add(this.shannon);
            this.Controls.Add(this.shanFan);
            this.Controls.Add(this.huffman);
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
        private System.Windows.Forms.Button huffman;
        private string curFileName = null;
        private System.Drawing.Bitmap curBitmap = null;
        private System.Windows.Forms.Button shanFan;
        private System.Windows.Forms.Button shannon;
        private System.Windows.Forms.Button rle;
        private System.Windows.Forms.Button lzw;
        private System.Windows.Forms.Button dpcm;
        private System.Windows.Forms.Button transform;
        private System.Windows.Forms.Button wavelet;
    }
}

