using CarbonEmissions.Backend.Data;
using CarbonEmissions.Backend.Repositories.Interfaces;
using CarbonEmissions.Shared.Entities;
using CarbonEmissions.Shared.Responses;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Metrics;

namespace CarbonEmissions.Backend.Repositories.Implementations
{
    public class CompaniesRepository : GenericRepository<Company>, ICompaniesRepository
    {
        private readonly DataContext _context;

        public CompaniesRepository(DataContext context) : base(context)
        {
            _context = context;
        }

        public override async Task<ActionResponse<Company>> GetAsync(int id)
        {
            var company = await _context.Companies.Include(c => c.Emissions).FirstOrDefaultAsync(c => c.Id == id);
            if (company == null)
            {
                return new ActionResponse<Company>
                {
                    WasSuccess = false,
                    Message = "Compañia no existe"
                };
            }
            return new ActionResponse<Company>
            {
                WasSuccess = true,
                Result = company
            };
        }

        public override async Task<ActionResponse<IEnumerable<Company>>> GetAsync()
        {            
            var companies = await _context.Companies.Include(c => c.Emissions).ToListAsync();
            return new ActionResponse<IEnumerable<Company>>
            {
                WasSuccess = true,
                Result = companies
            };
        }
    }
}