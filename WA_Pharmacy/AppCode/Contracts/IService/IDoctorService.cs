using WA_Pharmacy.DTOs.Doctor;
using WA_Pharmacy.EFCore.Entities;

namespace WA_Pharmacy.AppCode.Contracts.IService
{
    public interface IDoctorService
    {
        Task<List<Doctor>> GetAllDoctorsAsync();
        Task<List<Doctor>> GetSomeDoctorsAsync(int count);
        Task<Doctor> GetDoctorByIdAsync(int id);

        Task<List<DoctorListDto>> GetDoctorsForListAsync();
        Task<DoctorEditDto> GetDoctorForEditAsync(int id);

        Task AddDoctorAsync(DoctorEditDto doctorDto);
        Task UpdateDoctorAsync(DoctorEditDto doctorDto);
        Task DeleteDoctorAsync(int id);
    }
}
