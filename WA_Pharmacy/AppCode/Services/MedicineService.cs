using AutoMapper;
using WA_Pharmacy.AppCode.Contracts.IService;
using WA_Pharmacy.AppCode.Repository;
using WA_Pharmacy.DTOs.Medicine;
using WA_Pharmacy.EFCore.Entities;
using WA_Pharmacy.EFCore.UnitOfWork;

namespace WA_Pharmacy.AppCode.Services
{
    public class MedicineService : IMedicineService
    {

        private readonly IGenericRepository<Medicine, int> _medicineRepo;
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public MedicineService(IGenericRepository<Medicine, int> medicineRepo, IUnitOfWork uow, IMapper mapper)
        {
            _medicineRepo = medicineRepo;
            _uow = uow;
            _mapper = mapper;
        }

        // دریافت لیست خام (همه)
        public async Task<List<Medicine>> GetAllMedicinesAsync()
        {
            return await _medicineRepo.GetAllAsync();
        }

        public async Task<List<Medicine>> GetSomeMedicinesAsync(int count)
        {
            return await _medicineRepo.GetSomeAsync(count);
        }

        public async Task<Medicine> GetMedicineByIdAsync(int id)
        {
            return await _medicineRepo.GetByIdAsync(id);
        }

        public async Task<List<MedicineListDto>> GetMedicinesForListAsync()
        {
            return await _medicineRepo.GetProjectedAsync<MedicineListDto>();
        }

        public async Task<MedicineEditDto> GetMedicineForEditAsync(int id)
        {
            return (await _medicineRepo.GetProjectedAsync<MedicineEditDto>(m => m.Id == id)).FirstOrDefault();
        }

        public async Task AddMedicineAsync(MedicineEditDto dto)
        {
            var medicine = _mapper.Map<Medicine>(dto);

            await _medicineRepo.AddAsync(medicine);
            await _uow.SaveChangesAsync();
        }

        public async Task UpdateMedicineAsync(MedicineEditDto dto)
        {
            var existingMedicine = await _medicineRepo.GetByIdAsync(dto.Id);
            if (existingMedicine == null) return;

            _mapper.Map(dto, existingMedicine);

            await _uow.SaveChangesAsync();
        }

        public async Task DeleteMedicineAsync(int id)
        {
            await _medicineRepo.DeleteAsync(id);
            await _uow.SaveChangesAsync();
        }

    }
}
