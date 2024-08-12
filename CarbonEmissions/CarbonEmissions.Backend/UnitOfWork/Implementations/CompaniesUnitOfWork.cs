using CarbonEmissions.Backend.Repositories.Interfaces;
using CarbonEmissions.Backend.UnitOfWork.Interfaces;
using CarbonEmissions.Shared.Entities;
using CarbonEmissions.Shared.Responses;

namespace CarbonEmissions.Backend.UnitOfWork.Implementations
{
    public class CompaniesUnitOfWork : GenericUnitOfWork<Company>, ICompaniesUnitOfWork
    {
       
        private readonly ICompaniesRepository _countriesRepository;

        public CompaniesUnitOfWork(IGenericRepository<Company> repository, ICompaniesRepository companyRepository) : base(repository)
        {       
            _countriesRepository = companyRepository;
        }

        public override async Task<ActionResponse<Company>> GetAsync(int id) => await _countriesRepository.GetAsync(id);
        
        public override async Task<ActionResponse<IEnumerable<Company>>> GetAsync() => await _countriesRepository.GetAsync();        
    }
}
