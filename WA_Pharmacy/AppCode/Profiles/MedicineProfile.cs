using AutoMapper;
using WA_Pharmacy.DTOs.Medicine;
using WA_Pharmacy.EFCore.Entities;

namespace WA_Pharmacy.AppCode.Profiles
{
    public class MedicineProfile : Profile
    {
        public MedicineProfile()
        {
            CreateMap<Medicine, MedicineListDto>()
                .ForMember(dest => dest.IsInsurance, opt => opt.MapFrom(src => src.IsInsurance ? "بله" : "خیر"));

            CreateMap<Medicine, MedicineEditDto>();

            CreateMap<MedicineEditDto, Medicine>()
                .ForMember(dest => dest.Id, opt => opt.Ignore()); // آیدی رو مپ نکنیم بهتره
        }

    }
}
