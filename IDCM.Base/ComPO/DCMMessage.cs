﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDCM.Base.ComPO
{
    public class DCMMessage
    {
        public DCMMessage(DCMMsgType msgType, string msg = null)
        {
            this.msg = msg;
            this.msgType = msgType;
        }
        private readonly string msg;
        public string Msg
        {
            get
            {
                return this.msg;
            }
        }
        private readonly DCMMsgType msgType;

        public DCMMsgType MsgType
        {
            get
            {
                return this.msgType;
            }
        }

        public override string ToString()
        {
            return msg;
        }   
    }
    /// <summary>
    /// 预定义的消息类型
    /// </summary>
    public enum DCMMsgType
    {
        trace = 0,
        tip = 1,
    }
}
