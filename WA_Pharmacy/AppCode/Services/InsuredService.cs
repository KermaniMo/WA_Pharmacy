using AutoMapper;
using WA_Pharmacy.AppCode.Contracts.IService;
using WA_Pharmacy.AppCode.Repository;
using WA_Pharmacy.DTOs.Insured;
using WA_Pharmacy.EFCore.Entities;
using WA_Pharmacy.EFCore.UnitOfWork;

namespace WA_Pharmacy.AppCode.Services
{
    public class InsuredService : IInsuredService
    {
        private readonly IGenericRepository<Insured, long> _insuredRepo;
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public InsuredService(IGenericRepository<Insured, long> insuredRepo, IUnitOfWork uow, IMapper mapper)
        {
            _insuredRepo = insuredRepo;
            _uow = uow;
            _mapper = mapper;
        }

        public async Task<List<InsuredListDto>> GetInsuredsForListAsync()
        {
            return await _insuredRepo.GetProjectedAsync<InsuredListDto>();
        }

        public async Task<InsuredListDto> GetInsuredDetailsAsync(long id)
        {
            return (await _insuredRepo.GetProjectedAsync<InsuredListDto>(x => x.Id == id)).FirstOrDefault();
        }

        public async Task<InsuredEditDto> GetInsuredForEditAsync(long id)
        {
            return (await _insuredRepo.GetProjectedAsync<InsuredEditDto>(x => x.Id == id)).FirstOrDefault();
        }

        public async Task AddInsuredAsync(InsuredEditDto dto)
        {
            var insured = _mapper.Map<Insured>(dto);
            await _insuredRepo.AddAsync(insured);
            await _uow.SaveChangesAsync();
        }

        public async Task UpdateInsuredAsync(InsuredEditDto dto)
        {
            var existingInsured = await _insuredRepo.GetByIdAsync(dto.Id);
            if (existingInsured == null) return;

            _mapper.Map(dto, existingInsured);
            await _uow.SaveChangesAsync();
        }

        public async Task DeleteInsuredAsync(long id)
        {
            await _insuredRepo.DeleteAsync(id);
            await _uow.SaveChangesAsync();
        }

        public async Task<List<InsuredListDto>> GetInsuredsByCustomerIdAsync(long customerId)
        {
            return await _insuredRepo.GetProjectedAsync<InsuredListDto>(x => x.CustomerId == customerId);
        }
    }
}
