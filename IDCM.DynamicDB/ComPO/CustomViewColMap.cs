using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDCM.DynamicDB
{
    public class CustomViewColMap
    {
        /// <summary>
        /// 属性名称(属性名称可以由大小写字母、数字、下划线组成，字段名大小写敏感)
        /// </summary>
        public string Attr { get; set; }
        /// <summary>
        /// 数据展示映射位序(正值视为有效，负值视为隐藏)
        /// </summary>
        public int ViewOrder { get; set; }
        /// <summary>
        /// 从属表名(名称可以由大小写字母、数字、下划线组成，字段名大小写敏感)
        /// </summary>
        public string TName { get; set; }
        /// <summary>
        /// 主表域最大显示字段数
        /// </summary>
        public const int MaxMainViewCount = 1000;
    }
}
