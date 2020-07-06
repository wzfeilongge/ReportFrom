using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WZ.Report.Application.AdminTableDto;
using WZ.Report.Model;

namespace WZ.Report.IServices
{
    public interface IRegisterInfoService :Base.IBaseServices<RegisterInfo>
    {
        /// <summary>
        /// 写入查询报表数据
        /// </summary>
        /// <param name="UserId"></param>
        /// <param name="Role"></param>
        /// <param name="Year"></param>
        /// <param name="Mounth"></param>
        /// <returns></returns>
        Task<int> WriteOrUpdateUserinfo(int UserId,int Role,int Year,int Mounth);

        /// <summary>
        /// 分页查询报表数据 部门负责人 班子成员
        /// </summary>
        /// <param name="Role"></param>
        /// <param name="PageIndex"></param>
        /// <param name="Pagesize"></param>
        /// <returns></returns>
        Task<List<CurrencyResultModel>> GetModelinfo(int Year, int Role,int PageIndex,int Pagesize);

        /// <summary>
        /// 分页查询报表数据 党组书记
        /// </summary>
        /// <param name="PageIndex"></param>
        /// <param name="Pagesize"></param>
        /// <returns></returns>
        Task<List<CurrencyDangResultModel>> GetModelinfoDang(int Year,int PageIndex, int Pagesize);
    }
}
