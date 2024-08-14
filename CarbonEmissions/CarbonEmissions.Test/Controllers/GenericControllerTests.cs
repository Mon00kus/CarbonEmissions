using CarbonEmissions.Backend.UnitOfWork.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CarbonEmissions.Test.Controllers
{
    [TestClass]
    public  class GenericControllerTests<T> : Controller where T : class
    {
        private readonly IGenericUnitOfWork<T> _unitOfWork;

        public GenericControllerTests(IGenericUnitOfWork<T> unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet("all")]
        public virtual async Task<IActionResult> GetAsync()
        {
            var action = await _unitOfWork.GetAsync();
            if (action.WasSuccess)
            {
                return Ok(action.Result);
            }
            return BadRequest();
        }

    }
}
