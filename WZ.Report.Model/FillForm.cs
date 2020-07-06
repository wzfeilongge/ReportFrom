using FreeSql.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Text;

namespace WZ.Report.Model
{
    /// <summary>
    /// 填报
    /// </summary>
    /// 

   public class FillForm :Root
    {
        /// <summary>
        /// 填报人Id
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// 填报权限
        /// </summary>
        public int Role { get; set; }

        /// <summary>
        /// 填报年份
        /// </summary>
        public int Year { get; set; }

        /// <summary>
        /// 填报月份
        /// </summary>
        public int Mounth { get; set; }

        /// <summary>
        /// 是否删除
        /// </summary>
        public bool IsDeleted { get; set; } = false;

        [Column(DbType = "longtext")]
        /// <summary>
        /// 当前填写表格 月份数据 Json
        /// </summary>       
        public string TableData { get; set; }
        [Column(DbType = "longtext")]
        public string TableFour { get; set; }
        [Column(DbType = "longtext")]
        public string TableFive { get; set; }

        [Column(IsVersion = true)]
        public int Version { get; set; }


        public FillForm()
        {

        }
    }
}
