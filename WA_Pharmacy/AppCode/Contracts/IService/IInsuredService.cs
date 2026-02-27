using WA_Pharmacy.DTOs.Insured;

namespace WA_Pharmacy.AppCode.Contracts.IService
{
    public interface IInsuredService
    {
        Task<List<InsuredListDto>> GetInsuredsForListAsync();
        Task<InsuredListDto> GetInsuredDetailsAsync(long id);
        Task<InsuredEditDto> GetInsuredForEditAsync(long id);

        Task AddInsuredAsync(InsuredEditDto dto);
        Task UpdateInsuredAsync(InsuredEditDto dto);
        Task DeleteInsuredAsync(long id);
        
        // Additional methods specifically for Insured if needed
        Task<List<InsuredListDto>> GetInsuredsByCustomerIdAsync(long customerId);
    }
}
