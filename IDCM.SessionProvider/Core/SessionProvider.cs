using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Concurrent;
using System.Windows.Forms;

namespace IDCM.SessionProvider.Core
{
    internal class SessionProvider
    {
        internal static string addControl(string group, Control ctrl)
        {
            ConcurrentDictionary<string,Control> controls=null;
            if(!controlCache.TryGetValue(group,out controls))
            {
                controls=new ConcurrentDictionary<string,Control>();
                controlCache.TryAdd(group,controls);
            }
            controls.AddOrUpdate(ctrl.Name,ctrl,(tkey,tval)=>tval=ctrl);
            return ctrl.Name;
        }
        internal static Control getControl(string group, string name)
        {
            ConcurrentDictionary<string,Control> controls=null;
            if(controlCache.TryGetValue(group,out controls))
            {
                Control ctrl = null;
                if (controls.TryGetValue(name, out ctrl))
                    return ctrl;
            }
            return null;
        }
        internal static bool removeControl(string group, string name)
        {
            ConcurrentDictionary<string, Control> controls = null;
            if (controlCache.TryGetValue(group, out controls))
            {
                Control ctrl = null;
                return controls.TryRemove(name, out ctrl);
            }
            return false;
        }
        internal static bool removeControls(string group)
        {
            ConcurrentDictionary<string, Control> controls = null;
            return controlCache.TryRemove(group, out controls);
        }
        private static ConcurrentDictionary<string,ConcurrentDictionary<string,Control>> controlCache=new ConcurrentDictionary<string,ConcurrentDictionary<string,Control>>();
    }
}
