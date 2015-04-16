using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDCM.IDB
{
    public enum IDBStatus
    {
        /// <summary>
        /// 空闲状态中
        /// </summary>
        Idle = 0,
        /// <summary>
        /// 已建立数据文档的连接操作
        /// </summary>
        Connected = 1,
        /// <summary>
        /// 数据源连接保持中，正常工作状态
        /// </summary>
        InWorking = 2,
        /// <summary>
        /// 异常状态
        /// </summary>
        FATAL =3
    }
}
