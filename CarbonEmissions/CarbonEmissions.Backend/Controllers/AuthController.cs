using CarbonEmissions.Backend.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CarbonEmissions.Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public AuthController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpPost("getToken")]
        public IActionResult Login()
        {
            try
            {
                // Llega la info del user por parametro, no se valida absoultamente nada porque es info statica
               
                var token = JwtEntesions.GetToken(_configuration);

                return Ok(new { Token = token });

            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }
    }
}
