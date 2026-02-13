using WA_Pharmacy.DTOs.Insurance;

namespace WA_Pharmacy.AppCode.Contracts.IService
{
    public interface IInsuranceService
    {

        Task<List<InsuranceListDto>> GetInsurancesForListAsync();
        Task<InsuranceEditDto> GetInsuranceForEditAsync(long id);

        Task AddInsuranceAsync(InsuranceEditDto dto);
        Task UpdateInsuranceAsync(InsuranceEditDto dto);
        Task DeleteInsuranceAsync(long id);

    }
}
