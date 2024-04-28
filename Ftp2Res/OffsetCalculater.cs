using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Ftp2Res
{
    //建立偏移计算类
    public class OffsetCalculater
    {
        //生成偏移计算部分
        public JObject OffsetCalculate(JObject channel, JProperty scale, long isold, string atlasname, string idl, long zoffset)
        {
            try
            {
                if (scale.Name == "w")
                {
                    //卡槽自动判定计算20231209添加
                    if (atlasname.ToUpper().Contains("ATLASIMAGE_ATLAS_UI_SEEDPACKETS_1536_") || atlasname.ToUpper().Contains("ATLASIMAGE_ATLAS_UI_SEEDPACKETS_1200_") || atlasname.ToUpper().Contains("ATLASIMAGE_ATLAS_UI_SEEDPACKETS_768_") || atlasname.ToUpper().Contains("ATLASIMAGE_ATLAS_UI_SEEDPACKETS_384_"))
                    {
                        if (isold == 0)
                        {
                            if (int.Parse((string)scale.Value) == 238 && idl != "IMAGE_UI_STOREMULTI_SEEDPACKETICON" && !idl.Contains("IMAGE_UI_PACKETS_TOOLS_PROJECTILE_BOWLINGBULB"))
                            {
                                channel.Property("frame").AddAfterSelf(new JProperty("x", 20));
                            }
                            else if (int.Parse((string)scale.Value) == 239 && idl != "IMAGE_UI_STOREMULTI_SEEDPACKETICON" && !idl.Contains("IMAGE_UI_PACKETS_TOOLS_PROJECTILE_BOWLINGBULB"))
                            {
                                channel.Property("frame").AddAfterSelf(new JProperty("x", 19));
                            }
                            else if (idl == "IMAGE_UI_PACKETS_COOLDOWN")
                            {
                                channel.Property("frame").AddAfterSelf(new JProperty("x", 22));
                            }
                            else if (idl == "IMAGE_UI_PACKETS_SELECT")
                            {
                                channel.Property("frame").AddAfterSelf(new JProperty("x", 16));
                            }
                            else if (idl.Contains("_PRICE_TAB"))
                            {
                                channel.Property("frame").AddAfterSelf(new JProperty("x", 148));
                            }
                            else if (idl.Contains("_DOTS_"))
                            {
                                channel.Property("frame").AddAfterSelf(new JProperty("x", 139));
                            }
                            else if (idl.Contains("_ICON"))
                            {
                                if (int.Parse((string)scale.Value) == 66)
                                {
                                    channel.Property("frame").AddAfterSelf(new JProperty("x", 185));
                                }
                                else channel.Property("frame").AddAfterSelf(new JProperty("x", 184));
                            }
                            else if (idl.Contains("_LEVEL_TAB_"))
                            {
                                if (idl.Contains("_GOLD") || idl.Contains("_SILVER"))
                                {
                                    channel.Property("frame").AddAfterSelf(new JProperty("x", 205));
                                }
                                else if (idl.Contains("_BRONZE"))
                                {
                                    channel.Property("frame").AddAfterSelf(new JProperty("x", 204));
                                }
                                else channel.Property("frame").AddAfterSelf(new JProperty("x", 179));
                            }
                            else if (idl.Contains("_LOCK_SMALL"))
                            {
                                channel.Property("frame").AddAfterSelf(new JProperty("x", 198));
                            }
                            else if (idl.Contains("_LOCKED"))
                            {
                                channel.Property("frame").AddAfterSelf(new JProperty("x", 169));
                            }
                            else if (idl.Contains("MINTFAM_BANNER"))
                            {
                                //为统一调整做准备20240308添加
                                channel.Property("frame").AddAfterSelf(new JProperty("x", 0));
                            }
                            else if (idl.Contains("_MINTFAM_"))
                            {
                                //看错了，不应该赋值20240208发现
                                ///channel.Property("frame").AddAfterSelf(new JProperty("x", 111));
                                //为统一调整做准备20240308添加
                                channel.Property("frame").AddAfterSelf(new JProperty("x", 0));
                            }
                            //20231218添加
                            else if (idl.Contains("_POWERTILE_"))
                            {
                                if (idl.Contains("_ALPHA") || idl.Contains("_BETA") || idl.Contains("_GAMMA"))
                                {
                                    channel.Property("frame").AddAfterSelf(new JProperty("x", 78));
                                }
                                else
                                {
                                    channel.Property("frame").AddAfterSelf(new JProperty("x", 79));
                                }
                            }
                            else if (idl == "IMAGE_UI_PACKETS_TOOLS_TOP")
                            {
                                channel.Property("frame").AddAfterSelf(new JProperty("x", 13));
                            }
                            //增加三个无辜的20240310添加
                            else if (idl == "IMAGE_UI_STOREMULTI_SEEDPACKETICON")
                            {
                                channel.Property("frame").AddAfterSelf(new JProperty("x", 0));
                            }
                            else if (idl == "IMAGE_UI_STOREMULTI_SEEDPACKETMINIICON")
                            {
                                channel.Property("frame").AddAfterSelf(new JProperty("x", 0));
                            }
                            else if (idl == "IMAGE_UI_STOREMULTI_SEEDPACKETMINIICONPREMIUM")
                            {
                                channel.Property("frame").AddAfterSelf(new JProperty("x", 0));
                            }
                            //增加一堆无辜的保龄球20240331添加
                            else if (idl.Contains("IMAGE_UI_PACKETS_TOOLS_PROJECTILE_BOWLINGBULB"))
                            {
                                channel.Property("frame").AddAfterSelf(new JProperty("x", 0));
                            }
                            else
                            {
                                //20240209修改
                                channel.Property("frame").AddAfterSelf(new JProperty("x", 25 + 6));
                            }
                        }
                        else if (isold == 1)
                        {
                            if (int.Parse((string)scale.Value) == 238 && idl != "IMAGE_UI_STOREMULTI_SEEDPACKETICON" && !idl.Contains("IMAGE_UI_PACKETS_TOOLS_PROJECTILE_BOWLINGBULB"))
                            {
                                channel.Property("frame").AddAfterSelf(new JProperty("x", 0));
                            }
                            //20240208添加
                            else if (int.Parse((string)scale.Value) == 239 && idl != "IMAGE_UI_STOREMULTI_SEEDPACKETICON" && !idl.Contains("IMAGE_UI_PACKETS_TOOLS_PROJECTILE_BOWLINGBULB"))
                            {
                                channel.Property("frame").AddAfterSelf(new JProperty("x", -1));
                            }
                            else if (idl == "IMAGE_UI_PACKETS_SELECT")
                            {
                                channel.Property("frame").AddAfterSelf(new JProperty("x", 0));
                            }
                            else if (idl == "IMAGE_UI_PACKETS_COOLDOWN")
                            {
                                channel.Property("frame").AddAfterSelf(new JProperty("x", 3));
                            }
                            else if (idl.Contains("_PRICE_TAB"))
                            {
                                if (idl.Contains("_PREMIUM"))
                                {
                                    channel.Property("frame").AddAfterSelf(new JProperty("x", 140));
                                }
                                else
                                {
                                    channel.Property("frame").AddAfterSelf(new JProperty("x", 139));
                                }
                            }
                            else if (idl.Contains("_DOTS_"))
                            {
                                if (idl == "IMAGE_UI_PACKETS_DOTS_RIGHT")
                                {
                                    channel.Property("frame").AddAfterSelf(new JProperty("x", 219));
                                }
                                else
                                {
                                    channel.Property("frame").AddAfterSelf(new JProperty("x", 13));
                                }
                            }
                            else if (idl.Contains("_ICON"))
                            {
                                if (int.Parse((string)scale.Value) == 66)
                                {
                                    channel.Property("frame").AddAfterSelf(new JProperty("x", 163));
                                }
                                else
                                {
                                    channel.Property("frame").AddAfterSelf(new JProperty("x", 162));
                                }
                            }
                            else if (idl.Contains("_LEVEL_TAB_"))
                            {
                                if (idl.Contains("_GOLD") || idl.Contains("_SILVER"))
                                {
                                    channel.Property("frame").AddAfterSelf(new JProperty("x", 153));
                                }
                                else if (idl.Contains("_BRONZE"))
                                {
                                    channel.Property("frame").AddAfterSelf(new JProperty("x", 152));
                                }
                                else channel.Property("frame").AddAfterSelf(new JProperty("x", 146));
                            }
                            else if (idl.Contains("_LOCK_SMALL"))
                            {
                                channel.Property("frame").AddAfterSelf(new JProperty("x", 173));
                            }
                            else if (idl.Contains("_LOCKED"))
                            {
                                channel.Property("frame").AddAfterSelf(new JProperty("x", 149));
                            }
                            else if (idl.Contains("_MINTFAM_"))
                            {
                                //为统一调整做准备20240308添加
                                channel.Property("frame").AddAfterSelf(new JProperty("x", 0));
                            }
                            //20231218添加
                            else if (idl.Contains("_POWERTILE_"))
                            {
                                if (idl.Contains("_ALPHA") || idl.Contains("_BETA") || idl.Contains("_GAMMA"))
                                {
                                    channel.Property("frame").AddAfterSelf(new JProperty("x", 78));
                                }
                                else
                                {
                                    channel.Property("frame").AddAfterSelf(new JProperty("x", 79));
                                }
                            }
                            else if (idl == "IMAGE_UI_PACKETS_TOOLS_TOP")
                            {
                                channel.Property("frame").AddAfterSelf(new JProperty("x", 13));
                            }
                            //20240208添加
                            else if (idl == "IMAGE_UI_PACKETS_EXPLODEONUT")
                            {
                                channel.Property("frame").AddAfterSelf(new JProperty("x", 1));
                            }
                            //增加三个无辜的20240310添加
                            else if (idl == "IMAGE_UI_STOREMULTI_SEEDPACKETICON")
                            {
                                channel.Property("frame").AddAfterSelf(new JProperty("x", 0));
                            }
                            else if (idl == "IMAGE_UI_STOREMULTI_SEEDPACKETMINIICON")
                            {
                                channel.Property("frame").AddAfterSelf(new JProperty("x", 0));
                            }
                            else if (idl == "IMAGE_UI_STOREMULTI_SEEDPACKETMINIICONPREMIUM")
                            {
                                channel.Property("frame").AddAfterSelf(new JProperty("x", 0));
                            }
                            //增加一堆无辜的保龄球20240331添加
                            else if (idl.Contains("IMAGE_UI_PACKETS_TOOLS_PROJECTILE_BOWLINGBULB"))
                            {
                                channel.Property("frame").AddAfterSelf(new JProperty("x", 0));
                            }
                            else
                            {
                                //修改植物偏移20240208
                                channel.Property("frame").AddAfterSelf(new JProperty("x", 1 + 14));
                            }
                        }
                        //20240209添加
                        else if (isold == -1)
                        {
                            //为统一调整做准备20240308添加
                            channel.Property("frame").AddAfterSelf(new JProperty("x", 0));
                        }
                    }
                    //僵尸图鉴偏移计算20240308添加
                    else if (atlasname.ToUpper().Contains("ATLASIMAGE_ATLAS_UI_ZOMBIEPACKETS_1536_") || atlasname.ToUpper().Contains("ATLASIMAGE_ATLAS_UI_ZOMBIEPACKETS_1200_") || atlasname.ToUpper().Contains("ATLASIMAGE_ATLAS_UI_ZOMBIEPACKETS_768_") || atlasname.ToUpper().Contains("ATLASIMAGE_ATLAS_UI_ZOMBIEPACKETS_384_"))
                    {
                        if (zoffset == -1)
                        {
                            //为统一调整做准备20240308添加
                            channel.Property("frame").AddAfterSelf(new JProperty("x", 0));
                        }
                        else
                        {
                            if (idl == "IMAGE_UI_ALMANAC_PACKETS_ZOMBIES_GUIDE")
                            {
                                channel.Property("frame").AddAfterSelf(new JProperty("x", 1));
                            }
                            else if (idl == "IMAGE_UI_ALMANAC_PACKETS_ZOMBIES_READY")
                            {
                                channel.Property("frame").AddAfterSelf(new JProperty("x", 1));
                            }
                            else if (idl == "IMAGE_UI_ALMANAC_PACKETS_ZOMBIES_SELECTED")
                            {
                                channel.Property("frame").AddAfterSelf(new JProperty("x", 1));
                            }
                            else if (idl == "IMAGE_UI_ALMANAC_PACKETS_ZOMBIES_INNER_AREA_GUIDE")
                            {
                                channel.Property("frame").AddAfterSelf(new JProperty("x", 16));
                            }
                            else if (idl == "IMAGE_UI_ALMANAC_PACKETS_ZOMBIES_ZOMBOSSMECH_PIRATE")
                            {
                                channel.Property("frame").AddAfterSelf(new JProperty("x", 17));
                            }
                            else
                            {
                                channel.Property("frame").AddAfterSelf(new JProperty("x", 204 - int.Parse((string)scale.Value)));
                            }
                        }
                    }
                    else { }
                }
                if (scale.Name == "h")
                {
                    //卡槽自动判定计算20231209添加
                    if (atlasname.ToUpper().Contains("ATLASIMAGE_ATLAS_UI_SEEDPACKETS_1536_") || atlasname.ToUpper().Contains("ATLASIMAGE_ATLAS_UI_SEEDPACKETS_1200_") || atlasname.ToUpper().Contains("ATLASIMAGE_ATLAS_UI_SEEDPACKETS_768_") || atlasname.ToUpper().Contains("ATLASIMAGE_ATLAS_UI_SEEDPACKETS_384_"))
                    {
                        if (isold == 0)
                        {
                            if (int.Parse((string)scale.Value) == 150)
                            {
                                channel.Property("x").AddAfterSelf(new JProperty("y", 26));
                            }
                            else if (int.Parse((string)scale.Value) == 151)
                            {
                                channel.Property("x").AddAfterSelf(new JProperty("y", 25));
                            }
                            else if (idl == "IMAGE_UI_PACKETS_COOLDOWN")
                            {
                                channel.Property("x").AddAfterSelf(new JProperty("y", 27));
                            }
                            else if (idl == "IMAGE_UI_PACKETS_SELECT")
                            {
                                channel.Property("x").AddAfterSelf(new JProperty("y", 19));
                            }
                            else if (idl.Contains("_PRICE_TAB"))
                            {
                                if (int.Parse((string)scale.Value) == 72)
                                {
                                    channel.Property("x").AddAfterSelf(new JProperty("y", 90));
                                }
                                else channel.Property("x").AddAfterSelf(new JProperty("y", 91));
                            }
                            else if (idl.Contains("_DOTS_"))
                            {
                                channel.Property("x").AddAfterSelf(new JProperty("y", 172));
                            }
                            else if (idl.Contains("_ICON"))
                            {
                                if (int.Parse((string)scale.Value) == 140)
                                {
                                    channel.Property("x").AddAfterSelf(new JProperty("y", 29));
                                }
                                else channel.Property("x").AddAfterSelf(new JProperty("y", 28));
                            }
                            else if (idl.Contains("_LEVEL_TAB_"))
                            {
                                if (idl.Contains("_MASTERY"))
                                {
                                    channel.Property("x").AddAfterSelf(new JProperty("y", 20));
                                }
                                else channel.Property("x").AddAfterSelf(new JProperty("y", 10));
                            }
                            else if (idl.Contains("_LOCK_SMALL"))
                            {
                                channel.Property("x").AddAfterSelf(new JProperty("y", 69));
                            }
                            else if (idl.Contains("_LOCKED"))
                            {
                                channel.Property("x").AddAfterSelf(new JProperty("y", 93));
                            }
                            else if (idl.Contains("MINTFAM_BANNER"))
                            {
                                //为统一调整做准备20240308添加
                                channel.Property("x").AddAfterSelf(new JProperty("y", 0));
                            }
                            else if (idl.Contains("_MINTFAM_"))
                            {
                                //看错了，不应该赋值20240208发现
                                ///if (idl.Contains("_DEFENSE"))
                                ///{
                                ///channel.Property("x").AddAfterSelf(new JProperty("y", 107));
                                ///}
                                ///else channel.Property("x").AddAfterSelf(new JProperty("y", 108));
                                //为统一调整做准备20240308添加
                                channel.Property("x").AddAfterSelf(new JProperty("y", 0));
                            }
                            //20231218添加
                            else if (idl.Contains("_POWERTILE_"))
                            {
                                if (idl.Contains("_ALPHA") || idl.Contains("_BETA") || idl.Contains("_GAMMA") || idl.Contains("_DELTA"))
                                {
                                    channel.Property("x").AddAfterSelf(new JProperty("y", 29));
                                }
                                else
                                {
                                    channel.Property("x").AddAfterSelf(new JProperty("y", 28));
                                }
                            }
                            else if (idl == "IMAGE_UI_PACKETS_TOOLS_TOP")
                            {
                                channel.Property("x").AddAfterSelf(new JProperty("y", 33));
                            }
                            //增加三个无辜的20240310添加
                            else if (idl == "IMAGE_UI_STOREMULTI_SEEDPACKETICON")
                            {
                                channel.Property("x").AddAfterSelf(new JProperty("y", 0));
                            }
                            else if (idl == "IMAGE_UI_STOREMULTI_SEEDPACKETMINIICON")
                            {
                                channel.Property("x").AddAfterSelf(new JProperty("y", 0));
                            }
                            else if (idl == "IMAGE_UI_STOREMULTI_SEEDPACKETMINIICONPREMIUM")
                            {
                                channel.Property("x").AddAfterSelf(new JProperty("y", 0));
                            }
                            //增加一堆无辜的保龄球20240331添加
                            else if (idl.Contains("IMAGE_UI_PACKETS_TOOLS_PROJECTILE_BOWLINGBULB"))
                            {
                                channel.Property("x").AddAfterSelf(new JProperty("y", 0));
                            }
                            else
                            {
                                //20240209修改
                                channel.Property("x").AddAfterSelf(new JProperty("y", 167 - int.Parse((string)scale.Value) - 4));
                            }
                        }
                        else if (isold == 1)
                        {
                            if (int.Parse((string)scale.Value) == 151)
                            {
                                channel.Property("x").AddAfterSelf(new JProperty("y", 5));
                            }
                            //20240208添加
                            else if (int.Parse((string)scale.Value) == 150)
                            {
                                channel.Property("x").AddAfterSelf(new JProperty("y", 6));
                            }
                            else if (idl == "IMAGE_UI_PACKETS_SELECTED_PREMIUM")
                            {
                                channel.Property("x").AddAfterSelf(new JProperty("y", 6));
                            }
                            else if (idl == "IMAGE_UI_PACKETS_SELECT")
                            {
                                channel.Property("x").AddAfterSelf(new JProperty("y", 3));
                            }
                            else if (idl == "IMAGE_UI_PACKETS_COOLDOWN")
                            {
                                channel.Property("x").AddAfterSelf(new JProperty("y", 8));
                            }
                            else if (idl == "IMAGE_UI_PACKETS_READY_PREMIUM")
                            {
                                channel.Property("x").AddAfterSelf(new JProperty("y", 0));
                            }
                            else if (idl.Contains("_PRICE_TAB"))
                            {
                                channel.Property("x").AddAfterSelf(new JProperty("y", 68));
                            }
                            else if (idl == "IMAGE_UI_PACKETS_DOTS_BOTTOM")
                            {
                                channel.Property("x").AddAfterSelf(new JProperty("y", 136));
                            }
                            else if (idl == "IMAGE_UI_PACKETS_DOTS_LEFT")
                            {
                                channel.Property("x").AddAfterSelf(new JProperty("y", 33));
                            }
                            else if (idl == "IMAGE_UI_PACKETS_DOTS_RIGHT")
                            {
                                channel.Property("x").AddAfterSelf(new JProperty("y", 74));
                            }
                            else if (idl.Contains("_ICON"))
                            {
                                if (int.Parse((string)scale.Value) == 140)
                                {
                                    channel.Property("x").AddAfterSelf(new JProperty("y", 9));
                                }
                                else
                                {
                                    channel.Property("x").AddAfterSelf(new JProperty("y", 8));
                                }
                            }
                            else if (idl.Contains("_LEVEL_TAB_"))
                            {
                                if (idl.Contains("_MASTERY"))
                                {
                                    channel.Property("x").AddAfterSelf(new JProperty("y", 0));
                                }
                                else channel.Property("x").AddAfterSelf(new JProperty("y", 1));
                            }
                            else if (idl.Contains("_LOCK_SMALL"))
                            {
                                channel.Property("x").AddAfterSelf(new JProperty("y", 49));
                            }
                            else if (idl.Contains("_LOCKED"))
                            {
                                channel.Property("x").AddAfterSelf(new JProperty("y", 73));
                            }
                            else if (idl.Contains("_MINTFAM_"))
                            {
                                //为统一调整做准备20240308添加
                                channel.Property("x").AddAfterSelf(new JProperty("y", 0));
                            }
                            //20231218添加
                            else if (idl.Contains("_POWERTILE_"))
                            {
                                if (idl.Contains("_ALPHA") || idl.Contains("_BETA") || idl.Contains("_GAMMA") || idl.Contains("_DELTA"))
                                {
                                    channel.Property("x").AddAfterSelf(new JProperty("y", 29));
                                }
                                else
                                {
                                    channel.Property("x").AddAfterSelf(new JProperty("y", 28));
                                }
                            }
                            else if (idl == "IMAGE_UI_PACKETS_TOOLS_TOP")
                            {
                                channel.Property("x").AddAfterSelf(new JProperty("y", 33));
                            }
                            //20240208添加
                            else if (idl == "IMAGE_UI_PACKETS_EXPLODEONUT")
                            {
                                //为统一调整做准备20240308添加
                                channel.Property("x").AddAfterSelf(new JProperty("y", 0));
                            }
                            //增加三个无辜的20240310添加
                            else if (idl == "IMAGE_UI_STOREMULTI_SEEDPACKETICON")
                            {
                                channel.Property("x").AddAfterSelf(new JProperty("y", 0));
                            }
                            else if (idl == "IMAGE_UI_STOREMULTI_SEEDPACKETMINIICON")
                            {
                                channel.Property("x").AddAfterSelf(new JProperty("y", 0));
                            }
                            else if (idl == "IMAGE_UI_STOREMULTI_SEEDPACKETMINIICONPREMIUM")
                            {
                                channel.Property("x").AddAfterSelf(new JProperty("y", 0));
                            }
                            //增加一堆无辜的保龄球20240331添加
                            else if (idl.Contains("IMAGE_UI_PACKETS_TOOLS_PROJECTILE_BOWLINGBULB"))
                            {
                                channel.Property("x").AddAfterSelf(new JProperty("y", 0));
                            }
                            else
                            {
                                //修改植物偏移20240208
                                channel.Property("x").AddAfterSelf(new JProperty("y", 156 - 17 - int.Parse((string)scale.Value)));
                            }
                        }
                        //20240209添加
                        else if (isold == -1)
                        {
                            //为统一调整做准备20240308添加
                            channel.Property("x").AddAfterSelf(new JProperty("y", 0));
                        }
                    }
                    //僵尸图鉴偏移计算20240308添加
                    else if (atlasname.ToUpper().Contains("ATLASIMAGE_ATLAS_UI_ZOMBIEPACKETS_1536_") || atlasname.ToUpper().Contains("ATLASIMAGE_ATLAS_UI_ZOMBIEPACKETS_1200_") || atlasname.ToUpper().Contains("ATLASIMAGE_ATLAS_UI_ZOMBIEPACKETS_768_") || atlasname.ToUpper().Contains("ATLASIMAGE_ATLAS_UI_ZOMBIEPACKETS_384_"))
                    {
                        if (zoffset == -1)
                        {
                            //为统一调整做准备20240308添加
                            channel.Property("x").AddAfterSelf(new JProperty("y", 0));
                        }
                        else
                        {
                            if (idl == "IMAGE_UI_ALMANAC_PACKETS_ZOMBIES_GUIDE")
                            {
                                channel.Property("x").AddAfterSelf(new JProperty("y", 1));
                            }
                            else if (idl == "IMAGE_UI_ALMANAC_PACKETS_ZOMBIES_READY")
                            {
                                channel.Property("x").AddAfterSelf(new JProperty("y", 1));
                            }
                            else if (idl == "IMAGE_UI_ALMANAC_PACKETS_ZOMBIES_SELECTED")
                            {
                                channel.Property("x").AddAfterSelf(new JProperty("y", 1));
                            }
                            else if (idl == "IMAGE_UI_ALMANAC_PACKETS_ZOMBIES_INNER_AREA_GUIDE")
                            {
                                channel.Property("x").AddAfterSelf(new JProperty("y", 17));
                            }
                            else
                            {
                                channel.Property("x").AddAfterSelf(new JProperty("y", 291 - int.Parse((string)scale.Value)));
                            }
                        }
                    }
                    else { }
                }
            }
            catch
            {
                MessageBox.Show("OffsetCalculate ERROR!");
            }
            return channel;
        }
    }
}