using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace EEQG.Data.Models
{
    public class EeqgPeople_Hospital
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string ID { get; set; }

        [Required]
        public string PeopleID { get; set; }
        [ForeignKey(nameof(PeopleID))]
        public virtual EeqgPeople People { get; set; }

        #region 医院信息
        public string JianChaRen { get; set; }
        public string Zhuyuanhao { get; set; }
        public string Menzhenhao { get; set; }
        public string Keshi { get; set; }
        public string Bingqu { get; set; }
        public string Chuanghao { get; set; }
        public string Yishi { get; set; }
        public string Hezuochengdu { get; set; }
        public string Jianchacishu { get; set; }
        public string Jianchatiwei { get; set; }
        public string Bingshi { get; set; }
        public string Yongyao { get; set; }
        public string Linchuang { get; set; }
        public string Zhenduanyishi { get; set; }
        public string Yishizhicheng { get; set; }
        public string Naodiantu { get; set; }
        public string Naoxiangtu { get; set; }
        public string Naodixingtu { get; set; }
        public string Beizhu { get; set; }
        #endregion
    }
}
