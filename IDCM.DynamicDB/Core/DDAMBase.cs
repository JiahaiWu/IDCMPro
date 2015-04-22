using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IDCM.IDB;
using IDCM.Base.ComPO;
using IDCM.DynamicDB.ComPO;

namespace IDCM.DynamicDB.Core
{
    public class DDAMBase
    {
        /// <summary>
        /// 结构化的静态数据表单初始化定义
        /// 说明：
        /// 1.可重入
        /// </summary>
        /// <returns>初始化操作完成与否</returns>
        public static bool prepareTables(IDBManager dbm)
        {
            if (dbm == null || !IDBStatus.InWorking.Equals(dbm.Status))
                return false;
            string[] cmds = getBasicTableCmds();
            int[] res = DataSupporter.executeSQL(dbm, cmds);
            return DataSupporter.checkExecuteOk(res);
        }
        /// <summary>
        /// 清空原有表内数据记录
        /// </summary>
        /// <param name="picker"></param>
        /// <returns></returns>
        public static bool dropCustomTableDef(IDBManager dbm, string TName)
        {
            if (dbm == null || !IDBStatus.InWorking.Equals(dbm.Status))
                return false;
            string tablename = CVNameWrapper.toDBName(TName);
            string cmd = "drop table if exists " + tablename + ";";
            string cmd1 = "delete from " + typeof(CustomTableDef).Name + " where TName ='" + tablename + "';";
            string cmd2 = "delete from " + typeof(CustomTColDef).Name + " where TName ='" + tablename + "';";
            int[] res = DataSupporter.executeSQL(dbm, cmd,cmd1, cmd2, cmd3);
            return DataSupporter.checkExecuteOk(res);
        }

        /// <summary>
        /// 创建用户录入数据表的动态形式化定义
        /// </summary>
        /// <param name="dbm"></param>
        /// <param name="dts"></param>
        /// <returns></returns>
        public static bool buildCustomTable(IDBManager dbm, CustomTDS dts)
        {
            string cmd = "insert into " +typeof(CustomTableDef).Name+" (TName,Comments,CreateDate) values("
                + "'" + CVNameWrapper.toDBName(dts.TName) + "','" + dts.Comments + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm") + "'";
            if(DataSupporter.checkExecuteOk(DataSupporter.executeSQL(dbm,cmd)))
            {
                CustomTColDef[] ctcds = getPackagedTableDef(dts.TName, dts.ccds.ToArray()).ToArray();
                if (saveCustomTableA(dbm, ctcds) && saveCustomTColDef(dbm, ctcds))
                {
                    return initCustomViewColMap(dbm, ctcds);
                }
                else //rollback
                {
                    dropCustomTableDef(dbm, dts.TName);
                }
            }
            return false;
        }

