using Microsoft.EntityFrameworkCore;
using WA_Pharmacy.AppCode.Contracts.IService;
using WA_Pharmacy.AppCode.Exceptions;
using WA_Pharmacy.AppCode.Repository;
using WA_Pharmacy.DTOs.Prescription;
using WA_Pharmacy.EFCore.Entities;
using WA_Pharmacy.EFCore.UnitOfWork;

namespace WA_Pharmacy.AppCode.Services
{
    public class PrescriptionService : IPrescriptionService
    {
        private readonly IGenericRepository<Prescription, long> _prescriptionRepo;
        private readonly IGenericRepository<Medicine, int> _medicineRepo;
        private readonly IUnitOfWork _uow;

        public PrescriptionService(
            IGenericRepository<Prescription, long> prescriptionRepo,
            IGenericRepository<Medicine, int> medicineRepo,
            IUnitOfWork uow)
        {
            _prescriptionRepo = prescriptionRepo;
            _medicineRepo = medicineRepo;
            _uow = uow;
        }

        public async Task<Prescription> AddPrescriptionAsync(PrescriptionCreateDto dto)
        {
            // شروع تراکنش
            await using var transaction = await _uow.BeginTransactionAsync();

            try
            {
                // 1. تولید کد پیگیری 8 رقمی یونیک
                string trackingCode;
                do
                {
                    trackingCode = new Random().Next(10000000, 99999999).ToString();
                }
                while (await _prescriptionRepo.ExistsAsync(p => p.TrackingCode == trackingCode));

                // 2. ایجاد هدر نسخه
                var prescription = new Prescription
                {
                    TrackingCode = trackingCode,
                    CustomerId = dto.CustomerId,
                    DoctorId = dto.DoctorId,
                    MedicineList = new List<PrescriptionDetail>()
                };

                decimal totalPrice = 0;

                // 3. پردازش هر آیتم
                foreach (var item in dto.Items)
                {
                    // دریافت دارو از دیتابیس
                    var medicine = await _medicineRepo.GetByIdAsync(item.MedicineId);
                    if (medicine == null)
                    {
                        throw new InvalidOperationException($"داروی با شناسه {item.MedicineId} یافت نشد.");
                    }

                    // بررسی موجودی
                    if (medicine.Stock < item.Quantity)
                    {
                        throw new InsufficientStockException(
                            medicine.MedicineName,
                            item.Quantity,
                            medicine.Stock);
                    }

                    // کاهش موجودی
                    medicine.Stock -= item.Quantity;

                    // ایجاد جزئیات نسخه با قیمت از دیتابیس (Price Snapshot)
                    var detail = new PrescriptionDetail
                    {
                        MedicineId = item.MedicineId,
                        Quantity = item.Quantity,
                        OrginalPrice = medicine.Price, // قیمت اصلی از دیتابیس
                        InsurancePrice = item.InsurancePrice // قیمت بیمه شده از کاربر
                    };

                    prescription.MedicineList.Add(detail);

                    // محاسبه قیمت کل (بر اساس قیمت اصلی ضرب در تعداد)
                    totalPrice += medicine.Price * item.Quantity;
                }

                // 4. ذخیره قیمت کل
                prescription.TotalPrice = totalPrice;

                // 5. ذخیره نسخه
                await _prescriptionRepo.AddAsync(prescription);
                await _uow.SaveChangesAsync();

                // 6. کامیت تراکنش
                await transaction.CommitAsync();

                return prescription;
            }
            catch
            {
                // در صورت خطا، رول‌بک تراکنش
                await transaction.RollbackAsync();
                throw;
            }
        }
    }
}
