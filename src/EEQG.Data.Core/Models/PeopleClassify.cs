using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace EEQG.Data.Models
{
    public class PeopleClassify
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string ID { get; set; }

        public virtual IEnumerable<EeqgPeople> Peoples { get; set; }
       
        public string Name { get; set; }
        public bool Hidden { get; set; }
        
       
    }

    public class PeopleDevClassify
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string ID { get; set; }
        
        public virtual IEnumerable<EeqgPeople> Peoples { get; set; }

        public string Name { get; set; }
        public bool Hidden { get; set; }
    }
}
