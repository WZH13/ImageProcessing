namespace compression
{
    partial class shannonFannon
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
            this.close = new System.Windows.Forms.Button();
            this.sfData = new System.Windows.Forms.ListView();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.entropy = new System.Windows.Forms.Label();
            this.averLength = new System.Windows.Forms.Label();
            this.efficiency = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // close
            // 
            this.close.Location = new System.Drawing.Point(324, 321);
            this.close.Name = "close";
            this.close.Size = new System.Drawing.Size(75, 23);
            this.close.TabIndex = 0;
            this.close.Text = "退出";
            this.close.UseVisualStyleBackColor = true;
            this.close.Click += new System.EventHandler(this.close_Click);
            // 
            // sfData
            // 
            this.sfData.Location = new System.Drawing.Point(32, 24);
            this.sfData.Name = "sfData";
            this.sfData.Size = new System.Drawing.Size(391, 222);
            this.sfData.TabIndex = 1;
            this.sfData.UseCompatibleStateImageBehavior = false;
            this.sfData.View = System.Windows.Forms.View.Details;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(30, 274);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 2;
            this.label1.Text = "图像熵：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(239, 274);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 12);
            this.label2.TabIndex = 3;
            this.label2.Text = "平均码长：";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(30, 321);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 12);
            this.label3.TabIndex = 4;
            this.label3.Text = "编码效率：";
            // 
            // entropy
            // 
            this.entropy.AutoSize = true;
            this.entropy.BackColor = System.Drawing.SystemColors.HighlightText;
            this.entropy.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.entropy.Location = new System.Drawing.Point(101, 274);
            this.entropy.Name = "entropy";
            this.entropy.Size = new System.Drawing.Size(43, 14);
            this.entropy.TabIndex = 5;
            this.entropy.Text = "label4";
            // 
            // averLength
            // 
            this.averLength.AutoSize = true;
            this.averLength.BackColor = System.Drawing.SystemColors.HighlightText;
            this.averLength.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.averLength.Location = new System.Drawing.Point(310, 274);
            this.averLength.Name = "averLength";
            this.averLength.Size = new System.Drawing.Size(43, 14);
            this.averLength.TabIndex = 6;
            this.averLength.Text = "label5";
            // 
            // efficiency
            // 
            this.efficiency.AutoSize = true;
            this.efficiency.BackColor = System.Drawing.SystemColors.HighlightText;
            this.efficiency.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.efficiency.Location = new System.Drawing.Point(101, 321);
            this.efficiency.Name = "efficiency";
            this.efficiency.Size = new System.Drawing.Size(43, 14);
            this.efficiency.TabIndex = 7;
            this.efficiency.Text = "label6";
            // 
            // shannonFannon
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(460, 373);
            this.ControlBox = false;
            this.Controls.Add(this.efficiency);
            this.Controls.Add(this.averLength);
            this.Controls.Add(this.entropy);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.sfData);
            this.Controls.Add(this.close);
            this.Name = "shannonFannon";
            this.Text = "香农-弗诺编码";
            this.Load += new System.EventHandler(this.shannonFannon_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button close;
        private System.Windows.Forms.ListView sfData;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label entropy;
        private System.Windows.Forms.Label averLength;
        private System.Windows.Forms.Label efficiency;
        private System.Drawing.Bitmap bmpSF;
    }
}