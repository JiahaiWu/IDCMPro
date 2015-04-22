using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IDCM.IDB;
using IDCM.Base;

namespace IDCM.DynamicDB.DAM
{
    public class CustomVColMapDAM
    {
        public static List<CustomViewColMap> loadVisibleCols(IDBManager dbm, string tableName)
        {
            string cmd = "SELECT * FROM CustomVColMap where TName='" + tableName + "' order by vieworder";
            return DataSupporter.ListSQLQuery<CustomViewColMap>(dbm, cmd);
        }
        public static List<CustomTColDef> loadAllColDefs(IDBManager dbm, string tableName)
        {
            string cmd = "SELECT CustomTColDef.*,CustomVColMap.ViewOrder FROM CustomTColDef join CustomVColMap " +
                "on CustomVColMap.Attr=CustomTColDef.Attr and CustomVColMap.TName=CustomTColDef.TName "+
                "and CustomTColDef.TName='" + tableName + "' order by CustomVColMap.vieworder";
            return DataSupporter.ListSQLQuery<CustomTColDef>(dbm, cmd);
        }
        public static bool updateViewOrder(IDBManager dbm, string tableName, Dictionary<string, int> mapValues)
        {
            if (mapValues == null || mapValues.Count < 1)
                throw new IDCMDataException("Illegal paramters for updateViewOrder(...)");
            StringBuilder cmds = new StringBuilder();
            foreach(KeyValuePair<string,int> kvpair in mapValues)
            {
                cmds.Append("replace into " + typeof(CustomViewColMap).Name + "(TName,Attr,ViewOrder) values('");
                cmds.Append(tableName).Append("','").Append(kvpair.Key).Append("',").Append(kvpair.Value).Append(");");
            }
            int res = DataSupporter.executeSQL(dbm, cmds.ToString());
            return DataSupporter.checkExecuteOk(res);
        }
        public static bool updateViewOrder(IDBManager dbm, string tableName, string name,int viewOrder)
        {
            StringBuilder cmds = new StringBuilder();
            cmds.Append("replace into " + typeof(CustomViewColMap).Name + "(TName,Attr,ViewOrder) values('");
            cmds.Append(tableName).Append("','").Append(name).Append("',").Append(viewOrder).Append(");");
            int res = DataSupporter.executeSQL(dbm, cmds.ToString());
            return DataSupporter.checkExecuteOk(res);
        }
    }
}
