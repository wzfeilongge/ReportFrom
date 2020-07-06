using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WZ.Report.Application.AdminTableDto;
using WZ.Report.Common;
using WZ.Report.IRepository;
using WZ.Report.IServices;
using WZ.Report.Model;

namespace WZ.Report.Services
{
   public class RegisterInfoService :Base.BaseServices<RegisterInfo>, IRegisterInfoService
    {

        private readonly IRegisterInfoRepository _IRegisterInfoRepository;

        private readonly ISysUserService _sysUserService;

        private readonly IMapper _mapper;

        

        public RegisterInfoService(IRegisterInfoRepository registerInfoRepository, ISysUserService sysUserService, IMapper mapper)
        {
            _IRegisterInfoRepository = registerInfoRepository;
            base.BaseDal = registerInfoRepository;
            _sysUserService = sysUserService;
            _mapper = mapper;
        }

        public  async Task<List<CurrencyResultModel>> GetModelinfo(int Year,int Role, int PageIndex, int Pagesize)
        {
          return _mapper.Map<List<CurrencyResultModel>>(await _IRegisterInfoRepository.GetPagedList(PageIndex,Pagesize,x=>x.Role==Role&&x.Year==Year,x=>x.Id));    
        }

        public async Task<List<CurrencyDangResultModel>> GetModelinfoDang(int Year,int PageIndex, int Pagesize)
        {
            return _mapper.Map<List<CurrencyDangResultModel>>(await _IRegisterInfoRepository.GetPagedList(PageIndex, Pagesize, x => x.Role == 2&&x.Year==Year, x => x.Id));
        }

        public async  Task<int> WriteOrUpdateUserinfo(int UserId, int Role, int Year, int Mounth)
        {
           var model= await base.BaseDal.GetModelAsync(x => x.UserId == UserId && x.Role == Role && x.Year == Year);
            RegisterInfo registerInfo = new RegisterInfo
            {
                UserId = UserId,
                Role = Role,
                Year = Year
            };
            if (model!=null)
            {
              model=  ChangeMounth(model, Mounth,true);
              return await base.BaseDal.Modify(model);
            }

            var sysuser = await _sysUserService.GetModelAsync(x=>x.Id==UserId);

            if (registerInfo.Role==1)
            {
                registerInfo.Name = sysuser.UserName;
                registerInfo.PhoneNameDepartment = sysuser.Department;
            }
            if (registerInfo.Role == 2)
            {
                registerInfo.Name = sysuser.JobAddress;
                registerInfo.PhoneNameDepartment = sysuser.UserName;
                registerInfo.Undertaker = sysuser.Undertaker;
                registerInfo.Phone = sysuser.UndertakerPhone;
            }
            if (registerInfo.Role == 3)
            {
                registerInfo.Name = sysuser.UserName;
                registerInfo.PhoneNameDepartment = sysuser.UserPhone;
            }

            registerInfo = ChangeMounth(registerInfo, Mounth, true);
            return await BaseDal.AddModel(registerInfo);
        }

        private RegisterInfo ChangeMounth(RegisterInfo obj,int mounth,bool changetrue=true)
        {
            switch (mounth)
            {
                case 1:
                    obj.January = changetrue;
                    break;
                case 2:
                    obj.February = changetrue;
                    break;
                case 3:
                    obj.March = changetrue;
                    break;
                case 4:
                    obj.April = changetrue;
                    break;
                case 5:
                    obj.May = changetrue;
                    break;
                case 6:
                    obj.June = changetrue;
                    break;
                case 7:
                    obj.July = changetrue;
                    break;
                case 8:
                    obj.August = changetrue;
                    break;
                case 9:
                    obj.September = changetrue;
                    break;
                case 10:
                    obj.October = changetrue;
                    break;
                case 11:
                    obj.November = changetrue;
                    break;
                case 12:
                    obj.December = changetrue;
                    break;
            }
            return obj;
        }
    }
}
