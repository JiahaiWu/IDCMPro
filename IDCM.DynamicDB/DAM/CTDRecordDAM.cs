using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using IDCM.IDB;
using IDCM.DynamicDB.ComPO;

namespace IDCM.DynamicDB.DAM
{
    public class CTDRecordDAM
    {
        /// <summary>
        /// 查询记录
        /// </summary>
        /// <param name="nodeIds"></param>
        /// <param name="rids"></param>
        /// <returns></returns>
        public static DataTable queryCTDRecord(IDBManager wsm,string tableName, string rids, out string cmdstr)
        {
            StringBuilder cmdBuilder = new StringBuilder("SELECT * FROM " + tableName);
            if (rids != null)
            {
                cmdBuilder.Append(" where ");
                if (rids != null)
                    cmdBuilder.Append(" ").Append(CTDRecordA.CTD_RID).Append(" in (").Append(rids).Append(") ");
            }
            cmdstr = cmdBuilder.ToString();
            return DataSupporter.SQLQuery(wsm, cmdstr);
        }
        /// <summary>
        /// 查询记录
        /// </summary>
        /// <param name="nodeIds"></param>
        /// <param name="rids"></param>
        /// <returns></returns>
        public static DataTable queryCTDRecord(IDBManager wsm, string tableName, int limit,int offset, out string cmdstr)
        {
            StringBuilder cmdBuilder = new StringBuilder("SELECT * FROM " + tableName);
            if (limit>0 && offset>-1)
            {
                cmdBuilder.Append(" limit ").Append(limit).Append(" ").Append(" offset ").Append(offset);
            }
            cmdstr = cmdBuilder.ToString();
            return DataSupporter.SQLQuery(wsm, cmdstr);
        }
        /// <summary>
        /// 根据指定的SQL查询结果集
        /// </summary>
        /// <param name="nodeIds"></param>
        /// <returns></returns>
        public static DataTable queryCTDRecordBySQL(IDBManager wsm, string tableName, string whereCmd, long limit = 0, long offset = 0)
        {
            StringBuilder cmdBuilder = new StringBuilder("SELECT * FROM " + tableName);
            if(whereCmd!=null)
            {
                int whereIndex = whereCmd.IndexOf(" where ", StringComparison.CurrentCultureIgnoreCase);
                int startIndex=" where ".Length+ whereIndex>0?whereIndex:0;
                int limitIndex = whereCmd.IndexOf(" limit ", StringComparison.CurrentCultureIgnoreCase);
                int len=limitIndex>whereIndex?limitIndex-limitIndex:whereCmd.Length-whereIndex;
                cmdBuilder.Append(" Where "+whereCmd.Substring(startIndex,len));
            }
            if (limit > 0 && offset > -1)
            {
                cmdBuilder.Append(" limit ").Append(limit).Append(" ").Append(" offset ").Append(offset);
            }
            return DataSupporter.SQLQuery(wsm, cmdBuilder.ToString());
        }

        /// <summary>
        /// 合并式插入或更新数据记录
        /// </summary>
        /// <param name="mapValues"></param>
        /// <returns></returns>
        public static long mergeRecord(IDBManager wsm, string tableName,Dictionary<string, string> mapValues)
        {
            string rid=null;
            mapValues.TryGetValue(CTDRecordA.CTD_RID,out rid);
            rid = rid == null ? DataSupporter.nextSeqID(wsm).ToString() : rid;
            StringBuilder cmdBuilder = new StringBuilder();
            cmdBuilder.Append("replace into " + tableName + "(" + CTDRecordA.CTD_RID);
            foreach (string key in mapValues.Keys)
            {
                if (key.Equals(CTDRecordA.CTD_RID))
                    continue;
                cmdBuilder.Append(",").Append(key);
            }
            cmdBuilder.Append(") values (" + rid);
            foreach (KeyValuePair<string, string> kvpair in mapValues)
            {
                cmdBuilder.Append(",").Append("'" + kvpair.Value + "'");
            }
            cmdBuilder.Append(")");
            DataSupporter.executeSQL(wsm, cmdBuilder.ToString());
            return Convert.ToInt64(rid);
        }

        /// <summary>
        /// 完整更新数据记录
        /// </summary>
        /// <param name="uid"></param>
        /// <param name="cellVal"></param>
        /// <param name="attrName"></param>
        /// <returns></returns>
        public static int updateAttrVal(IDBManager wsm, string tableName, long rid, Dictionary<string, string> mapValues)
        {
            if (mapValues == null || mapValues.Count < 1)
                throw new IDCMDataException("Illegal parameter for updateAttrVal(...)");
            KeyValuePair<string, string> fikv = mapValues.First();
            StringBuilder cmdBuilder = new StringBuilder();
            cmdBuilder.Append("update ").Append(tableName).Append(" set ");
            foreach (KeyValuePair<string, string> kvpair in mapValues)
            {
                if (kvpair.Key.Equals(CTDRecordA.CTD_RID))
                    continue;
                if(!kvpair.Equals(fikv))
                    cmdBuilder.Append(",");
                cmdBuilder.Append(kvpair.Key).Append("='" + kvpair.Value + "'");
            }
            cmdBuilder.Append(" where " + CTDRecordA.CTD_RID + "=" + rid);
            return DataSupporter.executeSQL(wsm, cmdBuilder.ToString());
        }
        /// <summary>
        /// 添加新数据记录
        /// </summary>
        /// <param name="lid"></param>
        /// <param name="plid"></param>
        /// <returns></returns>
        public static long addNewRecord(IDBManager wsm, string tableName, Dictionary<string, string> mapValues)
        {
            StringBuilder cmdBuilder = new StringBuilder();
            long rid = DataSupporter.nextSeqID(wsm);
            cmdBuilder.Append("insert into " + tableName + "(" + CTDRecordA.CTD_RID);
            foreach (string key in mapValues.Keys)
            {
                if (key.Equals(CTDRecordA.CTD_RID))
                    continue;
                cmdBuilder.Append(",").Append(key);
            }
            cmdBuilder.Append(") values (" + rid);
            foreach (KeyValuePair<string, string> kvpair in mapValues)
            {
                cmdBuilder.Append(",").Append("'" + kvpair.Value + "'");
            }
            cmdBuilder.Append(")");
            DataSupporter.executeSQL(wsm, cmdBuilder.ToString());
            return rid;
        }
        /// <summary>
        /// 删除记录
        /// </summary>
        /// <param name="uid"></param>
        public static bool deleteRec(IDBManager wsm, string tableName, long rid)
        {
            string cmd = "delete from " + tableName + " where " + CTDRecordA.CTD_RID + "=" + rid;
            return DataSupporter.checkExecuteOk(DataSupporter.executeSQL(wsm, cmd));
        }
    }
}
