using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IDCM.IDB.DAM;
using System.Data;
using System.Data.SQLite;
using IDCM.Base;

namespace IDCM.IDB
{
    public class DataSupporter
    {

        /// <summary>
        /// 获取唯一序列生成ID值
        /// 1.可重入，可并入。
        /// 注意：
        /// 1.该序号的生成在同一数据库内部，由独占进程请求该方法时，保证生成序号值全局唯一。
        /// 2.该序号生成值会有规律地进行数据库同步写入操作，在进程重启后需调用loadBaseInfo更新目标生成序列起始值。
        /// </summary>
        /// <param name="wsm">工作空间管理器对象实例</param>
        /// <returns>新序列值</returns>
        public static long nextSeqID(IDBManager wsm)
        {
            if (wsm == null || !IDBStatus.InWorking.Equals(wsm.Status))
                throw new IDCMDataException("Ivalid params and status for get next sequence id!");
            return BaseInfoNoteDAM.nextSeqID(wsm.getConnection());
        }

        /// <summary>
        /// 执行SQL查询命令，返回查询结果
        /// 说明：
        /// 1.可重入，可并入。
        /// </summary>
        /// <param name="wsm">工作空间管理器对象实例</param>
        /// <param name="sqlExpression"></param>
        /// <returns></returns>
        public static DataTable SQLQuery(IDBManager wsm, string sqlExpression)
        {
            if (wsm == null || !IDBStatus.InWorking.Equals(wsm.Status))
                throw new IDCMDataException("Ivalid params and status for get next sequence id!");
            if (sqlExpression == null || sqlExpression.Length == 0)
                throw new IDCMDataException("Ivalid sqlExpression");
            DataSet ds = new DataSet();
            using (DHCP.SQLiteConnPicker picker = new DHCP.SQLiteConnPicker(wsm.getConnection()))
            {
                SQLiteConnection conn = picker.getConnection();
                SQLiteCommand cmd = new SQLiteCommand();
                cmd.Connection = conn;
                cmd.CommandText = sqlExpression;
                cmd.CommandType = CommandType.Text;
                try
                {
#if DEBUG
                    log.Debug("DataTableSQLQuery Info: @CommandText=" + cmd.CommandText);
#endif
                    SQLiteDataAdapter sda = new SQLiteDataAdapter(cmd);
                    sda.Fill(ds);
                }
                catch (Exception ex)
                {
                    throw new IDCMDataException("DataTableSQLQuery Error.", ex);
                }
            }
            return ds.Tables.Count > 0 ? ds.Tables[0] : null;
        }
        /// <summary>
        /// 执行SQL查询命令，返回查询结果集
        /// 说明：
        /// 1.可重入，可并入。
        /// </summary>
        /// <param name="wsm">工作空间管理器对象实例</param>
        /// <param name="sqlExpressions"></param>
        /// <returns></returns>
        public static IEnumerable<T>[] SQLQuery<T>(IDBManager wsm, params string[] sqlExpressions)
        {
            IEnumerable<T>[] res = wsm.SQLQuery<T>(sqlExpressions);
            return res;
        }
        /// <summary>
        /// 执行SQL查询命令，返回查询结果
        /// 说明：
        /// 1.可重入，可并入。
        /// </summary>
        /// <param name="wsm">工作空间管理器对象实例</param>
        /// <param name="sqlExpression"></param>
        /// <returns></returns>
        public static IEnumerable<T> SQLQuery<T>(IDBManager wsm, string sqlExpression)
        {
            IEnumerable<T>[] res = wsm.SQLQuery<T>(sqlExpression);
            if (res != null && res.Length > 0)
                return res[0];
            return null;
        }
        /// <summary>
        /// 执行SQL查询命令，返回查询结果
        /// 说明：
        /// 1.可重入，可并入。
        /// </summary>
        /// <param name="wsm">工作空间管理器对象实例</param>
        /// <param name="sqlExpression"></param>
        /// <returns></returns>
        public static List<T> ListSQLQuery<T>(IDBManager wsm, string sqlExpression)
        {
            IEnumerable<T>[] res = wsm.SQLQuery<T>(sqlExpression);
            if (res != null && res.Length > 0)
                return res[0].ToList();
            return null;
        }
        /// <summary>
        /// 执行SQL查询命令，返回查询结果
        /// 说明：
        /// 1.可重入，可并入。
        /// </summary>
        /// <param name="wsm">工作空间管理器对象实例</param>
        /// <param name="sqlExpression"></param>
        /// <returns></returns>
        public static long CountSQLQuery(IDBManager wsm, string sqlExpression)
        {
            IEnumerable<long>[] res = wsm.SQLQuery<long>(sqlExpression);
            if (res != null && res.Length > 0)
                return res[0].FirstOrDefault();
            return 0;
        }

        /// <summary>
        /// 执行SQL非查询命令，返回执行结果
        /// 说明：
        /// 1.可重入，可并入。
        /// </summary>
        /// <param name="wsm">工作空间管理器对象实例</param>
        /// <param name="commands"></param>
        /// <returns>Array of Number of rows affected</returns>
        public static int[] executeSQL(IDBManager wsm, params string[] commands)
        {
            return wsm.executeSQL(commands);
        }
        /// <summary>
        /// 执行SQL非查询命令，返回执行结果
        /// 说明：
        /// 1.可重入，可并入。
        /// </summary>
        /// <param name="wsm">工作空间管理器对象实例</param>
        /// <param name="command"></param>
        /// <returns>Number of rows affected</returns>
        public static int executeSQL(IDBManager wsm, string command)
        {
            int[] res = wsm.executeSQL(command);
            if (res != null && res.Length > 0)
                return res[0];
            return -2;
        }
        /// <summary>
        /// 对批量执行SQL的返回结果值的全部完成标识判断
        /// </summary>
        /// <param name="exeResults"></param>
        /// <returns></returns>
        public static bool checkExecuteOk(params int[] exeResults)
        {
            if (exeResults == null || exeResults.Length == 0)
                return false;
            foreach(int res in exeResults)
            {
                if (res < 0)
                    return false;
            }
            return true;
        }
        private static NLog.Logger log = NLog.LogManager.GetCurrentClassLogger();
    }
}
