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
        //生成元件解密部分
        public void ClipDecrypt(string Jpath, string Fpath)
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