using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IDCM.Base;

namespace IDCM.JobDriver
{
    public class JobHandOption:ICloneable
    {
        public JobHandOption(bool isInterdict=false, bool isReplace=false, bool isPriority=false, bool isLazyTransaction=false, bool stopAble=false, int maxWaitOutSeconds=0)
        {
            this.interdict = isInterdict;
            this.replaceOld = isReplace;
            this.priority = isPriority;
            this.lazyTransaction = isLazyTransaction;
            this.stopAble = stopAble;
            this.maxWaitOutSeconds = maxWaitOutSeconds;
            temporal = maxWaitOutSeconds > 0;
        }
        public bool IsInterdictMode
        {
            get
            {
                return interdict;
            }
            set
            {
                interdict = value;
            }
        }
        public bool IsReplaceMode
        {
            get
            {
                return replaceOld;
            }
            set
            {
                replaceOld = value;
            }
        }
        public bool IsPriorityMode
        {
            get
            {
                return priority;
            }
            set
            {
                priority = value;
            }
        }
        public bool IsLazyTransaction
        {
            get
            {
                return lazyTransaction;
            }
            set
            {
                lazyTransaction = true;
            }
        }
        public bool IsTemporalMode
        {
            get
            {
                return temporal && maxWaitOutSeconds>0;
            }
            set
            {
                if (value && maxWaitOutSeconds>0)
                {
                    temporal = value;
                }
                else if (value == false)
                {
                    temporal = value;
                    maxWaitOutSeconds = 0;
                }
                else
                {
                    throw new IDCMException("It's should update the value of MaxWaitOutSeconds greater than zero instead of changing this attribute.");
                }
            }
        }
        public bool IsStopAble
        {
            get
            {
                return stopAble;
            }
            set
            {
                stopAble = value;
            }
        }

        /// <summary>
        /// 最长候选等待时长设定(以秒为单位)
        /// </summary>
        public int MaxWaitOutSenconds
        {
            get
            {
                return maxWaitOutSeconds;
            }
            set
            {
                maxWaitOutSeconds = value;
                temporal=maxWaitOutSeconds > 0;
            }
        }

        public object Clone()
        {
            return new JobHandOption(interdict, replaceOld, priority, lazyTransaction, stopAble, maxWaitOutSeconds) as object;
        }
        /// <summary>
        /// 最长候选等待时长(以秒为单位)
        /// </summary>
        private int maxWaitOutSeconds = 0;
        /// <summary>
        /// 是否要求阻断后续请求  （如果启用阻断机制则优先级别固定位最低）
        /// </summary>
        private bool interdict = false;
        /// <summary>
        /// 是否要求等候队列中满足后者生效机制
        /// </summary>
        private bool replaceOld=false;
        /// <summary>
        /// 是否要求为优先请求
        /// </summary>
        private bool priority=false;
        /// <summary>
        /// 是否要求为空闲时懒加载的执行事务 （作为空闲时执行事务必须提供 序列化事务支持）
        /// </summary>
        private bool lazyTransaction=false;
        /// <summary>
        /// 是否支持等待超时失效特性 （超时失效特性必须提供 最大等候时长）
        /// </summary>
        private bool temporal=false;
        /// <summary>
        /// 是否支持运行时中断请求 (有效中断需要线程实例中 支持中断掩码自主识别)
        /// </summary>
        private bool stopAble=false;
        /// <summary>
        /// 是否要求循环调度（循环调度必须提供 最小时长）
        /// </summary>
        private bool loop=false;
    }
}
