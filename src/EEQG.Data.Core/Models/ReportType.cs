
using EEQG.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EEQG.Data.Models
{
    public enum ReportNameEnum
    {
         抑郁症版本=1, 成人版本=2, 职业经理版本=3, 儿童版本=4, 小学版本=5, 初中版本=6,
         高中版本 =7, 成人系列=8, 大学生系列=9, 亚健康系列=10
    }
    public class ReportTypeItem
    {
        public string ID { get; protected set; }
        public string Name { get; protected set; }
        public bool Hidden { get; protected set; }
        public ReportTypeItem(string id, string name)
        {
            this.ID = id;
            this.Name = name;
            Hidden = false;
        }
        /// <summary>
        /// 重写了tostring显示name
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return this.Name;
        }
    }
    /// <summary>
    /// 报告类型
    /// </summary>
    public class ReportType
    {
        #region 报告库 Dictionary<ReportNameEnum, List<ReportTypeItem>> ReportTypeData

        public static Dictionary<ReportNameEnum, List<ReportTypeItem>> ReportTypeData = new Dictionary<ReportNameEnum, List<ReportTypeItem>>()
        {
              {
                    ReportNameEnum.抑郁症版本,new List<ReportTypeItem>()
                    {
                        new ReportTypeItem("抑郁症报告","抑郁症报告")
                    }
              },

              {
                    ReportNameEnum.成人版本,new List<ReportTypeItem>()
                    {
                        new ReportTypeItem("报告书简易版","报告书简易版"),
                        new ReportTypeItem("报告书简易版(小学生)","报告书简易版(小学生)"),
                        new ReportTypeItem("中高级管理人才报告书","中高级管理人才报告书"),
                        new ReportTypeItem("人才素质测评报告A","人才素质测评报告"),
                        new ReportTypeItem("人才素质测评报告B","人才素质测评报告B"),
                        new ReportTypeItem("人才素质测评报告C","人才素质测评报告C"),
                        new ReportTypeItem("职业倾向测评报告","职业倾向测评报告"),
                        new ReportTypeItem("工作风格测评报告","工作风格测评报告"),
                        new ReportTypeItem("亚健康测评报告","亚健康测评报告"),
                        new ReportTypeItem("北京人才中心版","北京人才中心版")
                    }
              },

              {
                    ReportNameEnum.职业经理版本,new List<ReportTypeItem>()
                    {
                        new ReportTypeItem("经理通用人才","经理通用人才"),
                        new ReportTypeItem("经理后备人才","经理后备人才"),
                        new ReportTypeItem("大学生职业倾向","大学生职业倾向")
                    }
              },
              {
                    ReportNameEnum.儿童版本,new List<ReportTypeItem>()
                    {
                       new ReportTypeItem("幼儿体验版报告","幼儿体验版报告"),
                       new ReportTypeItem("幼儿简易版报告","幼儿简易版报告"),
                       new ReportTypeItem("幼儿病理版报告","幼儿病理版报告"),
                       new ReportTypeItem("幼儿综合版报告","幼儿综合版报告")
                    }
              },
              {
                    ReportNameEnum.小学版本,new List<ReportTypeItem>()
                    {
                        new ReportTypeItem("小学综合版报告","小学综合版报告"),
                    }
              },

              {
                    ReportNameEnum.初中版本,new List<ReportTypeItem>()
                    {
                        new ReportTypeItem("初中生综合版报告","初中生综合版报告")
                    }
              },
              {
                    ReportNameEnum.高中版本,new List<ReportTypeItem>()
                    {
                         new ReportTypeItem("高中生综合版报告","高中综合版（填报志愿）"),
                         new ReportTypeItem("高中生综合版报告B","高中综合版"),
                         new ReportTypeItem("高中生综合版报告C","高中综合版（文理分科_限用）")
                    }
              },
              {
                    ReportNameEnum.成人系列,new List<ReportTypeItem>()
                    {
                        new ReportTypeItem("成人系列综合版报告","成人综合版（职业选择）"),
                        new ReportTypeItem("成人系列综合版报告B","成人综合版")
                    }
              },
              {
                    ReportNameEnum.大学生系列,new List<ReportTypeItem>()
                    {
                        new ReportTypeItem("大学生系列_职业选择","大学生综合版（职业选择）")
                    }
              },
              {
                    ReportNameEnum.亚健康系列,new List<ReportTypeItem>()
                    {
                        new ReportTypeItem("亚健康体检报告","亚健康体检报告")
                    }
              }
        };
        #endregion


        public static ReportType GetDefaultReportType(ReportNameEnum typesName)
        {
            if (ReportTypeData.ContainsKey(typesName))
            {
                var item = ReportTypeData[typesName];
                return new ReportType(typesName, item);
            }
            else
            {
                throw (new Exception("找不到" + typesName + "的报告类型"));
            }
        }

        public static List<string> GetSupportReportTypesNames()
        {
            var ls = Enum.GetNames(typeof(ReportNameEnum)).ToList();
           
            ls.Remove(ReportNameEnum.成人版本.ToString());
            return ls;
        }

        public static List<ReportTypeItem> GetDefaultReportItems(ReportNameEnum name)
        {
            if (ReportTypeData.ContainsKey(name))
            {
                return ReportTypeData[name];
            }
            else
            {
                return new List<ReportTypeItem>();
            }

        }
       
        public ReportNameEnum Name { get; set; }
       
        //public string NameStr { get; set; }
        /// <summary>
        /// 保存的是报告书id 可EF序列化 为了兼容性目前为public 应该为protect权限
        /// </summary>
        public EFSeriousStringCollection RegistReports { get; set; }


        public ReportType()
        {
            Name = ReportNameEnum.职业经理版本;
            RegistReports = new EFSeriousStringCollection(new List<string>());
        }
        public ReportType(ReportNameEnum name, List<string> ids)
        {
            Name = name;
            RegistReports = new EFSeriousStringCollection(ids);
        }
        public ReportType(ReportNameEnum name, List<ReportTypeItem> items)
        {
            Name = name;
            RegistReports = new EFSeriousStringCollection(items.Select(x => x.ID).ToList());
        }
        public ReportType(string s)
        {
            if (!string.IsNullOrEmpty(s))
            {
                string[] zzz = s.Split('-');
                Name = (ReportNameEnum)Enum.Parse(typeof(ReportNameEnum), zzz[0]);
                if (zzz.Length > 1)
                {
                    RegistReports = new EFSeriousStringCollection(zzz[1].Split(',').ToList());
                }
            }
        }


        public List<ReportTypeItem> GetChildReports()
        {
            var ls = new List<ReportTypeItem>();
            if (ReportTypeData.ContainsKey(Name))
            {
                var store = ReportTypeData[Name];
                foreach (var item in RegistReports)
                {
                    if (item != "小学生综合报告")
                    {
                        ls.Add(store.First(x => x.ID == item));
                    }
                    else
                    {
                        ls.Add(store.First());
                    }

                }

            }
            return ls;
        }
        public void SetChildReports(List<string> ids)
        {
            RegistReports = new EFSeriousStringCollection(ids);
        }
        public bool ContainsChildByID(string id)
        {
            return RegistReports.Contains(id);
        }
        public void ClearChilds()
        {
            RegistReports.Clear();
        }
        public ReportType Clone()
        {
            return XmlSerialHelp.Clone<ReportType>(this);
        }
        /// <summary>
        /// 重写了ToString 这个输出的是ID不是Name
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return Name.ToString() + "-" +  RegistReports.ToSplitString(',');
        }
        public string ToStringOnlyList()
        {
            return GetChildReports().Select(x => x.Name).ToSplitString(',');
        }
        /// <summary>
        /// 返回存储进数据库中的列表字符
        /// </summary>
        /// <returns></returns>
        public string ChildReportsInDbStr()
        {
            return RegistReports.SerializedValue;
        }


        public static string ToListString(List<ReportType> list)
        {
            if (list == null)
            {
                return "";
            }
            string ss = "";
            for (int i = 0; i < list.Count; i++)
            {
                ss += list[i].ToString();
                if (i != list.Count - 1)
                {
                    ss += ";";
                }
            }
            return ss;

        }
        public static List<ReportType> GetListFromString(string ss)
        {
            if (ss == "")
            {
                return new List<ReportType>();
            }
            string[] ass = ss.Split(';');
            List<ReportType> list = new List<ReportType>();
            for (int i = 0; i < ass.Length; i++)
            {
                ReportType rr = new ReportType(ass[i]);
                list.Add(rr);
            }
            return list;
        }


    }
}
