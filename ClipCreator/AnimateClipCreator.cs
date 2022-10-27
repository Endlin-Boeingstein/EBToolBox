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
    //建立a元件创制类
    class AnimateClipCreator
    {
        //创建acra实例
        public AnimateClipReader acra = new AnimateClipReader();
        //定义arecord
        ArrayList arecord = new ArrayList();
        //定义inum
        ArrayList inum = new ArrayList();
        //定义airecord
        ArrayList airecord = new ArrayList();
        //生成a元件创制部分
        public void AnimateClipCreate(string Fpath, ArrayList arecord, ArrayList inum, ArrayList airecord)
        {
            try
            {
                form1.textBox15.AppendText("进行a元件创制操作......" + "\r\n");
                //为arecord赋值
                this.arecord = arecord;
                //为inum赋值
                this.inum = inum;
                //为airecord赋值
                this.airecord = airecord;
                //预定义新建元件名称
                string acname = "a";
                //录入i元件名称
                string iid = "i";
                //判定i元件是否被a元件引用
                foreach (string item in this.inum)
                {
                    //重置acname
                    acname = "a";
                    //重置iid
                    iid = "i";
                    //新功能更新而停用//刷新空白记录数组
                    //新功能更新而停用///this.acra.AnimateClipRead(Fpath);
                    //新功能更新而停用//空白数组刷新
                    //新功能更新而停用///this.arecord = this.acra.arecord;
                    //新功能更新而停用//i元件序号数组刷新
                    //新功能更新而停用///this.inum = this.acra.inum;
                    //新功能更新而停用//i元件引用序号数组刷新
                    //新功能更新而停用///this.airecord = this.acra.airecord;
                    if (!this.airecord.Contains(item))
                    {
                        //获得未被引用的i元件名称
                        iid += item;
                        if (this.arecord.Contains(null))
                        {
                            acname += this.arecord.IndexOf(null).ToString() + ".xml";
                            //信息录入数组
                            this.arecord[this.arecord.IndexOf(null)] = iid;
                            this.airecord.Add(item);
                        }
                        else
                        {
                            acname += this.arecord.Count.ToString() + ".xml";
                            //信息录入数组
                            this.arecord.Add(iid);
                            this.airecord.Add(item);
                        }
                        //得到预置的和exe同文件夹的samplea.xml的路径
                        FileInfo fi = new FileInfo(System.Environment.CurrentDirectory + "\\ClipCreator\\samplea.xml");
                        //判定根目录读取函数是否生效，否则换第二条函数
                        if (!fi.Exists)
                        {
                            //得到预置的和exe同文件夹的samplea.xml的路径
                            fi = new FileInfo(AppDomain.CurrentDomain.BaseDirectory + "ClipCreator\\samplea.xml");
                        }
                        //判断文件是否缺失
                        if (fi.Exists)
                        {
                            //添加预置的元件
                            fi.CopyTo(Fpath + "\\" + acname);
                            //创建xml读取对象
                            XmlDocument xmlDoc = new XmlDocument();
                            //读取xml
                            xmlDoc.Load(Fpath + "\\" + acname);
                            //读取DOMSymbolItem节点
                            XmlElement element = (XmlElement)xmlDoc.GetElementsByTagName("DOMSymbolItem")[0];
                            //设定aname
                            string aname = null;
                            //读取DOMSymbolItem节点name属性字符串
                            aname = element.GetAttribute("name");
                            if (aname == null || aname == "0" || aname == "" || aname != acname.Substring(0, acname.Length - 4))
                            {
                                //录入name
                                element.SetAttribute("name", acname.Substring(0, acname.Length - 4));
                                //保存xml
                                xmlDoc.Save(Fpath + "\\" + acname);
                            }
                            else { }
                            //读取DOMTimeline节点
                            XmlElement frameelement = (XmlElement)xmlDoc.GetElementsByTagName("DOMTimeline")[0];
                            if (frameelement.GetAttribute("name") == null || frameelement.GetAttribute("name") == "0" || frameelement.GetAttribute("name") == "" || frameelement.GetAttribute("name") != acname.Substring(0, acname.Length - 4))
                            {
                                //录入name
                                frameelement.SetAttribute("name", acname.Substring(0, acname.Length - 4));
                                //保存xml
                                xmlDoc.Save(Fpath + "\\" + acname);
                            }
                            else { }
                            //读取DOMSymbolInstance节点
                            XmlElement dsielement = (XmlElement)xmlDoc.GetElementsByTagName("DOMSymbolInstance")[0];
                            if (dsielement.GetAttribute("libraryItemName") == null || dsielement.GetAttribute("libraryItemName") == "0" || dsielement.GetAttribute("libraryItemName") == "" || dsielement.GetAttribute("libraryItemName") != iid)
                            {
                                //录入name
                                dsielement.SetAttribute("libraryItemName", iid);
                                //保存xml
                                xmlDoc.Save(Fpath + "\\" + acname);
                            }
                            else { }
                            //提示创制完成
                            form1.textBox15.AppendText("已为元件" + iid + "创制元件" + acname.Substring(0, acname.Length - 4) + "\r\n");
                        }
                        else MessageBox.Show("软件原有的samplea.xml缺失！");
                    }
                    else { }
                }
                form1.textBox15.AppendText("a元件创制完成" + "\r\n");
            }
            catch
            {
                MessageBox.Show("AnimateClipCreate ERROR");
            }
        }
    }
}