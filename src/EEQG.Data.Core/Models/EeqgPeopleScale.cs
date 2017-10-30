using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace EEQG.Data.Models
{
    public class EeqgPeopleScale
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string ID { set; get; }

        [Required]
        public string EeqgPeopleID { get; set; }
        [ForeignKey(nameof(EeqgPeopleID))]
        public virtual EeqgPeople EeqgPeople { get; set; }
        /// 构造函数
        public EeqgPeopleScale()
        {
            CreatTime = DateTime.Now;
        }
        public EeqgPeopleScale(string num, string ty)
        {
            this.Num = num;
            ScaleType = ty;
            CreatTime = DateTime.Now;
        }

        public DateTime? CreatTime { set; get; }

        public String Num { set; get; }

        public string ScaleType { set; get; }

        public string ValueStr { get; set; }
        [NotMapped]
        public List<string> ValueList
        {
            get
            {
                if (string.IsNullOrEmpty(ValueStr))
                {
                    return new List<string>();
                }
                return ValueStr.Split(';').ToList();
            }
            set
            {
                if (value == null)
                {
                    ValueStr = "";
                }
                else
                {
                    ValueStr = string.Join(";", value);
                }

            }
        }




    }
}
