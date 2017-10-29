using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml.Serialization;

namespace EEQG.Tools
{
    public class XmlSerialHelp
    {
        //定义Color属性的序列化为cat节点的属性         [XmlAttribute("color")] 
        //要求不序列化Speed属性         [XmlIgnore] 
        //设置Saying属性序列化为Xml子元素         [XmlElement("saying")] 
        public static void Serialize<T>(T obj, string filePath)
        {
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
            using (FileStream fs = new FileStream(filePath, FileMode.Create))
            {
                XmlSerializer formatter = new XmlSerializer(typeof(T));
                formatter.Serialize(fs, obj);
            }
        }
        //定义Color属性的序列化为cat节点的属性         [XmlAttribute("color")] 
        //要求不序列化Speed属性         [XmlIgnore] 
        //设置Saying属性序列化为Xml子元素         [XmlElement("saying")] 
        public static void SerializeAES<T>(T obj, string filePath)
        {
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
            using (FileStream fs = new FileStream(filePath, FileMode.Create))
            {
                var s = Serialize<T>(obj);
                var so = AESEncryption.AESEncrypt(s);
                BinaryWriter bw = new BinaryWriter(fs);
                bw.Write(so);
            }
        }
        public static T DeSerializeAES<T>(string filePath)
        {
            T obj;
            using (FileStream fs = new FileStream(filePath, FileMode.Open))
            {
                BinaryReader br = new BinaryReader(fs);
                var so = br.ReadBytes((int)fs.Length);
                var s = AESEncryption.AESDecrypt(so);
                obj = DeSerialize<T>(s);
            }
            return obj;
        }
        public static byte[] Serialize<T>(T obj)
        {

            using (MemoryStream fs = new MemoryStream())
            {
                XmlSerializer formatter = new XmlSerializer(typeof(T));
                formatter.Serialize(fs, obj);
                fs.Seek(0, SeekOrigin.Begin);
                byte[] bytes = new byte[fs.Length];
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
                XmlSerializer formatter = new XmlSerializer(typeof(T));
                obj = (T)formatter.Deserialize(fs);
            }
            return obj;
        }

        public static T DeSerialize<T>(string filePath)
        {
            T obj;
            using (FileStream fs = new FileStream(filePath, FileMode.Open))
            {
                XmlSerializer formatter = new XmlSerializer(typeof(T));
                obj = (T)formatter.Deserialize(fs);
                //if (obj is IDeserializationCallbackCustom)
                //{
                //    var zz = obj as IDeserializationCallbackCustom;
                //    zz.OnDeserializationCustom(param);
                //}
            }
            return obj;
        }
        #region SerializeString
        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string SerializeString<T>(T obj)
        {
            using (MemoryStream fs = new MemoryStream())
            {
                XmlSerializer formatter = new XmlSerializer(typeof(T));
                try
                {
                    formatter.Serialize(fs, obj);
                }
                catch (Exception ee)
                {
                    ee.ToString();
                }

                fs.Seek(0, SeekOrigin.Begin);
                byte[] bytes = fs.ToArray(); //new byte[fs.Length];
                //fs.Read(bytes, 0, bytes.Length);
                // 设置当前流的位置为流的开始 
                //fs.Seek(0, SeekOrigin.Begin); 
                return Encoding.UTF8.GetString(bytes);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="xmlstring"></param>
        /// <returns></returns>
        public static T DeSerializeString<T>(string xmlstring)
        {
            byte[] buffer = Encoding.UTF8.GetBytes(xmlstring);
            using (MemoryStream fs = new MemoryStream(buffer))
            {
                XmlSerializer formatter = new XmlSerializer(typeof(T));
                return (T)formatter.Deserialize(fs);
            }

        }
        #endregion
        public static T Clone<T>(T obj)
        {
            using (MemoryStream fs = new System.IO.MemoryStream())
            {
                XmlSerializer formatter = new XmlSerializer(typeof(T));
                formatter.Serialize(fs, obj);
                fs.Position = 0;
                return (T)formatter.Deserialize(fs);
            }
        }


    }
}
