namespace smooth
{
    partial class meanMedian
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
            this.mean = new System.Windows.Forms.RadioButton();
            this.median = new System.Windows.Forms.RadioButton();
            this.label1 = new System.Windows.Forms.Label();
            this.length = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.length)).BeginInit();
            this.SuspendLayout();
            // 
            // start
            // 
            this.start.Location = new System.Drawing.Point(27, 135);
            this.start.Name = "start";
            this.start.Size = new System.Drawing.Size(75, 23);
            this.start.TabIndex = 0;
            this.start.Text = "确定";
            this.start.UseVisualStyleBackColor = true;
            this.start.Click += new System.EventHandler(this.start_Click);
            // 
            // close
            // 
            this.close.Location = new System.Drawing.Point(156, 135);
            this.close.Name = "close";
            this.close.Size = new System.Drawing.Size(75, 23);
            this.close.TabIndex = 1;
            this.close.Text = "退出";
            this.close.UseVisualStyleBackColor = true;
            this.close.Click += new System.EventHandler(this.close_Click);
            // 
            // mean
            // 
            this.mean.AutoSize = true;
            this.mean.Checked = true;
            this.mean.Location = new System.Drawing.Point(27, 40);
            this.mean.Name = "mean";
            this.mean.Size = new System.Drawing.Size(71, 16);
            this.mean.TabIndex = 2;
            this.mean.TabStop = true;
            this.mean.Text = "均值滤波";
            this.mean.UseVisualStyleBackColor = true;
            // 
            // median
            // 
            this.median.AutoSize = true;
            this.median.Location = new System.Drawing.Point(27, 90);
            this.median.Name = "median";
            this.median.Size = new System.Drawing.Size(71, 16);
            this.median.TabIndex = 3;
            this.median.TabStop = true;
            this.median.Text = "中值滤波";
            this.median.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(118, 65);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 4;
            this.label1.Text = "模板边长：";
            // 
            // length
            // 
            this.length.Increment = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.length.Location = new System.Drawing.Point(189, 63);
            this.length.Maximum = new decimal(new int[] {
            7,
            0,
            0,
            0});
            this.length.Minimum = new decimal(new int[] {
            3,
            0,
            0,
            0});
            this.length.Name = "length";
            this.length.Size = new System.Drawing.Size(42, 21);
            this.length.TabIndex = 5;
            this.length.Value = new decimal(new int[] {
            3,
            0,
            0,
            0});
            // 
            // meanMedian
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(274, 192);
            this.ControlBox = false;
            this.Controls.Add(this.length);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.median);
            this.Controls.Add(this.mean);
            this.Controls.Add(this.close);
            this.Controls.Add(this.start);
            this.Name = "meanMedian";
            this.Text = "均值滤波与中值滤波";
            ((System.ComponentModel.ISupportInitialize)(this.length)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button start;
        private System.Windows.Forms.Button close;
        private System.Windows.Forms.RadioButton mean;
        private System.Windows.Forms.RadioButton median;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown length;
    }
}