using AutoMapper;
using WA_Pharmacy.AppCode.Contracts.IService;
using WA_Pharmacy.AppCode.Repository;
using WA_Pharmacy.DTOs.Customer;
using WA_Pharmacy.EFCore.Entities;
using WA_Pharmacy.EFCore.UnitOfWork;

namespace WA_Pharmacy.AppCode.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly IGenericRepository<Customer, long> _customerRepo;


        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public CustomerService(IGenericRepository<Customer, long> customerRepo, IUnitOfWork uow, IMapper mapper)
        {
            _customerRepo = customerRepo;
            _uow = uow;
            _mapper = mapper;
        }
        public async Task<List<Customer>> GetAllCustomersAsync()
        {
            return await _customerRepo.GetAllAsync();
        }



        public async Task<List<Customer>> GetSomeCustomersAsync(int count)
        {
            return await _customerRepo.GetSomeAsync(count);
        }

        public async Task<Customer> GetCustomerByIdAsync(long id)
        {
            return await _customerRepo.GetByIdAsync(id);

        }

        public async Task<List<CustomerListDto>> GetCustomersForListAsync()
        {
            return await _customerRepo.GetProjectedAsync<CustomerListDto>();
        }

        public async Task<CustomerEditDto> GetCustomerForEditAsync(long id)
        {
            return (await _customerRepo.GetProjectedAsync<CustomerEditDto>(p => p.Id == id)).FirstOrDefault();
        }

        public async Task AddCustomerAsync(CustomerEditDto customerDto)
        {
            var customer = _mapper.Map<Customer>(customerDto);
            customer.RegisterDate = DateTime.Now;
            await _customerRepo.AddAsync(customer);
            await _uow.SaveChangesAsync();
        }

        public async Task UpdateCustomerAsync(CustomerEditDto customerDto)
        {
            var existingCustomer = await _customerRepo.GetByIdAsync(customerDto.Id);

            if (existingCustomer == null) return;


            _mapper.Map(customerDto, existingCustomer);

            await _uow.SaveChangesAsync();
        }

        public async Task DeleteCustomerAsync(long id)
        {
            await _customerRepo.DeleteAsync(id);
            await _uow.SaveChangesAsync();
        }
    }
}
