using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Concurrent;

namespace IDCM.JobDriver.Core
{
    internal class DCMJobScheduler
    {
        public static bool note(DCMJob job)
        {
            jobPoller.Enabled = true;
            return true;
        }

        public static void OnPollerElaspsed(object sender,EventArgs args)
        {
            DCMJob tjob = null;
            while (priorityQueue.TryDequeue(out tjob))
            {
                BGWorkerInvoker.pushHandler(tjob.BGHandler);
            }
            while (readyQueue.TryDequeue(out tjob))
            {
                BGWorkerInvoker.pushHandler(tjob.BGHandler);
            }
            if(waitIdleQueue.TryDequeue(out tjob))
            {
                BGWorkerInvoker.pushHandler(tjob.BGHandler);
            }
            jobPoller.Enabled = false;
        }
        public static void initScheduler()
        {
            jobPoller = new System.Timers.Timer(5);
            jobPoller.Elapsed+=OnPollerElaspsed;
            priorityQueue = new ConcurrentQueue<DCMJob>();
            readyQueue = new ConcurrentQueue<DCMJob>();
            waitIdleQueue = new ConcurrentQueue<DCMJob>();
        }
        /// <summary>
        /// 轮询器
        /// </summary>
        private static System.Timers.Timer jobPoller = null;
        /// <summary>
        /// 线程调度最大并行数
        /// </summary>
        private static int MaxParallelSize = 2;
        /// <summary>
        /// 快速优先队列
        /// </summary>
        private static ConcurrentQueue<DCMJob> priorityQueue = null;
        /// <summary>
        /// 候选队列
        /// </summary>
        private static ConcurrentQueue<DCMJob> readyQueue = null;
        /// <summary>
        /// 忙时等待队列
        /// </summary>
        private static ConcurrentQueue<DCMJob> waitIdleQueue =null;
    }
}
