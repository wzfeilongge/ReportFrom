using AutoMapper;
using Castle.Core.Logging;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WZ.Report.Application.ProjectDTO;
using WZ.Report.Application.WriteProjectDto;
using WZ.Report.Common;
using WZ.Report.Common.Attribute;
using WZ.Report.IRepository;
using WZ.Report.IServices;
using WZ.Report.Model;

namespace WZ.Report.Services
{
    public class ProjectInfoService : Base.BaseServices<ProjectInfo>, IProjectInfoService
    {
        /// <summary>
        /// 附件五
        /// </summary>
        private readonly IConversationService _ConversationService;

        private readonly IFillFormService _FillFormService;
        private readonly ISysUserService _ISysUserServices;
        private readonly ILogger<ProjectInfoService> _logger;
        private readonly IMapper _Mapper;
        private readonly IProjectInfoRepository _ProjectInfoRepository;
        /// <summary>
        /// 附件四
        /// </summary>
        private readonly IQuestionsService _QuestionsServices;
        private readonly IRegisterInfoService _RegisterInfoService;

      //  private readonly IUnitOfWork unitOfWork;
        public ProjectInfoService(IProjectInfoRepository ProjectInfoRepository, IMapper mapper
            , ISysUserService ISysUserServices, IFillFormService fillFormService
            , IQuestionsService QuestionsServices, IConversationService ConversationService
            , ILogger<ProjectInfoService> logger, IRegisterInfoService RegisterInfoService
            )
        {
            _ProjectInfoRepository = ProjectInfoRepository;
            base.BaseDal = ProjectInfoRepository;
            _ISysUserServices = ISysUserServices;
            _FillFormService = fillFormService;
            _QuestionsServices = QuestionsServices;
            _ConversationService = ConversationService;
            _RegisterInfoService = RegisterInfoService;
            _logger = logger;
            _Mapper = mapper;
        }

        public async Task<QueryDataModel> GetDataModelinfo(int UserId)
        {
            var data = await _ISysUserServices.GetModelAsync(x => x.Id == UserId && x.IsDelete == false);
            if (data != null)
            {
                QueryDataModel queryDataModel = new QueryDataModel
                {
                    Id = data.Id,
                    UserName = data.UserName,
                    Data = new List<Data>()
                };
                var FormData = await _FillFormService.Query(x => x.UserId == data.Id);
                if (FormData != null)
                {
                    foreach (var item in FormData)
                    {
                        queryDataModel.Data.Add(
                         new Data
                         {
                             Mounth = item.Mounth,
                             Year = item.Year,
                             TableData = _Mapper.Map<List<ProjectDto>>(JsonConvert.DeserializeObject<List<ProjectInfo>>(item.TableData)),
                             TableFive = _Mapper.Map<ProjectFiveDto>(JsonConvert.DeserializeObject<Conversation>(item.TableFive)),
                             TableFour = _Mapper.Map<ProjectFourDto>(JsonConvert.DeserializeObject<Questions>(item.TableFour))
                         });
                    }
                    queryDataModel.Count = queryDataModel.Data.Count;
                    return queryDataModel;
                }
            }
            return new QueryDataModel();
        }
        //1 部门负责人  2 党组书记 3 班子成员 0管理员
        public async Task<object> GetOtherProjects(int Role)
        {
            if (Role >= 1 && Role < 4)
            {
                return await Task.FromResult(
                   new
                   {
                       four = new ProjectFourDto(),
                       five = new ProjectFiveDto()
                   });
            }
            return null;
        }

        public async Task<List<ProjectDto>> GetProjects(int Role)
        {
            return _Mapper.Map<List<ProjectDto>>((await base.BaseDal.Query(x => x.Role == Role)).OrderBy(x => x.Sort).ToList());
        }

        public async Task<bool> IsEnableMouth(int UserId, int Mounth, int Year = 2020)
        {
            var data = await _ISysUserServices.GetModelAsync(x => x.Id == UserId && x.IsDelete == false);
            if (data != null)
            {
                var FormData = await _FillFormService.GetModelAsync(x => x.UserId == UserId && x.Role == data.Role && x.Mounth == Mounth && x.Year == Year && x.IsDeleted == false);
                if (FormData == null) //该年该月还未填写表
                {
                    return true;
                }
            }
            return false;
        }

        [UseTran]
        public async Task<bool> WriteTableData(WriteModel model)
        {
            var flag = await IsEnableMouth(model.UserId, model.Mounth, model.Year);
            if (flag)
            {
                var data = await _ISysUserServices.GetModelAsync(x => x.Id == model.UserId && x.Role == model.Role && x.IsDelete == false);
                if (data != null)
                {
                    foreach (var item in model.Tables)
                    {
                        if (item.RunState == string.Empty && item.DefaultData != null)
                        {
                            item.RunState = item.DefaultData;
                        }
                    }
                    model.TableFive.ForEach(x => x.CreateId = data.Id);

                    model.TablesFour.ForEach(x =>
                    {
                        x.CreateId = data.Id;
                        x.Year = model.Year;
                        x.Mounth = model.Mounth;
                    });
                    if (data.Role >= 1 && data.Role < 4)
                    {
                        var FillModel = new FillForm
                        {
                            UserId = model.UserId,
                            Role = data.Role,
                            Mounth = model.Mounth,
                            Year = model.Year,
                            TableData = JsonConvert.SerializeObject(model.Tables),
                            TableFive = JsonConvert.SerializeObject(model.TableFive),
                            TableFour = JsonConvert.SerializeObject(model.TablesFour),
                            IsDeleted = false
                        };
                        var Form = await _FillFormService.Add(FillModel);
                        _logger.LogInformation($" 用户名：{data.UserName} ID：{data.Id}  插入了1条数据");
                        if (Form != null)
                        {
                            if (model.TablesFour.Count != 0)
                            {
                                var fourtable = _Mapper.Map<List<Questions>>(model.TablesFour);
                                var four = await _QuestionsServices.AddRangeModels(fourtable);
                                _logger.LogInformation($" 用户名：{data.UserName} ID：{data.Id}  应插入表格4=>{fourtable.Count}条数据 实际插入{four}条数据");
                            }
                            if (model.TableFive.Count != 0)
                            {
                                var fivetable = _Mapper.Map<List<Conversation>>(model.TableFive);
                                var five = await _ConversationService.AddRangeModels(fivetable);
                                _logger.LogInformation($" 用户名：{data.UserName} ID：{data.Id}  应插入表格5=>{fivetable.Count}条数据 实际插入{five}条数据");
                            }
                            var SerachTable=  await _RegisterInfoService.WriteOrUpdateUserinfo(model.UserId,model.Role,model.Year,model.Mounth);
                            if (SerachTable > 0)
                            {
                                return true;
                            }
                            else {
                                return false;
                            }                
                        }
                        else
                        {
                            _logger.LogInformation($" 用户名：{data.UserName} ID：{data.Id}  翻车 插入通用表格失败");
                            throw new Exception("创建失败，请联系管理员"); //AOP 回滚
                        }
                    }
                }
            }
            return false;
        }
    }
}
