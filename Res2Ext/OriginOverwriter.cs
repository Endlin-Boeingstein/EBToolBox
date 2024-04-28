using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using static EBToolBox.Form1;

namespace Res2Ext
{
    //建立舞台重写类
    class OriginOverwriter
    {
        //建立origin的JSON对象
        public JObject Origin = new JObject();
        //生成舞台重写部分
        public void OriginOverwrite(string EPath, JObject origin)
        {
            try
            {
                //判定extra.json是否存在，若存在，则执行重写操作
                if (!File.Exists(EPath))
                {
                    form1.textBox16.AppendText("未检测到xfl文件夹内存在旧版extra.json，将执行默认操作..." + "\r\n");
                }
                else
                {
                    //读取文本
                    string json = File.ReadAllText(EPath);
                    //新功能更新而停用//询问是否参考旧版extra.json重写舞台大小
                    //新功能更新而停用///Console.WriteLine("舞台大小部分是否参考旧版extra.json?否则输入n或0并按回车键（不输入按回车默认执行参考操作）");
                    //新功能更新而停用//选项收录
                    //新功能更新而停用///string sorigin = Console.ReadLine();
                    //新功能更新而停用//判定是否直接回车，输入它值或直接回车则执行
                    if (form1.radioButton25.Checked && !form1.radioButton24.Checked)
                    {
                        //将读取文本转换为JSON对象
                        JObject rss = JObject.Parse(json);
                        //将旧舞台信息 Object化
                        JObject OldOrigin = new JObject();
                        //提取旧舞台信息的数组
                        JArray jao = JArray.Parse(rss["origin"].ToString());
                        //添加至OldOrigin
                        OldOrigin.Add(new JProperty("origin", jao));
                        //载入旧版origin
                        origin = OldOrigin;
                    }
                    else { }
                }
                //为上传类赋值
                Origin = origin;
            }
            catch
            {
                MessageBox.Show("OriginOverwrite ERROR");
            }
        }
    }
}