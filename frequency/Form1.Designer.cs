namespace frequency
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
            this.amplitude = new System.Windows.Forms.Button();
            this.phase = new System.Windows.Forms.Button();
            this.freGran = new System.Windows.Forms.Button();
            this.freOri = new System.Windows.Forms.Button();
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
            // amplitude
            // 
            this.amplitude.Location = new System.Drawing.Point(37, 150);
            this.amplitude.Name = "amplitude";
            this.amplitude.Size = new System.Drawing.Size(75, 23);
            this.amplitude.TabIndex = 2;
            this.amplitude.Text = "幅度图像";
            this.amplitude.UseVisualStyleBackColor = true;
            this.amplitude.Click += new System.EventHandler(this.amplitude_Click);
            // 
            // phase
            // 
            this.phase.Location = new System.Drawing.Point(37, 196);
            this.phase.Name = "phase";
            this.phase.Size = new System.Drawing.Size(75, 23);
            this.phase.TabIndex = 3;
            this.phase.Text = "相位图像";
            this.phase.UseVisualStyleBackColor = true;
            this.phase.Click += new System.EventHandler(this.phase_Click);
            // 
            // freGran
            // 
            this.freGran.Location = new System.Drawing.Point(37, 242);
            this.freGran.Name = "freGran";
            this.freGran.Size = new System.Drawing.Size(75, 23);
            this.freGran.TabIndex = 4;
            this.freGran.Text = "成分滤波";
            this.freGran.UseVisualStyleBackColor = true;
            this.freGran.Click += new System.EventHandler(this.freGran_Click);
            // 
            // freOri
            // 
            this.freOri.Location = new System.Drawing.Point(37, 288);
            this.freOri.Name = "freOri";
            this.freOri.Size = new System.Drawing.Size(75, 23);
            this.freOri.TabIndex = 5;
            this.freOri.Text = "方位滤波";
            this.freOri.UseVisualStyleBackColor = true;
            this.freOri.Click += new System.EventHandler(this.freOri_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(792, 565);
            this.Controls.Add(this.freOri);
            this.Controls.Add(this.freGran);
            this.Controls.Add(this.phase);
            this.Controls.Add(this.amplitude);
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
        private System.Windows.Forms.Button amplitude;

        private string curFileName = null;
        private System.Drawing.Bitmap curBitmap = null;
        private System.Windows.Forms.Button phase;
        private System.Windows.Forms.Button freGran;
        private System.Windows.Forms.Button freOri;
    }
}

