using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;

namespace ClipTransformer
{
    //建立DOMDocument元件读取类
    class DOMDocumentReader
    {
        //预置ca数组以记录被引用的元件名称
        public ArrayList ca = new ArrayList();
        //生成DOMDocument元件读取部分
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
                //读取symbols节点
                XmlElement symbols = (XmlElement)xmlDoc.GetElementsByTagName("symbols")[0];
                //读取元件引用
                foreach (XmlElement Include in symbols)
                {
                    ca.Add(Include.GetAttribute("href").Substring(0, Include.GetAttribute("href").Length - 4));
                }
            }
            catch
            {
                MessageBox.Show("DOMDocumentRead ERROR");
            }
        }
    }
}