using Microsoft.AspNetCore.Mvc;

using WA_Pharmacy.AppCode.Contracts.IService;
using System.Threading.Tasks;

using WA_Pharmacy.DTOs.Prescription;
using Microsoft.AspNetCore.Mvc.Rendering;
using WA_Pharmacy.AppCode.Exceptions;

namespace WA_Pharmacy.Controllers
{
    public class PrescriptionController : Controller
    {
        private readonly IPrescriptionService _prescriptionService;
        private readonly ICustomerService _customerService;
        private readonly IDoctorService _doctorService;
        private readonly IMedicineService _medicineService;

        public PrescriptionController(
            IPrescriptionService prescriptionService,
            ICustomerService customerService,
            IDoctorService doctorService,
            IMedicineService medicineService)
        {
            _prescriptionService = prescriptionService;
            _customerService = customerService;
            _doctorService = doctorService;
            _medicineService = medicineService;
        }

        public async Task<IActionResult> Index()
        {
            var model = await _prescriptionService.GetAllPrescriptionsAsync();
            return View(model);
        }

        public async Task<IActionResult> Details(long? id)
        {
            if (id == null) return NotFound();

            var prescriptionDto = await _prescriptionService.GetPrescriptionByIdAsync(id.Value);
            
            if (prescriptionDto == null) return NotFound();

            return View(prescriptionDto);
        }

        // GET: Prescription/Create
        public async Task<IActionResult> Create()
        {
            await PopulateDropdownsAsync();
            return View();
        }

        // POST: Prescription/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PrescriptionCreateDto model)
        {
            if (ModelState.IsValid)
            {
                if (model.Items == null || !model.Items.Any())
                {
                    ModelState.AddModelError(string.Empty, "لطفا حداقل یک دارو به نسخه اضافه کنید.");
                }
                else
                {
                    try
                    {
                        await _prescriptionService.AddPrescriptionAsync(model);
                        // Redirect to Index
                        return RedirectToAction(nameof(Index));
                    }
                    catch (InsufficientStockException ex)
                    {
                        ModelState.AddModelError(string.Empty, ex.Message);
                    }
                    catch (Exception ex)
                    {
                        ModelState.AddModelError(string.Empty, "خطایی در ثبت نسخه رخ داد.");
                    }
                }
            }

            await PopulateDropdownsAsync(model.CustomerId, model.DoctorId);
            return View(model);
        }

        // GET: Prescription/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null) return NotFound();

            var model = await _prescriptionService.GetPrescriptionForEditAsync(id.Value);
            if (model == null) return NotFound();

            await PopulateMedicinesAsync();
            return View(model);
        }

        // POST: Prescription/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, PrescriptionEditDto model)
        {
            if (id != model.Id) return NotFound();

            if (ModelState.IsValid)
            {
                if (model.Items == null || !model.Items.Any())
                {
                    ModelState.AddModelError(string.Empty, "لطفا حداقل یک دارو به نسخه اضافه کنید.");
                }
                else
                {
                    try
                    {
                        await _prescriptionService.UpdatePrescriptionAsync(model);
                        return RedirectToAction(nameof(Index));
                    }
                    catch (InsufficientStockException ex)
                    {
                        ModelState.AddModelError(string.Empty, ex.Message);
                    }
                    catch (Exception ex)
                    {
                        ModelState.AddModelError(string.Empty, "خطایی در ویرایش نسخه رخ داد.");
                    }
                }
            }

            await PopulateMedicinesAsync();
            return View(model);
        }

        private async Task PopulateDropdownsAsync(long? customerId = null, int? doctorId = null)
        {
            var customers = await _customerService.GetCustomersForListAsync();
            var doctors = await _doctorService.GetDoctorsForListAsync();
            var medicines = await _medicineService.GetMedicinesForListAsync();

            ViewData["CustomerId"] = new SelectList(customers, "Id", "FullName", customerId);
            ViewData["DoctorId"] = new SelectList(doctors, "Id", "FullName", doctorId);
            
            // Serialize medicines context for our JavaScript dynamic select
            ViewBag.MedicinesJson = System.Text.Json.JsonSerializer.Serialize(medicines);
            // Also pass standard SelectList if you want a base hidden select template
            ViewBag.MedicinesList = new SelectList(medicines, "Id", "MedicineName");
        }

        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null) return NotFound();

            var model = await _prescriptionService.GetPrescriptionByIdAsync(id.Value);
            if (model == null) return NotFound();

            return View(model);
        }

        // POST: Prescription/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            await _prescriptionService.DeletePrescriptionAsync(id);
            return RedirectToAction(nameof(Index));
        }

        private async Task PopulateMedicinesAsync()
        {
            var medicines = await _medicineService.GetMedicinesForListAsync();
            ViewBag.MedicinesJson = System.Text.Json.JsonSerializer.Serialize(medicines);
            ViewBag.MedicinesList = new SelectList(medicines, "Id", "MedicineName");
        }
    }
}

