using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WZ.Report.Application.SysViewModel;
using WZ.Report.Common;
using WZ.Report.IServices;

namespace WZ.Report.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AdminController : ControllerBase
    {
        private readonly IProjectInfoService _projectInfoService;
        private readonly ISysUserService _sysUserServices;
        private readonly IFillFormService _fillFormService;
        private readonly IRegisterInfoService _registerInfoService;
        private readonly IUser _user;
        public AdminController(ISysUserService sysUserServices, IProjectInfoService projectInfoService, IFillFormService fillFormService, IUser user, IRegisterInfoService registerInfoService)
        {
            _sysUserServices = sysUserServices;
            _projectInfoService = projectInfoService;
            _fillFormService = fillFormService;
            _registerInfoService = registerInfoService;
            _user = user;
        }

        /// <summary>
        /// 添加一个管理员账户
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("CreateAdminUser")]
        public async Task<IActionResult> CreateAdminUser([FromBody] AddAdminUser model)
        {
            var result = await _sysUserServices.CreateAdmin(model.Username, model.Password);
            return Ok(new
            {
                Success = result
            });
        }

        /// <summary>
        /// 添加一个普通用户
        /// </summary>
        /// <returns></returns>
        [HttpPost("CreateUser")]
        public async Task<IActionResult> CreateUser([FromBody] AddUserModel model)
        {
           var result = await _sysUserServices.CreateUser(model.Username, model.Password,model.Role);
            return Ok(new
            {
                Success = result
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
                Success = true
            });
        }

        /// <summary>
        /// ID查询用户登记表格情况
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpGet("GetUserWriteState")]
        public async Task<IActionResult> GetUserWriteState(int Id)
        {
            var data = await _projectInfoService.GetDataModelinfo(Id);
            return Ok(new
            {
                Success = true,
                data
            });
        }


        /// <summary>
        /// 删除一个用户ID的指定年份月份的表格数据
        /// </summary>
        /// <param name="deleteUserTableModel"></param>
        /// <returns></returns>
        [HttpPost(("DeleteUserTable"))]
        public async Task<IActionResult> DeleteUserTable([FromBody]DeleteUserTableModel deleteUserTableModel)
        {
            if (ModelState.IsValid)
            {
              var adminId = _user.ID;
              var data=   await _fillFormService.DeleteForm(adminId, deleteUserTableModel.DeleteUserTableId, deleteUserTableModel.Year, deleteUserTableModel.Mounth);
                return Ok(new
                {
                    Success = data          
                });
            }
            return Ok(new
            {
                Success = false
            });
        }

        /// <summary>
        /// 管理员权限获取表格登记情况  ?部门负责人和班子成员返回的字段是一样的 党组书记的字段不一样 
        /// </summary>
        /// <param name="RoleId">1 部门负责人 2 党组书记  3 班子成员</param>
        /// <param name="Year">要查询的年份</param>
        /// <param name="PageIndex">当前页码</param>
        /// <param name="PageSize">页码大小</param>
        /// <returns></returns>
        [HttpGet("QueryReport")]
        public async Task<IActionResult> QueryReport(int RoleId=2, int Year=2020,int PageIndex=1,int PageSize=10)
        {
            var data = new object();
            long count = await _registerInfoService.GetCount(x => x.Role == RoleId&&x.Year==Year); 
            if (RoleId == 2)
            {
                 data = await _registerInfoService.GetModelinfoDang(Year,PageIndex, PageSize);
            }
            else {

                 data = await _registerInfoService.GetModelinfo(Year,RoleId, PageIndex,PageSize);
            }
            return Ok(new
            {
                Success = true,
                data,
                datacount=count
            });
        }
    }
}
