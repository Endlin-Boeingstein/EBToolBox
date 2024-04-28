using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static EBToolBox.Form1;

namespace Res2Ext
{
    //建立加密元件创制类
    class EncryptClipCreator
    {
        //创建位图元件创建对象
        public ImageClipCreator ic = new ImageClipCreator();
        //创建动画元件创建对象
        public AnimateClipCreator ac = new AnimateClipCreator();
        //加密位图名
        public string encrypt_sprite;
        //生成加密元件创建部分
        public void EncryptClipCreate(string Fpath, string Dpath)
        {
            try
            {
                //加密信息选项收录
                if (!form1.radioButton31.Checked && form1.radioButton32.Checked) { }
                else if(form1.radioButton31.Checked && !form1.radioButton32.Checked)
                {
                    //记录加密位图名称
                    encrypt_sprite = form1.textBox20.Text;
                    ic.ImageClipCreate(Fpath, encrypt_sprite, Dpath);
                    ac.AnimateClipCreate(Fpath, ic.icname, Dpath);
                }
            }
            catch
            {
                MessageBox.Show("EncryptClipCreate ERROR");
            }
        }

    }
}