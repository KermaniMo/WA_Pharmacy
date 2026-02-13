using WA_Pharmacy.DTOs.Prescription;
using WA_Pharmacy.EFCore.Entities;

namespace WA_Pharmacy.AppCode.Contracts.IService
{
    public interface IPrescriptionService
    {
        /// <summary>
        /// ثبت نسخه جدید با بررسی موجودی، کاهش استوک، و محاسبه قیمت کل
        /// </summary>
        Task<Prescription> AddPrescriptionAsync(PrescriptionCreateDto dto);
    }
}
