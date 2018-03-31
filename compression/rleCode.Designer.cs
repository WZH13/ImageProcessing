namespace compression
{
    partial class rleCode
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
            this.rleCoding = new System.Windows.Forms.RadioButton();
            this.rleDecoding = new System.Windows.Forms.RadioButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // start
            // 
            this.start.Location = new System.Drawing.Point(61, 141);
            this.start.Name = "start";
            this.start.Size = new System.Drawing.Size(75, 23);
            this.start.TabIndex = 0;
            this.start.Text = "确定";
            this.start.UseVisualStyleBackColor = true;
            this.start.Click += new System.EventHandler(this.start_Click);
            // 
            // close
            // 
            this.close.Location = new System.Drawing.Point(196, 141);
            this.close.Name = "close";
            this.close.Size = new System.Drawing.Size(73, 23);
            this.close.TabIndex = 1;
            this.close.Text = "退出";
            this.close.UseVisualStyleBackColor = true;
            this.close.Click += new System.EventHandler(this.close_Click);
            // 
            // rleCoding
            // 
            this.rleCoding.AutoSize = true;
            this.rleCoding.Checked = true;
            this.rleCoding.Location = new System.Drawing.Point(23, 33);
            this.rleCoding.Name = "rleCoding";
            this.rleCoding.Size = new System.Drawing.Size(71, 16);
            this.rleCoding.TabIndex = 2;
            this.rleCoding.TabStop = true;
            this.rleCoding.Text = "行程编码";
            this.rleCoding.UseVisualStyleBackColor = true;
            // 
            // rleDecoding
            // 
            this.rleDecoding.AutoSize = true;
            this.rleDecoding.Location = new System.Drawing.Point(154, 33);
            this.rleDecoding.Name = "rleDecoding";
            this.rleDecoding.Size = new System.Drawing.Size(71, 16);
            this.rleDecoding.TabIndex = 3;
            this.rleDecoding.TabStop = true;
            this.rleDecoding.Text = "行程解码";
            this.rleDecoding.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rleCoding);
            this.groupBox1.Controls.Add(this.rleDecoding);
            this.groupBox1.Location = new System.Drawing.Point(42, 30);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(260, 81);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "行程编、解码";
            // 
            // rleCode
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(349, 196);
            this.ControlBox = false;
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.close);
            this.Controls.Add(this.start);
            this.Name = "rleCode";
            this.Text = "行程运算";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button start;
        private System.Windows.Forms.Button close;
        private System.Windows.Forms.RadioButton rleCoding;
        private System.Windows.Forms.RadioButton rleDecoding;
        private System.Windows.Forms.GroupBox groupBox1;
    }
}