using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IDCM.Base.AbsInterfaces;

namespace IDCM.JobDriver.Core
{
    internal class DCMJob
    {
        public DCMJob(AbsBGHandler handler,JobTypes type=JobTypes.Default)
        {
            this.bgHandler = handler;
            this.typecode = type;
            this.jobid = this.GetHashCode();
        }

        public int JID
        {
            get
            {
                return jobid;
            }
        }
        public AbsBGHandler BGHandler
        {
            get
            {
                return bgHandler;
            }
        }

        public JobTypes Types
        {
            get
            {
                return typecode;
            }
        }
        public bool IsInterdictMode
        {
            get
            {
                return (typecode & JobTypes.Interdict) > 0;
            }
        }
        public bool IsReplaceMode
        {
            get
            {
                return (typecode & JobTypes.Replace) > 0;
            }
        }
        public bool IsPriorityMode
        {
            get
            {
                return (typecode & JobTypes.Priority) > 0;
            }
        }
        public bool IsTransaction
        {
            get
            {
                return (typecode & JobTypes.Transaction) > 0;
            }
        }
        public bool IsTemporalMode
        {
            get
            {
                return (typecode & JobTypes.Temporal) > 0 && maxWaitOutSeconds > 0;
            }
        }
        public bool IsStopAble
        {
            get
            {
                return (typecode & JobTypes.StopAble) > 0;
            }
        }
        public bool IsLooping
        {
            get
            {
                return (typecode & JobTypes.Loop) > 0 && minLoopSenconds > 0;
            }
        }
        /// <summary>
        /// 最长候选等待时长设定(以秒为单位)
        /// </summary>
        public int MaxWaitOutSenconds
        {
            set
            {
                maxWaitOutSeconds = value;
                typecode = typecode & (maxWaitOutSeconds > 0 ? JobTypes.MaskCode & JobTypes.Temporal : JobTypes.MaskCode & JobTypes.Longterm);
            }
        }
        /// <summary>
        /// 最小循环时长设定(以秒为单位)
        /// </summary>
        public int MinLoopSenconds
        {
            set
            {
                minLoopSenconds = value;
                typecode = typecode & (minLoopSenconds > 0 ? JobTypes.MaskCode & JobTypes.Loop : JobTypes.MaskCode & JobTypes.Oneoff);
            }
        }

        private AbsBGHandler bgHandler;
        /// <summary>
        /// 最长候选等待时长(以秒为单位)
        /// </summary>
        private int maxWaitOutSeconds = 0;
        /// <summary>
        /// 最小循环时长(以秒为单位)
        /// </summary>
        private int minLoopSenconds = 0;
        /// <summary>
        /// 任务请求类型特征码
        /// </summary>
        private JobTypes typecode = JobTypes.Default;
        /// <summary>
        /// 当前任务识别码
        /// </summary>
        private int jobid;
    }
}
