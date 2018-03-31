namespace color
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
            this.tranSpace = new System.Windows.Forms.Button();
            this.chCom = new System.Windows.Forms.Button();
            this.pseudoC = new System.Windows.Forms.Button();
            this.equC = new System.Windows.Forms.Button();
            this.smoC = new System.Windows.Forms.Button();
            this.shaC = new System.Windows.Forms.Button();
            this.edgeC = new System.Windows.Forms.Button();
            this.segC = new System.Windows.Forms.Button();
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
            // tranSpace
            // 
            this.tranSpace.Location = new System.Drawing.Point(37, 150);
            this.tranSpace.Name = "tranSpace";
            this.tranSpace.Size = new System.Drawing.Size(75, 23);
            this.tranSpace.TabIndex = 2;
            this.tranSpace.Text = "空间转换";
            this.tranSpace.UseVisualStyleBackColor = true;
            this.tranSpace.Click += new System.EventHandler(this.tranSpace_Click);
            // 
            // chCom
            // 
            this.chCom.Location = new System.Drawing.Point(37, 196);
            this.chCom.Name = "chCom";
            this.chCom.Size = new System.Drawing.Size(75, 23);
            this.chCom.TabIndex = 3;
            this.chCom.Text = "调整分量";
            this.chCom.UseVisualStyleBackColor = true;
            this.chCom.Click += new System.EventHandler(this.chCom_Click);
            // 
            // pseudoC
            // 
            this.pseudoC.Location = new System.Drawing.Point(37, 242);
            this.pseudoC.Name = "pseudoC";
            this.pseudoC.Size = new System.Drawing.Size(75, 23);
            this.pseudoC.TabIndex = 4;
            this.pseudoC.Text = "伪彩色处理";
            this.pseudoC.UseVisualStyleBackColor = true;
            this.pseudoC.Click += new System.EventHandler(this.pseudoC_Click);
            // 
            // equC
            // 
            this.equC.Location = new System.Drawing.Point(37, 288);
            this.equC.Name = "equC";
            this.equC.Size = new System.Drawing.Size(75, 23);
            this.equC.TabIndex = 5;
            this.equC.Text = "直方图均衡";
            this.equC.UseVisualStyleBackColor = true;
            this.equC.Click += new System.EventHandler(this.equC_Click);
            // 
            // smoC
            // 
            this.smoC.Location = new System.Drawing.Point(37, 334);
            this.smoC.Name = "smoC";
            this.smoC.Size = new System.Drawing.Size(75, 23);
            this.smoC.TabIndex = 6;
            this.smoC.Text = "平滑处理";
            this.smoC.UseVisualStyleBackColor = true;
            this.smoC.Click += new System.EventHandler(this.smoC_Click);
            // 
            // shaC
            // 
            this.shaC.Location = new System.Drawing.Point(37, 380);
            this.shaC.Name = "shaC";
            this.shaC.Size = new System.Drawing.Size(75, 23);
            this.shaC.TabIndex = 7;
            this.shaC.Text = "锐化处理";
            this.shaC.UseVisualStyleBackColor = true;
            this.shaC.Click += new System.EventHandler(this.shaC_Click);
            // 
            // edgeC
            // 
            this.edgeC.Location = new System.Drawing.Point(37, 426);
            this.edgeC.Name = "edgeC";
            this.edgeC.Size = new System.Drawing.Size(75, 23);
            this.edgeC.TabIndex = 8;
            this.edgeC.Text = "边缘检测";
            this.edgeC.UseVisualStyleBackColor = true;
            this.edgeC.Click += new System.EventHandler(this.edgeC_Click);
            // 
            // segC
            // 
            this.segC.Location = new System.Drawing.Point(39, 472);
            this.segC.Name = "segC";
            this.segC.Size = new System.Drawing.Size(75, 23);
            this.segC.TabIndex = 9;
            this.segC.Text = "图像分割";
            this.segC.UseVisualStyleBackColor = true;
            this.segC.Click += new System.EventHandler(this.segC_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(792, 565);
            this.Controls.Add(this.segC);
            this.Controls.Add(this.edgeC);
            this.Controls.Add(this.shaC);
            this.Controls.Add(this.smoC);
            this.Controls.Add(this.equC);
            this.Controls.Add(this.pseudoC);
            this.Controls.Add(this.chCom);
            this.Controls.Add(this.tranSpace);
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
        private System.Windows.Forms.Button tranSpace;
        private string curFileName = null;
        private System.Drawing.Bitmap curBitmap = null;
        private System.Windows.Forms.Button chCom;
        public byte redCom;
        public byte greenCom;
        public byte blueCom;
        public byte hueCom;
        public byte satCom;
        public byte intCom;
        private byte[] tempArray;
        private System.Windows.Forms.Button pseudoC;
        private System.Windows.Forms.Button equC;
        private System.Windows.Forms.Button smoC;
        private System.Windows.Forms.Button shaC;
        private System.Windows.Forms.Button edgeC;
        private System.Windows.Forms.Button segC;
    }
}

