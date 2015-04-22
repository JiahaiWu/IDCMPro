using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IDCM.Base.AbsInterfaces;
using IDCM.Base.ComPO;

namespace IDCM.VModule.GCM.ViewManager
{
    internal class AsyncServInvoker:IMsgListener
    {
        #region GCM Pro 异步消息事务分发处理
        /// <summary>
        /// 消息事件分发处理
        /// </summary>
        /// <param name="msg"></param>
        internal void reportJobProgress(object handle, Int32 percent)
        {
            
        }
        /// <summary>
        /// 消息事件分发处理
        /// </summary>
        /// <param name="msg"></param>
        internal void reportSimpleMsg(object handle, DCMMessage dmsg)
        {
            if (dmsg == null)
                return;
            
        }
        /// <summary>
        /// 消息事件分发处理
        /// </summary>
        /// <param name="msg"></param>
        internal void reportJobFeedback(object handle, AsyncMsgNotice amsg)
        {
            if (amsg == null)
                return;
            switch (amsg.MsgType)
            {
                case MsgNoticeType.DataPrepared:
                    OnDataPrepared(this, new IDCMAsyncEventArgs(amsg.MsgTag,amsg.Parameters));
                    break;
                default:
                    log.Warn("Unhandled asynchronous message.  @msgTag=" + amsg.MsgTag);
                    break;
            }
        }

        //定义数据源加载完成事件
        internal event IDCMAsyncRequest OnDataPrepared;
        #endregion

        //异步消息事件委托形式化声明
        public delegate void IDCMAsyncRequest(object sender, IDCMAsyncEventArgs e);
        private static NLog.Logger log = NLog.LogManager.GetCurrentClassLogger();
    }
    /// <summary>
    /// 异步消息事件消息细节参数类定义
    /// </summary>
    internal class IDCMAsyncEventArgs : EventArgs
    {
        public readonly string msgTag;
        public readonly object[] values;

        public IDCMAsyncEventArgs(string msgTag,params object[] vals)
        {
            this.msgTag = msgTag;
            this.values = vals;
        }
    }
}
