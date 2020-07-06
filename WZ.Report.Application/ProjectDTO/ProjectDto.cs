using System;
using System.Collections.Generic;
using System.Text;

namespace WZ.Report.Application.ProjectDTO
{
   public class ProjectDto
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

        /// <summary>
        /// 指向其他表 暂时没有用到
        /// </summary>
        public int LinkTable { get; set; }

        /// <summary>
        /// 默认的数据
        /// </summary>
        public string DefaultData { get; set; }


    }
}
