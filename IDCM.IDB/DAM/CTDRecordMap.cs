using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDCM.IDB.DAM
{
    public class CTDRecordMap
    {
        private string attr { get; set; }
        private string alias { get; set; }
        private string viewOrder { get; set; }
        private string dbOrder { get; set; }
        private bool hide { get; set; }
        public const string KeyName = "attr";
    }
}
