namespace gray
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
            this.open = new System.Windows.Forms.Button();
            this.save = new System.Windows.Forms.Button();
            this.close = new System.Windows.Forms.Button();
            this.pixel = new System.Windows.Forms.Button();
            this.memory = new System.Windows.Forms.Button();
            this.pointer = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.timeBox = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // open
            // 
            this.open.Location = new System.Drawing.Point(43, 51);
            this.open.Name = "open";
            this.open.Size = new System.Drawing.Size(75, 23);
            this.open.TabIndex = 0;
            this.open.Text = "打开图像";
            this.open.UseVisualStyleBackColor = true;
            this.open.Click += new System.EventHandler(this.open_Click);
            // 
            // save
            // 
            this.save.Location = new System.Drawing.Point(42, 98);
            this.save.Name = "save";
            this.save.Size = new System.Drawing.Size(75, 23);
            this.save.TabIndex = 1;
            this.save.Text = "保存图像";
            this.save.UseVisualStyleBackColor = true;
            this.save.Click += new System.EventHandler(this.save_Click);
            // 
            // close
            // 
            this.close.Location = new System.Drawing.Point(42, 150);
            this.close.Name = "close";
            this.close.Size = new System.Drawing.Size(75, 23);
            this.close.TabIndex = 2;
            this.close.Text = "关闭";
            this.close.UseVisualStyleBackColor = true;
            this.close.Click += new System.EventHandler(this.close_Click);
            // 
            // pixel
            // 
            this.pixel.Location = new System.Drawing.Point(43, 238);
            this.pixel.Name = "pixel";
            this.pixel.Size = new System.Drawing.Size(75, 23);
            this.pixel.TabIndex = 3;
            this.pixel.Text = "提取像素法";
            this.pixel.UseVisualStyleBackColor = true;
            this.pixel.Click += new System.EventHandler(this.pixel_Click);
            // 
            // memory
            // 
            this.memory.Location = new System.Drawing.Point(42, 285);
            this.memory.Name = "memory";
            this.memory.Size = new System.Drawing.Size(75, 23);
            this.memory.TabIndex = 4;
            this.memory.Text = "内存法";
            this.memory.UseVisualStyleBackColor = true;
            this.memory.Click += new System.EventHandler(this.memory_Click);
            // 
            // pointer
            // 
            this.pointer.Location = new System.Drawing.Point(42, 329);
            this.pointer.Name = "pointer";
            this.pointer.Size = new System.Drawing.Size(75, 23);
            this.pointer.TabIndex = 5;
            this.pointer.Text = "指针法";
            this.pointer.UseVisualStyleBackColor = true;
            this.pointer.Click += new System.EventHandler(this.pointer_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(43, 391);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 6;
            this.label1.Text = "运行时间";
            // 
            // timeBox
            // 
            this.timeBox.Location = new System.Drawing.Point(42, 406);
            this.timeBox.Name = "timeBox";
            this.timeBox.Size = new System.Drawing.Size(100, 21);
            this.timeBox.TabIndex = 7;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 561);
            this.Controls.Add(this.timeBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pointer);
            this.Controls.Add(this.memory);
            this.Controls.Add(this.pixel);
            this.Controls.Add(this.close);
            this.Controls.Add(this.save);
            this.Controls.Add(this.open);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.Form1_Paint);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button open;
        private System.Windows.Forms.Button save;
        private System.Windows.Forms.Button close;
        private System.Windows.Forms.Button pixel;
        private System.Windows.Forms.Button memory;
        private System.Windows.Forms.Button pointer;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox timeBox;
    }
}

