using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace SpriteToLibrary
{
    //建立库元件读取修复类//2026年5月27日建立
    class LibraryClipReader
    {
        //预置位图引用数组
        public ArrayList sarray = new ArrayList();
        //生成库元件读取修复部分
        public void LibraryClipRead(string Fpath)
        {
            try
            {
                //创建路径文件夹实例
                DirectoryInfo TheFolder = new DirectoryInfo(Fpath);
                //创建文件数组
                FileInfo[] files = TheFolder.GetFiles();
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
                        //检测是否为引用位图元件，并对其二级路径进行修复（无论该元件是否引用符合规范）
                        if (xmlDoc.GetElementsByTagName("DOMBitmapInstance").Count >= 1)
                        {
                            //读取DOMBitmapInstance节点，录入位图引用信息以及修复二级路径
                            foreach (XmlElement el in xmlDoc.GetElementsByTagName("DOMBitmapInstance"))
                            {
                                //跳过为空的有问题的引用
                                if (el.GetAttribute("libraryItemName") == "" || el.GetAttribute("libraryItemName") == null || el == null) { }
                                else
                                {
                                    //防止二级路径
                                    string lin = el.GetAttribute("libraryItemName");
                                    //防止二级路径//添加删除.png后缀功能
                                    lin = lin.Substring(lin.LastIndexOf('/') + 1, lin.Length - lin.LastIndexOf('/') - 1).Replace(".png", "");
                                    //防止add$
                                    lin = lin.Substring(lin.LastIndexOf('$') + 1, lin.Length - lin.LastIndexOf('$') - 1).Replace(".png", "");
                                    //修复二级路径
                                    el.SetAttribute("libraryItemName", lin);
                                    if (!sarray.Contains(lin))
                                    {
                                        //记录引用信息
                                        sarray.Add(lin);
                                    }
                                    else { }
                                }
                            }
                            //保存xml
                            xmlDoc.Save(NextFile.FullName);
                        }
                        else { }
                    }
                    else { }
                }
            }
            catch
            {
                Console.WriteLine("LibraryClipRead ERROR");
                //提示按任意键继续
                Console.WriteLine("Press any key to continue...");
                //输入任意键退出
                Console.ReadLine();
            }
        }
    }
}