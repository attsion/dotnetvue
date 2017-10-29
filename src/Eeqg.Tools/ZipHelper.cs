using System;
using System.Collections.Generic;
using System.IO.Compression;
using System.Text;

namespace EEQG.Tools
{
    public static class ZipHelper
    {
        public static void CreatZip(string desFile, string SourcePath)
        {

            ZipFile.CreateFromDirectory(SourcePath, desFile, CompressionLevel.Optimal, false);

        }
        public static void ExtractZip(string zipFile, string DesPath)
        {
            try
            {
                ZipFile.ExtractToDirectory(zipFile, DesPath);
            }
            catch (Exception ee)
            {
                if (ee is System.IO.InvalidDataException)
                {
                    ExtractZipPassword(zipFile, DesPath, password: "fej121");
                }
                else
                {
                    throw new Exception("该文件可能损坏或被密码保护");
                }
            }
        }

        public static void CreatZipPassword(string desFile, string SourcePath,string password="fej121")
        {
            ICSharpCode.SharpZipLib.Zip.FastZip fastzip = new ICSharpCode.SharpZipLib.Zip.FastZip();
            fastzip.Password = password;
            fastzip.UseZip64 = ICSharpCode.SharpZipLib.Zip.UseZip64.Off;
            fastzip.CreateZip(desFile, SourcePath, true, "");
        }
        public static void ExtractZipPassword(string zipFile, string DesPath, string password = "fej121")
        {
            ICSharpCode.SharpZipLib.Zip.FastZip fastzip = new ICSharpCode.SharpZipLib.Zip.FastZip();
            fastzip.Password = password;
            fastzip.ExtractZip(zipFile, DesPath, "");
        }

    }
}
