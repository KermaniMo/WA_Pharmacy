using Microsoft.AspNetCore.Mvc;
using WA_Pharmacy.AppCode.Contracts.IService;
using WA_Pharmacy.DTOs.Medicine;

namespace WA_Pharmacy.Controllers
{
    public class MedicinesController : Controller
    {
        private readonly IMedicineService _medicineService;

        public MedicinesController(IMedicineService medicineService)
        {
            _medicineService = medicineService;
        }

        // GET: Medicines
        public async Task<IActionResult> Index()
        {
            var model = await _medicineService.GetMedicinesForListAsync();
            return View(model);
        }

        // GET: Medicines/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            // برای نمایش جزئیات فعلا از EditDto استفاده میکنیم که تمام فیلدها را دارد
            // (میتوانیم بعدا یک DTO جدا بسازیم اما فعلا نیازی نیست)
            var dto = await _medicineService.GetMedicineForEditAsync(id.Value);

            if (dto == null) return NotFound();

            return View(dto);
        }

        // GET: Medicines/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Medicines/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(MedicineEditDto medicineDto)
        {
            if (ModelState.IsValid)
            {
                await _medicineService.AddMedicineAsync(medicineDto);
                return RedirectToAction(nameof(Index));
            }
            return View(medicineDto);
        }

        // GET: Medicines/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var dto = await _medicineService.GetMedicineForEditAsync(id.Value);
            if (dto == null) return NotFound();

            return View(dto);
        }

        // POST: Medicines/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, MedicineEditDto medicineDto)
        {
            if (id != medicineDto.Id) return NotFound();

            if (ModelState.IsValid)
            {
                await _medicineService.UpdateMedicineAsync(medicineDto);
                return RedirectToAction(nameof(Index));
            }
            return View(medicineDto);
        }

        // GET: Medicines/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            // نمایش اطلاعات قبل از حذف
            var dto = await _medicineService.GetMedicineForEditAsync(id.Value);
            if (dto == null) return NotFound();

            return View(dto);
        }

        // POST: Medicines/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _medicineService.DeleteMedicineAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}