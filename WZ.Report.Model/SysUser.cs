using FreeSql.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Text;

namespace WZ.Report.Model
{


    /// <summary>
    /// 用户信息表
    /// </summary>
    [Table(Name ="sysuser")]
    public class SysUser :Root
    {
        /// <summary>
        /// 登录名称
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 登录密码
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// 联系电话
        /// </summary>
        public string UserPhone { get; set; }

        /// <summary>
        /// 1 部门负责人  2 党组书记 3 班子成员 0管理员
        /// </summary>
        public int Role { get; set; }

        /// <summary>
        /// 部门
        /// </summary>
        public string Department { get; set; }

        /// <summary>
        /// 法院地址
        /// </summary>
        public string JobAddress { get; set; }

        /// <summary>
        /// 承办人
        /// </summary>
        public string Undertaker { get; set; }

        /// <summary>
        /// 承办人手机号
        /// </summary>
        public string UndertakerPhone { get; set; }

        [Column(IsVersion = true)]
        public int Version { get; set; }

        /// <summary>
        /// 软删除
        /// </summary>
        public bool IsDelete { get; set; } = false;

        public SysUser()
        {

        }
    }
}
