using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IDCM.Base;
using IDCM.Base.Utils;

namespace IDCM.IDB.DAM
{
    public class CTDRecordDAM
    {
        /// <summary>
        /// 查询记录
        /// </summary>
        /// <returns></returns>
        public static List<CTDRecord> queryCTDRecord(IDBManager wsm, out string cmdstr,params long[] rids)
        {
            StringBuilder cmdBuilder = new StringBuilder("SELECT * FROM " + typeof(CTDRecord).Name);
            if (rids != null && rids.Length>0)
            {
                cmdBuilder.Append(" where ");
                cmdBuilder.Append(" ").Append(CTDRecord.KeyName).Append(" in (").Append(rids[0]);
                for (int i = 1; i < rids.Length; i++)
                {
                    cmdBuilder.Append(",").Append(rids[i]);
                }
                cmdBuilder.Append(");");
            }
            cmdstr = cmdBuilder.ToString();
            return DataSupporter.ListSQLQuery<CTDRecord>(wsm, cmdstr);
        }
        /// <summary>
        /// 查询记录
        /// </summary>
        /// <returns></returns>
        public static List<CTDRecord> queryCTDRecord(IDBManager wsm, int limit, int offset, out string cmdstr)
        {
            StringBuilder cmdBuilder = new StringBuilder("SELECT * FROM " + typeof(CTDRecord).Name);
            if (limit > 0 && offset > -1)
            {
                cmdBuilder.Append(" limit ").Append(limit).Append(" ").Append(" offset ").Append(offset);
            }
            cmdstr = cmdBuilder.ToString();
            return DataSupporter.ListSQLQuery<CTDRecord>(wsm, cmdstr);
        }
        /// <summary>
        /// 根据指定的SQL查询结果集
        /// </summary>
        /// <param name="nodeIds"></param>
        /// <returns></returns>
        public static List<CTDRecord> queryCTDRecordBySQL(IDBManager wsm, string whereCmd, long limit = 0, long offset = 0)
        {
            StringBuilder cmdBuilder = new StringBuilder("SELECT * FROM " + typeof(CTDRecord).Name);
            if (whereCmd != null)
            {
                int whereIndex = whereCmd.IndexOf(" where ", StringComparison.CurrentCultureIgnoreCase);
                int startIndex = " where ".Length + whereIndex > 0 ? whereIndex : 0;
                int limitIndex = whereCmd.IndexOf(" limit ", StringComparison.CurrentCultureIgnoreCase);
                int len = limitIndex > whereIndex ? limitIndex - limitIndex : whereCmd.Length - whereIndex;
                cmdBuilder.Append(" Where " + whereCmd.Substring(startIndex, len));
            }
            if (limit > 0 && offset > -1)
            {
                cmdBuilder.Append(" limit ").Append(limit).Append(" ").Append(" offset ").Append(offset);
            }
            return DataSupporter.ListSQLQuery<CTDRecord>(wsm, cmdBuilder.ToString());
        }

        /// <summary>
        /// 合并式插入或更新数据记录
        /// </summary>
        /// <param name="mapValues"></param>
        /// <returns></returns>
        public static long mergeRecord(IDBManager wsm, Dictionary<string, string> mapValues)
        {
            string rid = null;
            mapValues.TryGetValue(CTDRecord.KeyName, out rid);
            rid = rid == null ? DataSupporter.nextSeqID(wsm).ToString() : rid;
            StringBuilder cmdBuilder = new StringBuilder();
            cmdBuilder.Append("replace into " + typeof(CTDRecord).Name + "(" + CTDRecord.KeyName);
            foreach (string key in mapValues.Keys)
            {
                if (key.Equals(CTDRecord.KeyName))
                    continue;
                cmdBuilder.Append(",").Append(SQLiteUtil.sqliteEscape(key));
            }
            cmdBuilder.Append(") values (" + rid);
            foreach (KeyValuePair<string, string> kvpair in mapValues)
            {
                cmdBuilder.Append(",").Append("'" + SQLiteUtil.sqliteEscape(kvpair.Value) + "'");
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
        public static int updateAttrVal(IDBManager wsm,  long rid, Dictionary<string, string> mapValues)
        {
            if (mapValues == null || mapValues.Count < 1)
                throw new IDCMDataException("Illegal parameter for updateAttrVal(...)");
            KeyValuePair<string, string> fikv = mapValues.First();
            StringBuilder cmdBuilder = new StringBuilder();
            cmdBuilder.Append("update ").Append(typeof(CTDRecord).Name).Append(" set ");
            foreach (KeyValuePair<string, string> kvpair in mapValues)
            {
                if (kvpair.Key.Equals(CTDRecord.KeyName))
                    continue;
                if (!kvpair.Equals(fikv))
                    cmdBuilder.Append(",");
                cmdBuilder.Append(SQLiteUtil.sqliteEscape(kvpair.Key)).Append("='" + SQLiteUtil.sqliteEscape(kvpair.Value) + "'");
            }
            cmdBuilder.Append(" where " + CTDRecord.KeyName + "=" + rid);
            return DataSupporter.executeSQL(wsm, cmdBuilder.ToString());
        }
        /// <summary>
        /// 添加新数据记录
        /// </summary>
        /// <param name="lid"></param>
        /// <param name="plid"></param>
        /// <returns></returns>
        public static long addNewRecord(IDBManager wsm,  Dictionary<string, string> mapValues)
        {
            StringBuilder cmdBuilder = new StringBuilder();
            long rid = DataSupporter.nextSeqID(wsm);
            cmdBuilder.Append("insert into " + typeof(CTDRecord).Name + "(" + CTDRecord.KeyName);
            foreach (string key in mapValues.Keys)
            {
                if (key.Equals(CTDRecord.KeyName))
                    continue;
                cmdBuilder.Append(",").Append(SQLiteUtil.sqliteEscape(key));
            }
            cmdBuilder.Append(") values (" + rid);
            foreach (KeyValuePair<string, string> kvpair in mapValues)
            {
                cmdBuilder.Append(",").Append("'" + SQLiteUtil.sqliteEscape(kvpair.Value) + "'");
            }
            cmdBuilder.Append(")");
            DataSupporter.executeSQL(wsm, cmdBuilder.ToString());
            return rid;
        }
        /// <summary>
        /// 删除记录
        /// </summary>
        /// <param name="uid"></param>
        public static bool deleteRec(IDBManager wsm,  long rid)
        {
            string cmd = "delete from " + typeof(CTDRecord).Name + " where " + CTDRecord.KeyName + "=" + rid;
            return SQLiteUtil.checkExecuteOk(DataSupporter.executeSQL(wsm, cmd));
        }
    }
}
