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
    //建立Res切割类
    public class ResSpliter
    {
        //新功能更新而停用//创建ps实例
        //新功能更新而停用///public Preset ps = new Preset();
        //切割resource.json
        public void ResSplit(string Jpath)
        {
            try
            {
                //读取文本
                string json = File.ReadAllText(Jpath);
                //避免误打
                json = json.Replace(@"\\", "双");
                //中文版不规范修复
                json = json.Replace(@"\x", "十六进制转换");
                json = json.Replace(@"..\\..\\resprops\\rgtemp\\all", "中文临时路径");
                //将读取文本转换为JSON对象
                JObject rss = JObject.Parse(json);
                //新功能更新而停用//提取slot
                //新功能更新而停用///long sc = long.Parse(rss["slot_count"].ToString());
                //新功能更新而停用//将slot值覆盖预置resource.json头的slot
                //新功能更新而停用///ps.slot_count = sc;
                //得到resource.json的文件夹路径（不包括json的文件名）
                string activeDir = Path.GetDirectoryName(Jpath);
                //在原有路径基础上添加路径字条
                string newPath = System.IO.Path.Combine(activeDir, "resources.dir");
                //创建新文件夹resources.dir
                System.IO.Directory.CreateDirectory(newPath);
                //新功能更新而停用//分割resource.json头（A.json）至resource.dir
                //新功能更新而停用///File.WriteAllText(Path.GetDirectoryName(Jpath) + "/resource.dir/A.json", JsonConvert.SerializeObject(ps, Formatting.Indented));
                ///JObject groups = (JObject)rss["groups"];废弃代码，用了出错
                //groups转json数组
                JArray Gja = JArray.Parse(rss["groups"].ToString());
                //遍历输出分割后的resource.json
                foreach (var item in Gja)
                {
                    //记录文件名称
                    string id = ((JObject)item)["id"].ToString();
                    //功能更新而弃用///if (json.Contains("双") && !json.Contains("十六进制转换"))
                    //功能更新而弃用///{
                    //功能更新而弃用///}
                    //功能更新而弃用///else { }
                    if (((JObject)item)["type"].ToString() == "simple")
                    {
                        //resources转JArray数组
                        JArray resources = JArray.Parse(item["resources"].ToString());
                        foreach (var im in resources)
                        {
                            if (im["path"].ToString().Contains("双"))
                            {
                                //从路径提取文件夹名
                                string[] imagename = im["path"].ToString().Split('双');
                                //转换为List
                                List<String> imagenamelist = new List<String>(imagename);
                                //修改path
                                im["path"] = (JToken)JsonConvert.DeserializeObject(JsonConvert.SerializeObject(imagenamelist));
                            }
                            else if (!(im["path"].ToString().Contains("[") && im["path"].ToString().Contains("]")))
                            {
                                JArray sgpath = new JArray();
                                sgpath.Add(im["path"]);
                                im["path"] = sgpath;
                            }
                            else { }
                            //判定srcpath是否存在
                            if (im["srcpath"] != null)
                            {
                                if (im["srcpath"].ToString().Contains("双"))
                                {
                                    //从路径提取文件夹名
                                    string[] srcname = im["srcpath"].ToString().Split('双');
                                    //转换为List
                                    List<String> srcnamelist = new List<String>(srcname);
                                    //修改srcpath
                                    im["srcpath"] = (JToken)JsonConvert.DeserializeObject(JsonConvert.SerializeObject(srcnamelist));
                                }
                                else if (!(im["srcpath"].ToString().Contains("[") && im["srcpath"].ToString().Contains("]")))
                                {
                                    JArray sgpath = new JArray();
                                    sgpath.Add(im["srcpath"]);
                                    im["srcpath"] = sgpath;
                                }
                                else { }
                            }
                            else { }
                        }
                        item["resources"] = resources;
                    }
                    else { }
                    File.WriteAllText(Path.GetDirectoryName(Jpath) + "/resources.dir/" + id + ".json", JsonConvert.SerializeObject(item, Formatting.Indented).Replace("双", @"\\").Replace("十六进制转换", @"\x"));
                }
                //提示分解完成
                MessageBox.Show("ResSplite Done");
            }
            catch
            {
                MessageBox.Show("ResSplite ERROR");
            }
        }
    }
}