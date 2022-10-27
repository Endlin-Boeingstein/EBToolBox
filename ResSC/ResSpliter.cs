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
                    string id = ((JObject)item)["id"].ToString();
                    File.WriteAllText(Path.GetDirectoryName(Jpath) + "/resources.dir/" + id + ".json", JsonConvert.SerializeObject(item, Formatting.Indented));
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