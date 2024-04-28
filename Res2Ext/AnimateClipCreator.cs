using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;
using static EBToolBox.Form1;

namespace Res2Ext
{
    //建立a元件创制类
    class AnimateClipCreator
    {
        //创建acra实例
        public AnimateClipReader acra = new AnimateClipReader();
        public string acname;
        //定义arecord
        ArrayList arecord = new ArrayList();

        //生成DOMDocument添加信息部分
        public void DOMDocumentClipAdd(string Dpath, string acname)
        {
            try
            {
                form1.textBox16.AppendText("进行加密动画元件记录添加操作......" + "\r\n");
                //创建路径文件夹实例
                DirectoryInfo TheFolder = new DirectoryInfo(Dpath);
                //创建xml读取对象
                XmlDocument xmlDoc = new XmlDocument();
                //读取xml
                xmlDoc.Load(TheFolder.FullName);
                //读取symbols节点
                XmlElement symbols = (XmlElement)xmlDoc.GetElementsByTagName("symbols")[0];
                //记录是否有撞衫记录
                ArrayList aarr = new ArrayList();
                foreach (XmlElement inclu in symbols.ChildNodes)
                {
                    if (inclu.GetAttribute("href") == acname)
                    {
                        aarr.Add(inclu);
                    }
                    else { }
                }
                if (aarr.Count > 0) { }
                else
                {
                    //预置Include节点
                    XmlElement incl = xmlDoc.CreateElement("Include", xmlDoc.DocumentElement.NamespaceURI);
                    //录入i元件全名
                    incl.SetAttribute("href", acname);
                    //将incl作为symbols的子节点
                    symbols.AppendChild(incl);
                    //保存xml
                    xmlDoc.Save(Dpath);
                }
                form1.textBox16.AppendText("加密动画元件记录添加完成" + "\r\n");
            }
            catch
            {
                MessageBox.Show("DOMDocumentClipAdd ERROR");
            }
        }

        //生成a元件创制部分
        public void AnimateClipCreate(string Fpath, string iid, string Dpath)
        {
            try
            {
                form1.textBox16.AppendText("进行加密动画元件创制操作......" + "\r\n");
                //iid修正
                iid = iid.Substring(0, iid.Length - 4);
                //得到预置的和exe同文件夹的samplea.xml的路径
                FileInfo fi = new FileInfo(System.Environment.CurrentDirectory + "\\Res2Ext\\samplea.xml");
                //判定根目录读取函数是否生效，否则换第二条函数
                if (!fi.Exists)
                {
                    //得到预置的和exe同文件夹的samplea.xml的路径
                    fi = new FileInfo(AppDomain.CurrentDomain.BaseDirectory + "Res2Ext\\samplea.xml");
                }
                //判断文件是否缺失
                if (fi.Exists)
                {
                    //运行元件检测
                    acra.AnimateClipRead(Fpath);
                    //设置新建元件名称
                    acname = "a" + acra.arecord.Count + ".xml";
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
                    form1.textBox16.AppendText("已为元件" + iid + "创制元件" + acname.Substring(0, acname.Length - 4) + "\r\n");
                    //加密元件记录添加DOMDocument
                    DOMDocumentClipAdd(Dpath, acname);
                }
                else MessageBox.Show("软件原有的samplea.xml缺失！");
                form1.textBox16.AppendText("加密动画元件创制完成" + "\r\n");
            }
            catch
            {
                MessageBox.Show("AnimateClipCreate ERROR");
            }
        }
    }
}