using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IDCM.Base.ComPO;
using IDCM.Base.AbsInterfaces;
using System.Threading;
using System.Collections.Concurrent;
using IDCM.Base;
using System.Windows.Forms;

namespace IDCM.MsgDriver
{
    public class DCMPublisher
    {
        public static void noteJobProgress(object handle, Int32 percent)
        {
            if (msgObs == null)
                throw new IDCMException("Default MsgObserver hasn't inited for publish Message!");
            sendMsgCache.Enqueue(new ObjectPair<object, object>(handle, percent));
            messageMonitor.Enabled = true;
        }
        public static void noteSimpleMsg(object handle, DCMMessage dmsg)
        {
            if (msgObs == null)
                throw new IDCMException("Default MsgObserver hasn't inited for publish Message!");
            sendMsgCache.Enqueue(new ObjectPair<object, object>(handle, dmsg));
            messageMonitor.Enabled = true;
        }
        public static void noteSimpleMsg(string msg, DCMMsgType msgType = DCMMsgType.tip)
        {
            if (msgObs == null)
                throw new IDCMException("Default MsgObserver hasn't inited for publish Message!");
            sendMsgCache.Enqueue(new ObjectPair<object, object>(Thread.CurrentThread.ManagedThreadId, new DCMMessage(msgType, msg)));
            messageMonitor.Enabled = true;
        }
        public static void noteJobFeedback(object handle, AsyncMsgNotice amsg)
        {
            if (msgObs == null)
                throw new IDCMException("Default MsgObserver hasn't inited for publish Message!");
            sendMsgCache.Enqueue(new ObjectPair<object, object>(handle, amsg));
            messageMonitor.Enabled = true;
        }
        public static IMsgObserver initDefaultMsgObserver()
        {
            msgObs=new MsgObserver();
            sendMsgCache = new ConcurrentQueue<ObjectPair<object, object>>();
            messageMonitor=new System.Windows.Forms.Timer();
            messageMonitor.Interval = 5;
            messageMonitor.Tick += OnMMHeartBreak;
            messageMonitor.Enabled = false;
            return msgObs;
        }
        /// <summary>
        /// 异步消息轮询监视器的心跳检测事件处理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void OnMMHeartBreak(object sender, EventArgs e)
        {
            ObjectPair<object, object> sendPair = null;
            if (msgObs == null)
                throw new IDCMException("Default MsgObserver hasn't inited for publish Message!");
            ICollection<IMsgListener> listeners = msgObs.getMsgListeners();
            while (sendMsgCache.TryDequeue(out sendPair))
            {
                foreach (IMsgListener ls in listeners)
                {
                    string srcType=sendPair.Val.GetType().FullName;
                    if(srcType.Equals(typeof(Int32).FullName))
                        ls.reportJobProgress(sendPair.Key, (Int32)sendPair.Val);
                    else if(srcType.Equals(typeof(DCMMessage).FullName))
                        ls.reportSimpleMsg(sendPair.Key, (DCMMessage)sendPair.Val);
                    else if (srcType.Equals(typeof(AsyncMsgNotice).FullName))
                        ls.reportJobFeedback(sendPair.Key, (AsyncMsgNotice)sendPair.Val);
                }
            }
            messageMonitor.Enabled = false;
        }
        private static MsgObserver msgObs=null;
        /// <summary>
        /// Message Instance Monitor
        /// </summary>
        private static System.Windows.Forms.Timer messageMonitor = null;
        /// <summary>
        /// send Message Cache
        /// </summary>
        private static ConcurrentQueue<ObjectPair<object, object>> sendMsgCache = null;
    }
}
