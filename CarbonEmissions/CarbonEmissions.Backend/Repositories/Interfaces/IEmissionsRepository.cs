using CarbonEmissions.Shared.Entities;
using CarbonEmissions.Shared.Responses;

namespace CarbonEmissions.Backend.Repositories.Interfaces
{
    public interface IEmissionsRepository
    {
        Task<ActionResponse<Emission>> GetAsync(int id);

        Task<ActionResponse<IEnumerable<Emission>>> GetAsync();

        Task<ActionResponse<Emission>> AddAsync(Emission emission);

        Task<ActionResponse<Emission>> UpdateAsync(Emission emission);
    }
}
