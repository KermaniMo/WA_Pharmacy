using WA_Pharmacy.App_Const;
using WA_Pharmacy.AppCode.Contracts.IService;
using WA_Pharmacy.AppCode.Exceptions;
using WA_Pharmacy.AppCode.Repository;
using WA_Pharmacy.DTOs.Prescription;
using WA_Pharmacy.EFCore.Entities;
using WA_Pharmacy.EFCore.UnitOfWork;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace WA_Pharmacy.AppCode.Services
{
    public class PrescriptionService : IPrescriptionService
    {
        private readonly IGenericRepository<Prescription, long> _prescriptionRepo;
        private readonly IGenericRepository<Medicine, int> _medicineRepo;
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public PrescriptionService(
          IGenericRepository<Prescription, long> prescriptionRepo,
          IGenericRepository<Medicine, int> medicineRepo,
          IUnitOfWork uow,
          IMapper mapper)
        {
            _prescriptionRepo = prescriptionRepo;
            _medicineRepo = medicineRepo;
            _uow = uow;
            _mapper = mapper;
        }

        public async Task<Prescription> AddPrescriptionAsync(PrescriptionCreateDto dto)
        {
            // 1. تولید کد رهگیری (بیرون از حلقه و منطق اصلی)
            string trackingCode;
            do
            {
                trackingCode = new Random().Next(10000000, 99999999).ToString();
            }
            while (await _prescriptionRepo.ExistsAsync(p => p.TrackingCode == trackingCode));

            // 2. ساخت هدر نسخه (در حافظه رم)
            var prescription = new Prescription
            {
                TrackingCode = trackingCode,
                CustomerId = dto.CustomerId,
                DoctorId = dto.DoctorId,
                InsuredId = dto.InsuredId,
                MedicineList = new List<PrescriptionDetail>()
            };

            decimal totalPrice = 0;

            // 3. حلقه روی اقلام
            foreach (var item in dto.Items)
            {
                var medicine = await _medicineRepo.GetByIdAsync(item.MedicineId);

                if (medicine == null)
                    throw new InvalidOperationException($"دارو با شناسه {item.MedicineId} یافت نشد.");

                // چک کردن موجودی
                if (medicine.Stock < item.Quantity)
                {
                    // اینجا هنوز هیچی تو دیتابیس نرفته، پس نیازی به Rollback دستی نیست.
                    // فقط ارور میدیم و درخواست کنسل میشه.
                    throw new InsufficientStockException(medicine.MedicineName, item.Quantity, medicine.Stock);
                }

                // کسر موجودی (توی رم انجام میشه)
                medicine.Stock -= item.Quantity;

                // محاسبه قیمت (اسنپ‌شات)
                // نکته مهم: اینجا قیمت رو باید کامل از دیتابیس بخونیم
                var detail = new PrescriptionDetail
                {
                    MedicineId = item.MedicineId,
                    Quantity = item.Quantity,
                    OrginalPrice = medicine.Price,       // قیمت از دیتابیس
                    InsurancePrice = medicine.Price * ((100 - InsuranceConst.discountPercent) / 100)
                };

                prescription.MedicineList.Add(detail);

                // جمع زدن قیمت کل
                totalPrice += (medicine.Price * item.Quantity);
            }

            prescription.TotalPrice = totalPrice;

            // 4. ثبت نهایی (جادوی EF Core)
            // این دو خط پایین خودشون به صورت اتمیک اجرا میشن
            await _prescriptionRepo.AddAsync(prescription);
            await _uow.SaveChangesAsync(); // <--- اینجا یا همه چی ثبت میشه یا هیچی

            return prescription;
        }

        public async Task<PrescriptionDto> GetPrescriptionByIdAsync(long id)
        {
            var entity = await _prescriptionRepo.GetByIdAsync(id, include: query => 
                query.Include(p => p.Customer)
                     .Include(p => p.Doctor)
                     .Include(p => p.Insured)
                     .Include(p => p.MedicineList)
                        .ThenInclude(m => m.Medicine));
            
            return _mapper.Map<PrescriptionDto>(entity);
        }

        public async Task<List<PrescriptionDto>> GetAllPrescriptionsAsync()
        {
            var entities =  await _prescriptionRepo.GetAllAsync(include: query => 
                query.Include(p => p.Customer)
                     .Include(p => p.Doctor)
                     .Include(p => p.Insured)
                     .Include(p => p.MedicineList)
                        .ThenInclude(m => m.Medicine));

            return _mapper.Map<List<PrescriptionDto>>(entities);
        }

        public async Task<PrescriptionEditDto> GetPrescriptionForEditAsync(long id)
        {
            var entity = await _prescriptionRepo.GetByIdAsync(id, include: query =>
                query.Include(p => p.Customer)
                     .Include(p => p.Doctor)
                     .Include(p => p.Insured)
                     .Include(p => p.MedicineList)
                        .ThenInclude(d => d.Medicine));

            return _mapper.Map<PrescriptionEditDto>(entity);
        }

        public async Task UpdatePrescriptionAsync(PrescriptionEditDto dto)
        {
            // 1. لود نسخه فعلی به همراه اقلام دارویی
            var prescription = await _prescriptionRepo.GetByIdAsync(dto.Id, include: query =>
                query.Include(p => p.MedicineList));

            if (prescription == null)
                throw new InvalidOperationException($"نسخه با شناسه {dto.Id} یافت نشد.");

            // 2. بازگرداندن موجودی داروهای قبلی (Restore Stock)
            foreach (var oldDetail in prescription.MedicineList)
            {
                var medicine = await _medicineRepo.GetByIdAsync(oldDetail.MedicineId);
                if (medicine != null)
                {
                    medicine.Stock += oldDetail.Quantity;
                }
            }

            // 3. حذف اقلام قبلی
            prescription.MedicineList.Clear();

            // 4. پردازش آیتم‌های جدید
            decimal totalPrice = 0;

            foreach (var item in dto.Items)
            {
                var medicine = await _medicineRepo.GetByIdAsync(item.MedicineId);

                if (medicine == null)
                    throw new InvalidOperationException($"دارو با شناسه {item.MedicineId} یافت نشد.");

                // چک کردن موجودی
                if (medicine.Stock < item.Quantity)
                {
                    throw new InsufficientStockException(medicine.MedicineName, item.Quantity, medicine.Stock);
                }

                // کسر موجودی
                medicine.Stock -= item.Quantity;

                // اسنپ‌شات قیمت
                var detail = new PrescriptionDetail
                {
                    MedicineId = item.MedicineId,
                    Quantity = item.Quantity,
                    OrginalPrice = medicine.Price,
                    InsurancePrice = medicine.Price * ((100 - InsuranceConst.discountPercent) / 100)
                };

                prescription.MedicineList.Add(detail);
                totalPrice += (medicine.Price * item.Quantity);
            }

            // 5. آپدیت قیمت کل (هدر نسخه تغییر نمی‌کند)
            prescription.TotalPrice = totalPrice;

            // 6. ذخیره همه تغییرات به صورت اتمیک
            await _uow.SaveChangesAsync();
        }

        public async Task DeletePrescriptionAsync(long id)
        {
            // لود نسخه به همراه اقلام دارویی
            var prescription = await _prescriptionRepo.GetByIdAsync(id, include: query =>
                query.Include(p => p.MedicineList));

            if (prescription != null)
            {
                // بازگرداندن موجودی داروها
                foreach (var detail in prescription.MedicineList)
                {
                    var medicine = await _medicineRepo.GetByIdAsync(detail.MedicineId);
                    if (medicine != null)
                    {
                        medicine.Stock += detail.Quantity;
                    }
                }

                // حذف نسخه (به دلیل تنظیمات Cascade در EF Core، رکورد‌های PrescriptionDetail هم حذف می‌شوند)
                await _prescriptionRepo.DeleteAsync(prescription.Id);
                await _uow.SaveChangesAsync();
            }
        }
    }
}


