using System;
using System.Collections.Generic;
using System.Text;
using WZ.Report.Application.ResutData;

namespace WZ.Report.Extensions.AuthorizationSetup.Policys
{
    public class ApiResponse
    {
        public int Status { get; set; } = 404;
        public string Value { get; set; } = "No Found";
        public MessageModel<string> MessageModel ;

        public ApiResponse(StatusCode apiCode, string msg = null)
        {
            switch (apiCode)
            {
                case StatusCode.CODE401:
                    Status = 401;
                    Value = "Token无效 Httpcontext 解析失败";
                    break;
                case StatusCode.CODE403:
                    Status = 403;
                    Value = "Token过期";
                    break;
                case StatusCode.CODE500:
                    Status = 500;
                    Value = msg;
                    break;
            }

            MessageModel = new MessageModel<string>()
            {
                StatusCode = Status,
                Msg = Value,
                Success = false
            };
        }
    }

    public enum StatusCode
    {
        CODE401,
        CODE403,
        CODE404,
        CODE500
    }
}
