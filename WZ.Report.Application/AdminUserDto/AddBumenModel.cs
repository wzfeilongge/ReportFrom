﻿using System;
using System.Collections.Generic;
using System.Text;

namespace WZ.Report.Application.AdminUserDto
{
   public class AddBumenModel
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
        public int Role { get; set; } = 1;

        /// <summary>
        /// 部门
        /// </summary>
        public string Department { get; set; }

        public bool IsAdmin { get; set; } = false;
    }
}
