using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDCM.Data.DAM
{
    /// <summary>
    /// 用户标记当前数据库记录版本标识类定义
    /// 该类实例应当唯一确定，不接受运行时变更标识信息。
    /// </summary>
    public class BaseInfoNote
    {
        private string dbType = "SQLite";

        public string DbType
        {
            get { return dbType; }
            set { //No Updates
            }
        }
        private long seqId = 1;

        public long SeqId
        {
            get { return seqId; }
            set { //No Updates
            }
        }

        private string appType = "IDCMPro";

        public string AppType
        {
            get { return appType; }
            set {
                //No Updates
            }
        }
        private double appVercode = 0.1;

        public double AppVercode
        {
            get { return appVercode; }
            set { 
                //No Updates
            }
        }
        private string configSyncTag = "";
        public string ConfigSyncTag
        {
            get { return configSyncTag; }
            set { configSyncTag = value; }
        }
    }
}
