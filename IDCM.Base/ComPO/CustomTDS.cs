using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDCM.Base.ComPO
{
    public class CustomTDS
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
        /// 自定义数据列定义集
        /// </summary>
        public List<CustomColDef> ccds { get; set; }
    }
}
