using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace ClipTransformer
{
    //建立Library元件读取类
    class LibraryReader
    {
        //预置ca数组以记录元件名称
        public ArrayList ca = new ArrayList();
        //生成Library元件读取部分
        public void LibraryRead(string Fpath)
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
                        ca.Add(NextFile.Name.Substring(0, NextFile.Name.Length - 4));
                    }
                    else { }
                }
            }
            catch
            {
                MessageBox.Show("LibraryRead ERROR");
            }
        }
    }
}