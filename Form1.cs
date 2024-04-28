using ReaLTaiizor.Forms;
using ReaLTaiizor.Colors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using Ftp2Res;
using System.Text.RegularExpressions;
using ResSC;
using BnkCvt;
using ClipCreator;
using Res2Ext;
using SpriteToLibrary;
using ClipTransformer;
using ReaLTaiizor.Controls;
using static EBToolBox.Form1;
using CCWin.Win32.Const;

namespace EBToolBox
{
    public partial class Form1 : MaterialForm
    {
        //用以外部类修改
        public static Form1 form1;
        ///readonly MaterialSkin.MaterialSkinManager materialSkinManager;
        readonly ReaLTaiizor.Manager.MaterialSkinManager materialSkinManager;
        public Form1()
        {
            InitializeComponent();
            form1 = this;
            materialSkinManager = ReaLTaiizor.Manager.MaterialSkinManager.Instance;
            materialSkinManager.EnforceBackcolorOnAllComponents = true;
            ///materialSkinManager.AddFormToManage(this);
            materialSkinManager.Theme = ReaLTaiizor.Manager.MaterialSkinManager.Themes.LIGHT;
            materialSkinManager.ColorScheme = new ReaLTaiizor.Colors.MaterialColorScheme(ReaLTaiizor.Colors.MaterialPrimary.Indigo500, ReaLTaiizor.Colors.MaterialPrimary.Indigo700, ReaLTaiizor.Colors.MaterialPrimary.Indigo100, ReaLTaiizor.Colors.MaterialAccent.Pink200, ReaLTaiizor.Util.MaterialTextShade.WHITE);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //将当前窗体实例复制给全局常量Form
            form1 = this;
            //这个是允许其他线程控制本窗体控件
            System.Windows.Forms.Control.CheckForIllegalCrossThreadCalls = false;
        }

        private void materialButton3_MouseHover(object sender, EventArgs e)
        {
            colorDialog1.ShowDialog();
            Color c = colorDialog1.Color;
            materialSkinManager.ColorScheme = new ReaLTaiizor.Colors.MaterialColorScheme(c, materialSkinManager.ColorScheme.DarkPrimaryColor, materialSkinManager.ColorScheme.LightPrimaryColor, materialSkinManager.ColorScheme.AccentColor, ReaLTaiizor.Util.MaterialTextShade.WHITE);
            Invalidate();
            materialSkinManager.AddFormToManage(this);
        }

        private void materialButton1_Click(object sender, EventArgs e)
        {
            materialSkinManager.Theme = materialSkinManager.Theme == ReaLTaiizor.Manager.MaterialSkinManager.Themes.DARK ? ReaLTaiizor.Manager.MaterialSkinManager.Themes.LIGHT : ReaLTaiizor.Manager.MaterialSkinManager.Themes.DARK;
            materialSkinManager.AddFormToManage(this);
        }
        private int colorSchemeIndex;

        //切换颜色按钮点击
        private void materialButton2_Click(object sender, EventArgs e)
        {
            colorSchemeIndex++;
            if (colorSchemeIndex > 2)
                colorSchemeIndex = 0;
            updateColor();
            materialSkinManager.AddFormToManage(this);
        }
        private void updateColor()
        {
            //These are just example color schemes
            switch (colorSchemeIndex)
            {
                case 0:
                    materialSkinManager.ColorScheme = new ReaLTaiizor.Colors.MaterialColorScheme(
                        ReaLTaiizor.Colors.MaterialPrimary.Indigo500,
                        ReaLTaiizor.Colors.MaterialPrimary.Indigo700,
                        ReaLTaiizor.Colors.MaterialPrimary.Indigo100,
                        ReaLTaiizor.Colors.MaterialAccent.Pink200,
                        ReaLTaiizor.Util.MaterialTextShade.WHITE);
                    break;

                case 1:
                    materialSkinManager.ColorScheme = new ReaLTaiizor.Colors.MaterialColorScheme(
                        ReaLTaiizor.Colors.MaterialPrimary.Green600,
                        ReaLTaiizor.Colors.MaterialPrimary.Green700,
                        ReaLTaiizor.Colors.MaterialPrimary.Green200,
                        ReaLTaiizor.Colors.MaterialAccent.Red100,
                        ReaLTaiizor.Util.MaterialTextShade.WHITE);
                    break;

                case 2:
                    materialSkinManager.ColorScheme = new ReaLTaiizor.Colors.MaterialColorScheme(
                        ReaLTaiizor.Colors.MaterialPrimary.BlueGrey800,
                        ReaLTaiizor.Colors.MaterialPrimary.BlueGrey900,
                        ReaLTaiizor.Colors.MaterialPrimary.BlueGrey500,
                        ReaLTaiizor.Colors.MaterialAccent.LightBlue200,
                        ReaLTaiizor.Util.MaterialTextShade.WHITE);
                    break;
            }
            Invalidate();
            materialSkinManager.AddFormToManage(this);
        }

