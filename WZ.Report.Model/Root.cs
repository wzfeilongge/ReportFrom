using FreeSql.DataAnnotations;
using System;
using System.Collections.Generic;

using System.Security.Principal;
using System.Text;

namespace WZ.Report.Model
{
   public class Root
    {
        [Column(IsIdentity  = true)]
        public int Id { get; set; }
    }
}
