namespace EBToolBox
{
    partial class Form3
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
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.materialTabControl1 = new ReaLTaiizor.Controls.MaterialTabControl();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.materialButton1 = new ReaLTaiizor.Controls.MaterialButton();
            this.materialButton20 = new ReaLTaiizor.Controls.MaterialButton();
            this.panel12 = new System.Windows.Forms.Panel();
            this.textBox20 = new System.Windows.Forms.TextBox();
            this.radioButton31 = new System.Windows.Forms.RadioButton();
            this.radioButton32 = new System.Windows.Forms.RadioButton();
            this.foxLabel44 = new ReaLTaiizor.Controls.FoxLabel();
            this.materialTabControl1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.panel12.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabPage1
            // 
            this.tabPage1.AutoScroll = true;
            this.tabPage1.Location = new System.Drawing.Point(0, 0);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Size = new System.Drawing.Size(200, 100);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "tabPage1";
            // 
            // materialTabControl1
            // 
            this.materialTabControl1.Controls.Add(this.tabPage2);
            this.materialTabControl1.Depth = 0;
            this.materialTabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.materialTabControl1.Location = new System.Drawing.Point(3, 24);
            this.materialTabControl1.MouseState = ReaLTaiizor.Helper.MaterialDrawHelper.MaterialMouseState.HOVER;
            this.materialTabControl1.Multiline = true;
            this.materialTabControl1.Name = "materialTabControl1";
            this.materialTabControl1.SelectedIndex = 0;
            this.materialTabControl1.Size = new System.Drawing.Size(1066, 651);
            this.materialTabControl1.TabIndex = 0;
            // 
            // tabPage2
            // 
            this.tabPage2.AutoScroll = true;
            this.tabPage2.Controls.Add(this.materialButton1);
            this.tabPage2.Controls.Add(this.materialButton20);
            this.tabPage2.Controls.Add(this.panel12);
            this.tabPage2.Controls.Add(this.foxLabel44);
            this.tabPage2.Location = new System.Drawing.Point(4, 28);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(1058, 619);
            this.tabPage2.TabIndex = 0;
            this.tabPage2.Text = "EncryptSpriteAdd";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // materialButton1
            // 
            this.materialButton1.AutoSize = false;
            this.materialButton1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.materialButton1.Density = ReaLTaiizor.Controls.MaterialButton.MaterialButtonDensity.Default;
            this.materialButton1.Depth = 0;
            this.materialButton1.Font = new System.Drawing.Font("宋体", 12F);
            this.materialButton1.HighEmphasis = true;
            this.materialButton1.Icon = null;
            this.materialButton1.IconType = ReaLTaiizor.Controls.MaterialButton.MaterialIconType.Rebase;
            this.materialButton1.Location = new System.Drawing.Point(919, 531);
            this.materialButton1.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.materialButton1.MouseState = ReaLTaiizor.Helper.MaterialDrawHelper.MaterialMouseState.HOVER;
            this.materialButton1.Name = "materialButton1";
            this.materialButton1.NoAccentTextColor = System.Drawing.Color.Empty;
            this.materialButton1.Size = new System.Drawing.Size(103, 44);
            this.materialButton1.TabIndex = 32;
            this.materialButton1.Text = "退出";
            this.materialButton1.Type = ReaLTaiizor.Controls.MaterialButton.MaterialButtonType.Contained;
            this.materialButton1.UseAccentColor = false;
            this.materialButton1.UseVisualStyleBackColor = true;
            this.materialButton1.Click += new System.EventHandler(this.materialButton1_Click);
            // 
            // materialButton20
            // 
            this.materialButton20.AutoSize = false;
            this.materialButton20.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.materialButton20.Density = ReaLTaiizor.Controls.MaterialButton.MaterialButtonDensity.Default;
            this.materialButton20.Depth = 0;
            this.materialButton20.Enabled = false;
            this.materialButton20.Font = new System.Drawing.Font("宋体", 12F);
            this.materialButton20.HighEmphasis = true;
            this.materialButton20.Icon = null;
            this.materialButton20.IconType = ReaLTaiizor.Controls.MaterialButton.MaterialIconType.Rebase;
            this.materialButton20.Location = new System.Drawing.Point(98, 531);
            this.materialButton20.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.materialButton20.MouseState = ReaLTaiizor.Helper.MaterialDrawHelper.MaterialMouseState.HOVER;
            this.materialButton20.Name = "materialButton20";
            this.materialButton20.NoAccentTextColor = System.Drawing.Color.Empty;
            this.materialButton20.Size = new System.Drawing.Size(813, 44);
            this.materialButton20.TabIndex = 31;
            this.materialButton20.Text = "运行";
            this.materialButton20.Type = ReaLTaiizor.Controls.MaterialButton.MaterialButtonType.Contained;
            this.materialButton20.UseAccentColor = false;
            this.materialButton20.UseVisualStyleBackColor = true;
            this.materialButton20.Click += new System.EventHandler(this.materialButton20_Click);
            // 
            // panel12
            // 
            this.panel12.Controls.Add(this.textBox20);
            this.panel12.Controls.Add(this.radioButton31);
            this.panel12.Controls.Add(this.radioButton32);
            this.panel12.Location = new System.Drawing.Point(276, 22);
            this.panel12.Name = "panel12";
            this.panel12.Size = new System.Drawing.Size(776, 48);
            this.panel12.TabIndex = 30;
            // 
            // textBox20
            // 
            this.textBox20.Enabled = false;
            this.textBox20.Location = new System.Drawing.Point(680, 11);
            this.textBox20.Name = "textBox20";
            this.textBox20.Size = new System.Drawing.Size(93, 28);
            this.textBox20.TabIndex = 19;
            // 
            // radioButton31
            // 
            this.radioButton31.AutoSize = true;
            this.radioButton31.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radioButton31.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(76)))), ((int)(((byte)(88)))), ((int)(((byte)(100)))));
            this.radioButton31.Location = new System.Drawing.Point(537, 14);
            this.radioButton31.Name = "radioButton31";
            this.radioButton31.Size = new System.Drawing.Size(137, 29);
            this.radioButton31.TabIndex = 18;
            this.radioButton31.Text = "加密标识为";
            this.radioButton31.UseVisualStyleBackColor = true;
            this.radioButton31.CheckedChanged += new System.EventHandler(this.radioButton31_CheckedChanged);
            // 
            // radioButton32
            // 
            this.radioButton32.AutoSize = true;
            this.radioButton32.Checked = true;
            this.radioButton32.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radioButton32.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(76)))), ((int)(((byte)(88)))), ((int)(((byte)(100)))));
            this.radioButton32.Location = new System.Drawing.Point(0, 14);
            this.radioButton32.Name = "radioButton32";
            this.radioButton32.Size = new System.Drawing.Size(137, 29);
            this.radioButton32.TabIndex = 18;
            this.radioButton32.TabStop = true;
            this.radioButton32.Text = "不进行添加";
            this.radioButton32.UseVisualStyleBackColor = true;
            this.radioButton32.CheckedChanged += new System.EventHandler(this.radioButton32_CheckedChanged);
            // 
            // foxLabel44
            // 
            this.foxLabel44.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.foxLabel44.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.foxLabel44.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(76)))), ((int)(((byte)(88)))), ((int)(((byte)(100)))));
            this.foxLabel44.Location = new System.Drawing.Point(62, 33);
            this.foxLabel44.Name = "foxLabel44";
            this.foxLabel44.Size = new System.Drawing.Size(208, 39);
            this.foxLabel44.TabIndex = 29;
            this.foxLabel44.Text = "资源加密标识信息";
            // 
            // Form3
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1072, 678);
            this.Controls.Add(this.materialTabControl1);
            this.DrawerNonClickTabPage = new System.Windows.Forms.TabPage[] {
        this.tabPage1};
            this.DrawerTabControl = this.materialTabControl1;
            this.FormStyle = ReaLTaiizor.Enum.Material.FormStyles.ActionBar_None;
            this.Name = "Form3";
            this.Padding = new System.Windows.Forms.Padding(3, 24, 3, 3);
            this.Text = "EncryptSpriteAdd";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form3_FormClosing);
            this.Load += new System.EventHandler(this.Form3_Load);
            this.materialTabControl1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.panel12.ResumeLayout(false);
            this.panel12.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabPage tabPage1;
        private ReaLTaiizor.Controls.MaterialTabControl materialTabControl1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Panel panel12;
        public System.Windows.Forms.TextBox textBox20;
        public System.Windows.Forms.RadioButton radioButton31;
        public System.Windows.Forms.RadioButton radioButton32;
        private ReaLTaiizor.Controls.FoxLabel foxLabel44;
        private ReaLTaiizor.Controls.MaterialButton materialButton1;
        private ReaLTaiizor.Controls.MaterialButton materialButton20;
    }
}