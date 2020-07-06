using FreeSql.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Text;

namespace WZ.Report.Model
{
    [Table(Name ="projectinfo")]
    /// <summary>
    /// 责任项目通用表
    /// </summary>
   public class ProjectInfo : Root
    {

        /// <summary>
        /// 责任项目
        /// </summary>
        public string ProjectName { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        public int Sort { get; set; }

        /// <summary>
        /// 执行次数
        /// </summary>
        public  string RunCount { get; set; }

        /// <summary>
        /// 软权限
        /// </summary>
        public int Role { get; set; }

        /// <summary>
        /// 指向其他表
        /// </summary>
        public int LinkTable { get; set; }

        /// <summary>
        /// 默认的数据
        /// </summary>
        public string DefaultData { get; set; }

        [Column(IsVersion = true)]
        public int Version { get; set; }



        public ProjectInfo()
        {

        }


    }
}
