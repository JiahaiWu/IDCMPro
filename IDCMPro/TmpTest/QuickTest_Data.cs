using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IDCM.Data;
using System.Threading;
using IDCM.Data.DAM;

namespace IDCM.TmpTest
{
    class QuickTest_Data
    {

        public void test()
        {
            //System.IO.File.Delete("F:/Test.mrc");
            //冒烟测试部分
            IDBManager wsm = new IDBManager("F:/Test.mrc");
            if (wsm.connect())
            {
                string connStr = wsm.getTrueConnectStr();
                long sid = DataSupporter.nextSeqID(wsm);
                Console.WriteLine("数据源连接快速检测\n#冒烟测试部分 通过。\n");
                wsm.disconnect();
            }else
                Console.WriteLine("[lastError] ?");

            //重建连接并进行线程池请求测试
            if (wsm.connect())
            {
                ParameterizedThreadStart pts = new ParameterizedThreadStart(DBQueryTest);
                Thread[] threads = new Thread[100];
                int tx = 0;
                while (tx < threads.Length)
                {
                    threads[tx]=new Thread(pts);
                    tx++;
                }
                string ts = DateTime.Now.Ticks.ToString();
                while (tx > 0)
                {
                    tx--;
                    threads[tx].Start(new object[] { wsm, tx.ToString(), ts });
                }
                //while (tx > 0)
                //{
                //    tx--;
                //    DBQueryTest(new object[] { wsm, tx.ToString(), ts });
                //}
                Console.WriteLine("线程池请求测试部分 通过。");
            }
        }

        public void DBQueryTest(object wsmObj)
        {
            object[] pas=(wsmObj as object[]);
            for (int i = 0; i < 10; i++)
            {
                IDBManager wsm = pas[0] as IDBManager;
                DataSupporter.executeSQL(wsm,"replace into BaseInfoNote(SeqId,DbType,AppType,ConfigSyncTag) values(" + i + ",'DbType','AppType','sadfhjhsadkfjsahdfkj');");
                int cc = DataSupporter.ListSQLQuery<BaseInfoNote>(wsm, "select * from BaseInfoNote").Count;
                Console.WriteLine("@cc" + (pas[1] as string) + "=" + cc);
                DataSupporter.executeSQL(wsm, "delete from BaseInfoNote where seqId=" + i + ";");
            }
            TimeSpan ts = new TimeSpan(DateTime.Now.Ticks-long.Parse(pas[2] as string));
            Console.WriteLine("线程" + (pas[1] as string) + "耗时=" + ts.TotalMilliseconds + "ms");
        }
    }
}
