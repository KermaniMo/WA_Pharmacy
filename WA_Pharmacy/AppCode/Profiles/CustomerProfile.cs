using AutoMapper;
using WA_Pharmacy.AppCode.PublicClass;
using WA_Pharmacy.DTOs.Customer;
using WA_Pharmacy.EFCore.Entities;

namespace WA_Pharmacy.AppCode.Profiles
{
    public class CustomerProfile : Profile
    {
        public CustomerProfile()
        {
            CreateMap<Customer, CustomerListDto>()
                .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.Name + " " + src.LastName))
                .ForMember(dest => dest.RegisterDate, opt => opt.MapFrom(src => DateFunctions.DateToShamsi(src.RegisterDate)))
                .ForMember(dest => dest.Birthday, opt => opt.MapFrom(src => DateFunctions.DateToShamsi(src.Birthday.Value)))
                .ForMember(dest => dest.Sex, opt => opt.MapFrom(src => src.Sex ? "مرد" : "زن"));

            CreateMap<Customer, CustomerEditDto>()
                .ForMember(dest => dest.Birthday, opt => opt.MapFrom(src => DateFunctions.DateToShamsi(src.Birthday.Value)));


            CreateMap<CustomerEditDto, Customer>()
                .ForMember(dest => dest.Birthday, opt => opt.MapFrom(src => DateFunctions.ToMiladi(src.Birthday)));

        }

    }
}
