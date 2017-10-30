using System;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections;
using System.Collections.Generic;

namespace EEQG.Data.Models
{
    public class EeqgPeople
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string ID { get; set; }

        public string ClassifyID { get; set; }
        [ForeignKey(nameof(ClassifyID))]
        public virtual PeopleClassify Classify { get; set; }

        public virtual IEnumerable<PeopleDevClassify> DevClassifys { get; set; }

        [Required]
        public string Info_HealthID { get; set; }
        [ForeignKey(nameof(Info_HealthID))]
        public virtual EeqgPeople_Health Info_Health { get; set; }

        [Required]
        public string Info_HospitalID { get; set; }
        [ForeignKey(nameof(Info_HospitalID))]
        public virtual EeqgPeople_Health Info_Hospital { get; set; }

        [Required]
        public string Info_SchoolWkID { get; set; }
        [ForeignKey(nameof(Info_SchoolWkID))]
        public virtual EeqgPeople_Health Info_SchoolWk { get; set; }

        [Required]
        public string Info_SpecialID { get; set; }
        [ForeignKey(nameof(Info_SpecialID))]
        public virtual EeqgPeople_Health Info_Special { get; set; }

        public virtual IEnumerable<EeqgPeopleScale> Scales { get; set; }

        

        #region 基本信息
        [Required]
        public string Num { get; set; }
        public string Name { get; set; }
        public string Sex { get; set; }
        public DateTime? Born { get; set; }
        public string GuoJi { get; set; }
        public string ZongJiao { get; set; }
        public string XueXing { get; set; }
        public string ZuoYouLi { get; set; }
        public string JianKang { get; set; }
        public string TeChang { get; set; }
        public string HunYin { get; set; }
        #endregion

        #region 联系信息
        public string Tel { get; set; }
        public string EMail { get; set; }
        public string Address { get; set; }
        #endregion

        #region 测试信息
        public string CaiJiDianNum { get; set; }
        public string MyClass { get; set; }
        public string DevClass { get; set; }
        public string JiaoFei { get; set; }
        public string CaiJiRen { get; set; }
        public string BeiZhuSys { get; set; }
        public string SQReport { get; set; }
        #endregion

        





    }
}
