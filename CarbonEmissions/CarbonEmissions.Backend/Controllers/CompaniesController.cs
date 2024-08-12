using CarbonEmissions.Backend.UnitOfWork.Interfaces;
using CarbonEmissions.Shared.Entities;
using Microsoft.AspNetCore.Mvc;

namespace CarbonEmissions.Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CompaniesController : GenericController<Company>
    {
        private readonly ICompaniesUnitOfWork _companiesUnitOfWork;

        public CompaniesController(IGenericUnitOfWork<Company> unitOfWork, ICompaniesUnitOfWork companiesUnitOfWork) : base(unitOfWork)
        {
            _companiesUnitOfWork = companiesUnitOfWork;
        }

        [HttpGet("full")]
        public override async Task<IActionResult> GetAsync()
        {
            var response = await _companiesUnitOfWork.GetAsync();
            if (response.WasSuccess)
            {
                return Ok(response.Result);
            }
            return BadRequest();
        }

        [HttpGet("{id}")]
        public override async Task<IActionResult> GetAsync(int id)
        {
            var response = await _companiesUnitOfWork.GetAsync(id);
            if (response.WasSuccess)
            {
                return Ok(response.Result);
            }
            return NotFound(response.Message);  
        }
    }
}
