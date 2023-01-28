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

        }
    }
}
