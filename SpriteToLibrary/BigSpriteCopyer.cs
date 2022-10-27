using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace SpriteToLibrary
{
    //建立大文件夹图片复制类
    class BigSpriteCopyer
    {
        //生成大文件夹图片复制部分
        public void BigSpriteCopy(string Fpath, string Spath, ArrayList sarray)
        {
            try
            {
                //遍历以复制位图
                foreach (string sprite in sarray)
                {
                    //创建路径文件夹实例
                    DirectoryInfo TheFolder = new DirectoryInfo(Spath);
                    //数组记录各个位图文件信息
                    FileInfo[] sp = TheFolder.GetFiles(sprite, SearchOption.AllDirectories);
                    if (sp.Length != 0)
                    {
                        for (int i = 0; i < sp.Length; i++)
                        {
                            //得到图片路径
                            FileInfo fi = new FileInfo(Fpath + "\\" + "LIBRARY" + "\\" + sp[i].Name);
                            if (!fi.Exists)
                            {
                                //复制图片到LIBRARY
                                sp[i].CopyTo(Fpath + "\\" + "LIBRARY" + "\\" + sp[i].Name);
                            }
                            else { }
                        }
                    }
                    else { }
                }
            }
            catch
            {
                MessageBox.Show("BigSpriteCopy ERROR");
            }
        }
    }
}