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
    //建立a元件读取类
    class AnimateClipReader
    {
        //创建aicr实例
        public ImageClipReader aicr = new ImageClipReader();
        //创建i元件序号记录数组
        public ArrayList inum = new ArrayList();
        //创建a元件序号记录数组
        public ArrayList acrnum = new ArrayList();
        //定义a元件记录数组
        public ArrayList arecord = new ArrayList();
        //定义被引用i元件序号记录数组
        public ArrayList airecord = new ArrayList();
        //生成a元件预读得到最大下标部分
        public void AnimateClipReread(string Fpath)
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
                    //判定是a元件还是i元件
                    if (NextFile.Name.Substring(0, 1) == "a")
                    {
                        acrnum.Add(NextFile.Name.Substring(1, NextFile.Name.Length - 5));
                    }
                    else { }
                }
                else { }
            }
        }




        //生成a元件读取部分
        public void AnimateClipRead(string Fpath)
        {
            try
            {
                //运行预读程序以记录a元件序号
                AnimateClipReread(Fpath);
                //读取i元件信息并记录为数组
                aicr.ImageClipRead(Fpath);
                //记录下标
                int iidx = 0;
                //遍历i元件数组录入序号信息
                foreach (string item in aicr.irecord)
                {
                    if (item != null)
                    {
                        inum.Add(iidx.ToString());
                    }
                    else { }
                    iidx++;
                }
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
                        //获取DOMSymbolInstance节点libraryItemName属性
                        //创建xml读取对象
                        XmlDocument xmlDoc = new XmlDocument();
                        //读取xml
                        xmlDoc.Load(NextFile.FullName);
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
                        //判定是a元件还是i元件
                        if (NextFile.Name.Substring(0, 1) == "a")
                        {
                            //将数组填充空白至目前所在序号中
                            for (; arecord.Count < int.Parse(NextFile.Name.Substring(1, NextFile.Name.Length - 5)) + 1; arecord.Add(null)) ;
                            //检测是否存在a元件引用位图的情况
                            if (xmlDoc.GetElementsByTagName("DOMBitmapInstance").Count != 0)
                            {
                                form1.textBox15.AppendText(NextFile.Name.Substring(0, NextFile.Name.Length - 4) + "元件引用位图，将会引发错误,由于未进行元件替换位图操作，故移除" + "\r\n");
                                //插入空白记录
                                arecord[int.Parse(NextFile.Name.Substring(1, NextFile.Name.Length - 5))] = null;
                                //删除错误的元件
                                File.Delete(Fpath + "\\" + NextFile.Name);
                            }
                            else { }
                            //读取节点DOMSymbolInstance
                            XmlElement delement = (XmlElement)xmlDoc.GetElementsByTagName("DOMSymbolInstance")[0];
                            //判断该a元件是否引用元件
                            if (delement == null)
                            {
                                form1.textBox15.AppendText(NextFile.Name.Substring(0, NextFile.Name.Length - 4) + "元件未引用元件，将会引发错误，已删除" + "\r\n");
                                //插入空白记录
                                arecord[int.Parse(NextFile.Name.Substring(1, NextFile.Name.Length - 5))] = null;
                                //删除错误的元件
                                File.Delete(Fpath + "\\" + NextFile.Name);
                            }
                            else
                            {
                                //读取DOMSymbolInstance节点，检查a元件是否引用元件
                                foreach (XmlElement el in xmlDoc.GetElementsByTagName("DOMSymbolInstance"))
                                {
                                    //判断是否写引用元件名
                                    if (el.GetAttribute("libraryItemName") == "" || el.GetAttribute("libraryItemName") == null || el == null)
                                    {
                                        form1.textBox15.AppendText(NextFile.Name.Substring(0, NextFile.Name.Length - 4) + "元件未写引用元件名，将会引发错误，已删除" + "\r\n");
                                        //插入空白记录
                                        arecord[int.Parse(NextFile.Name.Substring(1, NextFile.Name.Length - 5))] = null;
                                        //删除错误的元件
                                        File.Delete(Fpath + "\\" + NextFile.Name);
                                        break;
                                    }
                                    else { }
                                }

                                //检测是否一个图层读取多个元件
                                //获取根节点root
                                XmlNode root = xmlDoc.DocumentElement;
                                //获取节点layers
                                XmlNode layers = root.FirstChild.FirstChild.FirstChild;
                                //获取layers图层列表
                                XmlNodeList layersnodeList = layers.ChildNodes;
                                //检测引用位图的图层和帧数
                                foreach (XmlNode node in layersnodeList)
                                {
                                    //转换DOMLayer为XmlElement以便于识别是否存在位图引用
                                    XmlElement DOMLayer = (XmlElement)node;
                                    //判断是否存在DOMSymbolInstance，以判定是否引用多元件
                                    foreach (XmlElement DOMFrame in DOMLayer.FirstChild.ChildNodes)
                                    {
                                        if (DOMFrame.GetElementsByTagName("DOMSymbolInstance").Count != 0)
                                        {
                                            if (DOMFrame.GetElementsByTagName("DOMSymbolInstance").Count != 1)
                                            {
                                                form1.textBox15.AppendText(NextFile.Name.Substring(0, NextFile.Name.Length - 4) + "元件引用多个元件，已移除" + "\r\n");
                                                //插入空白记录
                                                arecord[int.Parse(NextFile.Name.Substring(1, NextFile.Name.Length - 5))] = null;
                                                //删除错误的元件
                                                File.Delete(Fpath + "\\" + NextFile.Name);
                                                break;
                                            }
                                            else { }
                                        }
                                        else { }
                                    }
                                }

                                //读取DOMSymbolInstance节点
                                foreach (XmlElement el in xmlDoc.GetElementsByTagName("DOMSymbolInstance"))
                                {
                                    //a元件高低位检测
                                    //元件序号定义
                                    int anum = int.Parse(NextFile.Name.Substring(1, NextFile.Name.Length - 5));
                                    //被引用的首位定义
                                    string fname = el.GetAttribute("libraryItemName").Substring(0, 1);
                                    //被引用元件序号定义
                                    int ianum = int.Parse(el.GetAttribute("libraryItemName").Substring(1, el.GetAttribute("libraryItemName").Length - 1));
                                    //检测被引用的是否为a元件
                                    if (fname == "a")
                                    {
                                        //检测是否引用的元件为高序号
                                        if (anum > ianum)
                                        {

                                            if (acrnum.Contains(ianum.ToString()))
                                            {
                                                //记录元件序号对应的元件名称
                                                arecord[int.Parse(NextFile.Name.Substring(1, NextFile.Name.Length - 5))] = el.GetAttribute("libraryItemName");
                                            }
                                            else
                                            {
                                                form1.textBox15.AppendText(NextFile.Name.Substring(0, NextFile.Name.Length - 4) + "元件引用不存在元件" + el.GetAttribute("libraryItemName") + "，已移除" + "\r\n");
                                                //插入空白记录
                                                arecord[int.Parse(NextFile.Name.Substring(1, NextFile.Name.Length - 5))] = null;
                                                //删除错误的元件
                                                File.Delete(Fpath + "\\" + NextFile.Name);
                                                break;
                                            }
                                        }
                                        else
                                        {
                                            form1.textBox15.AppendText(NextFile.Name.Substring(0, NextFile.Name.Length - 4) + "元件引用高序号" + el.GetAttribute("libraryItemName") + "元件，将会引发错误，已删除" + "\r\n");
                                            //插入空白记录
                                            arecord[int.Parse(NextFile.Name.Substring(1, NextFile.Name.Length - 5))] = null;
                                            //删除错误的元件
                                            File.Delete(Fpath + "\\" + NextFile.Name);
                                            break;
                                        }
                                    }
                                    //检测被引用的是否为i元件
                                    if (fname == "i")
                                    {
                                        if (inum.Contains(ianum.ToString()))
                                        {
                                            //记录元件序号对应的元件名称
                                            arecord[int.Parse(NextFile.Name.Substring(1, NextFile.Name.Length - 5))] = el.GetAttribute("libraryItemName");
                                            //记录被引用的i元件序号，并且避免重复
                                            if (!airecord.Contains(el.GetAttribute("libraryItemName").Substring(1, el.GetAttribute("libraryItemName").Length - 1)))
                                            {
                                                //记录被引用的i元件序号
                                                airecord.Add(el.GetAttribute("libraryItemName").Substring(1, el.GetAttribute("libraryItemName").Length - 1));
                                            }
                                            else { }
                                        }
                                        else
                                        {
                                            form1.textBox15.AppendText(NextFile.Name.Substring(0, NextFile.Name.Length - 4) + "元件引用不存在元件" + el.GetAttribute("libraryItemName") + "，已移除" + "\r\n");
                                            //插入空白记录
                                            arecord[int.Parse(NextFile.Name.Substring(1, NextFile.Name.Length - 5))] = null;
                                            //删除错误的元件
                                            File.Delete(Fpath + "\\" + NextFile.Name);
                                            break;
                                        }
                                    }
                                    else { }
                                }
                            }
                        }
                        else { }
                    }
                    else { }
                }
            }
            catch
            {
                MessageBox.Show("AnimateClipRead ERROR");
            }
        }
    }
}