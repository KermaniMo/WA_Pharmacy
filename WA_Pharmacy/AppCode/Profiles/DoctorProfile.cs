using AutoMapper;
using WA_Pharmacy.AppCode.PublicClass;
using WA_Pharmacy.DTOs.Doctor;
using WA_Pharmacy.EFCore.Entities;

namespace WA_Pharmacy.AppCode.Profiles
{
    public class DoctorProfile : Profile
    {
        public DoctorProfile()
        {
            CreateMap<Doctor, DoctorListDto>()
                .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.Name + " " + src.LastName))
                .ForMember(dest => dest.RegisterDate, opt => opt.MapFrom(src => DateFunctions.DateToShamsi(src.RegisterDate)))
                .ForMember(dest => dest.Birthday, opt => opt.MapFrom(src => DateFunctions.DateToShamsi(src.Birthday)))
                .ForMember(dest => dest.Sex, opt => opt.MapFrom(src => src.Sex ? "مرد" : "زن"));

            CreateMap<Doctor, DoctorEditDto>()
                .ForMember(dest => dest.Birthday, opt => opt.MapFrom(src => DateFunctions.DateToShamsi(src.Birthday)));

            CreateMap<DoctorEditDto, Doctor>()
                .ForMember(dest => dest.Birthday, opt => opt.MapFrom(src => DateFunctions.ToMiladi(src.Birthday) ?? default))
                .ForMember(dest => dest.RegisterDate, opt => opt.Ignore()) 
                .ForMember(dest => dest.DoctorNumber, opt => opt.Ignore()); 
        }
    }
}
