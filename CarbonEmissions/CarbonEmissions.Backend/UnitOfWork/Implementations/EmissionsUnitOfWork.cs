using CarbonEmissions.Backend.Repositories.Interfaces;
using CarbonEmissions.Backend.UnitOfWork.Interfaces;
using CarbonEmissions.Shared.Entities;
using CarbonEmissions.Shared.Responses;

namespace CarbonEmissions.Backend.UnitOfWork.Implementations
{
    public class EmissionsUnitOfWork : GenericUnitOfWork<Emission>, IEmissionsUnitOfWork
    {
        private readonly IEmissionsRepository _emissionsRepo;

        public EmissionsUnitOfWork(IGenericRepository<Emission> repository, IEmissionsRepository emissionsRepository) : base(repository)
        {
            _emissionsRepo = emissionsRepository;
        }

        public Task<ActionResponse<Emission>> AddAsync(Emission emission)
        {
            throw new NotImplementedException();
        }

        public override async Task<ActionResponse<Emission>> GetAsync(int id) => await _emissionsRepo.GetAsync(id);

        public override async Task<ActionResponse<IEnumerable<Emission>>> GetAsync() => await _emissionsRepo.GetAsync();

        public override async Task<ActionResponse<Emission>> UpdateAsync(Emission emission) => await _emissionsRepo.UpdateAsync(emission);        
    }
}
