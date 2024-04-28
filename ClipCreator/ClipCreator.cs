using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;
using static EBToolBox.Form1;

namespace ClipCreator
{
    //建立元件建立类
    class ClipCreator
    {
        //创建icr实例
        public ImageClipReader icr = new ImageClipReader();
        //创建icc实例
        public ImageClipCreator icc = new ImageClipCreator();
        //创建acr实例
        public AnimateClipReader acr = new AnimateClipReader();
        //创建acc实例
        public AnimateClipCreator acc = new AnimateClipCreator();
        //创建rs实例
        public ReplaceScanner rs = new ReplaceScanner();
        //创建ccsr实例
        public SpriteReplacer ccsr = new SpriteReplacer();
        //创建mlls实例
        public MultiLoadedLayerSpliter mlls = new MultiLoadedLayerSpliter();
        //预定义Fpath
        public string Fpath = null;
        //生成元件建立部分
        public void ClipCreate(string Fpath)
        {
            try
            {
                this.Fpath = Fpath;
                //读取被a元件或main元件引用的位图信息，给数组留下记录
                rs.ReplaceScan(this.Fpath + "\\LIBRARY");
                //读取i元件信息并删除问题元件，给数组留下空白记录
                icr.ImageClipRead(this.Fpath + "\\LIBRARY");
                //对引用被删除i元件的a元件进行删除20240307添加
                acr.AnimateClipRead(this.Fpath + "\\LIBRARY", icr.delirecord);
                //根据数组创立i元件
                icc.ImageClipCreate(this.Fpath + "\\LIBRARY", icr.irecord, rs.rsrecord);
                //根据数组替换对应位图为i元件
                ccsr.SpriteReplace(this.Fpath + "\\LIBRARY");
                //读取被a元件或main元件引用的位图信息，给数组留下记录
                rs.ReplaceScan(this.Fpath + "\\LIBRARY");
                //分解多位图/元件图层
                mlls.MultiLoadedLayerSplite(this.Fpath + "\\LIBRARY", rs.mllsrecord);
                //读取a元件信息并删除问题元件，给数组留下空白记录
                acr.AnimateClipRead(this.Fpath + "\\LIBRARY");
                //根据数组创立a元件
                acc.AnimateClipCreate(this.Fpath + "\\LIBRARY", acr.arecord, acr.inum, acr.airecord);
            }
            catch
            {
                MessageBox.Show("ClipCreate ERROR");
            }
        }
    }
}