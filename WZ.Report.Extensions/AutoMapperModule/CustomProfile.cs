using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using WZ.Report.Application.AdminTableDto;
using WZ.Report.Application.InfoViewModel;
using WZ.Report.Application.InfoViewModel.ProjectMinModel;
using WZ.Report.Application.ProjectDTO;
using WZ.Report.Model;

namespace WZ.Report.Extensions.AutoMapperModule
{
   public class CustomProfile: Profile
    {
        public CustomProfile()
        {
            CreateMap<ProjectDto, ProjectInfo>();

            CreateMap<ProjectInfo, ProjectDto>();

            CreateMap<Questions, ProjectFourDto>();

            CreateMap<ProjectFourDto, Questions>();

            CreateMap<Conversation, ProjectFiveDto>();

            CreateMap<ProjectFiveDto, Conversation>();

            CreateMap<FillForm, FillformDto>();

            CreateMap<FillformDto, FillForm>();


            CreateMap<ProjectMinDto, ProjectDto>();

            CreateMap<ProjectDto, ProjectMinDto>();

            CreateMap<ProjectMinFiveDto, ProjectFiveDto>();

            CreateMap<ProjectFiveDto, ProjectMinFiveDto>();

            CreateMap<ProjectMinFourDto, ProjectFourDto>();

            CreateMap<ProjectFourDto, ProjectMinFourDto>();


            CreateMap<CurrencyResultModel, RegisterInfo>();

            CreateMap<RegisterInfo, CurrencyResultModel>();

            CreateMap<CurrencyDangResultModel, RegisterInfo>();

            CreateMap<RegisterInfo, CurrencyDangResultModel>();















        }
    }
}
