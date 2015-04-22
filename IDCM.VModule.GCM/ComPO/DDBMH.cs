using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IDCM.IDB;
using IDCM.DynamicDB;

namespace IDCM.VModule.GCM.ComPO
{
    internal class DDBMH
    {
        public new DDBMH(DynamicDBManager ddbManager, IDBManager dbmanger, string tableName)
        {
            this.ddbManager = ddbManager;
            this.dbmanger = dbmanger;
            this.tableName = tableName;
        }
        public DynamicDBManager DDBManager{get{return this.ddbManager}}
        public IDBManager DBmanger { get { return dbmanger; } }
        public string TableName { get { return tableName; } }

        private DynamicDBManager ddbManager;
        private IDBManager dbmanger;
        private string tableName;
    }
}