        protected static List<CustomTColDef> getPackagedTableDef(string TName,CustomColDef[] ccds)
        {
            string tablename = CVNameWrapper.toDBName(TName);
            List<CustomTColDef> ctcds = new List<CustomTColDef>();
            CustomTColDef rid = new CustomTColDef();
            rid.Attr = CTDRecordA.CTD_RID;
            rid.AttrType = AttrTypeConverter.IDCM_Integer;
            rid.IsRequire = true;
            rid.IsUnique = true;
            rid.DefaultVal = null;
            rid.IsInter = true;
            rid.Comments = "CTDRecordDA.CTD_RID";
            rid.TName = tablename;
            rid.COrder = ctcds.Count;
            rid.ViewOrder = ctcds.Count;
            ctcds.Add(rid);
            foreach (CustomColDef ccd in ccds.OrderBy(rs=>rs.Corder))
            {
                CustomTColDef ctcd = new CustomTColDef();
                ctcd.Attr = CVNameWrapper.toDBName(ccd.Attr);
                ctcd.AttrType = ccd.AttrType;
                ctcd.Comments = ccd.Comments;
                ctcd.COrder = ctcds.Count;
                ctcd.ViewOrder = ctcds.Count * (ccd.IsRequire ? 1 : -1);
                ctcd.DefaultVal = ccd.DefaultVal;
                ctcd.IsRequire = ccd.IsRequire;
                ctcd.IsUnique = ccd.IsUnique;
                ctcd.Restrict = ccd.Restrict;
                ctcd.IsInter = false;
                ctcd.TName = tablename;
                ctcds.Add(ctcd);
            }
            return ctcds;
        }
        /// <summary>
        /// 保存新列属性定义记录集
        /// </summary>
        /// <param name="ctcd"></param>
        /// <returns></returns>
        protected static bool saveCustomTColDef(IDBManager dbm, params CustomTColDef[] ctcds)
        {
            List<string> cmds = new List<string>();
            foreach (CustomTColDef ctcd in ctcds)
            {
                StringBuilder cmdBuilder = new StringBuilder();
                cmdBuilder.Append("insert Or Replace into " + typeof(CustomTColDef).Name
                    + "(attr,attrType,comments,defaultVal,COrder,isInter,isRequire,isUnique,restrict,ViewOrder) values(");
                cmdBuilder.Append("'").Append(ctcd.Attr).Append("',");
                if (ctcd.AttrType == null)
                    cmdBuilder.Append("null,");
                else
                    cmdBuilder.Append("'").Append(ctcd.AttrType).Append("',");
                if (ctcd.Comments == null)
                    cmdBuilder.Append("null,");
                else
                    cmdBuilder.Append("'").Append(ctcd.Comments).Append("',");
                if (ctcd.DefaultVal == null)
                    cmdBuilder.Append("null,");
                else
                    cmdBuilder.Append("'").Append(ctcd.DefaultVal).Append("',");
                cmdBuilder.Append(ctcd.COrder).Append(",");
                cmdBuilder.Append("'").Append(ctcd.IsInter).Append("',");
                cmdBuilder.Append("'").Append(ctcd.IsRequire).Append("',");
                cmdBuilder.Append("'").Append(ctcd.IsUnique).Append("',");
                if (ctcd.Restrict == null)
                    cmdBuilder.Append("null");
                else
                    cmdBuilder.Append("'").Append(ctcd.Restrict).Append("',");
                cmdBuilder.Append(");");
                cmds.Add(cmdBuilder.ToString());
            }
            if (cmds.Count > 0)
            {
                int[] res = DataSupporter.executeSQL(dbm, cmds.ToArray());
                return DataSupporter.checkExecuteOk(res);
            }
            return false;
        }

        /// <summary>
        /// 创建自定义表实例
        /// </summary>
        protected static bool saveCustomTableA(IDBManager dbm, params CustomTColDef[] ctcds)
        {
            if (ctcds == null || ctcds.Length < 1)
                return false;
            string tablename = ctcds[0].TName;
            StringBuilder cmdBuilder = new StringBuilder();
            cmdBuilder.Append("Create Table if Not Exists " + tablename + " ("+ CTDRecordA.CTD_RID + " Integer unique primary key ");
            HashSet<string> noteAttrs = new HashSet<string>();
            foreach (CustomTColDef ctcd in ctcds)
            {
                if (!ctcd.TName.Equals(tablename))
                {
                    log.Error("TName Conflicting: " + ctcd.TName);
                    return false;
                }
                if (noteAttrs.Contains(ctcd.Attr))
                {
                    log.Error("Duplicate column name: " + ctcd.Attr);
                    return false;
                }
                noteAttrs.Add(ctcd.Attr);
                cmdBuilder.Append(",");
                cmdBuilder.Append(ctcd.Attr).Append(" Text ");
                if (ctcd.IsUnique)
                {
                    cmdBuilder.Append(" UNIQUE ");
                }
                if (ctcd.DefaultVal != null && ctcd.DefaultVal.Length > 0)
                {
                    cmdBuilder.Append(" Default '").Append(ctcd.DefaultVal).Append("'");
                }
            }
            cmdBuilder.Append(")");
            int res=DataSupporter.executeSQL(dbm, cmdBuilder.ToString());
            return DataSupporter.checkExecuteOk(res);
        }
        /// <summary>
        /// 创建自定义列显示映射关系
        /// </summary>
        protected static bool initCustomViewColMap(IDBManager dbm, params CustomTColDef[] ctcds)
        {
            if (ctcds == null || ctcds.Length < 1)
                return false;
            string tablename = CVNameWrapper.toDBName(ctcds[0].TName);
            List<string> noteCmds = new List<string>();
            foreach (CustomTColDef ctcd in ctcds)
            {
                if (CVNameWrapper.isViewWrapName(ctcd.Attr) && ctcd.IsRequire)
                {
                    noteCmds.Add("insert into " + typeof(CustomViewColMap).Name + "(attr,TName,viewOrder) values('" 
                        + ctcd.Attr + "'," + tablename + "," + ctcd.Corder + ")");
                }
            }
            int[] res= DataSupporter.executeSQL(dbm, noteCmds.ToArray()); //noteDefaultColMap
            return DataSupporter.checkExecuteOk(res);
        }

