using AutoMapper;
using SostavSD.Models;
using SostavSD.Entities;

namespace SostavSD
{
    public class AppMappingProfile: Profile
    {
        public AppMappingProfile()
        {
            CreateMap<Contract, ContractModel>().ReverseMap().PreserveReferences();
            CreateMap<Company, CompanyModel>().ReverseMap().PreserveReferences();
			CreateMap<UserSostav, UserSostavModel>().ReverseMap().PreserveReferences();
			CreateMap<BuildingZone, BuildingZoneModel>().ReverseMap().PreserveReferences();
			CreateMap<SourceOfFinacing, SourceOfFinacingModel>().ReverseMap().PreserveReferences();
			CreateMap<DesignStage, DesignStageModel>().ReverseMap().PreserveReferences();
			CreateMap<BuildingView, BuildingViewModel>().ReverseMap().PreserveReferences();
			CreateMap<Status, StatusModel>().ReverseMap().PreserveReferences();
			CreateMap<Project, ProjectModel>().ReverseMap().PreserveReferences();
			CreateMap<Status, StatusModel>().ReverseMap().PreserveReferences();
			CreateMap<Drawing,DrawingModel>().ReverseMap().PreserveReferences();
		}
    }
}
