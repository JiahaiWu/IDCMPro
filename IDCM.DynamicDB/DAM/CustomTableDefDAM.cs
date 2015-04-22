﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IDCM.IDB;
using IDCM.Base.ComPO;

namespace IDCM.DynamicDB.DAM
{
    public class CustomTableDefDAM
    {
        /// <summary>
        /// 查询所有数据表定义对象
        /// </summary>
        /// <returns></returns>
        public static List<CustomTableDef> loadTables(IDBManager dbm)
        {
            string cmd = "SELECT * FROM CustomTableDef";
            return DataSupporter.ListSQLQuery<CustomTableDef>(dbm, cmd);
        }
        /// <summary>
        /// 查询数据表定义对象
        /// </summary>
        /// <returns></returns>
        public static CustomTableDef queryTable(IDBManager dbm,string tableName)
        {
            string cmd = "SELECT * FROM CustomTableDef where TName='"+CVNameWrapper.toDBName(tableName)+"'";
            List<CustomTableDef> ctcds= DataSupporter.ListSQLQuery<CustomTableDef>(dbm, cmd);
            return ctcds == null || ctcds.Count < 1 ? null : ctcds[0];
        }
    }
}
