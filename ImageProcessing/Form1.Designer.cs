namespace ImageProcessing
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tab_file = new System.Windows.Forms.TabPage();
            this.btn_restore = new System.Windows.Forms.Button();
            this.btn_open = new System.Windows.Forms.Button();
            this.btn_save = new System.Windows.Forms.Button();
            this.tab_changepixel = new System.Windows.Forms.TabPage();
            this.btn_change = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.pixeltxbB = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.pixeltxbG = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.pixeltxbR = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label6 = new System.Windows.Forms.Label();
            this.ytxb = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.xtxb = new System.Windows.Forms.TextBox();
            this.tab_preprocessing = new System.Windows.Forms.TabPage();
            this.skinEngine1 = new Sunisoft.IrisSkin.SkinEngine();
            this.btn_gray = new System.Windows.Forms.Button();
            this.btn_binaryzation = new System.Windows.Forms.Button();
            this.tabControl1.SuspendLayout();
            this.tab_file.SuspendLayout();
            this.tab_changepixel.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.tab_preprocessing.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tab_file);
            this.tabControl1.Controls.Add(this.tab_changepixel);
            this.tabControl1.Controls.Add(this.tab_preprocessing);
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1318, 79);
            this.tabControl1.TabIndex = 0;
            // 
            // tab_file
            // 
            this.tab_file.Controls.Add(this.btn_restore);
            this.tab_file.Controls.Add(this.btn_open);
            this.tab_file.Controls.Add(this.btn_save);
            this.tab_file.Location = new System.Drawing.Point(4, 22);
            this.tab_file.Name = "tab_file";
            this.tab_file.Padding = new System.Windows.Forms.Padding(3);
            this.tab_file.Size = new System.Drawing.Size(1310, 53);
            this.tab_file.TabIndex = 0;
            this.tab_file.Text = "文件";
            this.tab_file.UseVisualStyleBackColor = true;
            // 
            // btn_restore
            // 
            this.btn_restore.FlatAppearance.BorderSize = 0;
            this.btn_restore.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_restore.Image = ((System.Drawing.Image)(resources.GetObject("btn_restore.Image")));
            this.btn_restore.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btn_restore.Location = new System.Drawing.Point(336, 14);
            this.btn_restore.Name = "btn_restore";
            this.btn_restore.Size = new System.Drawing.Size(84, 26);
            this.btn_restore.TabIndex = 3;
            this.btn_restore.Text = "还原图像";
            this.btn_restore.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btn_restore.UseVisualStyleBackColor = true;
            this.btn_restore.Click += new System.EventHandler(this.btn_restore_Click);
            // 
            // btn_open
            // 
            this.btn_open.FlatAppearance.BorderSize = 0;
            this.btn_open.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_open.Image = ((System.Drawing.Image)(resources.GetObject("btn_open.Image")));
            this.btn_open.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btn_open.Location = new System.Drawing.Point(73, 14);
            this.btn_open.Name = "btn_open";
            this.btn_open.Size = new System.Drawing.Size(84, 26);
            this.btn_open.TabIndex = 2;
            this.btn_open.Text = "打开文件";
            this.btn_open.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btn_open.UseVisualStyleBackColor = true;
            this.btn_open.Click += new System.EventHandler(this.btn_open_Click);
            // 
            // btn_save
            // 
            this.btn_save.FlatAppearance.BorderSize = 0;
            this.btn_save.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_save.Image = ((System.Drawing.Image)(resources.GetObject("btn_save.Image")));
            this.btn_save.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btn_save.Location = new System.Drawing.Point(208, 14);
            this.btn_save.Name = "btn_save";
            this.btn_save.Size = new System.Drawing.Size(84, 26);
            this.btn_save.TabIndex = 1;
            this.btn_save.Text = "保存文件";
            this.btn_save.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btn_save.UseVisualStyleBackColor = true;
            this.btn_save.Click += new System.EventHandler(this.btn_save_Click);
            // 
            // tab_changepixel
            // 
            this.tab_changepixel.Controls.Add(this.btn_change);
            this.tab_changepixel.Controls.Add(this.groupBox1);
            this.tab_changepixel.Controls.Add(this.groupBox2);
            this.tab_changepixel.Location = new System.Drawing.Point(4, 22);
            this.tab_changepixel.Name = "tab_changepixel";
            this.tab_changepixel.Padding = new System.Windows.Forms.Padding(3);
            this.tab_changepixel.Size = new System.Drawing.Size(1310, 53);
            this.tab_changepixel.TabIndex = 1;
            this.tab_changepixel.Text = "更改像素";
            this.tab_changepixel.UseVisualStyleBackColor = true;
            // 
            // btn_change
            // 
            this.btn_change.FlatAppearance.BorderSize = 0;
            this.btn_change.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_change.Image = ((System.Drawing.Image)(resources.GetObject("btn_change.Image")));
            this.btn_change.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btn_change.Location = new System.Drawing.Point(1049, 16);
            this.btn_change.Name = "btn_change";
            this.btn_change.Size = new System.Drawing.Size(84, 26);
            this.btn_change.TabIndex = 14;
            this.btn_change.Text = "确定更改";
            this.btn_change.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btn_change.UseVisualStyleBackColor = true;
            this.btn_change.Click += new System.EventHandler(this.btn_change_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.pixeltxbB);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.pixeltxbG);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.pixeltxbR);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Location = new System.Drawing.Point(586, 6);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(356, 50);
            this.groupBox1.TabIndex = 13;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "更新像素值";
            // 
            // pixeltxbB
            // 
            this.pixeltxbB.Location = new System.Drawing.Point(272, 21);
            this.pixeltxbB.Name = "pixeltxbB";
            this.pixeltxbB.Size = new System.Drawing.Size(66, 21);
            this.pixeltxbB.TabIndex = 10;
            this.pixeltxbB.Text = "0";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(249, 24);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(23, 12);
            this.label5.TabIndex = 9;
            this.label5.Text = "B：";
            // 
            // pixeltxbG
            // 
            this.pixeltxbG.Location = new System.Drawing.Point(166, 21);
            this.pixeltxbG.Name = "pixeltxbG";
            this.pixeltxbG.Size = new System.Drawing.Size(66, 21);
            this.pixeltxbG.TabIndex = 8;
            this.pixeltxbG.Text = "0";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(143, 24);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(23, 12);
            this.label4.TabIndex = 7;
            this.label4.Text = "G：";
            // 
            // pixeltxbR
            // 
            this.pixeltxbR.Location = new System.Drawing.Point(59, 21);
            this.pixeltxbR.Name = "pixeltxbR";
            this.pixeltxbR.Size = new System.Drawing.Size(66, 21);
            this.pixeltxbR.TabIndex = 6;
            this.pixeltxbR.Text = "0";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(36, 24);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(23, 12);
            this.label3.TabIndex = 5;
            this.label3.Text = "R：";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.ytxb);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.xtxb);
            this.groupBox2.Location = new System.Drawing.Point(122, 6);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(328, 47);
            this.groupBox2.TabIndex = 12;
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
            this.ytxb.Location = new System.Drawing.Point(238, 20);
            this.ytxb.Name = "ytxb";
            this.ytxb.Size = new System.Drawing.Size(66, 21);
            this.ytxb.TabIndex = 7;
            this.ytxb.Text = "0";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(185, 23);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 5;
            this.label2.Text = "纵坐标：";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(31, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 4;
            this.label1.Text = "横坐标：";
            // 
            // xtxb
            // 
            this.xtxb.Location = new System.Drawing.Point(84, 20);
            this.xtxb.Name = "xtxb";
            this.xtxb.Size = new System.Drawing.Size(66, 21);
            this.xtxb.TabIndex = 6;
            this.xtxb.Text = "0";
            // 
            // tab_preprocessing
            // 
            this.tab_preprocessing.Controls.Add(this.btn_binaryzation);
            this.tab_preprocessing.Controls.Add(this.btn_gray);
            this.tab_preprocessing.Location = new System.Drawing.Point(4, 22);
            this.tab_preprocessing.Name = "tab_preprocessing";
            this.tab_preprocessing.Size = new System.Drawing.Size(1310, 53);
            this.tab_preprocessing.TabIndex = 2;
            this.tab_preprocessing.Text = "图像预处理";
            this.tab_preprocessing.UseVisualStyleBackColor = true;
            // 
            // skinEngine1
            // 
            this.skinEngine1.@__DrawButtonFocusRectangle = true;
            this.skinEngine1.DisabledButtonTextColor = System.Drawing.Color.Gray;
            this.skinEngine1.DisabledMenuFontColor = System.Drawing.SystemColors.GrayText;
            this.skinEngine1.InactiveCaptionColor = System.Drawing.SystemColors.InactiveCaptionText;
            this.skinEngine1.SerialNumber = "";
            this.skinEngine1.SkinFile = null;
            // 
            // btn_gray
            // 
            this.btn_gray.FlatAppearance.BorderSize = 0;
            this.btn_gray.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_gray.Image = ((System.Drawing.Image)(resources.GetObject("btn_gray.Image")));
            this.btn_gray.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btn_gray.Location = new System.Drawing.Point(85, 14);
            this.btn_gray.Name = "btn_gray";
            this.btn_gray.Size = new System.Drawing.Size(70, 26);
            this.btn_gray.TabIndex = 4;
            this.btn_gray.Text = "灰度化";
            this.btn_gray.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btn_gray.UseVisualStyleBackColor = true;
            this.btn_gray.Click += new System.EventHandler(this.btn_gray_Click);
            // 
            // btn_binaryzation
            // 
            this.btn_binaryzation.FlatAppearance.BorderSize = 0;
            this.btn_binaryzation.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_binaryzation.Image = ((System.Drawing.Image)(resources.GetObject("btn_binaryzation.Image")));
            this.btn_binaryzation.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btn_binaryzation.Location = new System.Drawing.Point(195, 14);
            this.btn_binaryzation.Name = "btn_binaryzation";
            this.btn_binaryzation.Size = new System.Drawing.Size(70, 26);
            this.btn_binaryzation.TabIndex = 5;
            this.btn_binaryzation.Text = "二值化";
            this.btn_binaryzation.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btn_binaryzation.UseVisualStyleBackColor = true;
            this.btn_binaryzation.Click += new System.EventHandler(this.btn_binaryzation_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1318, 721);
            this.Controls.Add(this.tabControl1);
            this.Name = "Form1";
            this.Text = "图像处理";
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.Form1_Paint);
            this.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseDoubleClick);
            this.tabControl1.ResumeLayout(false);
            this.tab_file.ResumeLayout(false);
            this.tab_changepixel.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.tab_preprocessing.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tab_file;
        private System.Windows.Forms.TabPage tab_changepixel;
        private System.Windows.Forms.TabPage tab_preprocessing;
        private Sunisoft.IrisSkin.SkinEngine skinEngine1;
        private System.Windows.Forms.Button btn_save;
        private System.Windows.Forms.Button btn_open;
        private System.Windows.Forms.Button btn_restore;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox ytxb;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox xtxb;
        private System.Windows.Forms.Button btn_change;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox pixeltxbB;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox pixeltxbG;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox pixeltxbR;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btn_gray;
        private System.Windows.Forms.Button btn_binaryzation;
    }
}

