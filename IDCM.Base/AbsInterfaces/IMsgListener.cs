using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IDCM.Base.ComPO;

namespace IDCM.Base.AbsInterfaces
{
    public interface IMsgListener
    {
        void reportJobProgress(object handle, Int32 percent);
        void reportSimpleMsg(object handle, DCMMessage dmsg);
        void reportJobFeedback(object handle, AsyncMsgNotice amsg);
    }
}
