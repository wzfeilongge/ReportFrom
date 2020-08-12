using System;
using System.Collections.Generic;
using System.Text;
using WZ.Report.Application.ProjectDTO;
using WZ.Report.Model;

namespace WZ.Report.Application.WriteProjectDto
{
    /// <summary>
    /// 表格Model
    /// </summary>
   public class WriteModel
    {
        /// <summary>
        /// 填报人Id
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// 责任人
        /// </summary>
        public string DutyPeople { get; set; }

        /// <summary>
        /// 填报权限
        /// </summary>
        public int Role { get; set; }

        /// <summary>
        /// 填报月份
        /// </summary>
        public int Mounth { get; set; }

        /// <summary>
        /// 填报年份
        /// </summary>
        public int Year { get; set; } = 2020;

        /// <summary>
        /// 表格内容
        /// </summary>
        public List<ProjectDto> Tables { get; set; }

        /// <summary>
        /// 附件四
        /// </summary>
        public List<ProjectFourDto> TablesFour { get; set; } = new List<ProjectFourDto>();

        /// <summary>
        /// 附件五
        /// </summary>
        public List<ProjectFiveDto> TableFive { get; set; } = new List<ProjectFiveDto>();
    }
}