        #region 基础数据库表定义
        protected static string[] getBasicTableCmds()
        {
            List<string> cmds = new List<string>();
            string cmd = "Create Table if Not Exists " + typeof(CustomTColDef).Name + "("
                + "Attr TEXT,"
                + "AttrType TEXT default 'string',"
                + "DefaultVal TEXT default NULL,"
                + "Comments TEXT default NULL,"
                + "Restrict TEXT default NULL,"
                + "TName TEXT NOT NULL,"
                + "IsUnique INTEGER default 0,"
                + "IsRequire INTEGER default 0,"
                + "Corder INTEGER default 0,"
                + "IsInter INTEGER default 0,"
                + "constraint pk_t2 primary key (Attr,TName));";
            cmds.Add(cmd);
            //创建CustomTColMap数据表定义
            cmd = "Create Table if Not Exists " + typeof(CustomViewColMap).Name + "("
                + "Attr TEXT,"
                + "TName TEXT NOT NULL,"
                + "ViewOrder INTEGER default -1"
                + "constraint pk_t2 primary key (Attr,TName));";
            cmds.Add(cmd);
            cmd = "Create Table if Not Exists " + typeof(CustomTableDef).Name + "("
                + "TName TEXT primary key,"
                + "Comments TEXT default 'string',"
                + "CreateDate TEXT default NULL);";
            cmds.Add(cmd);
            return cmds.ToArray();
        }
        #endregion


        /// <summary>
        /// 重建用户录入数据表的动态形式化定义
        /// 说明:
        /// 1.此操作将彻底清空原有表内数据记录
        /// </summary>
        /// <param name="picker"></param>
        /// <returns></returns>
        public static bool rebuildCustomTColDef(IDBManager idbm)
        {
            if (idbm == null || !IDBStatus.InWorking.Equals(idbm.Status))
                return false;
            string cmd1 = "drop table if exists " + typeof(CustomTColDef).Name + ";";
            string cmd2 = "Create Table if Not Exists " + typeof(CustomTColDef).Name + "("
                + "Attr TEXT primary key,"
                + "AttrType TEXT default 'string',"
                + "DefaultVal TEXT default NULL,"
                + "Comments TEXT default NULL,"
                + "Restrict TEXT default NULL,"
                + "TName TEXT NOT NULL,"
                + "IsUnique INTEGER default 0,"
                + "IsRequire INTEGER default 0,"
                + "Corder INTEGER default 0,"
                + "IsInter INTEGER default 0);";
            int[] res = DataSupporter.executeSQL(idbm, cmd1, cmd2);
            return (res.Length > 1 && res[1] > -1);
        }
        private static NLog.Logger log = NLog.LogManager.GetCurrentClassLogger();
    }
}
