using EBToolBox;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using static EBToolBox.Form1;

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
                //判定是否转新版res
                long trans;
                //因UI化移除///Console.WriteLine("是否合成结果是旧版res,是输入y或者1，否输入n或者0，并按一次回车键（不输入按回车默认为是）");
                //因UI化移除//获取输入字符串
                //因UI化移除///string strtrans = System.Console.ReadLine();
                if (form1.radioButton28.Checked && !form1.radioButton29.Checked)
                    trans = 1;
                else
                    trans = 0;


                //判定是否结构破坏20240331添加
                long encrypt;
                Console.WriteLine("是否进行结构破坏性合成？是输入y或者1，否输入n或者0，并按一次回车键（不输入按回车默认为否）");
                //获取输入字符串//20240331添加
                string strencrypt = System.Console.ReadLine();
                if (form1.radioButton34.Checked && !form1.radioButton35.Checked)
                    encrypt = 1;
                else
                    encrypt = 0;


                //遍历文件夹内文件
                foreach (FileInfo NextFile in TheFolder.GetFiles())
                {

                    //读取文件夹内文本
                    string json = File.ReadAllText(Fpath + "/" + NextFile.Name);
                    //中文版不规范修复
                    json = json.Replace(@"\x", "十六进制转换");
                    //将读取文本转换为json对象
                    JObject group = JObject.Parse(json);
                    //将type结果转为字符串
                    string type = group["type"].ToString();
                    //判断是否需要重载slot
                    if (type != "composite")
                    {
                        //根据选项将id转换为parent的id//20240331添加
                        if (encrypt == 1)
                        {
                            if (group["parent"] != null)
                            {
                                //将parent赋值给id
                                group["id"] = group["parent"];
                            }
                            else { }
                        }
                        else { }


                        JArray resources = JArray.Parse(group["resources"].ToString());
                        foreach (var item in resources)
                        {
                            item["slot"] = slot;
                            slot++;
                            //判断是否需要转为新版
                            if (trans == 1)
                            {
                                string wpath = null;
                                JArray ipath = JArray.Parse(item["path"].ToString());
                                for (int i = 0; i < ipath.Count; i++)
                                {
                                    if (i != ipath.Count - 1) wpath += ipath[i] + @"\";
                                    else wpath += ipath[i];
                                }
                                item["path"] = (JToken)wpath;
                                //判定srcpath是否存在
                                if (item["srcpath"] != null)
                                {
                                    string wspath = null;
                                    JArray spath = JArray.Parse(item["srcpath"].ToString());
                                    for (int i = 0; i < spath.Count; i++)
                                    {
                                        if (i != spath.Count - 1) wspath += spath[i] + @"\";
                                        else wspath += spath[i];
                                    }
                                    //修改srcpath
                                    item["srcpath"] = (JToken)wspath;
                                }
                                else { }
                            }
                            else { }
                        }
                        //给resources数组赋值
                        group["resources"] = resources;
                    }
                    //新增关于结构的破坏//20240331新增
                    else
                    {
                        if (encrypt == 1)
                        {
                            //将type结果转为字符串
                            string compid = group["id"].ToString();
                            //读取subgroups
                            JArray subgroups = JArray.Parse(group["subgroups"].ToString());
                            //遍历subgroups以修改id
                            foreach (JObject subgroup in subgroups)
                            {
                                subgroup["id"] = compid;
                            }
                            //覆盖原subgroups
                            group["subgroups"] = subgroups;
                        }
                        else { }
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
                //json中文版不规范修复
                output = output.Replace("十六进制转换", @"\x");
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