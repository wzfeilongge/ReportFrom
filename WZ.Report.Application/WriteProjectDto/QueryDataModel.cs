using System;
using System.Collections.Generic;
using System.Text;
using WZ.Report.Application.ProjectDTO;

namespace WZ.Report.Application.WriteProjectDto
{
    public class Data
    {
        /// <summary>
        /// 登记月份
        /// </summary>
        public int Mounth { get; set; }

        /// <summary>
        /// 登记年份
        /// </summary>
        public int Year { get; set; }

        /// <summary>
        /// 责任人
        /// </summary>
        public string DutyPeople { get; set; }

        /// <summary>
        /// 表格数据
        /// </summary>
        public List<ProjectDto> TableData { get; set; }

        public List<ProjectFiveDto> TableFive { get; set; }

        public List<ProjectFourDto> TableFour { get; set; }




    }

    public class QueryDataModel
    {
        public int Id { get; set; }

        public string UserName { get; set; }

        public List<Data> Data { get; set; }

        public int Count = 0;
    }
}
