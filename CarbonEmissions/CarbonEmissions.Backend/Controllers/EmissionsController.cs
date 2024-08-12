using CarbonEmissions.Backend.UnitOfWork.Interfaces;
using CarbonEmissions.Shared.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CarbonEmissions.Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmissionsController : GenericController<Emission>
    {
        public EmissionsController(IGenericUnitOfWork<Emission> unitOfWork) : base(unitOfWork)
        {
        }
    }
}
