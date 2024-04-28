using EBToolBox;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;
using static EBToolBox.Form1;

namespace Res2Ext
{
    //建立加密元件创制类
    class ImageClipCreator
    {
        //创建icri实例
        public ImageClipReader icri = new ImageClipReader();
        public string icname;
        //生成DOMDocument添加信息部分
        public void DOMDocumentClipAdd(string Dpath, string icname)
        {
            try
            {
                form1.textBox16.AppendText("进行加密位图元件记录添加操作......" + "\r\n");
                //创建路径文件夹实例
                DirectoryInfo TheFolder = new DirectoryInfo(Dpath);
                //创建xml读取对象
                XmlDocument xmlDoc = new XmlDocument();
                //读取xml
                xmlDoc.Load(TheFolder.FullName);
                //读取symbols节点
                XmlElement symbols = (XmlElement)xmlDoc.GetElementsByTagName("symbols")[0];
                //记录是否有撞衫记录
                ArrayList iarr = new ArrayList();
                foreach (XmlElement inclu in symbols.ChildNodes)
                {
                    if (inclu.GetAttribute("href") == icname)
                    {
                        iarr.Add(inclu);
                    }
                    else { }
                }
                if (iarr.Count > 0) { }
                else
                {
                    //预置Include节点
                    XmlElement incl = xmlDoc.CreateElement("Include", xmlDoc.DocumentElement.NamespaceURI);
                    //录入i元件全名
                    incl.SetAttribute("href", icname);
                    //将incl作为symbols的子节点
                    symbols.AppendChild(incl);
                    //保存xml
                    xmlDoc.Save(Dpath);
                }
                form1.textBox16.AppendText("加密位图元件记录添加完成" + "\r\n");
            }
            catch
            {
                MessageBox.Show("DOMDocumentClipAdd ERROR");
            }
        }
        //生成加密元件创制部分
        public void ImageClipCreate(string Fpath, string pid, string Dpath)
        {
            try
            {
                form1.textBox16.AppendText("进行加密位图元件创制操作......" + "\r\n");
                //得到预置的和exe同文件夹的samplei.xml的路径
                FileInfo fi = new FileInfo(System.Environment.CurrentDirectory + "\\Res2Ext\\encrypt_clip.xml");
                //判定根目录读取函数是否生效，否则换第二条函数
                if (!fi.Exists)
                {
                    //得到预置的和exe同文件夹的samplei.xml的路径
                    fi = new FileInfo(AppDomain.CurrentDomain.BaseDirectory + "Res2Ext\\encrypt_clip.xml");
                }
                //判断文件是否缺失
                if (fi.Exists)
                {
                    //运行元件检测
                    icri.ImageClipRead(Fpath);
                    //设置新建元件名称
                    icname = "i" + icri.irecord.Count + ".xml";
                    //添加预置的元件
                    fi.CopyTo(Fpath + "\\" + icname);
                    //创建xml读取对象
                    XmlDocument xmlDoc = new XmlDocument();
                    //读取xml
                    xmlDoc.Load(Fpath + "\\" + icname);
                    //读取DOMSymbolItem节点
                    XmlElement element = (XmlElement)xmlDoc.GetElementsByTagName("DOMSymbolItem")[0];
                    //设定iname
                    string iname = null;
                    //读取DOMSymbolItem节点name属性字符串
                    iname = element.GetAttribute("name");
                    if (iname == null || iname == "0" || iname == "" || iname != icname.Substring(0, icname.Length - 4))
                    {
                        //录入name
                        element.SetAttribute("name", icname.Substring(0, icname.Length - 4));
                        //保存xml
                        xmlDoc.Save(Fpath + "\\" + icname);
                    }
                    else { }
                    //读取DOMTimeline节点
                    XmlElement frameelement = (XmlElement)xmlDoc.GetElementsByTagName("DOMTimeline")[0];
                    if (frameelement.GetAttribute("name") == null || frameelement.GetAttribute("name") == "0" || frameelement.GetAttribute("name") == "" || frameelement.GetAttribute("name") != icname.Substring(0, icname.Length - 4))
                    {
                        //录入name
                        frameelement.SetAttribute("name", icname.Substring(0, icname.Length - 4));
                        //保存xml
                        xmlDoc.Save(Fpath + "\\" + icname);
                    }
                    else { }
                    //读取DOMBitmapInstance节点
                    XmlElement dbielement = (XmlElement)xmlDoc.GetElementsByTagName("DOMBitmapInstance")[0];
                    if (dbielement.GetAttribute("libraryItemName") == null || dbielement.GetAttribute("libraryItemName") == "0" || dbielement.GetAttribute("libraryItemName") == "" || dbielement.GetAttribute("libraryItemName") != pid)
                    {
                        //录入name
                        dbielement.SetAttribute("libraryItemName", pid);
                        //保存xml
                        xmlDoc.Save(Fpath + "\\" + icname);
                    }
                    else { }
                    //提示创制完成
                    form1.textBox16.AppendText("已为位图" + pid + "创制元件" + icname.Substring(0, icname.Length - 4) + "\r\n");
                    //加密元件记录添加DOMDocument
                    DOMDocumentClipAdd(Dpath, icname);
                }
                else MessageBox.Show("软件原有的encrypt_clip.xml缺失！");
                form1.textBox16.AppendText("加密位图元件创制完成" + "\r\n");
            }
            catch
            {
                MessageBox.Show("ImageClipCreate ERROR");
            }
        }
    }
}