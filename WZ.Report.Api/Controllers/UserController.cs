using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Castle.Core.Logging;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WZ.Report.Application.DTO;
using WZ.Report.Application.ResutData;
using WZ.Report.Common;
using WZ.Report.IServices;

namespace WZ.Report.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IFillFormService _fillFormService;
        private readonly ISysUserService _sysUserServices;
        private readonly IUser _user;
        private readonly ILogger<UserController> _logger;
        public UserController(ISysUserService sysUserServices, IUser user, IFillFormService fillFormService, ILogger<UserController> logger)
        {
            _sysUserServices = sysUserServices;
            _user = user;
            _fillFormService = fillFormService;
            _logger = logger;
        }

        /// <summary>
        /// 分页查询 登录用户的表格填写情况
        /// </summary>
        /// <param name="pageindex">当前页码</param>
        /// <param name="pagesize">页码大小</param>
        /// <returns></returns>
        [HttpGet("GetWriteInfo")]
        public async Task<IActionResult> GetWriteInfo(int pageindex = 1, int pagesize = 10)
        {
            var id = _user.ID;
            if (id==0)
            {
                _logger.LogWarning($"{DateTime.Now} 请求GetWriteInfo access_token 无效");
                return Ok(new
                {
                    Sucess = false,
                    Msg = "Token无效 Httpcontext 解析失败"
                }) ;                                                 
            }
            var data = await _fillFormService.GetFillformDtos(pageindex, pagesize,id);
            var count = await _fillFormService.GetCount(x => x.UserId == id);
            return Ok(new
            {
                Success = true,
                data,
                count,
                Page= pageindex,
                PageSize= pagesize
            });
        }

        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginViewModel model)
        {
            var data = await _sysUserServices.Login(model);
            if (data == null)
            {
                return Ok(new MessageModel<LoginResultModel>
                {
                    Success = false,
                    Msg = "登录失败，用户名或密码错误",
                });
            }
            return Ok(new MessageModel<LoginResultModel>
            {
                Success = true,
                Msg = "登录成功",
                Response = data
            });
        }
    }
}
