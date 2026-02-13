using AutoMapper;
using WA_Pharmacy.DTOs.Insurance;
using WA_Pharmacy.EFCore.Entities;

namespace WA_Pharmacy.AppCode.Profiles
{
    public class InsuranceProfile : Profile
    {

        public InsuranceProfile()
        {
            CreateMap<Insurance, InsuranceListDto>();
            CreateMap<Insurance, InsuranceEditDto>();

            CreateMap<InsuranceEditDto, Insurance>()
               .ForMember(dest => dest.Id, opt => opt.Ignore());
        }

    }
}
