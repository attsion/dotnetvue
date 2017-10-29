using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace EEQG.Tools
{
    public static class FileOperantion
    {
        // ======================================================
        // 实现一个静态方法将指定文件夹下面的所有内容copy到目标文件夹下面
        // 如果目标文件夹为只读属性就会报错。

        // ======================================================
        public static void CopyDir(string srcPath, string aimPath)
        {
            try
            {
                // 检查目标目录是否以目录分割字符结束如果不是则添加之
                if (aimPath[aimPath.Length - 1] != Path.DirectorySeparatorChar)
                    aimPath += Path.DirectorySeparatorChar;
                // 判断目标目录是否存在如果不存在则新建之
                if (!Directory.Exists(aimPath)) Directory.CreateDirectory(aimPath);
                // 得到源目录的文件列表，该里面是包含文件以及目录路径的一个数组
                // 如果你指向copy目标文件下面的文件而不包含目录请使用下面的方法
                // string[] fileList = Directory.GetFiles(srcPath);
                string[] fileList = Directory.GetFileSystemEntries(srcPath);
                // 遍历所有的文件和目录
                foreach (string file in fileList)
                {
                    // 先当作目录处理如果存在这个目录就递归Copy该目录下面的文件
                    if (Directory.Exists(file))
                        CopyDir(file, aimPath + Path.GetFileName(file));
                    // 否则直接Copy文件
                    else
                        File.Copy(file, aimPath + Path.GetFileName(file), true);
                }
            }
            catch (Exception e)
            {
                throw (e);
                //MessageBox.Show(e.ToString());
            }
        }


        // ======================================================
        // 实现一个静态方法将指定文件夹下面的所有内容Detele
        // 测试的时候要小心操作，删除之后无法恢复。

        // ======================================================
        public static void DeleteDir(string aimPath)
        {
            try
            {
                if (!Directory.Exists(aimPath))
                {
                    return;
                }
                if (aimPath.Last() == '\\')
                {
                    return;
                }
                // 检查目标目录是否以目录分割字符结束如果不是则添加之
                if (aimPath[aimPath.Length - 1] != Path.DirectorySeparatorChar)
                    aimPath += Path.DirectorySeparatorChar;
                // 得到源目录的文件列表，该里面是包含文件以及目录路径的一个数组
                // 如果你指向Delete目标文件下面的文件而不包含目录请使用下面的方法
                // string[] fileList = Directory.GetFiles(aimPath);
                string[] fileList = Directory.GetFileSystemEntries(aimPath);
                // 遍历所有的文件和目录
                foreach (string file in fileList)
                {
                    // 先当作目录处理如果存在这个目录就递归Delete该目录下面的文件
                    if (Directory.Exists(file))
                    {
                        DeleteDir(aimPath + Path.GetFileName(file));
                    }
                    // 否则直接Delete文件
                    else
                    {
                        File.Delete(aimPath + Path.GetFileName(file));
                    }
                }
                //删除文件夹
                System.IO.Directory.Delete(aimPath, true);
            }
            catch (Exception e)
            {
                throw (e);
                //MessageBox.Show(e.ToString());
            }
        }

        public static void ClearDir(string path)
        {
            DeleteDir(path);
            Directory.CreateDirectory(path);
        }

        /// <summary>
        /// 创建一个文件的完整路径 防止路径不完整
        /// </summary>
        /// <param name="filePath"></param>
        public static void CreatCompletePathOfFile(string filePath)
        {
            string dpath = filePath.Substring(0, filePath.LastIndexOf('\\') + 1);
            if (!Directory.Exists(dpath))
            {
                Directory.CreateDirectory(dpath);
            }
        }
        /// <summary>
        /// 会自动将路径补充完整
        /// </summary>
        /// <param name="r"></param>
        /// <param name="desc"></param>
        public static void MoveAuto(string r, string desc)
        {
            CreatCompletePathOfFile(desc);
            File.Move(r, desc);
        }
        /// <summary>
        /// 会自动将路径补充完整
        /// </summary>
        /// <param name="r"></param>
        /// <param name="desc"></param>
        public static void CopyAuto(string r, string desc)
        {
            CreatCompletePathOfFile(desc);
            File.Copy(r, desc);
        }
    }
}