        private void materialButton4_MouseHover(object sender, EventArgs e)
        {
            colorDialog2.ShowDialog();
            Color c = colorDialog2.Color;
            materialSkinManager.ColorScheme = new ReaLTaiizor.Colors.MaterialColorScheme(materialSkinManager.ColorScheme.PrimaryColor, c, materialSkinManager.ColorScheme.LightPrimaryColor, materialSkinManager.ColorScheme.AccentColor, ReaLTaiizor.Util.MaterialTextShade.WHITE);
            Invalidate();
            materialSkinManager.AddFormToManage(this);
        }

        private void materialButton5_MouseHover(object sender, EventArgs e)
        {
            colorDialog3.ShowDialog();
            Color c = colorDialog3.Color;
            materialSkinManager.ColorScheme = new ReaLTaiizor.Colors.MaterialColorScheme(materialSkinManager.ColorScheme.PrimaryColor, materialSkinManager.ColorScheme.DarkPrimaryColor, c, materialSkinManager.ColorScheme.AccentColor, ReaLTaiizor.Util.MaterialTextShade.WHITE);
            Invalidate();
            materialSkinManager.AddFormToManage(this);
        }

        private void materialButton6_MouseHover(object sender, EventArgs e)
        {
            colorDialog4.ShowDialog();
            Color c = colorDialog4.Color;
            materialSkinManager.ColorScheme = new ReaLTaiizor.Colors.MaterialColorScheme(materialSkinManager.ColorScheme.PrimaryColor, materialSkinManager.ColorScheme.DarkPrimaryColor, materialSkinManager.ColorScheme.LightPrimaryColor, c, ReaLTaiizor.Util.MaterialTextShade.WHITE);
            Invalidate();
            materialSkinManager.AddFormToManage(this);
        }

        //Ftp2Res第一个文字框读取
        private void textBox1_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effect = DragDropEffects.All;                                                              //重要代码：表明是所有类型的数据，比如文件路径
            else
                e.Effect = DragDropEffects.None;
        }

        //Ftp2Res第一个文字框显示
        private void textBox1_DragDrop(object sender, DragEventArgs e)
        {
            string path = ((System.Array)e.Data.GetData(DataFormats.FileDrop)).GetValue(0).ToString();       //获得路径
            if (File.Exists(path))
            {
                textBox1.Text = path;
            }
            else if (Directory.Exists(path))
            {
                MessageBox.Show("检测路径输入为文件夹,请正确输入文件路径");
            }
            else
            {
                MessageBox.Show("路径输入有误！请检查！");
            }
            //文件及文件夹检测模板///if (File.Exists(textBox1.Text))
            //文件及文件夹检测模板///{
            //文件及文件夹检测模板///    MessageBox.Show("检测路径输入为文件,请正确输入文件夹路径");
            //文件及文件夹检测模板///}
            //文件及文件夹检测模板///else if (Directory.Exists(textBox1.Text))
            //文件及文件夹检测模板///{
            //文件及文件夹检测模板///    MessageBox.Show("检测路径输入为文件夹,请正确输入文件路径");
            //文件及文件夹检测模板///}
            //文件及文件夹检测模板///else
            //文件及文件夹检测模板///{
            //文件及文件夹检测模板///    MessageBox.Show("路径输入有误！请检查！");
            //文件及文件夹检测模板///}
        }

