﻿using Newtonsoft.Json;
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
    //建立其余元件修复重写类
    class OtherClipOverwriter
    {
        //生成其余元件修复重写部分
        public void OtherClipOverwrite(string Fpath)
        {
            try
            {
                form1.textBox16.AppendText("开始对其余元件进行删除全空和全空帧图层以及删除图层末尾空帧......" + "\r\n");
                //创建路径文件夹实例
                DirectoryInfo TheFolder = new DirectoryInfo(Fpath);
                //创建文件数组
                FileInfo[] files = TheFolder.GetFiles();
                //为文件数组排序
                Array.Sort(files, new FileNameSort());
                //遍历文件夹内文件
                foreach (FileInfo NextFile in files)
                {
                    //流式读取文件类型
                    FileStream stream = new FileStream(NextFile.FullName, FileMode.Open, FileAccess.Read);
                    BinaryReader reader = new BinaryReader(stream);
                    string fileclass = "";
                    try
                    {
                        for (int i = 0; i < 2; i++)
                        {
                            fileclass += reader.ReadByte().ToString();
                        }
                    }
                    catch (Exception)
                    {
                        throw;
                    }
                    stream.Close();
                    //判定是否为xml
                    if (fileclass == "6068")
                    {
                        //创建xml读取对象
                        XmlDocument xmlDoc = new XmlDocument();
                        //读取xml
                        xmlDoc.Load(NextFile.FullName);
                        //获取根节点root
                        XmlNode root = xmlDoc.DocumentElement;
                        //获取节点layers
                        XmlNode layers = root.FirstChild.FirstChild.FirstChild;
                        //获取layers图层列表
                        XmlNodeList layersnodeList = layers.ChildNodes;
                        //预置全空或全空帧图层删除列表
                        List<XmlNode> LayersToRemove = new List<XmlNode>();
                        //预置删除图层末尾空帧列表
                        List<XmlNode> LayersToOverwrite = new List<XmlNode>();
                        //预置空帧列表
                        List<XmlNode> FramesToRemove = new List<XmlNode>();

                        //判断为SPCUtil解析的元件类型并提示
                        if (NextFile.Name.Substring(0, 1) == "M" || NextFile.Name.Substring(0, 1) == "A" || NextFile.Name.Substring(0, NextFile.Name.Length - 4) == "A_Main")
                        {
                            MessageBox.Show("抱歉，不支持用SPCUtil解析PAM得到的元件");
                        }
                        //判断为TwinKles-ToolKit解析的元件类型并提示
                        if (NextFile.Name.Substring(0, 2) == "sp" || NextFile.Name.Substring(0, 2) == "an" || NextFile.Name.Substring(0, NextFile.Name.Length - 4) == "main_animation")
                        {
                            MessageBox.Show("抱歉，不支持用TwinKles-ToolKit解析PAM得到的元件");
                        }
                        //判定是i元件还是a元件
                        if (NextFile.Name.Substring(0, 1) == "i")
                        {
                            //删除全空或全空帧图层
                            foreach (XmlNode node in layersnodeList)
                            {
                                //转换DOMLayer为XmlElement以便于识别是否存在实帧
                                XmlElement DOMLayer = (XmlElement)node;
                                //判断是否存在DOMBitmapInstance，以判定是否为全空或全空帧，默认为0(结果为0则为全空或全空帧)
                                int dsiexist = 0;
                                foreach (XmlElement element in DOMLayer.ChildNodes)
                                {
                                    if (element.GetElementsByTagName("DOMBitmapInstance").Count != 0 || element.GetElementsByTagName("DOMSymbolInstance").Count != 0)
                                    {
                                        dsiexist = 1;
                                    }
                                    else { }
                                }
                                //积累待删除记录列表
                                if (dsiexist == 0)
                                {
                                    LayersToRemove.Add(node);
                                    //node XmlElement化
                                    XmlElement enode = (XmlElement)node;
                                    //获取被删图层名称
                                    string nenode = enode.GetAttribute("name");
                                    form1.textBox16.AppendText("删除" + NextFile.Name.Substring(0, NextFile.Name.Length - 4) + "元件全空或全空帧图层" + nenode + "\r\n");
                                    continue;
                                }
                                else { }
                            }
                            //根据删除列表删除全空或全空帧图层
                            foreach (XmlNode node in LayersToRemove)
                            {
                                node.ParentNode.RemoveChild(node);
                                //保存xml
                                xmlDoc.Save(NextFile.FullName);
                            }


                            //重载xml
                            xmlDoc.Load(NextFile.FullName);
                            //获取根节点root
                            root = xmlDoc.DocumentElement;
                            //获取节点layers
                            layers = root.FirstChild.FirstChild.FirstChild;
                            //获取layers图层列表
                            layersnodeList = layers.ChildNodes;
                            //删除末尾空帧
                            foreach (XmlNode node in layersnodeList)
                            {
                                //转换DOMLayer为XmlElement以便于识别是否存在实帧
                                XmlElement DOMLayer = (XmlElement)node;
                                //判断是否存在DOMBitmapInstance，以判定是否为末尾空帧，默认为0(最终结果为0则存在末尾空帧)
                                int dsitrue = 0;
                                foreach (XmlElement element in DOMLayer.FirstChild.ChildNodes)
                                {
                                    if (element.GetElementsByTagName("DOMBitmapInstance").Count != 0 || element.GetElementsByTagName("DOMSymbolInstance").Count != 0)
                                    {
                                        dsitrue = 1;
                                    }
                                    else
                                    {
                                        dsitrue = 0;
                                    }
                                }
                                //积累待重写记录列表
                                if (dsitrue == 0)
                                {
                                    LayersToOverwrite.Add(node);
                                    //node XmlElement化
                                    XmlElement enode = (XmlElement)node;
                                    //获取被删末尾空帧的图层名称
                                    string nenode = enode.GetAttribute("name");
                                    form1.textBox16.AppendText("删除" + NextFile.Name.Substring(0, NextFile.Name.Length - 4) + "元件图层" + nenode + "的末尾空帧" + "\r\n");
                                    continue;
                                }
                                else { }
                            }
                            //根据删除列表重写末尾空帧图层
                            foreach (XmlNode node in LayersToOverwrite)
                            {
                                //判断是否存在DOMSymbolInstance，以判定是否为末尾空帧，默认为0(最终结果为0则存在末尾空帧)
                                int dsitrue = 0;
                                //倒序检测空帧
                                for (int i = node.FirstChild.ChildNodes.Count - 1; dsitrue == 0; i--)
                                {
                                    //取需要删除的空帧
                                    XmlNode element = node.FirstChild.ChildNodes[i];
                                    //转换DOMFrame为XmlElement以便于识别是否存在实帧
                                    XmlElement DOMFrame = (XmlElement)element;
                                    if (DOMFrame.GetElementsByTagName("DOMBitmapInstance").Count != 0 || DOMFrame.GetElementsByTagName("DOMSymbolInstance").Count != 0)
                                    {
                                        dsitrue = 1;
                                    }
                                    else
                                    {
                                        FramesToRemove.Add(element);
                                        continue;
                                    }
                                }
                            }
                            //根据删除列表删除空帧
                            foreach (XmlNode element in FramesToRemove)
                            {
                                element.ParentNode.RemoveChild(element);
                                //保存xml
                                xmlDoc.Save(NextFile.FullName);
                            }
                        }
                        //判断为a元件并修复
                        if (NextFile.Name.Substring(0, 1) == "a")
                        {
                            //删除全空或全空帧图层
                            foreach (XmlNode node in layersnodeList)
                            {
                                //转换DOMLayer为XmlElement以便于识别是否存在实帧
                                XmlElement DOMLayer = (XmlElement)node;
                                //判断是否存在DOMSymbolInstance，以判定是否为全空或全空帧，默认为0(结果为0则为全空或全空帧)
                                int dsiexist = 0;
                                foreach (XmlElement element in DOMLayer.ChildNodes)
                                {
                                    if (element.GetElementsByTagName("DOMBitmapInstance").Count != 0 || element.GetElementsByTagName("DOMSymbolInstance").Count != 0)
                                    {
                                        dsiexist = 1;
                                    }
                                    else { }
                                }
                                //积累待删除记录列表
                                if (dsiexist == 0)
                                {
                                    LayersToRemove.Add(node);
                                    //node XmlElement化
                                    XmlElement enode = (XmlElement)node;
                                    //获取被删图层名称
                                    string nenode = enode.GetAttribute("name");
                                    form1.textBox16.AppendText("删除" + NextFile.Name.Substring(0, NextFile.Name.Length - 4) + "元件全空或全空帧图层" + nenode + "\r\n");
                                    continue;
                                }
                                else { }
                            }
                            //根据删除列表删除全空或全空帧图层
                            foreach (XmlNode node in LayersToRemove)
                            {
                                node.ParentNode.RemoveChild(node);
                                //保存xml
                                xmlDoc.Save(NextFile.FullName);
                            }


                            //重载xml
                            xmlDoc.Load(NextFile.FullName);
                            //获取根节点root
                            root = xmlDoc.DocumentElement;
                            //获取节点layers
                            layers = root.FirstChild.FirstChild.FirstChild;
                            //获取layers图层列表
                            layersnodeList = layers.ChildNodes;
                            //删除末尾空帧
                            foreach (XmlNode node in layersnodeList)
                            {
                                //转换DOMLayer为XmlElement以便于识别是否存在实帧
                                XmlElement DOMLayer = (XmlElement)node;
                                //判断是否存在DOMSymbolInstance，以判定是否为末尾空帧，默认为0(最终结果为0则存在末尾空帧)
                                int dsitrue = 0;
                                foreach (XmlElement element in DOMLayer.FirstChild.ChildNodes)
                                {
                                    if (element.GetElementsByTagName("DOMBitmapInstance").Count != 0 || element.GetElementsByTagName("DOMSymbolInstance").Count != 0)
                                    {
                                        dsitrue = 1;
                                    }
                                    else
                                    {
                                        dsitrue = 0;
                                    }
                                }
                                //积累待重写记录列表
                                if (dsitrue == 0)
                                {
                                    LayersToOverwrite.Add(node);
                                    //node XmlElement化
                                    XmlElement enode = (XmlElement)node;
                                    //获取被删末尾空帧的图层名称
                                    string nenode = enode.GetAttribute("name");
                                    form1.textBox16.AppendText("删除" + NextFile.Name.Substring(0, NextFile.Name.Length - 4) + "元件图层" + nenode + "的末尾空帧" + "\r\n");
                                    continue;
                                }
                                else { }
                            }
                            //根据删除列表重写末尾空帧图层
                            foreach (XmlNode node in LayersToOverwrite)
                            {
                                //判断是否存在DOMSymbolInstance，以判定是否为末尾空帧，默认为0(最终结果为0则存在末尾空帧)
                                int dsitrue = 0;
                                //倒序检测空帧
                                for (int i = node.FirstChild.ChildNodes.Count - 1; dsitrue == 0; i--)
                                {
                                    //取需要删除的空帧
                                    XmlNode element = node.FirstChild.ChildNodes[i];
                                    //转换DOMFrame为XmlElement以便于识别是否存在实帧
                                    XmlElement DOMFrame = (XmlElement)element;
                                    if (DOMFrame.GetElementsByTagName("DOMBitmapInstance").Count != 0 || DOMFrame.GetElementsByTagName("DOMSymbolInstance").Count != 0)
                                    {
                                        dsitrue = 1;
                                    }
                                    else
                                    {
                                        FramesToRemove.Add(element);
                                        continue;
                                    }
                                }
                            }
                            //根据删除列表删除空帧
                            foreach (XmlNode element in FramesToRemove)
                            {
                                element.ParentNode.RemoveChild(element);
                                //保存xml
                                xmlDoc.Save(NextFile.FullName);
                            }
                        }
                        else { }
                    }
                    else { }
                }
                form1.textBox16.AppendText("图层修复检测完成" + "\r\n");
            }
            catch
            {
                MessageBox.Show("OtherClipOverwrite ERROR");
            }
        }
    }
}