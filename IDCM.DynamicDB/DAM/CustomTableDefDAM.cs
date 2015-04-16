using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IDCM.IDB;
using IDCM.Base.ComPO;

namespace IDCM.DynamicDB.DAM
{
    public class CustomTableDefDAM
    {
        /// <summary>
        /// 查询所有数据表定义对象
        /// </summary>
        /// <param name="picker"></param>
        /// <param name="refresh"></param>
        /// <returns></returns>
        public static List<CustomTableDef> loadTables(IDBManager dbm)
        {
            string cmd = "SELECT * FROM CustomTableDef";
            return DataSupporter.ListSQLQuery<CustomTableDef>(dbm, cmd);
        }
    }
}
