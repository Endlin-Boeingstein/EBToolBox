using Ftp2Res;
using Newtonsoft.Json.Linq;
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
    public partial class Form3 : MaterialForm
    {
        //定义rss
        public JObject rss = new JObject();
        //用以外部类修改
        public static Form3 form3;
        ///readonly MaterialSkin.MaterialSkinManager materialSkinManager;
        readonly ReaLTaiizor.Manager.MaterialSkinManager materialSkinManager;
        public Form3()
        {
            InitializeComponent();
            form3 = this;
            materialSkinManager = ReaLTaiizor.Manager.MaterialSkinManager.Instance;
            materialSkinManager.EnforceBackcolorOnAllComponents = true;
            ///materialSkinManager.AddFormToManage(this);
            materialSkinManager.Theme = ReaLTaiizor.Manager.MaterialSkinManager.Themes.LIGHT;
            materialSkinManager.ColorScheme = new ReaLTaiizor.Colors.MaterialColorScheme(ReaLTaiizor.Colors.MaterialPrimary.Indigo500, ReaLTaiizor.Colors.MaterialPrimary.Indigo700, ReaLTaiizor.Colors.MaterialPrimary.Indigo100, ReaLTaiizor.Colors.MaterialAccent.Pink200, ReaLTaiizor.Util.MaterialTextShade.WHITE);
        }

        public Form3(JObject rss)
        {
            this.rss = rss;
            InitializeComponent();
            form3 = this;
            materialSkinManager = ReaLTaiizor.Manager.MaterialSkinManager.Instance;
            materialSkinManager.EnforceBackcolorOnAllComponents = true;
            ///materialSkinManager.AddFormToManage(this);
            materialSkinManager.Theme = ReaLTaiizor.Manager.MaterialSkinManager.Themes.LIGHT;
            materialSkinManager.ColorScheme = new ReaLTaiizor.Colors.MaterialColorScheme(ReaLTaiizor.Colors.MaterialPrimary.Indigo500, ReaLTaiizor.Colors.MaterialPrimary.Indigo700, ReaLTaiizor.Colors.MaterialPrimary.Indigo100, ReaLTaiizor.Colors.MaterialAccent.Pink200, ReaLTaiizor.Util.MaterialTextShade.WHITE);
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            //将当前窗体实例复制给全局常量Form
            form3 = this;
            //这个是允许其他线程控制本窗体控件
            System.Windows.Forms.Control.CheckForIllegalCrossThreadCalls = false;
        }

        private void Form3_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
        }

        private void materialButton20_Click(object sender, EventArgs e)
        {
            Task.Run(() =>
            {
                materialButton20.Enabled = false;
                //创建加密位图添加对象esa20240331添加
                Ftp2Res.EncryptSpriteAdder esa = new EncryptSpriteAdder();
                rss = esa.EncryptSpriteAdd(rss);
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

        //Ftp2Res加密标识添加选项选中检测
        private void radioButton31_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton31.Checked && !radioButton32.Checked)
            {
                textBox20.Enabled = true;
                materialButton20.Enabled = true;
            }
            else if (!radioButton31.Checked && radioButton32.Checked)
            {
                textBox20.Enabled = false;
                materialButton20.Enabled = false;
            }
            else
            {
                textBox20.Enabled = false;
                materialButton20.Enabled = false;
            }
        }

        private void radioButton32_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton31.Checked && !radioButton32.Checked)
            {
                textBox20.Enabled = true;
                materialButton20.Enabled = true;
            }
            else if (!radioButton31.Checked && radioButton32.Checked)
            {
                textBox20.Enabled = false;
                materialButton20.Enabled = false;
            }
            else
            {
                textBox20.Enabled = false;
                materialButton20.Enabled = false;
            }
        }
    }
}
