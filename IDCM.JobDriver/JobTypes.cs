using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDCM.JobDriver
{
    public enum JobTypes
    {
        ///////////////////////////////////////////////////////////////////////////
        NoInterdict = 0x0002,   //backwardType  interdict=false; [Default]
        Interdict =   0x0003,   //backwardType interdict=true;
        //是否要求阻断后续请求  （如果启用阻断机制则优先级别固定位最低）
        ///////////////////////////////////////////////////////////////////////////
        NoReplace=    0x0008,   //forwardType repalce=false; [Default]
        Replace =     0x000c,   //forwardType  repalce=true;
        //是否要求等候队列中满足后者生效机制
        ///////////////////////////////////////////////////////////////////////////
        Normal=       0x0020,   //priorLevel  normal queue wait level; [Default]
        Priority =    0x0030,   //priorLevel  priority queue wait level;
        //是否要求为优先请求
        ///////////////////////////////////////////////////////////////////////////
        KeepWait =    0x0080,   //No Transaction  no delay to execute; [Default]
        Transaction = 0x00c0,   //Use Transaction  delay to execute by service Invoking;
        //是否要求为空闲时执行事务 （作为空闲时执行事务必须提供 序列化事务支持）
        ///////////////////////////////////////////////////////////////////////////
        Longterm=     0x0200,   //long time limit to keep waiting; [Default]
        Temporal=     0x0300,   //temporal time limit to keep waiting; 
        //是否支持等待超时失效特性 （超时失效特性必须提供 最大等候时长）
        ///////////////////////////////////////////////////////////////////////////
        NoStop=       0x0800,   //can not be suspended; [Default]
        StopAble=     0x0C00,   //can be suspended;
        //是否支持运行时中断请求 (有效中断需要线程实例中 支持中断掩码自主识别)
        ///////////////////////////////////////////////////////////////////////////
        Oneoff=       0x2000,   //execute only once; [Default]
        Loop =        0x3000,   //loop execute;
        //是否要求循环调度（循环调度必须提供 最小时长）
        ///////////////////////////////////////////////////////////////////////////
        Default = NoInterdict | NoReplace | Normal | KeepWait | Longterm | NoStop | Oneoff,  //Equals 0x2aaa
        MaskCode = 0x3fff,  //0x3fff Equals Interdict | Replace | Priority | Transaction | Longterm | Temporal | StopAble | Loop
    }
}
