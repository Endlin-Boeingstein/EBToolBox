namespace EBToolBox
{
    partial class Form2
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
            this.materialTabControl1 = new ReaLTaiizor.Controls.MaterialTabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.foxLabel3 = new ReaLTaiizor.Controls.FoxLabel();
            this.foxLabel2 = new ReaLTaiizor.Controls.FoxLabel();
            this.textBox14 = new System.Windows.Forms.TextBox();
            this.foxLabel1 = new ReaLTaiizor.Controls.FoxLabel();
            this.checkedListBox1 = new System.Windows.Forms.CheckedListBox();
            this.foxLabel32 = new ReaLTaiizor.Controls.FoxLabel();
            this.materialButton20 = new ReaLTaiizor.Controls.MaterialButton();
            this.materialButton1 = new ReaLTaiizor.Controls.MaterialButton();
            this.materialTabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.SuspendLayout();
            // 
            // materialTabControl1
            // 
            this.materialTabControl1.Controls.Add(this.tabPage1);
            this.materialTabControl1.Depth = 0;
            this.materialTabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.materialTabControl1.Location = new System.Drawing.Point(3, 24);
            this.materialTabControl1.Margin = new System.Windows.Forms.Padding(0);
            this.materialTabControl1.MouseState = ReaLTaiizor.Helper.MaterialDrawHelper.MaterialMouseState.HOVER;
            this.materialTabControl1.Multiline = true;
            this.materialTabControl1.Name = "materialTabControl1";
            this.materialTabControl1.SelectedIndex = 0;
            this.materialTabControl1.Size = new System.Drawing.Size(1066, 651);
            this.materialTabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.AutoScroll = true;
            this.tabPage1.Controls.Add(this.materialButton1);
            this.tabPage1.Controls.Add(this.textBox1);
            this.tabPage1.Controls.Add(this.foxLabel3);
            this.tabPage1.Controls.Add(this.foxLabel2);
            this.tabPage1.Controls.Add(this.textBox14);
            this.tabPage1.Controls.Add(this.foxLabel1);
            this.tabPage1.Controls.Add(this.checkedListBox1);
            this.tabPage1.Controls.Add(this.foxLabel32);
            this.tabPage1.Controls.Add(this.materialButton20);
            this.tabPage1.Location = new System.Drawing.Point(4, 28);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Size = new System.Drawing.Size(1058, 619);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "OffsetAdjustment";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(663, 100);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(103, 28);
            this.textBox1.TabIndex = 17;
            this.textBox1.Text = "0";
            // 
            // foxLabel3
            // 
            this.foxLabel3.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.foxLabel3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.foxLabel3.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.foxLabel3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(76)))), ((int)(((byte)(88)))), ((int)(((byte)(100)))));
            this.foxLabel3.Location = new System.Drawing.Point(633, 93);
            this.foxLabel3.Name = "foxLabel3";
            this.foxLabel3.Size = new System.Drawing.Size(24, 39);
            this.foxLabel3.TabIndex = 16;
            this.foxLabel3.Text = "y";
            // 
            // foxLabel2
            // 
            this.foxLabel2.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.foxLabel2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.foxLabel2.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.foxLabel2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(76)))), ((int)(((byte)(88)))), ((int)(((byte)(100)))));
            this.foxLabel2.Location = new System.Drawing.Point(633, 48);
            this.foxLabel2.Name = "foxLabel2";
            this.foxLabel2.Size = new System.Drawing.Size(24, 39);
            this.foxLabel2.TabIndex = 15;
            this.foxLabel2.Text = "x";
            // 
            // textBox14
            // 
            this.textBox14.Location = new System.Drawing.Point(663, 56);
            this.textBox14.Name = "textBox14";
            this.textBox14.Size = new System.Drawing.Size(103, 28);
            this.textBox14.TabIndex = 14;
            this.textBox14.Text = "0";
            // 
            // foxLabel1
            // 
            this.foxLabel1.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.foxLabel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.foxLabel1.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.foxLabel1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(76)))), ((int)(((byte)(88)))), ((int)(((byte)(100)))));
            this.foxLabel1.Location = new System.Drawing.Point(602, 3);
            this.foxLabel1.Name = "foxLabel1";
            this.foxLabel1.Size = new System.Drawing.Size(237, 39);
            this.foxLabel1.TabIndex = 12;
            this.foxLabel1.Text = "自定义偏移移动尺寸";
            // 
            // checkedListBox1
            // 
            this.checkedListBox1.BackColor = System.Drawing.Color.White;
            this.checkedListBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.checkedListBox1.CheckOnClick = true;
            this.checkedListBox1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkedListBox1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(76)))), ((int)(((byte)(88)))), ((int)(((byte)(100)))));
            this.checkedListBox1.FormattingEnabled = true;
            this.checkedListBox1.HorizontalScrollbar = true;
            this.checkedListBox1.Items.AddRange(new object[] {
            "1.卡槽世界背景",
            "2.植物",
            "3.冷却",
            "4.选择框",
            "5.右下角标",
            "6.下框点线",
            "7.能量闪电框",
            "8.金银铜等级牌",
            "9.植物锁",
            "10.禁植物图标",
            "11.家族圆形背景",
            "12.家族图标",
            "13.未来瓷砖",
            "14.上框点线",
            "15.商店种子包",
            "全选",
            "全不选"});
            this.checkedListBox1.Location = new System.Drawing.Point(98, 48);
            this.checkedListBox1.Name = "checkedListBox1";
            this.checkedListBox1.Size = new System.Drawing.Size(228, 476);
            this.checkedListBox1.TabIndex = 11;
            this.checkedListBox1.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.checkedListBox1_ItemCheck);
            // 
            // foxLabel32
            // 
            this.foxLabel32.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.foxLabel32.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.foxLabel32.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.foxLabel32.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(76)))), ((int)(((byte)(88)))), ((int)(((byte)(100)))));
            this.foxLabel32.Location = new System.Drawing.Point(98, 3);
            this.foxLabel32.Name = "foxLabel32";
            this.foxLabel32.Size = new System.Drawing.Size(208, 39);
            this.foxLabel32.TabIndex = 10;
            this.foxLabel32.Text = "偏移类型选择";
            // 
            // materialButton20
            // 
            this.materialButton20.AutoSize = false;
            this.materialButton20.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.materialButton20.Density = ReaLTaiizor.Controls.MaterialButton.MaterialButtonDensity.Default;
            this.materialButton20.Depth = 0;
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
            this.materialButton20.TabIndex = 7;
            this.materialButton20.Text = "运行";
            this.materialButton20.Type = ReaLTaiizor.Controls.MaterialButton.MaterialButtonType.Contained;
            this.materialButton20.UseAccentColor = false;
            this.materialButton20.UseVisualStyleBackColor = true;
            this.materialButton20.Click += new System.EventHandler(this.materialButton20_Click);
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
            this.materialButton1.TabIndex = 18;
            this.materialButton1.Text = "退出";
            this.materialButton1.Type = ReaLTaiizor.Controls.MaterialButton.MaterialButtonType.Contained;
            this.materialButton1.UseAccentColor = false;
            this.materialButton1.UseVisualStyleBackColor = true;
            this.materialButton1.Click += new System.EventHandler(this.materialButton1_Click);
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(1072, 678);
            this.Controls.Add(this.materialTabControl1);
            this.DrawerTabControl = this.materialTabControl1;
            this.FormStyle = ReaLTaiizor.Enum.Material.FormStyles.ActionBar_None;
            this.Name = "Form2";
            this.Padding = new System.Windows.Forms.Padding(3, 24, 3, 3);
            this.Text = "OffsetAdjustment";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form2_FormClosing);
            this.Load += new System.EventHandler(this.Form2_Load);
            this.materialTabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private ReaLTaiizor.Controls.MaterialTabControl materialTabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private ReaLTaiizor.Controls.MaterialButton materialButton20;
        private ReaLTaiizor.Controls.FoxLabel foxLabel32;
        private ReaLTaiizor.Controls.FoxLabel foxLabel1;
        private ReaLTaiizor.Controls.FoxLabel foxLabel3;
        private ReaLTaiizor.Controls.FoxLabel foxLabel2;
        public System.Windows.Forms.CheckedListBox checkedListBox1;
        public System.Windows.Forms.TextBox textBox14;
        public System.Windows.Forms.TextBox textBox1;
        private ReaLTaiizor.Controls.MaterialButton materialButton1;
    }
}