using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DirectoryCreator
{
    //建立路径创建类
    class DirectoryCreator
    {
        //生成路径创建部分
        public void DirectoryCreate(string Fpath)
        {
            try
            {
                //得到预置文件夹的路径
                DirectoryInfo fi = new DirectoryInfo(System.Environment.CurrentDirectory + "\\DirectoryCreator\\images");
                //得到目标文件夹的路径
                DirectoryInfo fp = new DirectoryInfo(Fpath);
                //判定根目录读取函数是否生效，否则换第二条函数
                if (!fi.Exists)
                {
                    //得到预置文件夹的路径
                    fi = new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory + "DirectoryCreator\\images");
                }
                //判断文件是否缺失
                if (fi.Exists)
                {
                    //废弃代码，用了出错//备份fi
                    //废弃代码，用了出错///DirectoryInfo nfi = fi;
                    //废弃代码，用了出错///fi.MoveTo(Fpath);
                    //废弃代码，用了出错///nfi.MoveTo(AppDomain.CurrentDomain.BaseDirectory);
                    //创建fo实例
                    FileOperator fo = new FileOperator();
                    //复制路径
                    fo.CopyDirectory(fi.FullName, Fpath);
                    //多余提示//提示覆盖完成
                    //多余提示///MessageBox.Show("DirectoryCreate Done");
                }
                else MessageBox.Show("软件原有的images文件夹缺失！");
            }
            catch
            {
                MessageBox.Show("DirectoryCreate ERROR");
            }
        }

    }
}