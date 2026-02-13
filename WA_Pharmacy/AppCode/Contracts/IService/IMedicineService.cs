using WA_Pharmacy.DTOs.Medicine;
using WA_Pharmacy.EFCore.Entities;

namespace WA_Pharmacy.AppCode.Contracts.IService
{
    public interface IMedicineService
    {
        Task<List<Medicine>> GetAllMedicinesAsync();
        Task<List<Medicine>> GetSomeMedicinesAsync(int count);
        Task<Medicine> GetMedicineByIdAsync(int id);

        Task<List<MedicineListDto>> GetMedicinesForListAsync();
        Task<MedicineEditDto> GetMedicineForEditAsync(int id);

        Task AddMedicineAsync(MedicineEditDto dto);
        Task UpdateMedicineAsync(MedicineEditDto dto);
        Task DeleteMedicineAsync(int id);

    }
}
