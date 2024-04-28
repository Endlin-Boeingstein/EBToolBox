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
    //建立元件解密类
    class ClipDecryptor
    {
        //创建mdfl实例
        public MediaDataFormater mdfl = new MediaDataFormater();
        //创建mcol实例
        public MainClipOverwriter mcol = new MainClipOverwriter();
        //记录被删元件名称20240319添加
        ArrayList delirec = new ArrayList();
        //删除加密labels20240323添加
        public void DOMDocumentLabelsDelete(string Dpath)
        {
            try
            {
                form1.textBox16.AppendText("进行DOMDocument的labels解密操作......" + "\r\n");
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
                //记录要删除的labels
                ArrayList dellabels = new ArrayList();
                //搜寻每个index
                foreach (XmlElement DOMFrame in frames.ChildNodes)
                {
                    //记录是否加密
                    string ecpt = null;
                    ecpt = DOMFrame.GetAttribute("encrypted");
                    if (ecpt == "true" || DOMFrame.ToString().Contains("encrypt_label_"))
                    {
                        dellabels.Add(DOMFrame);
                    }
                    else { }
                }
                //删除记录的加密labels
                foreach (XmlElement DOMFrame in dellabels)
                {
                    frames.RemoveChild(DOMFrame);
                }
                //保存xml
                xmlDoc.Save(Dpath);
                form1.textBox16.AppendText("DOMDocument的labels解密完成" + "\r\n");
            }
            catch
            {
                MessageBox.Show("DOMDocumentLabelsDelete ERROR");
            }
        }


        //删除加密元件记录20240319添加
        public void DOMDocumentClipDelete(string Dpath)
        {
            try
            {
                form1.textBox16.AppendText("进行加密元件记录删除操作......" + "\r\n");
                //创建路径文件夹实例
                DirectoryInfo TheFolder = new DirectoryInfo(Dpath);
                //创建xml读取对象
                XmlDocument xmlDoc = new XmlDocument();
                //读取xml
                xmlDoc.Load(TheFolder.FullName);
                //读取symbols节点
                XmlElement symbols = (XmlElement)xmlDoc.GetElementsByTagName("symbols")[0];
                //记录要删除的Include
                ArrayList deli = new ArrayList();
                foreach (XmlElement incl in symbols.ChildNodes)
                {
                    if (delirec.Contains(incl.GetAttribute("href")))
                    {
                        deli.Add(incl);
                    }
                    else { }
                }
                foreach (XmlElement incl in deli)
                {
                    symbols.RemoveChild(incl);
                }
                //保存xml
                xmlDoc.Save(Dpath);
                form1.textBox16.AppendText("加密元件记录删除完成" + "\r\n");
            }
            catch
            {
                MessageBox.Show("DOMDocumentClipDelete ERROR");
            }
        }
        //删除加密元件20240319添加
        public void EncryptClipDelete(string Fpath)
        {
            try
            {
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
                        //预置加密图层删除列表
                        List<XmlNode> LayersToRemove = new List<XmlNode>();
                        //收录加密图层
                        foreach (XmlNode node in layersnodeList)
                        {
                            //转换DOMLayer为XmlElement以便于识别是否存在加密层
                            XmlElement DOMLayer = (XmlElement)node;
                            if (DOMLayer.GetAttribute("name") == "encrypt_clip_layer")
                            {
                                form1.textBox16.AppendText("已删除加密元件" + NextFile.Name.Substring(0, NextFile.Name.Length - 4) + "\r\n");
                                delirec.Add(NextFile.Name);
                                //删除加密元件
                                File.Delete(Fpath + "\\" + NextFile.Name);
                                break;
                            }
                            else { }
                        }
                    }
                    else { }
                }
            }
            catch
            {
                MessageBox.Show("EncryptClipDelete ERROR");
            }
        }
        //生成元件解密部分
        public void ClipDecrypt(string Jpath, string Fpath, string Dpath)
        {
            try
            {
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
                        //预置加密图层删除列表
                        List<XmlNode> LayersToRemove = new List<XmlNode>();
                        //收录加密图层
                        foreach (XmlNode node in layersnodeList)
                        {
                            //转换DOMLayer为XmlElement以便于识别是否存在加密层
                            XmlElement DOMLayer = (XmlElement)node;
                            if (DOMLayer.GetAttribute("name") == "encrypt_layer")
                            {
                                LayersToRemove.Add(node);
                                continue;
                            }
                            else { }
                        }
                        //根据删除列表删除加密图层
                        foreach (XmlNode node in LayersToRemove)
                        {
                            node.ParentNode.RemoveChild(node);
                            //保存xml
                            xmlDoc.Save(NextFile.FullName);
                        }
                    }
                    else { }
                }
                //删除加密元件20240319添加
                EncryptClipDelete(Fpath);
                //删除加密元件记录20240319添加
                DOMDocumentClipDelete(Dpath);
                //解密labels20240323添加
                DOMDocumentLabelsDelete(Dpath);
                //解密后重新生成extra.json
                MessageBox.Show("解密完成，需重新生成extra.json");
                mdfl.MediaDataFormat(Jpath, Fpath);
                mcol.MainClipOverwrite(Fpath + "\\main.xml");
            }
            catch
            {
                MessageBox.Show("ClipDecrypt ERROR");
            }
        }
    }
}