using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Concurrent;
using System.Threading;
using IDCM.Base;
using IDCM.Base.AbsInterfaces;
using IDCM.IDB;
using IDCM.JobDriver.DAM;

namespace IDCM.JobDriver.Core
{
    internal class DCMJobScheduler
    {
        public DCMJobScheduler(IDBManager dbm,int maxParallelSize = 2, double interval = 5)
        {
            this.MaxParallelSize = maxParallelSize;
            poolSemaphore = new Semaphore(MaxParallelSize,MaxParallelSize);
            jobPoller = new System.Timers.Timer(interval);
            jobPoller.Elapsed += OnPollerElaspsed;
            priorityQueue = new ConcurrentQueue<DCMJob>();
            readyQueue = new ConcurrentQueue<DCMJob>();
            waitIdleQueue = new ConcurrentQueue<DCMJob>();
            replaceFilter = new ConcurrentDictionary<int, DCMJob>();
            this.dbm = dbm;
            if (dbm != null)
                LazyWorkNoteDAM.prepareTables(dbm);
        }

        public bool note(AbsBGHandler handler, JobHandOption option = null, object[] args = null)
        {
            DCMJob job = option==null?new DCMJob(handler):new DCMJob(handler,option.Clone() as JobHandOption);
            job.appendArgs(args);
            if (formJobQueue(job))
            {
                jobPoller.Enabled = true;
                return true;
            }
            return false;
        }
        /// <summary>
        /// 查询句柄记录集，验证当前空闲状态
        /// </summary>
        /// <returns></returns>
        public bool checkForIdle()
        {
            return priorityQueue.IsEmpty && readyQueue.IsEmpty;
        }
        protected bool formJobQueue(DCMJob job)
        {
            JobHandOption option = job.JobOption;
            if (!lastOneIsInterdictMode())
            {
                if(option.IsReplaceMode)
                    addJobReplaceFilter(job);
                if (option.IsLazyTransaction)
                {
                    if (dbm != null && dbm.Status.Equals(IDBStatus.InWorking))
                    {
                        LazyWorkNote lwn = ConvertToLazyWorkNote(job);
                        if(lwn!=null)
                            LazyWorkNoteDAM.saveWork(dbm, lwn);
                    }
                }
                else
                {
                    if (option.IsPriorityMode)
                    {
                        priorityQueue.Enqueue(job);
                    }
                    else
                    {
                        readyQueue.Enqueue(job);
                    }
                }
            }
            return false;
        }
        private void addJobReplaceFilter(DCMJob job)
        {
            replaceFilter.AddOrUpdate(job.JID, job, (key, oldVlaue) => oldVlaue =job);
        }
        private LazyWorkNote ConvertToLazyWorkNote(DCMJob job)
        {
            //unimplemented!
            return null;
        }
        private bool lastOneIsInterdictMode()
        {
            if (!waitIdleQueue.IsEmpty && waitIdleQueue.Last().JobOption.IsInterdictMode)
                return true;
            return false;
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
            if (job == null || job.TimeOut)
                return;
            DCMJob rejob = null;
            replaceFilter.TryGetValue(job.JID, out rejob);
            if (rejob != null)
            {
                if (rejob != job)
                    return;
                else replaceFilter.TryRemove(job.JID,out rejob);
            }
            if (poolSemaphore.WaitOne(MAX_Job_REQUEST_TIME_OUT))
            {
                BGWorkerInvoker.pushHandler(job, OnJobRelease);
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
        /// 用于后者替换式的任务标记集合
        /// </summary>
        private ConcurrentDictionary<int, DCMJob> replaceFilter = null;
        private IDBManager dbm;
        /// <summary>
        /// 同步信号量
        /// </summary>
        protected volatile Semaphore poolSemaphore;
        /// <summary>
        /// 最长等待毫秒数
        /// </summary>
        public static int MAX_Job_REQUEST_TIME_OUT = 3600000;  //one hour for waiting
    }
}
