using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace EEQG.Data.Models
{
    public abstract class TransFile
    {
        public static readonly string waveExt = ".hdw";

        public virtual string WaveExt { get { return waveExt; } }

        public virtual string ReportExt { get { return ""; } }

        public virtual string WaveDirPath { get { return ""; } }//接收波形文件的dir

        public virtual string ReportDirPath { get { return ""; } }//应答文件的dir

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string ID { get; set; }

        

        /// <summary>
        /// 连接客户和服务器的好东西
        /// </summary>
        public string FileKey { get; set; }

        public string ReportTypeStr { get; set; }
        public DateTime CreatTime { get; set; }
        public string WaveID { get; set; }
        public string WaveFileName { get; set; }
        public string ReportFileName { get; set; }
        public long WaveFileSize { get; set; }
        public long WaveFileHaveSize { get; set; }
        public long ReportFileSize { get; set; }
        public long ReportFileHaveSize { get; set; }

        public string WaveNowSelect { get; set; }
        public DateTime? WaveCreatTime { get; set; }
        public string WaveDevice { get; set; }
        public string WaveDeviceNum { get; set; }
        public int? WaveFre { get; set; }
        public int? WaveChanel { get; set; }
        public int? WaveZTime { get; set; }
        public double? WaveLowPassFilter { get; set; }
        public double? WaveHeightPassFilter { get; set; }
        public double? WaveNoPassFilter { get; set; }
        public bool? TransFileDisable { get; set; }
        public bool? ByImpedanceTest { get; set; }
        [NotMapped]
        public ReportType ReportType
        {
            get
            {
                return new ReportType(ReportTypeStr);
            }
            set
            {
                ReportTypeStr = value.ToString();
            }
        }
        public abstract string WaveFilePath { get; }

        public abstract string ReportFilePath { get; }

       


        public double WaveFileHaveSizeMB { get { return WaveFileHaveSize * 1.0 / 1048576; } }

        public double WaveFileSizeMB { get { return WaveFileSize * 1.0 / 1048576; } }

        public int WavePrecent { get { return getPrecent(WaveFileHaveSize, WaveFileSize); } }

        public string WavePrecentString { get { return getPrecentString(WaveFileHaveSize, WaveFileSize); } }

        public double ReportFileHaveSizeMB { get { return ReportFileSize * 1.0 / 1048576; } }

        public double ReportFileSizeMB { get { return ReportFileHaveSize * 1.0 / 1048576; } }

        public int ReportPrecent { get { return getPrecent(ReportFileHaveSize, ReportFileSize); } }

        public string ReportPrecentString { get { return getPrecentString(ReportFileHaveSize, ReportFileSize); } }



        protected string getMbOfKb(long kb)
        {
            return Math.Round(kb * 1.0 / 1024 / 1024, 3).ToString() + "MB";
        }
        protected static int getPrecent(long location, long filesize)
        {
            return filesize == 0 ? 0 : (int)(location * 1.0 / filesize * 100);
        }
        protected static string getPrecentString(long location, long filesize)
        {
            return getPrecent(location, filesize).ToString() + "%";
        }
        protected static int? getIntR(object str)
        {
            try
            {
                if (str == null)
                {
                    return null;
                }
                return int.Parse(str.ToString());

            }
            catch
            {
                return null;
            }
        }
        protected static double? getDoubleR(object str)
        {
            try
            {
                if (str == null)
                {
                    return null;
                }
                return double.Parse(str.ToString());

            }
            catch
            {
                return null;
            }
        }
    }
}
