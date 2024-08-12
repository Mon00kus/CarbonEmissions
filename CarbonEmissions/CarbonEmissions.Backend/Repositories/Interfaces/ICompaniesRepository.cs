using CarbonEmissions.Shared.Dtos;
using CarbonEmissions.Shared.Entities;
using CarbonEmissions.Shared.Responses;
using System.Diagnostics.Metrics;

namespace CarbonEmissions.Backend.Repositories.Interfaces
{
    public interface ICompaniesRepository
    {
        Task<ActionResponse<Company>> GetAsync(int id);

        Task<ActionResponse<IEnumerable<Company>>> GetAsync();

        //Task<ActionResponse<IEnumerable<Company>>> GetAsync(PaginationDTO pagination);

        //Task<ActionResponse<int>> GetTotalPagesAsync(PaginationDTO pagination);

        //Task<IEnumerable<Company>> GetComboAsync();
    }
}