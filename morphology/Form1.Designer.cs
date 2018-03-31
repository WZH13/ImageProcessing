namespace morphology
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
            this.erode = new System.Windows.Forms.Button();
            this.dilate = new System.Windows.Forms.Button();
            this.opening = new System.Windows.Forms.Button();
            this.closing = new System.Windows.Forms.Button();
            this.hitMiss = new System.Windows.Forms.Button();
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
            // erode
            // 
            this.erode.Location = new System.Drawing.Point(37, 150);
            this.erode.Name = "erode";
            this.erode.Size = new System.Drawing.Size(75, 23);
            this.erode.TabIndex = 2;
            this.erode.Text = "图像腐蚀";
            this.erode.UseVisualStyleBackColor = true;
            this.erode.Click += new System.EventHandler(this.erode_Click);
            // 
            // dilate
            // 
            this.dilate.Location = new System.Drawing.Point(37, 196);
            this.dilate.Name = "dilate";
            this.dilate.Size = new System.Drawing.Size(75, 23);
            this.dilate.TabIndex = 3;
            this.dilate.Text = "图像膨胀";
            this.dilate.UseVisualStyleBackColor = true;
            this.dilate.Click += new System.EventHandler(this.dilate_Click);
            // 
            // opening
            // 
            this.opening.Location = new System.Drawing.Point(37, 242);
            this.opening.Name = "opening";
            this.opening.Size = new System.Drawing.Size(75, 23);
            this.opening.TabIndex = 4;
            this.opening.Text = "开运算";
            this.opening.UseVisualStyleBackColor = true;
            this.opening.Click += new System.EventHandler(this.opening_Click);
            // 
            // closing
            // 
            this.closing.Location = new System.Drawing.Point(37, 288);
            this.closing.Name = "closing";
            this.closing.Size = new System.Drawing.Size(75, 23);
            this.closing.TabIndex = 5;
            this.closing.Text = "闭运算";
            this.closing.UseVisualStyleBackColor = true;
            this.closing.Click += new System.EventHandler(this.closing_Click);
            // 
            // hitMiss
            // 
            this.hitMiss.Location = new System.Drawing.Point(37, 334);
            this.hitMiss.Name = "hitMiss";
            this.hitMiss.Size = new System.Drawing.Size(75, 23);
            this.hitMiss.TabIndex = 6;
            this.hitMiss.Text = "击中击不中";
            this.hitMiss.UseVisualStyleBackColor = true;
            this.hitMiss.Click += new System.EventHandler(this.hitMiss_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(792, 565);
            this.Controls.Add(this.hitMiss);
            this.Controls.Add(this.closing);
            this.Controls.Add(this.opening);
            this.Controls.Add(this.dilate);
            this.Controls.Add(this.erode);
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
        private System.Windows.Forms.Button erode;

        private string curFileName = null;
        private System.Drawing.Bitmap curBitmap = null;
        private System.Windows.Forms.Button dilate;
        private System.Windows.Forms.Button opening;
        private System.Windows.Forms.Button closing;
        private System.Windows.Forms.Button hitMiss;
    }
}

