using EEQG.Tools;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace EEQG.ServerData.Models
{
    public enum ClientTypeEnum
    {
        V1, V2, V3
    }
    /// <summary>
    /// 授权模型
    /// </summary>
    [Serializable]
    public class WorkStation
    {
        public static string confPathNew = AppDomain.CurrentDomain.BaseDirectory + @"data\License.lic";
        public static WorkStation LicenSys = new WorkStation() { CompanyID = "Sys", CompanyKey = "Sys", CompanyName = "脑象图测评中心" };
        public static WorkStation LicenReportSys = new WorkStation()
        {
            CompanyID = "SYS",
            CompanyName = "天津和德科技发展有限公司",

            Email = "hedepx@126.com",
            QQ = "qq",
            WeChat = "weichat",
            URL = "http://eeqg.cn",
            address = "天津市河西区大沽南路恒华大厦1号楼1408室",
            tel = "022-58383059/62",
            person = "admin",
            chuanzhen = "022-58383063",
            youzheng = "300000",
        };
        
        public string CompanyID { get; set; }
        public string CompanyName { get; set; }
        public string CompanyKey { get; set; }
        public ClientTypeEnum ClientType { get; set; }


        
        public string Email { get; set; }
       
        public string QQ { get; set; }
        
        public string WeChat { get; set; }
       
        public string URL { get; set; }
       
        public string address { get; set; }
        
        public string tel { get; set; }
        
        public string person { get; set; }
        
        public string chuanzhen { get; set; }
       
        public string youzheng { get; set; }

        //报告书读取字段
        public string ReportCompanyName
        {
            get
            {
                return CompanyName + "·脑象图测评中心";
            }
        }
        public string ReportAddress
        {
            get
            {
                return "地址：" + address;
            }
        }
        public string ReportTel
        {
            get
            {
                return "电话：" + tel;
            }
        }
        public string ReportChuanZhen
        {
            get
            {
                return "传真：" + chuanzhen;
            }
        }
        public string ReportEmail
        {
            get
            {
                return "邮箱：" + Email;
            }
        }

        public void Save(string path)
        {
            FileOperantion.CreatCompletePathOfFile(path);
            File.Delete(path);
            XmlSerialHelp.SerializeAES(this, path);
            //    if (ClientTypeStr == null) ClientTypeStr = "";
            //    XDocument doc = new XDocument(              
            //        new XDeclaration("1.0", "utf-8", "yes"),
            //        new XElement("config",                  
            //            new XElement("licence",             
            //                new XAttribute("key", CompanyKey),
            //                new XAttribute("ClientType", ClientTypeStr) 
            //                 ),
            //            new XElement("company",                
            //                new XAttribute("id", CompanyID),   
            //                new XAttribute("name", CompanyName) 
            //                 )
            //             )
            //);
            //    ///保存XML文件到指定地址  
            //    doc.Save(path);  
        }

        public static WorkStation Load(string path)
        {

            //XElement doc = XElement.Load(path);
            try
            {
                return XmlSerialHelp.DeSerializeAES<WorkStation>(path);
                //var _licenKey = doc.Elements("licence").First().Attribute("key").Value;
                //var _companyid = doc.Elements("company").First().Attribute("id").Value;// rwc.readxmlp("company", "id");
                //var _companyName = doc.Elements("company").First().Attribute("name").Value; //rwc.readxmlp("company", "name");
                //var _ClientType = EClientTypeEnum.职业经理版本;
                //string _ClientTypeStr = "";
                //if (doc.Element("licence").Attributes("ClientType").Count() > 0)
                //{
                //    _ClientTypeStr = doc.Element("licence").Attributes("ClientType").First().Value;
                //    if (_ClientTypeStr == "")
                //    {
                //        _ClientType = EClientTypeEnum.职业经理版本;
                //    }
                //    else
                //    {
                //        var str = AESEncryption.AESDecryptStr(_ClientTypeStr);
                //        str = str.Substring(_companyid.Length, str.Length - _companyid.Length);
                //        _ClientType = (EClientTypeEnum)Enum.Parse(typeof(EClientTypeEnum), str);
                //    }

                //}
                //return new XLicen() { CompanyID = _companyid, CompanyKey = _licenKey, CompanyName = _companyName, ClientTypeStr = _ClientTypeStr, ClientType = _ClientType };
            }
            catch (Exception ee)
            {
                ee.ToString();
                throw (new Exception("授权信息读取错误"));
            }
        }
        /// <summary>
        /// 这个函数读取本地版本 之所以不在local里读取是因为获取机器码太耗时。
        /// </summary>
        /// <returns></returns>
        public static ClientTypeEnum getClientTypeLocal()
        {
            var lic = Load(confPathNew);
            return lic.ClientType;
        }
        /// <summary>
        /// 这个函数读取本地名称。
        /// </summary>
        /// <returns></returns>
        public static string getClientNameLocal()
        {
            var lic = Load(confPathNew);
            return lic.CompanyName;
        }
    }
}
