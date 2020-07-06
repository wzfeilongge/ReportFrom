using System;
using System.Collections.Generic;
using System.Text;

namespace WZ.Report.Application.InfoViewModel.ProjectMinModel
{
   public class ProjectMinDto
    {
        /// <summary>
        /// 项目名称
        /// </summary>
        public string ProjectName { get; set; }

        /// <summary>
        /// 次数执行次数
        /// </summary>
        public string RunCount { get; set; }

        /// <summary>
        /// 执行情况
        /// </summary>
        public string RunState { get; set; } = string.Empty;
    }
}
