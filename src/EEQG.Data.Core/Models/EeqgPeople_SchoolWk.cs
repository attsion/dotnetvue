using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace EEQG.Data.Models
{
    public class EeqgPeople_SchoolWk
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string ID { get; set; }

        [Required]
        public string PeopleID { get; set; }
        [ForeignKey(nameof(PeopleID))]
        public virtual EeqgPeople People { get; set; }


        #region 家庭信息
        public string FartherZhiYe { get; set; }
        public string FartherXueLi { get; set; }
        public string MortherZhiYe { get; set; }
        public string MorterXueLi { get; set; }
        public string FamilyEnviroment { get; set; }
        #endregion

        #region 学业信息
        public string CompanyOrSchool { get; set; }
        public string NianJi { get; set; }
        public string XueLi { get; set; }
        public string XueWei { get; set; }
        public string ChengJi { get; set; }
        public string HuoJiang { get; set; }
        public string XueKePianHao { get; set; }
        public string WenLiPianHao { get; set; }
        public string YuanXiZhuanYe { get; set; }
        public string SchoolZhiWu { get; set; }
        public string SchoolZhiWuDurationTime { get; set; }
        public string BiYeSchool { get; set; }
        public string XwZhiYe { get; set; }
        public string RuXue { get; set; }

        public string RiChangBiaoXian { get; set; }
        #endregion

        #region 工作信息
        public string ZhiYe { get; set; }
        public string GongZuoNianXian { get; set; }
        public string ZhiYeJingLi { get; set; }
        public string BuMen { get; set; }
        public string ZhiWu { get; set; }
        public string ZhiCheng { get; set; }
        public string GongZuoNeiRong { get; set; }
        #endregion
    }
}
