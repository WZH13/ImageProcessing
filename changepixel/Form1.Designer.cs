namespace changepixel
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
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.close = new System.Windows.Forms.Button();
            this.save = new System.Windows.Forms.Button();
            this.open = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.pixeltxbB = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.pixeltxbG = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.pixeltxbR = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.change = new System.Windows.Forms.Button();
            this.gray = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label6 = new System.Windows.Forms.Label();
            this.ytxb = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.xtxb = new System.Windows.Forms.TextBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // close
            // 
            this.close.Location = new System.Drawing.Point(27, 447);
            this.close.Name = "close";
            this.close.Size = new System.Drawing.Size(75, 23);
            this.close.TabIndex = 8;
            this.close.Text = "关闭";
            this.close.UseVisualStyleBackColor = true;
            this.close.Click += new System.EventHandler(this.close_Click);
            // 
            // save
            // 
            this.save.Location = new System.Drawing.Point(26, 401);
            this.save.Name = "save";
            this.save.Size = new System.Drawing.Size(75, 23);
            this.save.TabIndex = 7;
            this.save.Text = "保存图像";
            this.save.UseVisualStyleBackColor = true;
            this.save.Click += new System.EventHandler(this.save_Click);
            // 
            // open
            // 
            this.open.Location = new System.Drawing.Point(26, 23);
            this.open.Name = "open";
            this.open.Size = new System.Drawing.Size(75, 23);
            this.open.TabIndex = 6;
            this.open.Text = "打开图像";
            this.open.UseVisualStyleBackColor = true;
            this.open.Click += new System.EventHandler(this.open_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.pixeltxbB);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.pixeltxbG);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.pixeltxbR);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.change);
            this.groupBox1.Location = new System.Drawing.Point(12, 233);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(154, 152);
            this.groupBox1.TabIndex = 9;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "更新像素值";
            // 
            // pixeltxbB
            // 
            this.pixeltxbB.Location = new System.Drawing.Point(52, 85);
            this.pixeltxbB.Name = "pixeltxbB";
            this.pixeltxbB.Size = new System.Drawing.Size(66, 21);
            this.pixeltxbB.TabIndex = 10;
            this.pixeltxbB.Text = "0";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(29, 88);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(23, 12);
            this.label5.TabIndex = 9;
            this.label5.Text = "B：";
            // 
            // pixeltxbG
            // 
            this.pixeltxbG.Location = new System.Drawing.Point(52, 58);
            this.pixeltxbG.Name = "pixeltxbG";
            this.pixeltxbG.Size = new System.Drawing.Size(66, 21);
            this.pixeltxbG.TabIndex = 8;
            this.pixeltxbG.Text = "0";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(29, 61);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(23, 12);
            this.label4.TabIndex = 7;
            this.label4.Text = "G：";
            // 
            // pixeltxbR
            // 
            this.pixeltxbR.Location = new System.Drawing.Point(52, 31);
            this.pixeltxbR.Name = "pixeltxbR";
            this.pixeltxbR.Size = new System.Drawing.Size(66, 21);
            this.pixeltxbR.TabIndex = 6;
            this.pixeltxbR.Text = "0";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(29, 34);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(23, 12);
            this.label3.TabIndex = 5;
            this.label3.Text = "R：";
            // 
            // change
            // 
            this.change.Location = new System.Drawing.Point(31, 117);
            this.change.Name = "change";
            this.change.Size = new System.Drawing.Size(75, 23);
            this.change.TabIndex = 4;
            this.change.Text = "确定";
            this.change.UseVisualStyleBackColor = true;
            this.change.Click += new System.EventHandler(this.change_Click);
            // 
            // gray
            // 
            this.gray.Location = new System.Drawing.Point(26, 73);
            this.gray.Name = "gray";
            this.gray.Size = new System.Drawing.Size(75, 23);
            this.gray.TabIndex = 10;
            this.gray.Text = "图像灰度化";
            this.gray.UseVisualStyleBackColor = true;
            this.gray.Click += new System.EventHandler(this.gray_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.ytxb);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.xtxb);
            this.groupBox2.Location = new System.Drawing.Point(12, 113);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(154, 114);
            this.groupBox2.TabIndex = 11;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "填写坐标";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("宋体", 7.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label6.ForeColor = System.Drawing.Color.Red;
            this.label6.Location = new System.Drawing.Point(22, 89);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(115, 10);
            this.label6.TabIndex = 12;
            this.label6.Text = "提示：可双击图片选择点";
            // 
            // ytxb
            // 
            this.ytxb.Location = new System.Drawing.Point(67, 56);
            this.ytxb.Name = "ytxb";
            this.ytxb.Size = new System.Drawing.Size(66, 21);
            this.ytxb.TabIndex = 7;
            this.ytxb.Text = "0";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(14, 59);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 5;
            this.label2.Text = "纵坐标：";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(14, 27);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 4;
            this.label1.Text = "横坐标：";
            // 
            // xtxb
            // 
            this.xtxb.Location = new System.Drawing.Point(67, 24);
            this.xtxb.Name = "xtxb";
            this.xtxb.Size = new System.Drawing.Size(66, 21);
            this.xtxb.TabIndex = 6;
            this.xtxb.Text = "0";
            // 
            // R
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(922, 503);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.gray);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.close);
            this.Controls.Add(this.save);
            this.Controls.Add(this.open);
            this.Name = "R";
            this.Text = "修改指定点颜色";
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.Form1_Paint);
            this.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.R_MouseDoubleClick);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button close;
        private System.Windows.Forms.Button save;
        private System.Windows.Forms.Button open;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button change;
        private System.Windows.Forms.TextBox pixeltxbR;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button gray;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox ytxb;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox xtxb;
        private System.Windows.Forms.TextBox pixeltxbB;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox pixeltxbG;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label6;
    }
}

