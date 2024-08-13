using CarbonEmissions.Backend.Data;
using CarbonEmissions.Backend.Repositories.Interfaces;
using CarbonEmissions.Shared.Dtos;
using CarbonEmissions.Shared.Entities;
using CarbonEmissions.Shared.Responses;
using Microsoft.EntityFrameworkCore;

namespace CarbonEmissions.Backend.Repositories.Implementations
{
    public class EmissionsRepository : GenericRepository<Emission>, IEmissionsRepository
    {
        private readonly DataContext _context;

        public EmissionsRepository(DataContext context) : base(context)
        {
            _context = context;
        }

        public override async Task<ActionResponse<Emission>> GetAsync(int id)
        {
            var emission = await _context.Emissions.FirstOrDefaultAsync(e=>e.Id == id);
            if (emission == null)
            {
                return new ActionResponse<Emission>
                {
                    WasSuccess = false,
                    Message = "Emisión inexistente"
                };
            }
            return new ActionResponse<Emission>
            {
                WasSuccess = true,
                Result = emission
            };
        }     
        
        public override async Task<ActionResponse<IEnumerable<Emission>>> GetAsync()
        {
            var emissions = await _context.Emissions.ToListAsync();
            return new ActionResponse<IEnumerable<Emission>>
            {
                WasSuccess = true,
                Result = emissions
            };
        }

        public override async Task<ActionResponse<Emission>> AddAsync(Emission emission)
        {
            try
            {
                emission.EmissionDate.ToUniversalTime();
                await _context.Emissions.AddAsync(emission);
                await _context.SaveChangesAsync();
            }
            catch(Exception ex)
            {
                throw (ex.InnerException!);
            }
            return new ActionResponse<Emission>
            {
                WasSuccess = true,
                Result = emission
            };
        }        
    }
}