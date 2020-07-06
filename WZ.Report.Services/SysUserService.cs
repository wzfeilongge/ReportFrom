﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

using WZ.Report.Application.DTO;
using WZ.Report.Application.WriteProjectDto;
using WZ.Report.Common;
using WZ.Report.Common.AuthHelper;
using WZ.Report.IRepository;
using WZ.Report.IServices;
using WZ.Report.Model;
using WZ.Report.Services.Base;

namespace WZ.Report.Services
{
   public class SysUserService: BaseServices<SysUser>,ISysUserService
    {
        private readonly ISysUserRepository sysUserRepository;

        public SysUserService(ISysUserRepository sysUserRepository)
        {
            this.sysUserRepository = sysUserRepository;
            base.BaseDal = sysUserRepository;
        }

        public  async Task<bool> CreateAdmin(string UserName, string Password)
        {
            var model = await base.BaseDal.GetModelAsync(x => x.UserName == UserName);

            if (model == null)
            {
                var result = await base.BaseDal.AddModel(new SysUser
                {
                    UserName = UserName,
                    Password = MD5Helper.MD5Encrypt32(Password),
                    Role = 0
                });

                if (result > 0)
                {
                    return true;
                }
            }

            return false;
        }

        public async Task<bool> CreateUser(string UserName, string Password, int Role)
        {
            var model = await base.BaseDal.GetModelAsync(x=>x.UserName==UserName);

            if (model==null)
            {

                if (Role==0||Role>4)
                {
                    return false;
                }
                var result=   await base.BaseDal.AddModel(new SysUser
                { 
                    UserName=UserName,
                    Password= MD5Helper.MD5Encrypt32(Password),
                    Role=Role
                });
                if (result>0)
                {
                    return true;
                }
          }
            return false;
        }

        public  async Task<List<SysUser>> GetALLUser()
        {
            return await sysUserRepository.Query(x => x.Id>0);
        }


        public async Task<LoginResultModel> Login(LoginViewModel loginViewModel)
        {
            loginViewModel.PassWord= MD5Helper.MD5Encrypt32(loginViewModel.PassWord);
            var model = await sysUserRepository.GetModelAsync(x=>x.UserName==loginViewModel.UserName&&x.Password==loginViewModel.PassWord);
            if (model!=null)
            {
                var accesstoken = JwtHelper.IssueJwt(new TokenModelJwt 
                { 
                    Role=model.Role.ToString(),
                    Uid=model.Id                                       
                });
                return new LoginResultModel
                {
                    RoleId=model.Role,
                    UserName=loginViewModel.UserName,
                    UserId=model.Id,
                    AccessToken=accesstoken
                };
            }
            return null;
        }
    }
}