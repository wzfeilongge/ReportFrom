using System;
using System.Collections.Generic;
using System.Text;
using WZ.Report.Application.ProjectDTO;

namespace WZ.Report.Application.WriteProjectDto
{
    public class WriteData
    {
        public List<ProjectDto> Tables { get; set; }

        /// <summary>
        /// 附件四
        /// </summary>
        public ProjectFourDto TablesFour { get; set; } = null;

        /// <summary>
        /// 附件五
        /// </summary>
        public ProjectFiveDto TableFive { get; set; } = null;

    }
}
