using System;
using System.Collections.Generic;
using System.Text;

namespace WZ.Report.Common
{
   public interface IUser
    {
        string Name { get; }
        int ID { get; }
        string GetToken();

        int GetRole();

        string GetClientIP();

    }
}
