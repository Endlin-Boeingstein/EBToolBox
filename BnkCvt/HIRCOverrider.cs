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
    //建立HIRC重写类
    class HIRCOverrider
    {
        //创建sc实例
        public SizeConverter sc = new SizeConverter();
        //重写HIRC
        public void HIRCOverride(string Jpath, string Wpath)
        {
            try
            {
                //读取文本
                string json = File.ReadAllText(Jpath);
                //将读取文本转换为json数组
                JArray rss = JArray.Parse(json);
                //bnk的id定义
                string pre = null;
                //先读取json数组以获得bnk的id
                foreach (JObject hirc in rss)
                {
                    //将type转为int
                    int type = int.Parse(((JObject)hirc)["type"].ToString());
                    //先遍历一遍找出播放事件动作的最后四位
                    if (type == 3)
                    {
                        //定义分段的字节字符串
                        string a1 = null, b1 = null, c1 = null, d1 = null;
                        //将data内容转换为字节数组
                        string[] btarray1 = hirc["data"].ToString().Split(' ');
                        //事件动作标识符
                        a1 = btarray1[0];
                        //动作代号
                        b1 = btarray1[1];
                        //不能修改的data部分
                        c1 += btarray1[2] + " " + btarray1[3] + " " + btarray1[4] + " " + btarray1[5] + " " + btarray1[6] + " " + btarray1[7] + " " + btarray1[8] + " " + btarray1[9];
                        //bnk的id
                        for (int i = 10; i < btarray1.Length; i++)
                        {
                            d1 += btarray1[i];
                            if (i != btarray1.Length - 1)
                            {
                                d1 += " ";
                            }
                        }
                        //判断是否为播放事件，以获得bnk的id
                        if (b1 == "04")
                        {
                            pre = d1;
                        }
                        //判断是否为停止事件，以修改事件动作的最后四位
                        if (b1 == "01")
                        {
                            d1 = "00 00 00 00";
                            hirc["data"] = a1 + " " + b1 + " " + c1 + " " + d1;
                        }
                    }
                    if (type == 4)
                    {
                        //定义分段的字节字符串
                        string a2 = null, b2 = null, c2 = null;
                        //将data内容转换为字节数组
                        string[] btarray2 = hirc["data"].ToString().Split(' ');
                        //事件动作标识符
                        a2 = btarray2[0];
                        //占位符
                        b2 = "00 00 00";
                        //事件字节第三部分
                        for (int i = 1; i < btarray2.Length; i++)
                        {
                            c2 += btarray2[i];
                            if (i != btarray2.Length - 1)
                            {
                                c2 += " ";
                            }
                        }
                        //重写data
                        hirc["data"] = a2 + " " + b2 + " " + c2;
                    }
                }
                //遍历读取json数组并修改
                foreach (JObject hirc in rss)
                {
                    //将type转为int
                    int type = int.Parse(((JObject)hirc)["type"].ToString());
                    //判断type是否为2
                    if (type == 2)
                    {
                        //新功能更新而停用//判断下一个的type以将事件动作data的最后四位覆盖SFX的data的无效代码
                        //新功能更新而停用///JObject hirc1 = (JObject)rss[item.Next.ToString()];
                        //新功能更新而停用//判断下一个是否为对应事件动作
                        //新功能更新而停用///if (int.Parse(((JObject)hirc1)["type"].ToString()) == 3)
                        //新功能更新而停用///{ }
                        //六段有效字节定义以及引用的wem名称定义
                        string aa = null, bb = null, cc = null, dd = null, ee = null, ff = null, tid = null;
                        //将data内容转换为字节数组
                        string[] btarray = hirc["data"].ToString().Split(' ');
                        //不能修改的data部分
                        aa += btarray[0] + " " + btarray[1] + " " + btarray[2] + " " + btarray[3] + " " + btarray[4] + " " + btarray[5] + " " + btarray[6] + " " + btarray[7] + " " + btarray[8];
                        //引用音乐的id
                        tid = btarray[8] + btarray[7] + btarray[6] + btarray[5];
                        //bnk的id
                        bb += pre;
                        //固定值
                        cc += "40 00 00 00";
                        //占位符
                        dd += "AA BB CC 00";
                        //占位符
                        ee += "00 00 00 00";
                        //固定值
                        ff += "37 BC B7 E2 00 00 00 00 00 01 3A 00 00 00 00 00 C3 00 00 01 00 00 00 00 00 00 00 00 00 00";
                        //计算wem的size
                        sc.SizeConvert(Wpath, tid);
                        //给size赋值
                        dd = sc.sd;
                        //清空sc类的size防止重复写值
                        sc.sd = null;
                        //重写data
                        hirc["data"] = aa + " " + bb + " " + cc + " " + dd + " " + ee + " " + ff;
                    }
                }
                //JSON数据字符串化
                string output = Newtonsoft.Json.JsonConvert.SerializeObject(rss, Newtonsoft.Json.Formatting.Indented);
                //输出文本
                File.WriteAllText(Jpath, output);
                //提示重写完成
                form1.textBox19.AppendText("HIRCOverride Done" + "\r\n");
            }
            catch
            {
                MessageBox.Show("HIRCOverride ERROR");
            }
        }
    }
}