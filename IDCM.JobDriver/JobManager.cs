using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IDCM.Base.AbsInterfaces;
using System.Reflection;

namespace IDCM.JobDriver
{
    public class JobManager
    {
        /// <summary>
        /// 向指定的对象实例和关联方法传递参数，并请求执行
        /// </summary>
        /// <param name="servInstance"></param>
        /// <param name="method"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public static bool note(AbsBGHandler hanldeInstance, params object[] initArgs)
        {
            BGWorkerInvoker.pushHandler(hanldeInstance);
            return true;
        }
        /// <summary>
        /// 查询句柄记录集，验证当前空闲状态
        /// </summary>
        /// <returns></returns>
        public static bool checkForIdle()
        {
            return LongTermHandleNoter.checkForIdle();
        }

        /// <summary>
        /// 向指定的对象实例和关联方法传递参数，并中断执行
        /// </summary>
        /// <param name="servInstance"></param>
        /// <param name="method"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public static bool killAsyncHandle(AbsBGHandler servInstance,object killMask = null)
        {
            BGWorkerInvoker.abortHandlerByType(servInstance.GetType());
            return true;
        }

        public static HandleRunInfo[] getRunInfoList()
        {
            return LongTermHandleNoter.getHandleList();
        }
    }
}
