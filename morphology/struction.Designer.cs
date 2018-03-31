namespace morphology
{
    partial class struction
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
            this.square = new System.Windows.Forms.RadioButton();
            this.cross = new System.Windows.Forms.RadioButton();
            this.column = new System.Windows.Forms.RadioButton();
            this.row = new System.Windows.Forms.RadioButton();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.five = new System.Windows.Forms.RadioButton();
            this.three = new System.Windows.Forms.RadioButton();
            this.struPic = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.struPic)).BeginInit();
            this.SuspendLayout();
            // 
            // start
            // 
            this.start.Location = new System.Drawing.Point(193, 289);
            this.start.Name = "start";
            this.start.Size = new System.Drawing.Size(75, 23);
            this.start.TabIndex = 0;
            this.start.Text = "确定";
            this.start.UseVisualStyleBackColor = true;
            this.start.Click += new System.EventHandler(this.start_Click);
            // 
            // close
            // 
            this.close.Location = new System.Drawing.Point(298, 289);
            this.close.Name = "close";
            this.close.Size = new System.Drawing.Size(75, 23);
            this.close.TabIndex = 1;
            this.close.Text = "退出";
            this.close.UseVisualStyleBackColor = true;
            this.close.Click += new System.EventHandler(this.close_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.square);
            this.groupBox1.Controls.Add(this.cross);
            this.groupBox1.Controls.Add(this.column);
            this.groupBox1.Controls.Add(this.row);
            this.groupBox1.Location = new System.Drawing.Point(29, 22);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(131, 179);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "结构元素的形状";
            // 
            // square
            // 
            this.square.AutoSize = true;
            this.square.Location = new System.Drawing.Point(6, 133);
            this.square.Name = "square";
            this.square.Size = new System.Drawing.Size(47, 16);
            this.square.TabIndex = 3;
            this.square.Text = "方形";
            this.square.UseVisualStyleBackColor = true;
            this.square.CheckedChanged += new System.EventHandler(this.square_CheckedChanged);
            // 
            // cross
            // 
            this.cross.AutoSize = true;
            this.cross.Location = new System.Drawing.Point(6, 100);
            this.cross.Name = "cross";
            this.cross.Size = new System.Drawing.Size(95, 16);
            this.cross.TabIndex = 2;
            this.cross.Text = "“十”字形状";
            this.cross.UseVisualStyleBackColor = true;
            this.cross.CheckedChanged += new System.EventHandler(this.cross_CheckedChanged);
            // 
            // column
            // 
            this.column.AutoSize = true;
            this.column.Location = new System.Drawing.Point(6, 67);
            this.column.Name = "column";
            this.column.Size = new System.Drawing.Size(71, 16);
            this.column.TabIndex = 1;
            this.column.Text = "垂直方向";
            this.column.UseVisualStyleBackColor = true;
            this.column.CheckedChanged += new System.EventHandler(this.column_CheckedChanged);
            // 
            // row
            // 
            this.row.AutoSize = true;
            this.row.Checked = true;
            this.row.Location = new System.Drawing.Point(6, 34);
            this.row.Name = "row";
            this.row.Size = new System.Drawing.Size(71, 16);
            this.row.TabIndex = 0;
            this.row.TabStop = true;
            this.row.Text = "水平方向";
            this.row.UseVisualStyleBackColor = true;
            this.row.CheckedChanged += new System.EventHandler(this.row_CheckedChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.five);
            this.groupBox2.Controls.Add(this.three);
            this.groupBox2.Location = new System.Drawing.Point(29, 222);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(131, 99);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "结构元素的位数";
            // 
            // five
            // 
            this.five.AutoSize = true;
            this.five.Location = new System.Drawing.Point(16, 67);
            this.five.Name = "five";
            this.five.Size = new System.Drawing.Size(29, 16);
            this.five.TabIndex = 1;
            this.five.Text = "5";
            this.five.UseVisualStyleBackColor = true;
            this.five.CheckedChanged += new System.EventHandler(this.five_CheckedChanged);
            // 
            // three
            // 
            this.three.AutoSize = true;
            this.three.Checked = true;
            this.three.Location = new System.Drawing.Point(16, 34);
            this.three.Name = "three";
            this.three.Size = new System.Drawing.Size(29, 16);
            this.three.TabIndex = 0;
            this.three.TabStop = true;
            this.three.Text = "3";
            this.three.UseVisualStyleBackColor = true;
            this.three.CheckedChanged += new System.EventHandler(this.three_CheckedChanged);
            // 
            // struPic
            // 
            this.struPic.Location = new System.Drawing.Point(193, 46);
            this.struPic.Name = "struPic";
            this.struPic.Size = new System.Drawing.Size(180, 180);
            this.struPic.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.struPic.TabIndex = 4;
            this.struPic.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(191, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(89, 12);
            this.label1.TabIndex = 5;
            this.label1.Text = "结构元素预览：";
            // 
            // struction
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(412, 345);
            this.ControlBox = false;
            this.Controls.Add(this.label1);
            this.Controls.Add(this.struPic);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.close);
            this.Controls.Add(this.start);
            this.Name = "struction";
            this.Text = "结构元素";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.struPic)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button start;
        private System.Windows.Forms.Button close;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton square;
        private System.Windows.Forms.RadioButton cross;
        private System.Windows.Forms.RadioButton column;
        private System.Windows.Forms.RadioButton row;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RadioButton five;
        private System.Windows.Forms.RadioButton three;
        private System.Windows.Forms.PictureBox struPic;
        private System.Windows.Forms.Label label1;

        private byte temp;
    }
}