using CarbonEmissions.Shared.Dtos;
using CarbonEmissions.Shared.Entities;
using CarbonEmissions.Shared.Responses;

namespace CarbonEmissions.Backend.UnitOfWork.Interfaces
{
    public interface IEmissionsUnitOfWork
    {
        Task<ActionResponse<Emission>> GetAsync(int id);

        Task<ActionResponse<IEnumerable<Emission>>> GetAsync();

        Task<ActionResponse<Emission>> AddAsync(Emission emission);
    }
}
