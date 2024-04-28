using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Policy;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace LevelSC
{
    //建立Res合成类
    class LevelCrafter
    {
        //合成level.json
        public void LevelCraft(string Fpath)
        {
            try
            {
                //取得字符串中间值
                string MidStrEx(string sourse, string startstr, string endstr)
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
                //新功能更新而停用//读取resource.json头（A.json）
                //新功能更新而停用///string res = File.ReadAllText(Fpath+"/A.json");
                //新功能更新而停用//将resource.json头（A.json）转换为JSON对象
                //新功能更新而停用///JObject rss = JObject.Parse(res);
                //创建路径文件夹level.dir实例
                DirectoryInfo TheFolder = new DirectoryInfo(Fpath);
                //建立json数组
                JArray aa = new JArray();
                //创建Waves文件夹实例
                DirectoryInfo Waves = new DirectoryInfo(Fpath + "/Waves");
                //获得排序后的子文件夹信息
                //创建文件数组
                DirectoryInfo[] WaveF = Waves.GetDirectories();
                //为文件数组排序
                Array.Sort(WaveF, new FileNameSort());
                //读取WaveManagerProps文本
                string wmp = File.ReadAllText(Fpath + "/" + "WaveManagerProps.json");
                //建立WaveManagerProps对象
                JObject jwmp = JObject.Parse(wmp);
                //建立数组wm
                JArray wm = new JArray();
                //遍历文件数组读取文件
                foreach (DirectoryInfo Wave in WaveF)
                {
                    //创建文件数组
                    FileInfo[] wavefiles = Wave.GetFiles();
                    //为文件数组排序
                    Array.Sort(wavefiles, new FileNameSort());
                    //建立数组wz
                    JArray wz = new JArray();
                    //遍历Waves文件夹以修改WaveManagerProps
                    foreach (FileInfo NextFile in wavefiles)
                    {
                        //废弃语句，用了出错//删除.json字符串
                        //废弃语句，用了出错///char[] jstr = { '.', 'j', 's', 'o', 'n' };
                        //事件名称
                        string th = "RTID(" + NextFile.Name.Replace(".json", "") + "@CurrentLevel)";
                        //添加进子数组
                        wz.Add(th);
                    }
                    //添加进Waves数组
                    wm.Add(wz);
                }
                //修改原来Waves值
                jwmp["objdata"]["Waves"] = wm;
                //json数据字符串化
                string outputwmp = Newtonsoft.Json.JsonConvert.SerializeObject(jwmp, Newtonsoft.Json.Formatting.Indented);
                //输出文本
                File.WriteAllText(Fpath + "/WaveManagerProps.json", outputwmp);
                ////创建文件数组
                //FileInfo[] wavefiles = Waves.GetFiles();
                ////为文件数组排序
                //Array.Sort(wavefiles, new FileNameSort());
                //旧版wmp重写方法-->
                ////读取WaveManagerProps文本
                //string wmp = File.ReadAllText(Fpath + "/" + "WaveManagerProps.json");
                ////建立WaveManagerProps对象
                //JObject jwmp = JObject.Parse(wmp);
                ////建立数组wm
                //JArray wm = new JArray();
                ////遍历Waves文件夹以修改WaveManagerProps
                //foreach (FileInfo NextFile in wavefiles)
                //{
                //    //删除.json字符串
                //    char[] jstr = { '.', 'j', 's', 'o', 'n' };

                //    if (Regex.IsMatch(NextFile.Name, @"^Wave\d*\.json$"))
                //    {
                //        //建立数组wz
                //        JArray wz = new JArray();
                //        //取前位以搜索
                //        string wname = NextFile.Name.Substring(0, NextFile.Name.IndexOf("."));
                //        foreach (FileInfo nextfile in wavefiles)
                //        {
                //            //记录去头字符串
                //            string tnf = nextfile.Name.Trim(wname.ToCharArray());
                //            string test = nextfile.Name.Substring(0, NextFile.Name.IndexOf("."));
                //            if (nextfile.Name.Substring(0, NextFile.Name.IndexOf(".")) == wname)
                //            {
                //                if(Regex.IsMatch(tnf.Substring(0,1), @"^\d*$")) { }
                //                else
                //                {
                //                    //事件名称
                //                    string th = "RTID(" + nextfile.Name.TrimEnd(jstr) + "@CurrentLevel)";
                //                    //废弃代码，用了报错//将读取文本转换为json对象
                //                    //废弃代码，用了报错///JObject wobject = JObject.Parse(th);
                //                    //添加进子数组
                //                    wz.Add(th);
                //                }
                //            }
                //            else { }
                //        }
                //        //添加进Waves数组
                //        wm.Add(wz);
                //    }
                //    else { }
                //}
                ////修改原来Waves值
                //jwmp["objdata"]["Waves"] = wm;
                ////json数据字符串化
                //string outputwmp = Newtonsoft.Json.JsonConvert.SerializeObject(jwmp, Newtonsoft.Json.Formatting.Indented);
                ////输出文本
                //File.WriteAllText(Fpath + "/WaveManagerProps.json", outputwmp);
                //旧版wmp重写方法<--
                //读取WaveManagerProps文本
                string nwmp = File.ReadAllText(Fpath + "/" + "WaveManagerProps.json");
                //建立WaveManagerProps对象
                JObject njwmp = JObject.Parse(nwmp);
                //Waves转json数组
                JArray Wja = JArray.Parse(njwmp["objdata"]["Waves"].ToString());
                //遍历数组
                foreach (var item in Wja)
                {
                    foreach (var witem in item)
                    {
                        //记录事件名字
                        string wname = MidStrEx(witem.ToString(), "RTID(", "@");
                        //记录旧事件路径
                        string wopath = Fpath + "//" + wname + ".json";
                        //创建文件对象
                        FileInfo owinfo = new FileInfo(wopath);
                        if (!owinfo.Exists)
                        {
                            //Waves路径对象
                            DirectoryInfo WavesFolder = new DirectoryInfo(Fpath + "//Waves");
                            //数组记录各个位图文件信息
                            FileInfo[] sp = WavesFolder.GetFiles(wname + ".json", SearchOption.AllDirectories);
                            //复制文件
                            sp[0].CopyTo(wopath);
                        }
                        else { }
                    }
                }
                //创建文件数组
                FileInfo[] levelfiles = TheFolder.GetFiles();
                //为文件数组排序
                Array.Sort(levelfiles, new FileNameSort());
                //遍历文件夹内文件
                foreach (FileInfo NextFile in levelfiles)
                {
                    if (NextFile.Name != "#comment.json" || NextFile.Name != "version.json")
                    {
                        //读取文件夹内文本
                        string json = File.ReadAllText(Fpath + "/" + NextFile.Name);
                        //将读取文本转换为json对象
                        JObject lobject = JObject.Parse(json);
                        //将内容放进json数组
                        aa.Add(lobject);
                    }
                    else { }
                }
                //遍历删除残留文件
                foreach (var item in Wja)
                {
                    foreach (var witem in item)
                    {
                        //记录事件名字
                        string wname = MidStrEx(witem.ToString(), "RTID(", "@");
                        //记录旧事件路径
                        string wopath = Fpath + "//" + wname + ".json";
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
                //旧版wmp重写方法-->
                //foreach (FileInfo NextFile in wavefiles)
                //{
                //    //读取文件夹内文本
                //    string json = File.ReadAllText(Fpath + "/" +"Waves/"+ NextFile.Name);
                //    //将读取文本转换为json对象
                //    JObject lobject = JObject.Parse(json);
                //    //将内容放进json数组
                //    aa.Add(lobject);
                //}
                //旧版wmp重写方法<--
                //读取comment文本
                string jcomment = File.ReadAllText(Fpath + "/" + "#comment.json");
                //将读取文本转换为json对象
                JObject rss = JObject.Parse(jcomment);
                //读取version文本
                string jversion = File.ReadAllText(Fpath + "/" + "version.json");
                //将读取文本转换为json对象
                JObject ver = JObject.Parse(jversion);
                //废弃代码，用了报错//将读取文本转换为json对象
                //废弃代码，用了报错///JObject cobject = JObject.Parse(jcomment);
                //废弃代码，用了报错//创建jpcomment的JProperty类型
                //废弃代码，用了报错///JProperty jpcomment = (JProperty)jcomment;
                rss.Property("#comment").AddAfterSelf(new JProperty("objects", aa));
                rss.Property("objects").AddAfterSelf(new JProperty("version", ver["version"]));
                //json数据字符串化
                string output = Newtonsoft.Json.JsonConvert.SerializeObject(rss, Newtonsoft.Json.Formatting.Indented);
                //废弃语句，用了报错//删除dir字符串
                //废弃语句，用了报错///char[] dirstr = {'.', 'd', 'i', 'r' };
                //输出文本
                File.WriteAllText(Path.GetDirectoryName(Fpath) + "/" + TheFolder.Name.Replace(".dir", "") + ".json", output);
                //提示合成完成
                MessageBox.Show("LevelCraft Done");
            }
            catch
            {
                MessageBox.Show("LevelCraft ERROR");
            }

        }
    }
}