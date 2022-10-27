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
    //建立资源引用类
    class MediaDataFormater
    {
        //创建ps实例
        public Preset ps = new Preset();
        //创建icf实例
        public ImageClipFormater icf = new ImageClipFormater();
        //创建aco实例
        public AnimateClipOverwriter aco = new AnimateClipOverwriter();
        //生成资源引用部分
        public void MediaDataFormat(string Jpath, string Fpath)
        {
            try
            {
                //定义四舍五入默认值
                double round = 0.64;
                //新功能更新而停用//询问是否四舍五入
                //新功能更新而停用///Console.WriteLine("是否使用官方位图尺寸?如有四舍五入需求请输入n或0并按回车键（不输入按回车默认执行官方位图尺寸）");
                //新功能更新而停用//选项收录
                //新功能更新而停用///string s = Console.ReadLine();
                //新功能更新而停用//判定是否直接回车，输入它值或直接回车则执行
                if (form1.radioButton6.Checked&&!form1.radioButton5.Checked)
                {
                    round = 0;
                }
                else { }
                //建立imgSz的JSON对象
                JObject imgSz = new JObject();
                //读取文本
                string json = File.ReadAllText(Jpath);
                //将读取文本转换为JSON对象
                JObject rss = JObject.Parse(json);
                //resources转json数组
                JArray Rja = JArray.Parse(rss["resources"].ToString());
                //遍历resources数组
                foreach (var item in Rja)
                {
                    //新功能更新而停用///if (item.Contains("atlas"))
                    //新功能更新而停用///{
                    //新功能更新而停用//将atlas结果转为字符串
                    //新功能更新而停用///string atlas = item["atlas"].ToString();
                    //新功能更新而停用/// }
                    //判定是否为atlas，用以排除
                    if (!item.ToString().Contains("atlas"))
                    {
                        //读取各个切图的id
                        string id = ((JObject)item)["id"].ToString();
                        //读取各个切图的宽度并转换
                        //新功能更新而停用///int width = (int)((int.Parse(((JObject)item)["aw"].ToString())+0.64) * 0.78125);
                        int width = (int)((int.Parse(((JObject)item)["aw"].ToString()) + round) * 0.78125);
                        //废弃代码，用了报错
                        ///JObject width = new JObject();
                        ///width.Add(aw);
                        //读取各个切图的高度并转换
                        //新功能更新而停用///int height = (int)((int.Parse(((JObject)item)["ah"].ToString())+0.64) * 0.78125);
                        int height = (int)((int.Parse(((JObject)item)["ah"].ToString()) + round) * 0.78125);
                        //废弃代码，用了报错
                        ///JObject height = new JObject();
                        ///height.Add(ah);
                        //建立尺寸数组
                        JArray sizearray = new JArray();
                        //写入数组内容
                        //写入宽度
                        sizearray.Add(width);
                        //写入高度
                        sizearray.Add(height);
                        //添加切图大小数据
                        imgSz.Add(new JProperty(id, sizearray));
                    }
                    else { }
                }







                //将预置类序列化
                string output = JsonConvert.SerializeObject(ps);
                //将序列化的预置类 Object化
                JObject ext = JObject.Parse(output);
                //在origin后增加imgSz数组
                ext.Property("origin").AddAfterSelf(new JProperty("imgSz", imgSz));
                //建立i元件引用类并修复受损i元件和a元件
                icf.ImageClipFormat(Fpath, Rja, ext);
                //此功能因画蛇添足而移除//重写资源引用类
                //此功能因画蛇添足而移除///ext["imgSz"] = icf.ImgSz;
                //在imgSz后增加imgMapper数组
                ext.Property("imgSz").AddAfterSelf(new JProperty("imgMapper", icf.imgMapper));
                //重写animMapper数组
                aco.AnimateClipOverwrite(Path.GetDirectoryName(Fpath) + "/extra.json", icf.acf.animMapper);
                //在imgMapper后增加animMapper数组
                //新功能更新而停用///ext.Property("imgMapper").AddAfterSelf(new JProperty("animMapper", icf.acf.animMapper));
                ext.Property("imgMapper").AddAfterSelf(new JProperty("animMapper", aco.AnimMapper));
                //json数据字符串化
                output = Newtonsoft.Json.JsonConvert.SerializeObject(ext, Newtonsoft.Json.Formatting.Indented);
                //输出文本
                File.WriteAllText(Path.GetDirectoryName(Fpath) + "/extra.json", output);
            }
            catch
            {
                MessageBox.Show("MediaDataFormat ERROR");
            }
        }
    }
}