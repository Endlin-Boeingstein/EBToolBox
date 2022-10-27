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
    //建立其余元件位图引用检查类
    class ReplaceScanner
    {
        //创建ssr实例
        public SpriteReader ssr = new SpriteReader();
        //预定义srecord以记录所有位图名称
        public ArrayList srecord = new ArrayList();
        //定义rsrecord以记录被其余元件引用的位图信息
        public ArrayList rsrecord = new ArrayList();
        //定义rsarecord以记录引用位图的a元件信息
        public ArrayList rsarecord = new ArrayList();
        //定义mllsrecord以记录引用多位图/元件的a元件信息
        public ArrayList mllsrecord = new ArrayList();
        //新功能更新而停用//定义mr为是否需要main元件位图替换标志
        //新功能更新而停用///public int mr = 0;
        //预定义Fpath
        public string Fpath = null;
        //生成其余元件位图引用检查部分
        public void ReplaceScan(string Fpath)
        {
            try
            {
                this.Fpath = Fpath;
                //获得所有位图名称
                ssr.SpriteRead(this.Fpath);
                //为srecord赋值
                this.srecord = ssr.srecord;
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
                        //判定是a元件还是main元件
                        //判定为a元件
                        if (NextFile.Name.Substring(0, 1) == "a")
                        {
                            //新功能更新而停用//将数组填充空白至目前所在序号中
                            //新功能更新而停用///for (; rsarecord.Count < int.Parse(NextFile.Name.Substring(1, NextFile.Name.Length - 5)) + 1; rsarecord.Add(null)) ;
                            //检测是否存在a元件引用位图的情况
                            if (xmlDoc.GetElementsByTagName("DOMBitmapInstance").Count != 0)
                            {
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
                                    //判断是否存在DOMBitmapInstance，以判定是否引用位图
                                    foreach (XmlElement DOMFrame in DOMLayer.FirstChild.ChildNodes)
                                    {
                                        if (DOMFrame.GetElementsByTagName("DOMBitmapInstance").Count != 0)
                                        {
                                            //定义第一个位图名称以判定是否同图层每帧存在异位图
                                            string lname = ((XmlElement)DOMLayer.GetElementsByTagName("DOMBitmapInstance")[0]).GetAttribute("libraryItemName");

                                            if (DOMFrame.GetElementsByTagName("DOMBitmapInstance").Count == 1)
                                            {
                                                if (this.srecord.Contains(((XmlElement)DOMFrame.GetElementsByTagName("DOMBitmapInstance")[0]).GetAttribute("libraryItemName")))
                                                {
                                                    if (!rsarecord.Contains(((XmlElement)DOMFrame.GetElementsByTagName("DOMBitmapInstance")[0]).GetAttribute("libraryItemName")))
                                                    {
                                                        //新功能更新而停用//记录引用位图的a元件以及对应的位图名称
                                                        //新功能更新而停用///rsarecord[int.Parse(NextFile.Name.Substring(1, NextFile.Name.Length - 5))] = ((XmlElement)xmlDoc.GetElementsByTagName("DOMBitmapInstance")[0]).GetAttribute("libraryItemName");
                                                        //记录引用位图的a元件以及位图名称
                                                        //新功能更新而停用///rsarecord[int.Parse(NextFile.Name.Substring(1, NextFile.Name.Length - 5))] = NextFile.Name.Substring(0, NextFile.Name.Length - 4);
                                                        rsarecord.Add(NextFile.Name.Substring(0, NextFile.Name.Length - 4));
                                                    }
                                                    else { }
                                                    if (!rsrecord.Contains(((XmlElement)DOMFrame.GetElementsByTagName("DOMBitmapInstance")[0]).GetAttribute("libraryItemName")))
                                                    {
                                                        rsrecord.Add(((XmlElement)DOMFrame.GetElementsByTagName("DOMBitmapInstance")[0]).GetAttribute("libraryItemName"));
                                                    }
                                                    else { }
                                                }
                                                else
                                                {
                                                    //预置图层名
                                                    string bname = DOMLayer.GetAttribute("name");
                                                    //预置帧数
                                                    int bindex = int.Parse(DOMFrame.GetAttribute("index"));
                                                    //预置长度
                                                    int bduration;
                                                    if (DOMFrame.GetAttribute("duration") == null || DOMFrame.GetAttribute("duration") == "")
                                                    {
                                                        bduration = 1;
                                                    }
                                                    else
                                                    {
                                                        bduration = int.Parse(DOMFrame.GetAttribute("duration"));
                                                    }
                                                    form1.textBox15.AppendText(NextFile.Name.Substring(0, NextFile.Name.Length - 4) + "元件图层" + bname + "第" + bindex + "帧（在Adobe Animate中为第" + (bindex + 1) + "帧）引用不存在位图，长度" + bduration + "帧，将会引发错误" + "\r\n");
                                                }

                                                //判定是否不同帧不同位图
                                                if (((XmlElement)DOMFrame.GetElementsByTagName("DOMBitmapInstance")[0]).GetAttribute("libraryItemName") != lname)
                                                {
                                                    //预置图层名
                                                    string bname = DOMLayer.GetAttribute("name");
                                                    //预置帧数
                                                    int bindex = int.Parse(DOMFrame.GetAttribute("index"));
                                                    //预置长度
                                                    int bduration;
                                                    if (DOMFrame.GetAttribute("duration") == null || DOMFrame.GetAttribute("duration") == "")
                                                    {
                                                        bduration = 1;
                                                    }
                                                    else
                                                    {
                                                        bduration = int.Parse(DOMFrame.GetAttribute("duration"));
                                                    }
                                                    form1.textBox15.AppendText(NextFile.Name.Substring(0, NextFile.Name.Length - 4) + "元件图层" + bname + "第" + bindex + "帧（在Adobe Animate中为第" + (bindex + 1) + "帧）引用不同种位图，长度" + bduration + "帧，将会引发错误" + "\r\n");
                                                    if (!mllsrecord.Contains(NextFile.Name.Substring(0, NextFile.Name.Length - 4)))
                                                    {
                                                        //记录引用不同种位图/元件的a元件信息
                                                        mllsrecord.Add(NextFile.Name.Substring(0, NextFile.Name.Length - 4));
                                                    }
                                                    else { }
                                                }
                                                else { }
                                            }
                                            else
                                            {
                                                //预置图层名
                                                string bname = DOMLayer.GetAttribute("name");
                                                //预置帧数
                                                int bindex = int.Parse(DOMFrame.GetAttribute("index"));
                                                //预置长度
                                                int bduration;
                                                if (DOMFrame.GetAttribute("duration") == null || DOMFrame.GetAttribute("duration") == "")
                                                {
                                                    bduration = 1;
                                                }
                                                else
                                                {
                                                    bduration = int.Parse(DOMFrame.GetAttribute("duration"));
                                                }
                                                //遍历单图层引用多位图情况，并记录给数组
                                                for (int t = 0; t < DOMFrame.GetElementsByTagName("DOMBitmapInstance").Count; t++)
                                                {
                                                    if (this.srecord.Contains(((XmlElement)DOMFrame.GetElementsByTagName("DOMBitmapInstance")[t]).GetAttribute("libraryItemName")))
                                                    {
                                                        if (!rsarecord.Contains(((XmlElement)DOMFrame.GetElementsByTagName("DOMBitmapInstance")[t]).GetAttribute("libraryItemName")))
                                                        {
                                                            //新功能更新而停用//记录引用位图的a元件以及对应的位图名称
                                                            //新功能更新而停用///rsarecord[int.Parse(NextFile.Name.Substring(1, NextFile.Name.Length - 5))] = ((XmlElement)xmlDoc.GetElementsByTagName("DOMBitmapInstance")[t]).GetAttribute("libraryItemName");
                                                            //记录引用位图的a元件以及位图名称
                                                            //新功能更新而停用///rsarecord[int.Parse(NextFile.Name.Substring(1, NextFile.Name.Length - 5))] = NextFile.Name.Substring(0, NextFile.Name.Length - 4);
                                                            rsarecord.Add(NextFile.Name.Substring(0, NextFile.Name.Length - 4));
                                                            if (!mllsrecord.Contains(NextFile.Name.Substring(0, NextFile.Name.Length - 4)))
                                                            {
                                                                //记录引用多位图/元件的a元件信息
                                                                mllsrecord.Add(NextFile.Name.Substring(0, NextFile.Name.Length - 4));
                                                            }
                                                            else { }
                                                        }
                                                        else { }
                                                        if (!rsrecord.Contains(((XmlElement)DOMFrame.GetElementsByTagName("DOMBitmapInstance")[t]).GetAttribute("libraryItemName")))
                                                        {
                                                            rsrecord.Add(((XmlElement)DOMFrame.GetElementsByTagName("DOMBitmapInstance")[t]).GetAttribute("libraryItemName"));
                                                        }
                                                        else { }
                                                    }
                                                    else
                                                    {
                                                        form1.textBox15.AppendText(NextFile.Name.Substring(0, NextFile.Name.Length - 4) + "元件图层" + bname + "第" + bindex + "帧（在Adobe Animate中为第" + (bindex + 1) + "帧）引用不存在位图，长度" + bduration + "帧，将会引发错误" + "\r\n");
                                                    }
                                                }
                                                form1.textBox15.AppendText(NextFile.Name.Substring(0, NextFile.Name.Length - 4) + "元件图层" + bname + "第" + bindex + "帧（在Adobe Animate中为第" + (bindex + 1) + "帧）引用多个位图，长度" + bduration + "帧，将会引发错误" + "\r\n");
                                            }
                                        }
                                        else { }
                                    }
                                }
                                form1.textBox15.AppendText(NextFile.Name.Substring(0, NextFile.Name.Length - 4) + "元件引用位图情况检测完成，在i元件创制完成后使用i元件替换位图功能以解决问题\n（如果是多位图/元件图层，请先使用多位图/元件图层分解功能，以免后续操作造成无法挽回的损失）" + "\r\n");
                            }
                            else { }

                            //检测是否一个图层读取多个元件
                            if (xmlDoc.GetElementsByTagName("DOMSymbolInstance").Count != 0)
                            {
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
                                            //定义第一个元件名称以判定是否同图层每帧存在异元件
                                            string lname = ((XmlElement)DOMLayer.GetElementsByTagName("DOMSymbolInstance")[0]).GetAttribute("libraryItemName");

                                            if (DOMFrame.GetElementsByTagName("DOMSymbolInstance").Count != 1)
                                            {
                                                //预置图层名
                                                string bname = DOMLayer.GetAttribute("name");
                                                //预置帧数
                                                int bindex = int.Parse(DOMFrame.GetAttribute("index"));
                                                //预置长度
                                                int bduration;
                                                if (DOMFrame.GetAttribute("duration") == null || DOMFrame.GetAttribute("duration") == "")
                                                {
                                                    bduration = 1;
                                                }
                                                else
                                                {
                                                    bduration = int.Parse(DOMFrame.GetAttribute("duration"));
                                                }
                                                form1.textBox15.AppendText(NextFile.Name.Substring(0, NextFile.Name.Length - 4) + "元件图层" + bname + "第" + bindex + "帧（在Adobe Animate中为第" + (bindex + 1) + "帧）引用多个元件，长度" + bduration + "帧，将会引发错误" + "\r\n");
                                                if (!mllsrecord.Contains(NextFile.Name.Substring(0, NextFile.Name.Length - 4)))
                                                {
                                                    //记录引用多位图/元件的a元件信息
                                                    mllsrecord.Add(NextFile.Name.Substring(0, NextFile.Name.Length - 4));
                                                }
                                                else { }
                                            }
                                            else
                                            {
                                                //判定同图层是否每帧不同元件
                                                if (((XmlElement)DOMFrame.GetElementsByTagName("DOMSymbolInstance")[0]).GetAttribute("libraryItemName") != lname)
                                                {
                                                    //预置图层名
                                                    string bname = DOMLayer.GetAttribute("name");
                                                    //预置帧数
                                                    int bindex = int.Parse(DOMFrame.GetAttribute("index"));
                                                    //预置长度
                                                    int bduration;
                                                    if (DOMFrame.GetAttribute("duration") == null || DOMFrame.GetAttribute("duration") == "")
                                                    {
                                                        bduration = 1;
                                                    }
                                                    else
                                                    {
                                                        bduration = int.Parse(DOMFrame.GetAttribute("duration"));
                                                    }
                                                    form1.textBox15.AppendText(NextFile.Name.Substring(0, NextFile.Name.Length - 4) + "元件图层" + bname + "第" + bindex + "帧（在Adobe Animate中为第" + (bindex + 1) + "帧）引用不同种元件，长度" + bduration + "帧，将会引发错误" + "\r\n");
                                                    if (!mllsrecord.Contains(NextFile.Name.Substring(0, NextFile.Name.Length - 4)))
                                                    {
                                                        //记录引用不同种位图/元件的a元件信息
                                                        mllsrecord.Add(NextFile.Name.Substring(0, NextFile.Name.Length - 4));
                                                    }
                                                    else { }
                                                }
                                                else { }
                                            }
                                        }
                                        else { }
                                    }
                                }
                            }
                            else { }

                        }
                        //判定为main元件
                        if (NextFile.Name.Substring(0, NextFile.Name.Length - 4) == "main")
                        {
                            //检测是否存在引用位图的情况
                            if (xmlDoc.GetElementsByTagName("DOMBitmapInstance").Count == 0) { }
                            else
                            {
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
                                    //判断是否存在DOMBitmapInstance，以判定是否引用位图
                                    foreach (XmlElement DOMFrame in DOMLayer.FirstChild.ChildNodes)
                                    {
                                        if (DOMFrame.GetElementsByTagName("DOMBitmapInstance").Count != 0)
                                        {
                                            //定义第一个位图名称以判定是否同图层每帧存在异位图
                                            string lname = ((XmlElement)DOMLayer.GetElementsByTagName("DOMBitmapInstance")[0]).GetAttribute("libraryItemName");

                                            if (DOMFrame.GetElementsByTagName("DOMBitmapInstance").Count == 1)
                                            {
                                                if (this.srecord.Contains(((XmlElement)DOMFrame.GetElementsByTagName("DOMBitmapInstance")[0]).GetAttribute("libraryItemName")))
                                                {
                                                    if (!rsarecord.Contains("main"))
                                                    {
                                                        //新功能更新而停用//为mr赋值确定需要替换位图
                                                        //新功能更新而停用///mr = 1;
                                                        //记录引用位图的main元件
                                                        rsarecord.Add("main");
                                                    }
                                                    else { }
                                                    if (!rsrecord.Contains(((XmlElement)DOMFrame.GetElementsByTagName("DOMBitmapInstance")[0]).GetAttribute("libraryItemName")))
                                                    {
                                                        rsrecord.Add(((XmlElement)DOMFrame.GetElementsByTagName("DOMBitmapInstance")[0]).GetAttribute("libraryItemName"));
                                                    }
                                                    else { }
                                                }
                                                else
                                                {
                                                    //预置图层名
                                                    string bname = DOMLayer.GetAttribute("name");
                                                    //预置帧数
                                                    int bindex = int.Parse(DOMFrame.GetAttribute("index"));
                                                    //预置长度
                                                    int bduration;
                                                    if (DOMFrame.GetAttribute("duration") == null || DOMFrame.GetAttribute("duration") == "")
                                                    {
                                                        bduration = 1;
                                                    }
                                                    else
                                                    {
                                                        bduration = int.Parse(DOMFrame.GetAttribute("duration"));
                                                    }
                                                    form1.textBox15.AppendText("main元件图层" + bname + "第" + bindex + "帧（在Adobe Animate中为第" + (bindex + 1) + "帧）引用不存在位图，长度" + bduration + "帧，将会引发错误" + "\r\n");
                                                }

                                                //判定是否同图层每帧不同位图
                                                if (((XmlElement)DOMFrame.GetElementsByTagName("DOMBitmapInstance")[0]).GetAttribute("libraryItemName") != lname)
                                                {
                                                    //预置图层名
                                                    string bname = DOMLayer.GetAttribute("name");
                                                    //预置帧数
                                                    int bindex = int.Parse(DOMFrame.GetAttribute("index"));
                                                    //预置长度
                                                    int bduration;
                                                    if (DOMFrame.GetAttribute("duration") == null || DOMFrame.GetAttribute("duration") == "")
                                                    {
                                                        bduration = 1;
                                                    }
                                                    else
                                                    {
                                                        bduration = int.Parse(DOMFrame.GetAttribute("duration"));
                                                    }
                                                    form1.textBox15.AppendText("main元件图层" + bname + "第" + bindex + "帧（在Adobe Animate中为第" + (bindex + 1) + "帧）引用不同种位图，长度" + bduration + "帧，将会引发错误" + "\r\n");
                                                    if (!mllsrecord.Contains(NextFile.Name.Substring(0, NextFile.Name.Length - 4)))
                                                    {
                                                        //记录引用不同种位图/元件的元件信息
                                                        mllsrecord.Add(NextFile.Name.Substring(0, NextFile.Name.Length - 4));
                                                    }
                                                    else { }
                                                }
                                                else { }
                                            }
                                            else
                                            {
                                                //预置图层名
                                                string bname = DOMLayer.GetAttribute("name");
                                                //预置帧数
                                                int bindex = int.Parse(DOMFrame.GetAttribute("index"));
                                                //预置长度
                                                int bduration = int.Parse(DOMFrame.GetAttribute("duration"));
                                                if (DOMFrame.GetAttribute("duration") == null || DOMFrame.GetAttribute("duration") == "")
                                                {
                                                    bduration = 1;
                                                }
                                                else
                                                {
                                                    bduration = int.Parse(DOMFrame.GetAttribute("duration"));
                                                }
                                                //遍历单图层引用多位图情况，并记录给数组
                                                for (int t = 0; t < DOMFrame.GetElementsByTagName("DOMBitmapInstance").Count; t++)
                                                {
                                                    if (this.srecord.Contains(((XmlElement)DOMFrame.GetElementsByTagName("DOMBitmapInstance")[t]).GetAttribute("libraryItemName")))
                                                    {
                                                        if (!rsarecord.Contains("main"))
                                                        {
                                                            //新功能更新而停用//为mr赋值确定需要替换位图
                                                            //新功能更新而停用///mr = 1;
                                                            //记录引用位图的main元件
                                                            rsarecord.Add("main");
                                                            if (!mllsrecord.Contains(NextFile.Name.Substring(0, NextFile.Name.Length - 4)))
                                                            {
                                                                //记录引用多位图/元件的a元件信息
                                                                mllsrecord.Add(NextFile.Name.Substring(0, NextFile.Name.Length - 4));
                                                            }
                                                            else { }
                                                        }
                                                        else { }
                                                        if (!rsrecord.Contains(((XmlElement)DOMFrame.GetElementsByTagName("DOMBitmapInstance")[t]).GetAttribute("libraryItemName")))
                                                        {
                                                            rsrecord.Add(((XmlElement)DOMFrame.GetElementsByTagName("DOMBitmapInstance")[t]).GetAttribute("libraryItemName"));
                                                        }
                                                        else { }
                                                    }
                                                    else
                                                    {
                                                        form1.textBox15.AppendText("main元件图层" + bname + "第" + bindex + "帧（在Adobe Animate中为第" + (bindex + 1) + "帧）引用不存在位图，长度" + bduration + "帧，将会引发错误" + "\r\n");
                                                    }
                                                }
                                                form1.textBox15.AppendText("main元件图层" + bname + "第" + bindex + "帧（在Adobe Animate中为第" + (bindex + 1) + "帧）引用多个位图，长度" + bduration + "帧，将会引发错误" + "\r\n");
                                            }
                                        }
                                        else { }
                                    }
                                }
                                form1.textBox15.AppendText("main元件引用位图情况检测完成，在i元件创制完成后使用i元件替换位图功能以解决问题\n（如果是多位图/元件图层，请先使用多位图/元件图层分解功能，以免后续操作造成无法挽回的损失）" + "\r\n");
                            }

                            //检测是否一个图层读取多个元件
                            if (xmlDoc.GetElementsByTagName("DOMSymbolInstance").Count != 0)
                            {
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
                                            //定义第一个元件名称以判定是否同图层每帧存在异元件
                                            string lname = ((XmlElement)DOMLayer.GetElementsByTagName("DOMSymbolInstance")[0]).GetAttribute("libraryItemName");

                                            if (DOMFrame.GetElementsByTagName("DOMSymbolInstance").Count != 1)
                                            {
                                                //预置图层名
                                                string bname = DOMLayer.GetAttribute("name");
                                                //预置帧数
                                                int bindex = int.Parse(DOMFrame.GetAttribute("index"));
                                                //预置长度
                                                int bduration;
                                                if (DOMFrame.GetAttribute("duration") == null || DOMFrame.GetAttribute("duration") == "")
                                                {
                                                    bduration = 1;
                                                }
                                                else
                                                {
                                                    bduration = int.Parse(DOMFrame.GetAttribute("duration"));
                                                }
                                                form1.textBox15.AppendText("main元件图层" + bname + "第" + bindex + "帧（在Adobe Animate中为第" + (bindex + 1) + "帧）引用多个元件，长度" + bduration + "帧，将会引发错误" + "\r\n");
                                                if (!mllsrecord.Contains(NextFile.Name.Substring(0, NextFile.Name.Length - 4)))
                                                {
                                                    //记录引用多位图/元件的a元件信息
                                                    mllsrecord.Add(NextFile.Name.Substring(0, NextFile.Name.Length - 4));
                                                }
                                                else { }
                                            }
                                            else
                                            {
                                                //判定同图层是否每帧不同元件
                                                if (((XmlElement)DOMFrame.GetElementsByTagName("DOMSymbolInstance")[0]).GetAttribute("libraryItemName") != lname)
                                                {
                                                    //预置图层名
                                                    string bname = DOMLayer.GetAttribute("name");
                                                    //预置帧数
                                                    int bindex = int.Parse(DOMFrame.GetAttribute("index"));
                                                    //预置长度
                                                    int bduration;
                                                    if (DOMFrame.GetAttribute("duration") == null || DOMFrame.GetAttribute("duration") == "")
                                                    {
                                                        bduration = 1;
                                                    }
                                                    else
                                                    {
                                                        bduration = int.Parse(DOMFrame.GetAttribute("duration"));
                                                    }
                                                    form1.textBox15.AppendText("main元件图层" + bname + "第" + bindex + "帧（在Adobe Animate中为第" + (bindex + 1) + "帧）引用不同种元件，长度" + bduration + "帧，将会引发错误" + "\r\n");
                                                    if (!mllsrecord.Contains(NextFile.Name.Substring(0, NextFile.Name.Length - 4)))
                                                    {
                                                        //记录引用不同种位图/元件的元件信息
                                                        mllsrecord.Add(NextFile.Name.Substring(0, NextFile.Name.Length - 4));
                                                    }
                                                    else { }
                                                }
                                                else { }
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
                    else { }
                }
            }
            catch
            {
                MessageBox.Show("ReplaceScan ERROR");
            }
        }
    }
}