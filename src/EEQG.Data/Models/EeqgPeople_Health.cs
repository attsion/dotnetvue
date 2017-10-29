using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace EEQG.Data.Models
{
    public class EeqgPeople_Health
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string ID { get; set; }

        [Required]
        public string PeopleID { get; set; }
        [ForeignKey(nameof(PeopleID))]
        public virtual EeqgPeople People { get; set; }

       

        #region 健康信息
        public string YinShi { get; set; }
        public string XiYan { get; set; }
        public string XiYanLiang { get; set; }
        public string XiYanShi { get; set; }
        public string XiYanTimeDur { get; set; }
        public string YinJiu { get; set; }
        public string YinJiuZui { get; set; }
        public string YinJiuLiang { get; set; }
        public string YinJiuShi { get; set; }
        public string YinJiuTimeDur { get; set; }
        public string XueYa { get; set; }
        public string XueYaValue { get; set; }
        public string XueTang { get; set; }
        public string XueTangValue { get; set; }
        public string NeiKe { get; set; }
        public string NeiKeValue { get; set; }
        public string XueChangGui { get; set; }
        public string XueChangGuiValue { get; set; }
        public string NiaoChangGui { get; set; }
        public string NiaoChangGuiValue { get; set; }
        public string XueZhi { get; set; }
        public string XueZhiValue { get; set; }
        public string GanGong { get; set; }
        public string GanGongValue { get; set; }
        public string ShenGong { get; set; }
        public string ShenGongValue { get; set; }
        public string XGuang { get; set; }
        public string XGuangValue { get; set; }
        public string BChao { get; set; }
        public string BChaoValue { get; set; }
        public string NaoCT { get; set; }
        public string NaoCTValue { get; set; }
        public string NaoMR { get; set; }
        public string NaoMRValue { get; set; }
        public string XinDian { get; set; }
        public string XinDianValue { get; set; }
        public string JiaZuBingShi { get; set; }
        #endregion
    }
}
