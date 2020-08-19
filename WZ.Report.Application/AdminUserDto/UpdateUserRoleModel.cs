using System;
using System.Collections.Generic;
using System.Text;

namespace WZ.Report.Application.AdminUserDto
{
    public class UpdateUserRoleModel
    {
        /// <summary>
        /// 是否是管理员
        /// </summary>
        public bool Flag { get; set; } = false;

        /// <summary>
        /// 要修改的用户ID
        /// </summary>
        public int UserId { get; set; }
    }
}
