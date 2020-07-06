using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WZ.Report.Application.DTO;
using WZ.Report.Application.WriteProjectDto;
using WZ.Report.IServices.Base;
using WZ.Report.Model;

namespace WZ.Report.IServices
{
    public interface ISysUserService :IBaseServices<SysUser>
    {
        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="loginViewModel"></param>
        /// <returns></returns>
        Task<LoginResultModel> Login(LoginViewModel loginViewModel);

        /// <summary>
        /// 获取所有管理员用户信息
        /// </summary>
        /// <returns></returns>
        Task<List<SysUser>> GetALLUser();

        /// <summary>
        /// 创建管理员账号
        /// </summary>
        /// <param name="UserName"></param>
        /// <param name="Password"></param>
        /// <returns></returns>
        Task<bool> CreateAdmin(string UserName,string Password);

        /// <summary>
        /// 创建普通用户
        /// </summary>
        /// <param name="UserName"></param>
        /// <param name="Password"></param>
        /// <param name="Role"></param>
        /// <returns></returns>
        Task<bool> CreateUser(string UserName,string Password,int Role);
    }
}
