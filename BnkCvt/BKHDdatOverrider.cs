using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using static EBToolBox.Form1;

namespace BnkCvt
{
    //建立BKHD.dat覆盖类
    class BKHDdatOverrider
    {
        //覆盖BKHD.dat
        public void BKHDdatOverride(string Dpath)
        {
            try
            {
                //得到预置的和exe同文件夹的BKHD.dat的路径
                FileInfo fi = new FileInfo(System.Environment.CurrentDirectory + "\\BnkCvt\\BKHD.dat");
                //判定根目录读取函数是否生效，否则换第二条函数
                if (!fi.Exists)
                {
                    //得到预置的和exe同文件夹的BKHD.dat的路径
                    fi = new FileInfo(AppDomain.CurrentDomain.BaseDirectory + "BnkCvt\\BKHD.dat");
                }
                //判断文件是否缺失
                if (fi.Exists)
                {
                    //删除错误的BKHD.dat
                    File.Delete(Dpath);
                    //添加预置的BKHD.dat
                    fi.CopyTo(Dpath);
                    //提示覆盖完成
                    form1.textBox19.AppendText("BKHDdatOverride Done" + "\r\n");
                }
                else MessageBox.Show("软件原有的BKHD.dat缺失！");
            }
            catch
            {
                MessageBox.Show("BKHDdatOverride ERROR");
            }
        }
    }
}