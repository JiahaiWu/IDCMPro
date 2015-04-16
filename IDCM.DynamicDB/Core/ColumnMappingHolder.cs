using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IDCM.IDB;
using IDCM.DynamicDB.ComPO;
using System.Collections.Concurrent;
using IDCM.DynamicDB.DAM;
using IDCM.Base;

namespace IDCM.DynamicDB.Core
{
    /// <summary>
    /// 本类定义用于支持动态数据表的存储字段与显示字段之间的映射关系的动态缓存实现
    /// 说明：
    /// 1.基于ColumnMapping对象实例，记录全局数据表中的各个数据字段名与[数据存储，预览界面]的映射关系
    /// 2.由用户自定义的动态数据表在前端预览的界面中，分为首选字段和备选字段两个部分,字段显示顺序要有稳定性。
    /// 3.首选字段的显示序号大于0时视为首选可见字段，小于0时视为备选隐藏字段，等于0时为保持隐藏字段。
    /// 4.动态数据表的数据存储的字段顺序相对固定，但字段名称区别于用户显示的数据字段名，用户自定义的数据字段名均有[前缀和]后缀。
    /// 5.对于无有[前缀和]后缀的数据字段一律视为内置的字段项，由创建程序内嵌生成与内部管理维护。
    /// 6.本类缓存实现旨在为用户提供虚拟字段与动态显示顺序设定特性下的快速定位数据库存储记录的字段表示的映射对照。
    /// </summary>
    class ColumnMappingHolder
    {

