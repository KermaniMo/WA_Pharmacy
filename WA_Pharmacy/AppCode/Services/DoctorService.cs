using AutoMapper;
using WA_Pharmacy.AppCode.Contracts.IService;
using WA_Pharmacy.AppCode.Repository;
using WA_Pharmacy.DTOs.Doctor;
using WA_Pharmacy.EFCore.Entities;
using WA_Pharmacy.EFCore.UnitOfWork;

namespace WA_Pharmacy.AppCode.Services
{
    public class DoctorService : IDoctorService
    {
        private readonly IGenericRepository<Doctor, int> _doctorRepo;
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public DoctorService(IGenericRepository<Doctor, int> doctorRepo, IUnitOfWork uow, IMapper mapper)
        {
            _doctorRepo = doctorRepo;
            _uow = uow;
            _mapper = mapper;
        }

        public async Task<List<Doctor>> GetAllDoctorsAsync()
        {
            return await _doctorRepo.GetAllAsync();
        }

        public async Task<List<Doctor>> GetSomeDoctorsAsync(int count)
        {
            return await _doctorRepo.GetSomeAsync(count);
        }

        public async Task<Doctor> GetDoctorByIdAsync(int id)
        {
            return await _doctorRepo.GetByIdAsync(id);
        }

        public async Task<List<DoctorListDto>> GetDoctorsForListAsync()
        {
            return await _doctorRepo.GetProjectedAsync<DoctorListDto>();
        }

        public async Task<DoctorEditDto> GetDoctorForEditAsync(int id)
        {
            return (await _doctorRepo.GetProjectedAsync<DoctorEditDto>(d => d.Id == id)).FirstOrDefault();
        }

        public async Task AddDoctorAsync(DoctorEditDto doctorDto)
        {
            var doctor = _mapper.Map<Doctor>(doctorDto);

            // --- تولید کد 8 رقمی یونیک (بهینه) ---
            string generatedCode;
            do
            {
                generatedCode = new Random().Next(10000000, 99999999).ToString();
            }
            while (await _doctorRepo.ExistsAsync(d => d.DoctorNumber == generatedCode));

            doctor.DoctorNumber = generatedCode;
            // -------------------------------------

            doctor.RegisterDate = DateTime.Now;

            await _doctorRepo.AddAsync(doctor);
            await _uow.SaveChangesAsync();
        }

        public async Task UpdateDoctorAsync(DoctorEditDto doctorDto)
        {
            var existingDoctor = await _doctorRepo.GetByIdAsync(doctorDto.Id);
            if (existingDoctor == null) return;

            _mapper.Map(doctorDto, existingDoctor);
            await _uow.SaveChangesAsync();
        }

        public async Task DeleteDoctorAsync(int id)
        {
            await _doctorRepo.DeleteAsync(id);
            await _uow.SaveChangesAsync();
        }
    }
}
