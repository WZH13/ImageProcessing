namespace segmentatioin
{
    partial class thresholding
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.statis = new System.Windows.Forms.RadioButton();
            this.entropy2D = new System.Windows.Forms.RadioButton();
            this.entropy1D = new System.Windows.Forms.RadioButton();
            this.otsu = new System.Windows.Forms.RadioButton();
            this.iteration = new System.Windows.Forms.RadioButton();
            this.start = new System.Windows.Forms.Button();
            this.close = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.statis);
            this.groupBox1.Controls.Add(this.entropy2D);
            this.groupBox1.Controls.Add(this.entropy1D);
            this.groupBox1.Controls.Add(this.otsu);
            this.groupBox1.Controls.Add(this.iteration);
            this.groupBox1.Location = new System.Drawing.Point(38, 28);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(164, 299);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "阈值法";
            // 
            // statis
            // 
            this.statis.AutoSize = true;
            this.statis.Location = new System.Drawing.Point(19, 248);
            this.statis.Name = "statis";
            this.statis.Size = new System.Drawing.Size(83, 16);
            this.statis.TabIndex = 4;
            this.statis.TabStop = true;
            this.statis.Text = "简单统计法";
            this.statis.UseVisualStyleBackColor = true;
            this.statis.CheckedChanged += new System.EventHandler(this.statis_CheckedChanged);
            // 
            // entropy2D
            // 
            this.entropy2D.AutoSize = true;
            this.entropy2D.Location = new System.Drawing.Point(19, 189);
            this.entropy2D.Name = "entropy2D";
            this.entropy2D.Size = new System.Drawing.Size(95, 16);
            this.entropy2D.TabIndex = 3;
            this.entropy2D.TabStop = true;
            this.entropy2D.Text = "二维最大熵法";
            this.entropy2D.UseVisualStyleBackColor = true;
            this.entropy2D.CheckedChanged += new System.EventHandler(this.entropy2D_CheckedChanged);
            // 
            // entropy1D
            // 
            this.entropy1D.AutoSize = true;
            this.entropy1D.Location = new System.Drawing.Point(19, 132);
            this.entropy1D.Name = "entropy1D";
            this.entropy1D.Size = new System.Drawing.Size(95, 16);
            this.entropy1D.TabIndex = 2;
            this.entropy1D.TabStop = true;
            this.entropy1D.Text = "一维最大熵法";
            this.entropy1D.UseVisualStyleBackColor = true;
            this.entropy1D.CheckedChanged += new System.EventHandler(this.entropy1D_CheckedChanged);
            // 
            // otsu
            // 
            this.otsu.AutoSize = true;
            this.otsu.Location = new System.Drawing.Point(19, 82);
            this.otsu.Name = "otsu";
            this.otsu.Size = new System.Drawing.Size(59, 16);
            this.otsu.TabIndex = 1;
            this.otsu.Text = "Otsu法";
            this.otsu.UseVisualStyleBackColor = true;
            this.otsu.CheckedChanged += new System.EventHandler(this.otsu_CheckedChanged);
            // 
            // iteration
            // 
            this.iteration.AutoSize = true;
            this.iteration.Checked = true;
            this.iteration.Location = new System.Drawing.Point(19, 34);
            this.iteration.Name = "iteration";
            this.iteration.Size = new System.Drawing.Size(59, 16);
            this.iteration.TabIndex = 0;
            this.iteration.TabStop = true;
            this.iteration.Text = "迭代法";
            this.iteration.UseVisualStyleBackColor = true;
            this.iteration.CheckedChanged += new System.EventHandler(this.iteration_CheckedChanged);
            // 
            // start
            // 
            this.start.Location = new System.Drawing.Point(38, 363);
            this.start.Name = "start";
            this.start.Size = new System.Drawing.Size(75, 23);
            this.start.TabIndex = 1;
            this.start.Text = "确定";
            this.start.UseVisualStyleBackColor = true;
            this.start.Click += new System.EventHandler(this.start_Click);
            // 
            // close
            // 
            this.close.Location = new System.Drawing.Point(127, 363);
            this.close.Name = "close";
            this.close.Size = new System.Drawing.Size(75, 23);
            this.close.TabIndex = 2;
            this.close.Text = "退出";
            this.close.UseVisualStyleBackColor = true;
            this.close.Click += new System.EventHandler(this.close_Click);
            // 
            // thresholding
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(239, 421);
            this.ControlBox = false;
            this.Controls.Add(this.close);
            this.Controls.Add(this.start);
            this.Controls.Add(this.groupBox1);
            this.Name = "thresholding";
            this.Text = "阈值分割法";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button start;
        private System.Windows.Forms.Button close;
        private System.Windows.Forms.RadioButton entropy1D;
        private System.Windows.Forms.RadioButton otsu;
        private System.Windows.Forms.RadioButton iteration;

        private byte thr;
        private System.Windows.Forms.RadioButton entropy2D;
        private System.Windows.Forms.RadioButton statis;
    }
}