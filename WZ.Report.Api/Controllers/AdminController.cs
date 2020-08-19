using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Renci.SshNet.Messages;
using WZ.Report.Application.AdminUserDto;
using WZ.Report.Application.SysViewModel;
using WZ.Report.Common;
using WZ.Report.IServices;

namespace WZ.Report.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Permissions.Name)]
    public class AdminController : ControllerBase
    {
        private readonly IFillFormService _fillFormService;
        private readonly ILogger<AdminController> _logger;
        private readonly IProjectInfoService _projectInfoService;
        private readonly IRegisterInfoService _registerInfoService;
        private readonly ISysUserService _sysUserServices;
        private readonly IUser _user;
        public AdminController(ISysUserService sysUserServices, IProjectInfoService projectInfoService, IFillFormService fillFormService, IUser user, IRegisterInfoService registerInfoService, ILogger<AdminController> logger)
        {
            _sysUserServices = sysUserServices;
            _projectInfoService = projectInfoService;
            _fillFormService = fillFormService;
            _registerInfoService = registerInfoService;
            _user = user;
            _logger = logger;
        }

        /// <summary>
        ///  添加班子成员
        /// </summary>
        /// <returns></returns>
        [HttpPost("AddUserInfoBan")]
        public async Task<IActionResult> AddUserInfoBan([FromBody] AddBanModel addBanModel)
        {
            if (_user.GetRole() != 0)
            {
                return Ok(new
                {
                    Success = false,
                    Message = "权限不足，请联系管理员使用启用该操作",
                    StatusCode = 200
                });
            }
            var data = await _sysUserServices.AddBanUser(addBanModel);
            _logger.LogInformation($"admin {_user.ID} 正在添加班子成员");
            return Ok(new
            {
                Success = data,
                this.HttpContext.Response.StatusCode
            });
        }

        /// <summary>
        ///  添加部门成员
        /// </summary>
        /// <returns></returns>
        [HttpPost("AddUserInfoBumen")]
        public async Task<IActionResult> AddUserInfoBumen([FromBody] AddBumenModel addBumen)
        {
            if (_user.GetRole() != 0)
            {
                return Ok(new
                {
                    Success = false,
                    Message = "权限不足，请联系管理员使用启用该操作",
                    StatusCode = 200
                });
            }
            var data = await _sysUserServices.AddBumenUser(addBumen);
            _logger.LogInformation($"admin {_user.ID} 正在添加部门成员");
            return Ok(new
            {
                Success = data,
                this.HttpContext.Response.StatusCode
            });
        }

        /// <summary>
        ///  添加党组织成员
        /// </summary>
        /// <returns></returns>
        [HttpPost("AddUserInfoDang")]
        public async Task<IActionResult> AddUserInfoDang([FromBody] AddDangModel addDangModel)
        {
            if (_user.GetRole() != 0)
            {
                return Ok(new
                {
                    Success = false,
                    Message = "权限不足，请联系管理员使用启用该操作",
                    StatusCode = 200
                });
            }
            var data = await _sysUserServices.AddDangUser(addDangModel);
            _logger.LogInformation($"admin {_user.ID} 正在添加党组组织");
            return Ok(new
            {
                Success = data,
                this.HttpContext.Response.StatusCode
            });
        }

        /// <summary>
        /// 添加一个管理员账户
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("CreateAdminUser")]
        public async Task<IActionResult> CreateAdminUser([FromBody] AddAdminUser model)
        {
            if (_user.GetRole() != 0)
            {
                return Ok(new
                {
                    Success = false,
                    Message = "权限不足，请联系管理员使用启用该操作",
                    StatusCode = 200
                });
            }
            var result = await _sysUserServices.CreateAdmin(model.Username, model.Password);
            _logger.LogInformation($"admin {_user.ID} 正在添加管理员账户");
            return Ok(new
            {
                Success = result,
                this.HttpContext.Response.StatusCode
            });
        }

        /// <summary>
        /// 添加一个普通用户
        /// </summary>
        /// <returns></returns>
        [HttpPost("CreateUser")]
        public async Task<IActionResult> CreateUser([FromBody] AddUserModel model)
        {
            if (_user.GetRole() != 0)
            {
                return Ok(new
                {
                    Success = false,
                    Message = "权限不足，请联系管理员使用启用该操作",
                    StatusCode = 200
                });
            }
            var result = await _sysUserServices.CreateUser(model.Username, model.Password, model.Role);
            _logger.LogInformation($"admin {_user.ID} 正在添加普通用户");
            return Ok(new
            {
                Success = result,
                this.HttpContext.Response.StatusCode
            });
        }

        /// <summary>
        ///  危险操作 软删除一个用户 提供Id 数据库里可以恢复
        /// </summary>
        /// <param name="deleteUserModel"></param>
        /// <returns></returns>
        [HttpPost("DeleteSysUserinfoId")]
        public async Task<IActionResult> DeleteSysUserinfoId([FromBody] DeleteUserModel deleteUserModel)
        {
            if (_user.GetRole() != 0)
            {
                return Ok(new
                {
                    Success = false,
                    Message = "权限不足，请联系管理员使用启用该操作",
                    StatusCode = 200
                });
            }
            var data = await _sysUserServices.DeleteUser(deleteUserModel.Id);
            _logger.LogWarning($"admin {_user.ID} 正在删除用户ID {deleteUserModel.Id}");
            return Ok(new
            {
                Success = data,
                this.HttpContext.Response.StatusCode
            });

        }

        /// <summary>
        /// 删除一个用户ID的指定年份月份的表格数据
        /// </summary>
        /// <param name="deleteUserTableModel"></param>
        /// <returns></returns>
        [HttpPost(("DeleteUserTable"))]
        public async Task<IActionResult> DeleteUserTable([FromBody] DeleteUserTableModel deleteUserTableModel)
        {
            if (_user.GetRole() != 0)
            {
                return Ok(new
                {
                    Success = false,
                    Message = "权限不足，请联系管理员使用启用该操作",
                    StatusCode = 200
                });
            }


            if (!ModelState.IsValid)
                return Ok(new
                {
                    Success = false,
                    this.HttpContext.Response.StatusCode
                });
            var adminId = _user.ID;
            var data = await _fillFormService.DeleteForm(adminId, deleteUserTableModel.DeleteUserTableId, deleteUserTableModel.Year, deleteUserTableModel.Mounth);
            _logger.LogWarning($"admin {_user.ID} 正在删除用户ID {deleteUserTableModel.DeleteUserTableId} --{deleteUserTableModel.Year}年 --{deleteUserTableModel.Mounth}月份的表格数据");
            return Ok(new
            {
                Success = data,
                this.HttpContext.Response.StatusCode
            });
        }

        /// <summary>
        /// 获取所有用户信息
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetUserinfo")]
        public async Task<IActionResult> GetUserinfo()
        {
            var data = await _sysUserServices.GetALLUser();
            return Ok(new
            {
                data,
                Success = true,
                this.HttpContext.Response.StatusCode
            });
        }


        /// <summary>
        /// ID 年 月查询用户登记表格情况
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="Year"></param>
        /// <param name="Mounth"></param>
        /// <returns></returns>
        [HttpGet("GetUserWriteState")]
        public async Task<IActionResult> GetUserWriteState(int Id, int Year, int Mounth)
        {
            var data = await _projectInfoService.GetDataModelinfo(Id, Year, Mounth);
            return Ok(new
            {
                Success = true,
                data,
                this.HttpContext.Response.StatusCode
            });
        }

        /// <summary>
        /// 管理员权限获取表格登记情况  ?部门负责人和党组书记返回的字段是一样的 班子成员的字段不一样 
        /// </summary>
        /// <param name="RoleId">1 部门负责人 2 党组书记  3 班子成员</param>
        /// <param name="Year">要查询的年份</param>
        /// <param name="PageIndex">当前页码</param>
        /// <param name="PageSize">页码大小</param>
        /// <returns></returns>
        [HttpGet("QueryReport")]
        public async Task<IActionResult> QueryReport(int RoleId = 2, int Year = 2020, int PageIndex = 1, int PageSize = 10)
        {
            var data = new object();
            var count = await _registerInfoService.GetCount(x => x.Role == RoleId && x.Year == Year);
            if (RoleId == 2)
            {
                data = await _registerInfoService.GetModelinfoDang(Year, PageIndex, PageSize);
            }
            else
            {
                data = await _registerInfoService.GetModelinfo(Year, RoleId, PageIndex, PageSize);
            }
            return Ok(new
            {
                Success = true,
                data,
                datacount = count,
                this.HttpContext.Response.StatusCode
            });
        }

        /// <summary>
        /// 更新用户的用户名和密码
        /// </summary>
        /// <param name="updateUserModel"></param>
        /// <returns></returns>
        [HttpPost("UpdateUserInfo")]
        public async Task<IActionResult> UpdateUserInfo([FromBody] UpdateUserModel updateUserModel)
        {
            if (_user.GetRole() != 0)
            {
                return Ok(new
                {
                    Success = false,
                    Message = "权限不足，请联系管理员使用启用该操作",
                    StatusCode = 200
                });
            }
            var data = await _sysUserServices.UpdateUser(updateUserModel.UserId, updateUserModel.UserName, updateUserModel.PassWord);
            _logger.LogWarning($"admin {_user.ID} 正在修改用户ID {updateUserModel.UserId}的资料  Name={updateUserModel.UserName} Password={updateUserModel.PassWord}");
            return Ok(new
            {
                Success = data,
                this.HttpContext.Response.StatusCode
            });
        }

        /// <summary>
        /// 修改用户的管理员权限  传用户ID 和实际是否要启用的值 true/false
        /// </summary>
        /// <param name="updateUserModel"></param>
        /// <returns></returns>
        [HttpPost("UpdateUserRole")]
        public async Task<IActionResult> UpdateUserRole([FromBody] UpdateUserRoleModel updateUserModel)
        {
            if (_user.GetRole() != 0)
            {
                return Ok(new
                {
                    Success = false,
                    Message = "权限不足，请联系管理员使用启用该操作",
                    StatusCode = 200
                });
            }

            var data = await _sysUserServices.UpdateUserAdminRole(updateUserModel.UserId, updateUserModel.Flag);
            return Ok(new
            {
                Success = data,
                this.HttpContext.Response.StatusCode
            });
        }
    }
}
