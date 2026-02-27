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

        Task<PrescriptionDto> GetPrescriptionByIdAsync(long id);
        Task<List<PrescriptionDto>> GetAllPrescriptionsAsync();

        /// <summary>
        /// دریافت اطلاعات نسخه جهت ویرایش
        /// </summary>
        Task<PrescriptionEditDto> GetPrescriptionForEditAsync(long id);

        /// <summary>
        /// ویرایش نسخه با بازگرداندن موجودی قبلی و اعمال مجدد
        /// </summary>
        Task UpdatePrescriptionAsync(PrescriptionEditDto dto);

        /// <summary>
        /// حذف یک نسخه و بازگرداندن موجودی داروها
        /// </summary>
        Task DeletePrescriptionAsync(long id);
    }
}