        /// <summary>
        /// 缓存数据字段映射关联关系
        /// 说明：
        /// 1.该方法用于存储及刷新CTDRecord相关的字段映射关系缓存
        /// </summary>
        internal static void doCacheAttrDBMap(IDBManager dbm)
        {
            List<CustomTableDef> tables = CustomTableDefDAM.loadTables(dbm);
            foreach (CustomTableDef table in tables)
            {
                doCacheAttrDBMap(dbm, table.TName);
            }
        }
        /// <summary>
        /// 缓存数据字段映射关联关系
        /// 说明：
        /// 1.该方法用于存储及刷新CTDRecord相关的字段映射关系缓存
        /// </summary>
        /// <param name="dbm"></param>
        /// <param name="tableName"></param>
        internal static void doCacheAttrDBMap(IDBManager dbm, string tableName)
        {
            List<CustomTColDef> ctcds = CustomTColDefDAM.loadTableCols(dbm, tableName);
            Dictionary<string, int> ctcms = CustomVColMapDAM.loadVisibleCols(dbm, tableName).ToDictionary(rs => rs.Attr, rs => rs.ViewOrder);
            TColumnMapping tcm = null;
            TableAttrMapping.TryRemove(tableName, out tcm);
            if (tcm != null)
                tcm.Clear();
            else
                tcm = new TColumnMapping();
            foreach (CustomTColDef ctcd in ctcds)
            {
                int viewOrder = AttrCorderConverter.KeepHide.Value;
                if (!ctcms.TryGetValue(ctcd.Attr, out viewOrder))
                {
                    if (!ctcd.Attr.Equals(CTDRecordA.CTD_RID))
                        viewOrder = AttrCorderConverter.toHide(ctcd.Corder);
                    else
                        viewOrder = AttrCorderConverter.KeepHide.Value;
                }
                ObjectPair<int, int> mapPair = new ObjectPair<int, int>(ctcd.Corder, viewOrder);
                tcm.Add(ctcd.Attr, mapPair);
            }
        }
        /// <summary>
        /// 获取已缓存表单映射的表名
        /// </summary>
        /// <returns></returns>
        public string[] getCachedTables()
        {
            return TableAttrMapping.Keys.ToArray();
        }
        /// <summary>
        /// 获取已经被缓存的数据存储字段~数据库字段位序的映射关系。
        /// 说明：
        /// 1.本方法返回实际的[数据存储字段名称，数据存储字段位序]的映射关系。
        /// 2.数据库字段映射位序的值自0计数。
        /// 3.如果外部存在批量的字段映射匹配需要，需外部缓冲，重复请求该方法会重复创建对象资源。
        /// 4.返回的字典主键顺序以用户界面中字段列加载顺序为参照。
        /// </summary>
        /// <param name="sconn"></param>
        /// <returns></returns>
        public static Dictionary<string, int> getAttrDBMapping(string tableName)
        {
            Dictionary<string, int> maps = new Dictionary<string, int>();
            TColumnMapping attrMapping = null;
            if (!TableAttrMapping.TryGetValue(tableName, out attrMapping))
                throw new IDCMDataException("No cached TableAttrMapping for getAttDBMapping(...)");
            foreach (KeyValuePair<String, ObjectPair<int, int>> kvpair in attrMapping)
            {
                maps[kvpair.Key] = kvpair.Value.Key;
            }
            return maps;
        }
        /// <summary>
        /// 获取已经被缓存的用户浏览字段~数据库字段位序的映射关系。
        /// 说明：
        /// 1.本方法返回可见的[用户浏览字段名，数据存储字段位序]的映射关系。
        /// 2.数据库字段映射位序的值自0计数。
        /// 3.如果外部存在批量的字段映射匹配需要，需外部缓冲，重复请求该方法会重复创建对象资源。
        /// 4.返回的字典主键顺序以用户界面中字段列加载顺序为参照。
        /// </summary>
        /// <returns></returns>
        public static Dictionary<string, int> getCustomDefAttrDBMapping(string tableName)
        {
            Dictionary<string, int> maps = new Dictionary<string, int>();
            TColumnMapping attrMapping = null;
            if (!TableAttrMapping.TryGetValue(tableName, out attrMapping))
                throw new IDCMDataException("No cached TableAttrMapping for getCustomDefAttrDBMapping(...)");
            foreach (KeyValuePair<String, ObjectPair<int, int>> kvpair in attrMapping)
            {
                if (CVNameWrapper.isViewWrapName(kvpair.Key))
                {
                    string key = CVNameWrapper.toViewName(kvpair.Key);
                    maps[key] = kvpair.Value.Key;
                }
            }
            return maps;
        }
        /// <summary>
        /// 获取已经被缓存的数据存储字段~预览界面位序的映射关系。
        /// 说明：
        /// 1.本方法返回实际的[数据存储字段名称，预览界面位序]的映射关系。
        /// 2.数据库字段映射位序的值自0计数。
        /// 3.如果外部存在批量的字段映射匹配需要，需外部缓冲，重复请求该方法会重复创建对象资源。
        /// 4.返回的字典主键顺序以用户界面中字段列加载顺序为参照。
        /// </summary>
        /// <param name="sconn"></param>
        /// <returns></returns>
        public static Dictionary<string, int> getAttrViewMapping(string tableName)
        {
            Dictionary<string, int> maps = new Dictionary<string, int>();
            TColumnMapping attrMapping = null;
            if (!TableAttrMapping.TryGetValue(tableName, out attrMapping))
                throw new IDCMDataException("No cached TableAttrMapping for getAttrViewMapping(...)");
            foreach (KeyValuePair<String, ObjectPair<int, int>> kvpair in attrMapping)
            {
                maps[kvpair.Key] = kvpair.Value.Val;
            }
            return maps;
        }
        /// <summary>
        /// 获取已经被缓存的用户浏览字段~预览界面位序的映射关系。
        /// 说明：
        /// 1.本方法返回可见的[用户浏览字段名，预览界面位序]的映射关系。
        /// 2.数据库字段映射位序的值自0计数。
        /// 3.如果外部存在批量的字段映射匹配需要，需外部缓冲，重复请求该方法会重复创建对象资源。
        /// 4.返回的字典主键顺序以用户界面中字段列加载顺序为参照。
        /// </summary>
        /// <returns></returns>
        public static Dictionary<string, int> getCustomDefAttrViewMapping(string tableName)
        {
            Dictionary<string, int> maps = new Dictionary<string, int>();
            TColumnMapping attrMapping = null;
            if (!TableAttrMapping.TryGetValue(tableName, out attrMapping))
                throw new IDCMDataException("No cached TableAttrMapping for getCustomDefAttrViewMapping(...)");
            foreach (KeyValuePair<String, ObjectPair<int, int>> kvpair in attrMapping)
            {
                if (CVNameWrapper.isViewWrapName(kvpair.Key))
                {
                    string key = CVNameWrapper.toViewName(kvpair.Key);
                    maps[key] = kvpair.Value.Val;
                }
            }
            return maps;
        }

