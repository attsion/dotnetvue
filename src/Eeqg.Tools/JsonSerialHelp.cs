using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization.Json;
using System.IO;

namespace EEQG.Tools
{
    public class JsonSerialHelp
    {
        /// <summary>   
        /// JSON序列化   
        /// </summary>  
        public static string Serialize<T>(T t)
        {
            //DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(T));
            //MemoryStream ms = new MemoryStream();
            //ser.WriteObject(ms, t);
            //string jsonString = Encoding.UTF8.GetString(ms.ToArray());
            //ms.Close();
            //return jsonString;
            return Newtonsoft.Json.JsonConvert.SerializeObject(t);
        }

        /// <summary>  
        /// JSON反序列化
        /// </summary>  
        public static T DeSerialize<T>(string jsonString)
        {
            //DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(T));
            //MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(jsonString));
            //T obj = (T)ser.ReadObject(ms);
            //return obj;
            return Newtonsoft.Json.JsonConvert.DeserializeObject<T>(jsonString);
        }
        /// <summary>
        /// 克隆对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static T Clone<T>(T obj)
        {
            var str = JsonSerialHelp.Serialize(obj);
            return JsonSerialHelp.DeSerialize<T>(str);
        }
    }
}
