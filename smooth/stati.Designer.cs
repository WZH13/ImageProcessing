namespace smooth
{
    partial class stati
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
            this.five = new System.Windows.Forms.RadioButton();
            this.three = new System.Windows.Forms.RadioButton();
            this.label1 = new System.Windows.Forms.Label();
            this.thresh = new System.Windows.Forms.TextBox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // start
            // 
            this.start.Location = new System.Drawing.Point(22, 167);
            this.start.Name = "start";
            this.start.Size = new System.Drawing.Size(75, 23);
            this.start.TabIndex = 0;
            this.start.Text = "确定";
            this.start.UseVisualStyleBackColor = true;
            this.start.Click += new System.EventHandler(this.start_Click);
            // 
            // close
            // 
            this.close.Location = new System.Drawing.Point(147, 167);
            this.close.Name = "close";
            this.close.Size = new System.Drawing.Size(75, 23);
            this.close.TabIndex = 1;
            this.close.Text = "退出";
            this.close.UseVisualStyleBackColor = true;
            this.close.Click += new System.EventHandler(this.close_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.five);
            this.groupBox1.Controls.Add(this.three);
            this.groupBox1.Location = new System.Drawing.Point(22, 26);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(200, 77);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "邻域窗";
            // 
            // five
            // 
            this.five.AutoSize = true;
            this.five.Location = new System.Drawing.Point(125, 33);
            this.five.Name = "five";
            this.five.Size = new System.Drawing.Size(47, 16);
            this.five.TabIndex = 1;
            this.five.Text = "5×5";
            this.five.UseVisualStyleBackColor = true;
            // 
            // three
            // 
            this.three.AutoSize = true;
            this.three.Checked = true;
            this.three.Location = new System.Drawing.Point(28, 33);
            this.three.Name = "three";
            this.three.Size = new System.Drawing.Size(47, 16);
            this.three.TabIndex = 0;
            this.three.TabStop = true;
            this.three.Text = "3×3";
            this.three.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(48, 126);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(71, 12);
            this.label1.TabIndex = 3;
            this.label1.Text = "阈值（T）：";
            // 
            // thresh
            // 
            this.thresh.Location = new System.Drawing.Point(139, 123);
            this.thresh.Name = "thresh";
            this.thresh.Size = new System.Drawing.Size(55, 21);
            this.thresh.TabIndex = 4;
            this.thresh.Text = "1.5";
            // 
            // stati
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(252, 215);
            this.ControlBox = false;
            this.Controls.Add(this.thresh);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.close);
            this.Controls.Add(this.start);
            this.Name = "stati";
            this.Text = "统计平滑方法";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button start;
        private System.Windows.Forms.Button close;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton five;
        private System.Windows.Forms.RadioButton three;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox thresh;
    }
}