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
    //建立DOMDocument重写类
    class DOMDocumentOverwriter
    {
        //创建dsr实例
        public SpriteReader dsr = new SpriteReader();
        //创建dicr实例
        public ImageClipReader dicr = new ImageClipReader();
        //创建dacr实例
        public AnimateClipReader dacr = new AnimateClipReader();
        //生成DOMDocument重写部分
        public void DOMDocumentOverwrite(string DPath, string Fpath)
        {
            try
            {
                //读取位图列表建立数组
                dsr.SpriteRead(Fpath);
                //读取i元件列表建立数组
                dicr.ImageClipRead(Fpath);
                //读取a元件列表建立数组
                dacr.AnimateClipRead(Fpath);
                //创建路径文件夹实例
                DirectoryInfo TheFolder = new DirectoryInfo(DPath);
                //创建xml读取对象
                XmlDocument xmlDoc = new XmlDocument();
                //读取xml
                xmlDoc.Load(TheFolder.FullName);
                //读取media节点
                XmlElement media = (XmlElement)xmlDoc.GetElementsByTagName("media")[0];
                //读取symbols节点
                XmlElement symbols = (XmlElement)xmlDoc.GetElementsByTagName("symbols")[0];
                //移除media所有子节点
                media.RemoveAll();
                //移除symbols所有子节点
                symbols.RemoveAll();
                //遍历位图数组录入信息
                foreach (string item in dsr.srecord)
                {
                    //预置DOMBitmapItem节点
                    XmlElement dbi = xmlDoc.CreateElement("DOMBitmapItem", xmlDoc.DocumentElement.NamespaceURI);
                    //录入名称
                    dbi.SetAttribute("name", item);
                    //录入位图全名
                    dbi.SetAttribute("href", item + ".png");
                    //将dbi作为media的子节点
                    media.AppendChild(dbi);
                }
                //保存xml
                xmlDoc.Save(DPath);
                //遍历i元件数组录入信息
                for (int item = 0; item < dicr.irecord.Count; item++)
                {
                    if (dicr.irecord[item] != null)
                    {
                        //预置Include节点
                        XmlElement incl = xmlDoc.CreateElement("Include", xmlDoc.DocumentElement.NamespaceURI);
                        //录入i元件全名
                        incl.SetAttribute("href", "i" + item.ToString() + ".xml");
                        //将incl作为symbols的子节点
                        symbols.AppendChild(incl);
                        //重置避免重复录入
                        dicr.irecord[item] = null;
                    }
                    else { }
                }
                //保存xml
                xmlDoc.Save(DPath);
                //遍历a元件数组录入信息
                for (int item = 0; item < dacr.arecord.Count; item++)
                {
                    if (dacr.arecord[item] != null)
                    {
                        //预置Include节点
                        XmlElement incl = xmlDoc.CreateElement("Include", xmlDoc.DocumentElement.NamespaceURI);
                        //录入i元件全名
                        incl.SetAttribute("href", "a" + item.ToString() + ".xml");
                        //将incl作为symbols的子节点
                        symbols.AppendChild(incl);
                        //重置避免重复录入
                        dacr.arecord[item] = null;
                    }
                    else { }
                }
                //保存xml
                xmlDoc.Save(DPath);
                //预置Include节点
                XmlElement inclu = xmlDoc.CreateElement("Include", xmlDoc.DocumentElement.NamespaceURI);
                //录入i元件全名
                inclu.SetAttribute("href", "main.xml");
                //将inclu作为symbols的子节点
                symbols.AppendChild(inclu);
                //保存xml
                xmlDoc.Save(DPath);
                form1.textBox15.AppendText("DOMDocument引用重写完成" + "\r\n");
            }
            catch
            {
                MessageBox.Show("DOMDocumentOverwrite ERROR");
            }
        }
    }
}