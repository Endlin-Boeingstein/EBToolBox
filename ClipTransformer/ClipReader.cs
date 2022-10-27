using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace ClipTransformer
{
    //建立元件读取类
    class ClipReader
    {
        //创建lr实例
        LibraryReader lr = new LibraryReader();
        //创建ddr实例
        DOMDocumentReader ddr = new DOMDocumentReader();
        //预置ca数组以记录所有元件名称（包括被引用的）
        public ArrayList ca = new ArrayList();
        //生成元件读取部分
        public void ClipRead(string Fpath)
        {
            try
            {
                //读取Library文件夹中元件以记录数组
                lr.LibraryRead(Fpath + "\\LIBRARY");
                //读取DOMDocument中的元件引用以记录数组
                ddr.DOMDocumentRead(Fpath + "\\DOMDocument.xml");
                //合并数组
                ca.AddRange(lr.ca);
                ca.AddRange(ddr.ca);
                //ca去重
                for (int i = 0; i < ca.Count; i++)
                {
                    for (int j = i + 1; j < ca.Count; j++)
                    {
                        if (ca[i].Equals(ca[j]))
                        {
                            ca.RemoveAt(j);
                            i = 0;
                            break;
                        }
                        else { }
                    }
                }
            }
            catch
            {
                MessageBox.Show("ClipRead ERROR");
            }
        }
    }
}