        /// <summary>
        /// 获取预览字段集序列
        /// 说明:
        /// 1.如果外部存在批量的字段映射匹配需要，需外部缓冲，重复请求该方法会重复创建对象资源。
        /// </summary>
        /// <returns></returns>
        public static List<string> getViewAttrs(string tableName,bool withInnerField = true)
        {
            TColumnMapping attrMapping = null;
            if (!TableAttrMapping.TryGetValue(tableName, out attrMapping))
                throw new IDCMDataException("No cached TableAttrMapping for getViewAttrs(...)");
            if (withInnerField)
                return attrMapping.Keys.ToList<string>();//第一次进来没参数，所以返回key的集合(属性名称)
            else
            {
                List<string> res = new List<string>();
                foreach (string key in attrMapping.Keys)
                {
                    if (CVNameWrapper.isViewWrapName(key))//查看是否以[ 开头并以] 结尾
                        res.Add(key);
                }
                return res;
            }
        }
        /// <summary>
        /// 获取预览字段位序值(如查找失败返回-1)
        /// 说明：
        /// 1.如果外部存在批量的字段映射匹配需要，首选getAttrViewMapping或getCustomAttrViewMapping方法进行外部缓冲。
        /// </summary>
        /// <param name="attr">数据存储字段名称</param>
        /// <returns></returns>
        public static int getViewOrder(string tableName, string attr)
        {
            TColumnMapping attrMapping = null;
            if (!TableAttrMapping.TryGetValue(tableName, out attrMapping))
                throw new IDCMDataException("No cached TableAttrMapping for getViewOrder(...)");
            ObjectPair<int, int> kvpair = null;
            attrMapping.TryGetValue(CVNameWrapper.toDBName(attr), out kvpair);
            return kvpair == null ? AttrCorderConverter.Invalid.Value : kvpair.Val;
        }
        /// <summary>
        /// 获取存储字段序列值(如查找失败返回-1)
        /// 说明：
        /// 1.如果外部存在批量的字段映射匹配需要，首选getAttrDBMapping或getCustomAttrDBMapping方法进行外部缓冲。
        /// </summary>
        /// <param name="attr">数据存储字段名称</param>
        /// <returns></returns>
        public static int getDBOrder(string tableName, string attr, bool autoWrap = true)
        {
            TColumnMapping attrMapping = null;
            if (!TableAttrMapping.TryGetValue(tableName, out attrMapping))
                throw new IDCMDataException("No cached TableAttrMapping for getViewOrder(...)");
            ObjectPair<int, int> kvpair = null;
            if (autoWrap == true)
                attrMapping.TryGetValue(CVNameWrapper.toDBName(attr), out kvpair);
            else
                attrMapping.TryGetValue(attr, out kvpair);
            return kvpair == null ? AttrCorderConverter.Invalid.Value : kvpair.Key;
        }

        /// <summary>
        /// 更新预览字段位序值
        /// </summary>
        /// <param name="attr"></param>
        /// <param name="viewOrder"></param>
        public static void noteViewOrder(IDBManager dbm, string tableName, string attr, int viewOrder)
        {
            bool res = CustomVColMapDAM.updateViewOrder(dbm,tableName, attr, viewOrder);
            if (res)
            {
                TColumnMapping attrMapping = null;
                if (TableAttrMapping.TryGetValue(tableName, out attrMapping))
                {
                    attrMapping[attr] = new ObjectPair<int, int>(attrMapping[attr].Key, viewOrder);
                }
            }
        }

        public static bool clearCacheColMap(string tableName)
        {
            TColumnMapping attrMapping = null;
            return TableAttrMapping.TryRemove(tableName, out attrMapping);
        }
        /// <summary>
        /// 数据存储的字段名与[数据存储位序，预览界面位序]的双层映射关系存储集合对象
        /// </summary>
        protected static ConcurrentDictionary<string, TColumnMapping> TableAttrMapping = new ConcurrentDictionary<string, TColumnMapping>();
    }
}
