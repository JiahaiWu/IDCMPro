using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using System.Data;
using System.Threading;
using IDCM.IDB.DHCP;
using System.Data.SQLite;
using System.Data.SQLite.Generic;
using IDCM.Base;
using System.IO;

namespace IDCM.IDB.DAM
{
    internal class DAMBase
    {
        /// <summary>
        /// 启动数据库实例,返回数据库连接串，如失效则返回null.
        /// 异常:
        /// System.Data.Exception  文件创建或访问异常
        /// System.Data.SQLite.SQLiteException  文件创建或访问异常
        /// </summary>
        /// <param name="dbFilePath"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public static ConnLabel startDBInstance(string dbFilePath, string password=null)
        {
#if DEBUG
            System.Diagnostics.Debug.Assert(dbFilePath != null);
#endif
            try
            {
                SQLiteConnectionStringBuilder sqlCSB = new SQLiteConnectionStringBuilder();
                sqlCSB.DataSource = dbFilePath;
                if(password!=null && password.Length>0)
                    sqlCSB.Password = password;//设置密码
                sqlCSB.SyncMode = SynchronizationModes.Off;//启用异步存储模式
                sqlCSB.Pooling = true;
                sqlCSB.AsParallel();
                sqlCSB.DefaultTimeout = SysConstants.MAX_DB_REQUEST_TIME_OUT;
                ////////////////////////////////////////////////////////////////
                //sqlCSB.ToFullPath = true;
                //@note 备选设置
                ////////////////////////////////////////////////////////////////
                ConnLabel sconn = new ConnLabel(sqlCSB);
                if (!File.Exists(dbFilePath))
                {
                    SQLiteConnection.CreateFile(dbFilePath);
                    sqlCSB.Add("PRAGMA application_id", IDCM_DATA_BIND_Code);//设置Like查询大小写敏感与否设置
                    sconn = new ConnLabel(sqlCSB);
                }
                using (SQLiteConnPicker picker = new SQLiteConnPicker(sconn))
                {
                    picker.getConnection().Execute("PRAGMA application_id(" + IDCM_DATA_BIND_Code + ");");
                    int bindcode = picker.getConnection().ExecuteScalar<int>("PRAGMA application_id");
                    if (bindcode == IDCM_DATA_BIND_Code)
                    {
                        return sconn;
                    }
                    else
                    {
                        throw new System.Data.DataException("Invalid database file for IDCM.Data Module!");
                    }
                }
            }
            catch (SQLiteException ex)
            {
                log.Error("Error in IDCM.Data.startDBInstance(). ", ex);
                throw ex;
            }
        }
        /// <summary>
        /// 关闭目标数据库，停止所有未完成的连接过程
        /// 说明：
        /// 1.Passes a shutdown request to the SQLite core library. Does not throw an exception if the shutdown request fails.
        /// </summary>
        /// <returns></returns>
        public static void stopDBInstance(ConnLabel sconn)
        {
            SQLiteConnPicker.shutdown(sconn);
        }

        /// <summary>
        /// 结构化的静态数据表单初始化定义
        /// 说明：
        /// 1.可重入
        /// </summary>
        /// <returns>初始化操作完成与否</returns>
        public static bool prepareTables(ConnLabel sconn)
        {
#if DEBUG
            System.Diagnostics.Debug.Assert(sconn != null);
#endif
            int rescode = -1;
            using (SQLiteConnPicker picker = new SQLiteConnPicker(sconn))
            {
                using (SQLiteTransaction transaction = picker.getConnection().BeginTransaction())
                {
                    string baseTableDefs = getBasicTableCmds();
                    log.Debug("PrepareTables @SQLCommand=" + baseTableDefs);
                    rescode = picker.getConnection().Execute(baseTableDefs);
                    transaction.Commit();
                }
            }
            return rescode > -1;
        }
        /// <summary>
        /// 执行SQL非查询命令，返回查询结果集
        /// </summary>
        /// <param name="picker"></param>
        /// <param name="sqlExpressions"></param>
        /// <returns></returns>
        public static IEnumerable<T>[] SQLQuery<T>(ConnLabel sconn, params string[] sqlExpressions)
        {
#if DEBUG
            System.Diagnostics.Debug.Assert(sconn != null);
            System.Diagnostics.Debug.Assert(sqlExpressions != null);
#endif
            List<IEnumerable<T>> res = new List<IEnumerable<T>>(sqlExpressions.Count());
            foreach (string sql in sqlExpressions)
            {
                using (SQLiteConnPicker picker = new SQLiteConnPicker(sconn))
                {
#if DEBUG
                    log.Debug("SQLQuery Info: @CommandText=" + sql);
#endif
                    IEnumerable<T> result = picker.getConnection().Query<T>(sql);
                    res.Add(result);
                }
            }
            return res.ToArray();
        }
        /// <summary>
        /// 执行SQL查询命令，返回查询结果集
        /// </summary>
        /// <param name="picker">连接句柄</param>
        /// <param name="commands"></param>
        /// <returns></returns>
        public static int[] executeSQL(ConnLabel sconn, params string[] commands)
        {
#if DEBUG
            System.Diagnostics.Debug.Assert(sconn != null);
            System.Diagnostics.Debug.Assert(commands != null);
#endif
            List<int> res = new List<int>(commands.Count());
            SQLiteCommand cmd = new SQLiteCommand();
            using (SQLiteConnPicker picker = new SQLiteConnPicker(sconn))
            {
                using (SQLiteTransaction transaction = picker.getConnection().BeginTransaction())
                {
                    foreach (string execmd in commands)
                    {
#if DEBUG
                        log.Debug("executeSQL Info: @CommandText=" + execmd);
#endif
                        int result = picker.getConnection().Execute(execmd);
                        res.Add(result);
                    }
                    transaction.Commit();
                }
            }
            return res.ToArray();
        }

        #region 基础数据库表定义
        protected static string getBasicTableCmds()
        {
            StringBuilder strbuilder = new StringBuilder();
            string cmd = null;
            BaseInfoNote dbvn = new BaseInfoNote();
            //创建基础自增长序列及版本记录数据表
            cmd = "Create Table if Not Exists " + typeof(BaseInfoNote).Name + "("
            + "SeqId INTEGER primary key DEFAULT " + dbvn.SeqId + ","
            + "DbType Text default '" + dbvn.DbType + "', "
            + "AppType Text default '" + dbvn.AppType + "',"
            + "AppVercode Real default " + dbvn.AppVercode +","
            + "ConfigSyncTag INTEGER DEFAULT '" + dbvn.ConfigSyncTag + "');";
            strbuilder.Append(cmd).Append("\n");
            return strbuilder.ToString();
        }
        #endregion



        private static NLog.Logger log = NLog.LogManager.GetCurrentClassLogger();
        /// <summary>
        /// IDCM.Data数据库文档特征标识码设定(设定为32bit长度)
        /// </summary>
        private const UInt32 IDCM_DATA_BIND_Code = 1415926535;
    }
}
