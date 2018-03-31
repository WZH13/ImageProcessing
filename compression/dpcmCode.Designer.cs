namespace compression
{
    partial class dpcmCode
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
            this.dpcmDecoding = new System.Windows.Forms.RadioButton();
            this.dpcmEncoding = new System.Windows.Forms.RadioButton();
            this.methodBox = new System.Windows.Forms.ListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // start
            // 
            this.start.Location = new System.Drawing.Point(59, 136);
            this.start.Name = "start";
            this.start.Size = new System.Drawing.Size(75, 23);
            this.start.TabIndex = 0;
            this.start.Text = "确定";
            this.start.UseVisualStyleBackColor = true;
            this.start.Click += new System.EventHandler(this.start_Click);
            // 
            // close
            // 
            this.close.Location = new System.Drawing.Point(185, 136);
            this.close.Name = "close";
            this.close.Size = new System.Drawing.Size(75, 23);
            this.close.TabIndex = 1;
            this.close.Text = "退出";
            this.close.UseVisualStyleBackColor = true;
            this.close.Click += new System.EventHandler(this.close_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.dpcmDecoding);
            this.groupBox1.Controls.Add(this.dpcmEncoding);
            this.groupBox1.Location = new System.Drawing.Point(39, 27);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(112, 78);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "DPCM";
            // 
            // dpcmDecoding
            // 
            this.dpcmDecoding.AutoSize = true;
            this.dpcmDecoding.Location = new System.Drawing.Point(20, 54);
            this.dpcmDecoding.Name = "dpcmDecoding";
            this.dpcmDecoding.Size = new System.Drawing.Size(71, 16);
            this.dpcmDecoding.TabIndex = 1;
            this.dpcmDecoding.TabStop = true;
            this.dpcmDecoding.Text = "DPCM解码";
            this.dpcmDecoding.UseVisualStyleBackColor = true;
            this.dpcmDecoding.CheckedChanged += new System.EventHandler(this.dpcmDecoding_CheckedChanged);
            // 
            // dpcmEncoding
            // 
            this.dpcmEncoding.AutoSize = true;
            this.dpcmEncoding.Checked = true;
            this.dpcmEncoding.Location = new System.Drawing.Point(20, 21);
            this.dpcmEncoding.Name = "dpcmEncoding";
            this.dpcmEncoding.Size = new System.Drawing.Size(71, 16);
            this.dpcmEncoding.TabIndex = 0;
            this.dpcmEncoding.TabStop = true;
            this.dpcmEncoding.Text = "DPCM编码";
            this.dpcmEncoding.UseVisualStyleBackColor = true;
            this.dpcmEncoding.CheckedChanged += new System.EventHandler(this.dpcmEncoding_CheckedChanged);
            // 
            // methodBox
            // 
            this.methodBox.FormattingEnabled = true;
            this.methodBox.ItemHeight = 12;
            this.methodBox.Location = new System.Drawing.Point(183, 53);
            this.methodBox.Name = "methodBox";
            this.methodBox.Size = new System.Drawing.Size(99, 52);
            this.methodBox.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(183, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 4;
            this.label1.Text = "预测方法：";
            // 
            // dpcmCode
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(324, 191);
            this.ControlBox = false;
            this.Controls.Add(this.label1);
            this.Controls.Add(this.methodBox);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.close);
            this.Controls.Add(this.start);
            this.Name = "dpcmCode";
            this.Text = "DPCM";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button start;
        private System.Windows.Forms.Button close;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton dpcmDecoding;
        private System.Windows.Forms.RadioButton dpcmEncoding;
        private System.Windows.Forms.ListBox methodBox;
        private System.Windows.Forms.Label label1;
        private bool DorC;
        private byte methodDPCM;
    }
}