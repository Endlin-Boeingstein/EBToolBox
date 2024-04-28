using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace LevelSC
{
    //建立Level切割类
    class LevelSpliter
    {
        //取得字符串中间值
        public static string MidStrEx(string sourse, string startstr, string endstr)
        {
            string result = string.Empty;
            int startindex, endindex;
            try
            {
                startindex = sourse.IndexOf(startstr);
                if (startindex == -1)
                    return result;
                string tmpstr = sourse.Substring(startindex + startstr.Length);
                endindex = tmpstr.IndexOf(endstr);
                if (endindex == -1)
                    return result;
                result = tmpstr.Remove(endindex);
            }
            catch //(Exception ex)
            {
                //Log.WriteLog("MidStrEx Err:" + ex.Message);
                MessageBox.Show("MidStrEx ERROR");
            }
            return result;
        }
        //新功能更新而停用//创建ps实例
        //新功能更新而停用///public Preset ps = new Preset();
        //切割resource.json
        public void LevelSplit(string Jpath)
        {
            try
            {
                //读取文本
                string json = File.ReadAllText(Jpath);
                //将读取文本转换为JSON对象
                JObject rss = JObject.Parse(json);
                //新功能更新而停用//提取slot
                //新功能更新而停用///long sc = long.Parse(rss["slot_count"].ToString());
                //新功能更新而停用//将slot值覆盖预置resource.json头的slot
                //新功能更新而停用///ps.slot_count = sc;
                //得到level.json的文件夹路径（不包括json的文件名）
                string activeDir = Path.GetDirectoryName(Jpath);
                //在原有路径基础上添加路径字条
                string newPath = System.IO.Path.Combine(activeDir, Path.GetFileNameWithoutExtension(Jpath) + ".dir");
                //创建新文件夹level.dir
                System.IO.Directory.CreateDirectory(newPath);
                //创建新文件夹Waves
                System.IO.Directory.CreateDirectory(newPath + "//Waves");
                //新功能更新而停用//分割resource.json头（A.json）至resource.dir
                //新功能更新而停用///File.WriteAllText(Path.GetDirectoryName(Jpath) + "/resource.dir/A.json", JsonConvert.SerializeObject(ps, Formatting.Indented));
                ///JObject groups = (JObject)rss["groups"];废弃代码，用了出错
                //objects转json数组
                JArray Oja = JArray.Parse(rss["objects"].ToString());
                //分解出#comment
                //废弃代码///string Comment = rss["#comment"].ToString();
                //无用代码，废弃//创建数组jac
                //无用代码，废弃///JArray jac = new JArray();
                //无用代码，废弃//加入comment
                //无用代码，废弃///jac.Add(rss.First);
                //创建comment的JObject对象
                JObject joc = new JObject();
                joc.Add(rss.Property("#comment"));
                //输出#comment.json文件
                File.WriteAllText(Path.GetDirectoryName(Jpath) + "\\" + Path.GetFileNameWithoutExtension(Jpath) + ".dir" + "\\" + "#comment" + ".json", JsonConvert.SerializeObject(joc, Formatting.Indented));
                //创建version的JObject对象
                JObject jov = new JObject();
                jov.Add(rss.Property("version"));
                //输出version.json文件
                File.WriteAllText(Path.GetDirectoryName(Jpath) + "\\" + Path.GetFileNameWithoutExtension(Jpath) + ".dir" + "\\" + "version" + ".json", JsonConvert.SerializeObject(jov, Formatting.Indented));
                //遍历输出分割后的level.json
                foreach (var item in Oja)
                {
                    string aliases = null;
                    if (((JObject)item).ToString().Contains("aliases"))
                        aliases = ((JObject)item)["aliases"].First.ToString();
                    else aliases = "__LevelDefinition__";
                    //测试用参数
                    string test = Path.GetDirectoryName(Jpath) + "\\" + Path.GetFileNameWithoutExtension(Jpath) + ".dir" + "\\" + aliases + ".json";
                    //输出碎片
                    //新功能更新弃用-->
                    //if(Regex.IsMatch(aliases, @"^Wave.*") && aliases!= "WaveManagerProps")
                    //    File.WriteAllText(Path.GetDirectoryName(Jpath) + "\\" + Path.GetFileNameWithoutExtension(Jpath) + ".dir" + "\\Waves" +"\\" + aliases + ".json", JsonConvert.SerializeObject(item, Formatting.Indented));
                    //else 
                    //    File.WriteAllText(Path.GetDirectoryName(Jpath) + "\\" + Path.GetFileNameWithoutExtension(Jpath) + ".dir" + "\\" + aliases + ".json", JsonConvert.SerializeObject(item, Formatting.Indented));
                    //<--新功能更新弃用
                    //新功能添加
                    File.WriteAllText(Path.GetDirectoryName(Jpath) + "\\" + Path.GetFileNameWithoutExtension(Jpath) + ".dir" + "\\" + aliases + ".json", JsonConvert.SerializeObject(item, Formatting.Indented));
                }
                //新功能添加
                //读取wmp文本
                string wmp = File.ReadAllText(newPath + "/" + "WaveManagerProps.json");
                //将读取文本转换为JSON对象
                JObject jwmp = JObject.Parse(wmp);
                //Waves转json数组
                JArray Wja = JArray.Parse(jwmp["objdata"]["Waves"].ToString());
                //波数计数器
                int wnum = 1;
                //遍历数组
                foreach (var item in Wja)
                {
                    //创建新文件夹Waves\num
                    System.IO.Directory.CreateDirectory(newPath + "//Waves" + "//" + wnum);
                    foreach (var witem in item)
                    {
                        //记录事件名字
                        string wname = MidStrEx(witem.ToString(), "RTID(", "@");
                        //记录旧事件路径
                        string wopath = newPath + "//" + wname + ".json";
                        //记录新事件路径
                        string wpath = newPath + "//Waves" + "//" + wnum + "//" + wname + ".json";
                        //创建文件对象
                        FileInfo winfo = new FileInfo(wopath);
                        //复制文件
                        winfo.CopyTo(wpath);
                    }
                    wnum++;
                }
                //遍历删除残留文件
                foreach (var item in Wja)
                {
                    foreach (var witem in item)
                    {
                        //记录事件名字
                        string wname = MidStrEx(witem.ToString(), "RTID(", "@");
                        //记录旧事件路径
                        string wopath = newPath + "//" + wname + ".json";
                        //创建文件对象
                        FileInfo winfo = new FileInfo(wopath);
                        //防止重复删除出bug
                        if (winfo.Exists)
                        {
                            //删除文件
                            winfo.Delete();
                        }
                        else { }
                    }
                }
                //提示分解完成
                MessageBox.Show("LevelSplit Done");
            }
            catch
            {
                MessageBox.Show("LevelSplit ERROR");
            }
        }
    }
}