using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IDCM.Base.AbsInterfaces;
using System.Reflection;
using IDCM.JobDriver.Core;
using IDCM.IDB;
using IDCM.Base.ComPO;

namespace IDCM.JobDriver
{
    public class JobManager
    {
        public void init(IDBManager dbm)
        {
            jobScheduler = new DCMJobScheduler(dbm);
        }
        /// <summary>
        /// 向指定的对象实例和关联方法传递参数，并请求执行
        /// </summary>
        /// <param name="servInstance"></param>
        /// <param name="method"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public static bool note(AbsBGHandler hanldeInstance, JobHandOption option=null,object[] args=null)
        {
            if(jobScheduler!=null)
               return jobScheduler.note(hanldeInstance, option,args);
            return false;
        }
        /// <summary>
        /// 查询句柄记录集，验证当前空闲状态
        /// </summary>
        /// <returns></returns>
        public static bool checkForIdle()
        {
            if (jobScheduler == null)
                return RunningHandlerNoter.checkForIdle();
            return RunningHandlerNoter.checkForIdle() && jobScheduler.checkForIdle();
        }

        /// <summary>
        /// 向指定的对象实例和关联方法传递参数，并中断执行
        /// </summary>
        /// <param name="servInstance"></param>
        /// <param name="method"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public static bool killAsyncHandle(AbsBGHandler servInstance)
        {
            BGWorkerInvoker.abortHandlerByType(servInstance.GetType());
            return true;
        }

        public static HandleRunInfo[] getRunInfoList()
        {
            return RunningHandlerNoter.getHandleList();
        }
        private static DCMJobScheduler jobScheduler = null;
    }
}
