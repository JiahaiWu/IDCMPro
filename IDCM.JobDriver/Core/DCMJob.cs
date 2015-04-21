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
        public DCMJob(AbsBGHandler handler)
        {
            this.bgHandler = handler;
            this.jobOption = new JobHandOption();
            this.jobid = handler.GetType().GetHashCode();
            timestamp = DateTime.Now.Ticks;
        }
        public DCMJob(AbsBGHandler handler, JobHandOption option)
        {
            this.bgHandler = handler;
            this.jobOption = option;
            this.jobid = handler.GetType().GetHashCode();
            timestamp = DateTime.Now.Ticks;
        }

        internal int JID
        {
            get
            {
                return jobid;
            }
            set
            {
                jobid = value;
            }
        }
        public AbsBGHandler BGHandler
        {
            get
            {
                return bgHandler;
            }
        }

        public JobHandOption JobOption
        {
            get
            {
                return jobOption;
            }
        }
        public bool TimeOut
        {
            get
            {
                return jobOption.IsTemporalMode &&
                    new TimeSpan(DateTime.Now.Ticks - timestamp).TotalMilliseconds>jobOption.MaxWaitOutSenconds*1000;
            }
        }
        public object[] Args
        {
            get
            {
                return _args.ToArray();
            }
        }
        public void appendArgs(object[] args)
        {
            if (_args == null)
                _args = new List<object>();
            _args.AddRange(args);
        }
        /// <summary>
        /// created time stamp
        /// </summary>
        private long timestamp;
        private AbsBGHandler bgHandler;
        /// <summary>
        /// 任务请求类型特征码
        /// </summary>
        private JobHandOption jobOption;
        /// <summary>
        /// 当前任务识别码
        /// </summary>
        private int jobid;

        private List<object> _args;
    }
}
