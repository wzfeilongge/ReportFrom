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

        public async Task<QueryDataModel> GetDataModelinfo(int UserId, int Year, int Mounth)
        {
            var data = await _ISysUserServices.GetModelAsync(x => x.Id == UserId && x.IsDelete == false);
            if (data == null) return new QueryDataModel();
            {
                QueryDataModel queryDataModel = new QueryDataModel
                {
                    Id = data.Id,
                    UserName = data.UserName,
                    Data = new List<Data>()
                };
                var formData = await _FillFormService.Query(x => x.UserId == data.Id && x.Year == Year && x.Mounth == Mounth);
                if (formData == null) return new QueryDataModel();
                foreach (var item in formData)
                {
                    var datas = new Data
                    {
                        DutyPeople = item.DutyPeople,
                        Mounth = item.Mounth,
                        Year = item.Year,
                        TableData =
                        _Mapper.Map<List<ProjectDto>>(JsonConvert.DeserializeObject<List<ProjectInfo>>(item.TableData)),
                        TableFour = new List<ProjectFourDto>(),
                        TableFive = new List<ProjectFiveDto>()
                    };

                    if (item.TableFive == "[]")
                    {
                        item.TableFive = string.Empty;
                    }
                    else
                    {
                        datas.TableFive =
                            _Mapper.Map<List<ProjectFiveDto>>(JsonConvert.DeserializeObject<Conversation>(item.TableFive));
                    }
                    if (item.TableFour == "[]")
                    {
                        item.TableFour = string.Empty;
                    }
                    else
                    {
                        datas.TableFour =
                            _Mapper.Map<List<ProjectFourDto>>(JsonConvert.DeserializeObject<Questions>(item.TableFour));
                    }

                    if (datas.TableFour == null || datas.TableFour.Count == 0)
                    {
                        datas.TableFour = new List<ProjectFourDto>();
                    }

                    if (datas.TableFive == null || datas.TableFive.Count == 0)
                    {
                        datas.TableFive = new List<ProjectFiveDto>();
                    }

                    queryDataModel.Data.Add(datas);

                }
                queryDataModel.Count = queryDataModel.Data.Count;
                return queryDataModel;
            }
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
            if (data == null) return false;
            {
                var formData = await _FillFormService.GetModelAsync(x => x.UserId == UserId && x.Role == (data.Role) && x.Mounth == Mounth && x.Year == Year && x.IsDeleted == false);
                if (formData == null) //该年该月还未填写表
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
            if (!flag) return false;
            var data = await _ISysUserServices.GetModelAsync(x => x.Id == model.UserId && x.Role == (model.Role) && x.IsDelete == false);
            if (data == null) return false;
            {
                foreach (var item in model.Tables.Where(item => item.RunState == string.Empty && item.DefaultData != null))
                {
                    item.RunState = item.DefaultData;
                }

                if (model.TableFive.Count < 1)
                {
                    model.TableFive = new List<ProjectFiveDto>();
                }
                if (model.TablesFour.Count < 1)
                {
                    model.TablesFour = new List<ProjectFourDto>();
                }
                model.TableFive.ForEach(x => x.CreateId = data.Id);

                model.TablesFour.ForEach(x =>
                {
                    x.CreateId = data.Id;
                    x.Year = model.Year;
                    x.Mounth = model.Mounth;
                });

                var trolley = data.Role.ObjToInt();
                if (trolley < 1 || trolley >= 4) return false;

                if (string.IsNullOrWhiteSpace(model.DutyPeople))
                {
                    model.DutyPeople = data.UserName;
                }

                var fillModel = new FillForm
                {
                    UserId = model.UserId,
                    Role = data.Role,
                    Mounth = model.Mounth,
                    Year = model.Year,
                    TableData = JsonConvert.SerializeObject(model.Tables),
                    TableFive = JsonConvert.SerializeObject(model.TableFive),
                    TableFour = JsonConvert.SerializeObject(model.TablesFour),
                    DutyPeople = model.DutyPeople,
                    IsDeleted = false
                };
                var form = await _FillFormService.Add(fillModel);
                _logger.LogInformation($" 用户名：{data.UserName} ID：{data.Id}  插入了1条数据");
                if (form != null)
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
                    var serachTable = await _RegisterInfoService.WriteOrUpdateUserinfo(model.UserId, trolley, model.Year, model.Mounth);
                    return serachTable > 0;
                }

                _logger.LogInformation($" 用户名：{data.UserName} ID：{data.Id}  翻车 插入通用表格失败");
                throw new Exception("创建失败，请联系管理员"); //AOP 回滚
            }
        }
    }
}
