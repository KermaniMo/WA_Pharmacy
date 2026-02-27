using AutoMapper;
using WA_Pharmacy.DTOs.Prescription;
using WA_Pharmacy.EFCore.Entities;

namespace WA_Pharmacy.AppCode.Profiles
{
    public class PrescriptionProfile : Profile
    {
        public PrescriptionProfile()
        {
            CreateMap<Prescription, PrescriptionDto>()
                .ForMember(dest => dest.CustomerFullName, opt => opt.MapFrom(src => src.Customer != null ? $"{src.Customer.Name} {src.Customer.LastName}" : null))
                .ForMember(dest => dest.DoctorName, opt => opt.MapFrom(src => src.Doctor != null ? $"{src.Doctor.Name} {src.Doctor.LastName}" : null))
                .ForMember(dest => dest.InsuranceNumber, opt => opt.MapFrom(src => src.Insured != null ? src.Insured.InsuranceNumber : null));

            CreateMap<PrescriptionDetail, PrescriptionDetailDto>()
                .ForMember(dest => dest.MedicineName, opt => opt.MapFrom(src => src.Medicine != null ? src.Medicine.MedicineName : null));

            // Mapping for Input (Create)
            CreateMap<PrescriptionCreateDto, Prescription>()
                .ForMember(dest => dest.MedicineList, opt => opt.MapFrom(src => src.Items));

            CreateMap<PrescriptionItemDto, PrescriptionDetail>();

            // Mapping for Edit (Output)
            CreateMap<Prescription, PrescriptionEditDto>()
                .ForMember(dest => dest.Items, opt => opt.MapFrom(src => src.MedicineList))
                .ForMember(dest => dest.CustomerFullName, opt => opt.MapFrom(src => src.Customer != null ? $"{src.Customer.Name} {src.Customer.LastName}" : null))
                .ForMember(dest => dest.DoctorName, opt => opt.MapFrom(src => src.Doctor != null ? $"{src.Doctor.Name} {src.Doctor.LastName}" : null))
                .ForMember(dest => dest.InsuranceNumber, opt => opt.MapFrom(src => src.Insured != null ? src.Insured.InsuranceNumber : null));

            CreateMap<PrescriptionDetail, PrescriptionItemDto>();
        }
    }
}
