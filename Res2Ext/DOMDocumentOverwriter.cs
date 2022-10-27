using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
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
    //建立DOMDocument重写类
    class DOMDocumentOverwriter
    {
        //生成DOMDocument重写部分
        public void DOMDocumentOverwrite(string DPath)
        {
            try
            {
                //创建路径文件夹实例
                DirectoryInfo TheFolder = new DirectoryInfo(DPath);
                //获取DOMDocument节点__ABOUT__属性
                //创建xml读取对象
                XmlDocument xmlDoc = new XmlDocument();
                //读取xml
                xmlDoc.Load(TheFolder.FullName);
                //读取DOMDocument节点
                XmlElement element = (XmlElement)xmlDoc.GetElementsByTagName("DOMDocument")[0];
                //设定nag
                string nag = null;
                //读取DOMDocument节点__ABOUT__属性字符串
                nag = element.GetAttribute("__ABOUT__");
                if (nag == null || nag == "0" || nag == "")
                {
                    //恢复xml中太极标签
                    element.SetAttribute("__ABOUT__", "this XFL is convert from Popcap-AniMation file , by TaiJi .");
                    //保存xml
                    xmlDoc.Save(TheFolder.FullName);
                }
                else { }
            }
            catch
            {
                MessageBox.Show("DOMDocumentOverwrite ERROR");
            }
        }
    }
}