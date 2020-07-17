using System;
using System.Collections.Generic;
using System.Text;

namespace WZ.Report.Application.AdminUserDto
{
   public class AddDangModel
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

        /// <summary>
        /// 1 部门负责人  2 党组书记 3 班子成员 0管理员
        /// </summary>
        public int Role { get; set; } = 2;

        public bool IsAdmin { get; set; } = false;

    }
}
