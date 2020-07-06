using System;
using System.Collections.Generic;
using System.Text;

namespace WZ.Report.Application.SysViewModel
{
   public class DeleteUserTableModel
    {
        public int DeleteUserTableId { get; set; }

        /// <summary>
        /// 填报月份
        /// </summary>
        public int Mounth { get; set; }

        /// <summary>
        /// 填报年份
        /// </summary>
        public int Year { get; set; } 
    }
}
