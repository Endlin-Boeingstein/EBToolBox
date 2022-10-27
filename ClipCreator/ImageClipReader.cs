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
    //建立i元件读取类
    class ImageClipReader
    {
        //创建isr实例
        public SpriteReader isr = new SpriteReader();
        //定义i元件记录数组
        public ArrayList irecord = new ArrayList();
        //生成i元件读取部分
        public void ImageClipRead(string Fpath)
        {
            try
            {
                //读取位图信息并记录为数组
                isr.SpriteRead(Fpath);
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
                        //获取DOMBitmapInstance节点libraryItemName属性
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
                        //判定是i元件还是a元件
                        if (NextFile.Name.Substring(0, 1) == "i")
                        {
                            //将数组填充空白至目前所在序号中
                            for (; irecord.Count < int.Parse(NextFile.Name.Substring(1, NextFile.Name.Length - 5)) + 1; irecord.Add(null)) ;
                            //检测是否存在i元件引用元件以及i元件引用多个位图的情况
                            if (xmlDoc.GetElementsByTagName("DOMBitmapInstance").Count > 1)
                            {
                                form1.textBox15.AppendText(NextFile.Name.Substring(0, NextFile.Name.Length - 4) + "元件引用多个位图，将会引发错误，已移除" + "\r\n");
                                //插入空白记录
                                irecord[int.Parse(NextFile.Name.Substring(1, NextFile.Name.Length - 5))] = null;
                                //删除错误的元件
                                File.Delete(Fpath + "\\" + NextFile.Name);
                            }
                            if (xmlDoc.GetElementsByTagName("DOMSymbolInstance").Count != 0)
                            {
                                form1.textBox15.AppendText(NextFile.Name.Substring(0, NextFile.Name.Length - 4) + "元件引用元件，将会引发错误，已移除" + "\r\n");
                                //插入空白记录
                                irecord[int.Parse(NextFile.Name.Substring(1, NextFile.Name.Length - 5))] = null;
                                //删除错误的元件
                                File.Delete(Fpath + "\\" + NextFile.Name);
                            }
                            else { }
                            //读取DOMBitmapInstance节点
                            XmlElement element = (XmlElement)xmlDoc.GetElementsByTagName("DOMBitmapInstance")[0];
                            //判断该i元件是否引用位图
                            if (element == null)
                            {
                                form1.textBox15.AppendText(NextFile.Name.Substring(0, NextFile.Name.Length - 4) + "元件未引用位图，将会引发错误，已移除" + "\r\n");
                                //插入空白记录
                                irecord[int.Parse(NextFile.Name.Substring(1, NextFile.Name.Length - 5))] = null;
                                //删除错误的元件
                                File.Delete(Fpath + "\\" + NextFile.Name);
                            }
                            else
                            {
                                //读取DOMBitmapInstance节点，检查i元件是否引用位图
                                foreach (XmlElement el in xmlDoc.GetElementsByTagName("DOMBitmapInstance"))
                                {
                                    //判断是否写引用位图名
                                    if (el.GetAttribute("libraryItemName") == "" || el.GetAttribute("libraryItemName") == null || el == null)
                                    {
                                        form1.textBox15.AppendText(NextFile.Name.Substring(0, NextFile.Name.Length - 4) + "元件未写引用位图名，将会引发错误，已移除" + "\r\n");
                                        //插入空白记录
                                        irecord[int.Parse(NextFile.Name.Substring(1, NextFile.Name.Length - 5))] = null;
                                        //删除错误的元件
                                        File.Delete(Fpath + "\\" + NextFile.Name);
                                    }
                                    else
                                    {
                                        //删除引用不存在位图的元件
                                        if (isr.srecord.Contains(el.GetAttribute("libraryItemName")))
                                        {
                                            //记录元件序号对应的位图名称
                                            irecord[int.Parse(NextFile.Name.Substring(1, NextFile.Name.Length - 5))] = el.GetAttribute("libraryItemName");
                                        }
                                        else
                                        {
                                            form1.textBox15.AppendText(NextFile.Name.Substring(0, NextFile.Name.Length - 4) + "元件引用不存在位图" + el.GetAttribute("libraryItemName") + "，已移除" + "\r\n");
                                            //插入空白记录
                                            irecord[int.Parse(NextFile.Name.Substring(1, NextFile.Name.Length - 5))] = null;
                                            //删除错误的元件
                                            File.Delete(Fpath + "\\" + NextFile.Name);
                                        }
                                    }
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
                MessageBox.Show("ImageClipRead ERROR");
            }
        }
    }
}