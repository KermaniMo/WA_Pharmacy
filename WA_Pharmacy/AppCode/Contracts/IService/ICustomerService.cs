using WA_Pharmacy.DTOs.Customer;
using WA_Pharmacy.EFCore.Entities;

namespace WA_Pharmacy.AppCode.Contracts.IService
{
    public interface ICustomerService
    {


        Task<List<Customer>> GetAllCustomersAsync();
        Task<List<Customer>> GetSomeCustomersAsync(int count);
        Task<Customer> GetCustomerByIdAsync(long id);
        Task<List<CustomerListDto>> GetCustomersForListAsync();
        Task<CustomerEditDto> GetCustomerForEditAsync(long id);

        Task AddCustomerAsync(CustomerEditDto customerDto);
        Task UpdateCustomerAsync(CustomerEditDto customerDto);
        Task DeleteCustomerAsync(long id);

    }
}
