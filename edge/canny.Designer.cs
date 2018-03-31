namespace edge
{
    partial class canny
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
            this.label1 = new System.Windows.Forms.Label();
            this.sigma = new System.Windows.Forms.TextBox();
            this.threshHigh = new System.Windows.Forms.TextBox();
            this.threshLow = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // start
            // 
            this.start.Location = new System.Drawing.Point(36, 157);
            this.start.Name = "start";
            this.start.Size = new System.Drawing.Size(75, 23);
            this.start.TabIndex = 0;
            this.start.Text = "确定";
            this.start.UseVisualStyleBackColor = true;
            this.start.Click += new System.EventHandler(this.start_Click);
            // 
            // close
            // 
            this.close.Location = new System.Drawing.Point(154, 157);
            this.close.Name = "close";
            this.close.Size = new System.Drawing.Size(75, 23);
            this.close.TabIndex = 1;
            this.close.Text = "退出";
            this.close.UseVisualStyleBackColor = true;
            this.close.Click += new System.EventHandler(this.close_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(34, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(89, 12);
            this.label1.TabIndex = 2;
            this.label1.Text = "均方差（σ）：";
            // 
            // sigma
            // 
            this.sigma.Location = new System.Drawing.Point(129, 25);
            this.sigma.Name = "sigma";
            this.sigma.Size = new System.Drawing.Size(100, 21);
            this.sigma.TabIndex = 3;
            this.sigma.Text = "2";
            // 
            // threshHigh
            // 
            this.threshHigh.Location = new System.Drawing.Point(129, 65);
            this.threshHigh.Name = "threshHigh";
            this.threshHigh.Size = new System.Drawing.Size(100, 21);
            this.threshHigh.TabIndex = 4;
            this.threshHigh.Text = "100";
            // 
            // threshLow
            // 
            this.threshLow.Location = new System.Drawing.Point(129, 105);
            this.threshLow.Name = "threshLow";
            this.threshLow.Size = new System.Drawing.Size(100, 21);
            this.threshLow.TabIndex = 5;
            this.threshLow.Text = "50";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(34, 68);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(89, 12);
            this.label2.TabIndex = 6;
            this.label2.Text = "高阈值（T1）：";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(34, 108);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(89, 12);
            this.label3.TabIndex = 7;
            this.label3.Text = "低阈值（T2）：";
            // 
            // canny
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(268, 214);
            this.ControlBox = false;
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.threshLow);
            this.Controls.Add(this.threshHigh);
            this.Controls.Add(this.sigma);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.close);
            this.Controls.Add(this.start);
            this.Name = "canny";
            this.Text = "Canny边缘检测";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button start;
        private System.Windows.Forms.Button close;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox sigma;
        private System.Windows.Forms.TextBox threshHigh;
        private System.Windows.Forms.TextBox threshLow;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;

        private byte[] thresh;
    }
}