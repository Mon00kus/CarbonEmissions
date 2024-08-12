using CarbonEmissions.Shared.Entities;
using CarbonEmissions.Shared.Responses;

namespace CarbonEmissions.Backend.UnitOfWork.Interfaces
{
    public interface ICompaniesUnitOfWork
    {
        Task<ActionResponse<Company>> GetAsync(int id);

        Task<ActionResponse<IEnumerable<Company>>> GetAsync();
    }
}
