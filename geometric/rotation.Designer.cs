namespace geometric
{
    partial class rotation
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.startRot = new System.Windows.Forms.Button();
            this.close = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.degree = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // startRot
            // 
            this.startRot.Location = new System.Drawing.Point(42, 101);
            this.startRot.Name = "startRot";
            this.startRot.Size = new System.Drawing.Size(75, 23);
            this.startRot.TabIndex = 0;
            this.startRot.Text = "确定";
            this.startRot.UseVisualStyleBackColor = true;
            this.startRot.Click += new System.EventHandler(this.startRot_Click);
            // 
            // close
            // 
            this.close.Location = new System.Drawing.Point(143, 101);
            this.close.Name = "close";
            this.close.Size = new System.Drawing.Size(75, 23);
            this.close.TabIndex = 1;
            this.close.Text = "关闭";
            this.close.UseVisualStyleBackColor = true;
            this.close.Click += new System.EventHandler(this.close_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(28, 38);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(89, 12);
            this.label1.TabIndex = 2;
            this.label1.Text = "旋转角度（°）";
            // 
            // degree
            // 
            this.degree.Location = new System.Drawing.Point(123, 35);
            this.degree.Name = "degree";
            this.degree.Size = new System.Drawing.Size(100, 21);
            this.degree.TabIndex = 3;
            this.degree.Text = "0";
            // 
            // rotation
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(269, 165);
            this.ControlBox = false;
            this.Controls.Add(this.degree);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.close);
            this.Controls.Add(this.startRot);
            this.Name = "rotation";
            this.Text = "图像旋转";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button startRot;
        private System.Windows.Forms.Button close;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox degree;
    }
}