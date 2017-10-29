using System;
using System.Collections.Generic;
using System.Text;
using EEQG.Data.Models;
namespace EEQG.ServerData.Models
{
    public class EeqgPeople_Server:EeqgPeople
    {
        public virtual IEnumerable<TransFile_Service> TransFiles { get; set; }
    }
}
