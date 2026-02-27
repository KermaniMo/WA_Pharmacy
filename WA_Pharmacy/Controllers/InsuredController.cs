using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WA_Pharmacy.AppCode.Contracts.IService;
using WA_Pharmacy.DTOs.Insured;

namespace WA_Pharmacy.Controllers
{
    public class InsuredController : Controller
    {
        private readonly IInsuredService _insuredService;
        private readonly ICustomerService _customerService;
        private readonly IInsuranceService _insuranceService;

        public InsuredController(IInsuredService insuredService, ICustomerService customerService, IInsuranceService insuranceService)
        {
            _insuredService = insuredService;
            _customerService = customerService;
            _insuranceService = insuranceService;
        }

        public async Task<IActionResult> Index()
        {
            var list = await _insuredService.GetInsuredsForListAsync();
            return View(list);
        }

        private async Task PopulateDropdownsAsync(long? customerId = null, long? insuranceId = null)
        {
            var customers = await _customerService.GetCustomersForListAsync();
            var insurances = await _insuranceService.GetInsurancesForListAsync();

            ViewData["CustomerId"] = new SelectList(customers, "Id", "FullName", customerId);
            ViewData["InsuranceId"] = new SelectList(insurances, "Id", "CompanyName", insuranceId);
        }

        // GET: Insured/Create
        public async Task<IActionResult> Create()
        {
            await PopulateDropdownsAsync();
            return View();
        }

        // POST: Insured/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(InsuredEditDto model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _insuredService.AddInsuredAsync(model);
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, "خطایی در ثبت اطلاعات رخ داد.");
                }
            }

            await PopulateDropdownsAsync(model.CustomerId, model.InsuranceId);
            return View(model);
        }

        // GET: Insured/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null) return NotFound();

            var model = await _insuredService.GetInsuredForEditAsync(id.Value);
            if (model == null) return NotFound();

            await PopulateDropdownsAsync(model.CustomerId, model.InsuranceId);
            return View(model);
        }

        // POST: Insured/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, InsuredEditDto model)
        {
            if (id != model.Id) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    await _insuredService.UpdateInsuredAsync(model);
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, "خطایی در به‌روزرسانی اطلاعات رخ داد.");
                }
            }

            await PopulateDropdownsAsync(model.CustomerId, model.InsuranceId);
            return View(model);
        }


        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dto = await _insuredService.GetInsuredDetailsAsync(id.Value);

            if (dto == null)
            {
                return NotFound();
            }
            return View(dto);
        }

        // GET: Insured/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null) return NotFound();

            var model = await _insuredService.GetInsuredDetailsAsync(id.Value);
            if (model == null) return NotFound();

            return View(model);
        }

        // POST: Insured/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            await _insuredService.DeleteInsuredAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
