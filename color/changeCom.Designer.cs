namespace color
{
    partial class changeCom
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
            this.redTB = new System.Windows.Forms.TrackBar();
            this.greenTB = new System.Windows.Forms.TrackBar();
            this.blueTB = new System.Windows.Forms.TrackBar();
            this.hueTB = new System.Windows.Forms.TrackBar();
            this.satTB = new System.Windows.Forms.TrackBar();
            this.intTB = new System.Windows.Forms.TrackBar();
            this.redUD = new System.Windows.Forms.NumericUpDown();
            this.greenUD = new System.Windows.Forms.NumericUpDown();
            this.blueUD = new System.Windows.Forms.NumericUpDown();
            this.hueUD = new System.Windows.Forms.NumericUpDown();
            this.satUD = new System.Windows.Forms.NumericUpDown();
            this.intUD = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            ((System.ComponentModel.ISupportInitialize)(this.redTB)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.greenTB)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.blueTB)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.hueTB)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.satTB)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.intTB)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.redUD)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.greenUD)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.blueUD)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.hueUD)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.satUD)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.intUD)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // close
            // 
            this.close.Location = new System.Drawing.Point(350, 305);
            this.close.Name = "close";
            this.close.Size = new System.Drawing.Size(75, 23);
            this.close.TabIndex = 0;
            this.close.Text = "退出";
            this.close.UseVisualStyleBackColor = true;
            this.close.Click += new System.EventHandler(this.close_Click);
            // 
            // redTB
            // 
            this.redTB.Location = new System.Drawing.Point(83, 24);
            this.redTB.Maximum = 100;
            this.redTB.Minimum = -100;
            this.redTB.Name = "redTB";
            this.redTB.Size = new System.Drawing.Size(229, 45);
            this.redTB.TabIndex = 1;
            this.redTB.Scroll += new System.EventHandler(this.redTB_Scroll);
            // 
            // greenTB
            // 
            this.greenTB.Location = new System.Drawing.Point(83, 92);
            this.greenTB.Maximum = 100;
            this.greenTB.Minimum = -100;
            this.greenTB.Name = "greenTB";
            this.greenTB.Size = new System.Drawing.Size(229, 45);
            this.greenTB.TabIndex = 2;
            this.greenTB.Scroll += new System.EventHandler(this.greenTB_Scroll);
            // 
            // blueTB
            // 
            this.blueTB.Location = new System.Drawing.Point(83, 160);
            this.blueTB.Maximum = 100;
            this.blueTB.Minimum = -100;
            this.blueTB.Name = "blueTB";
            this.blueTB.Size = new System.Drawing.Size(229, 45);
            this.blueTB.TabIndex = 3;
            this.blueTB.Scroll += new System.EventHandler(this.blueTB_Scroll);
            // 
            // hueTB
            // 
            this.hueTB.Location = new System.Drawing.Point(83, 24);
            this.hueTB.Maximum = 180;
            this.hueTB.Minimum = -180;
            this.hueTB.Name = "hueTB";
            this.hueTB.Size = new System.Drawing.Size(229, 45);
            this.hueTB.TabIndex = 4;
            this.hueTB.Scroll += new System.EventHandler(this.hueTB_Scroll);
            // 
            // satTB
            // 
            this.satTB.Location = new System.Drawing.Point(83, 92);
            this.satTB.Maximum = 100;
            this.satTB.Minimum = -100;
            this.satTB.Name = "satTB";
            this.satTB.Size = new System.Drawing.Size(229, 45);
            this.satTB.TabIndex = 5;
            this.satTB.Scroll += new System.EventHandler(this.satTB_Scroll);
            // 
            // intTB
            // 
            this.intTB.Location = new System.Drawing.Point(83, 160);
            this.intTB.Maximum = 100;
            this.intTB.Minimum = -100;
            this.intTB.Name = "intTB";
            this.intTB.Size = new System.Drawing.Size(229, 45);
            this.intTB.TabIndex = 6;
            this.intTB.Scroll += new System.EventHandler(this.intTB_Scroll);
            // 
            // redUD
            // 
            this.redUD.Location = new System.Drawing.Point(327, 28);
            this.redUD.Minimum = new decimal(new int[] {
            100,
            0,
            0,
            -2147483648});
            this.redUD.Name = "redUD";
            this.redUD.Size = new System.Drawing.Size(50, 21);
            this.redUD.TabIndex = 7;
            this.redUD.ValueChanged += new System.EventHandler(this.redUD_ValueChanged);
            // 
            // greenUD
            // 
            this.greenUD.Location = new System.Drawing.Point(327, 96);
            this.greenUD.Minimum = new decimal(new int[] {
            100,
            0,
            0,
            -2147483648});
            this.greenUD.Name = "greenUD";
            this.greenUD.Size = new System.Drawing.Size(50, 21);
            this.greenUD.TabIndex = 8;
            this.greenUD.ValueChanged += new System.EventHandler(this.greenUD_ValueChanged);
            // 
            // blueUD
            // 
            this.blueUD.Location = new System.Drawing.Point(327, 164);
            this.blueUD.Minimum = new decimal(new int[] {
            100,
            0,
            0,
            -2147483648});
            this.blueUD.Name = "blueUD";
            this.blueUD.Size = new System.Drawing.Size(50, 21);
            this.blueUD.TabIndex = 9;
            this.blueUD.ValueChanged += new System.EventHandler(this.blueUD_ValueChanged);
            // 
            // hueUD
            // 
            this.hueUD.Location = new System.Drawing.Point(327, 28);
            this.hueUD.Maximum = new decimal(new int[] {
            180,
            0,
            0,
            0});
            this.hueUD.Minimum = new decimal(new int[] {
            180,
            0,
            0,
            -2147483648});
            this.hueUD.Name = "hueUD";
            this.hueUD.Size = new System.Drawing.Size(50, 21);
            this.hueUD.TabIndex = 10;
            this.hueUD.ValueChanged += new System.EventHandler(this.hueUD_ValueChanged);
            // 
            // satUD
            // 
            this.satUD.Location = new System.Drawing.Point(327, 96);
            this.satUD.Minimum = new decimal(new int[] {
            100,
            0,
            0,
            -2147483648});
            this.satUD.Name = "satUD";
            this.satUD.Size = new System.Drawing.Size(50, 21);
            this.satUD.TabIndex = 11;
            this.satUD.ValueChanged += new System.EventHandler(this.satUD_ValueChanged);
            // 
            // intUD
            // 
            this.intUD.Location = new System.Drawing.Point(327, 164);
            this.intUD.Minimum = new decimal(new int[] {
            100,
            0,
            0,
            -2147483648});
            this.intUD.Name = "intUD";
            this.intUD.Size = new System.Drawing.Size(50, 21);
            this.intUD.TabIndex = 12;
            this.intUD.ValueChanged += new System.EventHandler(this.intUD_ValueChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(71, 12);
            this.label1.TabIndex = 13;
            this.label1.Text = "红色（R）：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 98);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(71, 12);
            this.label2.TabIndex = 14;
            this.label2.Text = "绿色（G）：";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 166);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(71, 12);
            this.label3.TabIndex = 15;
            this.label3.Text = "蓝色（B）：";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 30);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(71, 12);
            this.label4.TabIndex = 16;
            this.label4.Text = "色度（H）：";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 98);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(83, 12);
            this.label5.TabIndex = 17;
            this.label5.Text = "饱和度（S）：";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(6, 166);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(71, 12);
            this.label6.TabIndex = 18;
            this.label6.Text = "亮度（I）：";
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(45, 25);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(406, 258);
            this.tabControl1.TabIndex = 19;
            this.tabControl1.SelectedIndexChanged += new System.EventHandler(this.tabControl1_SelectedIndexChanged);
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Controls.Add(this.redTB);
            this.tabPage1.Controls.Add(this.redUD);
            this.tabPage1.Controls.Add(this.label2);
            this.tabPage1.Controls.Add(this.label3);
            this.tabPage1.Controls.Add(this.greenTB);
            this.tabPage1.Controls.Add(this.greenUD);
            this.tabPage1.Controls.Add(this.blueUD);
            this.tabPage1.Controls.Add(this.blueTB);
            this.tabPage1.Location = new System.Drawing.Point(4, 21);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(398, 233);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "RGB空间";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.label4);
            this.tabPage2.Controls.Add(this.intUD);
            this.tabPage2.Controls.Add(this.label6);
            this.tabPage2.Controls.Add(this.intTB);
            this.tabPage2.Controls.Add(this.hueTB);
            this.tabPage2.Controls.Add(this.label5);
            this.tabPage2.Controls.Add(this.satUD);
            this.tabPage2.Controls.Add(this.hueUD);
            this.tabPage2.Controls.Add(this.satTB);
            this.tabPage2.Location = new System.Drawing.Point(4, 21);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(398, 233);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "HSI空间";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // changeCom
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(497, 355);
            this.ControlBox = false;
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.close);
            this.Name = "changeCom";
            this.Text = "彩色分量调整";
            ((System.ComponentModel.ISupportInitialize)(this.redTB)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.greenTB)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.blueTB)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.hueTB)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.satTB)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.intTB)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.redUD)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.greenUD)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.blueUD)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.hueUD)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.satUD)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.intUD)).EndInit();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button close;
        private System.Windows.Forms.TrackBar redTB;
        private System.Windows.Forms.TrackBar greenTB;
        private System.Windows.Forms.TrackBar blueTB;
        private System.Windows.Forms.TrackBar hueTB;
        private System.Windows.Forms.TrackBar satTB;
        private System.Windows.Forms.TrackBar intTB;
        private System.Windows.Forms.NumericUpDown redUD;
        private System.Windows.Forms.NumericUpDown greenUD;
        private System.Windows.Forms.NumericUpDown blueUD;
        private System.Windows.Forms.NumericUpDown hueUD;
        private System.Windows.Forms.NumericUpDown satUD;
        private System.Windows.Forms.NumericUpDown intUD;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private Form1 masterF;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
    }
}