using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDCM.IDB.DAM
{
    public class CTDRecordMap
    {
        public string attr { get; set; }
        public string alias { get; set; }
        public string viewOrder { get; set; }
        public string dbOrder { get; set; }
        public bool hide { get; set; }
        public const string KeyName = "attr";
    }
}
