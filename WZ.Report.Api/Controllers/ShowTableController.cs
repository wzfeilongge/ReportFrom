using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper.Configuration.Conventions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WZ.Report.Application.WriteProjectDto;
using WZ.Report.Common;
using WZ.Report.IServices;

namespace WZ.Report.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShowTableController : ControllerBase
    {
        private readonly ILogger<ShowTableController> _logger;
        private readonly IProjectInfoService _ProjectInfoService;
        private readonly IUser _User;
        public ShowTableController(IProjectInfoService projectInfoService, IUser User, ILogger<ShowTableController> logger)
        {
            _ProjectInfoService = projectInfoService;
            _User = User;
            _logger = logger;
        }

        /// <summary>
        /// 获取表格数据
        /// </summary>      
        /// <returns></returns>
        [HttpGet("GetTable")]
        public async Task<IActionResult> GetTable()
        {
            var UserId = _User.ID;
            if (UserId == 0)
            {
                _logger.LogWarning($"{DateTime.Now} 请求GetTable access_token 无效");
                this.HttpContext.Response.StatusCode = 401;
                return Ok(new
                {
                    Success = false,
                    Table = new List<object>(),
                    OtherTable = new object(),
                    Msg = "Token无效 Httpcontext 解析失败",
                    StatusCode = this.HttpContext.Response.StatusCode
                });
            }
            var Role = _User.GetRole();
            
          //  Console.WriteLine($"RoleId  {roleId}");
            var Table = await _ProjectInfoService.GetProjects(Role);
            if (Table == null || Table.Count < 0)
            {
                return Ok(new
                {
                    Success = false,
                    Table,
                    OtherTable = new object(),
                    this.HttpContext.Response.StatusCode
                });
            }
            var OtherTable = await _ProjectInfoService.GetOtherProjects(Role);
            return Ok(new
            {
                Success = true,
                Table,
                OtherTable,
                this.HttpContext.Response.StatusCode
            });
        }

        /// <summary>
        /// 获取是否可以填写表格
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetWriteState")]
        public async Task<IActionResult> GetWriteState(int mouth, int year = 2020)
        {
            var userId = _User.ID;
            if (userId == 0)
            {
                _logger.LogWarning($"{DateTime.Now} 请求GetWriteState access_token 无效");
                this.HttpContext.Response.StatusCode = 401;
                return Ok(new
                {
                    Success = false,
                    Msg = "Token无效 Httpcontext 解析失败",
                    this.HttpContext.Response.StatusCode
                });
            }
            var result = await _ProjectInfoService.IsEnableMouth(userId, mouth, year);
            _logger.LogInformation($"IP: {_User.GetClientIP()} 用户名：{_User.Name} ID：{_User.ID}  查询了GetWriteState接口");
            return Ok(new
            {
                Success = result,
                Msg="接口请求成功",
                this.HttpContext.Response.StatusCode
            });
        }

        /// <summary>
        /// 写入表格数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("WriteTable")]
        public async Task<IActionResult> WriteTable([FromBody] WriteModel model)
        {        
            model.UserId = _User.ID;
            if (model.UserId == 0)
            {
                _logger.LogWarning($"{DateTime.Now} 请求WriteTable access_token 无效");
                this.HttpContext.Response.StatusCode = 401;
                return Ok(new
                {
                    Success = false,
                    Msg = "Token无效 Httpcontext 解析失败",
                    this.HttpContext.Response.StatusCode
                });
            }
            model.Role = _User.GetRole();
            var result = await _ProjectInfoService.WriteTableData(model);
            _logger.LogInformation($"IP: {_User.GetClientIP()} 用户名：{_User.Name} ID：{_User.ID}  成功写入了数据");
            return Ok(new
            {
                Success = result,
                Msg = result == true ? "写入成功" : "写入失败",
                this.HttpContext.Response.StatusCode
            });
        }
    }
}
