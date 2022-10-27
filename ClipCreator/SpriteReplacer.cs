using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;
using static EBToolBox.Form1;

namespace ClipCreator
{
    //建立位图替换类
    class SpriteReplacer
    {
        //创建srrs实例
        public ReplaceScanner srrs = new ReplaceScanner();
        //创建sricr实例
        public ImageClipReader sricr = new ImageClipReader();
        //预定义rsrecord以记录被其余元件引用的位图信息
        public ArrayList rsrecord = new ArrayList();
        //预定义rsarecord以记录引用位图的a元件信息
        public ArrayList rsarecord = new ArrayList();
        //预定义rsirecord以记录引用位图的i元件信息
        public ArrayList rsirecord = new ArrayList();
        //新功能更新而停用//预定义mr为是否需要main元件位图替换标志
        //新功能更新而停用///public int mr = 0;
        //预定义Fpath
        public string Fpath = null;
        //生成位图替换部分
        public void SpriteReplace(string Fpath)
        {
            try
            {
                form1.textBox15.AppendText("进行位图替换操作......" + "\r\n");
                this.Fpath = Fpath;
                //i元件位图引用检测
                sricr.ImageClipRead(this.Fpath);
                this.rsirecord = sricr.irecord;
                //其余元件位图引用检测
                srrs.ReplaceScan(this.Fpath);
                //为rsrecord赋值
                this.rsrecord = srrs.rsrecord;
                //为rsarecord赋值
                this.rsarecord = srrs.rsarecord;
                //新功能更新而停用//为mr赋值
                //新功能更新而停用///this.mr = srrs.mr;
                //创建路径文件夹实例
                DirectoryInfo TheFolder = new DirectoryInfo(Fpath);
                //创建文件数组
                FileInfo[] files = TheFolder.GetFiles();
                //为文件数组排序
                Array.Sort(files, new FileNameSort());
                //遍历文件夹内文件
                foreach (FileInfo NextFile in files)
                {
                    //判定是否存在引用位图的a元件
                    if (rsarecord.Contains(NextFile.Name.Substring(0, NextFile.Name.Length - 4)))
                    {
                        //读取xml
                        string xml = File.ReadAllText(NextFile.FullName);
                        //读取引用位图的元件数组以对元件进行修改
                        for (int i = 0; i < this.rsrecord.Count; i++)
                        {
                            if (this.rsirecord.Contains((string)this.rsrecord[i]))
                            {
                                //替换DOMBitmapInstance为DOMSymbolInstance
                                xml = xml.Replace("DOMBitmapInstance", "DOMSymbolInstance");
                                //替换libraryItemName
                                xml = xml.Replace((string)this.rsrecord[i], "i" + this.rsirecord.IndexOf((string)this.rsrecord[i]).ToString());
                                form1.textBox15.AppendText("元件" + "i" + this.rsirecord.IndexOf((string)this.rsrecord[i]).ToString() + "已替换" + "元件" + NextFile.Name.Substring(0, NextFile.Name.Length - 4) + "的位图" + (string)this.rsrecord[i] + "\r\n");
                            }
                            else
                            {
                                form1.textBox15.AppendText("元件" + NextFile.Name.Substring(0, NextFile.Name.Length - 4) + "的位图" + (string)this.rsrecord[i] + "未被i元件引用，如后续操作未进行i元件创制，则将会引发错误" + "\r\n");
                            }

                        }
                        //信息录入数组
                        this.rsarecord[this.rsarecord.IndexOf(NextFile.Name.Substring(0, NextFile.Name.Length - 4))] = null;
                        //输出文本
                        File.WriteAllText(NextFile.FullName, xml);
                    }
                    else { }
                }
                form1.textBox15.AppendText("位图替换完成" + "\r\n");
            }
            catch
            {
                MessageBox.Show("SpriteReplace ERROR");
            }
        }
    }
}