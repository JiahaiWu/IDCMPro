using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IDCM.IDB.DAM;
using IDCM.IDB.DHCP;
using IDCM.Base;
using System.Data.SQLite;
using Dapper;

namespace IDCM.IDB
{
    public class IDBManager
    {
        /// <summary>
        /// 工作空间管理器构造方法，要求指定标准格式的连接句柄
        /// </summary>
        /// <param name="dbPath">数据存档文件路径</param>
        /// <param name="password">数据存档加密字符串</param>
        public IDBManager(string dbPath, string pwd = null)
        {
            this.DBPath = dbPath;
            this.password = pwd;
        }
        /// <summary>
        /// 请求数据源连接操作
        /// </summary>
        /// <returns>连接成功与否状态</returns>
        public bool connect()
        {
#if DEBUG
            System.Diagnostics.Debug.Assert(_status.Equals(IDBStatus.Idle), "Ilde tatus is illegal to connect Database!");
#endif
            if (DBSpaceKeeper.isWorkSpaceAccessible(DBPath))
            {
                if (!DBSpaceKeeper.isProcessDuplicate())
                {
                    sconn = DAMBase.startDBInstance(DBPath, password);
                    if (sconn != null)
                    {
                        _status = IDBStatus.Connected;
                        if (DAMBase.prepareTables(sconn)) ////定义最基础的静态表
                        {
                            BaseInfoNoteDAM.loadBaseInfo(sconn);
                            _status = IDBStatus.InWorking;
                            return true;
                        }
                        else
                        {
                            throw new IDCMDataException("Prepare Tables for database init failed!!");
                        }
                    }
                    else
                    {
                        throw new IDCMDataException("start DB Instance failed!!");
                    }
                }
                else
                {
                    throw new IDCMDataException("There is already another process instance with the same location and name, is should not be started again.");
                }
            }
            else
            {
                throw new IDCMDataException("Database path is not valid or cannot be exclusively locked. @DBPath=" + DBPath);
            }
        }

        /// <summary>
        /// 断开数据库连接，释放访问连接池资源占用。
        /// 说明：
        /// 1.可重入，可并入。
        /// 2.断开数据库连接后，任何后续的数据访问请求都必须重新建立。
        /// </summary>
        /// <returns>断开连接成功与否</returns>
        public bool disconnect()
        {
            //关闭用户工作空间
            if (!_status.Equals(IDBStatus.Idle))
            {
                DAMBase.stopDBInstance(sconn);
                sconn = null;
                _status = IDBStatus.Idle;
                SQLiteConnection.ClearAllPools();
                SQLiteConnection.Shutdown(true, true);
                return true;
            }
            return false;
        }

        /// <summary>
        /// 执行SQL查询命令，返回查询结果集
        /// 说明：
        /// 1.可重入，可并入。
        /// </summary>
        /// <param name="sqlExpressions"></param>
        /// <returns></returns>
        internal IEnumerable<T>[] SQLQuery<T>(params string[] sqlExpressions)
        {
            if (!_status.Equals(IDBStatus.InWorking) || sqlExpressions == null)
                return null;
            try
            {
                return DAMBase.SQLQuery<T>(sconn, sqlExpressions);
            }
            catch (Exception ex)
            {
                throw new IDCMDataException("SQLQuery Failed.", ex);
            }
        }
        /// <summary>
        /// 执行SQL非查询命令，返回执行结果
        /// 说明：
        /// 1.可重入，可并入。
        /// </summary>
        /// <param name="commands"></param>
        /// <returns></returns>
        internal int[] executeSQL(params string[] commands)
        {
            if (!_status.Equals(IDBStatus.InWorking) || commands == null)
                return null;
            try
            {
                return DAMBase.executeSQL(sconn, commands);
            }
            catch (Exception ex)
            {
                throw new IDCMDataException("ExecuteSQL Failed.", ex);
            }
        }
        /// <summary>
        /// 获取数据库连接器对象实例
        /// </summary>
        /// <param name="renew"></param>
        /// <returns></returns>
        internal ConnLabel getConnection(bool doCheckValid = false)
        {
            if (doCheckValid == false)
                return sconn;
            else if (sconn != null){
                try
                {
                    using (SQLiteConnPicker picker = new SQLiteConnPicker(sconn))
                    {
                        picker.getConnection();
                        return sconn;
                    }
                }
                catch (Exception ex)
                {
                    throw new IDCMDataException("Failed to get DB Connection!!!",ex);
                }
            }
            return null;
        }
        /// <summary>
        /// 获取当前有效连接句柄标识
        /// 说明：
        /// 如果连接串不可用，则返回null
        /// </summary>
        /// <returns></returns>
        public string getTrueConnectStr()
        {
            if (_status.Equals(IDBStatus.InWorking))
            {
                return sconn.connectStr;
            }
            return null;
        }
        /// <summary>
        /// 获取当前数据源状态标识
        /// </summary>
        /// <returns></returns>
        public IDBStatus Status
        {
            get
            {
                return _status;
            }
        }
        #region 内置实例对象保持部分
        /// <summary>
        /// 用户工作空间运营状态标识
        /// </summary>
        protected volatile IDBStatus _status = IDBStatus.Idle;
        /// <summary>
        /// 数据库连接句柄标识
        /// </summary>
        protected volatile ConnLabel sconn = null;
        /// <summary>
        /// 数据库目标存档路径
        /// </summary>
        public readonly string DBPath;
        /// <summary>
        /// 数据库访问密码
        /// </summary>
        public readonly string password;
        private static NLog.Logger log = NLog.LogManager.GetCurrentClassLogger();
        #endregion
    }
}
