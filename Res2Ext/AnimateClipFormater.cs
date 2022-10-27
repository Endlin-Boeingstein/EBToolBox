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
    //建立a元件引用类
    class AnimateClipFormater
    {
        //建立animMapper的JSON对象
        public JObject animMapper = new JObject();
        //生成a元件引用部分
        public void AnimateClipFormat(string FileName)
        {
            try
            {
                //添加a元件切图引用数据
                animMapper.Add(new JProperty(FileName.Substring(0, FileName.Length - 4), "default_layer"));
            }
            catch
            {
                MessageBox.Show("AnimateClipFormat ERROR");
            }
        }
    }
}