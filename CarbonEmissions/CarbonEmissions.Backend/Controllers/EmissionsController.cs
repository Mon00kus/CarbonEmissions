using CarbonEmissions.Backend.UnitOfWork.Implementations;
using CarbonEmissions.Backend.UnitOfWork.Interfaces;
using CarbonEmissions.Shared.Dtos;
using CarbonEmissions.Shared.Entities;
using CarbonEmissions.Shared.Mappers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace CarbonEmissions.Backend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EmissionsController : GenericController<Emission>
    {
               
        public EmissionsController(IGenericUnitOfWork<Emission> unitOfWork/*, IEmissionsUnitOfWork emissionsUnitOfWork*/) : base(unitOfWork)
        {
        
        }

        
        //public override async Task<IActionResult> GetAsync()
        //{
        //    var response = await _emissionsUnitOfWork.GetAsync();
        //    if (response.WasSuccess)
        //    {
        //        return Ok(response.Result);
        //    }
        //    return BadRequest();
        //}

        //[HttpGet("emissions/{id}")]
        //public override async Task<IActionResult> GetAsync(int id)
        //{
        //    var response = await _emissionsUnitOfWork.GetAsync(id);
        //    if (response.WasSuccess)
        //    {
        //        return Ok(response.Result);
        //    }
        //    return NotFound(response.Message);
        //}

        //[HttpPost("/emissions")]
        //public async Task<IActionResult> PostAsync([FromBody] CreateEmissionsDTO createEmissionsDTO)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }
        //    var newEmission = createEmissionsDTO.toEmissionsFromCreate(1);
        //    await _emissionsUnitOfWork.AddAsync(newEmission);
        //    return Ok(newEmission);
        //}         

    //    [HttpPut("{id}")]
    //    public override async Task<IActionResult> PutAsync(int id, [FromQuery] CreateEmissionsDTO updateEmission)
    //    {
    //        if (!ModelState.IsValid)
    //        {
    //            return BadRequest(ModelState);
    //        }

    //        var company = _unitOfWork.GetAsync(id);           

    //        var model = updateEmission.toEmissionsFromCreate(company.Id);

    //        model.Id = id;
            
    //        await _unitOfWork.UpdateAsync(model);

    //        return Ok(new { message="Actualizado"});
    //    }
    }
}
