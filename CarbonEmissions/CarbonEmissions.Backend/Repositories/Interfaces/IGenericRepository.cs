﻿using CarbonEmissions.Shared.Dtos;
using CarbonEmissions.Shared.Responses;

namespace CarbonEmissions.Backend.Repositories.Interfaces
{
    public interface IGenericRepository<T>
    {
        Task<ActionResponse<T>> GetAsync(int id);
        Task<ActionResponse<IEnumerable<T>>> GetAsync();
        Task<ActionResponse<IEnumerable<T>>> GetAsync(PaginationDTO pagination);
        Task<ActionResponse<int>> GetTotalPagesAsync(PaginationDTO pagination);
        Task<ActionResponse<T>> AddAsync(T entity);
        Task<ActionResponse<T>> DeleteAsync(int id);
        Task<ActionResponse<T>> UpdateAsync(T entity);
    }
}
