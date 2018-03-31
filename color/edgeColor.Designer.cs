namespace color
{
    partial class edgeColor
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
            this.vectEdge2 = new System.Windows.Forms.RadioButton();
            this.rgbEdge = new System.Windows.Forms.RadioButton();
            this.threshUD = new System.Windows.Forms.NumericUpDown();
            this.vectEdge1 = new System.Windows.Forms.RadioButton();
            ((System.ComponentModel.ISupportInitialize)(this.threshUD)).BeginInit();
            this.SuspendLayout();
            // 
            // start
            // 
            this.start.Location = new System.Drawing.Point(38, 168);
            this.start.Name = "start";
            this.start.Size = new System.Drawing.Size(75, 23);
            this.start.TabIndex = 0;
            this.start.Text = "确定";
            this.start.UseVisualStyleBackColor = true;
            this.start.Click += new System.EventHandler(this.start_Click);
            // 
            // close
            // 
            this.close.Location = new System.Drawing.Point(190, 168);
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
            this.label1.Location = new System.Drawing.Point(170, 74);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 2;
            this.label1.Text = "阈值：";
            // 
            // vectEdge2
            // 
            this.vectEdge2.AutoSize = true;
            this.vectEdge2.Location = new System.Drawing.Point(38, 72);
            this.vectEdge2.Name = "vectEdge2";
            this.vectEdge2.Size = new System.Drawing.Size(71, 16);
            this.vectEdge2.TabIndex = 3;
            this.vectEdge2.Text = "向量法Ⅱ";
            this.vectEdge2.UseVisualStyleBackColor = true;
            // 
            // rgbEdge
            // 
            this.rgbEdge.AutoSize = true;
            this.rgbEdge.Location = new System.Drawing.Point(38, 118);
            this.rgbEdge.Name = "rgbEdge";
            this.rgbEdge.Size = new System.Drawing.Size(125, 16);
            this.rgbEdge.TabIndex = 4;
            this.rgbEdge.Text = "RGB分量直接梯度法";
            this.rgbEdge.UseVisualStyleBackColor = true;
            // 
            // threshUD
            // 
            this.threshUD.Location = new System.Drawing.Point(217, 72);
            this.threshUD.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.threshUD.Name = "threshUD";
            this.threshUD.Size = new System.Drawing.Size(49, 21);
            this.threshUD.TabIndex = 5;
            this.threshUD.Value = new decimal(new int[] {
            75,
            0,
            0,
            0});
            // 
            // vectEdge1
            // 
            this.vectEdge1.AutoSize = true;
            this.vectEdge1.Checked = true;
            this.vectEdge1.Location = new System.Drawing.Point(38, 26);
            this.vectEdge1.Name = "vectEdge1";
            this.vectEdge1.Size = new System.Drawing.Size(71, 16);
            this.vectEdge1.TabIndex = 6;
            this.vectEdge1.TabStop = true;
            this.vectEdge1.Text = "向量法Ⅰ";
            this.vectEdge1.UseVisualStyleBackColor = true;
            // 
            // edgeColor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(309, 227);
            this.ControlBox = false;
            this.Controls.Add(this.vectEdge1);
            this.Controls.Add(this.threshUD);
            this.Controls.Add(this.rgbEdge);
            this.Controls.Add(this.vectEdge2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.close);
            this.Controls.Add(this.start);
            this.Name = "edgeColor";
            this.Text = "彩色图像边缘检测";
            ((System.ComponentModel.ISupportInitialize)(this.threshUD)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button start;
        private System.Windows.Forms.Button close;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RadioButton vectEdge2;
        private System.Windows.Forms.RadioButton rgbEdge;
        private System.Windows.Forms.NumericUpDown threshUD;        
        private System.Windows.Forms.RadioButton vectEdge1;

        private byte methodF;
    }
}