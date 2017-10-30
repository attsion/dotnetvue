using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace EEQG.Data.Models
{
    public class EeqgPeople_Special
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string ID { get; set; }

        [Required]
        public string PeopleID { get; set; }
        [ForeignKey(nameof(PeopleID))]
        public virtual EeqgPeople People { get; set; }


        //经理人专项
        public string JL_DanWeiXingZhi { get; set; }
        
        public string JL_DanWeiGuiMo { get; set; }
        public string JL_DanWeiLeiXing { get; set; }
        //班主任专项

        public string JX_BanZhuRen { get; set; }
        public string JX_BanZhuRenTime { get; set; }
        public string JX_SuoJiaoKeMu { get; set; }
        public string JX_Jilv { get; set; }
        public string JX_TanXin { get; set; }
        public string JX_BanDuiHuoDong { get; set; }
        public string JX_XueShengGuanXi { get; set; }
        public string JX_HuoDongTaiDu { get; set; }
        public string JX_GuanLiNengLi { get; set; }
        public string JX_ZongTiPingJia { get; set; }
        public string JX_RenKeLianXi { get; set; }
        public string JX_YaLi { get; set; }
        public string JX_QiTaFangMian { get; set; }
        //绘画专项

        public string HH_HuiHuaNianLing { get; set; }
        public string HH_ShanChangLei { get; set; }
        public string HH_ShanChangDuiXiang { get; set; }
        public string HH_YiChuan { get; set; }
        public string HH_HuanJing { get; set; }
        public string HH_JiaoYu { get; set; }
        public string HH_JiYiWenZi { get; set; }
        public string HH_KongJianDingWei { get; set; }
        //舞蹈专项

        public string WD_WuDaoNianLing { get; set; }
        public string WD_WuDaoZhengShu { get; set; }
        public string WD_ShanChangWuDao { get; set; }
        public string WD_JiaShuWuDao { get; set; }
        public string WD_JiaShuGuanXi { get; set; }
        //围棋专项

        public string WQ_WeiQiNianLing { get; set; }
        public string WQ_WeiQiDengJi { get; set; }
        //舞蹈专项

        public string LY_ChuShengQingKuang { get; set; }
        public string LY_JuTiQingKuang { get; set; }
        public string LY_ZhiBingYuanYin { get; set; }
    }
}
