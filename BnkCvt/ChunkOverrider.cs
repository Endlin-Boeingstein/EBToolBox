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
    //建立BKHD重写类
    class ChunkOverrider
    {
        //重写chunk.json
        public void ChunkOverride(string Jpath)
        {
            try
            {
                //读取chunk.json
                string json = File.ReadAllText(Jpath);
                //将读取文本转换为JArray对象
                JArray rss = JArray.Parse(json);
                //在JArray数组中添加STID
                rss.Add("STID");
                //JSON数据字符串化
                string output = Newtonsoft.Json.JsonConvert.SerializeObject(rss, Newtonsoft.Json.Formatting.Indented);
                //输出文本
                File.WriteAllText(Jpath, output);
                //提示重写完成
                form1.textBox19.AppendText("ChunkOverride Done" + "\r\n");
            }
            catch
            {
                MessageBox.Show("ChunkOverride ERROR");
            }
        }
    }
}