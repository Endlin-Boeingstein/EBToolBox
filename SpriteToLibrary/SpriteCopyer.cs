using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace SpriteToLibrary
{
    //建立位图复制类
    class SpriteCopyer
    {
        //生成位图复制部分
        public void SpriteCopy(string Fpath)
        {
            try
            {
                //创建路径文件夹实例
                DirectoryInfo TheFolder = new DirectoryInfo(Path.GetDirectoryName(Fpath));
                //遍历文件夹内文件
                foreach (FileInfo NextFile in TheFolder.GetFiles())
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
                        //得到图片路径
                        FileInfo fi = new FileInfo(Fpath + "\\" + "LIBRARY" + "\\" + NextFile.Name);
                        if (!fi.Exists)
                        {
                            //复制图片到LIBRARY
                            NextFile.CopyTo(Fpath + "\\" + "LIBRARY" + "\\" + NextFile.Name);
                        }
                        else { }
                    }
                    else { }
                }
            }
            catch
            {
                MessageBox.Show("SpriteCopy ERROR");
            }
        }
    }
}