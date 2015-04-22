using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IDCM.DynamicDB.Core;
using IDCM.IDB;
using IDCM.DynamicDB.DAM;
using IDCM.Base.ComPO;
using System.Data;
using IDCM.Base;

namespace IDCM.DynamicDB
{
    public class DynamicDBManager
    {
        public DynamicDBManager(IDBManager dbm=null)
        {
            if (dbm != null)
                if (!DDAMBase.prepareTables(dbm))
                    throw new IDCMException("Failed to prepare tables for DynamicDBManager!");
        }

        public bool prepare(IDBManager dbm)
        {
            return DDAMBase.prepareTables(dbm);
        }
        public bool buildTable(IDBManager dbm, CustomTDS dts)
        {
            if (DDAMBase.buildCustomTable(dbm, dts))
            {
                ColumnMappingHolder.doCacheAttrDBMap(dbm, dts.TName);
                return true;
            }
            return false;
        }
        public bool existTable(IDBManager dbm,string tableName)
        {
            return CustomTableDefDAM.queryTable(dbm, tableName)!=null;
        }
        public Dictionary<string, int> getAttrViewMapping(string tableName, bool withInnerField = true)
        {
            if (withInnerField)
                return ColumnMappingHolder.getAttrViewMapping(tableName);
            else
                return ColumnMappingHolder.getCustomDefAttrViewMapping(tableName);
        }
        public List<CustomTColDef> getViewMappingAttrDef(IDBManager dbm, string tableName)
        {
            List<CustomTColDef> ctcds=CustomVColMapDAM.loadAllColDefs(dbm, tableName);
            return res;
        }
        /// <summary>
        /// 查询记录
        /// </summary>
        /// <param name="nodeIds"></param>
        /// <param name="rids"></param>
        /// <returns></returns>
        public DataTable queryCTDRecord(IDBManager wsm, string tableName, string rids, out string cmdstr)
        {
            return CTDRecordDAM.queryCTDRecord(wsm, tableName, rids,out cmdstr);
        }
        /// <summary>
        /// 查询记录
        /// </summary>
        /// <param name="nodeIds"></param>
        /// <param name="rids"></param>
        /// <returns></returns>
        public DataTable queryCTDRecord(IDBManager wsm, string tableName, int limit, int offset, out string cmdstr)
        {
            return CTDRecordDAM.queryCTDRecord(wsm, tableName, limit,offset,out cmdstr);
        }
        /// <summary>
        /// 根据指定的SQL查询结果集
        /// </summary>
        /// <param name="nodeIds"></param>
        /// <returns></returns>
        public DataTable queryCTDRecordBySQL(IDBManager wsm, string tableName, string whereCmd, long limit = 0, long offset = 0)
        {
            return CTDRecordDAM.queryCTDRecordBySQL(wsm, tableName, whereCmd,limit,offset);
        }

        /// <summary>
        /// 合并式插入或更新数据记录
        /// </summary>
        /// <param name="mapValues"></param>
        /// <returns></returns>
        public long mergeRecord(IDBManager wsm, string tableName, Dictionary<string, string> mapValues)
        {
            return CTDRecordDAM.mergeRecord(wsm, tableName, mapValues);
        }
        /// <summary>
        /// 完整更新数据记录
        /// </summary>
        /// <param name="uid"></param>
        /// <param name="cellVal"></param>
        /// <param name="attrName"></param>
        /// <returns></returns>
        public int updateAttrVal(IDBManager wsm, string tableName, long rid,string attrName,string attrVal)
        {
            Dictionary<string, string> mapValues = new Dictionary<string, string>(1);
            mapValues.Add(attrName, attrVal);
            return CTDRecordDAM.updateAttrVal(wsm, tableName, rid, mapValues);
        }

        /// <summary>
        /// 完整更新数据记录
        /// </summary>
        /// <param name="uid"></param>
        /// <param name="cellVal"></param>
        /// <param name="attrName"></param>
        /// <returns></returns>
        public int updateAttrVal(IDBManager wsm, string tableName, long rid, Dictionary<string, string> mapValues)
        {
            return CTDRecordDAM.updateAttrVal(wsm, tableName, rid,mapValues);
        }
        /// <summary>
        /// 添加新数据记录
        /// </summary>
        /// <param name="lid"></param>
        /// <param name="plid"></param>
        /// <returns></returns>
        public long addNewRecord(IDBManager wsm, string tableName, Dictionary<string, string> mapValues)
        {
            return CTDRecordDAM.addNewRecord(wsm, tableName, mapValues);
        }
        /// <summary>
        /// 删除记录
        /// </summary>
        /// <param name="uid"></param>
        public bool deleteRec(IDBManager wsm, string tableName, long rid)
        {
            return CTDRecordDAM.deleteRec(wsm, tableName, rid);
        }
    }
}
