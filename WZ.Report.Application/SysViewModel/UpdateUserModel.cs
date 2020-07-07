using System;
using System.Collections.Generic;
using System.Text;

namespace WZ.Report.Application.SysViewModel
{
    public class UpdateUserModel
    {

        /// <summary>
        /// 用户Id
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// 登录名
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 登录密码
        /// </summary>
        public string PassWord { get; set; }
    }
}
