using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDCM.Base.ComPO
{
    /// <summary>
    /// 异步消息类型及附属参数的封装类
    /// </summary>
    public class AsyncMsgNotice
    {
        public static readonly AsyncMsgNotice DataPrepared = new AsyncMsgNotice(MsgNoticeType.DataPrepared, "Data Prepared");
        public static readonly AsyncMsgNotice RetryQuickStartConnect = new AsyncMsgNotice(MsgNoticeType.RetryQuickStartConnect, "Retry Quick Start Connect");
        
        /// <summary>
        /// For iterator 
        /// </summary>
        public static IEnumerable<AsyncMsgNotice> Values
        {
            get
            {
                yield return DataPrepared;
                yield return RetryQuickStartConnect;
            }
        }
        private readonly string msgTag;
        private readonly MsgNoticeType msgType;
        private readonly object[] parameters;

        public AsyncMsgNotice(AsyncMsgNotice amsg, params object[] parameters)
        {
            this.msgTag = amsg.msgTag;
            this.msgType = amsg.msgType;
            this.parameters = parameters;
        }

        protected AsyncMsgNotice(MsgNoticeType msgType, string msgTag, object[] parameters = null)
        {
            this.msgTag = msgTag;
            this.msgType = msgType;
            this.parameters = parameters;
        }

        public string MsgTag { get { return msgTag; } }

        public MsgNoticeType MsgType { get { return msgType; } }

        public object[] Parameters { get { return parameters; } }

        public override string ToString()
        {
            return msgType + ":" + msgTag;
        }
    }
    /// <summary>
    /// 预定义的消息类型
    /// </summary>
    public enum MsgNoticeType
    {
        DataPrepared = 0,
        RetryQuickStartConnect = 1,
    }
}
