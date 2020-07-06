using FreeSql.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Text;

namespace WZ.Report.Model
{

    /// <summary>
    /// 附件 四干预过问
    /// </summary>
    public class Questions : Root
    {
        /// <summary>
        /// 创建人ID
        /// </summary>
        public int CreateId { get; set; }

        /// <summary>
        /// 干预过问人姓名
        /// </summary>
        public string InterventionName { get; set; }

        /// <summary>
        /// 职务
        /// </summary>
        public string Job { get; set; }

        /// <summary>
        /// 内容
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// 情况
        /// </summary>
        public string Situation { get; set; }

        /// <summary>
        /// 月份
        /// </summary>
        public int Mounth { get; set; }

        /// <summary>
        /// 填报年份
        /// </summary>
        public int Year { get; set; }

        /// <summary>
        /// 是否删除
        /// </summary>
        public bool IsDeleted { get; set; } = false;

        [Column(IsVersion = true)]
        public int Version { get; set; }



        public Questions()
        {

        }

    }
}
