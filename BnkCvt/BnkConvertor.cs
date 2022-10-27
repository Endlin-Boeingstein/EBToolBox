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
    //建立Bnk转换类
    class BnkConvertor
    {
        //创建bdo实例
        public BKHDdatOverrider bdo = new BKHDdatOverrider();
        //创建bo实例
        public BKHDOverrider bo = new BKHDOverrider();
        //创建co实例
        public ChunkOverrider co = new ChunkOverrider();
        //创建sa实例
        public STIDAdded sa = new STIDAdded();
        //创建ho实例
        public HIRCOverrider ho = new HIRCOverrider();
        //转换Bnk
        public void BnkCvt(string Fpath)
        {
            try
            {
                //新功能更新而停用//读取resource.json头（A.json）
                //新功能更新而停用///string res = File.ReadAllText(Fpath+"/A.json");
                //新功能更新而停用//将resource.json头（A.json）转换为JSON对象
                //新功能更新而停用///JObject rss = JObject.Parse(res);
                //创建路径文件夹bnk.dir实例
                DirectoryInfo TheFolder = new DirectoryInfo(Fpath);
                //BKHD.dat替换
                bdo.BKHDdatOverride(Fpath + "\\BKHD.dat");
                //BKHD.json重写
                bo.BKHDOverride(Fpath + "\\BKHD.json");
                //chunk.json重写
                co.ChunkOverride(Fpath + "\\chunk.json");
                //STID.json添加
                sa.STIDAdd(Fpath);
                //HIRC.json重写
                ho.HIRCOverride(Fpath + "\\HIRC.json", Fpath + "\\DATA");
                //提示转换完成
                form1.textBox19.AppendText("BnkConvert Done" + "\r\n");
            }
            catch
            {
                MessageBox.Show("BnkConvert ERROR");
            }
        }
    }
}