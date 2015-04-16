using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IDCM.IDB;

namespace IDCM.DynamicDB.DAM
{
    public class CustomTColDefDAM
    {
        public static List<CustomTColDef> loadTableCols(IDBManager dbm, string tableName)
        {
            string cmd = "SELECT * FROM CustomTColDef where TName='" + tableName + "' order by corder";
            return DataSupporter.ListSQLQuery<CustomTColDef>(dbm, cmd);
        }

    }
}
