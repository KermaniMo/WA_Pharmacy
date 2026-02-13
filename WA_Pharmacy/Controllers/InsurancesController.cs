using Microsoft.AspNetCore.Mvc;
using WA_Pharmacy.AppCode.Contracts.IService;
using WA_Pharmacy.DTOs.Insurance;

namespace WA_Pharmacy.Controllers
{
    public class InsurancesController : Controller
    {
        private readonly IInsuranceService _insuranceService;

        // تزریق سرویس به جای دیتابیس
        public InsurancesController(IInsuranceService insuranceService)
        {
            _insuranceService = insuranceService;
        }

        // GET: Insurances
        public async Task<IActionResult> Index()
        {
            // دریافت لیست برای نمایش در جدول
            var model = await _insuranceService.GetInsurancesForListAsync();
            return View(model);
        }

        // GET: Insurances/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null) return NotFound();

            var dto = await _insuranceService.GetInsuranceForEditAsync(id.Value);
            if (dto == null) return NotFound();

            return View(dto);
        }

        // GET: Insurances/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Insurances/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(InsuranceEditDto insuranceDto)
        {
            if (ModelState.IsValid)
            {
                await _insuranceService.AddInsuranceAsync(insuranceDto);
                return RedirectToAction(nameof(Index));
            }
            return View(insuranceDto);
        }

        // GET: Insurances/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null) return NotFound();

            var dto = await _insuranceService.GetInsuranceForEditAsync(id.Value);
            if (dto == null) return NotFound();

            return View(dto);
        }

        // POST: Insurances/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, InsuranceEditDto insuranceDto)
        {
            if (id != insuranceDto.Id) return NotFound();

            if (ModelState.IsValid)
            {
                await _insuranceService.UpdateInsuranceAsync(insuranceDto);
                return RedirectToAction(nameof(Index));
            }
            return View(insuranceDto);
        }

        // GET: Insurances/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null) return NotFound();

            var dto = await _insuranceService.GetInsuranceForEditAsync(id.Value);
            if (dto == null) return NotFound();

            return View(dto);
        }

        // POST: Insurances/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            await _insuranceService.DeleteInsuranceAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}