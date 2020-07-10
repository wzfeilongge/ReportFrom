using System;
using System.Collections.Generic;
using System.Text;

namespace WZ.Report.Application.AdminUserDto
{
   public class DeleteUserModel
    {


        /// <summary>
        /// 需要删除的用户ID 危险操作 内部软删除 可以恢复
        /// </summary>
        public int  Id { get; set; }

    }
}
