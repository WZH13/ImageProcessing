namespace frequency
{
    partial class orientation
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
            this.sOrie = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.fOrie = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.sOrie)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fOrie)).BeginInit();
            this.SuspendLayout();
            // 
            // start
            // 
            this.start.Location = new System.Drawing.Point(45, 300);
            this.start.Name = "start";
            this.start.Size = new System.Drawing.Size(75, 23);
            this.start.TabIndex = 0;
            this.start.Text = "确定";
            this.start.UseVisualStyleBackColor = true;
            this.start.Click += new System.EventHandler(this.start_Click);
            // 
            // close
            // 
            this.close.Location = new System.Drawing.Point(204, 300);
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
            this.label1.Location = new System.Drawing.Point(30, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 12);
            this.label1.TabIndex = 2;
            this.label1.Text = "掩码图预览：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(21, 257);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 12);
            this.label2.TabIndex = 3;
            this.label2.Text = "起始角度：";
            // 
            // sOrie
            // 
            this.sOrie.Location = new System.Drawing.Point(81, 252);
            this.sOrie.Maximum = new decimal(new int[] {
            135,
            0,
            0,
            0});
            this.sOrie.Minimum = new decimal(new int[] {
            45,
            0,
            0,
            -2147483648});
            this.sOrie.Name = "sOrie";
            this.sOrie.Size = new System.Drawing.Size(55, 21);
            this.sOrie.TabIndex = 4;
            this.sOrie.ValueChanged += new System.EventHandler(this.sOrie_ValueChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(164, 257);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 12);
            this.label3.TabIndex = 5;
            this.label3.Text = "终止角度：";
            // 
            // fOrie
            // 
            this.fOrie.Location = new System.Drawing.Point(224, 252);
            this.fOrie.Maximum = new decimal(new int[] {
            225,
            0,
            0,
            0});
            this.fOrie.Minimum = new decimal(new int[] {
            45,
            0,
            0,
            -2147483648});
            this.fOrie.Name = "fOrie";
            this.fOrie.Size = new System.Drawing.Size(55, 21);
            this.fOrie.TabIndex = 6;
            this.fOrie.ValueChanged += new System.EventHandler(this.fOrie_ValueChanged);
            // 
            // orientation
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(327, 349);
            this.ControlBox = false;
            this.Controls.Add(this.fOrie);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.sOrie);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.close);
            this.Controls.Add(this.start);
            this.Name = "orientation";
            this.Text = "频率方位滤波";
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.orientation_Paint);
            ((System.ComponentModel.ISupportInitialize)(this.sOrie)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.fOrie)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button start;
        private System.Windows.Forms.Button close;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown sOrie;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown fOrie;

        private int[] orient;
        private byte flag;
        private int leng1, leng2;
    }
}