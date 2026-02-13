using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WA_Pharmacy.AppCode.Contracts.IService;
using WA_Pharmacy.DTOs.Doctor;

namespace WA_Pharmacy.Controllers
{
    public class DoctorsController : Controller
    {
        private readonly IDoctorService _doctorService;
        private readonly IMapper _mapper;
        private readonly UserManager<IdentityUser> _userManager;

        public DoctorsController(IDoctorService doctorService, IMapper mapper, UserManager<IdentityUser> userManager)
        {
            _doctorService = doctorService;
            _mapper = mapper;
            _userManager = userManager;
        }

        // GET: Doctors
        public async Task<IActionResult> Index()
        {
            var model = await _doctorService.GetDoctorsForListAsync();
            return View(model);
        }

        // GET: Doctors/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var doctor = await _doctorService.GetDoctorByIdAsync(id.Value);
            if (doctor == null) return NotFound();

            var dto = _mapper.Map<DoctorListDto>(doctor);
            if (!string.IsNullOrEmpty(doctor.UserId))
            {
                var user = await _userManager.FindByIdAsync(doctor.UserId);
                if (user != null)
                {
                    dto.UserName = user.UserName; // قرار دادن اسم در DTO
                }
            }
            return View(dto);
        }

        // GET: Doctors/Create
        public async Task<IActionResult> Create()
        {
            await LoadUsersAsync();
            return View();
        }

        // POST: Doctors/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(DoctorEditDto doctorDto)
        {
            ModelState.Remove("DoctorNumber");
            if (ModelState.IsValid)
            {
                await _doctorService.AddDoctorAsync(doctorDto);
                return RedirectToAction(nameof(Index));
            }


            await LoadUsersAsync(doctorDto.UserId);
            return View(doctorDto);
        }

        // GET: Doctors/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var doctor = await _doctorService.GetDoctorForEditAsync(id.Value);
            if (doctor == null) return NotFound();

            await LoadUsersAsync(doctor.UserId);
            return View(doctor);
        }

        // POST: Doctors/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, DoctorEditDto doctorDto)
        {
            if (id != doctorDto.Id) return NotFound();

            if (ModelState.IsValid)
            {
                await _doctorService.UpdateDoctorAsync(doctorDto);
                return RedirectToAction(nameof(Index));
            }

            await LoadUsersAsync(doctorDto.UserId);
            return View(doctorDto);
        }
        private async Task LoadUsersAsync(string selectedUserId = null)
        {
            var users = await _userManager.Users.AsNoTracking().ToListAsync();
            ViewData["UserId"] = new SelectList(users, "Id", "UserName", selectedUserId);
        }
        // GET: Doctors/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var doctor = await _doctorService.GetDoctorByIdAsync(id.Value);
            if (doctor == null) return NotFound();

            var dto = _mapper.Map<DoctorListDto>(doctor);
            return View(dto);
        }

        // POST: Doctors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _doctorService.DeleteDoctorAsync(id);
            return RedirectToAction(nameof(Index));
        }


    }
}
