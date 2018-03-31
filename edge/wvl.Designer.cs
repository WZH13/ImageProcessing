namespace edge
{
    partial class wvl
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
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.highT = new System.Windows.Forms.TextBox();
            this.lowT = new System.Windows.Forms.TextBox();
            this.multiscale = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.multiscale)).BeginInit();
            this.SuspendLayout();
            // 
            // start
            // 
            this.start.Location = new System.Drawing.Point(42, 151);
            this.start.Name = "start";
            this.start.Size = new System.Drawing.Size(75, 23);
            this.start.TabIndex = 0;
            this.start.Text = "确定";
            this.start.UseVisualStyleBackColor = true;
            this.start.Click += new System.EventHandler(this.start_Click);
            // 
            // close
            // 
            this.close.Location = new System.Drawing.Point(138, 151);
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
            this.label1.Location = new System.Drawing.Point(76, 36);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 2;
            this.label1.Text = "尺度：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(28, 71);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(89, 12);
            this.label2.TabIndex = 3;
            this.label2.Text = "高阈值（T1）：";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(28, 106);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(89, 12);
            this.label3.TabIndex = 4;
            this.label3.Text = "低阈值（T2）：";
            // 
            // highT
            // 
            this.highT.Location = new System.Drawing.Point(138, 68);
            this.highT.Name = "highT";
            this.highT.Size = new System.Drawing.Size(75, 21);
            this.highT.TabIndex = 5;
            this.highT.Text = "100";
            // 
            // lowT
            // 
            this.lowT.Location = new System.Drawing.Point(138, 103);
            this.lowT.Name = "lowT";
            this.lowT.Size = new System.Drawing.Size(75, 21);
            this.lowT.TabIndex = 6;
            this.lowT.Text = "50";
            // 
            // multiscale
            // 
            this.multiscale.Location = new System.Drawing.Point(138, 34);
            this.multiscale.Maximum = new decimal(new int[] {
            3,
            0,
            0,
            0});
            this.multiscale.Name = "multiscale";
            this.multiscale.Size = new System.Drawing.Size(75, 21);
            this.multiscale.TabIndex = 7;
            // 
            // wvl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(262, 210);
            this.ControlBox = false;
            this.Controls.Add(this.multiscale);
            this.Controls.Add(this.lowT);
            this.Controls.Add(this.highT);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.close);
            this.Controls.Add(this.start);
            this.Name = "wvl";
            this.Text = "小波变换边缘检测";
            ((System.ComponentModel.ISupportInitialize)(this.multiscale)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button start;
        private System.Windows.Forms.Button close;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox highT;
        private System.Windows.Forms.TextBox lowT;
        private System.Windows.Forms.NumericUpDown multiscale;

        private byte[] thresh;
    }
}