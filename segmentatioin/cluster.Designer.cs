namespace segmentatioin
{
    partial class cluster
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
            this.isodata = new System.Windows.Forms.RadioButton();
            this.kmean = new System.Windows.Forms.RadioButton();
            this.label1 = new System.Windows.Forms.Label();
            this.numClusters = new System.Windows.Forms.NumericUpDown();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numClusters)).BeginInit();
            this.SuspendLayout();
            // 
            // start
            // 
            this.start.Location = new System.Drawing.Point(33, 179);
            this.start.Name = "start";
            this.start.Size = new System.Drawing.Size(75, 23);
            this.start.TabIndex = 0;
            this.start.Text = "确定";
            this.start.UseVisualStyleBackColor = true;
            this.start.Click += new System.EventHandler(this.start_Click);
            // 
            // close
            // 
            this.close.Location = new System.Drawing.Point(221, 179);
            this.close.Name = "close";
            this.close.Size = new System.Drawing.Size(75, 23);
            this.close.TabIndex = 1;
            this.close.Text = "退出";
            this.close.UseVisualStyleBackColor = true;
            this.close.Click += new System.EventHandler(this.close_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.isodata);
            this.groupBox1.Controls.Add(this.kmean);
            this.groupBox1.Location = new System.Drawing.Point(33, 25);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(260, 90);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "聚类方法：";
            // 
            // isodata
            // 
            this.isodata.AutoSize = true;
            this.isodata.Location = new System.Drawing.Point(141, 42);
            this.isodata.Name = "isodata";
            this.isodata.Size = new System.Drawing.Size(101, 16);
            this.isodata.TabIndex = 1;
            this.isodata.Text = "ISODATA聚类法";
            this.isodata.UseVisualStyleBackColor = true;
            // 
            // kmean
            // 
            this.kmean.AutoSize = true;
            this.kmean.Checked = true;
            this.kmean.Location = new System.Drawing.Point(17, 42);
            this.kmean.Name = "kmean";
            this.kmean.Size = new System.Drawing.Size(95, 16);
            this.kmean.TabIndex = 0;
            this.kmean.TabStop = true;
            this.kmean.Text = "K-均值聚类法";
            this.kmean.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(68, 134);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 12);
            this.label1.TabIndex = 3;
            this.label1.Text = "分割聚类数：";
            // 
            // numClusters
            // 
            this.numClusters.Location = new System.Drawing.Point(174, 132);
            this.numClusters.Minimum = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.numClusters.Name = "numClusters";
            this.numClusters.Size = new System.Drawing.Size(74, 21);
            this.numClusters.TabIndex = 4;
            this.numClusters.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            // 
            // cluster
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(332, 235);
            this.ControlBox = false;
            this.Controls.Add(this.numClusters);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.close);
            this.Controls.Add(this.start);
            this.Name = "cluster";
            this.Text = "特征空间聚类法";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numClusters)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button start;
        private System.Windows.Forms.Button close;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton isodata;
        private System.Windows.Forms.RadioButton kmean;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown numClusters;
    }
}