using System;
using System.Collections.Generic;
using System.Text;

namespace WZ.Report.Application.DTO
{
   public class LoginResultModel
    {
        public int UserId { get; set; }
        public int RoleId { get; set; }

        public string UserName { get; set; }

        public string AccessToken { get; set; }

    }
}
