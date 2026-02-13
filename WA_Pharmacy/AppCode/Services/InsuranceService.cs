using AutoMapper;
using WA_Pharmacy.AppCode.Contracts.IService;
using WA_Pharmacy.AppCode.Repository;
using WA_Pharmacy.DTOs.Insurance;
using WA_Pharmacy.EFCore.Entities;
using WA_Pharmacy.EFCore.UnitOfWork;

namespace WA_Pharmacy.AppCode.Services
{
    public class InsuranceService : IInsuranceService
    {
        private readonly IGenericRepository<Insurance, long> _insuranceRepo;
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public InsuranceService(IGenericRepository<Insurance, long> insuranceRepo, IUnitOfWork uow, IMapper mapper)
        {
            _insuranceRepo = insuranceRepo;
            _uow = uow;
            _mapper = mapper;
        }

        public async Task<List<InsuranceListDto>> GetInsurancesForListAsync()
        {
            // اینجا چون تعداد کمه، ProjectTo عالی کار میکنه
            return await _insuranceRepo.GetProjectedAsync<InsuranceListDto>();
        }

        public async Task<InsuranceEditDto> GetInsuranceForEditAsync(long id)
        {
            return (await _insuranceRepo.GetProjectedAsync<InsuranceEditDto>(x => x.Id == id)).FirstOrDefault();
        }

        public async Task AddInsuranceAsync(InsuranceEditDto dto)
        {
            var insurance = _mapper.Map<Insurance>(dto);
            await _insuranceRepo.AddAsync(insurance);
            await _uow.SaveChangesAsync();
        }

        public async Task UpdateInsuranceAsync(InsuranceEditDto dto)
        {
            var existingInsurance = await _insuranceRepo.GetByIdAsync(dto.Id);
            if (existingInsurance == null) return;

            _mapper.Map(dto, existingInsurance);
            await _uow.SaveChangesAsync();
        }

        public async Task DeleteInsuranceAsync(long id)
        {
            await _insuranceRepo.DeleteAsync(id);
            await _uow.SaveChangesAsync();
        }


    }
}
