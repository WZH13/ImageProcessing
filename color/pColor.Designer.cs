namespace color
{
    partial class pColor
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
            this.intNum = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.gcTrans = new System.Windows.Forms.RadioButton();
            this.intSeg = new System.Windows.Forms.RadioButton();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // start
            // 
            this.start.Location = new System.Drawing.Point(62, 187);
            this.start.Name = "start";
            this.start.Size = new System.Drawing.Size(75, 23);
            this.start.TabIndex = 0;
            this.start.Text = "确定";
            this.start.UseVisualStyleBackColor = true;
            this.start.Click += new System.EventHandler(this.start_Click);
            // 
            // close
            // 
            this.close.Location = new System.Drawing.Point(218, 187);
            this.close.Name = "close";
            this.close.Size = new System.Drawing.Size(75, 23);
            this.close.TabIndex = 1;
            this.close.Text = "退出";
            this.close.UseVisualStyleBackColor = true;
            this.close.Click += new System.EventHandler(this.close_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.intNum);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.gcTrans);
            this.groupBox1.Controls.Add(this.intSeg);
            this.groupBox1.Location = new System.Drawing.Point(31, 23);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(303, 137);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "伪彩色处理方法";
            // 
            // intNum
            // 
            this.intNum.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.intNum.FormattingEnabled = true;
            this.intNum.Location = new System.Drawing.Point(208, 33);
            this.intNum.Name = "intNum";
            this.intNum.Size = new System.Drawing.Size(71, 20);
            this.intNum.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(134, 37);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 12);
            this.label1.TabIndex = 2;
            this.label1.Text = " 分层数：";
            // 
            // gcTrans
            // 
            this.gcTrans.AutoSize = true;
            this.gcTrans.Location = new System.Drawing.Point(31, 90);
            this.gcTrans.Name = "gcTrans";
            this.gcTrans.Size = new System.Drawing.Size(125, 16);
            this.gcTrans.TabIndex = 1;
            this.gcTrans.Text = "灰度级-彩色变换法";
            this.gcTrans.UseVisualStyleBackColor = true;
            this.gcTrans.CheckedChanged += new System.EventHandler(this.gcTrans_CheckedChanged);
            // 
            // intSeg
            // 
            this.intSeg.AutoSize = true;
            this.intSeg.Checked = true;
            this.intSeg.Location = new System.Drawing.Point(31, 35);
            this.intSeg.Name = "intSeg";
            this.intSeg.Size = new System.Drawing.Size(83, 16);
            this.intSeg.TabIndex = 0;
            this.intSeg.TabStop = true;
            this.intSeg.Text = "强度分层法";
            this.intSeg.UseVisualStyleBackColor = true;
            this.intSeg.CheckedChanged += new System.EventHandler(this.intSeg_CheckedChanged);
            // 
            // pColor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(362, 235);
            this.ControlBox = false;
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.close);
            this.Controls.Add(this.start);
            this.Name = "pColor";
            this.Text = "伪彩色处理";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button start;
        private System.Windows.Forms.Button close;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton gcTrans;
        private System.Windows.Forms.RadioButton intSeg;
        private System.Windows.Forms.ComboBox intNum;
        private System.Windows.Forms.Label label1;
        private bool pseMethod;
        private byte numSeg;
    }
}