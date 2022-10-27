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
    //建立多位图/元件图层分解类
    class MultiLoadedLayerSpliter
    {
        //预定义mllsrecord以记录引用位图的a元件信息
        public ArrayList mllsrecord = new ArrayList();
        //预定义Fpath
        public string Fpath = null;
        //生成多位图/元件图层分解部分
        public void MultiLoadedLayerSplite(string Fpath, ArrayList mllsrecord)
        {
            try
            {
                form1.textBox15.AppendText("进行多位图/元件图层分解操作......" + "\r\n");
                this.Fpath = Fpath;
                //为mllsrecord赋值
                this.mllsrecord = mllsrecord;
                //创建路径文件夹实例
                DirectoryInfo TheFolder = new DirectoryInfo(Fpath);
                //创建文件数组
                FileInfo[] files = TheFolder.GetFiles();
                //为文件数组排序
                Array.Sort(files, new FileNameSort());
                //遍历文件夹内文件
                foreach (FileInfo NextFile in files)
                {
                    //判定是否存在引用多位图/元件的a元件
                    if (this.mllsrecord.Contains(NextFile.Name.Substring(0, NextFile.Name.Length - 4)))
                    {
                        //信息录入数组
                        this.mllsrecord[this.mllsrecord.IndexOf(NextFile.Name.Substring(0, NextFile.Name.Length - 4))] = null;
                        //创建xml读取对象
                        XmlDocument xmlDoc = new XmlDocument();
                        //读取xml
                        xmlDoc.Load(NextFile.FullName);

                        //载位图图层分解
                        if (xmlDoc.GetElementsByTagName("DOMBitmapInstance").Count != 0)
                        {
                            //获取根节点root
                            XmlNode root = xmlDoc.DocumentElement;
                            //获取节点layers
                            XmlNode layers = root.FirstChild.FirstChild.FirstChild;
                            //获取layers图层列表
                            XmlNodeList layersnodeList = layers.ChildNodes;
                            //检测引用位图的图层和帧数
                            for (int a = 0; a < layersnodeList.Count; a++)
                            {
                                //读取每一个图层
                                XmlNode node = layersnodeList[a];
                                //记录多图层的数组
                                ArrayList dbia = new ArrayList();
                                //记录是否多位图
                                int som = 1;
                                //转换DOMLayer为XmlElement以便于识别是否存在位图引用
                                XmlElement DOMLayer = (XmlElement)node;
                                //判断是否存在DOMBitmapInstance，以判定是否引用位图
                                foreach (XmlElement DOMFrame in DOMLayer.FirstChild.ChildNodes)
                                {
                                    if (DOMFrame.GetElementsByTagName("DOMBitmapInstance").Count != 0)
                                    {
                                        //新功能更新而停用///if (DOMFrame.GetElementsByTagName("DOMBitmapInstance").Count == 1) { }
                                        //新功能更新而停用///else
                                        //新功能更新而停用///{
                                        if (DOMFrame.GetElementsByTagName("DOMBitmapInstance").Count == 1) { }
                                        else
                                        {
                                            som++;
                                        }
                                        for (int t = 0; t < DOMFrame.GetElementsByTagName("DOMBitmapInstance").Count; t++)
                                        {
                                            if ((!dbia.Contains(((XmlElement)DOMFrame.GetElementsByTagName("DOMBitmapInstance")[t]).GetAttribute("libraryItemName"))) && (((XmlElement)DOMFrame.GetElementsByTagName("DOMBitmapInstance")[t]).GetAttribute("libraryItemName") != null) && (((XmlElement)DOMFrame.GetElementsByTagName("DOMBitmapInstance")[t]).GetAttribute("libraryItemName") != ""))
                                            {
                                                dbia.Add(((XmlElement)DOMFrame.GetElementsByTagName("DOMBitmapInstance")[t]).GetAttribute("libraryItemName"));
                                            }
                                            else { }
                                        }
                                        //新功能更新而停用///}
                                    }
                                    else { }
                                }

                                if (dbia.Count != 1 || (dbia.Count == 1 && som > 1))
                                {
                                    //将每帧的第n位图提出来放在新图层里
                                    for (int t = 0; t < dbia.Count; t++)
                                    {
                                        //备份b值以判断是否重置t
                                        int b = 0;
                                        //预置addDOMLayer节点，新建图层
                                        XmlElement addDOMLayer = xmlDoc.CreateElement("DOMLayer", xmlDoc.DocumentElement.NamespaceURI);
                                        //预置addframes节点
                                        XmlElement addframes = xmlDoc.CreateElement("frames", xmlDoc.DocumentElement.NamespaceURI);
                                        //设name为DOMLayer的name值
                                        addDOMLayer.SetAttribute("name", DOMLayer.GetAttribute("name") + "_" + t);
                                        //将addframes作为addDOMLayer的子节点
                                        addDOMLayer.AppendChild(addframes);
                                        //判断是否存在DOMBitmapInstance，以判定是否引用位图
                                        foreach (XmlElement DOMFrame in DOMLayer.FirstChild.ChildNodes)
                                        {
                                            //预置addDOMFrame节点
                                            XmlElement addDOMFrame = xmlDoc.CreateElement("DOMFrame", xmlDoc.DocumentElement.NamespaceURI);
                                            //预置addelements节点
                                            XmlElement addelements = xmlDoc.CreateElement("elements", xmlDoc.DocumentElement.NamespaceURI);
                                            //设index为DOMFrame的index值
                                            addDOMFrame.SetAttribute("index", DOMFrame.GetAttribute("index"));
                                            //设duration为DOMFrame的duration值
                                            string fduration = DOMFrame.GetAttribute("duration");
                                            if (fduration == "" || fduration == "0" || fduration == null)
                                            {
                                                fduration = "1";
                                            }
                                            else { }
                                            addDOMFrame.SetAttribute("duration", fduration);
                                            //将addelements作为addDOMFrame的子节点
                                            addDOMFrame.AppendChild(addelements);
                                            if (DOMFrame.GetElementsByTagName("DOMBitmapInstance").Count != 0)
                                            {
                                                //新功能更新而停用///if (DOMFrame.GetElementsByTagName("DOMBitmapInstance").Count == 1) { }
                                                //新功能更新而停用///else
                                                //新功能更新而停用///{
                                                for (int j = 0, n = 0; j < DOMFrame.GetElementsByTagName("DOMBitmapInstance").Count; j++)
                                                {
                                                    if (((XmlElement)DOMFrame.GetElementsByTagName("DOMBitmapInstance")[j]).GetAttribute("libraryItemName") == dbia[t].ToString())
                                                    {
                                                        n++;
                                                        if (n > 1)
                                                        {
                                                            form1.textBox15.AppendText(NextFile.Name.Substring(0, NextFile.Name.Length - 4) + "元件由于图层" + DOMLayer.GetAttribute("name") + "第" + DOMFrame.GetAttribute("index") + "帧引用了多个相同位图，长度" + fduration + "帧，将会引发错误" + "\r\n");
                                                            b++;
                                                            break;
                                                        }
                                                        else { }
                                                        //将当前位图作为addelements的子节点
                                                        addelements.AppendChild(DOMFrame.GetElementsByTagName("DOMBitmapInstance")[j]);
                                                    }
                                                    else { }
                                                }
                                                //新功能更新而停用///}
                                            }
                                            else { }
                                            //将addDOMFrame作为addframes的子节点
                                            addframes.AppendChild(addDOMFrame);
                                        }
                                        //新功能更新而停用///if (t != dbia.Count - 1)
                                        //新功能更新而停用///{
                                        //将addDOMLayer插在DOMLayer的前面
                                        root.FirstChild.FirstChild.FirstChild.InsertBefore(addDOMLayer, node);
                                        //新功能更新而停用///}
                                        //新功能更新而停用///else { }
                                        //保存xml
                                        xmlDoc.Save(NextFile.FullName);
                                        form1.textBox15.AppendText("已提取" + NextFile.Name.Substring(0, NextFile.Name.Length - 4) + "元件的" + DOMLayer.GetAttribute("name") + "图层的位图" + dbia[t].ToString() + "并新建图层" + "\r\n");
                                        //新功能更新而停用//刷新数组
                                        //新功能更新而停用///dbia[t] = null;
                                        //判断是否重置t
                                        if (b != 0)
                                        {
                                            t--;
                                        }
                                        else { }
                                    }
                                }
                                else { }

                                //确定创建完毕并删除旧图层
                                if (dbia.Count != 0 && (dbia.Count != 1 || (dbia.Count == 1 && som > 1)))
                                {
                                    //移除旧图层
                                    root.FirstChild.FirstChild.FirstChild.RemoveChild(DOMLayer);
                                    //重置计数器
                                    a = 0;
                                }
                                else { }
                                //保存xml
                                xmlDoc.Save(NextFile.FullName);
                            }
                            //保存xml
                            xmlDoc.Save(NextFile.FullName);
                        }
                        else { }

                        //载元件图层分解
                        if (xmlDoc.GetElementsByTagName("DOMSymbolInstance").Count != 0)
                        {
                            //获取根节点root
                            XmlNode root = xmlDoc.DocumentElement;
                            //获取节点layers
                            XmlNode layers = root.FirstChild.FirstChild.FirstChild;
                            //获取layers图层列表
                            XmlNodeList layersnodeList = layers.ChildNodes;
                            //检测引用元件的图层和帧数
                            for (int a = 0; a < layersnodeList.Count; a++)
                            {
                                //读取每一个图层
                                XmlNode node = layersnodeList[a];
                                //记录多图层的数组
                                ArrayList dsia = new ArrayList();
                                //记录是否多元件
                                int som = 1;
                                //转换DOMLayer为XmlElement以便于识别是否存在元件引用
                                XmlElement DOMLayer = (XmlElement)node;
                                string ss = DOMLayer.GetAttribute("name");
                                //判断是否存在DOMSymbolInstance，以判定是否引用元件
                                foreach (XmlElement DOMFrame in DOMLayer.FirstChild.ChildNodes)
                                {
                                    if (DOMFrame.GetElementsByTagName("DOMSymbolInstance").Count != 0)
                                    {
                                        //新功能更新而停用///if (DOMFrame.GetElementsByTagName("DOMSymbolInstance").Count == 1) { }
                                        //新功能更新而停用///else
                                        //新功能更新而停用///{
                                        if (DOMFrame.GetElementsByTagName("DOMSymbolInstance").Count == 1) { }
                                        else
                                        {
                                            som++;
                                        }
                                        for (int t = 0; t < DOMFrame.GetElementsByTagName("DOMSymbolInstance").Count; t++)
                                        {
                                            if ((!dsia.Contains(((XmlElement)DOMFrame.GetElementsByTagName("DOMSymbolInstance")[t]).GetAttribute("libraryItemName"))) && (((XmlElement)DOMFrame.GetElementsByTagName("DOMSymbolInstance")[t]).GetAttribute("libraryItemName") != null) && (((XmlElement)DOMFrame.GetElementsByTagName("DOMSymbolInstance")[t]).GetAttribute("libraryItemName") != ""))
                                            {
                                                dsia.Add(((XmlElement)DOMFrame.GetElementsByTagName("DOMSymbolInstance")[t]).GetAttribute("libraryItemName"));
                                            }
                                            else { }
                                        }
                                        //新功能更新而停用///}
                                    }
                                    else { }
                                }

                                if (dsia.Count != 1 || (dsia.Count == 1 && som > 1))
                                {
                                    //将每帧的第n元件提出来放在新图层里
                                    for (int t = 0; t < dsia.Count; t++)
                                    {
                                        //备份b值以判断是否重置t
                                        int b = 0;
                                        //预置addDOMLayer节点，新建图层
                                        XmlElement addDOMLayer = xmlDoc.CreateElement("DOMLayer", xmlDoc.DocumentElement.NamespaceURI);
                                        //预置addframes节点
                                        XmlElement addframes = xmlDoc.CreateElement("frames", xmlDoc.DocumentElement.NamespaceURI);
                                        //设name为DOMLayer的name值
                                        addDOMLayer.SetAttribute("name", DOMLayer.GetAttribute("name") + "_" + t);
                                        //将addframes作为addDOMLayer的子节点
                                        addDOMLayer.AppendChild(addframes);
                                        //判断是否存在DOMSymbolInstance，以判定是否引用元件
                                        foreach (XmlElement DOMFrame in DOMLayer.FirstChild.ChildNodes)
                                        {
                                            //预置addDOMFrame节点
                                            XmlElement addDOMFrame = xmlDoc.CreateElement("DOMFrame", xmlDoc.DocumentElement.NamespaceURI);
                                            //预置addelements节点
                                            XmlElement addelements = xmlDoc.CreateElement("elements", xmlDoc.DocumentElement.NamespaceURI);
                                            //设index为DOMFrame的index值
                                            addDOMFrame.SetAttribute("index", DOMFrame.GetAttribute("index"));
                                            //设duration为DOMFrame的duration值
                                            string fduration = DOMFrame.GetAttribute("duration");
                                            if (fduration == "" || fduration == "0" || fduration == null)
                                            {
                                                fduration = "1";
                                            }
                                            else { }
                                            addDOMFrame.SetAttribute("duration", fduration);
                                            //将addelements作为addDOMFrame的子节点
                                            addDOMFrame.AppendChild(addelements);
                                            if (DOMFrame.GetElementsByTagName("DOMSymbolInstance").Count != 0)
                                            {
                                                //新功能更新而停用///if (DOMFrame.GetElementsByTagName("DOMSymbolInstance").Count == 1) { }
                                                //新功能更新而停用///else
                                                //新功能更新而停用///{
                                                for (int j = 0, n = 0; j < DOMFrame.GetElementsByTagName("DOMSymbolInstance").Count; j++)
                                                {
                                                    if (((XmlElement)DOMFrame.GetElementsByTagName("DOMSymbolInstance")[j]).GetAttribute("libraryItemName") == dsia[t].ToString())
                                                    {
                                                        n++;
                                                        if (n > 1)
                                                        {
                                                            form1.textBox15.AppendText(NextFile.Name.Substring(0, NextFile.Name.Length - 4) + "元件由于图层" + DOMLayer.GetAttribute("name") + "第" + DOMFrame.GetAttribute("index") + "帧引用了多个相同元件，长度" + fduration + "帧，将会引发错误" + "\r\n");
                                                            b++;
                                                            break;
                                                        }
                                                        else { }
                                                        //将当前元件作为addelements的子节点
                                                        addelements.AppendChild(DOMFrame.GetElementsByTagName("DOMSymbolInstance")[j]);
                                                    }
                                                    else { }
                                                }
                                                //新功能更新而停用///}
                                            }
                                            else { }
                                            //将addDOMFrame作为addframes的子节点
                                            addframes.AppendChild(addDOMFrame);
                                        }
                                        //新功能更新而停用///if (t != dsia.Count - 1)
                                        //新功能更新而停用///{
                                        //将addDOMLayer插在DOMLayer的前面
                                        root.FirstChild.FirstChild.FirstChild.InsertBefore(addDOMLayer, node);
                                        //新功能更新而停用///}
                                        //新功能更新而停用///else { }
                                        //保存xml
                                        xmlDoc.Save(NextFile.FullName);
                                        form1.textBox15.AppendText("已提取" + NextFile.Name.Substring(0, NextFile.Name.Length - 4) + "元件的" + DOMLayer.GetAttribute("name") + "图层的元件" + dsia[t].ToString() + "并新建图层" + "\r\n");
                                        //新功能更新而停用//刷新数组
                                        //新功能更新而停用///dsia[t] = null;
                                        //判断是否重置t
                                        if (b != 0)
                                        {
                                            t--;
                                        }
                                        else { }
                                    }
                                }
                                else { }

                                //确定创建完毕并删除旧图层
                                if (dsia.Count != 0 && (dsia.Count != 1 || (dsia.Count == 1 && som > 1)))
                                {
                                    //移除旧图层
                                    root.FirstChild.FirstChild.FirstChild.RemoveChild(DOMLayer);
                                    //重置计数器
                                    a = 0;
                                }
                                else { }
                                //保存xml
                                xmlDoc.Save(NextFile.FullName);
                            }
                            //保存xml
                            xmlDoc.Save(NextFile.FullName);
                        }
                        else { }
                        //保存xml
                        xmlDoc.Save(NextFile.FullName);
                    }
                    else { }
                }
                form1.textBox15.AppendText("多位图/元件图层分解完成" + "\r\n");
            }
            catch
            {
                MessageBox.Show("MultiLoadedLayerSplite ERROR");
            }
        }
    }
}