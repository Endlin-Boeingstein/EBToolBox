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
    //建立位图读取类
    class SpriteReader
    {
        //定义位图记录数组
        public ArrayList srecord = new ArrayList();
        //定义引用图片文件名称
        public string pid = null;
        //生成位图读取部分
        public void SpriteRead(string Fpath)
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
                    //判定是否为png
                    if (fileclass == "13780")
                    {
                        //录入图片名称
                        pid = NextFile.Name.Substring(0, NextFile.Name.Length - 4);
                        //记录位图名称
                        srecord.Add(pid);
                    }
                    else { }
                }
            }
            catch
            {
                MessageBox.Show("SpriteRead ERROR");
            }
        }
    }
}