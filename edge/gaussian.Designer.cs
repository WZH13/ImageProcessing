namespace edge
{
    partial class gaussian
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
            this.dog = new System.Windows.Forms.RadioButton();
            this.log = new System.Windows.Forms.RadioButton();
            this.sigmaValue = new System.Windows.Forms.TextBox();
            this.thresdValue = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // start
            // 
            this.start.Location = new System.Drawing.Point(29, 256);
            this.start.Name = "start";
            this.start.Size = new System.Drawing.Size(75, 23);
            this.start.TabIndex = 0;
            this.start.Text = "确定";
            this.start.UseVisualStyleBackColor = true;
            this.start.Click += new System.EventHandler(this.start_Click);
            // 
            // close
            // 
            this.close.Location = new System.Drawing.Point(140, 256);
            this.close.Name = "close";
            this.close.Size = new System.Drawing.Size(75, 23);
            this.close.TabIndex = 1;
            this.close.Text = "退出";
            this.close.UseVisualStyleBackColor = true;
            this.close.Click += new System.EventHandler(this.close_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.dog);
            this.groupBox1.Controls.Add(this.log);
            this.groupBox1.Location = new System.Drawing.Point(29, 22);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(186, 124);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "高斯算子";
            // 
            // dog
            // 
            this.dog.AutoSize = true;
            this.dog.Location = new System.Drawing.Point(21, 71);
            this.dog.Name = "dog";
            this.dog.Size = new System.Drawing.Size(95, 16);
            this.dog.TabIndex = 1;
            this.dog.Text = "差分高斯算子";
            this.dog.UseVisualStyleBackColor = true;
            // 
            // log
            // 
            this.log.AutoSize = true;
            this.log.Checked = true;
            this.log.Location = new System.Drawing.Point(21, 32);
            this.log.Name = "log";
            this.log.Size = new System.Drawing.Size(125, 16);
            this.log.TabIndex = 0;
            this.log.TabStop = true;
            this.log.Text = "拉普拉斯-高斯算子";
            this.log.UseVisualStyleBackColor = true;
            // 
            // sigmaValue
            // 
            this.sigmaValue.Location = new System.Drawing.Point(115, 164);
            this.sigmaValue.Name = "sigmaValue";
            this.sigmaValue.Size = new System.Drawing.Size(60, 21);
            this.sigmaValue.TabIndex = 3;
            this.sigmaValue.Text = "1.5";
            // 
            // thresdValue
            // 
            this.thresdValue.Location = new System.Drawing.Point(115, 205);
            this.thresdValue.Name = "thresdValue";
            this.thresdValue.Size = new System.Drawing.Size(60, 21);
            this.thresdValue.TabIndex = 4;
            this.thresdValue.Text = "40";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(48, 167);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 5;
            this.label1.Text = "均方差：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(48, 208);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 12);
            this.label2.TabIndex = 6;
            this.label2.Text = "阈值：";
            // 
            // gaussian
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(242, 305);
            this.ControlBox = false;
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.thresdValue);
            this.Controls.Add(this.sigmaValue);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.close);
            this.Controls.Add(this.start);
            this.Name = "gaussian";
            this.Text = "高斯算子";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button start;
        private System.Windows.Forms.Button close;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton dog;
        private System.Windows.Forms.RadioButton log;
        private System.Windows.Forms.TextBox sigmaValue;
        private System.Windows.Forms.TextBox thresdValue;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
    }
}