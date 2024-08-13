using CarbonEmissions.Backend.UnitOfWork.Interfaces;
using CarbonEmissions.Shared.Dtos;
using CarbonEmissions.Shared.Entities;
using CarbonEmissions.Shared.Mappers;
using Microsoft.AspNetCore.Mvc;

namespace CarbonEmissions.Backend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EmissionsController : GenericController<Emission>
    {
        private readonly IEmissionsUnitOfWork _emissionsUnitOfWork;

        public EmissionsController(IGenericUnitOfWork<Emission> unitOfWork, IEmissionsUnitOfWork emissionsUnitOfWork) : base(unitOfWork)
        {
            _emissionsUnitOfWork = emissionsUnitOfWork;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsync(int id, [FromQuery] CreateEmissionsDTO updateEmission)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var emission = updateEmission.toEmissionsFromCreate(id);

            await _emissionsUnitOfWork.UpdateAsync(emission);
            
            return Ok(new { message = "Actualizado" });
        }
    }
}