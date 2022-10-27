using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace ResSC
{
    //建立Res合成类
    public class ResCrafter
    {
        //创建ps实例
        public Preset ps = new Preset();
        //合成resource.json
        public void ResCraft(string Fpath)
        {
            try
            {
                //新功能更新而停用//读取resource.json头（A.json）
                //新功能更新而停用///string res = File.ReadAllText(Fpath+"/A.json");
                //新功能更新而停用//将resource.json头（A.json）转换为JSON对象
                //新功能更新而停用///JObject rss = JObject.Parse(res);
                //创建路径文件夹resource.dir实例
                DirectoryInfo TheFolder = new DirectoryInfo(Fpath);
                //建立json数组
                JArray aa = new JArray();
                //定义slot
                long slot = 0;
                //遍历文件夹内文件
                foreach (FileInfo NextFile in TheFolder.GetFiles())
                {

                    //读取文件夹内文本
                    string json = File.ReadAllText(Fpath + "/" + NextFile.Name);
                    //将读取文本转换为json对象
                    JObject group = JObject.Parse(json);
                    //将type结果转为字符串
                    string type = group["type"].ToString();
                    //判断是否需要重载slot
                    if (type != "composite")
                    {
                        JArray resources = JArray.Parse(group["resources"].ToString());
                        foreach (var item in resources)
                        {
                            item["slot"] = slot;
                            slot++;
                        }
                        //给resources数组赋值
                        group["resources"] = resources;
                    }
                    //将内容放进json数组
                    aa.Add(group);
                }
                //赋值slot_count为转译后的slot数
                ps.slot_count = slot;
                //将预置类序列化
                string output = JsonConvert.SerializeObject(ps);
                //将序列化的预置类 Object化
                JObject rss = JObject.Parse(output);
                //在slot_count后增加groups数组
                rss.Property("slot_count").AddAfterSelf(new JProperty("groups", aa));
                //json数据字符串化
                output = Newtonsoft.Json.JsonConvert.SerializeObject(rss, Newtonsoft.Json.Formatting.Indented);
                //输出文本
                File.WriteAllText(Path.GetDirectoryName(Fpath) + "/resources.json", output);
                //提示合成完成
                MessageBox.Show("ResCraft Done");
            }
            catch
            {
                MessageBox.Show("ResCraft ERROR");
            }

        }
    }
}