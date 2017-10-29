using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using EEQG.Data.Models;
namespace EEQG.ServerData.Models
{

    public enum ServerFileStatusEnum
    {
        无数据,
        采集完毕,
        识图完毕,
        报告已审核
    }
    public class TransFile_Service:TransFile
    {
        
        public const string reportExt = ".hdr";
        public const string infoExt = ".info";

        public static readonly string WorkPath = AppDomain.CurrentDomain.BaseDirectory;
       

        public override string ReportExt { get { return reportExt; } }
        public virtual string InfoExt { get { return infoExt; } }

        public override string WaveDirPath { get { return WorkPath + @"wave"; } }
        public override string ReportDirPath { get { return WorkPath + @"report"; } }
        public string InfDirPath { get { return WorkPath + @"info"; } }




        [Required]
        public string PeopleID { get; set; }
        [ForeignKey(nameof(PeopleID))]
        public virtual EeqgPeople_Server PeopleInfo { get; set; }

        [Required]
        public string WorkStationID { get; set; }
        [ForeignKey(nameof(WorkStationID))]
        public WorkStation WorkStation { get; set; }

        [Required]
        public string WorkStationSenderID { get; set; }
        [ForeignKey(nameof(WorkStationSenderID))]
        public WorkStation WorkStationSender { get; set; }

        public string InfoFileName { get; set; }
        public long InfoFileSize { get; set; }

        public ServerFileStatusEnum Status { get; set; }
        public string RemoteIP { get; set; }

        public int SendReportCount { get; set; }

        public string TransFileFollow { get; set; }
        
        

        public DateTime? LastUpdateTime { get; set; }
        /// <summary>
        /// 数据属性 0：正常 1：waveid 重复 2：姓名和生日重复 3:waveid重复且姓名重复
        /// </summary>
        public int DataProp { get; set; }

        

        public List<string> TransFileFollowList
        {
            get
            {
                if (string.IsNullOrEmpty(TransFileFollow))
                {
                    return new List<string>();
                }
                return TransFileFollow.Split(';').ToList();
            }
        }
       
        public string WaveFilePathRelative { get { return string.Format("{0}\\{1}\\{2}", "wave", CreatTime.ToString("yyyyMMdd"), WaveFileName); } }
        public string ReportFilePathRelative { get { return string.Format("{0}\\{1}\\{2}", "report", CreatTime.ToString("yyyyMMdd"), ReportFileName); } }
        public string InfoFilePathRelative { get { return string.Format("{0}\\{1}\\{2}", "info", CreatTime.ToString("yyyyMMdd"), InfoFileName); } }

        public override  string WaveFilePath { get { return string.Format("{0}\\{1}", WorkPath, WaveFilePathRelative); } }
        public override string ReportFilePath { get { return string.Format("{0}\\{1}", WorkPath, ReportFilePathRelative); } }
        public string InfoFilePath { get { return string.Format("{0}\\{1}", WorkPath, InfoFilePathRelative); } }

        
    }
}
