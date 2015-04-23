using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Concurrent;
using IDCM.Base.Utils;
using IDCM.Base;
using System.Reflection;

namespace IDCM.IDB.DAM
{
    public class CTDRecordMapDAM
    {

        /// <summary>
        /// 获取已经被缓存的用户浏览字段~预览界面位序的映射关系。
        /// 说明：
        /// 1.本方法返回可见的[用户浏览字段名，预览界面位序]的映射关系。
        /// 2.数据库字段映射位序的值自0计数。
        /// 3.如果外部存在批量的字段映射匹配需要，需外部缓冲，重复请求该方法会重复创建对象资源。
        /// 4.返回的字典主键顺序以用户界面中字段列加载顺序为参照。
        /// </summary>
        /// <returns></returns>
        public static Dictionary<string, object> reverseAliasToAttrMapping(Dictionary<string, object> mapVals)
        {
            CTDRecordMap map = null;
            Dictionary<string, object> res = new Dictionary<string, object>();
            foreach (KeyValuePair<string, object> kvpair in mapVals)
            {
                if (CTDAliasMapping.TryGetValue(kvpair.Key,out map))
                {
                    res[map.attr] = kvpair.Value;
                }
            }
            return res;
        }
        /// <summary>
        /// 获取预览字段集序列
        /// 说明:
        /// 1.如果外部存在批量的字段映射匹配需要，需外部缓冲，重复请求该方法会重复创建对象资源。
        /// </summary>
        /// <returns></returns>
        public static Dictionary<string,string> getViewAliasAndAttrs(bool withInnerField = true)
        {
            Dictionary<string,string> res = new Dictionary<string,string>();
            foreach (KeyValuePair<string,CTDRecordMap> kvpair in CTDAliasMapping)
            {
                if (withInnerField | !kvpair.Value.hide)
                    res.Add(kvpair.Key, kvpair.Value.attr);
            }
            return res;
        }

        public static List<CTDRecordMap> loadRecordCols(IDBManager dbm,bool hide)
        {
            string cmd = "SELECT * FROM " + typeof(CTDRecordMap).Name + " where hide=@hide order by vieworder";
            cmd = SQLiteUtil.parameterizedSQLEscape(cmd, hide);
            return DataSupporter.ListSQLQuery<CTDRecordMap>(dbm, cmd);
        }
        public static List<CTDRecordMap> loadRecordDefs(IDBManager dbm)
        {
            string cmd = "SELECT * FROM " + typeof(CTDRecordMap).Name + " order by vieworder";
            return DataSupporter.ListSQLQuery<CTDRecordMap>(dbm, cmd);
        }

        public static bool updateViewOrder(IDBManager dbm, string name, int viewOrder,bool hide)
        {
            string cmds="replace into " + typeof(CTDRecordMap).Name + "(Attr,ViewOrder,hide) values(@attr,@viewOrder,@hide);";
            cmds = SQLiteUtil.parameterizedSQLEscape(cmds, name, viewOrder, hide);
            int res = DataSupporter.executeSQL(dbm, cmds.ToString());
            return SQLiteUtil.checkExecuteOk(res);
        }
        public static bool updateViewOrder(IDBManager dbm, string name, int viewOrder)
        {
            string cmds = "replace into " + typeof(CTDRecordMap).Name + "(Attr,ViewOrder,hide) values(@attr,@viewOrder);";
            cmds = SQLiteUtil.parameterizedSQLEscape(cmds, name, viewOrder);
            int res = DataSupporter.executeSQL(dbm, cmds.ToString());
            return SQLiteUtil.checkExecuteOk(res);
        }

        
        /// <summary>
        /// 数据存储的字段名与[数据存储位序，预览界面位序]的双层映射关系存储集合对象
        /// </summary>
        protected static ConcurrentDictionary<string, CTDRecordMap> CTDAttrMapping = new ConcurrentDictionary<string, CTDRecordMap>();
        protected static ConcurrentDictionary<string, CTDRecordMap> CTDAliasMapping = new ConcurrentDictionary<string, CTDRecordMap>();
    }
}
