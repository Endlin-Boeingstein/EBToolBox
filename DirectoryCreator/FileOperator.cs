using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;

namespace DirectoryCreator
{
    //建立路径复制类
    public class FileOperator
    {
        /// <summary>
        /// Copy directory to target path
        /// </summary>
        /// <param name="source">the directory you want to copy</param>
        /// <param name="target">the target path</param>
        /// <param name="throwException">whether throw exceptions when it meet issues, if false, it will skip the files which has problem</param>
        public void CopyDirectory(string source, string target, bool throwException = false)
        {
            DirectoryInfo sor = new DirectoryInfo(source);
            DirectoryInfo fp = new DirectoryInfo(target + "\\" + sor.Name);
            if (!fp.Exists)
            {
                Directory.CreateDirectory(target + "\\" + sor.Name);
                target = target + "\\" + sor.Name;
            }
            DirectoryInfo sourceInfo = new DirectoryInfo(source);
            FileSystemInfo[] children = sourceInfo.GetFileSystemInfos();
            foreach (var child in children)
            {
                try
                {
                    //废弃代码///string targetName = Path.Combine(target, child.Name);
                    //废弃代码///string containerDirectory = Path.GetDirectoryName(targetName);
                    CopyDirectory(child.FullName, target, throwException);
                }
                catch// (Exception exception)
                {
                    //废弃代码///if (throwException)
                    //废弃代码///throw;
                    //废弃代码///else
                    //废弃代码///Console.WriteLine($"Copy file/folder failed {target}, here is the exception : {exception.ToString()}");
                    MessageBox.Show("CopyDirectory ERROR");
                }
            }
        }
    }
}