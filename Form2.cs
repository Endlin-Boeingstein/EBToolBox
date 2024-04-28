using Ftp2Res;
using Newtonsoft.Json.Linq;
using ReaLTaiizor.Controls;
using ReaLTaiizor.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EBToolBox
{
    public partial class Form2 : MaterialForm
    {
        //定义rss
        public JObject rss = new JObject();
        //用以外部类修改
        public static Form2 form2;
        ///readonly MaterialSkin.MaterialSkinManager materialSkinManager;
        readonly ReaLTaiizor.Manager.MaterialSkinManager materialSkinManager;
        public Form2()
        {
            InitializeComponent();
            form2 = this;
            materialSkinManager = ReaLTaiizor.Manager.MaterialSkinManager.Instance;
            materialSkinManager.EnforceBackcolorOnAllComponents = true;
            ///materialSkinManager.AddFormToManage(this);
            materialSkinManager.Theme = ReaLTaiizor.Manager.MaterialSkinManager.Themes.LIGHT;
            materialSkinManager.ColorScheme = new ReaLTaiizor.Colors.MaterialColorScheme(ReaLTaiizor.Colors.MaterialPrimary.Indigo500, ReaLTaiizor.Colors.MaterialPrimary.Indigo700, ReaLTaiizor.Colors.MaterialPrimary.Indigo100, ReaLTaiizor.Colors.MaterialAccent.Pink200, ReaLTaiizor.Util.MaterialTextShade.WHITE);
        }
        public Form2(JObject rss)
        {
            this.rss = rss;
            InitializeComponent();
            form2 = this;
            materialSkinManager = ReaLTaiizor.Manager.MaterialSkinManager.Instance;
            materialSkinManager.EnforceBackcolorOnAllComponents = true;
            ///materialSkinManager.AddFormToManage(this);
            materialSkinManager.Theme = ReaLTaiizor.Manager.MaterialSkinManager.Themes.LIGHT;
            materialSkinManager.ColorScheme = new ReaLTaiizor.Colors.MaterialColorScheme(ReaLTaiizor.Colors.MaterialPrimary.Indigo500, ReaLTaiizor.Colors.MaterialPrimary.Indigo700, ReaLTaiizor.Colors.MaterialPrimary.Indigo100, ReaLTaiizor.Colors.MaterialAccent.Pink200, ReaLTaiizor.Util.MaterialTextShade.WHITE);
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            //将当前窗体实例复制给全局常量Form
            form2 = this;
            //这个是允许其他线程控制本窗体控件
            System.Windows.Forms.Control.CheckForIllegalCrossThreadCalls = false;
        }

        private void checkedListBox1_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            if (e.CurrentValue == CheckState.Checked) return;//取消选中就不用进行以下操作
            var index = checkedListBox1.SelectedIndices;//获取此时鼠标点击了哪个项
            if (index.Contains(0) || index.Contains(1) || index.Contains(2) || index.Contains(3) || index.Contains(4) || index.Contains(5) || index.Contains(6) || index.Contains(7) || index.Contains(8) || index.Contains(9) || index.Contains(10) || index.Contains(11) || index.Contains(12) || index.Contains(13) || index.Contains(14))
            {
                ((CheckedListBox)sender).SetItemChecked(15, false);
                ((CheckedListBox)sender).SetItemChecked(16, false);
            }
            if (index.Contains(15))
            {
                ((CheckedListBox)sender).SetSelected(0, true);
                ((CheckedListBox)sender).SetItemChecked(0, true);
                ((CheckedListBox)sender).SetSelected(1, true);
                ((CheckedListBox)sender).SetItemChecked(1, true);
                ((CheckedListBox)sender).SetSelected(2, true);
                ((CheckedListBox)sender).SetItemChecked(2, true);
                ((CheckedListBox)sender).SetSelected(3, true);
                ((CheckedListBox)sender).SetItemChecked(3, true);
                ((CheckedListBox)sender).SetSelected(4, true);
                ((CheckedListBox)sender).SetItemChecked(4, true);
                ((CheckedListBox)sender).SetSelected(5, true);
                ((CheckedListBox)sender).SetItemChecked(5, true);
                ((CheckedListBox)sender).SetSelected(6, true);
                ((CheckedListBox)sender).SetItemChecked(6, true);
                ((CheckedListBox)sender).SetSelected(7, true);
                ((CheckedListBox)sender).SetItemChecked(7, true);
                ((CheckedListBox)sender).SetSelected(8, true);
                ((CheckedListBox)sender).SetItemChecked(8, true);
                ((CheckedListBox)sender).SetSelected(9, true);
                ((CheckedListBox)sender).SetItemChecked(9, true);
                ((CheckedListBox)sender).SetSelected(10, true);
                ((CheckedListBox)sender).SetItemChecked(10, true);
                ((CheckedListBox)sender).SetSelected(11, true);
                ((CheckedListBox)sender).SetItemChecked(11, true);
                ((CheckedListBox)sender).SetSelected(12, true);
                ((CheckedListBox)sender).SetItemChecked(12, true);
                ((CheckedListBox)sender).SetSelected(13, true);
                ((CheckedListBox)sender).SetItemChecked(13, true);
                ((CheckedListBox)sender).SetSelected(14, true);
                ((CheckedListBox)sender).SetItemChecked(14, true);
                //this.checkedListBox1.SetItemChecked(1, true);
                //this.checkedListBox1.SetItemChecked(2, true);
                //this.checkedListBox1.SetItemChecked(3, true);
                //this.checkedListBox1.SetItemChecked(4, true);
                //this.checkedListBox1.SetItemChecked(5, true);
                //this.checkedListBox1.SetItemChecked(6, true);
                //this.checkedListBox1.SetItemChecked(7, true);
                //this.checkedListBox1.SetItemChecked(8, true);
                //this.checkedListBox1.SetItemChecked(9, true);
                //this.checkedListBox1.SetItemChecked(10, true);
                //this.checkedListBox1.SetItemChecked(11, true);
                //this.checkedListBox1.SetItemChecked(12, true);
                //this.checkedListBox1.SetItemChecked(13, true);
                //this.checkedListBox1.SetItemChecked(14, true);
                //this.checkedListBox1.SetItemChecked(15, false);
                //this.checkedListBox1.SetItemChecked(16, false);
            }
            if (index.Contains(16))
            {
                e.NewValue = CheckState.Checked;//刷新
                ((CheckedListBox)sender).SetItemChecked(0, false);
                ((CheckedListBox)sender).SetItemChecked(1, false);
                ((CheckedListBox)sender).SetItemChecked(2, false);
                ((CheckedListBox)sender).SetItemChecked(3, false);
                ((CheckedListBox)sender).SetItemChecked(4, false);
                ((CheckedListBox)sender).SetItemChecked(5, false);
                ((CheckedListBox)sender).SetItemChecked(6, false);
                ((CheckedListBox)sender).SetItemChecked(7, false);
                ((CheckedListBox)sender).SetItemChecked(8, false);
                ((CheckedListBox)sender).SetItemChecked(9, false);
                ((CheckedListBox)sender).SetItemChecked(10, false);
                ((CheckedListBox)sender).SetItemChecked(11, false);
                ((CheckedListBox)sender).SetItemChecked(12, false);
                ((CheckedListBox)sender).SetItemChecked(13, false);
                ((CheckedListBox)sender).SetItemChecked(14, false);
            }
            else { }
            e.NewValue = CheckState.Checked;//刷新
        }

        private void Form2_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
        }

        private void materialButton20_Click(object sender, EventArgs e)
        {
            Task.Run(() =>
            {
                materialButton20.Enabled = false;
                //创建偏移调整对象oa
                Ftp2Res.OffsetAdjuster oa = new OffsetAdjuster();
                rss = oa.OffsetAdjust(rss);
                materialButton20.Enabled = true;
            });
        }

        private void materialButton1_Click(object sender, EventArgs e)
        {
            //关闭窗口
            this.Close();
            //关闭线程以让主线程停止等待
            this.Dispose();
        }
    }
}
