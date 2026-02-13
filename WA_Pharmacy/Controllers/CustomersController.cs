using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WA_Pharmacy.AppCode.Contracts.IService;
using WA_Pharmacy.DTOs.Customer;

namespace WA_Pharmacy.Controllers
{
    public class CustomersController : Controller
    {
        private readonly ICustomerService _customerService;
        private readonly IMapper _mapper; // اسم استاندارد _mapper هست

        public CustomersController(ICustomerService customerService, IMapper mapper)
        {
            _customerService = customerService;
            _mapper = mapper;
        }

        // GET: Customers
        public async Task<IActionResult> Index()
        {
            var model = await _customerService.GetCustomersForListAsync();
            return View(model);
        }

        // GET: Customers/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null) return NotFound();

            var customer = await _customerService.GetCustomerByIdAsync(id.Value);
            if (customer == null) return NotFound();
            var dto = _mapper.Map<CustomerListDto>(customer);
            return View(dto);
        }

        // GET: Customers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Customers/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        // ورودی رو DTO کردم تا استاندارد بشه
        public async Task<IActionResult> Create(CustomerEditDto customerDto)
        {
            if (ModelState.IsValid)
            {
                //var customer = _mapper.Map<Customer>(customerDto);

                await _customerService.AddCustomerAsync(customerDto);
                return RedirectToAction(nameof(Index));
            }
            return View(customerDto);
        }

        // GET: Customers/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null) return NotFound();

            var customer = await _customerService.GetCustomerForEditAsync(id.Value);
            if (customer == null) return NotFound();


            return View(customer);
        }

        // POST: Customers/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, CustomerEditDto customerDto)
        {
            if (id != customerDto.Id) return NotFound();

            if (ModelState.IsValid)
            {

                await _customerService.UpdateCustomerAsync(customerDto);

                return RedirectToAction(nameof(Index));
            }
            return View(customerDto);
        }

        // GET: Customers/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null) return NotFound();

            var customer = _mapper.Map<CustomerListDto>(await _customerService.GetCustomerByIdAsync(id.Value));
            if (customer == null) return NotFound();

            return View(customer);
        }

        // POST: Customers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            await _customerService.DeleteCustomerAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}