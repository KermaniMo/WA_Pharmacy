using AutoMapper;
using System;
using WA_Pharmacy.AppCode.PublicClass;
using WA_Pharmacy.DTOs.Insured;
using WA_Pharmacy.EFCore.Entities;

namespace WA_Pharmacy.AppCode.Profiles
{
    public class InsuredProfile : Profile
    {
        public InsuredProfile()
        {
            CreateMap<Insured, InsuredListDto>()
                .ForMember(dest => dest.CustomerFullName, opt => opt.MapFrom(src => src.Customer.Name + " " + src.Customer.LastName))
                .ForMember(dest => dest.InsuranceCompanyName, opt => opt.MapFrom(src => src.Insurance.CompanyName))
                .ForMember(dest => dest.StartDate, opt => opt.MapFrom(src => DateFunctions.DateToShamsi(src.StartDate)))
                .ForMember(dest => dest.ExpireDate, opt => opt.MapFrom(src => DateFunctions.DateToShamsi(src.ExpireDate)));

            CreateMap<Insured, InsuredEditDto>()
                .ForMember(dest => dest.StartDate, opt => opt.MapFrom(src => DateFunctions.DateToShamsi(src.StartDate)))
                .ForMember(dest => dest.ExpireDate, opt => opt.MapFrom(src => DateFunctions.DateToShamsi(src.ExpireDate)));

            CreateMap<InsuredEditDto, Insured>()
               .ForMember(dest => dest.Id, opt => opt.Ignore())
               .ForMember(dest => dest.StartDate, opt => opt.MapFrom(src => DateFunctions.ToMiladi(src.StartDate) != null ? DateFunctions.ToMiladi(src.StartDate).Value.ToDateTime(TimeOnly.MinValue) : default(DateTime)))
               .ForMember(dest => dest.ExpireDate, opt => opt.MapFrom(src => DateFunctions.ToMiladi(src.ExpireDate) != null ? DateFunctions.ToMiladi(src.ExpireDate).Value.ToDateTime(TimeOnly.MinValue) : default(DateTime)));
        }
    }
}
