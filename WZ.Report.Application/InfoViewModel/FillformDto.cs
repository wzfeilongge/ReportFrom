using System;
using System.Collections.Generic;
using System.Text;
using WZ.Report.Application.InfoViewModel.ProjectMinModel;

namespace WZ.Report.Application.InfoViewModel
{
   public class FillformDto
    {
        public int Year { get; set; }
        public int Mounth { get; set; }

        /// <summary>
        /// 表格内容
        /// </summary>
        public List<ProjectMinDto> Tables { get; set; }
        
        /// <summary>
        /// 附件四
        /// </summary>
        public List<ProjectMinFourDto> TablesFour { get; set; }

        /// <summary>
        /// 附件五
        /// </summary>
        public List<ProjectMinFiveDto> TableFive { get; set; }
    }
}
