namespace segmentatioin
{
    partial class ORI
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
            this.label2 = new System.Windows.Forms.Label();
            this.segNum = new System.Windows.Forms.NumericUpDown();
            this.iterNum = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.segNum)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.iterNum)).BeginInit();
            this.SuspendLayout();
            // 
            // start
            // 
            this.start.Location = new System.Drawing.Point(27, 143);
            this.start.Name = "start";
            this.start.Size = new System.Drawing.Size(75, 23);
            this.start.TabIndex = 0;
            this.start.Text = "确定";
            this.start.UseVisualStyleBackColor = true;
            this.start.Click += new System.EventHandler(this.start_Click);
            // 
            // close
            // 
            this.close.Location = new System.Drawing.Point(160, 143);
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
            this.label1.Location = new System.Drawing.Point(25, 36);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 2;
            this.label1.Text = "分割类数：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(25, 90);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 12);
            this.label2.TabIndex = 3;
            this.label2.Text = "迭代次数：";
            // 
            // segNum
            // 
            this.segNum.Location = new System.Drawing.Point(115, 34);
            this.segNum.Minimum = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.segNum.Name = "segNum";
            this.segNum.Size = new System.Drawing.Size(120, 21);
            this.segNum.TabIndex = 4;
            this.segNum.Value = new decimal(new int[] {
            8,
            0,
            0,
            0});
            // 
            // iterNum
            // 
            this.iterNum.Location = new System.Drawing.Point(115, 88);
            this.iterNum.Minimum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.iterNum.Name = "iterNum";
            this.iterNum.Size = new System.Drawing.Size(120, 21);
            this.iterNum.TabIndex = 5;
            this.iterNum.Value = new decimal(new int[] {
            25,
            0,
            0,
            0});
            // 
            // ORI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(272, 200);
            this.ControlBox = false;
            this.Controls.Add(this.iterNum);
            this.Controls.Add(this.segNum);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.close);
            this.Controls.Add(this.start);
            this.Name = "ORI";
            this.Text = "松弛迭代分割法";
            ((System.ComponentModel.ISupportInitialize)(this.segNum)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.iterNum)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button start;
        private System.Windows.Forms.Button close;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown segNum;
        private System.Windows.Forms.NumericUpDown iterNum;
    }
}