using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WZ.Report.Application.AdminUserDto;
using WZ.Report.Application.DTO;
using WZ.Report.Application.WriteProjectDto;
using WZ.Report.IServices.Base;
using WZ.Report.Model;

namespace WZ.Report.IServices
{
    public interface ISysUserService : IBaseServices<SysUser>
    {
        /// <summary>
        /// 添加班子成员
        /// </summary>
        /// <param name="banModel"></param>
        /// <returns></returns>
        Task<bool> AddBanUser(AddBanModel banModel);

        /// <summary>
        /// 添加部门成员
        /// </summary>
        /// <param name="dangModel"></param>
        /// <returns></returns>
        Task<bool> AddBumenUser(AddBumenModel dangModel);

        /// <summary>
        /// 添加党组书记
        /// </summary>
        /// <param name="dangModel"></param>
        /// <returns></returns>
        Task<bool> AddDangUser(AddDangModel dangModel);

        /// <summary>
        /// 创建管理员账号
        /// </summary>
        /// <param name="UserName"></param>
        /// <param name="Password"></param>
        /// <returns></returns>
        Task<bool> CreateAdmin(string UserName, string Password);

        /// <summary>
        /// 创建普通用户
        /// </summary>
        /// <param name="UserName"></param>
        /// <param name="Password"></param>
        /// <param name="Role"></param>
        /// <returns></returns>
        Task<bool> CreateUser(string UserName, string Password, int Role);

        /// <summary>
        /// ID 删除一个用户
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<bool> DeleteUser(int id);

        /// <summary>
        /// 获取所有用户信息 非删除
        /// </summary>
        /// <returns></returns>
        Task<List<SysUser>> GetALLUser();

        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="loginViewModel"></param>
        /// <returns></returns>
        Task<LoginResultModel> Login(LoginViewModel loginViewModel);
        /// <summary>
        /// 更新用户的用户名和密码
        /// </summary>
        /// <param name="UserId"></param>
        /// <param name="UserName"></param>
        /// <param name="Password"></param>
        /// <returns></returns>
        Task<bool> UpdateUser(int UserId, string UserName, string Password);
        Task<bool> UpdateUserAdminRole(int userid, bool flag);


        Task<List<SysUser>> GetAllUserData();
    }
}
