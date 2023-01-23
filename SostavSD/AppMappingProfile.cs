using AutoMapper;
using SostavSD.Data;
using SostavSD.Interfaces;

namespace SostavSD
{
    public class AppMappingProfile:Profile
    {
        public AppMappingProfile()
        {
            CreateMap<IContractService, SostavSDContext>();
        }
    }
}
