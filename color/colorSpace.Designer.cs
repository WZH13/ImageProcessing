namespace color
{
    partial class colorSpace
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
            this.intC = new System.Windows.Forms.RadioButton();
            this.satC = new System.Windows.Forms.RadioButton();
            this.hueC = new System.Windows.Forms.RadioButton();
            this.blueC = new System.Windows.Forms.RadioButton();
            this.greenC = new System.Windows.Forms.RadioButton();
            this.redC = new System.Windows.Forms.RadioButton();
            this.start = new System.Windows.Forms.Button();
            this.close = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.intC);
            this.groupBox1.Controls.Add(this.satC);
            this.groupBox1.Controls.Add(this.hueC);
            this.groupBox1.Controls.Add(this.blueC);
            this.groupBox1.Controls.Add(this.greenC);
            this.groupBox1.Controls.Add(this.redC);
            this.groupBox1.Location = new System.Drawing.Point(26, 21);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(274, 153);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "彩色空间分量";
            // 
            // intC
            // 
            this.intC.AutoSize = true;
            this.intC.Location = new System.Drawing.Point(145, 116);
            this.intC.Name = "intC";
            this.intC.Size = new System.Drawing.Size(77, 16);
            this.intC.TabIndex = 5;
            this.intC.TabStop = true;
            this.intC.Text = "亮度（I）";
            this.intC.UseVisualStyleBackColor = true;
            this.intC.CheckedChanged += new System.EventHandler(this.intC_CheckedChanged);
            // 
            // satC
            // 
            this.satC.AutoSize = true;
            this.satC.Location = new System.Drawing.Point(145, 73);
            this.satC.Name = "satC";
            this.satC.Size = new System.Drawing.Size(89, 16);
            this.satC.TabIndex = 4;
            this.satC.TabStop = true;
            this.satC.Text = "饱和度（S）";
            this.satC.UseVisualStyleBackColor = true;
            this.satC.CheckedChanged += new System.EventHandler(this.satC_CheckedChanged);
            // 
            // hueC
            // 
            this.hueC.AutoSize = true;
            this.hueC.Location = new System.Drawing.Point(145, 30);
            this.hueC.Name = "hueC";
            this.hueC.Size = new System.Drawing.Size(77, 16);
            this.hueC.TabIndex = 3;
            this.hueC.TabStop = true;
            this.hueC.Text = "色度（H）";
            this.hueC.UseVisualStyleBackColor = true;
            this.hueC.CheckedChanged += new System.EventHandler(this.hueC_CheckedChanged);
            // 
            // blueC
            // 
            this.blueC.AutoSize = true;
            this.blueC.Location = new System.Drawing.Point(24, 116);
            this.blueC.Name = "blueC";
            this.blueC.Size = new System.Drawing.Size(65, 16);
            this.blueC.TabIndex = 2;
            this.blueC.TabStop = true;
            this.blueC.Text = "蓝（B）";
            this.blueC.UseVisualStyleBackColor = true;
            this.blueC.CheckedChanged += new System.EventHandler(this.blueC_CheckedChanged);
            // 
            // greenC
            // 
            this.greenC.AutoSize = true;
            this.greenC.Location = new System.Drawing.Point(24, 73);
            this.greenC.Name = "greenC";
            this.greenC.Size = new System.Drawing.Size(65, 16);
            this.greenC.TabIndex = 1;
            this.greenC.TabStop = true;
            this.greenC.Text = "绿（G）";
            this.greenC.UseVisualStyleBackColor = true;
            this.greenC.CheckedChanged += new System.EventHandler(this.greenC_CheckedChanged);
            // 
            // redC
            // 
            this.redC.AutoSize = true;
            this.redC.Checked = true;
            this.redC.Location = new System.Drawing.Point(24, 30);
            this.redC.Name = "redC";
            this.redC.Size = new System.Drawing.Size(65, 16);
            this.redC.TabIndex = 0;
            this.redC.TabStop = true;
            this.redC.Text = "红（R）";
            this.redC.UseVisualStyleBackColor = true;
            this.redC.CheckedChanged += new System.EventHandler(this.redC_CheckedChanged);
            // 
            // start
            // 
            this.start.Location = new System.Drawing.Point(50, 194);
            this.start.Name = "start";
            this.start.Size = new System.Drawing.Size(75, 23);
            this.start.TabIndex = 1;
            this.start.Text = "确定";
            this.start.UseVisualStyleBackColor = true;
            this.start.Click += new System.EventHandler(this.start_Click);
            // 
            // close
            // 
            this.close.Location = new System.Drawing.Point(204, 194);
            this.close.Name = "close";
            this.close.Size = new System.Drawing.Size(75, 23);
            this.close.TabIndex = 2;
            this.close.Text = " 退出";
            this.close.UseVisualStyleBackColor = true;
            this.close.Click += new System.EventHandler(this.close_Click);
            // 
            // colorSpace
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(328, 235);
            this.ControlBox = false;
            this.Controls.Add(this.close);
            this.Controls.Add(this.start);
            this.Controls.Add(this.groupBox1);
            this.Name = "colorSpace";
            this.Text = "彩色空间";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton intC;
        private System.Windows.Forms.RadioButton satC;
        private System.Windows.Forms.RadioButton hueC;
        private System.Windows.Forms.RadioButton blueC;
        private System.Windows.Forms.RadioButton greenC;
        private System.Windows.Forms.RadioButton redC;
        private System.Windows.Forms.Button start;
        private System.Windows.Forms.Button close;
        private byte component;
    }
}