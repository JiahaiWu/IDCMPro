using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Concurrent;
using System.Threading;
using IDCM.Base;

namespace IDCM.JobDriver.Core
{
    internal class DCMJobScheduler
    {
        public DCMJobScheduler(int maxParallelSize=2,double interval=5)
        {
            this.MaxParallelSize = maxParallelSize;
            poolSemaphore = new Semaphore(MaxParallelSize,MaxParallelSize);
            jobPoller = new System.Timers.Timer(interval);
            jobPoller.Elapsed += OnPollerElaspsed;
            priorityQueue = new ConcurrentQueue<DCMJob>();
            readyQueue = new ConcurrentQueue<DCMJob>();
            waitIdleQueue = new ConcurrentQueue<DCMJob>();
        }


        public bool note(DCMJob job)
        {
            jobPoller.Enabled = true;
            return true;
        }

        protected void OnPollerElaspsed(object sender,EventArgs args)
        {
            DCMJob tjob = null;
            while (priorityQueue.TryDequeue(out tjob))
            {
                pushHandler(tjob);
            }
            while (readyQueue.TryDequeue(out tjob))
            {
                pushHandler(tjob);
            }
            while(waitIdleQueue.TryDequeue(out tjob))
            {
                pushHandler(tjob);
            }
            jobPoller.Enabled = false;
        }

        protected void pushHandler(DCMJob job)
        {
            if (poolSemaphore.WaitOne(MAX_Job_REQUEST_TIME_OUT))
            {
                BGWorkerInvoker.pushHandler(job.BGHandler, OnJobRelease);
            }
            else
            {
                //信号量等待超时！！
                throw new IDCMServException("Waiting Time out for DCMJobScheduler push DCMJob Handler, please check relative program coding!");
            }
        }
        protected void OnJobRelease(bool Canceled,Exception ex)
        {
            poolSemaphore.Release();
            return;
        }
        /// <summary>
        /// 轮询器
        /// </summary>
        private System.Timers.Timer jobPoller = null;
        /// <summary>
        /// 线程调度最大并行数
        /// </summary>
        private int MaxParallelSize = 2;
        /// <summary>
        /// 快速优先队列
        /// </summary>
        private ConcurrentQueue<DCMJob> priorityQueue = null;
        /// <summary>
        /// 候选队列
        /// </summary>
        private ConcurrentQueue<DCMJob> readyQueue = null;
        /// <summary>
        /// 忙时等待队列
        /// </summary>
        private ConcurrentQueue<DCMJob> waitIdleQueue =null;
        /// <summary>
        /// 同步信号量
        /// </summary>
        protected volatile Semaphore poolSemaphore;
        /// <summary>
        /// 线程池数量
        /// </summary>
        private readonly int poolSize;
        /// <summary>
        /// 最长等待毫秒数
        /// </summary>
        public static int MAX_Job_REQUEST_TIME_OUT = 3600000;  //one hour for waiting
    }
}
