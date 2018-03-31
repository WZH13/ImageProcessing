namespace edge
{
    partial class mask
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
            this.kirsch = new System.Windows.Forms.RadioButton();
            this.laplacian3 = new System.Windows.Forms.RadioButton();
            this.laplacian2 = new System.Windows.Forms.RadioButton();
            this.laplacian1 = new System.Windows.Forms.RadioButton();
            this.sobel = new System.Windows.Forms.RadioButton();
            this.prewitt = new System.Windows.Forms.RadioButton();
            this.roberts = new System.Windows.Forms.RadioButton();
            this.label1 = new System.Windows.Forms.Label();
            this.thresholding = new System.Windows.Forms.NumericUpDown();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.thresholding)).BeginInit();
            this.SuspendLayout();
            // 
            // start
            // 
            this.start.Location = new System.Drawing.Point(40, 440);
            this.start.Name = "start";
            this.start.Size = new System.Drawing.Size(75, 23);
            this.start.TabIndex = 0;
            this.start.Text = "确定";
            this.start.UseVisualStyleBackColor = true;
            this.start.Click += new System.EventHandler(this.start_Click);
            // 
            // close
            // 
            this.close.Location = new System.Drawing.Point(149, 440);
            this.close.Name = "close";
            this.close.Size = new System.Drawing.Size(75, 23);
            this.close.TabIndex = 1;
            this.close.Text = "退出";
            this.close.UseVisualStyleBackColor = true;
            this.close.Click += new System.EventHandler(this.close_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.kirsch);
            this.groupBox1.Controls.Add(this.laplacian3);
            this.groupBox1.Controls.Add(this.laplacian2);
            this.groupBox1.Controls.Add(this.laplacian1);
            this.groupBox1.Controls.Add(this.sobel);
            this.groupBox1.Controls.Add(this.prewitt);
            this.groupBox1.Controls.Add(this.roberts);
            this.groupBox1.Location = new System.Drawing.Point(40, 22);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(184, 346);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "边缘检测模板";
            // 
            // kirsch
            // 
            this.kirsch.AutoSize = true;
            this.kirsch.Location = new System.Drawing.Point(30, 306);
            this.kirsch.Name = "kirsch";
            this.kirsch.Size = new System.Drawing.Size(59, 16);
            this.kirsch.TabIndex = 6;
            this.kirsch.Text = "Kirsch";
            this.kirsch.UseVisualStyleBackColor = true;
            this.kirsch.CheckedChanged += new System.EventHandler(this.kirsch_CheckedChanged);
            // 
            // laplacian3
            // 
            this.laplacian3.AutoSize = true;
            this.laplacian3.Location = new System.Drawing.Point(30, 260);
            this.laplacian3.Name = "laplacian3";
            this.laplacian3.Size = new System.Drawing.Size(83, 16);
            this.laplacian3.TabIndex = 5;
            this.laplacian3.Text = "Laplacian3";
            this.laplacian3.UseVisualStyleBackColor = true;
            this.laplacian3.CheckedChanged += new System.EventHandler(this.laplacian3_CheckedChanged);
            // 
            // laplacian2
            // 
            this.laplacian2.AutoSize = true;
            this.laplacian2.Location = new System.Drawing.Point(30, 214);
            this.laplacian2.Name = "laplacian2";
            this.laplacian2.Size = new System.Drawing.Size(83, 16);
            this.laplacian2.TabIndex = 4;
            this.laplacian2.Text = "Laplacian2";
            this.laplacian2.UseVisualStyleBackColor = true;
            this.laplacian2.CheckedChanged += new System.EventHandler(this.laplacian2_CheckedChanged);
            // 
            // laplacian1
            // 
            this.laplacian1.AutoSize = true;
            this.laplacian1.Location = new System.Drawing.Point(30, 168);
            this.laplacian1.Name = "laplacian1";
            this.laplacian1.Size = new System.Drawing.Size(83, 16);
            this.laplacian1.TabIndex = 3;
            this.laplacian1.Text = "Laplacian1";
            this.laplacian1.UseVisualStyleBackColor = true;
            this.laplacian1.CheckedChanged += new System.EventHandler(this.laplacian1_CheckedChanged);
            // 
            // sobel
            // 
            this.sobel.AutoSize = true;
            this.sobel.Location = new System.Drawing.Point(30, 122);
            this.sobel.Name = "sobel";
            this.sobel.Size = new System.Drawing.Size(53, 16);
            this.sobel.TabIndex = 2;
            this.sobel.Text = "Sobel";
            this.sobel.UseVisualStyleBackColor = true;
            this.sobel.CheckedChanged += new System.EventHandler(this.sobel_CheckedChanged);
            // 
            // prewitt
            // 
            this.prewitt.AutoSize = true;
            this.prewitt.Location = new System.Drawing.Point(30, 76);
            this.prewitt.Name = "prewitt";
            this.prewitt.Size = new System.Drawing.Size(65, 16);
            this.prewitt.TabIndex = 1;
            this.prewitt.Text = "Prewitt";
            this.prewitt.UseVisualStyleBackColor = true;
            this.prewitt.CheckedChanged += new System.EventHandler(this.prewitt_CheckedChanged);
            // 
            // roberts
            // 
            this.roberts.AutoSize = true;
            this.roberts.Checked = true;
            this.roberts.Location = new System.Drawing.Point(30, 30);
            this.roberts.Name = "roberts";
            this.roberts.Size = new System.Drawing.Size(65, 16);
            this.roberts.TabIndex = 0;
            this.roberts.TabStop = true;
            this.roberts.Text = "Roberts";
            this.roberts.UseVisualStyleBackColor = true;
            this.roberts.CheckedChanged += new System.EventHandler(this.roberts_CheckedChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(59, 394);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 3;
            this.label1.Text = "阈值：";
            // 
            // thresholding
            // 
            this.thresholding.Location = new System.Drawing.Point(116, 392);
            this.thresholding.Maximum = new decimal(new int[] {
            1500,
            0,
            0,
            0});
            this.thresholding.Name = "thresholding";
            this.thresholding.Size = new System.Drawing.Size(82, 21);
            this.thresholding.TabIndex = 4;
            this.thresholding.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            // 
            // mask
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(262, 485);
            this.ControlBox = false;
            this.Controls.Add(this.thresholding);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.close);
            this.Controls.Add(this.start);
            this.Name = "mask";
            this.Text = "模板算子";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.thresholding)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button start;
        private System.Windows.Forms.Button close;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton kirsch;
        private System.Windows.Forms.RadioButton laplacian3;
        private System.Windows.Forms.RadioButton laplacian2;
        private System.Windows.Forms.RadioButton laplacian1;
        private System.Windows.Forms.RadioButton sobel;
        private System.Windows.Forms.RadioButton prewitt;
        private System.Windows.Forms.RadioButton roberts;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown thresholding;

        private byte operMask;
    }
}