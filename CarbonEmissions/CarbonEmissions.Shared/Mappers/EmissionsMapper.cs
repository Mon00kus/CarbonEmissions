using CarbonEmissions.Shared.Dtos;
using CarbonEmissions.Shared.Entities;

namespace CarbonEmissions.Shared.Mappers
{
    public static class EmissionsMapper
    {
        public static Emission toEmissionsFromCreate(this CreateEmissionsDTO createEmissionsDTO, int companyId)
        {
            return new Emission
            {
                Id = 1, 
                Description = createEmissionsDTO.Description,
                Quantity = createEmissionsDTO.Quantity,
                EmissionDate = createEmissionsDTO.EmissionDate,
                EmissionType = createEmissionsDTO.EmissionType,
                CompanyId = companyId
            };
        }
    }
}