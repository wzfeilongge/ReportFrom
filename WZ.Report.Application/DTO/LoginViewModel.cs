using System;
using System.Collections.Generic;
using System.Text;

namespace WZ.Report.Application.DTO
{
   public class LoginViewModel
    {
        /// <summary>
        /// 用户名 使用 中文名称
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// 密码 使用用户名拼音
        /// </summary>
        public string PassWord { get; set; }
    }
}