        //Ftp2Res第一个文字框文件读取按钮
        private void materialButton7_Click(object sender, EventArgs e)
        {
            //判断是否点击的“打开”按钮
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                textBox1.Text = openFileDialog1.FileName;
            }
        }

        //BnkCvt第一个文字框读取
        private void textBox2_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effect = DragDropEffects.All;                                                              //重要代码：表明是所有类型的数据，比如文件路径
            else
                e.Effect = DragDropEffects.None;
        }

        //BnkCvt第一个文字框显示
        private void textBox2_DragDrop(object sender, DragEventArgs e)
        {
            string path = ((System.Array)e.Data.GetData(DataFormats.FileDrop)).GetValue(0).ToString();       //获得路径
            if (File.Exists(path))
            {
                MessageBox.Show("检测路径输入为文件,请正确输入文件夹路径");
            }
            else if (Directory.Exists(path))
            {
                textBox2.Text = path;
            }
            else
            {
                MessageBox.Show("路径输入有误！请检查！");
            }
        }

        //BnkCvt第一个文字框文件读取按钮
        private void materialButton8_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                textBox2.Text = folderBrowserDialog1.SelectedPath;
            }
        }

        private int RSCIndex;

        //ResSC切换模式按钮
        private void materialSwitch1_CheckStateChanged(object sender, EventArgs e)
        {
            if (RSCIndex > 1)
                RSCIndex = 0;
            switch (RSCIndex)
            {
                case 0:
                    materialTabControl2.SelectedTab = materialTabControl2.TabPages[1];
                    break;
                case 1:
                    materialTabControl2.SelectedTab = materialTabControl2.TabPages[0];
                    break;
            }
            RSCIndex++;
        }

        //ResSC第一个文字框读取
        private void textBox3_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effect = DragDropEffects.All;                                                              //重要代码：表明是所有类型的数据，比如文件路径
            else
                e.Effect = DragDropEffects.None;
        }

        //ResSC第一个文字框显示
        private void textBox3_DragDrop(object sender, DragEventArgs e)
        {
            string path = ((System.Array)e.Data.GetData(DataFormats.FileDrop)).GetValue(0).ToString();       //获得路径
            if (File.Exists(path))
            {
                textBox3.Text = path;
            }
            else if (Directory.Exists(path))
            {
                MessageBox.Show("检测路径输入为文件夹,请正确输入文件路径");
            }
            else
            {
                MessageBox.Show("路径输入有误！请检查！");
            }
        }

        //ResSC第一个文字框文件读取按钮
        private void materialButton9_Click(object sender, EventArgs e)
        {
            //判断是否点击的“打开”按钮
            if (openFileDialog2.ShowDialog() == DialogResult.OK)
            {
                textBox3.Text = openFileDialog2.FileName;
            }
        }

        //ResSC第二个文字框读取
        private void textBox4_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effect = DragDropEffects.All;                                                              //重要代码：表明是所有类型的数据，比如文件路径
            else
                e.Effect = DragDropEffects.None;
        }

        //ResSC第二个文字框显示
        private void textBox4_DragDrop(object sender, DragEventArgs e)
        {
            string path = ((System.Array)e.Data.GetData(DataFormats.FileDrop)).GetValue(0).ToString();       //获得路径
            if (File.Exists(path))
            {
             MessageBox.Show("检测路径输入为文件,请正确输入文件夹路径");
            }
            else if (Directory.Exists(path))
            {
                textBox4.Text = path;
            }
            else
            {
             MessageBox.Show("路径输入有误！请检查！");
            }
        }

        //BnkCvt第二个文字框文件读取按钮
        private void materialButton10_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog2.ShowDialog() == DialogResult.OK)
            {
                textBox4.Text = folderBrowserDialog2.SelectedPath;
            }
        }

        private int LSCIndex;

        //levelSC切换模式按钮
        private void materialSwitch2_CheckStateChanged(object sender, EventArgs e)
        {
            if (LSCIndex > 1)
                LSCIndex = 0;
            switch (LSCIndex)
            {
                case 0:
                    materialTabControl4.SelectedTab = materialTabControl4.TabPages[1];
                    break;
                case 1:
                    materialTabControl4.SelectedTab = materialTabControl4.TabPages[0];
                    break;
            }
            LSCIndex++;
        }

        //LevelSC第一个文字框读取
        private void textBox5_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effect = DragDropEffects.All;                                                              //重要代码：表明是所有类型的数据，比如文件路径
            else
                e.Effect = DragDropEffects.None;
        }

        //LevelSC第一个文字框显示
        private void textBox5_DragDrop(object sender, DragEventArgs e)
        {
            string path = ((System.Array)e.Data.GetData(DataFormats.FileDrop)).GetValue(0).ToString();       //获得路径
            if (File.Exists(path))
            {
                textBox5.Text = path;
            }
            else if (Directory.Exists(path))
            {
                MessageBox.Show("检测路径输入为文件夹,请正确输入文件路径");
            }
            else
            {
                MessageBox.Show("路径输入有误！请检查！");
            }
        }

        //LevelSC第一个文字框文件读取按钮
        private void materialButton11_Click(object sender, EventArgs e)
        {
            //判断是否点击的“打开”按钮
            if (openFileDialog3.ShowDialog() == DialogResult.OK)
            {
                textBox5.Text = openFileDialog3.FileName;
            }
        }

        //LevelSC第二个文字框读取
        private void textBox6_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effect = DragDropEffects.All;                                                              //重要代码：表明是所有类型的数据，比如文件路径
            else
                e.Effect = DragDropEffects.None;
        }

        //LevelSC第二个文字框显示
        private void textBox6_DragDrop(object sender, DragEventArgs e)
        {
            string path = ((System.Array)e.Data.GetData(DataFormats.FileDrop)).GetValue(0).ToString();       //获得路径
            if (File.Exists(path))
            {
                MessageBox.Show("检测路径输入为文件,请正确输入文件夹路径");
            }
            else if (Directory.Exists(path))
            {
                textBox6.Text = path;
            }
            else
            {
                MessageBox.Show("路径输入有误！请检查！");
            }
        }

        //LevelSC第二个文字框文件读取按钮
        private void materialButton12_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog3.ShowDialog() == DialogResult.OK)
            {
                textBox6.Text = folderBrowserDialog3.SelectedPath;
            }
        }

        //ClipCreator第一个文字框读取
        private void textBox7_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effect = DragDropEffects.All;                                                              //重要代码：表明是所有类型的数据，比如文件路径
            else
                e.Effect = DragDropEffects.None;
        }

        //ClipCreator第一个文字框显示
        private void textBox7_DragDrop(object sender, DragEventArgs e)
        {
            string path = ((System.Array)e.Data.GetData(DataFormats.FileDrop)).GetValue(0).ToString();       //获得路径
            if (File.Exists(path))
            {
                MessageBox.Show("检测路径输入为文件,请正确输入文件夹路径");
            }
            else if (Directory.Exists(path))
            {
                textBox7.Text = path;
            }
            else
            {
                MessageBox.Show("路径输入有误！请检查！");
            }
        }

        //ClipCreator第一个文字框文件读取按钮
        private void materialButton13_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog4.ShowDialog() == DialogResult.OK)
            {
                textBox7.Text = folderBrowserDialog4.SelectedPath;
            }
        }

        //ClipTransformer第一个文字框读取
        private void textBox8_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effect = DragDropEffects.All;                                                              //重要代码：表明是所有类型的数据，比如文件路径
            else
                e.Effect = DragDropEffects.None;
        }

        //ClipTransformer第一个文字框显示
        private void textBox8_DragDrop(object sender, DragEventArgs e)
        {
            string path = ((System.Array)e.Data.GetData(DataFormats.FileDrop)).GetValue(0).ToString();       //获得路径
            if (File.Exists(path))
            {
                MessageBox.Show("检测路径输入为文件,请正确输入文件夹路径");
            }
            else if (Directory.Exists(path))
            {
                textBox8.Text = path;
            }
            else
            {
                MessageBox.Show("路径输入有误！请检查！");
            }
        }

        //ClipTransformer第一个文字框文件读取按钮
        private void materialButton14_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog5.ShowDialog() == DialogResult.OK)
            {
                textBox8.Text = folderBrowserDialog5.SelectedPath;
            }
        }

        //DictionaryCreator第一个文字框读取
        private void textBox9_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effect = DragDropEffects.All;                                                              //重要代码：表明是所有类型的数据，比如文件路径
            else
                e.Effect = DragDropEffects.None;
        }

        //DictionaryCreator第一个文字框显示
        private void textBox9_DragDrop(object sender, DragEventArgs e)
        {
            string path = ((System.Array)e.Data.GetData(DataFormats.FileDrop)).GetValue(0).ToString();       //获得路径
            if (File.Exists(path))
            {
                MessageBox.Show("检测路径输入为文件,请正确输入文件夹路径");
            }
            else if (Directory.Exists(path))
            {
                textBox9.Text = path;
            }
            else
            {
                MessageBox.Show("路径输入有误！请检查！");
            }
        }

        //DictionaryCreator第一个文字框文件读取按钮
        private void materialButton15_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog6.ShowDialog() == DialogResult.OK)
            {
                textBox9.Text = folderBrowserDialog6.SelectedPath;
            }
        }

        //Res2Ext第一个文字框读取
        private void textBox10_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effect = DragDropEffects.All;                                                              //重要代码：表明是所有类型的数据，比如文件路径
            else
                e.Effect = DragDropEffects.None;
        }

        //Res2Ext第一个文字框显示
        private void textBox10_DragDrop(object sender, DragEventArgs e)
        {
            string path = ((System.Array)e.Data.GetData(DataFormats.FileDrop)).GetValue(0).ToString();       //获得路径
            if (File.Exists(path))
            {
                MessageBox.Show("检测路径输入为文件,请正确输入文件夹路径");
            }
            else if (Directory.Exists(path))
            {
                textBox10.Text = path;
            }
            else
            {
                MessageBox.Show("路径输入有误！请检查！");
            }
        }

        //Res2Ext第一个文字框文件读取按钮
        private void materialButton16_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog7.ShowDialog() == DialogResult.OK)
            {
                textBox10.Text = folderBrowserDialog7.SelectedPath;
            }
        }

        //SpriteToLibrary第一个文字框读取
        private void textBox11_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effect = DragDropEffects.All;                                                              //重要代码：表明是所有类型的数据，比如文件路径
            else
                e.Effect = DragDropEffects.None;
        }

        //SpriteToLibrary第一个文字框显示
        private void textBox11_DragDrop(object sender, DragEventArgs e)
        {
            string path = ((System.Array)e.Data.GetData(DataFormats.FileDrop)).GetValue(0).ToString();       //获得路径
            if (File.Exists(path))
            {
                MessageBox.Show("检测路径输入为文件,请正确输入文件夹路径");
            }
            else if (Directory.Exists(path))
            {
                textBox11.Text = path;
            }
            else
            {
                MessageBox.Show("路径输入有误！请检查！");
            }
        }

        //SpriteToLibrary第一个文字框文件读取按钮
        private void materialButton17_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog8.ShowDialog() == DialogResult.OK)
            {
                textBox11.Text = folderBrowserDialog8.SelectedPath;
            }
        }

        //SpriteToLibrary第二个文字框读取
        private void textBox12_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effect = DragDropEffects.All;                                                              //重要代码：表明是所有类型的数据，比如文件路径
            else
                e.Effect = DragDropEffects.None;
        }

        //SpriteToLibrary第二个文字框显示
        private void textBox12_DragDrop(object sender, DragEventArgs e)
        {
            string path = ((System.Array)e.Data.GetData(DataFormats.FileDrop)).GetValue(0).ToString();       //获得路径
            if (File.Exists(path))
            {
                MessageBox.Show("检测路径输入为文件,请正确输入文件夹路径");
            }
            else if (Directory.Exists(path))
            {
                textBox12.Text = path;
            }
            else
            {
                MessageBox.Show("路径输入有误！请检查！");
            }
        }

        //SpriteToLibrary第二个文字框文件读取按钮
        private void materialButton18_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog9.ShowDialog() == DialogResult.OK)
            {
                textBox12.Text = folderBrowserDialog9.SelectedPath;
            }
        }

        //Res2Ext第二个文字框读取
        private void textBox13_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effect = DragDropEffects.All;                                                              //重要代码：表明是所有类型的数据，比如文件路径
            else
                e.Effect = DragDropEffects.None;
        }

        //Res2Ext第二个文字框显示
        private void textBox13_DragDrop(object sender, DragEventArgs e)
        {
            string path = ((System.Array)e.Data.GetData(DataFormats.FileDrop)).GetValue(0).ToString();       //获得路径
            if (File.Exists(path)|| Directory.Exists(path))
            {
                textBox13.Text = path;
            }
            //更新而弃用///else if (Directory.Exists(path))
            //更新而弃用///{
            //更新而弃用///    MessageBox.Show("检测路径输入为文件夹,请正确输入文件路径");
            //更新而弃用///}
            else
            {
                MessageBox.Show("路径输入有误！请检查！");
            }
        }

        //Res2Ext第二个文字框文件读取按钮
        private void materialButton19_Click(object sender, EventArgs e)
        {
            //判断是否点击的“打开”按钮
            //更新而弃用///if (openFileDialog4.ShowDialog() == DialogResult.OK)
            if (folderBrowserDialog10.ShowDialog() == DialogResult.OK)
            {
                textBox13.Text = folderBrowserDialog10.SelectedPath;
            }
        }

        //Ftp2Res的运行按钮
        private void spaceButton1_Click(object sender, EventArgs e)
        {
            Task.Run(() =>
            {
                materialButton20.Enabled = false;
                Ftp2Res.Ftp2Res ftp2Res = new Ftp2Res.Ftp2Res();
                string filepath = null, SLOT = null;
                if (File.Exists(textBox1.Text))
                {
                    filepath = textBox1.Text;
                }
                else if (Directory.Exists(textBox1.Text))
                {
                    MessageBox.Show("检测路径输入为文件夹,请正确输入文件路径");
                }
                else
                {
                    MessageBox.Show("路径输入有误！请检查！");
                }
                if (Regex.IsMatch(textBox14.Text, @"^\d+$")) // 判断字符串是否为数字 的正则表达式)
                {
                    SLOT = textBox14.Text;
                }
                else
                {
                    MessageBox.Show("Slot输入不为阿拉伯数字！请检查！");
                }
                if (File.Exists(textBox1.Text) && Regex.IsMatch(textBox14.Text, @"^\d+$"))
                {
                    ftp2Res.FtpToRes(filepath, SLOT);
                }
                else { }
                materialButton20.Enabled = true;
            });
        }

        //ResSC的第一个运行按钮
        private void spaceButton2_Click(object sender, EventArgs e)
        {
            Task.Run(() =>
            {
                materialButton21.Enabled = false;
                ResSC.ResSpliter resSpliter = new ResSC.ResSpliter();
                if (File.Exists(textBox3.Text))
                {
                    resSpliter.ResSplit(textBox3.Text);
                }
                else if (Directory.Exists(textBox3.Text))
                {
                    MessageBox.Show("检测路径输入为文件夹,请正确输入文件路径");
                }
                else
                {
                    MessageBox.Show("路径输入有误！请检查！");
                }
                materialButton21.Enabled = true;
            });
        }

        //ResSC的第二个运行按钮
        private void spaceButton3_Click(object sender, EventArgs e)
        {
            Task.Run(() =>
            {
                materialButton22.Enabled = false;
                ResSC.ResCrafter resCrafter = new ResSC.ResCrafter();
                if (File.Exists(textBox4.Text))
                {
                    MessageBox.Show("检测路径输入为文件,请正确输入文件夹路径");
                }
                else if (Directory.Exists(textBox4.Text))
                {
                    resCrafter.ResCraft(textBox4.Text);
                }
                else
                {
                    MessageBox.Show("路径输入有误！请检查！");
                }
                materialButton22.Enabled = true;
            });
        }

        //BnkCvt的运行按钮
        private void materialButton23_Click(object sender, EventArgs e)
        {
            Task.Run(() =>
            {
                materialButton23.Enabled = false;
                BnkCvt.BnkConvertor bnkConvertor = new BnkCvt.BnkConvertor();
                if (File.Exists(textBox2.Text))
                {
                    MessageBox.Show("检测路径输入为文件,请正确输入文件夹路径");
                }
                else if (Directory.Exists(textBox2.Text))
                {
                    bnkConvertor.BnkCvt(textBox2.Text);
                }
                else
                {
                    MessageBox.Show("路径输入有误！请检查！");
                }
                materialButton23.Enabled = true;
            });
        }

        //SpriteToLibrary的运行按钮
        private void materialButton24_Click(object sender, EventArgs e)
        {
            Task.Run(() =>
            {
                materialButton24.Enabled = false;
                //创建xr实例
                SpriteToLibrary.XflReader xr = new SpriteToLibrary.XflReader();
                string dictionarypath1 = null, dictionarypath2 = null;
                if (File.Exists(textBox11.Text))
                {
                    MessageBox.Show("检测XFL总文件夹路径输入为文件,请正确输入文件夹路径");
                }
                else if (Directory.Exists(textBox11.Text))
                {
                    dictionarypath1 = textBox11.Text;
                }
                else
                {
                    MessageBox.Show("XFL总文件夹路径输入有误！请检查！");
                }
                if (File.Exists(textBox12.Text))
                {
                    MessageBox.Show("检测位图总文件夹路径输入为文件,请正确输入文件夹路径");
                }
                else if (Directory.Exists(textBox12.Text) || textBox12.Text == null || textBox12.Text == "")
                {
                    dictionarypath2 = textBox12.Text;
                }
                else
                {
                    MessageBox.Show("位图总文件夹路径输入有误！请检查！");
                }
                if (Directory.Exists(textBox11.Text) && (Directory.Exists(textBox12.Text) || textBox12.Text == null || textBox12.Text == ""))
                {
                    if (textBox12.Text == null || textBox12.Text == "")
                    {
                        dictionarypath2 = dictionarypath1;
                    }
                    else { }
                    //寻找总文件夹中所有Anim.xfl文件
                    xr.XflRead(dictionarypath1, dictionarypath2);
                    MessageBox.Show("SpriteToLibrary Done");
                }
                else { }
                materialButton24.Enabled = true;
            });
        }

        //ClipCreator的运行按钮
        private void materialButton25_Click(object sender, EventArgs e)
        {
            Task.Run(() =>
            {
                materialButton25.Enabled = false;
                //创建cc实例
                ClipCreator.ClipCreator cc = new ClipCreator.ClipCreator();
                //创建sr实例
                ClipCreator.SpriteReplacer sr = new ClipCreator.SpriteReplacer();
                //创建ddo实例
                ClipCreator.DOMDocumentOverwriter ddo = new ClipCreator.DOMDocumentOverwriter();
                if (File.Exists(textBox7.Text))
                {
                    MessageBox.Show("检测路径输入为文件,请正确输入文件夹路径");
                }
                else if (Directory.Exists(textBox7.Text))
                {
                    textBox15.AppendText("必要预检测执行中......\r\n");
                    cc.rs.ReplaceScan(textBox7.Text + "\\LIBRARY");
                    textBox15.AppendText("必要预检测执行完成\r\n");
                    if (checkedListBox1.GetItemChecked(5))
                    {
                        cc.ClipCreate(textBox7.Text);
                        ddo.DOMDocumentOverwrite(textBox7.Text + "\\DOMDocument.xml", textBox7.Text + "\\LIBRARY");
                    }
                    else
                    {
                        if (checkedListBox1.GetItemChecked(0))
                        {
                            cc.icr.ImageClipRead(textBox7.Text + "\\LIBRARY");
                            //对引用被删除i元件的a元件进行删除20240307添加
                            cc.acr.AnimateClipRead(textBox7.Text + "\\LIBRARY", cc.icr.delirecord);
                            cc.icc.ImageClipCreate(textBox7.Text + "\\LIBRARY", cc.icr.irecord, cc.rs.rsrecord);
                        }
                        if (checkedListBox1.GetItemChecked(2))
                        {
                            sr.SpriteReplace(textBox7.Text + "\\LIBRARY");
                        }
                        if (checkedListBox1.GetItemChecked(3))
                        {
                            cc.mlls.MultiLoadedLayerSplite(textBox7.Text + "\\LIBRARY", cc.rs.mllsrecord);
                        }
                        if (checkedListBox1.GetItemChecked(1))
                        {
                            cc.acr.AnimateClipRead(textBox7.Text + "\\LIBRARY");
                            cc.acc.AnimateClipCreate(textBox7.Text + "\\LIBRARY", cc.acr.arecord, cc.acr.inum, cc.acr.airecord);
                        }
                        if (checkedListBox1.GetItemChecked(4))
                        {
                            ddo.DOMDocumentOverwrite(textBox7.Text + "\\DOMDocument.xml", textBox7.Text + "\\LIBRARY");
                        }
                        else { }
                    }
                    MessageBox.Show("ClipCreator Done");
                }
                else
                {
                    MessageBox.Show("路径输入有误！请检查！");
                }
                materialButton25.Enabled = true;
            });
        }

        //ClipTransformer的运行按钮
        private void materialButton26_Click(object sender, EventArgs e)
        {
            Task.Run(() =>
            {
                materialButton26.Enabled = false;
                //创建ct实例
                ClipTransformer.ClipTransformer ct = new ClipTransformer.ClipTransformer();
                if (File.Exists(textBox8.Text))
                {
                    MessageBox.Show("检测路径输入为文件,请正确输入文件夹路径");
                }
                else if (Directory.Exists(textBox8.Text))
                {
                    ct.ClipTransform(textBox8.Text);
                    MessageBox.Show("ClipTransformer Done");
                }
                else
                {
                    MessageBox.Show("路径输入有误！请检查！");
                }
                materialButton26.Enabled = true;
            });
        }

        //Res2Ext的运行按钮
        private void materialButton27_Click(object sender, EventArgs e)
        {
            Task.Run(() =>
            {
                materialButton27.Enabled = false;
                //创建mdf实例
                Res2Ext.MediaDataFormater mdf = new Res2Ext.MediaDataFormater();
                //创建ddo实例
                Res2Ext.DOMDocumentOverwriter ddo = new Res2Ext.DOMDocumentOverwriter();
                //创建mco实例
                Res2Ext.MainClipOverwriter mco = new Res2Ext.MainClipOverwriter();
                //创建oco实例
                Res2Ext.OtherClipOverwriter oco = new Res2Ext.OtherClipOverwriter();
                //创建ce实例
                Res2Ext.ClipEncryptor ce = new Res2Ext.ClipEncryptor();
                //创建ecc实例20240317添加
                Res2Ext.EncryptClipCreator ecc = new EncryptClipCreator();
                string filepath = null, dictionarypath = null;
                if (File.Exists(textBox10.Text))
                {
                    MessageBox.Show("检测XFL文件夹路径输入为文件,请正确输入文件夹路径");
                }
                else if (Directory.Exists(textBox10.Text))
                {
                    dictionarypath = textBox10.Text;
                }
                else
                {
                    MessageBox.Show("XFL文件夹路径输入有误！请检查！");
                }
                if (File.Exists(textBox13.Text)|| Directory.Exists(textBox13.Text))
                {
                    filepath = textBox13.Text;
                }
                //新功能更新而废弃///else if (Directory.Exists(textBox13.Text))
                //新功能更新而废弃///{
                //新功能更新而废弃///    MessageBox.Show("检测资源片段文件路径输入为文件夹,请正确输入文件路径");
                //新功能更新而废弃///}
                else
                {
                    MessageBox.Show("资源片段文件（夹）路径输入有误！请检查！");
                }
                if (Directory.Exists(textBox10.Text) && (File.Exists(textBox13.Text)|| Directory.Exists(textBox13.Text)))
                {
                    //加密元件创制20240317添加
                    ecc.EncryptClipCreate(dictionarypath + "\\LIBRARY", dictionarypath + "\\DOMDocument.xml");
                    mdf.MediaDataFormat(filepath, dictionarypath + "\\LIBRARY");
                    ddo.DOMDocumentOverwrite(dictionarypath + "\\DOMDocument.xml");
                    mco.MainClipOverwrite(dictionarypath + "\\LIBRARY\\main.xml");
                    oco.OtherClipOverwrite(dictionarypath + "\\LIBRARY");
                    //加密元件插入20240319添加
                    ce.SpecialClipEncrypt(filepath, dictionarypath + "\\LIBRARY", ecc.ic.icname);
                    ce.SpecialClipEncrypt(filepath, dictionarypath + "\\LIBRARY", ecc.ac.acname);
                    ce.ClipEncrypt(filepath, dictionarypath + "\\LIBRARY", dictionarypath + "\\DOMDocument.xml");
                    MessageBox.Show("Res2Ext Done");
                }
                else { }
                materialButton27.Enabled = true;
            });
        }

        //LevelSC的第一个运行按钮
        private void materialButton28_Click(object sender, EventArgs e)
        {
            Task.Run(() =>
            {
                materialButton28.Enabled = false;
                LevelSC.LevelSpliter levelSpliter = new LevelSC.LevelSpliter();
                if (File.Exists(textBox5.Text))
                {
                    levelSpliter.LevelSplit(textBox5.Text);
                }
                else if (Directory.Exists(textBox5.Text))
                {
                    MessageBox.Show("检测路径输入为文件夹,请正确输入文件路径");
                }
                else
                {
                    MessageBox.Show("路径输入有误！请检查！");
                }
                materialButton28.Enabled = true;
            });
        }

        //LevelSC的第二个运行按钮
        private void materialButton29_Click(object sender, EventArgs e)
        {
            Task.Run(() =>
            {
                materialButton29.Enabled = false;
                LevelSC.LevelCrafter levelCrafter = new LevelSC.LevelCrafter();
                if (File.Exists(textBox6.Text))
                {
                    MessageBox.Show("检测路径输入为文件,请正确输入文件夹路径");
                }
                else if (Directory.Exists(textBox6.Text))
                {
                    levelCrafter.LevelCraft(textBox6.Text);
                }
                else
                {
                    MessageBox.Show("路径输入有误！请检查！");
                }
                materialButton29.Enabled = true;
            });
        }

        //DirectoryCreator的运行按钮
        private void materialButton30_Click(object sender, EventArgs e)
        {
            Task.Run(() =>
            {
                materialButton30.Enabled = false;
                //创建dc实例
                DirectoryCreator.DirectoryCreator dc = new DirectoryCreator.DirectoryCreator();
                if (File.Exists(textBox9.Text))
                {
                    MessageBox.Show("检测路径输入为文件,请正确输入文件夹路径");
                }
                else if (Directory.Exists(textBox9.Text))
                {
                    //功能已修复///MessageBox.Show("功能损坏，待后续版本修复");
                    dc.DirectoryCreate(textBox9.Text);
                    MessageBox.Show("DirectoryCreator Done");
                }
                else
                {
                    MessageBox.Show("路径输入有误！请检查！");
                }
                materialButton30.Enabled = true;
            });
        }

        //ClipCreator多选框
        private void checkedListBox1_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            //废弃代码，用了报错///if (checkedListBox1.GetItemChecked(0) || checkedListBox1.GetItemChecked(1) || checkedListBox1.GetItemChecked(2) || checkedListBox1.GetItemChecked(3) || checkedListBox1.GetItemChecked(4)) { }
            //废弃代码，用了报错///if (checkedListBox1.GetItemChecked(5)) { }
            //废弃代码，用了报错///else { }
            if (e.CurrentValue == CheckState.Checked) return;//取消选中就不用进行以下操作
            int index = checkedListBox1.SelectedIndex;//获取此时鼠标点击了哪个项
            if (index == 0 || index == 1 || index == 2 || index == 3 || index == 4)
            {
                ((CheckedListBox)sender).SetItemChecked(5, false);
            }
            if (index == 5)
            {
                ((CheckedListBox)sender).SetItemChecked(0, false);
                ((CheckedListBox)sender).SetItemChecked(1, false);
                ((CheckedListBox)sender).SetItemChecked(2, false);
                ((CheckedListBox)sender).SetItemChecked(3, false);
                ((CheckedListBox)sender).SetItemChecked(4, false);
            }
            else { }
            e.NewValue = CheckState.Checked;//刷新
        }

        //ClipCreator多选框默认引用重写
        private void checkedListBox1_Layout(object sender, LayoutEventArgs e)
        {
            ((CheckedListBox)sender).SetItemChecked(4, true);
        }

        //Res2Ext加密元件载入选项选中检测
        private void radioButton31_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton31.Checked && !radioButton32.Checked)
            {
                textBox20.Enabled = true;
            }
            else if (!radioButton31.Checked && radioButton32.Checked)
            {
                textBox20.Enabled = false;
            }
            else
            {
                textBox20.Enabled = false;
            }
        }

        private void radioButton32_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton31.Checked && !radioButton32.Checked)
            {
                textBox20.Enabled = true;
            }
            else if (!radioButton31.Checked && radioButton32.Checked)
            {
                textBox20.Enabled = false;
            }
            else
            {
                textBox20.Enabled = false;
            }
        }
    }
}
