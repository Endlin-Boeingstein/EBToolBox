using EBToolBox;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;
using static EBToolBox.Form1;

namespace Res2Ext
{
    //建立labels添加类
    class DOMDocumentLabelAdder
    {
        //生成labels添加部分
        public void DOMDocumentLabelAdd(string Dpath)
        {
            try
            {
                //创建路径文件夹实例
                DirectoryInfo TheFolder = new DirectoryInfo(Dpath);
                //创建xml读取对象
                XmlDocument xmlDoc = new XmlDocument();
                //读取xml
                xmlDoc.Load(TheFolder.FullName);
                //读取layers节点
                XmlElement layers = (XmlElement)xmlDoc.GetElementsByTagName("DOMLayer")[0];
                //读取frames节点
                XmlElement frames = (XmlElement)layers.GetElementsByTagName("frames")[0];
                //记录要添加的labels
                ArrayList lbcount = new ArrayList();
                //设置max
                long max = 0;
                //搜寻每个index
                foreach (XmlElement DOMFrame in frames.ChildNodes)
                {
                    //设置index和duration
                    string index = null, duration = null;
                    //获得index属性字符串
                    index = DOMFrame.GetAttribute("index");
                    //获得duration属性字符串
                    duration = DOMFrame.GetAttribute("duration");
                    if (duration == "0" || duration == null || duration == "")
                    {
                        duration = "1";
                    }
                    else { }
                    //获取容量
                    if (long.Parse(index) + long.Parse(duration) > max)
                    {
                        max = long.Parse(index) + long.Parse(duration);
                    }
                    else { }
                    lbcount.Add(index);
                }
                for (int i = 0; i < max; i++)
                {
                    if (lbcount.Contains(i.ToString())) { }
                    else
                    {
                        //预置DOMFrame节点
                        XmlElement DOMFrameAdd = xmlDoc.CreateElement("DOMFrame", xmlDoc.DocumentElement.NamespaceURI);
                        //录入index
                        DOMFrameAdd.SetAttribute("index", i.ToString());
                        //录入duration
                        DOMFrameAdd.SetAttribute("duration", "0");
                        //录入name
                        DOMFrameAdd.SetAttribute("name", "encrypt_label_" + i);
                        //录入加密标志
                        DOMFrameAdd.SetAttribute("encrypted", "true");
                        //将DOMFrameAdd作为frames的子节点
                        frames.AppendChild(DOMFrameAdd);
                        //保存xml
                        xmlDoc.Save(Dpath);
                    }
                }
                if (form1.radioButton33.Checked)
                {
                    form1.textBox16.AppendText("DOMDocument的labels加密完成" + "\r\n");
                }
                else { }
            }
            catch
            {
                MessageBox.Show("DOMDocumentLabelAdd ERROR");
            }
        }
    }
}