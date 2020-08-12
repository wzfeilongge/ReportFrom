using AutoMapper;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WZ.Report.Application.InfoViewModel;
using WZ.Report.Application.InfoViewModel.ProjectMinModel;
using WZ.Report.Application.ProjectDTO;
using WZ.Report.Common.Attribute;
using WZ.Report.IRepository;
using WZ.Report.IServices;
using WZ.Report.Model;

namespace WZ.Report.Services
{
    public class FillFormService : Base.BaseServices<FillForm>, IFillFormService
    {
        /// <summary>
        /// 表五
        /// </summary>
        private readonly IConversationService _conversationService;

        private readonly IFillFormRepository _FillFormRepository;

        private readonly ILogger<FillFormService> _logger;

        private readonly IMapper _Mapper;

        /// <summary>
        /// 表四
        /// </summary>
        private readonly IQuestionsService _questionsService;
        public FillFormService(IFillFormRepository fillFormRepository, IMapper Mapper

            , IQuestionsService questionsService
            , IConversationService conversationService
            , ILogger<FillFormService> logger
            )
        {
            _FillFormRepository = fillFormRepository;
            base.BaseDal = fillFormRepository;
            _questionsService = questionsService;
            _conversationService = conversationService;
            _logger = logger;
            _Mapper = Mapper;
        }

        [UseTran]
        public async Task<bool> DeleteForm(int AdminID, int DelUserId, int Year, int Mounth)
        {
            var deldata = await base.BaseDal.GetModelAsync(x => x.UserId == DelUserId && x.Year == Year && x.Mounth == Mounth && x.IsDeleted == false);
            _logger.LogWarning($"管理员ID：{AdminID} 删除 用户ID {DelUserId} 的{Year}年 {Mounth}月表格数据");
            deldata.IsDeleted = true;
            var tableoneinfo = await base.BaseDal.Modify(deldata);
            if (tableoneinfo < 0)
            {
                throw new Exception("删除失败，请联系管理员");
            }
            var tbfive = await _conversationService.Query(x => x.CreateId == DelUserId && x.Year == Year && x.Mounth == Mounth && x.IsDeleted == false);
            var tbfour = await _questionsService.Query(x => x.CreateId == DelUserId && x.Year == Year && x.Mounth == Mounth && x.IsDeleted == false);
            tbfive.ForEach(x => x.IsDeleted = true);
            tbfour.ForEach(x => x.IsDeleted = true);
            var tablefourinfo = await _questionsService.ModifyList(tbfour);
            var tablefiveinfo = await _conversationService.ModifyList(tbfive);
            if (tablefourinfo <= 0 || tablefiveinfo <= 0)
            {
                _logger.LogError($"删除常规表格{deldata}条，表格4 =>{tbfour}条,表格5 =>{tbfive}条 失败");
                throw new Exception("删除失败，请联系管理员");
            }
            _logger.LogWarning($"删除常规表格{deldata}条，表格4 =>{tbfour}条,表格5 =>{tbfive}条 成功");
            return true;
        }

        public async Task<List<FillformDto>> GetFillformDtos(int pageindex, int pagesize, int UserId)
        {
            var data = await BaseDal.GetPagedList(pageindex, pagesize, x => x.UserId == UserId && x.IsDeleted == false, x => x.Year);
            List<FillformDto> dtos = new List<FillformDto>();
            foreach (var item in data)
            {
                var models = new FillformDto
                {
                    Mounth = item.Mounth,
                    Year = item.Year,
                    DutyPeople = item.DutyPeople,
                    TableData = _Mapper.Map<List<ProjectMinDto>>(JsonConvert.DeserializeObject<List<ProjectDto>>(item.TableData)),
                    TableFive = new List<ProjectMinFiveDto>(),
                    TableFour = new List<ProjectMinFourDto>()
                };
                if (item.TableFour != string.Empty)
                {
                    models.TableFour = _Mapper.Map<List<ProjectMinFourDto>>(JsonConvert.DeserializeObject<List<ProjectFourDto>>(item.TableFour));
                }
                else
                {
                    models.TableFour = new List<ProjectMinFourDto>();
                }
                if (item.TableFive != string.Empty)
                {
                    models.TableFive = _Mapper.Map<List<ProjectMinFiveDto>>(JsonConvert.DeserializeObject<List<ProjectFiveDto>>(item.TableFive));
                }
                else
                {
                    models.TableFive = new List<ProjectMinFiveDto>();
                }

                if (models.TableFive==null|| models.TableFive.Count==0)
                {
                    models.TableFive = new List<ProjectMinFiveDto>();
                }
                if (models.TableFour == null || models.TableFour.Count == 0)
                {
                    models.TableFour = new List<ProjectMinFourDto>();
                }




                dtos.Add(models);
            }
            return dtos;
        }
    }
}
