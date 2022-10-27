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
    //建立wem大小计算类
    class SizeConverter
    {
        //定义size
        public long cvtsize;
        //定义十六进制化的size
        public string size16;
        //定义重组后的size
        public string size;
        //定义BIG-Endian化的size
        public string dd;
        //定义文件名称
        public string id;
        //定义Wem的id
        public string tid;
        //定义最终的size
        public string sd;
        //计算wem大小
        public void SizeConvert(string Fpath, string tid)
        {
            try
            {
                //创建路径文件夹实例
                DirectoryInfo TheFolder = new DirectoryInfo(Fpath);
                //将wem的id十进制化
                this.tid = Convert.ToInt32(tid, 16).ToString();
                //遍历文件夹内文件
                foreach (FileInfo NextFile in TheFolder.GetFiles())
                {
                    //给文件名称赋值
                    this.id = NextFile.Name;
                    //去掉文件名称后缀
                    this.id = id.Substring(0, id.Length - 4);
                    //判断传输来的Wem的id是否等于文件名
                    if (this.tid == id)
                    {
                        //给size赋值
                        this.cvtsize = NextFile.Length;
                        //将size十六进制化
                        this.size16 = Convert.ToString(cvtsize, 16).ToUpper();
                        //判断十六进制后的size是否存在首位不为0而导致成为单数位的情况，如是，则添加0
                        if (this.size16.Length % 2 != 0)
                        {
                            this.size16 = "0" + this.size16;
                        }
                        //分割重组size
                        for (int i = 0; i <= size16.Length - 1; i += 2)
                        {
                            //两个两个分割size
                            this.size += size16.Substring(i, 2);
                            if (i != size16.Length - 2)
                            {
                                this.size += " ";
                            }
                        }
                        //将size内容转换为字节数组
                        string[] sarray = size.Split(' ');
                        //BIG-Endian化
                        for (int i = sarray.Length - 1; i >= 0; i--)
                        {
                            this.dd += sarray[i];
                            if (i != 0)
                            {
                                this.dd += " ";
                            }
                        }
                        //判断是否为复数位的size，不是则添00
                        if ((sarray.Length) % 2 == 1)
                        {
                            this.dd += " 00";
                        }
                        this.sd = dd;
                        //清空sc类数值
                        //清空size
                        this.cvtsize = 0;
                        //清空十六进制化的size
                        this.size16 = null;
                        //清空重组后的size
                        this.size = null;
                        //清空BIG-Endian化的size
                        this.dd = null;
                        //跳出文件检索循环
                        break;
                    }
                    else
                    {
                        //清空size
                        this.cvtsize = 0;
                        //清空十六进制化的size
                        this.size16 = null;
                        //清空重组后的size
                        this.size = null;
                        //清空BIG-Endian化的size
                        this.dd = null;
                    }
                }
                //提示计算完成
                form1.textBox19.AppendText("SizeConvert Done" + "\r\n");
            }
            catch
            {
                MessageBox.Show("SizeConvert ERROR");
            }
        }
    }
}