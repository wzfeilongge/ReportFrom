using System;
using System.Collections.Generic;
using System.Text;

namespace WZ.Report.Application.InfoViewModel.ProjectMinModel
{

    /// <summary>
    /// 表格四 真实表格DTO
    /// </summary>
   public class ProjectMinFourDto
    {
        public int TableId { get; set; }

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
        /// 内容
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// 情况
        /// </summary>
        public string Situation { get; set; }
    }
}
