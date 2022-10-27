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
    //建立元件加密类
    class ClipEncryptor
    {
        //加密元件计数器
        long count = 9999999999999999;
        //创建cd实例
        public ClipDecryptor cd = new ClipDecryptor();
        //生成元件加密部分
        public void ClipEncrypt(string Jpath, string Fpath)
        {
            try
            {
                form1.textBox16.AppendText("开始进行加解密操作......" + "\r\n");
                //新功能更新而停用///Console.WriteLine("请根据以下需求输入相应序号并按回车键（不输入按回车默认不加密）\n1.不进行任何加密\n2.仅加密a元件\n3.仅加密main元件\n4.全部加密\n5.全部解密\n注：这里的XFL加密仅仅是让太极合成后的PAM解不出对应元件，解密选项仅用于去除因为操作失误而用本软件给元件加密的元件保密层，并不是能把加密后合成的PAM解开\n加密后再次使用软件，生成extra.json会出现错误，属于正常现象，需解密后生成正常的extra.json");
                //是否a元件加密、是否main元件加密
                int ac = 0, mc = 0;
                //新功能更新而停用//选项收录
                //新功能更新而停用///string s = Console.ReadLine();
                //新功能更新而停用//判定是否直接回车，输入0、1或直接回车则执行，2为a元件加密，3为main元件加密，4为全部加密，5为全部解密
                if (form1.radioButton7.Checked)
                {
                    form1.textBox16.AppendText("不进行任何加密" + "\r\n");
                }
                else if (form1.radioButton2.Checked)
                {
                    form1.textBox16.AppendText("开始进行a元件加密操作......" + "\r\n");
                    ac = 1;
                }
                else if (form1.radioButton1.Checked)
                {
                    form1.textBox16.AppendText("开始进行main元件加密操作......" + "\r\n");
                    mc = 1;
                }
                else if (form1.radioButton8.Checked)
                {
                    form1.textBox16.AppendText("开始进行全部加密操作......" + "\r\n");
                    ac = 1;
                    mc = 1;
                }
                else if (form1.radioButton9.Checked)
                {
                    form1.textBox16.AppendText("开始进行全部解密操作......" + "\r\n");
                    cd.ClipDecrypt(Jpath, Fpath);
                }
                else
                {
                    form1.textBox16.AppendText("输入数字错误，执行默认操作" + "\r\n");
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
                        //创建xml读取对象
                        XmlDocument xmlDoc = new XmlDocument();
                        //读取xml
                        xmlDoc.Load(NextFile.FullName);
                        //预置DOMLayer节点
                        XmlElement DOMLayer = xmlDoc.CreateElement("DOMLayer", xmlDoc.DocumentElement.NamespaceURI);
                        //设name为encrypt_layer
                        DOMLayer.SetAttribute("name", "encrypt_layer");
                        //预置frames节点
                        XmlElement frames = xmlDoc.CreateElement("frames", xmlDoc.DocumentElement.NamespaceURI);
                        //预置DOMFrame节点
                        XmlElement DOMFrame = xmlDoc.CreateElement("DOMFrame", xmlDoc.DocumentElement.NamespaceURI);
                        //设index为0
                        DOMFrame.SetAttribute("index", "0");
                        //设duration为1
                        DOMFrame.SetAttribute("duration", "1");
                        //预置elements节点
                        XmlElement elements = xmlDoc.CreateElement("elements", xmlDoc.DocumentElement.NamespaceURI);
                        //预置DOMSymbolInstance节点
                        XmlElement DOMSymbolInstance = xmlDoc.CreateElement("DOMSymbolInstance", xmlDoc.DocumentElement.NamespaceURI);
                        //设libraryItemName为encrypt_clip
                        DOMSymbolInstance.SetAttribute("libraryItemName", "i9999999999999999");
                        //预置matrix节点
                        XmlElement matrix = xmlDoc.CreateElement("matrix", xmlDoc.DocumentElement.NamespaceURI);
                        //预置Matrix节点
                        XmlElement Matrix = xmlDoc.CreateElement("Matrix", xmlDoc.DocumentElement.NamespaceURI);
                        //设a为1.000000
                        Matrix.SetAttribute("a", "1.000000");
                        //设d为1.000000
                        Matrix.SetAttribute("d", "1.000000");
                        //将Matrix作为matrix的子节点
                        matrix.PrependChild(Matrix);
                        //将matrix作为DOMSymbolInstance的子节点
                        DOMSymbolInstance.PrependChild(matrix);
                        //将DOMSymbolInstance作为elements的子节点
                        elements.PrependChild(DOMSymbolInstance);
                        //将elements作为DOMFrame的子节点
                        DOMFrame.PrependChild(elements);
                        //将DOMFrame作为frames的子节点
                        frames.PrependChild(DOMFrame);
                        //将frames作为DOMLayer的子节点
                        DOMLayer.PrependChild(frames);

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
                        //判断为a元件并加密
                        if (NextFile.Name.Substring(0, 1) == "a" && ac == 1)
                        {
                            //计数器启动
                            count--;
                            //预置DOMLayerl节点
                            XmlElement DOMLayerl = xmlDoc.CreateElement("DOMLayer", xmlDoc.DocumentElement.NamespaceURI);
                            //设name为encrypt_layer
                            DOMLayerl.SetAttribute("name", "encrypt_layer");
                            //预置framesl节点
                            XmlElement framesl = xmlDoc.CreateElement("frames", xmlDoc.DocumentElement.NamespaceURI);
                            //预置DOMFramel节点
                            XmlElement DOMFramel = xmlDoc.CreateElement("DOMFrame", xmlDoc.DocumentElement.NamespaceURI);
                            //设index为0
                            DOMFramel.SetAttribute("index", "0");
                            //设duration为1
                            DOMFramel.SetAttribute("duration", "1");
                            //预置elementsl节点
                            XmlElement elementsl = xmlDoc.CreateElement("elements", xmlDoc.DocumentElement.NamespaceURI);
                            //预置DOMSymbolInstancel节点
                            XmlElement DOMSymbolInstancel = xmlDoc.CreateElement("DOMSymbolInstance", xmlDoc.DocumentElement.NamespaceURI);
                            //设libraryItemName为encrypt_clip
                            DOMSymbolInstancel.SetAttribute("libraryItemName", "i" + count.ToString());
                            //预置matrixl节点
                            XmlElement matrixl = xmlDoc.CreateElement("matrix", xmlDoc.DocumentElement.NamespaceURI);
                            //预置Matrixl节点
                            XmlElement Matrixl = xmlDoc.CreateElement("Matrix", xmlDoc.DocumentElement.NamespaceURI);
                            //设a为1.000000
                            Matrixl.SetAttribute("a", "1.000000");
                            //设d为1.000000
                            Matrixl.SetAttribute("d", "1.000000");
                            //将Matrixl作为matrixl的子节点
                            matrixl.PrependChild(Matrixl);
                            //将matrixl作为DOMSymbolInstancel的子节点
                            DOMSymbolInstancel.PrependChild(matrixl);
                            //将DOMSymbolInstancel作为elementsl的子节点
                            elementsl.PrependChild(DOMSymbolInstancel);
                            //将elementsl作为DOMFramel的子节点
                            DOMFramel.PrependChild(elementsl);
                            //将DOMFramel作为framesl的子节点
                            framesl.PrependChild(DOMFramel);
                            //将framesl作为DOMLayerl的子节点
                            DOMLayerl.PrependChild(framesl);

                            XmlElement layers = (XmlElement)xmlDoc.GetElementsByTagName("layers")[0];
                            layers.PrependChild(DOMLayerl);
                            //保存xml
                            xmlDoc.Save(NextFile.FullName);
                        }
                        //判断为main元件并加密
                        if (NextFile.Name.Substring(0, 4) == "main" && mc == 1)
                        {
                            XmlElement layers = (XmlElement)xmlDoc.GetElementsByTagName("layers")[0];
                            layers.PrependChild(DOMLayer);
                            //保存xml
                            xmlDoc.Save(NextFile.FullName);
                        }
                        else { }
                    }
                    else { }
                }
                form1.textBox16.AppendText("加解密操作执行完成......" + "\r\n");
            }
            catch
            {
                MessageBox.Show("ClipEncrypt ERROR");
            }
        }
    }
}