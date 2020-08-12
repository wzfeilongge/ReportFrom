using System;
using System.Collections.Generic;
using System.Text;

namespace WZ.Report.Application.ProjectDTO
{

    /// <summary>
    /// 附件四
    /// </summary>
    public class ProjectFourDto
    {

        /// <summary>
        /// 内容
        /// </summary>
        public string Content { get; set; }

        public int CreateId { get; set; }
        /// <summary>
        /// 部门 填报人
        /// </summary>
        public string Department { get; set; }

        /// <summary>
        /// 干预过问人姓名
        /// </summary>
        public string InterventionName { get; set; }

        /// <summary>
        /// 职务
        /// </summary>
        public string Job { get; set; }

        /// <summary>
        /// 月份
        /// </summary>
        public int Mounth { get; set; }

        /// <summary>
        /// 情况
        /// </summary>
        public string Situation { get; set; }

        public int TableId { get; set; }
        /// <summary>
        /// 填报年份
        /// </summary>
        public int Year { get; set; }

    }
}
