using AutoMapper;
using SostavSD.Data;
using SostavSD.Interfaces;
using SostavSD.Models;
using SostavSD.Entity;


namespace SostavSD
{
    public class AppMappingProfile:Profile
    {
        public AppMappingProfile()
        {
            CreateMap<Contract, ContractModel>().ReverseMap().PreserveReferences();

        }
    }
}
