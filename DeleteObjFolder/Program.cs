using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading;

namespace HJN.DeleteFolder
{
    class Program
    {
        static long DirCount = 0;
        static long FileCount = 0;
        static long FileSize = 0;
        static void Main(string[] args)
        {
            int sleeptyime = 10;
            Console.WriteLine("本程序功能是删除VS生成的OBJ文件夹，如果不需要请在" + sleeptyime + "秒内退出本程序");
            Thread.Sleep(1000 * sleeptyime);

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Title = "Delete \\obj\\ folder ";
            string root = Environment.CurrentDirectory;
            DeleteFiles(root);

            Console.WriteLine("Delete total file:" + FileCount);
            Console.WriteLine("Delete total folder:" + DirCount);
            Console.WriteLine("Delete total Size:" + FileSize + "(KB)");
            Console.ReadKey();
        }

        /// <summary>
        /// 删除文件夹及子文件内文件
        /// </summary>
        /// <param name="str"></param>
        public static void DeleteFiles(string str)
        {
            if (!str.EndsWith("\\")) str += "\\";
            DirectoryInfo fatherFolder = new DirectoryInfo(str);
            if (str.IndexOf("\\obj\\") > 0)
            {
                //删除当前文件夹内文件
                FileInfo[] files = fatherFolder.GetFiles();
                foreach (FileInfo file in files)
                {
                    //string fileName = file.FullName.Substring((file.FullName.LastIndexOf("\\") + 1), file.FullName.Length - file.FullName.LastIndexOf("\\") - 1);
                    string fileName = file.Name;
                    try
                    {
                        File.Delete(file.FullName);
                        Console.WriteLine("Del file:" + file.FullName);
                        FileSize += (file.Length / 1024);
                        FileCount++;
                    }
                    catch (Exception)
                    {
                    }
                }
                //递归删除子文件夹内文件
                foreach (DirectoryInfo childFolder in fatherFolder.GetDirectories())
                {
                    DeleteFiles(childFolder.FullName);
                }
                fatherFolder.Delete();
                Console.WriteLine("Del dir:" + fatherFolder.FullName);
                DirCount++;
            }
            else
            {
                DirectoryInfo[] arr = fatherFolder.GetDirectories();
                foreach (DirectoryInfo di in arr)
                {
                    foreach (FileInfo file in di.GetFiles())
                    {
                        if (file.Name.EndsWith(".pdb"))
                        {
                            File.Delete(file.FullName);
                            Console.WriteLine("Del file:" + file.FullName);
                            FileCount++;
                        }
                    }
                    DeleteFiles(di.FullName);
                }
            }
        }
    }


}
