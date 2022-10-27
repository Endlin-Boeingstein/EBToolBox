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
    //建立i元件创制类
    class ImageClipCreator
    {
        //创建icri实例
        public ImageClipReader icri = new ImageClipReader();
        //定义irecord
        ArrayList irecord = new ArrayList();
        //定义引用图片文件名称
        public string pid = null;
        //预定义iccrsrecord以记录被其余元件引用的位图信息
        public ArrayList iccrsrecord = new ArrayList();
        //生成i元件创制部分
        public void ImageClipCreate(string Fpath, ArrayList irecord, ArrayList rsrecord)
        {
            try
            {
                form1.textBox15.AppendText("进行i元件创制操作......" + "\r\n");
                //为irecord赋值
                this.irecord = irecord;
                //为iccrsrecord赋值
                this.iccrsrecord = rsrecord;
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
                    //判定是否为png
                    if (fileclass == "13780")
                    {

                        //预定义新建元件名称
                        string icname = "i";
                        //新功能更新而停用//刷新空白记录数组
                        //新功能更新而停用///this.icri.ImageClipRead(Fpath);
                        //新功能更新而停用//空白数组刷新
                        //新功能更新而停用///this.irecord = this.icri.irecord;
                        //录入图片名称
                        pid = NextFile.Name.Substring(0, NextFile.Name.Length - 4);
                        //判定图片是否被元件引用
                        if (!this.irecord.Contains(pid))
                        {
                            if (this.irecord.Contains(null))
                            {
                                icname += this.irecord.IndexOf(null).ToString() + ".xml";
                                //信息录入数组
                                this.irecord[this.irecord.IndexOf(null)] = pid;
                            }
                            else
                            {
                                icname += this.irecord.Count.ToString() + ".xml";
                                //信息录入数组
                                this.irecord.Add(pid);
                            }
                            //定义预置元件名
                            string sample = "samplei.xml";
                            //判定使用0.781250还是1.000000参数的i元件
                            if (iccrsrecord.Contains(pid))
                            {
                                sample = "sampleb.xml";
                            }
                            else
                            {
                                sample = "samplei.xml";
                            }
                            //得到预置的和exe同文件夹的samplei.xml的路径
                            FileInfo fi = new FileInfo(System.Environment.CurrentDirectory + "\\ClipCreator\\" + sample);
                            //判定根目录读取函数是否生效，否则换第二条函数
                            if (!fi.Exists)
                            {
                                //得到预置的和exe同文件夹的samplei.xml的路径
                                fi = new FileInfo(AppDomain.CurrentDomain.BaseDirectory + "ClipCreator\\" + sample);
                            }
                            //判断文件是否缺失
                            if (fi.Exists)
                            {
                                //添加预置的元件
                                fi.CopyTo(Fpath + "\\" + icname);
                                //创建xml读取对象
                                XmlDocument xmlDoc = new XmlDocument();
                                //读取xml
                                xmlDoc.Load(Fpath + "\\" + icname);
                                //读取DOMSymbolItem节点
                                XmlElement element = (XmlElement)xmlDoc.GetElementsByTagName("DOMSymbolItem")[0];
                                //设定iname
                                string iname = null;
                                //读取DOMSymbolItem节点name属性字符串
                                iname = element.GetAttribute("name");
                                if (iname == null || iname == "0" || iname == "" || iname != icname.Substring(0, icname.Length - 4))
                                {
                                    //录入name
                                    element.SetAttribute("name", icname.Substring(0, icname.Length - 4));
                                    //保存xml
                                    xmlDoc.Save(Fpath + "\\" + icname);
                                }
                                else { }
                                //读取DOMTimeline节点
                                XmlElement frameelement = (XmlElement)xmlDoc.GetElementsByTagName("DOMTimeline")[0];
                                if (frameelement.GetAttribute("name") == null || frameelement.GetAttribute("name") == "0" || frameelement.GetAttribute("name") == "" || frameelement.GetAttribute("name") != icname.Substring(0, icname.Length - 4))
                                {
                                    //录入name
                                    frameelement.SetAttribute("name", icname.Substring(0, icname.Length - 4));
                                    //保存xml
                                    xmlDoc.Save(Fpath + "\\" + icname);
                                }
                                else { }
                                //读取DOMBitmapInstance节点
                                XmlElement dbielement = (XmlElement)xmlDoc.GetElementsByTagName("DOMBitmapInstance")[0];
                                if (dbielement.GetAttribute("libraryItemName") == null || dbielement.GetAttribute("libraryItemName") == "0" || dbielement.GetAttribute("libraryItemName") == "" || dbielement.GetAttribute("libraryItemName") != pid)
                                {
                                    //录入name
                                    dbielement.SetAttribute("libraryItemName", pid);
                                    //保存xml
                                    xmlDoc.Save(Fpath + "\\" + icname);
                                }
                                else { }
                                //提示创制完成
                                form1.textBox15.AppendText("已为位图" + pid + "创制元件" + icname.Substring(0, icname.Length - 4) + "\r\n");
                            }
                            else MessageBox.Show("软件原有的samplei.xml或者sampleb.xml缺失！");
                        }
                        else { }
                    }
                    else { }
                }
                form1.textBox15.AppendText("i元件创制完成" + "\r\n");
            }
            catch
            {
                MessageBox.Show("ImageClipCreate ERROR");
            }
        }
    }
}