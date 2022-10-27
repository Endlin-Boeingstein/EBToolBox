using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;

namespace SpriteToLibrary
{
    //建立DOMDocument读取类
    class DOMDocumentReader
    {
        //预置位图引用数组
        public ArrayList sarray = new ArrayList();
        //生成DOMDocument读取部分
        public void DOMDocumentRead(string DPath)
        {
            try
            {
                //创建路径文件夹实例
                DirectoryInfo TheFolder = new DirectoryInfo(DPath);
                //创建xml读取对象
                XmlDocument xmlDoc = new XmlDocument();
                //读取xml
                xmlDoc.Load(TheFolder.FullName);
                //读取media节点
                XmlElement media = (XmlElement)xmlDoc.GetElementsByTagName("media")[0];
                //遍历以获取数组
                foreach (XmlElement DOMBitmapItem in media)
                {
                    string href = DOMBitmapItem.GetAttribute("name") + ".png";
                    if (!sarray.Contains(href))
                    {
                        //记录引用信息
                        sarray.Add(href);
                    }
                    else { }
                }
            }
            catch
            {
                MessageBox.Show("DOMDocumentRead ERROR");
            }
        }
    }
}