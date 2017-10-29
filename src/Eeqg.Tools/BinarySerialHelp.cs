using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;

namespace EEQG.Tools
{
    public static class BinarySerialHelp
    {
        public static bool Serialize(object obj, string filePath)
        {
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }

            Stream stream = new FileStream(filePath, FileMode.OpenOrCreate, FileAccess.Write, FileShare.None);
            try
            {

                IFormatter formatter = new BinaryFormatter();
                //通过formatter对象以二进制格式将obj对象序列化后到文件MyFile.bin中
                formatter.Serialize(stream, obj);

                return true;
            }
            catch (Exception ee)
            {
                throw (ee);
            }
            finally
            {
                stream.Close();
            }
        }
        public static byte[] Serialize<T>(T obj)
        {

            using (MemoryStream fs = new MemoryStream())
            {
                IFormatter formatter = new BinaryFormatter();
                formatter.Serialize(fs, obj);
                byte[] bytes = new byte[fs.Length];
                fs.Seek(0, SeekOrigin.Begin);
                fs.Read(bytes, 0, bytes.Length);
                // 设置当前流的位置为流的开始 
                //fs.Seek(0, SeekOrigin.Begin); 
                return bytes;
            }
        }
        public static T DeSerialize<T>(byte[] datas)
        {
            T obj;
            using (MemoryStream fs = new MemoryStream(datas))
            {
                fs.Seek(0, SeekOrigin.Begin);
                IFormatter formatter = new BinaryFormatter();
                obj = (T)formatter.Deserialize(fs);
            }
            return obj;
        }
        public static T DeSerialize<T>(string filePath, SerializationBinder binder)
        {
            T obj = default(T);
            try
            {
                if (!File.Exists(filePath))
                {
                    return obj;
                }
                IFormatter formatter = new BinaryFormatter();
                formatter.Binder = binder;


                using (Stream stream2 = new FileStream(filePath, FileMode.Open,
                FileAccess.Read, FileShare.None))
                {
                    obj = (T)formatter.Deserialize(stream2);
                }
                return obj;
            }
            catch (Exception ee)
            {
                var str = ee.ToString();
                throw (ee);
            }
        }

        public static T Clone<T>(T obj)
        {
            using (MemoryStream fs = new System.IO.MemoryStream())
            {
                IFormatter formatter = new BinaryFormatter();
                formatter.Serialize(fs, obj);
                fs.Position = 0;
                return (T)formatter.Deserialize(fs);
            }
        }

        //public static bool SerializeGZip(object obj, string filePath)
        //{
        //    if (File.Exists(filePath))
        //    {
        //        File.Delete(filePath);
        //    }

        //    try
        //    {
        //        using (Stream fstream = new FileStream(filePath, FileMode.OpenOrCreate, FileAccess.Write, FileShare.None))
        //        {
        //            using (var mstream = new MemoryStream())
        //            {
        //                IFormatter formatter = new BinaryFormatter();
        //                //通过formatter对象以二进制格式将obj对象序列化后到文件MyFile.bin中
        //                formatter.Serialize(mstream, obj);
        //                mstream.Flush();
        //                using (var gstream = new GZipStream(mstream, CompressionMode.Compress))
        //                {
        //                    gstream.CopyTo(fstream);
        //                }
        //            }
        //        }



        //        return true;
        //    }
        //    catch (Exception ee)
        //    {
        //        throw (ee);
        //    }
        //    finally
        //    {

        //    }
        //}


    }
}
