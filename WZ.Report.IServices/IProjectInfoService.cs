using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WZ.Report.Application.ProjectDTO;
using WZ.Report.Application.WriteProjectDto;
using WZ.Report.Model;

namespace WZ.Report.IServices
{
   public interface IProjectInfoService: Base.IBaseServices<ProjectInfo>
    {
        /// <summary>
        /// 获得通用的表格
        /// </summary>
        /// <param name="Role"></param>
        /// <returns></returns>
        Task<List<ProjectDto>> GetProjects(int Role);

        /// <summary>
        /// 获得表格四 表格五的数据
        /// </summary>
        /// <param name="Role"></param>
        /// <returns></returns>
        Task<object> GetOtherProjects(int Role);

        /// <summary>
        /// 数据写入数据库
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<bool> WriteTableData(WriteModel model);

        /// <summary>
        /// 用户 是否能填写 是否已经填写过
        /// </summary>
        /// <param name="UserId"></param>
        /// <param name="Mounth"></param>
        /// <param name="Year"></param>
        /// <returns></returns>
        Task<bool> IsEnableMouth(int UserId,int Mounth,int Year);

        /// <summary>
        /// ID查询 用户的填写表格情况 无分页
        /// </summary>
        /// <param name="UserId"></param>
        /// <returns></returns>
        Task<QueryDataModel> GetDataModelinfo(int UserId);
    }
}
