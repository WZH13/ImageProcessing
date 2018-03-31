namespace color
{
    partial class smoothColor
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
            this.lengthS = new System.Windows.Forms.NumericUpDown();
            this.rgbS = new System.Windows.Forms.RadioButton();
            this.hsiS = new System.Windows.Forms.RadioButton();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.lengthS)).BeginInit();
            this.SuspendLayout();
            // 
            // start
            // 
            this.start.Location = new System.Drawing.Point(57, 157);
            this.start.Name = "start";
            this.start.Size = new System.Drawing.Size(75, 23);
            this.start.TabIndex = 0;
            this.start.Text = "确定";
            this.start.UseVisualStyleBackColor = true;
            this.start.Click += new System.EventHandler(this.start_Click);
            // 
            // close
            // 
            this.close.Location = new System.Drawing.Point(244, 157);
            this.close.Name = "close";
            this.close.Size = new System.Drawing.Size(75, 23);
            this.close.TabIndex = 1;
            this.close.Text = "退出";
            this.close.UseVisualStyleBackColor = true;
            this.close.Click += new System.EventHandler(this.close_Click);
            // 
            // lengthS
            // 
            this.lengthS.Increment = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.lengthS.Location = new System.Drawing.Point(267, 69);
            this.lengthS.Maximum = new decimal(new int[] {
            7,
            0,
            0,
            0});
            this.lengthS.Minimum = new decimal(new int[] {
            3,
            0,
            0,
            0});
            this.lengthS.Name = "lengthS";
            this.lengthS.Size = new System.Drawing.Size(57, 21);
            this.lengthS.TabIndex = 2;
            this.lengthS.Value = new decimal(new int[] {
            3,
            0,
            0,
            0});
            // 
            // rgbS
            // 
            this.rgbS.AutoSize = true;
            this.rgbS.Checked = true;
            this.rgbS.Location = new System.Drawing.Point(27, 42);
            this.rgbS.Name = "rgbS";
            this.rgbS.Size = new System.Drawing.Size(113, 16);
            this.rgbS.TabIndex = 3;
            this.rgbS.TabStop = true;
            this.rgbS.Text = "RGB空间平滑处理";
            this.rgbS.UseVisualStyleBackColor = true;
            // 
            // hsiS
            // 
            this.hsiS.AutoSize = true;
            this.hsiS.Location = new System.Drawing.Point(27, 96);
            this.hsiS.Name = "hsiS";
            this.hsiS.Size = new System.Drawing.Size(113, 16);
            this.hsiS.TabIndex = 4;
            this.hsiS.Text = "HSI空间平滑处理";
            this.hsiS.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(168, 71);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(89, 12);
            this.label1.TabIndex = 5;
            this.label1.Text = "邻域模板边长：";
            // 
            // smoothColor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(383, 207);
            this.ControlBox = false;
            this.Controls.Add(this.label1);
            this.Controls.Add(this.hsiS);
            this.Controls.Add(this.rgbS);
            this.Controls.Add(this.lengthS);
            this.Controls.Add(this.close);
            this.Controls.Add(this.start);
            this.Name = "smoothColor";
            this.Text = " 彩色图像平滑处理";
            ((System.ComponentModel.ISupportInitialize)(this.lengthS)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button start;
        private System.Windows.Forms.Button close;
        private System.Windows.Forms.NumericUpDown lengthS;
        private System.Windows.Forms.RadioButton rgbS;
        private System.Windows.Forms.RadioButton hsiS;
        private System.Windows.Forms.Label label1;
    }
}