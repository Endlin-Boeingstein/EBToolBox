using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace Ftp2Res
{
	class Preset
	{
		public long slot;
		public string id;
		public String[] path = { "0" };
		public string type = "Image";
		public bool atlas = true;
		public long width = 0;
		public long height = 0;
	}
	public class Ftp2Res
	{
		public void FtpToRes(string filepath,string SLOT)
		{
			try
			{
				//第一个atlas的slot
				long slot;
				//因UI化移除///Console.WriteLine("请输入该group的resources数组集合第一个的atlas图的slot,并按一次回车键");
				//获取输入的slot
				slot = long.Parse(SLOT);
				//获取atlas的slot
				long aslot = slot;
				//读取
				string json = File.ReadAllText(@filepath);
				//将读取文本转换为JSON对象
				JObject rss = JObject.Parse(json);
				//将图集信息 Object化
				JObject meta = (JObject)rss["meta"];
				///JObject aimage = (JObject)meta["image"];废弃代码，将atlas名字 Object化
				//将atlas名字转换为字符串
				string atlasname = meta["image"].ToString();
				//添加前缀删除后缀形成parent,在frame前增加字段
				//删除后缀
				atlasname = atlasname.Substring(0, atlasname.Length - 4);
				//增加id
				//增加变量防止混淆
				string preatlasname = atlasname;
				preatlasname = preatlasname.Substring(0, atlasname.Length - 3);
				rss.Property("frames").AddBeforeSelf(new JProperty("id", preatlasname));
				//创建atlasname副本
				string atname = atlasname;
				//增加type
				//新功能更新而停用///Console.WriteLine("请输入该group的type(图像一般为simple)，并按回车键（不输入按回车默认simple）");
				//设置type默认simple
				string atlastype = "simple";
				//新功能更新而停用//判定是否直接回车，输入它值则type为输入值
				//新功能更新而停用///if (Console.ReadLine() != "")
				//新功能更新而停用///{
				//新功能更新而停用///if (Console.ReadLine() != "/n/n")
				//新功能更新而停用///{
				//新功能更新而停用///atlastype = Console.ReadLine();
				//新功能更新而停用///}
				//新功能更新而停用///}
				rss.Property("frames").AddBeforeSelf(new JProperty("type", atlastype));
				//增加parent
				string atlasparent = preatlasname;
				//判定parent后缀并删除
				//删除"_"和分辨率
				atlasparent = atlasparent.ToString().Replace("_1536", "").Replace("_768", "").Replace("_384", "");
				//输出parent
				rss.Property("frames").AddBeforeSelf(new JProperty("parent", atlasparent));
				//定义res值
				string atlasres = preatlasname;
				//判断提取res值
				if (atlasres.Contains("1536")) atlasres = "1536";
				if (atlasres.Contains("768")) atlasres = "768";
				if (atlasres.Contains("384")) atlasres = "384";
				///atlasres = atlasres.Substring(atlasres.IndexOf("_"), atlasres.Length);废弃代码，用来从atlas名称里提取分辨率，用了报错
				//输出res值
				rss.Property("frames").AddBeforeSelf(new JProperty("res", atlasres));
				//添加前缀
				atlasname = "ATLASIMAGE_ATLAS_" + atlasname;
				//将首个JProperty.Name Object化
				JObject frames = (JObject)rss["frames"];
				//遍历所有image数据的文件名并且移除多余数据
				foreach (JProperty item in frames.Properties())
				{
					//将各个image名称Object化
					JObject channel = (JObject)frames[item.Name];
					//记录各个image名称
					//从路径提取文件夹名
					string[] imagename = item.Name.Split('/');
					//转换为List
					List<String> imagenamelist = new List<String>(imagename);
					//转换为JSON数组
					///string Jimagename = JsonConvert.SerializeObject(imagenamelist);废弃代码，用了有斜杠转义字符
					//复制imagenamelist为idlist
					List<String> idlist = new List<String>(imagename);
					//删除idlist中的分辨率和图片类型,改images为image
					for (int i = idlist.Count - 1; i >= 0; i--)
					{
						if (idlist[i] == "images")
						{
							idlist[i] = "image";
						}
						if (idlist[i] == "1536")
						{
							idlist.Remove(idlist[i]);
						}
						if (idlist[i] == "768")
						{
							idlist.Remove(idlist[i]);
						}
						if (idlist[i] == "384")
						{
							idlist.Remove(idlist[i]);
						}
						if (idlist[i] == "initial")
						{
							idlist.Remove(idlist[i]);
						}
						if (idlist[i] == "full")
						{
							idlist.Remove(idlist[i]);
						}
					}
					///废弃代码段，用来添加集合元素用的，用不着了
					///List<String> list = new List<String>();
					///list.Add("One");
					///list.Add("Two");
					///list.Add("Three");
					///list.Add("Four");
					///list.Add("Five");
					///list.Add("Six");
					///list.Add("Seven");
					///list.Add("Eight");
					///Console.WriteLine("枚举器遍历列表元素...");
					///List<string>.Enumerator demoEnum = list.GetEnumerator();
					///while (demoEnum.MoveNext())
					///{
					///	string res = demoEnum.Current;
					///	Console.WriteLine(res);
					//设置id的字符串类型
					string idl = null;
					//将修改后得到的idlist转换为id并且用"_"分隔
					for (int i = 0; i < idlist.Count; i++)
					{
						idl += idlist[i];
						if (i != idlist.Count - 1) idl += "_";
					}
					//将idl转大写
					idl = idl.ToUpper();
					//将rotated移除
					channel.Property("rotated").Remove();
					//将trimmed移除
					channel.Property("trimmed").Remove();
					//将spriteSourceSize移除
					channel.Property("spriteSourceSize").Remove();
					//将sourceSize移除
					channel.Property("sourceSize").Remove();
					//将pivot移除
					channel.Property("pivot").Remove();
					//将frame尺寸参数Object化
					JObject scaledata = (JObject)channel["frame"];
					//遍历frame中的各个尺寸信息并判定移动到frame外（复制frame内容到frame外并在各个名称前加"a"）
					foreach (JProperty scale in scaledata.Properties())
					{
						///JObject sca = (JObject)scaledata[scale.Name];废弃代码，用了出错
						if (scale.Name == "x")
						{
							channel.Property("frame").AddBeforeSelf(new JProperty("ax", scale.Value));
						}
						if (scale.Name == "y")
						{
							channel.Property("frame").AddBeforeSelf(new JProperty("ay", scale.Value));
						}
						if (scale.Name == "w")
						{
							channel.Property("frame").AddBeforeSelf(new JProperty("aw", scale.Value));
						}
						if (scale.Name == "h")
						{
							channel.Property("frame").AddBeforeSelf(new JProperty("ah", scale.Value));
						}
					}
					//转parent为大写
					atlasname = atlasname.ToUpper();
					//移除旧flame
					channel.Property("frame").Remove();
					//加入slot
					slot++;
					channel.Property("ax").AddBeforeSelf(new JProperty("slot", slot));
					//加入id
					channel.Property("ax").AddBeforeSelf(new JProperty("id", idl));
					//加入path
					channel.Property("ax").AddBeforeSelf(new JProperty("path", JsonConvert.DeserializeObject(JsonConvert.SerializeObject(imagenamelist))));
					//加入type
					channel.Property("ax").AddBeforeSelf(new JProperty("type", "Image"));
					//加入parent
					channel.Property("ax").AddBeforeSelf(new JProperty("parent", atlasname));
				}
				//创建对象preset记录atlas数据
				Preset preset = new Preset();
				//为atname添加前缀
				atname = "atlases_" + atname;
				//根据分隔符"_"分割atlas名字生成路径数组
				string[] atlaspath = atname.Split('_');
				//根据数组生成集合
				List<String> atlaspathlist = new List<String>(atlaspath);
				//删除集合内多余元素，修改部分元素为缺失元素
				for (int i = atlaspathlist.Count - 1; i >= 0; i--)
				{
					//防止"1536"，"00"等被分离变成单独的集合元素
					if (atlaspathlist[i] == "1536" || atlaspathlist[i] == "768" || atlaspathlist[i] == "384")
					{
						atlaspathlist[i - 1] += "_" + atlaspathlist[i];
						atlaspathlist.Remove(atlaspathlist[i]);
						if (atlaspathlist[i] == "00" || atlaspathlist[i] == "01" || atlaspathlist[i] == "02" || atlaspathlist[i] == "03" || atlaspathlist[i] == "04" || atlaspathlist[i] == "05" || atlaspathlist[i] == "06" || atlaspathlist[i] == "07" || atlaspathlist[i] == "08" || atlaspathlist[i] == "09" || atlaspathlist[i] == "10")
						{
							atlaspathlist[i - 1] += "_" + atlaspathlist[i];
							atlaspathlist.Remove(atlaspathlist[i]);
						}
					}

				}
				//将meta下size Object化
				JObject asize = (JObject)meta["size"];
				//将meta尺寸数据赋值给atlas尺寸数据
				foreach (JProperty ascale in asize.Properties())
				{
					if (ascale.Name == "w")
					{
						preset.width = (long)ascale.Value;
					}
					if (ascale.Name == "h")
					{
						preset.height = (long)ascale.Value;
					}
				}
				//定义id
				string aname = atlasname;
				//id转大写
				aname = aname.ToUpper();
				//设置atlas的slot
				preset.slot = aslot;
				//设置atlas的id
				preset.id = aname;
				//path list转数组
				string[] apl = atlaspathlist.ToArray();
				//数组覆盖preset类path
				preset.path = apl;
				//在frames里添加atlas信息
				frames.AddFirst(new JProperty("preset", JsonConvert.DeserializeObject(JsonConvert.SerializeObject(preset))));
				//移除meta
				rss.Property("meta").Remove();
				//建立JSON数组
				JArray aa = new JArray();
				//遍历所有image数据的文件名
				foreach (JProperty item in frames.Properties())
				{
					//将各个image名称Object化
					JObject channel = (JObject)frames[item.Name];
					//将内容放进JSON数组
					aa.Add(channel);
				}
				//在frames后创建集合resources
				rss.Property("frames").AddAfterSelf(new JProperty("resources", aa));
				//移除frames
				rss.Property("frames").Remove();
				//命名输出文件名字
				string fileid = rss["id"].ToString();
				//JSON数据字符串化
				string output = Newtonsoft.Json.JsonConvert.SerializeObject(rss, Newtonsoft.Json.Formatting.Indented);
				//输出文本
				File.WriteAllText(Path.GetDirectoryName(filepath) + "/" + fileid + ".json", output);
				//提示Done
				MessageBox.Show("FtpToRes Done");
			}
			catch
			{
				MessageBox.Show("FtpToRes ERROR!");
			}
		}
	}
}