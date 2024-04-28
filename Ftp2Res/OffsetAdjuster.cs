using EBToolBox;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Channels;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static EBToolBox.Form2;

namespace Ftp2Res
{
    //建立偏移调整类
    public class OffsetAdjuster
    {
        //生成偏移计算调整部分
        public JObject OffsetMove(JObject resource, int xadd, int yadd)
        {
            //判定是否存在偏移
            if (resource.ContainsKey("x") && resource.ContainsKey("y"))
            {
                resource["x"] = (int)resource["x"] + xadd;
                resource["y"] = (int)resource["y"] + yadd;
            }
            else
            {
                if (!resource.ContainsKey("x"))
                {
                    resource.Property("ah").AddAfterSelf(new JProperty("x", xadd));
                }
                if (!resource.ContainsKey("y"))
                {
                    resource.Property("x").AddAfterSelf(new JProperty("y", yadd));
                }
            }
            return resource;
        }
        //生成偏移调整部分
        public JObject OffsetAdjust(JObject rss)
        {
            /*JSON结构化，半完工，20240310放弃
            JArray resources = JArray.Parse(rss["resources"].ToString());
            foreach (JObject res in resources)
            {

                if ((bool)res["atlas"])
                {
                    AtlasStruct as= new AtlasStruct((bool)res["atlas"], (int)res["width"], (int)res["height"], res["slot"].ToString(), res["id"].ToString(), res["path"], res["type"]);
                }
                else
                {

                }
            }
            ResourceStruct rs= new ResourceStruct(rss["id"].ToString(), rss["type"].ToString(), rss["parent"].ToString(), rss["res"].ToString(), );
            */
            try
            {
                //卡槽自动判定
                if (rss["parent"].ToString() == "UI_SeedPackets")
                {
                    //定义x和y要增加的量
                    int xadd = 0;
                    int yadd = 0;
                    ArrayList al = new ArrayList();
                    string x = form2.textBox14.Text;
                    //请输入x相对原来偏移量：
                    xadd = int.Parse(form2.textBox14.Text);
                    //请输入y相对原来偏移量：
                    yadd = int.Parse(form2.textBox1.Text);

                    //读取resources作为JArray
                    JArray resources = JArray.Parse(rss["resources"].ToString());
                    //遍历获取各个位图信息
                    foreach (JObject resource in resources)
                    {
                        if (resource.ContainsKey("atlas")) { }
                        else
                        {
                            if (int.Parse((string)resource["aw"]) == 238 && form2.checkedListBox1.GetItemChecked(0) && resource["id"].ToString() != "IMAGE_UI_STOREMULTI_SEEDPACKETICON" && !resource["id"].ToString().Contains("IMAGE_UI_PACKETS_TOOLS_PROJECTILE_BOWLINGBULB"))
                            {
                                //判定是否存在偏移
                                if (resource.ContainsKey("x"))
                                {
                                    resource["x"] = (int)resource["x"] + xadd;
                                }
                                else
                                {
                                    if (!resource.ContainsKey("x"))
                                    {
                                        resource.Property("ah").AddAfterSelf(new JProperty("x", xadd));
                                    }
                                }
                                //修复y不计算问题20240323修复
                                if (int.Parse((string)resource["ah"]) == 150 && form2.checkedListBox1.GetItemChecked(0))
                                {
                                    //判定是否存在偏移
                                    if (resource.ContainsKey("y"))
                                    {
                                        resource["y"] = (int)resource["y"] + yadd;
                                    }
                                    else
                                    {
                                        if (!resource.ContainsKey("y"))
                                        {
                                            resource.Property("x").AddAfterSelf(new JProperty("y", yadd));
                                        }
                                    }
                                }
                                else if (int.Parse((string)resource["ah"]) == 151 && form2.checkedListBox1.GetItemChecked(0))
                                {
                                    //判定是否存在偏移
                                    if (resource.ContainsKey("y"))
                                    {
                                        resource["y"] = (int)resource["y"] + yadd;
                                    }
                                    else
                                    {
                                        if (!resource.ContainsKey("y"))
                                        {
                                            resource.Property("x").AddAfterSelf(new JProperty("y", yadd));
                                        }
                                    }
                                }
                            }
                            else if (int.Parse((string)resource["aw"]) == 239 && form2.checkedListBox1.GetItemChecked(0) && resource["id"].ToString() != "IMAGE_UI_STOREMULTI_SEEDPACKETICON" && !resource["id"].ToString().Contains("IMAGE_UI_PACKETS_TOOLS_PROJECTILE_BOWLINGBULB"))
                            {
                                //判定是否存在偏移
                                if (resource.ContainsKey("x"))
                                {
                                    resource["x"] = (int)resource["x"] + xadd;
                                }
                                else
                                {
                                    if (!resource.ContainsKey("x"))
                                    {
                                        resource.Property("ah").AddAfterSelf(new JProperty("x", xadd));
                                    }
                                }
                                //修复y不计算问题20240323修复
                                if (int.Parse((string)resource["ah"]) == 150 && form2.checkedListBox1.GetItemChecked(0))
                                {
                                    //判定是否存在偏移
                                    if (resource.ContainsKey("y"))
                                    {
                                        resource["y"] = (int)resource["y"] + yadd;
                                    }
                                    else
                                    {
                                        if (!resource.ContainsKey("y"))
                                        {
                                            resource.Property("x").AddAfterSelf(new JProperty("y", yadd));
                                        }
                                    }
                                }
                                else if (int.Parse((string)resource["ah"]) == 151 && form2.checkedListBox1.GetItemChecked(0))
                                {
                                    //判定是否存在偏移
                                    if (resource.ContainsKey("y"))
                                    {
                                        resource["y"] = (int)resource["y"] + yadd;
                                    }
                                    else
                                    {
                                        if (!resource.ContainsKey("y"))
                                        {
                                            resource.Property("x").AddAfterSelf(new JProperty("y", yadd));
                                        }
                                    }
                                }
                            }
                            else if (resource["id"].ToString() == "IMAGE_UI_PACKETS_COOLDOWN" && form2.checkedListBox1.GetItemChecked(2))
                            {
                                //判定是否存在偏移
                                if (resource.ContainsKey("x"))
                                {
                                    resource["x"] = (int)resource["x"] + xadd;
                                }
                                if (resource.ContainsKey("y"))
                                {
                                    resource["y"] = (int)resource["y"] + yadd;
                                }
                                if (!resource.ContainsKey("x"))
                                {
                                    resource.Property("ah").AddAfterSelf(new JProperty("x", xadd));
                                }
                                if (!resource.ContainsKey("y"))
                                {
                                    resource.Property("x").AddAfterSelf(new JProperty("y", yadd));
                                }
                            }
                            else if (resource["id"].ToString() == "IMAGE_UI_PACKETS_SELECT" && form2.checkedListBox1.GetItemChecked(3))
                            {
                                //判定是否存在偏移
                                if (resource.ContainsKey("x"))
                                {
                                    resource["x"] = (int)resource["x"] + xadd;
                                }
                                if (resource.ContainsKey("y"))
                                {
                                    resource["y"] = (int)resource["y"] + yadd;
                                }
                                if (!resource.ContainsKey("x"))
                                {
                                    resource.Property("ah").AddAfterSelf(new JProperty("x", xadd));
                                }
                                if (!resource.ContainsKey("y"))
                                {
                                    resource.Property("x").AddAfterSelf(new JProperty("y", yadd));
                                }
                            }
                            else if (resource["id"].ToString().Contains("_PRICE_TAB") && form2.checkedListBox1.GetItemChecked(4))
                            {
                                //判定是否存在偏移
                                if (resource.ContainsKey("x"))
                                {
                                    resource["x"] = (int)resource["x"] + xadd;
                                }
                                if (resource.ContainsKey("y"))
                                {
                                    resource["y"] = (int)resource["y"] + yadd;
                                }
                                if (!resource.ContainsKey("x"))
                                {
                                    resource.Property("ah").AddAfterSelf(new JProperty("x", xadd));
                                }
                                if (!resource.ContainsKey("y"))
                                {
                                    resource.Property("x").AddAfterSelf(new JProperty("y", yadd));
                                }
                            }
                            else if (resource["id"].ToString().Contains("_DOTS_") && form2.checkedListBox1.GetItemChecked(5))
                            {
                                //判定是否存在偏移
                                if (resource.ContainsKey("x"))
                                {
                                    resource["x"] = (int)resource["x"] + xadd;
                                }
                                if (resource.ContainsKey("y"))
                                {
                                    resource["y"] = (int)resource["y"] + yadd;
                                }
                                if (!resource.ContainsKey("x"))
                                {
                                    resource.Property("ah").AddAfterSelf(new JProperty("x", xadd));
                                }
                                if (!resource.ContainsKey("y"))
                                {
                                    resource.Property("x").AddAfterSelf(new JProperty("y", yadd));
                                }
                            }
                            else if (resource["id"].ToString().Contains("_ICON") && form2.checkedListBox1.GetItemChecked(6))
                            {
                                //判定是否存在偏移
                                if (resource.ContainsKey("x"))
                                {
                                    resource["x"] = (int)resource["x"] + xadd;
                                }
                                if (resource.ContainsKey("y"))
                                {
                                    resource["y"] = (int)resource["y"] + yadd;
                                }
                                if (!resource.ContainsKey("x"))
                                {
                                    resource.Property("ah").AddAfterSelf(new JProperty("x", xadd));
                                }
                                if (!resource.ContainsKey("y"))
                                {
                                    resource.Property("x").AddAfterSelf(new JProperty("y", yadd));
                                }
                            }
                            else if (resource["id"].ToString().Contains("_LEVEL_TAB_") && form2.checkedListBox1.GetItemChecked(7))
                            {
                                //判定是否存在偏移
                                if (resource.ContainsKey("x"))
                                {
                                    resource["x"] = (int)resource["x"] + xadd;
                                }
                                if (resource.ContainsKey("y"))
                                {
                                    resource["y"] = (int)resource["y"] + yadd;
                                }
                                if (!resource.ContainsKey("x"))
                                {
                                    resource.Property("ah").AddAfterSelf(new JProperty("x", xadd));
                                }
                                if (!resource.ContainsKey("y"))
                                {
                                    resource.Property("x").AddAfterSelf(new JProperty("y", yadd));
                                }
                            }
                            else if (resource["id"].ToString().Contains("_LOCK_SMALL") && form2.checkedListBox1.GetItemChecked(8))
                            {
                                //判定是否存在偏移
                                if (resource.ContainsKey("x"))
                                {
                                    resource["x"] = (int)resource["x"] + xadd;
                                }
                                if (resource.ContainsKey("y"))
                                {
                                    resource["y"] = (int)resource["y"] + yadd;
                                }
                                if (!resource.ContainsKey("x"))
                                {
                                    resource.Property("ah").AddAfterSelf(new JProperty("x", xadd));
                                }
                                if (!resource.ContainsKey("y"))
                                {
                                    resource.Property("x").AddAfterSelf(new JProperty("y", yadd));
                                }
                            }
                            else if (resource["id"].ToString().Contains("_LOCKED") && form2.checkedListBox1.GetItemChecked(9))
                            {
                                //判定是否存在偏移
                                if (resource.ContainsKey("x"))
                                {
                                    resource["x"] = (int)resource["x"] + xadd;
                                }
                                if (resource.ContainsKey("y"))
                                {
                                    resource["y"] = (int)resource["y"] + yadd;
                                }
                                if (!resource.ContainsKey("x"))
                                {
                                    resource.Property("ah").AddAfterSelf(new JProperty("x", xadd));
                                }
                                if (!resource.ContainsKey("y"))
                                {
                                    resource.Property("x").AddAfterSelf(new JProperty("y", yadd));
                                }
                            }
                            else if (resource["id"].ToString().Contains("MINTFAM_BANNER") && form2.checkedListBox1.GetItemChecked(10))
                            {
                                //判定是否存在偏移
                                if (resource.ContainsKey("x"))
                                {
                                    resource["x"] = (int)resource["x"] + xadd;
                                }
                                if (resource.ContainsKey("y"))
                                {
                                    resource["y"] = (int)resource["y"] + yadd;
                                }
                                if (!resource.ContainsKey("x"))
                                {
                                    resource.Property("ah").AddAfterSelf(new JProperty("x", xadd));
                                }
                                if (!resource.ContainsKey("y"))
                                {
                                    resource.Property("x").AddAfterSelf(new JProperty("y", yadd));
                                }
                            }
                            else if (resource["id"].ToString().Contains("_MINTFAM_") && form2.checkedListBox1.GetItemChecked(11))
                            {
                                //判定是否存在偏移
                                if (resource.ContainsKey("x"))
                                {
                                    resource["x"] = (int)resource["x"] + xadd;
                                }
                                if (resource.ContainsKey("y"))
                                {
                                    resource["y"] = (int)resource["y"] + yadd;
                                }
                                if (!resource.ContainsKey("x"))
                                {
                                    resource.Property("ah").AddAfterSelf(new JProperty("x", xadd));
                                }
                                if (!resource.ContainsKey("y"))
                                {
                                    resource.Property("x").AddAfterSelf(new JProperty("y", yadd));
                                }
                            }
                            else if (resource["id"].ToString().Contains("_POWERTILE_") && form2.checkedListBox1.GetItemChecked(12))
                            {
                                //判定是否存在偏移
                                if (resource.ContainsKey("x"))
                                {
                                    resource["x"] = (int)resource["x"] + xadd;
                                }
                                if (resource.ContainsKey("y"))
                                {
                                    resource["y"] = (int)resource["y"] + yadd;
                                }
                                if (!resource.ContainsKey("x"))
                                {
                                    resource.Property("ah").AddAfterSelf(new JProperty("x", xadd));
                                }
                                if (!resource.ContainsKey("y"))
                                {
                                    resource.Property("x").AddAfterSelf(new JProperty("y", yadd));
                                }
                            }
                            else if (resource["id"].ToString() == "IMAGE_UI_PACKETS_TOOLS_TOP" && form2.checkedListBox1.GetItemChecked(13))
                            {
                                //判定是否存在偏移
                                if (resource.ContainsKey("x"))
                                {
                                    resource["x"] = (int)resource["x"] + xadd;
                                }
                                if (resource.ContainsKey("y"))
                                {
                                    resource["y"] = (int)resource["y"] + yadd;
                                }
                                if (!resource.ContainsKey("x"))
                                {
                                    resource.Property("ah").AddAfterSelf(new JProperty("x", xadd));
                                }
                                if (!resource.ContainsKey("y"))
                                {
                                    resource.Property("x").AddAfterSelf(new JProperty("y", yadd));
                                }
                            }
                            else if (resource["id"].ToString().Contains("IMAGE_UI_STOREMULTI_") && form2.checkedListBox1.GetItemChecked(14))
                            {
                                //判定是否存在偏移
                                if (resource.ContainsKey("x"))
                                {
                                    resource["x"] = (int)resource["x"] + xadd;
                                }
                                if (resource.ContainsKey("y"))
                                {
                                    resource["y"] = (int)resource["y"] + yadd;
                                }
                                if (!resource.ContainsKey("x"))
                                {
                                    resource.Property("ah").AddAfterSelf(new JProperty("x", xadd));
                                }
                                if (!resource.ContainsKey("y"))
                                {
                                    resource.Property("x").AddAfterSelf(new JProperty("y", yadd));
                                }
                            }
                            else if (form2.checkedListBox1.GetItemChecked(1))
                            {
                                if (int.Parse((string)resource["aw"]) == 238 && !resource["id"].ToString().Contains("IMAGE_UI_PACKETS_TOOLS_PROJECTILE_BOWLINGBULB")) { }
                                else if (int.Parse((string)resource["aw"]) == 239 && !resource["id"].ToString().Contains("IMAGE_UI_PACKETS_TOOLS_PROJECTILE_BOWLINGBULB")) { }
                                else if (int.Parse((string)resource["ah"]) == 150) { }
                                else if (int.Parse((string)resource["ah"]) == 151) { }
                                else if (resource["id"].ToString() == "IMAGE_UI_PACKETS_COOLDOWN") { }
                                else if (resource["id"].ToString() == "IMAGE_UI_PACKETS_SELECT") { }
                                else if (resource["id"].ToString().Contains("_PRICE_TAB")) { }
                                else if (resource["id"].ToString().Contains("_DOTS_")) { }
                                else if (resource["id"].ToString().Contains("_ICON")) { }
                                else if (resource["id"].ToString().Contains("_LEVEL_TAB_")) { }
                                else if (resource["id"].ToString().Contains("_LOCK_SMALL")) { }
                                else if (resource["id"].ToString().Contains("_LOCKED")) { }
                                else if (resource["id"].ToString().Contains("MINTFAM_BANNER")) { }
                                else if (resource["id"].ToString().Contains("_MINTFAM_")) { }
                                else if (resource["id"].ToString().Contains("_POWERTILE_")) { }
                                else if (resource["id"].ToString() == "IMAGE_UI_PACKETS_TOOLS_TOP") { }
                                else if (resource["id"].ToString().Contains("IMAGE_UI_STOREMULTI_")) { }
                                else
                                {
                                    //判定是否存在偏移
                                    if (resource.ContainsKey("x"))
                                    {
                                        resource["x"] = (int)resource["x"] + xadd;
                                    }
                                    if (resource.ContainsKey("y"))
                                    {
                                        resource["y"] = (int)resource["y"] + yadd;
                                    }
                                    if (!resource.ContainsKey("x"))
                                    {
                                        resource.Property("ah").AddAfterSelf(new JProperty("x", xadd));
                                    }
                                    if (!resource.ContainsKey("y"))
                                    {
                                        resource.Property("x").AddAfterSelf(new JProperty("y", yadd));
                                    }
                                }
                            }
                            else { }
                        }
                    }
                    MessageBox.Show("OffsetAdjustment Done.");
                    rss["resources"] = resources;
                }
                else { }
            }
            catch
            {
                MessageBox.Show("OffsetAdjust ERROR!");
            }
            return rss;
        }
    }
}