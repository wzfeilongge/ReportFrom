using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WZ.Report.Application.InfoViewModel;
using WZ.Report.Model;

namespace WZ.Report.IServices
{
   public interface IFillFormService :Base.IBaseServices<FillForm>
    {

        /// <summary>
        /// 查询用户实际表格填写情况
        /// </summary>
        /// <param name="pageindex"></param>
        /// <param name="pagesize"></param>
        /// <param name="UserId"></param>
        /// <returns></returns>
        Task<List<FillformDto>> GetFillformDtos(int pageindex, int pagesize,int UserId);

        /// <summary>
        /// 删除1个普通用户 指定年月的表格数据
        /// </summary>
        /// <param name="AdminID"></param>
        /// <param name="DelUserId"></param>
        /// <param name="Year"></param>
        /// <param name="Mounth"></param>
        /// <returns></returns>
        Task<bool> DeleteForm(int AdminID,int DelUserId,int Year,int Mounth);
    }
}
