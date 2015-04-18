﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IDCM.Base.AbsInterfaces;
using System.ComponentModel;

namespace IDCM.JobDriver.Core
{
    /// <summary>
    /// 本地后台运行服务池，为后台执行线程
    /// </summary>
    class BGWorkerInvoker
    {
        /// <summary>
        /// 加载后台执行任务袋，并立即提交异步执行操作
        /// </summary>
        /// <param name="handler"></param>
        public static void pushHandler(AbsBGHandler handler,OnJobFinished onfinish, Object args = null)
        {
            if (handler == null)
                return;
            BGHandlerProxy proxy = new BGHandlerProxy(handler);
            BackgroundWorker bgworker = new BackgroundWorker();
            bgworker.WorkerReportsProgress = true;
            bgworker.WorkerSupportsCancellation = true;
            bgworker.DoWork += proxy.worker_DoWork;
            bgworker.ProgressChanged += proxy.worker_ProgressChanged;
            bgworker.RunWorkerCompleted += proxy.worker_RunWorkerCompleted;
            bgworker.RunWorkerCompleted += proxy.worker_cascadeProcess;
            bgworker.RunWorkerCompleted += delegate(object tsender, RunWorkerCompletedEventArgs te) { workCompleted(tsender, te, onfinish); }; ;
            RunningHandlerNoter.note(bgworker, proxy);
            if (args == null)
                bgworker.RunWorkerAsync();
            else
                bgworker.RunWorkerAsync(args);
        }
        /// <summary>
        /// 移除特定的BackgroundWorker实例
        /// </summary>
        /// <param name="worker"></param>
        public static void removeWorker(BackgroundWorker worker)
        {
            if (worker == null)
                return;
            RunningHandlerNoter.removeBackgroundworker(worker);
        }
        /// <summary>
        /// 按照特征类型请求中止后台任务执行请求操作
        /// </summary>
        /// <param name="handlerType"></param>
        public static void abortHandlerByType(Type handlerType)
        {
            KeyValuePair<BackgroundWorker, BGHandlerProxy>[] worker = RunningHandlerNoter.getActiveWorkers();
            foreach (KeyValuePair<BackgroundWorker, BGHandlerProxy> kvpair in worker)
            {
                if (kvpair.Value.getHandler().GetType().Equals(handlerType))
                {
                    BackgroundWorker targetWorker = kvpair.Key;
                    if (targetWorker != null)
                    {
                        if (targetWorker.WorkerSupportsCancellation)
                        {
                            if (!targetWorker.CancellationPending)
                                targetWorker.CancelAsync();
                            RunningHandlerNoter.removeBackgroundworker(targetWorker);
                        }
                    }
                }
            }
        }
        /// <summary>
        /// 后台任务执行结束后的串联执行任务队列的代理实现代码段
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public static void workCompleted(object sender, RunWorkerCompletedEventArgs e, OnJobFinished onfinish)
        {
            if (onfinish != null)
                onfinish(e.Cancelled, e.Error);
        }
    }
    //异步消息事件委托形式化声明
    internal delegate void OnJobFinished(bool Canceled = false, Exception error = null);
}
