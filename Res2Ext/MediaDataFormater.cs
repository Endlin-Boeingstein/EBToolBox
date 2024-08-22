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
        //创建oo实例
        public OriginOverwriter oo = new OriginOverwriter();

        //生成资源尺寸部分
        public JObject ImageSizeFormat(JArray Rja, JObject imgSz, double round, double scale)
        {
            try
            {
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
                        //20240822宝开化aw+2
                        int width = (int)((int.Parse(((JObject)item)["aw"].ToString())+2 + round) * scale);
                        //废弃代码，用了报错
                        ///JObject width = new JObject();
                        ///width.Add(aw);
                        //读取各个切图的高度并转换
                        //新功能更新而停用///int height = (int)((int.Parse(((JObject)item)["ah"].ToString())+0.64) * 0.78125);
                        //20240822宝开化ah+2
                        int height = (int)((int.Parse(((JObject)item)["ah"].ToString())+2 + round) * scale);
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
                        if (!imgSz.ContainsKey(id))
                        {
                            //添加切图大小数据
                            imgSz.Add(new JProperty(id, sizearray));
                        }
                        else { }
                    }
                    else { }
                }
            }
            catch
            {
                MessageBox.Show("ImageSizeFormat ERROR");
            }
            return imgSz;
        }
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
                //定义extra位图尺寸缩放值
                double scale = 0.78125;
                //新功能更新而停用//询问是否使用1536位图尺寸
                //新功能更新而停用///Console.WriteLine("是否使用1536位图尺寸?如有中文1200位图尺寸需求请输入n或0并按回车键（不输入按回车默认执行1536位图尺寸）");
                //新功能更新而停用//选项收录
                //新功能更新而停用///string sscale = Console.ReadLine();
                //新功能更新而停用//判定是否直接回车，输入它值或直接回车则执行
                if (form1.radioButton22.Checked && !form1.radioButton23.Checked)
                {
                    scale = 1;
                }
                else { }
                //建立imgSz的JSON对象
                JObject imgSz = new JObject();



                //读取文本
                string json = null;
                //将读取文本转换为JSON对象
                JObject rss = new JObject();
                //resources转json数组
                JArray Rja = new JArray();

                //字符串转换为路径
                DirectoryInfo TheFolder = new DirectoryInfo(Jpath);


                //检测是批处理还是单文件
                if (Directory.Exists(Jpath))
                {
                    //创建文件数组
                    FileInfo[] files = TheFolder.GetFiles();
                    //为文件数组排序
                    Array.Sort(files, new FileNameSort());
                    //遍历文件夹内文件
                    foreach (FileInfo NextFile in files)
                    {
                        //读取文本
                        json = File.ReadAllText(NextFile.FullName);
                        //将读取文本转换为JSON对象
                        rss = JObject.Parse(json);
                        //resources转json数组
                        Rja = JArray.Parse(rss["resources"].ToString());
                        //生成资源尺寸部分
                        imgSz.Merge(ImageSizeFormat(Rja, imgSz, round, scale), new JsonMergeSettings
                        {
                            MergeArrayHandling = MergeArrayHandling.Union
                        });
                    }
                }
                else
                {
                    //读取文本
                    json = File.ReadAllText(Jpath);
                    //将读取文本转换为JSON对象
                    rss = JObject.Parse(json);
                    //resources转json数组
                    Rja = JArray.Parse(rss["resources"].ToString());
                    //生成资源尺寸部分
                    imgSz = ImageSizeFormat(Rja, imgSz, round, scale);
                }









                //将预置类序列化
                string output = JsonConvert.SerializeObject(ps);
                //将序列化的预置类 Object化
                JObject ext = JObject.Parse(output);
                //将预置origin Object化
                JObject origin = new JObject();
                origin.Add(ext.Property("origin"));
                //重写origin
                oo.OriginOverwrite(Path.GetDirectoryName(Fpath) + "/extra.json", origin);
                //移除原有的origin
                ext.Property("origin").Remove();
                //在unk后添加origin数组
                ext.Property("unk").AddAfterSelf(oo.Origin.Property("origin"));



                //设置imgMapper
                JObject imgMapper = new JObject();
                //检测是批处理还是单文件
                if (Directory.Exists(Jpath))
                {
                    //创建文件数组
                    FileInfo[] files = TheFolder.GetFiles();
                    //为文件数组排序
                    Array.Sort(files, new FileNameSort());
                    //遍历文件夹内文件
                    foreach (FileInfo NextFile in files)
                    {
                        //读取文本
                        json = File.ReadAllText(NextFile.FullName);
                        //将读取文本转换为JSON对象
                        rss = JObject.Parse(json);
                        //resources转json数组
                        Rja = JArray.Parse(rss["resources"].ToString());
                        //建立i元件引用类并修复受损i元件和a元件
                        icf.ImageClipFormat(Fpath, Rja, ext);
                        imgMapper.Merge(icf.imgMapper, new JsonMergeSettings
                        {
                            MergeArrayHandling = MergeArrayHandling.Union
                        });
                    }
                }
                else
                {
                    //读取文本
                    json = File.ReadAllText(Jpath);
                    //将读取文本转换为JSON对象
                    rss = JObject.Parse(json);
                    //resources转json数组
                    Rja = JArray.Parse(rss["resources"].ToString());
                    icf.ImageClipFormat(Fpath, Rja, ext);
                    imgMapper = icf.imgMapper;
                }

                //新增rmarray记录被删记录
                ArrayList rmarray = new ArrayList();
                //记录未引用尺寸
                foreach (var idsz in imgSz)
                {
                    if (!icf.idal.Contains(idsz.Key))
                    {
                        //废弃代码，用了报错///imgSz.Remove(idsz.Key);
                        rmarray.Add(idsz.Key);
                    }
                    else { }
                }
                //移除未引用尺寸
                foreach (string rma in rmarray)
                {
                    imgSz.Remove(rma);
                }

                //在origin后增加imgSz数组
                ext.Property("origin").AddAfterSelf(new JProperty("imgSz", imgSz));
                //此功能因画蛇添足而移除//重写资源引用类
                //此功能因画蛇添足而移除///ext["imgSz"] = icf.ImgSz;
                //在imgSz后增加imgMapper数组
                ext.Property("imgSz").AddAfterSelf(new JProperty("imgMapper", imgMapper));
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