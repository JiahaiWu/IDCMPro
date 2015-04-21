using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IDCM.IDB;

namespace IDCM.JobDriver.DAM
{
    class LazyWorkNoteDAM
    {
        public static long saveWork(IDBManager dbm, LazyWorkNote ltwNote)
        {
            if (ltwNote.JobSerialInfo != null && ltwNote.JobSerialInfo.Length > 0)
            {
                if (ltwNote.Nid < 1)
                    ltwNote.Nid = DataSupporter.nextSeqID(dbm);
                StringBuilder cmdBuilder = new StringBuilder();
                cmdBuilder.Append("insert or Replace into " + typeof(LazyWorkNote).Name);
                cmdBuilder.Append("(nid,jobType,jobSerialInfo,jobLevel,createTime,startCount,lastResult) values(");
                cmdBuilder.Append(ltwNote.Nid).Append(",'").Append(ltwNote.JobType).Append("','");
                cmdBuilder.Append(ltwNote.JobSerialInfo).Append("',").Append(ltwNote.JobLevel).Append(",").Append(ltwNote.CreateTime).Append(",");
                cmdBuilder.Append(ltwNote.StartCount).Append(",'").Append(ltwNote.LastResult).Append("')");
                DataSupporter.executeSQL(dbm, cmdBuilder.ToString());
                return ltwNote.Nid;
            }
            return -1;
        }

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
            string cmd = "Create Table if Not Exists " + typeof(LazyWorkNote).Name + "("
                + "Nid INTEGER primary key,"
                + "JobType TEXT not null,"
                + "JobSerialInfo TEXT,"
                + "JobLevel INTEGER default 1,"
                + "CreateTime INTEGER,"
                + "StartCount INTEGER default 0,"
                + "LastResult TEXT);";
            int res = DataSupporter.executeSQL(dbm, cmd);
            return DataSupporter.checkExecuteOk(res);
        }
    }
}
