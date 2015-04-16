using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDCM.DynamicDB
{
    public class CustomTableDef
    {
        /// <summary>
        /// 数据表名称(名称可以由大小写字母、数字、下划线组成，字段名大小写敏感)
        /// </summary>
        public string TName { get; set; }
        /// <summary>
        /// 数据表注解说明
        /// </summary>
        public string Comments { get; set; }
        /// <summary>
        /// 数据表创建日期
        /// </summary>
        public string CreateDate { get; set; }
    }
}
